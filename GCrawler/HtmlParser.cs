using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Yusen.GCrawler {
	public enum LinkType {
		AnchorOrFrame,
		Image,
	}

	public struct UriLinkTypePair : IEquatable<UriLinkTypePair>{
		private Uri uri;
		private LinkType linkType;
		
		public UriLinkTypePair(Uri uri, LinkType linkType) {
			this.uri = uri;
			this.linkType = linkType;
		}
		public Uri Uri {
			get { return this.uri; }
		}
		public LinkType LinkType {
			get { return this.linkType; }
		}
		
		public bool Equals(UriLinkTypePair other) {
			return this.LinkType == other.LinkType && this.Uri.Equals(other.Uri);
		}
	}

	public interface IHtmlParser {
		List<UriLinkTypePair> ExtractLinks(Uri uri);
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

		public List<UriLinkTypePair> ExtractLinks(Uri uri) {
			List<UriLinkTypePair> links = new List<UriLinkTypePair>();

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
							links.Add(new UriLinkTypePair(new Uri(uri, m.Groups[1].Value), LinkType.AnchorOrFrame));
						} catch(UriFormatException) {
						}
					}
					for(Match m = HtmlParserRegex.regexImgSrc.Match(line); m.Success; m=m.NextMatch()) {
						try {
							links.Add(new UriLinkTypePair(new Uri(uri, m.Groups[1].Value), LinkType.Image));
						} catch(UriFormatException) {
						}
					}
				}
			}
			return links;
		}

		public void Dispose() {
			this.wc.Dispose();
		}
	}
}
