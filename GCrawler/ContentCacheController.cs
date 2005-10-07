using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace Yusen.GCrawler {
	[Serializable]
	public struct ContentCache{
		private GContent content;
		private DateTime lastWriteTime;

		public ContentCache(GContent content, DateTime lastWriteTime) {
			this.content = content;
			this.lastWriteTime = lastWriteTime;
		}

		public GContent Content {
			get { return this.content; }
		}
		public DateTime LastWriteTime {
			get { return this.lastWriteTime; }
		}
	}

	public interface IContentCacheController {
		bool TryGetCache(string contentId, out ContentCache cache);
		bool RemoveCache(string contentId);
		void AddToCache(GContent cont);
		ReadOnlyCollection<string> ListAllCacheKeys();
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
		
		public bool TryGetCache(string contentId, out ContentCache cache) {
			lock (this) {
				FileInfo fi = new FileInfo(this.FileNameOf(contentId));
				if (fi.Exists) {
					GContent cont;
					if (GContent.TryDeserialize(fi.FullName, out cont)) {
						if (contentId.Equals(cont.ContentId)) {
							cache = new ContentCache(cont, fi.LastWriteTime);
							return true;
						}
					}
				}
				cache = default(ContentCache);
				return false;
			}
		}
		public bool RemoveCache(string contentId) {
			lock (this) {
				FileInfo fi = new FileInfo(this.FileNameOf(contentId));
				if (fi.Exists) {
					fi.Delete();
					return true;
				}
				return false;
			}
		}
		public void AddToCache(GContent cont) {
			lock (this) {
				GContent.Serialize(this.FileNameOf(cont.ContentId), cont);
			}
		}
		public ReadOnlyCollection<string> ListAllCacheKeys() {
			lock (this) {
				DirectoryInfo di = new DirectoryInfo(this.cacheDir);
				FileInfo[] fis = di.GetFiles("cnt*.xml", SearchOption.TopDirectoryOnly);
				List<string> ids = new List<string>();
				foreach (FileInfo fi in fis) {
					ids.Add(Path.GetFileNameWithoutExtension(fi.Name));
				}
				return ids.AsReadOnly();
			}
		}

		private string FileNameOf(string contId) {
			return Path.Combine(this.cacheDir, contId + ".xml");
		}
	}
}
