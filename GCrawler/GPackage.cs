using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Yusen.GCrawler {
	[Serializable]
	public class GPackage {
		private readonly static Regex regexId = new Regex("pac[0-9]{7}", RegexOptions.Compiled);

		private static readonly Regex regexPackagePackage = new Regex(@"<a href=""http://www\.gyao\.jp/sityou/catetop/genre_id/(?<GenreId>gen\d{7})/"">[\s\S]*?<td class=""title12b"">(?<PackageName>.*?)<!-- パックタイトル -->[\s\S]*?<b>(?<CatchCopy>.*?)<!-- パックキャッチコピー --></b>[\s\S]*?<td>(?<PackageText1>.*?)<!-- パックテキスト１ --></td>", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
		private static readonly Regex regexPackageContent = new Regex(@"<img src=""/img/info/[a-z]*?/{1,2}(?<ContentId>cnt\d{7})_s.jpg"" width=""80"" height=""60"" border=""0""><!-- サムネイル -->[\s\S]*?<td width=""235"" valign=""top"" class=""text10"">(?<Summary>.*)<!-- サマリー --></td>[\s\S]*?<td height=""14"" colspan=""2"" class=""bk10"">\r\n(?<Deadline>.*)</t[dr]>", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
		
		public static bool TryExtractPackageId(Uri uri, out string id) {
			Match match = GPackage.regexId.Match(uri.AbsoluteUri);
			if (match.Success) {
				id = match.Value;
				return true;
			} else {
				id = null;
				return false;
			}
		}
		public static string ExtractPackageId(Uri uri) {
			string id;
			if (GPackage.TryExtractPackageId(uri, out id)) {
				return id;
			} else {
				throw new ArgumentException("引数のURIからパッケージのIDを取得できなかった．引数のURI: " + uri.AbsoluteUri);
			}
		}
		public static bool CanExtractPackageId(Uri uri) {
			string dummy;
			return GPackage.TryExtractPackageId(uri, out dummy);
		}
		public static string ConvertToIdFromKey(int key) {
			return string.Format("pac{0:d7}", key);
		}
		public static int ConvertToKeyFromId(string id) {
			return int.Parse(id.Substring(3)); // 3 == "pac".Length
		}

		public static Uri CreatePackagePageUri(string packageId) {
			return new Uri("http://www.gyao.jp/sityou/catelist/pac_id/" + packageId + "/");
		}
		public static Uri CreatePackagePageUri(int packageKey) {
			return GPackage.CreatePackagePageUri(GPackage.ConvertToIdFromKey(packageKey));
		}

		internal static GPackage DoDownload(int packKey, out List<int> childContKeys, SortedDictionary<int, ContentPropertiesOnPackagePage> cpPacs) {
			string packId = GPackage.ConvertToIdFromKey(packKey);
			Uri uri = GPackage.CreatePackagePageUri(packId);
			using (TextReader reader = new StreamReader(new WebClient().OpenRead(uri), Encoding.GetEncoding("Shift_JIS"))) {
				string allHtml = reader.ReadToEnd();

				string genreId;
				string packageName;
				string packageCatchCopy;
				string packageText1;

				Match matchPackage = GPackage.regexPackagePackage.Match(allHtml);
				if (matchPackage.Success) {
					genreId = HtmlUtility.HtmlToText(matchPackage.Groups["GenreId"].Value);
					packageName = HtmlUtility.HtmlToText(matchPackage.Groups["PackageName"].Value);
					packageCatchCopy = HtmlUtility.HtmlToText(matchPackage.Groups["CatchCopy"].Value);
					packageText1 = HtmlUtility.HtmlToText(matchPackage.Groups["PackageText1"].Value);
				} else {
					throw new Exception("シリーズ一覧ページからパッケージの情報が取れなかった． <" + uri.AbsoluteUri + ">");
				}
				List<int> contentKeys = new List<int>();
				for (Match matchContent = GPackage.regexPackageContent.Match(allHtml); matchContent.Success; matchContent = matchContent.NextMatch()) {
					string id = matchContent.Groups["ContentId"].Value;
					string summary = HtmlUtility.HtmlToText(matchContent.Groups["Summary"].Value);
					string deadline = HtmlUtility.HtmlToText(matchContent.Groups["Deadline"].Value);
					
					ContentPropertiesOnPackagePage cpPac = new ContentPropertiesOnPackagePage(deadline, summary);
					int key = GContent.ConvertToKeyFromId(id);
					cpPacs.Add(key, cpPac);
					contentKeys.Add(key);
				}
				//コンテンツをひとつも取れなかったらエラーだろう
				if (0 == contentKeys.Count) {
					throw new Exception("シリーズ一覧からコンテンツIDをひとつも抽出できなかった． <" + uri.AbsoluteUri + ">");
				}
				
				childContKeys = contentKeys;
				return new GPackage(packId, packageName, packageCatchCopy, packageText1);
			}
		}
		internal static GPackage CreateDummyPackage() {
			return new GPackage("pac0000000", "(不明なパッケージ)", string.Empty, string.Empty);
		}

		private readonly string packageId;
		private readonly string packageName;
		[OptionalField]//2.0.5.0
		private /*readonly*/ string catchCopy;
		[OptionalField]//2.0.5.0
		private /*readonly*/ string packageText1;
		private ReadOnlyCollection<GContent> contents;

		private GPackage(string packageId, string packageName, string catchCopy, string packageText1) {
			this.packageId = packageId;
			this.packageName = packageName;
			this.catchCopy = catchCopy;
			this.packageText1 = packageText1;
		}

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context) {
			if (null == this.catchCopy) this.catchCopy = string.Empty;
			if (null == this.packageText1) this.packageText1 = string.Empty;
		}
		
		public string PackageId {
			get { return this.packageId; }
		}
		public string PackageName {
			get { return this.packageName; }
		}
		public string CatchCopy {
			get { return this.catchCopy; }
		}
		public string PackageText1 {
			get { return this.packageText1; }
		}

		public Uri PackagePageUri {
			get { return GPackage.CreatePackagePageUri(this.PackageId); }
		}
		public ReadOnlyCollection<GContent> Contents {
			get { return this.contents; }
			internal set { this.contents = value; }
		}
		public override string ToString() {
			return "<" + this.PackageId + "> " + this.PackageName;
		}
	}
	
	internal sealed class ContentPropertiesOnPackagePage {
		internal static readonly ContentPropertiesOnPackagePage Empty;
		static ContentPropertiesOnPackagePage() {
			ContentPropertiesOnPackagePage.Empty = new ContentPropertiesOnPackagePage(string.Empty, string.Empty);
		}
		
		private string deadline;
		private string summary;

		internal ContentPropertiesOnPackagePage(string deadline, string summary) {
			this.deadline = deadline;
			this.summary = summary;
		}

		public string Deadline {
			get { return this.deadline; }
		}
		public string Summary {
			get { return this.summary; }
		}
	}
}
