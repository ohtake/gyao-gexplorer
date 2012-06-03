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
		private readonly static Regex regexId = new Regex("pac[0-9]{7}");

		private readonly static Regex regexPackageName = new Regex(@"<td class=""title12b"">(.+)</td>");
		private const string strTitleDate = "<td class=\"titledate10\">";
		private readonly static Regex regexAnchorHref = new Regex(@"<a href=""(.+?)""");
		
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
				throw new ArgumentException("������URI����p�b�P�[�W��ID���擾�ł��Ȃ������D������URI: " + uri.AbsoluteUri);
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
				//�p�b�P�[�W���̎擾
				while (null != (line = reader.ReadLine())) {
					Match match = GPackage.regexPackageName.Match(line);
					if (match.Success) {
						packageName = HtmlUtility.HtmlToText(match.Groups[1].Value);
						break;
					}
				}
				if (null == line) {//�p�b�P�[�W���̎擾�~�X
					throw new Exception("�p�b�P�[�W���̎擾�~�X�D <" + uri.AbsoluteUri + ">");
				}
				//�R���e���cID�Ɗ����̒��o
				List<string> contentIds = new List<string>();
				string deadLine = null;
				while (null != (line = reader.ReadLine())) {
					if (null == deadLine) {
						if (line.EndsWith(GPackage.strTitleDate)) {
							deadLine = reader.ReadLine().Trim();
						}
						continue;
					}
					Match match = GPackage.regexAnchorHref.Match(line);
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
			return new GPackage("pac???????", "(�s���ȃp�b�P�[�W)");
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
