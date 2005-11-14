using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Yusen.GCrawler {
	public interface IHtmlParser {
		void ExtractLinks(Uri uri, out List<Uri> links, out List<Uri> images);
	}
	
	public class HtmlParserRegex : IHtmlParser , IDisposable{
		private static readonly Regex regexLinks = new Regex(@"<(?:(?:a(?:rea)?) [^>]*?href=""|i?frame [^>]*src="")(.+?)""", RegexOptions.Compiled);
		private static readonly Regex regexImgSrc = new Regex(@"<img src=""(.+?)""", RegexOptions.Compiled);
		private const string beginComment = "<!--";
		private const string endComment = "-->";
		
		private readonly WebClient wc;
		
		public HtmlParserRegex() {
			this.wc = new WebClient();
		}

		public void ExtractLinks(Uri uri, out List<Uri> links, out List<Uri> images) {
			links = new List<Uri>();
			images = new List<Uri>();
			using (TextReader reader = new StreamReader(this.wc.OpenRead(uri.AbsoluteUri), Encoding.GetEncoding("Shift_JIS"))) {
				string line;
				bool isInComment = false;
				while (null != (line = reader.ReadLine())) {
				processComment://コメントは読み飛ばす
					if (isInComment) {
						int end = line.IndexOf(HtmlParserRegex.endComment);
						if (end >= 0) {
							isInComment = false;
							line = line.Substring(end + HtmlParserRegex.endComment.Length);
							goto processComment;
						} else {
							continue;
						}
					} else {
						int begin = line.IndexOf(HtmlParserRegex.beginComment);
						if (begin >= 0) {
							isInComment = true;
							line = line.Substring(begin + HtmlParserRegex.beginComment.Length);
							goto processComment;
						}
					}
					//リンクと画像の抽出
					for(Match m = HtmlParserRegex.regexLinks.Match(line); m.Success; m = m.NextMatch()) {
						try {
							links.Add(new Uri(uri, m.Groups[1].Value));
						} catch(UriFormatException) {
						}
					}
					for(Match m = HtmlParserRegex.regexImgSrc.Match(line); m.Success; m=m.NextMatch()) {
						try {
							images.Add(new Uri(uri, m.Groups[1].Value));
						} catch(UriFormatException) {
						}
					}
				}
			}
		}

		public void Dispose() {
			this.wc.Dispose();
		}
	}
}
