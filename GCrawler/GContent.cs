using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Net;

namespace Yusen.GCrawler {
	public class GContent {
		private static readonly Regex regexAnchorHref = new Regex(@"http://www.gyao.jp/sityou/catedetail/contents_id/(cnt[0-9]+)/");
		private static readonly Regex regexAnchorJscript = new Regex(@"javascript:gotoDetail\((?:%20| )?'(cnt[0-9]+)'(?:%20| )?\);?");
		private static readonly Regex regexImageSrc = new Regex(@"http://www.gyao.jp/img/info/[a-z0-9]+/(cnt[0-9]+)_[0-9a-z]*\.(?:jpg|gif)");
		private static readonly IEnumerable<Regex> regexesExtractor;

		private static readonly Regex regexTitle = new Regex(@"<td class=""title12b"">(.*)</td>");
		private static readonly Regex regexSubtitle = new Regex(@"<td class=""title12"">(.*)</td><!--�T�u�^�C�g��-->");
		private static readonly Regex regexImageDir = new Regex(@"<img src=""(/img/info/[a-z0-9]+/{1,2})cnt[0-9]+_[0-9a-z]*\.(?:jpg|gif)""");
		private static readonly Regex regexEpisodeNum = new Regex(@"<td align=""left""><b>(.*)</b></td>");
		private static readonly Regex regexDuration = new Regex(@"<td align=""right""><b>�������� : (.*)</b></td>");
		private static readonly Regex regexDescription = new Regex(@"^(?:<td align=""[^""]*"">)?((?:[^<>]|<[Bb][Rr]>|(<[Aa][^>]*>)|</[Aa]>)+)</td>$");
		private const string endOfDescription = @"<table width=""770"" border=""0"" cellspacing=""0"" cellpadding=""0"">";
		
		static GContent() {
			GContent.regexesExtractor = new Regex[]{
				GContent.regexAnchorHref, GContent.regexAnchorJscript, GContent.regexImageSrc,};
		}
		
		public static void Serialize(string filename, GContent cont){
			XmlSerializer xs = new XmlSerializer(typeof(GContent));
			using (TextWriter writer = new StreamWriter(filename)) {
				xs.Serialize(writer, cont);
			}
		}
		public static bool TryDeserialize(string filename, out GContent cont) {
			XmlSerializer xs = new XmlSerializer(typeof(GContent));
			using (TextReader reader = new StreamReader(filename)) {
				try {
					cont = xs.Deserialize(reader) as GContent;
					return null != cont;
				} catch (FileNotFoundException) {
					cont = null;
					return false;
				}
			}
		}
		internal static bool TryExtractContentId(Uri uri, out string id) {
			foreach (Regex regex in GContent.regexesExtractor) {
				Match match = regex.Match(uri.AbsoluteUri);
				if (match.Success) {
					id = match.Groups[1].Value;
					return true;
				}
			}
			id = null;
			return false;
		}
		internal static string ExtractContentId(Uri uri) {
			string id;
			if (GContent.TryExtractContentId(uri, out id)) {
				return id;
			} else {
				throw new ArgumentException("������URI����R���e���c��ID���擾�ł��Ȃ������D\n������URI: " + uri.AbsoluteUri);
			}
		}
		internal static bool CanExtractContentId(Uri uri) {
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
				+ "&chapterNo=");
		}
		public static Uri CreateMediaFileUri(string contId, int userNo, GBitRate bitrate) {
			return new Uri("rtsp://wms.cd.gyao.jp/gyaovod01?QueryString="
				+ "contentsId=" + contId
				+ ":userNo=" + userNo.ToString()
				+ ":rateId=" + "bit" + ((int)bitrate).ToString("0000000"));
		}
		public static bool TryDownload(string contId, out GContent cont) {
			Uri uri = GContent.CreateDetailPageUri(contId);
			TextReader reader = null;
			try {
				reader = new StreamReader(new WebClient().OpenRead(uri), Encoding.GetEncoding("Shift_JIS"));
				string title;
				string subtitle;
				string imageDir;
				string episodeNum;
				string duration;
				StringBuilder description = new StringBuilder();

				if (!GContent.TryRegexForEachLine(reader, GContent.regexTitle, out title)) goto error;
				if (!GContent.TryRegexForEachLine(reader, GContent.regexSubtitle, out subtitle)) goto error;
				if (!GContent.TryRegexForEachLine(reader, GContent.regexImageDir, out imageDir)) goto error;
				if (!GContent.TryRegexForEachLine(reader, GContent.regexEpisodeNum, out episodeNum)) goto error;
				if (!GContent.TryRegexForEachLine(reader, GContent.regexDuration, out duration)) goto error;
				string line;
				while (null != (line = reader.ReadLine())) {
					if (GContent.endOfDescription == line) break;
					Match match = GContent.regexDescription.Match(line);
					if (match.Success) {
						string desc = match.Groups[1].Value;
						if (description.Length > 0) {
							description.Append("\n\n");
						}
						description.Append(HtmlUtility.HtmlToText(desc));
					}
				}
				cont = new GContent(contId, title, subtitle, imageDir, episodeNum, duration, description.ToString().Trim());
				return true;
			error:
				cont = null;
				return false;
			} catch {
				cont = null;
				return false;
			} finally {
				if (null != reader) reader.Close();
			}
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

		private string contentId;
		private string title;
		private string subtitle;
		private string imageDir;
		private string episodeNumber;
		private string duration;
		private string longDescription;
		private bool fromCache;
		
		public GContent() {
			this.fromCache = true;
		}
		private GContent(string contentId, string title, string subtitle, string imageDir, string episodeNumber, string duration, string longDescription) {
			this.contentId = contentId;
			this.title = title;
			this.contentId = contentId;
			this.subtitle = subtitle;
			this.imageDir = imageDir;
			this.episodeNumber = episodeNumber;
			this.duration  =duration;
			this.longDescription = longDescription;
			this.fromCache = false;
		}
		public string ContentId {
			get { return this.contentId; }
			set { this.contentId = value; }
		}
		public string Title {
			get { return this.title; }
			set { this.title = value; }
		}
		public string SubTitle {
			get { return this.subtitle; }
			set { this.subtitle = value; }
		}
		public string ImageDir {
			get { return this.imageDir; }
			set { this.imageDir = value; }
		}
		public string EpisodeNumber {
			get { return this.episodeNumber; }
			set { this.episodeNumber = value; }
		}
		public string Duration {
			get { return this.duration; }
			set { this.duration = value; }
		}
		public string LongDescription {
			get { return this.longDescription; }
			set { this.longDescription = value; }
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
		
		public override string ToString() {
			if (this.SubTitle.Length > 0) {
				return "<" + this.ContentId + "> " + this.Title + " / " + this.SubTitle;
			} else {
				return "<" + this.ContentId + "> " + this.Title;
			}
		}
	}
}