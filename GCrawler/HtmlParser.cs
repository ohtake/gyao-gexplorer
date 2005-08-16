using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using mshtml;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Xml;

namespace Yusen.GCrawler {
	public interface IHtmlParser {
		bool TryExtractLinks(Uri uri, out List<Uri> links, out List<Uri> images);
	}

	[Obsolete("遅すぎて使いもんにならん")]
	public class HtmlParserMshtml : IHtmlParser {
		private HTMLDocumentClass parentDocument;
		private IHTMLDocument2 doc2;
		private IHTMLDocument4 doc4;
		
		private int poling = 1000;
		private int interval = 10;
		
		private Mutex downloading = new Mutex();
		
		public HtmlParserMshtml(){
			this.parentDocument = new HTMLDocumentClass();
			this.doc2 = this.parentDocument;
			this.doc4 = this.parentDocument;
			doc2.write("<html></html>");
			doc2.close();
		}
		public HtmlParserMshtml(int poling, int interval)
			: this() {
			this.poling = poling;
			this.interval = interval;
		}
		
		public bool TryExtractLinks(Uri uri, out List<Uri> links, out List<Uri> images) {
			try {
				IHTMLDocument2 doc = this.doc4.createDocumentFromUrl(uri.AbsoluteUri, "null");
				for (int i = 0; "complete" != doc.readyState; i++) {
					if (i >= this.poling) {
						goto error;
					}
					Thread.Sleep(this.interval);
				}
				
				//リンク抽出
				links = new List<Uri>();
				foreach (IHTMLElement elem in doc.links) {
					try {
						links.Add(new Uri(elem.getAttribute("href", 0).ToString()));
					} catch {
					}
				}
				//画像URI抽出
				images = new List<Uri>();
				foreach (IHTMLElement elem in doc.images) {
					try {
						images.Add(new Uri(elem.getAttribute("src", 0).ToString()));
					} catch {
					}
				}
				return true;
			} catch {
				goto error;
			}
		error:
			links = null;
			images = null;
			return false;
		}
	}
	
	public class HtmlParserRegex : IHtmlParser {
		private static readonly Regex regexAnchorHref = new Regex(@"<(?:a|area) [^>]*href=""(.+?)""");
		private static readonly Regex regexImgSrc = new Regex(@"<img src=""(.+?)""");
		private const string beginComment = "<!--";
		private const string endComment = "-->";

		private readonly WebClient wc;
		
		public HtmlParserRegex() {
			this.wc = new WebClient();
		}
		
		public bool TryExtractLinks(Uri uri, out List<Uri> links, out List<Uri> images) {
			links = new List<Uri>();
			images = new List<Uri>();
			TextReader reader = null;
			try {
				reader = new StreamReader(this.wc.OpenRead(uri.AbsoluteUri), Encoding.GetEncoding("Shift_JIS"));
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
						} catch {
						}
					}
					match = HtmlParserRegex.regexImgSrc.Match(line);
					if (match.Success) {
						try {
							images.Add(new Uri(uri, match.Groups[1].Value));
						} catch {
						}
					}
				}
				return true;
			} catch {
				return false;
			} finally {
				if (null != reader) reader.Close();
			}
		}
	}
	
}
