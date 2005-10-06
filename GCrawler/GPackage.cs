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
		private readonly static Regex regexAnchorHref = new Regex(@"http://www.gyao.jp/sityou/catelist/pac_id/(pac[0-9]+)/");
		private readonly static Regex regexAnchorJscript = new Regex(@"javascript:gotoList\((?:%20| )?'(pac[0-9]+)(?:%20| )?'\);");
		private readonly static Regex regexImageSrc = new Regex(@"http://www.gyao.jp/img/info/[0-9a-z]+/(pac[0-9]+)_[0-9a-z]*\.(?:jpg|gif)");
		private readonly static IEnumerable<Regex> regexesExtractor;
		
		private readonly static Regex regexPackageName = new Regex(@"<td class=""title12b"">(.+)</td>");
		private const string strTitleDate = "<td class=\"titledate10\">";
		private readonly static Regex regexAnchorHref2 = new Regex(@"<a href=""(.+?)""");
		
		static GPackage() {
			GPackage.regexesExtractor = new Regex[]{
				GPackage.regexAnchorHref, GPackage.regexAnchorJscript, GPackage.regexImageSrc,};
		}
		
		public static bool TryExtractPackageId(Uri uri, out string id) {
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
		public static string ExtractPackageId(Uri uri) {
			string id;
			if (GPackage.TryExtractPackageId(uri, out id)) {
				return id;
			} else {
				throw new ArgumentException("引数のURIからパッケージのIDを取得できなかった．\n引数のURI: " + uri.AbsoluteUri);
			}
		}
		public static bool CanExtractPackageId(Uri uri) {
			string dummy;
			return GPackage.TryExtractPackageId(uri, out dummy);
		}

		public static Uri CreatePackagePageUri(string packageId) {
			return new Uri("http://www.gyao.jp/sityou/catelist/pac_id/" + packageId + "/");
		}

		internal static GPackage DoDownload(string packId, IDeadlineTable deadlineDic, out List<string> childContIds) {
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
				string deadLine = null;
				while (null != (line = reader.ReadLine())) {
					if (null == deadLine) {
						if (line.EndsWith(GPackage.strTitleDate)) {
							deadLine = reader.ReadLine().Trim();
						}
						continue;
					}
					Match match = GPackage.regexAnchorHref2.Match(line);
					if (match.Success) {
						string contId;
						if (GContent.TryExtractContentId(new Uri(uri, match.Groups[1].Value), out contId)) {
							contentIds.Add(contId);
							deadlineDic.SetDeadline(contId, deadLine);
							deadLine = null;
						}
					}
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
