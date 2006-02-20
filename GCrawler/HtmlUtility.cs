﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Yusen.GCrawler {
	static class HtmlUtility {
		private static readonly Regex regexWhiteSpaces = new Regex(@"\s+");
		private static readonly Regex regexTabParaBegin = new Regex(@"<p>", RegexOptions.IgnoreCase);
		private static readonly Regex regexTabParaEnd = new Regex(@"</p>", RegexOptions.IgnoreCase);
		private static readonly Regex regexTabBreak = new Regex(@"<br */?>", RegexOptions.IgnoreCase);
		private static readonly Regex regexTab = new Regex(@"<[^>]*>");
		
		public static string HtmlToText(string input) {
			input = HtmlUtility.regexWhiteSpaces.Replace(input, " ");
			input = HtmlUtility.regexTabParaBegin.Replace(input, "\n\n");
			input = HtmlUtility.regexTabParaEnd.Replace(input, "\n");
			input = HtmlUtility.regexTabBreak.Replace(input, "\n");
			input = HtmlUtility.regexTab.Replace(input, "");//System.Web.RegularExpression はうまくいかない？
			input = HttpUtility.HtmlDecode(input);
			return input;
		}
	}
}
