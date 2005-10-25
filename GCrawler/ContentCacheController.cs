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
		void AddCache(GContent cont);
		bool TryGetCache(string contentId, out ContentCache cache);
		bool RemoveCache(string contentId);
		ReadOnlyCollection<string> ListAllCacheKeys();
	}

	public class ContentCacheControllerXml : IContentCacheController {
		private string cacheDir;
		private object objLock = new object();
		
		public ContentCacheControllerXml(string cacheDir) {
			this.cacheDir = cacheDir;
			DirectoryInfo di = new DirectoryInfo(this.cacheDir);
			if (! di.Exists) {
				di.Create();
			}
		}
		
		public bool TryGetCache(string contentId, out ContentCache cache) {
			lock (this.objLock) {
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
			lock (this.objLock) {
				try {
					FileInfo fi = new FileInfo(this.FileNameOf(contentId));
					if (fi.Exists) {
						fi.Delete();
						return true;
					}
					return false;
				} catch {
					return false;
				}
			}
		}
		public void AddCache(GContent cont) {
			lock (this.objLock) {
				GContent.Serialize(this.FileNameOf(cont.ContentId), cont);
			}
		}
		public ReadOnlyCollection<string> ListAllCacheKeys() {
			lock (this.objLock) {
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
