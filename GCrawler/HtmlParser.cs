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
		private static readonly Regex regexAnchorHref = new Regex(@"<(?:a|area) [^>]*href=""(.+?)""");
		private static readonly Regex regexImgSrc = new Regex(@"<img src=""(.+?)""");
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
				Match match;
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
					match = HtmlParserRegex.regexAnchorHref.Match(line);
					if (match.Success) {
						try {
							links.Add(new Uri(uri, match.Groups[1].Value));
						} catch (UriFormatException) {
						}
					}
					match = HtmlParserRegex.regexImgSrc.Match(line);
					if (match.Success) {
						try {
							images.Add(new Uri(uri, match.Groups[1].Value));
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
