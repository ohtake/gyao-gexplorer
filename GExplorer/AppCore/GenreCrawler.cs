using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Net;
using System.IO;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.AppCore {
	sealed class GenreCrawler{
		private enum LinkType {
			AnchorOrFrame,
			Image,
		}
		private struct UriLinkTypePair : IEquatable<UriLinkTypePair> {
			private Uri uri;
			private LinkType linkType;
			
			public UriLinkTypePair(Uri uri, LinkType linkType) {
				this.uri = uri;
				this.linkType = linkType;
			}
			public Uri Uri {
				get { return this.uri; }
			}
			public LinkType LinkType {
				get { return this.linkType; }
			}
			
			public bool Equals(UriLinkTypePair other) {
				return this.LinkType == other.LinkType && this.Uri.Equals(other.Uri);
			}
		}
		private sealed class HtmlParserRegex{
			private static readonly Regex regexLinks = new Regex(@"<(?:(?:a(?:rea)?) [^>]*?href=""|i?frame [^>]*src="")(.+?)""", RegexOptions.Compiled);
			private static readonly Regex regexImgSrc = new Regex(@"<img src=""(.+?)""", RegexOptions.Compiled);
			private const string beginComment = "<!--";
			private const string endComment = "-->";
			
			private readonly Encoding enc;
			private readonly CookieContainer cookieContainer;
			private readonly CrawlOptions options;
			
			public HtmlParserRegex(Encoding encoding, CookieContainer cookieContainer, CrawlOptions options) {
				this.enc = encoding;
				this.cookieContainer = cookieContainer;
				this.options = options;
			}
			
			public List<UriLinkTypePair> DownloadAndExtractLinks(Uri uri) {
				List<UriLinkTypePair> links = new List<UriLinkTypePair>();

				TextReader reader = TextReader.Null;
				try {
					HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
					req.CookieContainer = this.cookieContainer;
					req.Timeout = this.options.Timeout;
					
					reader = new StreamReader(req.GetResponse().GetResponseStream(), this.enc);
					string line;
					bool isInComment = false;
					while (null != (line = reader.ReadLine())) {
					processComment://コメントは読み飛ばす
						if (isInComment) {
							int end = line.IndexOf(HtmlParserRegex.endComment);
							if (end >= 0) {
								isInComment = false;
								line = line.Substring(end + HtmlParserRegex.endComment.Length);
								goto processComment;
							} else {
								continue;
							}
						} else {
							int begin = line.IndexOf(HtmlParserRegex.beginComment);
							if (begin >= 0) {
								isInComment = true;
								line = line.Substring(begin + HtmlParserRegex.beginComment.Length);
								goto processComment;
							}
						}
						//リンクと画像の抽出
						for (Match m = HtmlParserRegex.regexLinks.Match(line); m.Success; m = m.NextMatch()) {
							try {
								links.Add(new UriLinkTypePair(new Uri(uri, m.Groups[1].Value), LinkType.AnchorOrFrame));
							} catch (UriFormatException) {
							}
						}
						for (Match m = HtmlParserRegex.regexImgSrc.Match(line); m.Success; m = m.NextMatch()) {
							try {
								links.Add(new UriLinkTypePair(new Uri(uri, m.Groups[1].Value), LinkType.Image));
							} catch (UriFormatException) {
							}
						}
					}
				} catch {
					reader.Close();
					throw;
				}
				return links;
			}
		}
		
		private sealed class CrawlProgressState : ICrawlProgressState{
			private readonly GenreCrawler owner;
			private int percentage;
			private int currentPhase;
			private string phaseName = string.Empty;
			private string message = string.Empty;

			public CrawlProgressState(GenreCrawler owner) {
				this.owner = owner;
			}
			
			public int MaxPhases {
				get { return GenreCrawler.MaxPhases; }
			}
			public int CurrentPhase {
				get { return this.currentPhase; }
				set { this.currentPhase = value; }
			}
			public string PhaseName {
				get { return this.phaseName; }
				set { this.phaseName = value; }
			}
			public int PhaseProgressPercentage {
				get { return this.percentage; }
				set { this.percentage = value; }
			}
			public string PhaseMessage {
				get { return this.message; }
				set { this.message = value; }
			}

			public int TotalPercentage {
				get {
					return 100 * (this.CurrentPhase - 1) / this.MaxPhases + this.PhaseProgressPercentage / this.MaxPhases;
				}
			}
			public string TotalMessage {
				get {
					return string.Format("フェーズ {0}/{1} {2}: {3}", this.CurrentPhase, this.MaxPhases, this.PhaseName, this.PhaseMessage);
				}
			}
		}
		
		private const int MaxPhases = 4;

		private static UriLinkTypePair ConvertUriWithoutQueryAndFragment(UriLinkTypePair pair) {
			if (string.IsNullOrEmpty(pair.Uri.Fragment) && string.IsNullOrEmpty(pair.Uri.Query)) {
				return pair;
			} else {
				return new UriLinkTypePair(new Uri(pair.Uri.GetLeftPart(UriPartial.Path)), pair.LinkType);
			}
		}
		
		private readonly GGenreClass genre;
		private readonly CrawlResult prevResult;
		private readonly CrawlOptions options;
		private readonly CacheController cacheController;
		private readonly CookieContainer cookieContainer;
		private readonly BackgroundWorker bw;
		
		private readonly HtmlParserRegex parser;
		private readonly CrawlProgressState ps;
		
		private Queue<Uri> pagesWaiting = new Queue<Uri>();
		private List<Uri> pagesSuccess = new List<Uri>();
		private List<Uri> pagesFailed = new List<Uri>();
		
		private Queue<int> pacsWaiting = new Queue<int>();
		private Dictionary<int, GPackageClass> pacsSuccess = new Dictionary<int, GPackageClass>();
		private List<int> pacsFailed = new List<int>();
		
		private Queue<int> contsWaiting = new Queue<int>();
		private Dictionary<int, GContentClass> contsSuccess = new Dictionary<int, GContentClass>();
		private List<int> contsFailed = new List<int>();
		
		private List<CrawlException> exceptions = new List<CrawlException>();
		
		private GenreCrawler() {
			this.ps = new CrawlProgressState(this);
		}
		public GenreCrawler(GGenreClass genre, CrawlResult prevResult, CrawlOptions options, CacheController cacheController, CookieContainer cookieContainer, BackgroundWorker bw)
			: this() {
			this.genre = genre;
			this.prevResult = prevResult;
			this.options = options;
			this.cacheController = cacheController;
			this.cookieContainer = cookieContainer;
			this.bw = bw;

			this.parser = new HtmlParserRegex(Encoding.GetEncoding("Shift_JIS"), cookieContainer, this.options);
		}
		
		public CrawlResult GetResult() {
			this.pagesWaiting.Enqueue(this.genre.TimetableRecentlyUpdatedFirstUri);
			this.pagesWaiting.Enqueue(this.genre.GenreTopPageUri);
			
			if (this.bw.CancellationPending) return null;
			this.StepPhaseAndReportProgress("一般ページの取得");
			this.CrawlPages();
			if (this.bw.CancellationPending) return null;
			this.StepPhaseAndReportProgress("シリーズ一覧ページの取得");
			this.FetchPackages();
			if (this.bw.CancellationPending) return null;
			this.StepPhaseAndReportProgress("詳細ページの取得またはキャッシュ取得");
			this.FetchContents();
			if (this.bw.CancellationPending) return null;
			this.StepPhaseAndReportProgress("クロール結果の作成");
			return this.CreateCrawlResult();
		}
		private void StepPhaseAndReportProgress(string phaseName) {
			this.ps.CurrentPhase++;
			this.ps.PhaseName = phaseName;
			this.ps.PhaseProgressPercentage = 0;
			this.ps.PhaseMessage = string.Empty;

			this.bw.ReportProgress(100*(this.ps.CurrentPhase - 1) / this.ps.MaxPhases, this.ps); 
		}
		private void ReportProgressInPhase(int percentage, string message) {
			this.ps.PhaseProgressPercentage = percentage;
			this.ps.PhaseMessage = message;
			
			this.bw.ReportProgress(100 * (this.ps.CurrentPhase - 1) / this.ps.MaxPhases, this.ps); 
		}
		private void IgnoreException(CrawlException ex) {
			this.exceptions.Add(ex);
		}
		private void CrawlPages() {
			while (this.pagesWaiting.Count > 0) {
				if (this.bw.CancellationPending) return;
				if (this.pagesSuccess.Count + this.pagesFailed.Count >= this.options.MaxNormalPages) {
					this.IgnoreException(new CrawlException("一般ページ数の取得数の上限に達した．"));
					return;
				}
				Uri uri = this.pagesWaiting.Dequeue();
				{
					int numerator = this.pagesSuccess.Count + this.pagesFailed.Count;
					int denominator = this.pagesWaiting.Count + numerator + 1;
					this.ReportProgressInPhase(100 * numerator / denominator, string.Format("{0}/{1} {2}", numerator, denominator, uri.PathAndQuery));
				}
				
				List<UriLinkTypePair> links;
				try {
					links = this.parser.DownloadAndExtractLinks(uri);
				} catch (Exception e){
					this.IgnoreException(new CrawlException(string.Format("<{0}> 一般ページの取得失敗．", uri.AbsolutePath), e));
					this.pagesFailed.Add(uri);
					continue;
				}
				this.pagesSuccess.Add(uri);

				links = links.ConvertAll<UriLinkTypePair>(GenreCrawler.ConvertUriWithoutQueryAndFragment);
				foreach (UriLinkTypePair pair in links) {
					//addMyList 関数は放送予定コンテンツをMyGyaOのリストへの追加にも使われるので無視する
					if (pair.Uri.AbsoluteUri.StartsWith(@"javascript:addMylist(")) {
						continue;
					}
					//ID抽出
					string id;
					if (GIdExtractor.TryExtractPackageId(pair.Uri, out id)) {
						int key = GConvert.ToPackageKey(id);
						if (!this.pacsWaiting.Contains(key)) {
							this.pacsWaiting.Enqueue(key);
						}
						continue;
					}
					if (GIdExtractor.TryExtractContentId(pair.Uri, out id)) {
						int key = GConvert.ToContentKey(id);
						if (!this.contsWaiting.Contains(key)) {
							this.contsWaiting.Enqueue(key);
						}
						continue;
					}
					//IDが取れなかったらクロールキューに追加
					if (this.IsInRestriction(pair.Uri)) {
						switch (pair.LinkType) {
							case LinkType.AnchorOrFrame:
								if (this.pagesWaiting.Contains(pair.Uri)) break;
								if (this.pagesSuccess.Contains(pair.Uri)) break;
								if (this.pagesFailed.Contains(pair.Uri)) break;
								if (uri.Equals(pair.Uri)) break;
								this.pagesWaiting.Enqueue(pair.Uri);
								break;
						}
					}
				}
			}
		}
		private void FetchPackages() {
			while (this.pacsWaiting.Count > 0) {
				if (this.bw.CancellationPending) return;

				int pacKey = this.pacsWaiting.Dequeue();
				{
					int numerator = this.pacsSuccess.Count + this.pacsFailed.Count;
					int denominator = this.pacsWaiting.Count + numerator + 1;
					this.ReportProgressInPhase(100 * numerator / denominator, string.Format("{0}/{1} {2}", numerator, denominator, GConvert.ToPackageId(pacKey)));
				}
				
				try {
					List<GContentClass> conts;
					GPackageClass pac = this.cacheController.FetchPackage(pacKey, out conts);
					if (!pac.GenreKey.HasValue || this.genre.GenreKey != pac.GenreKey) {
						this.IgnoreException(new CrawlException(string.Format("<{0}> 他ジャンルにより無視．", GConvert.ToPackageId(pacKey))));
						this.pacsFailed.Add(pacKey);
						continue;
					}
					this.pacsSuccess.Add(pacKey, pac);
					foreach (GContentClass cont in conts) {
						this.contsSuccess.Add(cont.ContentKey, cont);
					}
				} catch (Exception e) {
					this.pacsFailed.Add(pacKey);
					this.IgnoreException(new CrawlException(string.Format("<{0}> 取得失敗．", GConvert.ToPackageId(pacKey)), e));
				}
			}
		}
		private void FetchContents() {
			while (this.contsWaiting.Count > 0) {
				if (this.bw.CancellationPending) return;
				
				int contKey = this.contsWaiting.Dequeue();
				if(this.contsSuccess.ContainsKey(contKey)) continue;
				{
					int numerator = this.contsSuccess.Count + this.contsFailed.Count;
					int denominator = this.contsWaiting.Count + numerator +1;
					this.ReportProgressInPhase(100 * numerator / denominator, string.Format("{0}/{1} {2}", numerator, denominator, GConvert.ToContentId(contKey)));
				}
				GContentClass cont;
				try {
					cont = this.cacheController.FindContentOrFetchContent(contKey);
				} catch(Exception e) {
					this.contsFailed.Add(contKey);
					this.IgnoreException(new CrawlException(string.Format("<{0}> 取得失敗．", GConvert.ToContentId(contKey)), e));
					continue;
				}
				
				if (!cont.GenreKey.HasValue || this.genre.GenreKey != cont.GenreKey.Value) {
					this.IgnoreException(new CrawlException(string.Format("<{0}> 他ジャンルにより無視．", GConvert.ToContentId(contKey))));
					this.contsFailed.Add(contKey);
					continue;
				}
				if (cont.PackgeKey.HasValue) {
					int pacKey = cont.PackgeKey.Value;
					if (this.pacsSuccess.ContainsKey(pacKey) || this.pacsFailed.Contains(pacKey)) {
						this.contsSuccess.Add(contKey, cont);
					} else {
						try {
							List<GContentClass> conts;
							GPackageClass pac = this.cacheController.FetchPackage(pacKey, out conts);
							this.pacsSuccess.Add(pacKey, pac);
							foreach (GContentClass contInPac in conts) {
								this.contsSuccess.Add(contInPac.ContentKey, contInPac);
							}
						} catch (Exception e) {
							this.contsSuccess.Add(contKey, cont);
							this.pacsFailed.Add(pacKey);
							this.IgnoreException(new CrawlException(string.Format("<{0}> 取得失敗．", GConvert.ToPackageId(pacKey)), e));
						}
					}
				} else {
					this.contsSuccess.Add(contKey, cont);
				}
			}
		}
		private CrawlResult CreateCrawlResult() {
			this.ReportProgressInPhase(0, "作成中");
			List<int> cksDone = new List<int>();
			CrawlResult result = new CrawlResult();
			
			foreach (GContentClass cont in this.contsSuccess.Values) {
				result.Contents.Add(cont);
			}
			result.VisitedPages.AddRange(this.pagesSuccess);
			result.NotVisitedPages.AddRange(this.pagesWaiting);
			result.Exceptions.AddRange(this.exceptions);
			
			//比較
			if (null != prevResult) {
				SortedDictionary<int, GContentClass> prevDic = this.CreateContentsSortedDictionary(this.prevResult.Contents);
				SortedDictionary<int, GContentClass> nextDic = this.CreateContentsSortedDictionary(result.Contents);
				// 新規か変更
				foreach (KeyValuePair<int, GContentClass> np in nextDic) {
					GContentClass prevCont;
					if (prevDic.TryGetValue(np.Key, out prevCont)) {
						if (this.CheckIfModified(np.Value, prevCont)) {
							result.SortedCKeysModified.Add(np.Key);
						}
					} else {
						result.SortedCKeysNew.Add(np.Key);
					}
				}
				result.SortedCKeysNew.Sort();
				result.SortedCKeysModified.Sort();
				// 落ち
				foreach (KeyValuePair<int, GContentClass> pp in prevDic) {
					if (!nextDic.ContainsKey(pp.Key)) {
						result.DroppedContents.Add(pp.Value);
					}
				}
			} else {
				foreach (GContentClass cont in result.Contents) {
					result.SortedCKeysNew.Add(cont.ContentKey);
				}
				result.SortedCKeysNew.Sort();
			}
			return result;
		}
		private bool IsInRestriction(Uri uri) {
			return uri.AbsoluteUri.StartsWith(this.genre.GenreTopPageUri.AbsoluteUri);
		}
		private SortedDictionary<int, GContentClass> CreateContentsSortedDictionary(List<GContentClass> list) {
			SortedDictionary<int, GContentClass> dic = new SortedDictionary<int, GContentClass>();
			foreach (GContentClass cont in list) {
				dic.Add(cont.ContentKey, cont);
			}
			return dic;
		}
		private bool CheckIfModified(GContentClass a, GContentClass b) {
			if (a.ContentKey != b.ContentKey) return true;
			if (a.PackgeKey != b.PackgeKey) return true;
			if (a.GenreKey != b.GenreKey) return true;
			if (a.Title != b.Title) return true;
			if (a.SeriesNumber != b.SeriesNumber) return true;
			if (a.Subtitle != b.Subtitle) return true;
			if (a.SummaryHtml != b.SummaryHtml) return true;
			if (a.DurationValue != b.DurationValue) return true;
			if (a.DeadlineText != b.DeadlineText) return true;
			return false;
		}
	}
	
	public sealed class CrawlOptions {
		public CrawlOptions() {
		}
		
		private int maxNormalPages = 16;
		[Category("上限")]
		[DisplayName("一般ページ数の上限")]
		[Description("クローラが一般ページをクロールする際の一般ページ数の上限を指定します．")]
		[DefaultValue(16)]
		public int MaxNormalPages {
			get { return this.maxNormalPages; }
			set { this.maxNormalPages = value; }
		}
		
		private int timeout = 8000;
		[Category("通信")]
		[DisplayName("タイムアウト")]
		[Description("一般ページを取得するときのタイムアウトをミリ秒で指定します．")]
		[DefaultValue(8000)]
		public int Timeout {
			get { return this.timeout; }
			set { this.timeout = value; }
		}
	}
	
	interface ICrawlProgressState {
		int TotalPercentage { get;}
		string TotalMessage { get;}
	}

	[Serializable]
	public class CrawlException : Exception {
		protected CrawlException(SerializationInfo info, StreamingContext context)
			: base(info, context) {
		}
		
		public CrawlException(string message)
			: base(message) {
		}
		public CrawlException(string message, Exception innerException)
			: base(message, innerException) {
		}
	}
}
