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
		bool TryGetCache(int contentKey, out ContentCache cache);
		bool RemoveCache(int contentId);
		ReadOnlyCollection<int> ListAllCacheKeys();
	}
	
	public sealed class ContentCacheControllerSortedDic : IContentCacheController {
		private SortedDictionary<int, ContentCache> dic;
		private object lockDic = new object();

		public ContentCacheControllerSortedDic() {
			this.dic = new SortedDictionary<int, ContentCache>();
		}

		public void DeserializeContents(string filename) {
			using (TextReader tr = new StreamReader(filename)) {
				XmlSerializer serializer = new XmlSerializer(typeof(ContentCache[]));
				ContentCache[] deserializedCaches = serializer.Deserialize(tr) as ContentCache[];
				lock (this.lockDic) {
					this.dic.Clear();
					foreach (ContentCache cache in deserializedCaches) {
						this.dic.Add(cache.Content.ContentKey, cache);
					}
				}
			}
		}
		public void SerializeContentes(string filename) {
			using (TextWriter tw = new StreamWriter(filename)) {
				XmlSerializer serializer = new XmlSerializer(typeof(ContentCache[]));
				ContentCache[] caches;
				lock (this.lockDic) {
					caches = new List<ContentCache>(this.dic.Values).ToArray();
				}
				serializer.Serialize(tw, caches);
			}
		}
		
		private void AddCacheSync(ContentCache cache) {
			lock (this.lockDic) {
				this.dic.Add(cache.Content.ContentKey, cache);
			}
		}
		private bool TryGetCacheSync(int contentKey, out ContentCache cache) {
			lock (this.lockDic) {
				return this.dic.TryGetValue(contentKey, out cache);
			}
		}
		private bool RemoveCacheSync(int contentKey) {
			lock (this.lockDic) {
				return this.dic.Remove(contentKey);
			}
		}


		#region IContentCacheController Members
		public void AddCache(GContent cont) {
			this.AddCacheSync(new ContentCache(cont, DateTime.Now));
		}
		public bool TryGetCache(int contentKey, out ContentCache cache) {
			return this.TryGetCacheSync(contentKey, out cache);
		}
		public bool RemoveCache(int contentKey) {
			return this.RemoveCacheSync(contentKey);
		}
		public ReadOnlyCollection<int> ListAllCacheKeys() {
			List<int> keys;
			lock (this.lockDic) {
				keys = new List<int>(new List<int>(this.dic.Keys));
			}
			return new ReadOnlyCollection<int>(keys);
		}
		#endregion
	}
}
