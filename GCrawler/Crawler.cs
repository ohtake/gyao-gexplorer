using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Yusen.GCrawler {
	public class Crawler {
		private class CrawlerHelper{
			private static UriLinkTypePair ConvertUriWithoutFragment(UriLinkTypePair pair) {
				if (string.IsNullOrEmpty(pair.Uri.Fragment)) {
					return pair;
				} else {
					return new UriLinkTypePair(new Uri(pair.Uri.GetLeftPart(UriPartial.Query)), pair.LinkType);
				}
			}
			
			public event EventHandler<CrawlProgressEventArgs> CrawlProgress;

			private CrawlSettings settings;
			private GGenre genre;
			private IHtmlParser parser;
			private IContentCacheController cacheController;
			private IDeadlineTable deadLineDic;
			private Stack<Uri> waitingPages = new Stack<Uri>();
			private Queue<string> waitingPackages = new Queue<string>();
			private Queue<string> waitingContents = new Queue<string>();
			private List<Uri> visitedPages = new List<Uri>();
			private Dictionary<string, GPackage> visitedPackages = new Dictionary<string, GPackage>();
			private Dictionary<string, GContent> visitedContents = new Dictionary<string, GContent>();
			private Dictionary<string, bool> contentsCached = new Dictionary<string, bool>();
			private Dictionary<string, string> contPackRelations = new Dictionary<string, string>();
			private List<Exception> ignoredExceptions = new List<Exception>();

			public CrawlerHelper(CrawlSettings settings, GGenre genre, IHtmlParser parser, IContentCacheController cacheController, IDeadlineTable deadLineDic) {
				this.settings = settings;
				this.genre = genre;
				this.parser = parser;
				this.cacheController = cacheController;
				this.deadLineDic = deadLineDic;
				
				switch(settings.CrawlOrder) {
					case CrawlOrder.TopPageFirst:
						this.PushTimeTable();
						this.waitingPages.Push(this.genre.TopPageUri);
						break;
					case CrawlOrder.TimetableFirst:
						this.waitingPages.Push(this.genre.TopPageUri);
						this.PushTimeTable();
						break;
				}
			}
			private void PushTimeTable (){
				switch(this.settings.TimeTableSortType) {
					case TimetableSortType.RecentlyUpdatedFirst:
						this.waitingPages.Push(this.genre.TimetableRecentlyUpdatedFirstUri);
						break;
					case TimetableSortType.DeadlineNearFirst:
						this.waitingPages.Push(this.genre.TimetableDeadlineNearFirstUri);
						break;
				}
			}
			
			public CrawlResult StartCrawling() {
				this.CrawlPages();
				this.FetchPackages();
				this.FetchContents();
				return this.CreateCrawlResult(this.BuildGpcTree());
			}
			private void OnCrawlProgress(string message) {
				if (null != this.CrawlProgress) {
					this.CrawlProgress(this, new CrawlProgressEventArgs(
						message,
						this.visitedPages.Count + this.visitedPackages.Count + this.visitedContents.Count,
						this.waitingPages.Count + this.waitingPackages.Count + this.waitingContents.Count));
				}
			}
			private void OnIgnoringException(Exception e) {
				this.ignoredExceptions.Add(e);
			}
			/// <summary>
			/// パッケージとコンテンツ以外のページをクロールし，
			/// パッケージとコンテンツのIDを取得する．
			/// </summary>
			private void CrawlPages() {
				while (this.waitingPages.Count > 0) {
					if(this.visitedPages.Count >= this.settings.MaxPages) {
						this.OnIgnoringException(new Exception(string.Format("クロールする一般ページの上限数に達した． ({0}/{1})", this.settings.MaxPages, this.visitedPages.Count + this.waitingPages.Count)));
						this.waitingPages.Clear();
						return;
					}
					this.OnCrawlProgress(string.Format("フェーズ 1/4: 一般ページを取得中 ({0}/{1}) {2}",
						this.visitedPages.Count,
						this.visitedPages.Count + this.waitingPages.Count,
						this.waitingPages.Peek().PathAndQuery));
					
					Uri uri = this.waitingPages.Pop();
					this.visitedPages.Add(uri);
					List<UriLinkTypePair> links;
					try {
						links = this.parser.ExtractLinks(uri);
					} catch(Exception e) {
						this.OnIgnoringException(new Exception("一般ページの取得失敗． <" + uri.AbsoluteUri + ">", e));
						continue;
					}
					
					links = links.ConvertAll<UriLinkTypePair>(CrawlerHelper.ConvertUriWithoutFragment);
					foreach(UriLinkTypePair pair in links) {
						string id;
						if(GPackage.TryExtractPackageId(pair.Uri, out id)) {
							if(!this.waitingPackages.Contains(id)) {
								this.waitingPackages.Enqueue(id);
								continue;
							}
						}
						if(GContent.TryExtractContentId(pair.Uri, out id)) {
							if(!this.waitingContents.Contains(id)) {
								this.waitingContents.Enqueue(id);
								continue;
							}
						}
						if(this.IsInRestriction(pair.Uri)) {
							switch(pair.LinkType) {
								case LinkType.AnchorOrFrame:
									if(!this.visitedPages.Contains(pair.Uri) && !this.waitingPages.Contains(pair.Uri)) {
										this.waitingPages.Push(pair.Uri);
									}
									break;
							}
						}
					}
				}
			}
			/// <summary>
			/// パッケージページを読み込んでコンテンツのIDを取得する．
			/// </summary>
			private void FetchPackages() {
				while(this.waitingPackages.Count > 0){
					string packId = this.waitingPackages.Dequeue();
					this.OnCrawlProgress(string.Format("フェーズ 2/4: パッケージページを取得中 ({0}/{1}) {2}",
						this.visitedPackages.Count,
						this.visitedPackages.Count + this.waitingPackages.Count,
						packId));
					GPackage package;
					List<string> childContIds;
					try{
						package = GPackage.DoDownload(packId, this.deadLineDic, out childContIds);
					}catch(Exception e){
						this.OnIgnoringException(e);
						continue;
					}
					
					this.visitedPackages.Add(packId, package);
					foreach (string contId in childContIds) {
						if (!this.waitingContents.Contains(contId)) {
							this.waitingContents.Enqueue(contId);
						}
						this.contPackRelations.Add(contId, packId);
					}
				}
			}
			/// <summary>
			/// コンテンツページを読み込んで情報を取得する．
			/// もしキャッシュがあればキャッシュを利用する．
			/// </summary>
			private void FetchContents() {
				while(this.waitingContents.Count > 0){
					string contId = this.waitingContents.Dequeue();
					ContentCache cache;
					GContent content;
					if (this.cacheController.TryGetCache(contId, out cache)) {
						content = cache.Content;
						content.FromCache = true;
						this.OnCrawlProgress(string.Format("フェーズ 3/4: 詳細ページのキャッシュにヒット ({0}/{1}) {2}",
							this.visitedContents.Count,
							this.visitedContents.Count + this.waitingContents.Count,
							contId));
						this.visitedContents.Add(contId, content);
						this.contentsCached.Add(contId, true);
						continue;
					}
					this.OnCrawlProgress(string.Format("フェーズ 3/4: 詳細ページの取得中 ({0}/{1}) {2}",
						this.visitedContents.Count,
						this.visitedContents.Count + this.waitingContents.Count,
						contId));
					try {
						content = GContent.DoDownload(contId);
						//ダウンロード成功
						this.visitedContents.Add(contId, content);
						this.contentsCached.Add(contId, false);
						this.cacheController.AddCache(content);
						continue;
					} catch (ContentDownloadException e) {
						//ダウンロード失敗
						this.OnIgnoringException(e);
						content = GContent.CreateDummyContent(contId, this.genre, e.Message);
						this.visitedContents.Add(contId, content);
						this.contentsCached.Add(contId, false);
						continue;
					}
				}
			}
			/// <summary>
			/// クロール結果からジャンル，パッケージ，コンテンツの木構造を作る．
			/// </summary>
			private ReadOnlyCollection<GPackage> BuildGpcTree() {
				this.OnCrawlProgress("フェーズ 4/4: データ構造の解析中");
				//パッケージに含まれるコンテンツ
				List<GPackage> packages = new List<GPackage>();
				foreach (KeyValuePair<string, GPackage> packPair in this.visitedPackages) {
					packages.Add(packPair.Value);
					List<GContent> contents = new List<GContent>();
					foreach (KeyValuePair<string, string> cpPair in this.contPackRelations) {
						if (packPair.Key == cpPair.Value) {
							contents.Add(this.visitedContents[cpPair.Key]);
						}
					}
					packPair.Value.Contents = contents.AsReadOnly();
				}
				//どのパッケージに含まれているか不明なコンテンツ
				GPackage dummyPackage = GPackage.CreateDummyPackage();
				List<GContent> contentsWithoutPack = new List<GContent>();
				foreach (KeyValuePair<string, GContent> contPair in this.visitedContents) {
					if (!this.contPackRelations.ContainsKey(contPair.Key)) {
						contentsWithoutPack.Add(contPair.Value);
					}
				}
				if (contentsWithoutPack.Count > 0) {
					dummyPackage.Contents = contentsWithoutPack.AsReadOnly();
					packages.Add(dummyPackage);
				}
				return packages.AsReadOnly();
			}
			private CrawlResult CreateCrawlResult(ReadOnlyCollection<GPackage> packages) {
				int cDown = 0;
				int cCache = 0;
				foreach (KeyValuePair<string, bool> pair in this.contentsCached) {
					if (pair.Value) {
						cCache++;
					} else {
						cDown++;
					}
				}
				return new CrawlResult(this.genre, packages, this.visitedPages.AsReadOnly(), this.ignoredExceptions.AsReadOnly(), cDown, cCache);
			}
			private bool IsInRestriction(Uri uri) {
				return uri.AbsoluteUri.StartsWith(this.genre.RootUri.AbsoluteUri);
			}
			private Uri StartPageUri {
				get {return this.genre.TopPageUri;}
			}
		}
		
		public event EventHandler<CrawlProgressEventArgs> CrawlProgress;
		private readonly IHtmlParser parser;
		private readonly IContentCacheController cacheController;
		private readonly IDeadlineTable deadLineDic;

		public Crawler(IHtmlParser parser, IContentCacheController cacheController, IDeadlineTable deadLineDic) {
			this.parser = parser;
			this.cacheController = cacheController;
			this.deadLineDic = deadLineDic;
		}
		
		public CrawlResult Crawl(CrawlSettings settings, GGenre genre) {
			if (!genre.IsCrawlable) {
				throw new ArgumentException("ジャンル「" + genre.GenreName + "」はクロールできない．");
			}

			CrawlerHelper helper = new CrawlerHelper(settings, genre, this.parser, this.cacheController, this.deadLineDic);
			helper.CrawlProgress += delegate(object sender, CrawlProgressEventArgs e) {
				if (null != this.CrawlProgress) {
					this.CrawlProgress(sender, e);
				}
			};
			return helper.StartCrawling();
		}
	}

	public enum CrawlOrder {
		TopPageFirst,
		TimetableFirst,
	}
	public enum TimetableSortType {
		RecentlyUpdatedFirst=0,
		DeadlineNearFirst=1,
	}
	public class CrawlSettings {
		private int maxPages;
		private CrawlOrder crawlOrder;
		private TimetableSortType timeTableSortType;
		public CrawlSettings(int maxPages, CrawlOrder crawlOrder, TimetableSortType timeTableSortType) {
			this.maxPages = maxPages;
			this.crawlOrder = crawlOrder;
			this.timeTableSortType = timeTableSortType;
		}
		public int MaxPages {
			get { return this.maxPages; }
		}
		public CrawlOrder CrawlOrder {
			get { return this.crawlOrder; }
		}
		public TimetableSortType TimeTableSortType {
			get { return this.timeTableSortType; }
		}
	}

	public class CrawlProgressEventArgs : EventArgs {
		private readonly string message;
		private readonly int visited;
		private readonly int waiting;

		internal CrawlProgressEventArgs(string message, int visited, int waiting) {
			this.message = message;
			this.visited = visited;
			this.waiting = waiting;
		}
		public string Message {
			get { return this.message; }
		}
		public int Visited {
			get { return this.visited; }
		}
		public int Waiting {
			get { return this.waiting; }
		}
	}
	
	[Serializable]
	public class CrawlResult {
		public static CrawlResult Merge(GGenre genre, CrawlResult x, CrawlResult y) {
			if (!x.Success ||  !y.Success) {
				throw new ArgumentException("Successがfalseのはマージ不可．");
			}
			List<GPackage> packages = new List<GPackage>();
			packages.AddRange(x.Packages);
			packages.AddRange(y.Packages);
			List<Uri> pages = new List<Uri>();
			pages.AddRange(x.VisitedPages);
			pages.AddRange(y.VisitedPages);
			List<Exception> ignoredExceptinos = new List<Exception>();
			ignoredExceptinos.AddRange(x.IgnoredExceptions);
			ignoredExceptinos.AddRange(y.IgnoredExceptions);

			return new CrawlResult(
				genre, packages.AsReadOnly(), pages.AsReadOnly(), ignoredExceptinos.AsReadOnly(),
				x.ContentsDownloaded + y.ContentsDownloaded, x.ContentsCached + y.ContentsCached);
		}
		public static CrawlResult Merge(GGenre genre, IEnumerable<CrawlResult> results) {
			CrawlResult seed = new CrawlResult(
				genre,
				new ReadOnlyCollection<GPackage>(new List<GPackage>()),
				new ReadOnlyCollection<Uri>(new List<Uri>()),
				new ReadOnlyCollection<Exception>(new List<Exception>()),
				0, 0);
			foreach (CrawlResult result in results) {
				seed = CrawlResult.Merge(genre, seed, result);
			}
			return seed;
		}

		private readonly bool success;
		private readonly GGenre genre;
		private readonly ReadOnlyCollection<GPackage> packages;
		private readonly ReadOnlyCollection<Uri> visitedPages;
		private readonly ReadOnlyCollection<Exception> ignoredExceptions;
		private readonly int contentsDownloaded;
		private readonly int contentsCached;
		private readonly DateTime time;
		
		/// <summary>
		/// クロール成功時のクロール結果
		/// </summary>
		/// <param name="genre"></param>
		/// <param name="packages"></param>
		/// <param name="vPages"></param>
		/// <param name="cDown"></param>
		/// <param name="cCache"></param>
		internal CrawlResult(
				GGenre genre,
				ReadOnlyCollection<GPackage> packages, ReadOnlyCollection<Uri> vPages,
				ReadOnlyCollection<Exception> ignoredExceptions,
				int cDown, int cCache) {
			this.success = true;
			this.genre = genre;
			this.packages = packages;
			this.visitedPages = vPages;
			this.ignoredExceptions = ignoredExceptions;
			this.contentsDownloaded = cDown;
			this.contentsCached = cCache;
			this.time = DateTime.Now;
		}
		
		public GGenre Genre {
			get { return this.genre; }
		}
		public ReadOnlyCollection<GPackage> Packages {
			get { return this.packages; }
		}
		public ReadOnlyCollection<Uri> VisitedPages {
			get {return this.visitedPages;}
		}
		public ReadOnlyCollection<Exception> IgnoredExceptions {
			get { return this.ignoredExceptions; }
		}
		public int ContentsDownloaded {
			get { return this.contentsDownloaded; }
		}
		public int ContentsCached {
			get { return this.contentsCached; }
		}
		public bool Success {
			get { return this.success; }
		}
		public DateTime Time {
			get { return this.time; }
		}
	}
}
