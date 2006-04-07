using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Yusen.GCrawler {
	[Serializable]
	public class GContent {
		private static readonly XmlSerializer serializer = new XmlSerializer(typeof(GContent));
		
		private static readonly Regex regexId = new Regex("cnt[0-9]{7}", RegexOptions.Compiled);

		private static readonly Regex regexBreadGenre = new Regex(@"^<a href=""[^""]*"">(.*)</a> &gt; $", RegexOptions.Compiled);
		private static readonly Regex regexTitle = new Regex(@"^<td width=""459"" class=""title12b"">(.*)</td>$", RegexOptions.Compiled);
		private static readonly Regex regexSeriesAndSubtitle = new Regex(@"^(?:(.+)<!-- シリーズ番号 -->&nbsp;&nbsp;&nbsp;)?(.*)<!-- サブタイトル -->$", RegexOptions.Compiled);
		private static readonly Regex regexDuration = new Regex(@"^<b>[^:]*時間[^:]* : (.*)</b>$", RegexOptions.Compiled);
		private static readonly Regex regexImageDir = new Regex(@"<img src=""(/img/info/[a-z0-9]+/{1,2})cnt[0-9]+_[0-9a-z]*\.(?:jpg|gif)""", RegexOptions.Compiled); // 村上さんはなぜか / が2つ
		private static readonly Regex regexDescription = new Regex(@"^\s*(?:<td align=""[^""]*"">)?(.+)</td>$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		
		public static void Serialize(string filename, GContent cont){
			using (TextWriter writer = new StreamWriter(filename)) {
				GContent.serializer.Serialize(writer, cont);
			}
		}
		public static bool TryDeserialize(string filename, out GContent cont) {
			using (TextReader reader = new StreamReader(filename)) {
				try {
					cont = GContent.serializer.Deserialize(reader) as GContent;
					return null != cont;
				} catch (FileNotFoundException) {
					cont = null;
					return false;
				}
			}
		}
		public static bool TryExtractContentId(Uri uri, out string id) {
			Match match = GContent.regexId.Match(uri.AbsoluteUri);
			if (match.Success) {
				id = match.Value;
				return true;
			}
			id = null;
			return false;
		}
		public static string ExtractContentId(Uri uri) {
			string id;
			if (GContent.TryExtractContentId(uri, out id)) {
				return id;
			} else {
				throw new ArgumentException("引数のURIからコンテンツのIDを取得できなかった．\n引数のURI: " + uri.AbsoluteUri);
			}
		}
		public static string[] ExtractContentIds(string text) {
			List<string> ids = new List<string>();
			for (Match match = GContent.regexId.Match(text); match.Success; match = match.NextMatch()) {
				ids.Add(match.Value);
			}
			return ids.ToArray();
		}
		public static bool CanExtractContentId(Uri uri) {
			string dummy;
			return GContent.TryExtractContentId(uri, out dummy);
		}
		public static Uri CreateDetailPageUri(string contId) {
			return new Uri("http://www.gyao.jp/sityou/catedetail/contents_id/" + contId + "/");
		}
		public static Uri CreatePlayerPageUri(string contId, GBitRate bitrate) {
			return new Uri(
				"http://www.gyao.jp/login/judge_cookie/?"
				+ "contentsId=" + contId
				+ "&rateId=" + "bit" + ((int)bitrate).ToString("0000000")
				+ "&login_from=shityou"
				+ "&chapterNo="
				+ "&recommend="
				+ "&contents_id=");
		}
		public static Uri CreateRecommendPageUri(string contId, GBitRate bitrate) {
			return new Uri(
				"http://www.gyao.jp/sityou/catedetail/?"
				+ "contentsId=" + contId
				+ "&rateId=" + "bit" + ((int)bitrate).ToString("0000000")
				+ "&login_from=shityou"
				+ "&chapterNo="
				+ "&recommend=1"
				+ "&contents_id=" + contId);
		}
		public static Uri CreatePlaylistUri(string contId, int userNo, GBitRate bitrate) {
			return new Uri(
				"http://www.gyao.jp/sityou/asx.php?"
				+ "contentsId=" + contId
				+ "&userNo=" + userNo
				+ "&rateId=" + "bit" + ((int)bitrate).ToString("0000000"));
		}
		public static Uri CreatePlaylistUri(string contId, int userNo, GBitRate bitrate, int chapterNo) {
			return new Uri(
				"http://www.gyao.jp/sityou/asx.php?"
				+ "contentsId=" + contId
				+ "&userNo=" + userNo
				+ "&rateId=" + "bit" + ((int)bitrate).ToString("0000000")
				+ "&chapterNo=" + chapterNo.ToString());
		}
#if CLIP_RESUME
		public static Uri CreatePlaylistUri(string contId, int userNo, GBitRate bitrate, ClipResumeInfo resumeInfo) {
			return new Uri(
				"http://www.gyao.jp/sityou/asx.php?"
				+ "contentsId=" + contId
				+ "&userNo=" + userNo
				+ "&rateId=" + "bit" + ((int)bitrate).ToString("0000000")
				+ "&clipBegin=" + resumeInfo.ClipBegin.ToString()
				+ "&clipNo=" + resumeInfo.ClipNo.ToString());
		}
#endif
		internal static GContent DoDownload(string contId, ContentPropertiesOnPackagePage cpPac) {
			Uri uri = GContent.CreateDetailPageUri(contId);
			TextReader reader = null;
			try {
				reader = new StreamReader(new WebClient().OpenRead(uri), Encoding.GetEncoding("Shift_JIS"));
				string genre;
				string title;
				string seriesNum;
				string subtitle;
				string duration;
				string imageDir;
				StringBuilder description = new StringBuilder();

				if (!GContent.TryRegexForEachLine(reader, GContent.regexBreadGenre, out genre)) {
					throw new ContentDownloadException("ジャンル名の読み取り失敗 <" + uri.AbsoluteUri + ">");
				}
				if (!GContent.TryRegexForEachLine(reader, GContent.regexTitle, out title)) {
					throw new ContentDownloadException("タイトルの読み取り失敗 <" + uri.AbsoluteUri + ">");
				}
				if (!GContent.TryRegexForEachLine(reader, GContent.regexSeriesAndSubtitle, out seriesNum, out subtitle)) {
					throw new ContentDownloadException("シリーズ番号とサブタイトルの読み取り失敗 <" + uri.AbsoluteUri + ">");
				}
				if (!GContent.TryRegexForEachLine(reader, GContent.regexDuration, out duration)) {
					throw new ContentDownloadException("時間の読み取り失敗 <" + uri.AbsoluteUri + ">");
				}
				if (!GContent.TryRegexForEachLine(reader, GContent.regexImageDir, out imageDir)) {
					throw new ContentDownloadException("画像のあるディレクトリ名の読み取り失敗 <" + uri.AbsoluteUri + ">");
				}
				int emptyLineCount = 0;
				while (true) {
					string line = reader.ReadLine();
					if (string.IsNullOrEmpty(line)) {
						emptyLineCount++;
						if (emptyLineCount >= 2) {
							break;//2度目の空行で終了
						}
					}
					Match match = GContent.regexDescription.Match(line);
					if (match.Success) {
						string desc = HtmlUtility.HtmlToText(match.Groups[1].Value).Trim();
						if (!string.IsNullOrEmpty(desc)) {
							if (description.Length > 0) {
								description.Append("\n\n");
							}
							description.Append(desc);
						}
					}
				}
				ContentPropertiesOnContentPage cpCnt = new ContentPropertiesOnContentPage(genre, title, seriesNum, subtitle, duration, description.ToString(), imageDir);
				return new GContent(contId, cpCnt, cpPac);
			} catch (ContentDownloadException) {
				throw;
			} catch (Exception e) {
				throw new ContentDownloadException("不明なエラー．内部例外を参照．", e);
			} finally {
				if (null != reader) reader.Close();
			}
		}
		public static GContent DoDownload(string contId) {
			return GContent.DoDownload(contId, ContentPropertiesOnPackagePage.Empty);
		}
		private static bool TryRegexForEachLine(TextReader reader, Regex regex, out string group1) {
			string line;
			while (null != (line = reader.ReadLine())) {
				Match match = regex.Match(line);
				if (match.Success) {
					group1 = HtmlUtility.HtmlToText(match.Groups[1].Value);
					return true;
				}
			}
			group1 = null;
			return false;
		}
		private static bool TryRegexForEachLine(TextReader reader, Regex regex, out string group1, out string group2) {
			string line;
			while (null != (line = reader.ReadLine())) {
				Match match = regex.Match(line);
				if (match.Success) {
					group1 = HtmlUtility.HtmlToText(match.Groups[1].Value);
					group2 = HtmlUtility.HtmlToText(match.Groups[2].Value);
					return true;
				}
			}
			group1 = group2 = null;
			return false;
		}
		public static GContent CreateDummyContent(string contId, GGenre genre, string reason) {
			return new GContent(contId, new ContentPropertiesOnContentPage("(ダミー)", "(ダミー)", "(ダミー)", "(ダミー)", "(ダミー)", reason, "/img/info/" + genre.ImageDirName + "/"), new ContentPropertiesOnPackagePage("(ダミー)", reason));
		}
		
		private string contentId;
		private string genre;
		private string title;
		private string subtitle;
		private string imageDir;
		
		[Obsolete("seriesNumberに名称変更"), OptionalField]//2.0.3.2でObsolete
		private string episodeNumber;
		[OptionalField]//2.0.3.2で追加
		private string seriesNumber;
		
		private string duration;
		private string longDescription;
		private bool fromCache;
		private bool isDummy;
		
		[OptionalField]//2.0.4.0で追加
		private string deadline = string.Empty;
		[OptionalField]//2.0.4.0で追加
		private string summary = string.Empty;
		
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context) {
			//2.0.3.3における episodeNumber -> seriesNumber
			if (null == this.seriesNumber) {
				if (null == this.episodeNumber) {
					this.seriesNumber = string.Empty;
				} else {
					this.seriesNumber = this.episodeNumber;
				}
			} else {
				this.episodeNumber = null;
			}
			//2.0.4.0で追加した deadline と summary
			if (null == this.deadline) this.deadline = string.Empty;
			if (null == this.subtitle) this.subtitle = string.Empty;
		}
		
		public GContent() {
			this.fromCache = true;
		}
		private GContent(string contentId, ContentPropertiesOnContentPage cpCnt, ContentPropertiesOnPackagePage cpPac) {
			this.contentId = contentId;
			
			this.genre = cpCnt.GenreName;
			this.title = cpCnt.Title;
			this.seriesNumber = cpCnt.SeriesNumber;
			this.subtitle = cpCnt.Subtitle;
			this.duration = cpCnt.Duration;
			this.longDescription = cpCnt.Description;
			this.imageDir = cpCnt.ImageDirectory;
			
			this.deadline = cpPac.Deadline;
			this.summary = cpPac.Summary;
			
			this.fromCache = false;
			this.isDummy = false;
		}
		
		public string ContentId {
			get { return this.contentId; }
			set { this.contentId = value; }
		}
		public string GenreName {
			get { return this.genre; }
			set { this.genre = value; }
		}
		public string Title {
			get { return this.title; }
			set { this.title = value; }
		}
		
		public string SeriesNumber {
			get { return this.seriesNumber; }
			set { this.seriesNumber = value; }
		}

		public string SubTitle {
			get { return this.subtitle; }
			set { this.subtitle = value; }
		}
		public string ImageDir {
			get { return this.imageDir; }
			set { this.imageDir = value; }
		}
		
		public string Duration {
			get { return this.duration; }
			set { this.duration = value; }
		}
		public string LongDescription {
			get { return this.longDescription; }
			set { this.longDescription = value; }
		}

		public string Summary {
			get { return this.summary; }
			set { this.summary = value; }
		}
		public string Deadline {
			get { return this.deadline; }
			set { this.deadline = value; }
		}
		
		[XmlIgnore]
		public Uri DetailPageUri {
			get { return GContent.CreateDetailPageUri(this.ContentId); }
		}
		[XmlIgnore]
		public Uri ImageLargeUri{
			get{
				return new Uri(this.DetailPageUri, this.imageDir + this.ContentId + "_l.jpg");
			}
		}
		[XmlIgnore]
		public Uri ImageSmallUri {
			get{
				return new Uri(this.DetailPageUri, this.imageDir + this.ContentId + "_s.jpg");
			}
		}
		[XmlIgnore]
		public bool FromCache {
			get { return this.fromCache; }
			internal set { this.fromCache = value; }
		}
		[XmlIgnore]
		public bool IsDummy {
			get { return this.isDummy; }
		}
		
		public override string ToString() {
			if (this.SubTitle.Length > 0) {
				return "<" + this.ContentId + "> " + this.Title + " / " + this.SubTitle;
			} else {
				return "<" + this.ContentId + "> " + this.Title;
			}
		}

		[Obsolete]//2.0.4.0での追加フィールド用
		internal bool HasSameCpPac(ContentPropertiesOnPackagePage cpPac) {
			return cpPac.Deadline.Equals(this.deadline) && cpPac.Summary.Equals(this.summary);
		}
		[Obsolete]//2.0.4.0での追加フィールド用
		internal void SetNewCpPac(ContentPropertiesOnPackagePage cpPac) {
			this.deadline = cpPac.Deadline;
			this.summary = cpPac.Summary;
		}
	}
	
	[Serializable]
	public class ContentDownloadException : Exception {
		protected ContentDownloadException(SerializationInfo info, StreamingContext context):base(info,context){
		}
		
		public ContentDownloadException(string message)
			: base(message) {
		}
		public ContentDownloadException(string message, Exception innerException)
			: base(message, innerException) {
		}
	}
	
	internal sealed class ContentPropertiesOnContentPage {
		private string genreName;
		private string title;
		private string seriesNumber;
		private string subtitle;
		private string duration;
		private string description;
		private string imageDir;

		internal ContentPropertiesOnContentPage(string genreName, string title, string seriesNumber, string subtitle, string duration, string description, string imageDir) {
			this.genreName = genreName;
			this.title = title;
			this.seriesNumber = seriesNumber;
			this.subtitle = subtitle;
			this.duration = duration;
			this.description = description;
			this.imageDir = imageDir;
		}

		public string GenreName {
			get { return this.genreName; }
		}
		public string Title {
			get { return this.title; }
		}
		public string SeriesNumber {
			get { return this.seriesNumber; }
		}
		public string Subtitle {
			get { return this.subtitle; }
		}
		public string Duration {
			get { return this.duration; }
		}
		public string Description {
			get { return this.description; }
		}
		public string ImageDirectory {
			get { return this.imageDir; }
		}
	}
}
