using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Yusen.GExplorer.OldCrawler {
	public struct ContentCache{
		private GContent content;
		private DateTime lastWrittenTime;

		public ContentCache(GContent content, DateTime lastWrittenTime) {
			this.content = content;
			this.lastWrittenTime = lastWrittenTime;
		}

		public GContent Content {
			get { return this.content; }
			set { this.content = value; }
		}
		[XmlAttribute]
		public DateTime LastWrittenTime {
			get { return this.lastWrittenTime; }
			set { this.lastWrittenTime = value; }
		}
	}

	public interface IContentCacheController {
		void AddCache(GContent cont);
		bool TryGetCache(int contentKey, out ContentCache cache);
		bool RemoveCache(int contentId);
		ReadOnlyCollection<int> ListAllCacheKeys();
	}
}
