using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Yusen.GExplorer.GyaoModel;
using Clipboard = System.Windows.Forms.Clipboard;

namespace Yusen.GExplorer.UserInterfaces {
	static class ContentClipboardUtility {
		public static string ConvertToName(GContentClass cont) {
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
		/*private static string ConvertToName(GPackageClass pac) {
			string genName = (pac.ParentGenre != null) ? pac.ParentGenre.GenreName : "???";
			return string.Format("[{0}] {1}", genName, pac.PackageTitle);
		}*/
		
		public static void CopyNames(IEnumerable<GContentClass> conts) {
			StringBuilder sb = new StringBuilder();
			foreach (GContentClass cont in conts) {
				string name = ContentClipboardUtility.ConvertToName(cont);
				if (sb.Length > 0) sb.AppendLine();
				sb.Append(name);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		public static void CopyUris(IEnumerable<GContentClass> conts) {
			StringBuilder sb = new StringBuilder();
			foreach (GContentClass cont in conts) {
				Uri uri = cont.ContentDetailUri;
				if (sb.Length > 0) sb.AppendLine();
				sb.Append(uri.AbsoluteUri);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		public static void CopyNamesAndUris(IEnumerable<GContentClass> conts) {
			StringBuilder sb = new StringBuilder();
			foreach (GContentClass cont in conts) {
				string name = ContentClipboardUtility.ConvertToName(cont);
				Uri uri = cont.ContentDetailUri;
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
