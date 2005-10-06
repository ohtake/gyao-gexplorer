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
			this.dic[contId] = deadline;
		}
		public bool RemoveDeadlineOf(string contId) {
			return this.dic.Remove(contId);
		}
		public void ClearDeadlines() {
			this.dic.Clear();
		}
		public bool ContainsDeadlineOf(string contId) {
			return this.dic.ContainsKey(contId);
		}
		public bool TryGetDeadline(string contId, out string deadline) {
			return this.dic.TryGetValue(contId, out deadline);
		}
		public IEnumerable<string> ListContentIds() {
			return this.dic.Keys;
		}
		public int Count {
			get {return this.dic.Count;}
		}
	}
}
