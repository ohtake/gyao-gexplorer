using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Yusen.GCrawler {
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
		bool TryGetCache(string contentId, out ContentCache cache);
		bool RemoveCache(string contentId);
		ReadOnlyCollection<string> ListAllCacheKeys();
	}
	
	[Obsolete("パフォーマンスが悪いから移行すべし．")]//2.0.3.0からObsolete
	public sealed class ContentCacheControllerXml : IContentCacheController {
		private string cacheDir;
		private object objLock = new object();

		public ContentCacheControllerXml(string cacheDir) {
			this.cacheDir = cacheDir;
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
	
	public sealed class ContentCacheControllerSortedDic : IContentCacheController {
		private ContentCacheControllerXml oldController;
		private SortedDictionary<string, ContentCache> dic;
		private object lockDic = new object();

		public ContentCacheControllerSortedDic(string tempDir) {
			this.oldController = new ContentCacheControllerXml(tempDir);
			this.dic = new SortedDictionary<string, ContentCache>();
		}

		public void DeserializeContents(string filename) {
			try {
				using (TextReader tr = new StreamReader(filename)) {
					XmlSerializer serializer = new XmlSerializer(typeof(ContentCache[]));
					ContentCache[] deserializedCaches = serializer.Deserialize(tr) as ContentCache[];
					lock (this.lockDic) {
						this.dic.Clear();
						foreach (ContentCache cache in deserializedCaches) {
							this.dic.Add(cache.Content.ContentId, cache);
						}
					}
				}
			} catch {
			}
		}
		public void SerializeContentes(string filename) {
			try {
				using (TextWriter tw = new StreamWriter(filename)) {
					XmlSerializer serializer = new XmlSerializer(typeof(ContentCache[]));
					ContentCache[] caches;
					lock (this.lockDic) {
						caches = new List<ContentCache>(this.dic.Values).ToArray();
					}
					serializer.Serialize(tw, caches);
				}
			} catch {
			}
		}
		public void DeleteTempCachesRead() {
			lock (this.lockDic) {
				List<string> myIds = new List<string>(this.dic.Keys);
				List<string> oldIds = new List<string>(this.oldController.ListAllCacheKeys());
				oldIds.RemoveAll(myIds.Contains);
				foreach (string oldId in oldIds) {
					this.oldController.RemoveCache(oldId);
				}
			}
		}

		private void AddCacheSync(ContentCache cache) {
			lock (this.lockDic) {
				this.dic.Add(cache.Content.ContentId, cache);
			}
		}
		private bool TryGetCacheSync(string contentId, out ContentCache cache) {
			lock (this.lockDic) {
				return this.dic.TryGetValue(contentId, out cache);
			}
		}
		private bool RemoveCacheSync(string contentId) {
			lock (this.lockDic) {
				return this.dic.Remove(contentId);
			}
		}


		#region IContentCacheController Members
		public void AddCache(GContent cont) {
			this.AddCacheSync(new ContentCache(cont, DateTime.Now));
			//this.oldController.AddCache(cont);
		}
		public bool TryGetCache(string contentId, out ContentCache cache) {
			if (this.TryGetCacheSync(contentId, out cache)) {
				return true;
			} else if (this.oldController.TryGetCache(contentId, out cache)) {
				this.AddCacheSync(cache);
				return true;
			} else {
				return false;
			}
		}
		public bool RemoveCache(string contentId) {
			bool result = false;
			result |= this.oldController.RemoveCache(contentId);
			result |= this.RemoveCacheSync(contentId);
			return result;
		}
		public ReadOnlyCollection<string> ListAllCacheKeys() {
			List<string> ids;
			lock (this.lockDic) {
				ids = new List<string>(new List<string>(this.dic.Keys));
			}
			foreach (string solidId in this.oldController.ListAllCacheKeys()) {
				if (!ids.Contains(solidId)) ids.Add(solidId);
			}
			return new ReadOnlyCollection<string>(ids);
		}
		#endregion
	}
}
