using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace Yusen.GExplorer {
	abstract class ItemsManagerBase<T> : IEnumerable<T>{
		protected List<T> items;
		
		protected ItemsManagerBase() {
			this.items = new List<T>();
		}
		
		public void DeserializeItems() {
			List<T> items;
			if (Utility.TryDeserializeSettings(this.FilenameForSerialization, out items)) {
				this.items = items;
			}
		}
		public void SerializeItems() {
			Utility.SerializeSettings(this.FilenameForSerialization, this.items);
		}
		
		abstract protected void OnChanged();
		
		public T this[int idx] {
			get {
				return this.items[idx];
			}
			set {
				this.items[idx] = value;
				this.OnChanged();
			}
		}
		public int Count {
			get {
				return this.items.Count;
			}
		}
		public IEnumerator<T> GetEnumerator() {
			return this.items.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
		public void Remove(T item) {
			this.items.Remove(item);
			this.OnChanged();
		}
		public void RemoveAt(int idx) {
			this.items.RemoveAt(idx);
			this.OnChanged();
		}
		public void Add(T item) {
			this.items.Add(item);
			this.OnChanged();
		}
		public void Insert(int idx, T item) {
			this.items.Insert(idx, item);
			this.OnChanged();
		}
		public void Swap(int x, int y) {
			T cx = this[x];
			this[x] = this[y];
			this[y] = cx;
			this.OnChanged();
		}
		protected abstract string FilenameForSerialization { get;}
	}
}