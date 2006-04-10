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

		private static readonly Regex regexContentPage = new Regex(@"<a href=""http://www\.gyao\.jp/sityou/catetop/genre_id/(?<GenreId>gen\d{7})/"">.*?\r\n(<a href=""http://www\.gyao\.jp/sityou/catelist/pac_id/(?<PackageId>pac\d{7})/"">)?[\s\S]*?<td width=""459"" class=""title12b"">(?<Title>.*?)</td>[\s\S]*?((?<SeriesNumber>.*?)<!-- シリーズ番号 -->)?(&nbsp;&nbsp;&nbsp;)?(?<Subtitle>.*?)<!-- サブタイトル -->[\s\S]*?<b>[^:]*時間[^:]* : (?<Duration>.*?)</b>[\s\S]*?<td align=""left"">(?<Description1>.*?)</td>([\s\S]*?<td align=""left"">\r\n(?<Description2>.*?)</td>[\s\S]*?<td align=""left"" class=""text10"">\r\n(?<Description3>.*?)</td>[\s\S]*?<td align=""right"" class=""text10"">\r\n(?<Description4>.*?)</td>)?", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline);
		
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
				+ "&rateId=" + "bit" + ((int)bitrate).ToString("0000000")
				+ "&login_from=shityou"
				+ "&chapterNo="
				+ "&recommend="
				+ "&contents_id=");
		}
		public static Uri CreateRecommendPageUri(int contKey, GBitRate bitrate) {
			string contId = GContent.ConvertToIdFromKey(contKey);
			return new Uri(
				"http://www.gyao.jp/sityou/catedetail/?"
				+ "contentsId=" + contId
				+ "&rateId=" + "bit" + ((int)bitrate).ToString("0000000")
				+ "&login_from=shityou"
				+ "&chapterNo="
				+ "&recommend=1"
				+ "&contents_id=" + contId);
		}
		public static Uri CreatePlaylistUri(int contKey, int userNo, GBitRate bitrate) {
			return new Uri(
				"http://www.gyao.jp/sityou/asx.php?"
				+ "contentsId=" + GContent.ConvertToIdFromKey(contKey)
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
		internal static GContent DoDownload(int contKey, int packKey, ContentPropertiesOnPackagePage cpPac) {
			Uri uri = GContent.CreateDetailPageUri(contKey);
			TextReader reader = TextReader.Null;
			try {
				reader = new StreamReader(new WebClient().OpenRead(uri), Encoding.GetEncoding("Shift_JIS"));
				string allHtml = reader.ReadToEnd();

				string genreId;
				string packageId;
				string title;
				string seriesNumber;
				string subtitle;
				string duration;
				string description1;
				string description2;
				string description3;
				string description4;

				Match match = GContent.regexContentPage.Match(allHtml);
				if (match.Success) {
					genreId = match.Groups["GenreId"].Value;
					packageId = match.Groups["PackageId"].Value;
					title = HtmlUtility.HtmlToText(match.Groups["Title"].Value);
					seriesNumber = HtmlUtility.HtmlToText(match.Groups["SeriesNumber"].Value);
					subtitle = HtmlUtility.HtmlToText(match.Groups["Subtitle"].Value);
					duration = HtmlUtility.HtmlToText(match.Groups["Duration"].Value);
					description1 = HtmlUtility.HtmlToText(match.Groups["Description1"].Value);
					description2 = HtmlUtility.HtmlToText(match.Groups["Description2"].Value);
					description3 = HtmlUtility.HtmlToText(match.Groups["Description3"].Value);
					description4 = HtmlUtility.HtmlToText(match.Groups["Description4"].Value);
				} else {
					throw new ContentDownloadException("詳細ページの解釈ができなかった <" + uri.AbsoluteUri + ">");
				}

				int genreKey = GGenre.ConvertToKeyFromId(genreId);
				int packageKey = string.IsNullOrEmpty(packageId) ? packKey : GPackage.ConvertToKeyFromId(packageId);
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

		private string title = string.Empty;
		private string seriesNumber = string.Empty;
		private string subtitle = string.Empty;
		private string duration = string.Empty;
		private string description1 = string.Empty;
		private string description2 = string.Empty;
		private string description3 = string.Empty;
		private string description4 = string.Empty;

		private string deadline = string.Empty;
		private string summary = string.Empty;
		
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

		[OnDeserialized]//2.0.5.0
		private void OnDeserialized(StreamingContext context) {
			if (null == this.title) this.title = string.Empty;
			if (null == this.seriesNumber) this.seriesNumber = string.Empty;
			if (null == this.subtitle) this.subtitle = string.Empty;
			if (null == this.duration) this.duration = string.Empty;
			if (null == this.description1) this.description1 = string.Empty;
			if (null == this.description2) this.description2 = string.Empty;
			if (null == this.description3) this.description3 = string.Empty;
			if (null == this.description4) this.description4 = string.Empty;
			if (null == this.deadline) this.deadline = string.Empty;
			if (null == this.summary) this.summary = string.Empty;
		}
		
		public int ContentKey {
			get { return this.contentKey; }
			set { this.contentKey = value; }
		}
		//[XmlIgnore]
		public string ContentId {
			get { return GContent.ConvertToIdFromKey(this.ContentKey);}
			set { this.ContentKey = GContent.ConvertToKeyFromId(value); }
		}
		public int PackageKey {
			get { return this.packageKey; }
			set { this.packageKey = value; }
		}
		[XmlIgnore]
		public string PackageId {
			get { return GPackage.ConvertToIdFromKey(this.PackageKey); }
			set { this.PackageKey = GPackage.ConvertToKeyFromId(value); }
		}
		public int GenreKey {
			get { return this.genreKey; }
			set { this.genreKey = value; }
		}
		[XmlIgnore]
		public string GenreId {
			get { return GGenre.ConvertToIdFromKey(this.GenreKey); }
			set { this.GenreKey = GGenre.ConvertToKeyFromId(value); }
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
			if (this.Subtitle.Length > 0) {
				return "<" + this.ContentId + "> " + this.Title + " / " + this.Subtitle;
			} else {
				return "<" + this.ContentId + "> " + this.Title;
			}
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
