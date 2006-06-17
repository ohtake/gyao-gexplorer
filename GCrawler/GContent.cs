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
		
		private static readonly Regex regexId = new Regex(@"cnt\d{7}", RegexOptions.Compiled);
		private static readonly Regex regexContentPage = new Regex(
			@"<a href=""http://www\.gyao\.jp/sityou/catetop/genre_id/(?<GenreIdNavigation>gen\d{7})/"">.*?[\r\n]{1,2}(<a href=""http://www\.gyao\.jp/sityou/catelist/pac_id/(?<PackageIdNavigation>pac\d{7})/"">)?[\s\S]*?<td width=""459"" class=""title12b"">(?<Title>.*?)</td>[\s\S]*?((?<SeriesNumber>.*?)<!-- シリーズ番号 -->)?(&nbsp;&nbsp;&nbsp;)?(?<Subtitle>.*?)<!-- サブタイトル -->[\s\S]*?<b>[^:]*時間[^:]* : (?<Duration>.*?)</b>[\s\S]*?<td align=""left"">(?<Description1>.*?)</td>([\s\S]*?<td align=""left"">[\r\n]{1,2}(?<Description2>.*?)</td>[\s\S]*?<td align=""left"" class=""text10"">[\r\n]{1,2}(?<Description3>.*?)</td>[\s\S]*?<td align=""right"" class=""text10"">[\r\n]{1,2}(?<Description4>.*?)</td>)?([\s\S]*<div><a href=""http://www\.gyao\.jp/sityou_review/review_list\.php\?contents_id=cnt\d{7}&pac_id=(?<PackageIdReview>pac\d{7})"">)?",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline);
		
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
				throw new ArgumentException("引数のURIからコンテンツのIDを取得できなかった．引数のURI: " + uri.AbsoluteUri);
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
		public static string ConvertToIdFromKey(int key) {
			return string.Format("cnt{0:d7}", key);
		}
		public static int ConvertToKeyFromId(string id) {
			return int.Parse(id.Substring(3)); // 3 == "cnt".Length
		}

		public static Uri CreateDetailPageUri(int contKey) {
			return new Uri("http://www.gyao.jp/sityou/catedetail/contents_id/" + GContent.ConvertToIdFromKey(contKey) + "/");
		}
		public static Uri CreateImageUri(int contKey, string imageDir, char imageSizePostfix) {
			return new Uri("http://www.gyao.jp/img/info/" + imageDir + "/" + GContent.ConvertToIdFromKey(contKey) + "_" + imageSizePostfix + ".jpg");
		}
		public static Uri CreatePlayerPageUri(int contKey, GBitRate bitrate) {
			return new Uri(
				"http://www.gyao.jp/login/judge_cookie/?"
				+ "contentsId=" + GContent.ConvertToIdFromKey(contKey)
				+ "&rateId=" + GBitRateUtility.ConvertToIdFromKey(bitrate)
				+ "&login_from=shityou"
				+ "&chapterNo="
				+ "&recommend="
				+ "&contents_id=");
		}
		public static Uri CreatePlayerPageUri(int contKey, GBitRate bitrate, int chaperNo) {
			return new Uri(
				"http://www.gyao.jp/login/judge_cookie/?"
				+ "contentsId=" + GContent.ConvertToIdFromKey(contKey)
				+ "&rateId=" + GBitRateUtility.ConvertToIdFromKey(bitrate)
				+ "&login_from=shityou"
				+ "&chapterNo=" + chaperNo.ToString()
				+ "&recommend="
				+ "&contents_id=");
		}
		public static Uri CreateRecommendPageUri(int contKey, GBitRate bitrate) {
			string contId = GContent.ConvertToIdFromKey(contKey);
			return new Uri(
				"http://www.gyao.jp/sityou/catedetail/?"
				+ "login_from=shityou"
				+ "&contentsId=" + contId
				+ "&rateId=" + GBitRateUtility.ConvertToIdFromKey(bitrate)
				+ "&chapterNo="
				+ "&recommend=1"
				+ "&contents_id=" + contId);
		}
		public static Uri CreatePlaylistUri(int contKey, int userNo, GBitRate bitrate) {
			return new Uri(
				"http://www.gyao.jp/sityou/asx.php?"
				+ "contentsId=" + GContent.ConvertToIdFromKey(contKey)
				+ "&userNo=" + userNo.ToString()
				+ "&rateId=" + GBitRateUtility.ConvertToIdFromKey(bitrate)
				+ "&clipBegin=&clipNo=");
		}
		public static Uri CreatePlaylistUri(int contKey, int userNo, GBitRate bitrate, int chapterNo) {
			return new Uri(
				"http://www.gyao.jp/sityou/asx.php?"
				+ "contentsId=" + GContent.ConvertToIdFromKey(contKey)
				+ "&userNo=" + userNo.ToString()
				+ "&rateId=" + GBitRateUtility.ConvertToIdFromKey(bitrate)
				+ "&clipBegin=&clipNo="
				+ "&chapterNo=" + chapterNo.ToString());
		}
#if CLIP_RESUME
		public static Uri CreatePlaylistUri(string contId, int userNo, GBitRate bitrate, ClipResumeInfo resumeInfo) {
			return new Uri(
				"http://www.gyao.jp/sityou/asx.php?"
				+ "contentsId=" + contId
				+ "&userNo=" + userNo.ToString()
				+ "&rateId=" + GBitRateUtility.ConvertToIdFromKey(bitrate)
				+ "&clipBegin=" + resumeInfo.ClipBegin.ToString()
				+ "&clipNo=" + resumeInfo.ClipNo.ToString());
		}
#endif
		internal static GContent DoDownload(int contKey, int packKey, ContentPropertiesOnPackagePage cpPac) {
			Uri uri = GContent.CreateDetailPageUri(contKey);
			TextReader reader = TextReader.Null;
			try {
				reader = new StreamReader(new WebClient().OpenRead(uri), Encoding.GetEncoding("Shift_JIS"));
				string allHtml = reader.ReadToEnd();

				string genreIdNavigation;
				string packageIdNavigation;
				string title;
				string seriesNumber;
				string subtitle;
				string duration;
				string description1;
				string description2;
				string description3;
				string description4;
				string packageIdReview;

				Match match = GContent.regexContentPage.Match(allHtml);
				if (match.Success) {
					genreIdNavigation = match.Groups["GenreIdNavigation"].Value;
					packageIdNavigation = match.Groups["PackageIdNavigation"].Value;
					title = HtmlUtility.HtmlToText(match.Groups["Title"].Value);
					seriesNumber = HtmlUtility.HtmlToText(match.Groups["SeriesNumber"].Value);
					subtitle = HtmlUtility.HtmlToText(match.Groups["Subtitle"].Value);
					duration = HtmlUtility.HtmlToText(match.Groups["Duration"].Value);
					description1 = HtmlUtility.HtmlToText(match.Groups["Description1"].Value);
					description2 = HtmlUtility.HtmlToText(match.Groups["Description2"].Value);
					description3 = HtmlUtility.HtmlToText(match.Groups["Description3"].Value);
					description4 = HtmlUtility.HtmlToText(match.Groups["Description4"].Value);
					packageIdReview = match.Groups["PackageIdReview"].Value;
				} else {
					throw new ContentDownloadException(string.Format("詳細ページの解釈ができなかった <{0}>", GContent.ConvertToIdFromKey(contKey)));
				}

				int genreKey = GGenre.ConvertToKeyFromId(genreIdNavigation);
				int packageKey = packKey;
				if (0 == packKey) {
					if (!string.IsNullOrEmpty(packageIdNavigation)) {
						packageKey = GPackage.ConvertToKeyFromId(packageIdNavigation);
					} else if (!string.IsNullOrEmpty(packageIdReview)) {
						packageKey = GPackage.ConvertToKeyFromId(packageIdReview);
					}
				}
				ContentPropertiesOnContentPage cpCnt = new ContentPropertiesOnContentPage(genreKey, packageKey, title, seriesNumber, subtitle, duration, description1, description2, description3, description4);
				return new GContent(contKey, cpCnt, cpPac);
			}catch (ContentDownloadException) {
				throw;
			} catch (Exception e) {
				throw new ContentDownloadException("不明なエラー．内部例外を参照．", e);
			} finally {
				reader.Close();
			}
		}
		public static GContent DoDownload(int contKey) {
			return GContent.DoDownload(contKey, 0, new ContentPropertiesOnPackagePage("(不明なパッケージ)", "(不明なパッケージ)"));
		}
		public static GContent CreateDummyContent(int contKey, GGenre genre, string reason) {
			GContent dummy = new GContent(contKey, new ContentPropertiesOnContentPage(genre.GenreKey, 0, "(ダミー)", "(ダミー)", "(ダミー)", "(ダミー)", "(ダミー)", "(ダミー)", "(ダミー)", "(ダミー)"), new ContentPropertiesOnPackagePage("(ダミー)", reason));
			dummy.isDummy = true;
			return dummy;
		}
		
		private int contentKey;
		private int packageKey;
		private int genreKey;

		private string title;
		private string seriesNumber;
		private string subtitle;
		private string duration;
		private string description1;
		private string description2;
		private string description3;
		private string description4;

		private string deadline;
		private string summary;
		
		private bool fromCache;
		private bool isDummy;
		
		
		public GContent() {
			this.fromCache = true;
		}
		private GContent(int contKey, ContentPropertiesOnContentPage cpCnt, ContentPropertiesOnPackagePage cpPac) {
			this.contentKey = contKey;

			this.packageKey = cpCnt.PackageKey;
			this.genreKey = cpCnt.GenreKey;
			this.title = cpCnt.Title;
			this.seriesNumber = cpCnt.SeriesNumber;
			this.seriesNumber = cpCnt.SeriesNumber;
			this.subtitle = cpCnt.Subtitle;
			this.duration = cpCnt.Duration;
			this.description1 = cpCnt.Description1;
			this.description2 = cpCnt.Description2;
			this.description3 = cpCnt.Description3;
			this.description4 = cpCnt.Description4;
			
			this.deadline = cpPac.Deadline;
			this.summary = cpPac.Summary;
			
			this.fromCache = false;
			this.isDummy = false;
		}

		internal bool HasSameContentPropertiesOnPackagePage(ContentPropertiesOnPackagePage cpPac) {
			return this.deadline.Equals(cpPac.Deadline) && this.summary.Equals(cpPac.Summary);
		}
		internal void SetContentPropertiesOnPackagePage(ContentPropertiesOnPackagePage cpPac) {
			this.deadline = cpPac.Deadline;
			this.summary = cpPac.Summary;
		}

		public int ContentKey {
			get { return this.contentKey; }
			set { this.contentKey = value; }
		}
		[XmlIgnore]
		public string ContentId {
			get { return GContent.ConvertToIdFromKey(this.ContentKey);}
		}
		public int PackageKey {
			get { return this.packageKey; }
			set { this.packageKey = value; }
		}
		[XmlIgnore]
		public string PackageId {
			get { return GPackage.ConvertToIdFromKey(this.PackageKey); }
		}
		public int GenreKey {
			get { return this.genreKey; }
			set { this.genreKey = value; }
		}
		[XmlIgnore]
		public string GenreId {
			get { return GGenre.ConvertToIdFromKey(this.GenreKey); }
		}
		
		public string Title {
			get { return this.title; }
			set { this.title = value; }
		}
		public string SeriesNumber {
			get { return this.seriesNumber; }
			set { this.seriesNumber = value; }
		}
		public string Subtitle {
			get { return this.subtitle; }
			set { this.subtitle = value; }
		}
		public string Duration {
			get { return this.duration; }
			set { this.duration = value; }
		}
		public string Description1 {
			get { return this.description1; }
			set { this.description1 = value; }
		}
		public string Description2 {
			get { return this.description2; }
			set { this.description2 = value; }
		}
		public string Description3 {
			get { return this.description3; }
			set { this.description3 = value; }
		}
		public string Description4 {
			get { return this.description4; }
			set { this.description4 = value; }
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
			get { return GContent.CreateDetailPageUri(this.ContentKey); }
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
			return string.Format("<{0}> {1} / {2} / {3}", this.ContentId, this.Title, this.SeriesNumber, this.Subtitle);
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
		private int genreKey;
		private int packageKey;
		private string title;
		private string seriesNumber;
		private string subtitle;
		private string duration;
		private string description1;
		private string description2;
		private string description3;
		private string description4;

		internal ContentPropertiesOnContentPage(
				int genreKey, int packageKey,
				string title, string seriesNumber, string subtitle, string duration,
				string description1, string description2, string description3, string description4) {
			this.genreKey = genreKey;
			this.packageKey = packageKey;
			this.title = title;
			this.seriesNumber = seriesNumber;
			this.subtitle = subtitle;
			this.duration = duration;
			this.description1 = description1;
			this.description2 = description2;
			this.description3 = description3;
			this.description4 = description4;
		}

		public int GenreKey {
			get { return this.genreKey; }
		}
		public int PackageKey {
			get { return this.packageKey; }
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
		public string Description1 {
			get { return this.description1; }
		}
		public string Description2 {
			get { return this.description2; }
		}
		public string Description3 {
			get { return this.description3; }
		}
		public string Description4 {
			get { return this.description4; }
		}
	}
}
