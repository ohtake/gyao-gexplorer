using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer.GyaoModel {
	public static class GIdExtractor {
		private readonly static Regex regexGenreId = new Regex(@"gen\d{7}", RegexOptions.Compiled);
		private readonly static Regex regexPackageId = new Regex(@"pac\d{7}", RegexOptions.Compiled);
		private readonly static Regex regexContentId = new Regex(@"cnt\d{7}", RegexOptions.Compiled);

		public static bool TryExtractGenreId(string str, out string id) {
			Match match = GIdExtractor.regexGenreId.Match(str);
			if (match.Success) {
				id = match.Value;
				return true;
			} else {
				id = null;
				return false;
			}
		}
		public static bool TryExtractGenreId(Uri uri, out string id) {
			return GIdExtractor.TryExtractGenreId(uri.AbsoluteUri, out id);
		}

		public static bool TryExtractPackageId(string str, out string id) {
			Match match = GIdExtractor.regexPackageId.Match(str);
			if (match.Success) {
				id = match.Value;
				return true;
			} else {
				id = null;
				return false;
			}
		}
		public static bool TryExtractPackageId(Uri uri, out string id) {
			return GIdExtractor.TryExtractPackageId(uri.AbsoluteUri, out id);
		}

		public static bool TryExtractContentId(string str, out string id) {
			Match match = GIdExtractor.regexContentId.Match(str);
			if (match.Success) {
				id = match.Value;
				return true;
			} else {
				id = null;
				return false;
			}
		}
		public static bool TryExtractContentId(Uri uri, out string id) {
			return GIdExtractor.TryExtractContentId(uri.AbsoluteUri, out id);
		}
		public static string[] ExtractContentIds(string text) {
			List<string> ids = new List<string>();
			for (Match match = GIdExtractor.regexContentId.Match(text); match.Success; match = match.NextMatch()) {
				ids.Add(match.Value);
			}
			return ids.ToArray();
		}
	}
}
