using System;
using System.Collections.Generic;
using System.Text;

namespace Yusen.GCrawler {
	public interface IDeadLineDictionaryReadOnly {
		bool ContainsDeadLineOf(string contId);
		bool TryGetDeadLine(string contId, out string deadLine);
		IEnumerable<string> ListContentIds();
		int Count { get;}
	}
	public interface IDeadLineDictionary : IDeadLineDictionaryReadOnly{
		void SetDeadLine(string contId, string deadLine);
		bool RemoveDeadLineOf(string contId);
		void ClearDeadLines();
	}
	
	[Serializable]
	public class DeadLineDictionarySorted : IDeadLineDictionary{
		private SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
		
		public DeadLineDictionarySorted() {
		}
		
		public void SetDeadLine(string contId, string deadLine) {
			this.dic[contId] = deadLine;
		}
		public bool RemoveDeadLineOf(string contId) {
			return this.dic.Remove(contId);
		}
		public void ClearDeadLines() {
			this.dic.Clear();
		}
		public bool ContainsDeadLineOf(string contId) {
			return this.dic.ContainsKey(contId);
		}
		public bool TryGetDeadLine(string contId, out string deadLine) {
			return this.dic.TryGetValue(contId, out deadLine);
		}
		public IEnumerable<string> ListContentIds() {
			return this.dic.Keys;
		}
		public int Count {
			get {return this.dic.Count;}
		}
	}
}
