using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.ObjectModel;

namespace Yusen.GCrawler {
	[Serializable]
	public class GPackage {
		private readonly static Regex regexId = new Regex("pac[0-9]{7}", RegexOptions.Compiled);

		private readonly static Regex regexPackageName = new Regex(@"<td class=""title12b"">(.+)</td>", RegexOptions.Compiled);
		private static readonly Regex regexAddMyList = new Regex(@"<a href=""javascript:addMylist\('(cnt[0-9]{7})'\);"">", RegexOptions.Compiled);
		private static readonly Regex regexDeadline = new Regex(@"^<td height=""14"" colspan=""2"" class=""bk10"">(.+)</td>$", RegexOptions.Compiled);
		
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

		public static Uri CreatePackagePageUri(string packageId) {
			return new Uri("http://www.gyao.jp/sityou/catelist/pac_id/" + packageId + "/");
		}

		internal static GPackage DoDownload(string packId, IDeadlineTable deadlineTable, out List<string> childContIds) {
			Uri uri = GPackage.CreatePackagePageUri(packId);
			using (TextReader reader = new StreamReader(new WebClient().OpenRead(uri), Encoding.GetEncoding("Shift_JIS"))) {
				string line;
				string packageName = "";
				//パッケージ名の取得
				while (null != (line = reader.ReadLine())) {
					Match match = GPackage.regexPackageName.Match(line);
					if (match.Success) {
						packageName = HtmlUtility.HtmlToText(match.Groups[1].Value);
						break;
					}
				}
				if (null == line) {//パッケージ名の取得ミス
					throw new Exception("パッケージ名の取得ミス． <" + uri.AbsoluteUri + ">");
				}
				//コンテンツIDと期限の抽出
				List<string> contentIds = new List<string>();
				while (true) {
				beginExtractId:
					string contId;
					while (null != (line = reader.ReadLine())) {
						Match match = GPackage.regexAddMyList.Match(line);
						if (match.Success) {
							contId = match.Groups[1].Value;
							goto beginExtractDeadline;
						}
					}
					goto endExtract;
				beginExtractDeadline:
					while (null != (line = reader.ReadLine())) {
						Match match = GPackage.regexDeadline.Match(line);
						if (match.Success) {
							string deadline = match.Groups[1].Value;
							contentIds.Add(contId);
							deadlineTable.SetDeadline(contId, deadline);
							goto beginExtractId;
						}
					}
					throw new Exception("配信期限を取得できなかった． <" + uri.AbsoluteUri + ">");
				}
			endExtract:
				
				//コンテンツをひとつも取れなかったらエラーだろう
				if (0 == contentIds.Count) {
					throw new Exception("シリーズ一覧からコンテンツIDをひとつも抽出できなかった． <" + uri.AbsoluteUri + ">");
				}
				
				childContIds = contentIds;
				return new GPackage(packId, packageName);
			}
			
		}
		internal static GPackage CreateDummyPackage() {
			return new GPackage("pac???????", "(不明なパッケージ)");
		}

		private readonly string packageId;
		private readonly string packageName;
		private ReadOnlyCollection<GContent> contents;

		private GPackage(string packageId, string packageName) {
			this.packageId = packageId;
			this.packageName = packageName;
		}

		public string PackageId {
			get { return this.packageId; }
		}
		public string PackageName {
			get { return this.packageName; }
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
}
