using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.ObjectModel;

namespace Yusen.GCrawler {
	public class GPackage {
		private readonly static Regex regexAnchorHref = new Regex(@"http://www.gyao.jp/sityou/catelist/pac_id/(pac[0-9]+)/");
		private readonly static Regex regexAnchorJscript = new Regex(@"javascript:gotoList\((?:%20| )?'(pac[0-9]+)(?:%20| )?'\);");
		private readonly static Regex regexImageSrc = new Regex(@"http://www.gyao.jp/img/info/[0-9a-z]+/(pac[0-9]+)_[0-9a-z]*\.(?:jpg|gif)");
		private readonly static IEnumerable<Regex> regexesExtractor;
		
		private readonly static Regex regexPackageName = new Regex(@"<td class=""title12b"">(.+)</td>");
		private readonly static Regex regexAnchorHref2 = new Regex(@"<a href=""(.+?)""");
		
		static GPackage() {
			GPackage.regexesExtractor = new Regex[]{
				GPackage.regexAnchorHref, GPackage.regexAnchorJscript, GPackage.regexImageSrc,};
		}
		
		internal static bool TryExtractPackageId(Uri uri, out string id) {
			foreach (Regex regex in GPackage.regexesExtractor) {
				Match match = regex.Match(uri.AbsoluteUri);
				if (match.Success) {
					id = match.Groups[1].Value;
					return true;
				}
			}
			id = null;
			return false;
		}
		internal static string ExtractPackageId(Uri uri) {
			string id;
			if (GPackage.TryExtractPackageId(uri, out id)) {
				return id;
			} else {
				throw new ArgumentException("引数のURIからパッケージのIDを取得できなかった．\n引数のURI: " + uri.AbsoluteUri);
			}
		}
		internal static bool CanExtractPackageId(Uri uri) {
			string dummy;
			return GPackage.TryExtractPackageId(uri, out dummy);
		}

		public static Uri CreatePackagePageUri(string packageId) {
			return new Uri("http://www.gyao.jp/sityou/catelist/pac_id/" + packageId + "/");
		}

		internal static bool TryDownload(string packId, out GPackage package, out List<string> childContIds) {
			Uri uri = GPackage.CreatePackagePageUri(packId);
			TextReader reader = new StreamReader(new WebClient().OpenRead(uri), Encoding.GetEncoding("Shift_JIS"));
			
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
				package = null;
				childContIds = null;
				return false;
			}
			//コンテンツIDの抽出
			List<string> contentIds = new List<string>();
			while (null != (line = reader.ReadLine())) {
				Match match = GPackage.regexAnchorHref2.Match(line);
				if (match.Success) {
					string contId;
					if (GContent.TryExtractContentId(new Uri(uri, match.Groups[1].Value), out contId)) {
						contentIds.Add(contId);
					}
				}
			}

			package = new GPackage(packId, packageName);
			childContIds = contentIds;
			return true;
		}
		internal static GPackage CreateDummyPackage() {
			return new GPackage("pacXXXXXXX", "不明なパッケージ");
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
