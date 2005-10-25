using System;
using System.Collections.Generic;
using System.Text;

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
		
		public DeadlineTableSortedDic() {
		}
		
		public void SetDeadline(string contId, string deadline) {
			lock (this.dic) {
				this.dic[contId] = deadline;
			}
		}
		public bool RemoveDeadlineOf(string contId) {
			lock (this.dic) {
				return this.dic.Remove(contId);
			}
		}
		public void ClearDeadlines() {
			lock (this.dic) {
				this.dic.Clear();
			}
		}
		public bool ContainsDeadlineOf(string contId) {
			lock (this.dic) {
				return this.dic.ContainsKey(contId);
			}
		}
		public bool TryGetDeadline(string contId, out string deadline) {
			lock (this.dic) {
				return this.dic.TryGetValue(contId, out deadline);
			}
		}
		public IEnumerable<string> ListContentIds() {
			lock (this.dic) {
				return this.dic.Keys;
			}
		}
		public int Count {
			get {
				lock (this.dic) {
					return this.dic.Count;
				}
			}
		}
	}
}
