using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Yusen.GCrawler {
	static class HtmlUtility {
		private static readonly Regex regexWhiteSpaces = new Regex(@"\s{2,}");
		private static readonly Regex regexTabBreak = new Regex(@"<[Bb][Rr] */? *>");
		private static readonly Regex regexTab = new Regex(@"<[^>]*>");
		
		public static string HtmlToText(string input) {
			input = HtmlUtility.regexWhiteSpaces.Replace(input, " ");
			input = HtmlUtility.regexTabBreak.Replace(input, "\n");
			input = HtmlUtility.regexTab.Replace(input, "");
			if (input.Contains("&")) {
				input = input.Replace("&lt;", "<");
				input = input.Replace("&gt;", ">");
				input = input.Replace("&quot;", @"""");
				input = input.Replace("&apos;", "'");
				input = input.Replace("&amp;", "&");
			}
			return input;
		}
	}
}
