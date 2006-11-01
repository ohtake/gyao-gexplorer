using System;
using System.Collections.Generic;
using Yusen.GExplorer.OldCrawler;

namespace Yusen.GExplorer.OldApp {
	sealed class Cache {
		private static Cache instance = new Cache();
		public static Cache Instance {
			get { return Cache.instance; }
		}
		private IContentCacheController cacheCtl = null;
		
		private Cache() {
		}
		
		public ContentAdapter GetCacheOrDownloadContent(int contentKey) {
			ContentCache cache;
			if (this.cacheCtl.TryGetCache(contentKey, out cache)) {
				return new ContentAdapter(cache.Content);
			} else {
				return new ContentAdapter(GContent.DoDownload(contentKey));
			}
		}

		public IList<GContent> FindCaches(string query) {
			List<GContent> conts = new List<GContent>();
			foreach (int key in this.cacheCtl.ListAllCacheKeys()) {
				ContentCache cache;
				if (!this.cacheCtl.TryGetCache(key, out cache)) continue;
				GContent cont = cache.Content;
				
				if (cont.Title.Contains(query)) goto match;
				if (cont.SeriesNumber.Contains(query)) goto match;
				if (cont.Subtitle.Contains(query)) goto match;
				if (cont.Summary.Contains(query)) goto match;
				if (cont.Description1.Contains(query)) goto match;
				if (cont.Description2.Contains(query)) goto match;
				if (cont.Description3.Contains(query)) goto match;
				if (cont.Description4.Contains(query)) goto match;
				continue;
			match:
				conts.Add(cont);
				continue;
			}
			return conts;
		}
	}
}
