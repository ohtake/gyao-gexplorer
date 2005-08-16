using System;
using System.Collections.Generic;
using System.IO;

namespace Yusen.GCrawler {
	public interface IContentCacheController {
		bool TryGetCache(string contentId, out GContent cont);
		void RemoveCache(string contentId);
		void AddToCache(GContent cont);
	}

	public class ContentCacheControllerXml : IContentCacheController {
		private string cacheDir;
		
		public ContentCacheControllerXml(string cacheDir) {
			this.cacheDir = cacheDir;
			DirectoryInfo di = new DirectoryInfo(this.cacheDir);
			if (! di.Exists) {
				di.Create();
			}
		}
		
		public bool TryGetCache(string contentId, out GContent cont) {
			lock (this) {
				FileInfo fi = new FileInfo(this.FileNameOf(contentId));
				if (fi.Exists) {
					return GContent.TryDeserialize(fi.FullName, out cont);
				}
				cont = null;
				return false;
			}
		}
		public void RemoveCache(string contentId) {
			lock (this) {
				FileInfo fi = new FileInfo(this.FileNameOf(contentId));
				if (fi.Exists) {
					fi.Delete();
				}
			}
		}
		public void AddToCache(GContent cont) {
			lock (this) {
				GContent.Serialize(this.FileNameOf(cont.ContentId), cont);
			}
		}

		private string FileNameOf(string contId) {
			return Path.Combine(this.cacheDir, contId + ".xml");
		}
		
	}
}
