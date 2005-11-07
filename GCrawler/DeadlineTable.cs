using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

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
	
	[Serializable]
	public class DeadlineTableSortedDic : IDeadlineTable{
		private SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
		[NonSerialized]
		private object objLock = new object();
		
		public DeadlineTableSortedDic() {
		}
		
		public void SetDeadline(string contId, string deadline) {
			lock (this.objLock) {
				this.dic[contId] = deadline;
			}
		}
		public bool RemoveDeadlineOf(string contId) {
			lock(this.objLock) {
				return this.dic.Remove(contId);
			}
		}
		public void ClearDeadlines() {
			lock(this.objLock) {
				this.dic.Clear();
			}
		}
		public bool ContainsDeadlineOf(string contId) {
			lock(this.objLock) {
				return this.dic.ContainsKey(contId);
			}
		}
		public bool TryGetDeadline(string contId, out string deadline) {
			lock(this.objLock) {
				return this.dic.TryGetValue(contId, out deadline);
			}
		}
		public IEnumerable<string> ListContentIds() {
			lock(this.objLock) {
				return this.dic.Keys;
			}
		}
		public int Count {
			get {
				lock(this.objLock) {
					return this.dic.Count;
				}
			}
		}
		
		[OnDeserializing]
		private void OnDeserializingMethod(StreamingContext context) {
			this.objLock = new object();
		}
	}
}
