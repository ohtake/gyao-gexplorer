using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Yusen.GExplorer.Utilities {
	static class HtmlUtility {
		private static readonly Regex regexWhiteSpaces = new Regex(@"\s+", RegexOptions.Compiled);
		private static readonly Regex regexTabBreak = new Regex(@"<br\s*/?>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		private static readonly Regex regexTab = new Regex(@"<.*?>", RegexOptions.Compiled);
		
		public static string HtmlToText(string input) {
			if (string.IsNullOrEmpty(input)) return string.Empty;
			
			input = HtmlUtility.regexWhiteSpaces.Replace(input, " ");
			input = HtmlUtility.regexTabBreak.Replace(input, "\n");
			input = HtmlUtility.regexTab.Replace(input, string.Empty);//System.Web.RegularExpression はうまくいかない？
			input = HttpUtility.HtmlDecode(input);//実体参照などの解決
			return input;
		}
	}
}
