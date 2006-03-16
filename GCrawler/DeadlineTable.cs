using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace Yusen.GCrawler {
	public interface IDeadlineTableReadOnly {
		bool ContainsDeadlineOf(string contId);
		bool TryGetDeadline(string contId, out string deadline);
		IEnumerable<string> ListContentIds();
		int Count { get;}
	}
	public interface IDeadlineTable : IDeadlineTableReadOnly{
		void SetDeadline(string contId, string deadline);
		bool RemoveDeadlineOf(string contId);
		void ClearDeadlines();
	}
	public struct ContentIdDeadlinePair {
		private string contentId;
		private string deadline;
		public ContentIdDeadlinePair(string contentId, string deadline) {
			this.contentId = contentId;
			this.deadline = deadline;
		}
		[XmlAttribute]
		public string ContentId {
			get { return this.contentId; }
			set { this.contentId = value; }
		}
		[XmlAttribute]
		public string Deadline {
			get { return this.deadline; }
			set { this.deadline = value; }
		}
	}


	[Serializable]
	public class DeadlineTableSortedDic : IDeadlineTable{
		private SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
		[NonSerialized]
		private object lockDic = new object();
		
		public DeadlineTableSortedDic() {
		}

		public void SerializeDeadlineTableInXml(string filename) {
			try {
				using (TextWriter tw = new StreamWriter(filename)) {
					XmlSerializer serializer = new XmlSerializer(typeof(ContentIdDeadlinePair[]));
					ContentIdDeadlinePair[] pairs;
					lock (this.lockDic) {
						pairs = Array.ConvertAll<KeyValuePair<string, string>, ContentIdDeadlinePair>(
							new List<KeyValuePair<string, string>>(this.dic).ToArray(),
							delegate(KeyValuePair<string, string> input) {
								return new ContentIdDeadlinePair(input.Key, input.Value);
							});
					}
					serializer.Serialize(tw, pairs);
				}
			} catch {
			}
		}
		public void DeserializeDeadlineTableFromXml(string filename) {
			try {
				using (TextReader tr = new StreamReader(filename)) {
					XmlSerializer serializer = new XmlSerializer(typeof(ContentIdDeadlinePair[]));
					ContentIdDeadlinePair[] deserializedPairs = serializer.Deserialize(tr) as ContentIdDeadlinePair[];
					lock (this.lockDic) {
						this.dic.Clear();
						foreach (ContentIdDeadlinePair pair in deserializedPairs) {
							this.dic.Add(pair.ContentId, pair.Deadline);
						}
					}
				}
			} catch {
			}
		}

		public void SetDeadline(string contId, string deadline) {
			lock (this.lockDic) {
				this.dic[contId] = deadline;
			}
		}
		public bool RemoveDeadlineOf(string contId) {
			lock(this.lockDic) {
				return this.dic.Remove(contId);
			}
		}
		public void ClearDeadlines() {
			lock(this.lockDic) {
				this.dic.Clear();
			}
		}
		public bool ContainsDeadlineOf(string contId) {
			lock(this.lockDic) {
				return this.dic.ContainsKey(contId);
			}
		}
		public bool TryGetDeadline(string contId, out string deadline) {
			lock(this.lockDic) {
				return this.dic.TryGetValue(contId, out deadline);
			}
		}
		public IEnumerable<string> ListContentIds() {
			lock(this.lockDic) {
				return this.dic.Keys;
			}
		}
		public int Count {
			get {
				lock(this.lockDic) {
					return this.dic.Count;
				}
			}
		}
		
		[OnDeserializing]
		private void OnDeserializingMethod(StreamingContext context) {
			this.lockDic = new object();
		}
	}
}
