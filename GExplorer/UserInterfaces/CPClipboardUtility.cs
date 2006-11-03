using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using Yusen.GExplorer.GyaoModel;
using Clipboard = System.Windows.Forms.Clipboard;

namespace Yusen.GExplorer.UserInterfaces {
	static class CPClipboardUtility {
		private static string ConvertToName(GContentClass cont) {
			string genName = (cont.GrandparentGenre != null) ? cont.GrandparentGenre.GenreName : "???";
			StringBuilder sb = new StringBuilder();
			sb.Append("[" + genName + "]");
			sb.Append(" " + cont.Title);
			if (!string.IsNullOrEmpty(cont.SeriesNumber) && !cont.SeriesNumber.Equals(cont.Title)) {
				sb.Append(" / " + cont.SeriesNumber);
			}
			if (!string.IsNullOrEmpty(cont.Subtitle) && !cont.Subtitle.Equals(cont.Title) && !cont.Subtitle.Equals(cont.SeriesNumber)) {
				sb.Append(" / " + cont.Subtitle);
			}
			return sb.ToString();
		}
		private static string ConvertToName(GPackageClass pac) {
			string genName = (pac.ParentGenre != null) ? pac.ParentGenre.GenreName : "???";
			return string.Format("[{0}] {1}", genName, pac.PackageTitle);
		}
		private static Uri ConvertToUri(GContentClass cont) {
			return cont.ContentDetailUri;
		}
		private static Uri ConvertToUri(GPackageClass pac) {
			return pac.PackagePageUri;
		}
		
		public static void CopyNames(IEnumerable cps) {
			StringBuilder sb = new StringBuilder();
			foreach (object o in cps) {
				string name;
				GContentClass cont = o as GContentClass;
				GPackageClass pac = o as GPackageClass;
				if (null != cont) {
					name = CPClipboardUtility.ConvertToName(cont);
				} else if (null != pac) {
					name = CPClipboardUtility.ConvertToName(pac);
				} else {
					throw new ArgumentException();
				}
				if (sb.Length > 0) sb.AppendLine();
				sb.Append(name);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		public static void CopyUris(IEnumerable cps) {
			StringBuilder sb = new StringBuilder();
			foreach (object o in cps) {
				Uri uri;
				GContentClass cont = o as GContentClass;
				GPackageClass pac = o as GPackageClass;
				if (null != cont) {
					uri = CPClipboardUtility.ConvertToUri(cont);
				} else if (null != pac) {
					uri = CPClipboardUtility.ConvertToUri(pac);
				} else {
					throw new ArgumentException();
				}
				if (sb.Length > 0) sb.AppendLine();
				sb.Append(uri.AbsoluteUri);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		public static void CopyNamesAndUris(IEnumerable cps) {
			StringBuilder sb = new StringBuilder();
			foreach (object o in cps) {
				string name;
				Uri uri;
				GContentClass cont = o as GContentClass;
				GPackageClass pac = o as GPackageClass;
				if (null != cont) {
					name = CPClipboardUtility.ConvertToName(cont);
					uri = CPClipboardUtility.ConvertToUri(cont);
				} else if (null != pac) {
					name = CPClipboardUtility.ConvertToName(pac);
					uri = CPClipboardUtility.ConvertToUri(pac);
				} else {
					throw new ArgumentException();
				}
				if (sb.Length > 0) sb.AppendLine();
				sb.AppendLine(name);
				sb.Append(uri.AbsoluteUri);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		public static void CopyContentProperties(IEnumerable<GContentClass> conts, PropertyInfo pi) {
			StringBuilder sb = new StringBuilder();
			foreach (GContentClass cont in conts) {
				string val = pi.GetValue(cont, null).ToString();
				if (sb.Length > 0) sb.AppendLine();
				sb.Append(val);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
	}
}
