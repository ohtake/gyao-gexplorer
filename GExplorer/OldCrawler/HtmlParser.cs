using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Yusen.GExplorer.OldCrawler {
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
}
