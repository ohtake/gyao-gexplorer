using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace Yusen.GExplorer.OldApp {
	abstract class ItemsManagerBase<T> : IEnumerable<T>{
		protected List<T> items;
		
		protected ItemsManagerBase() {
			this.items = new List<T>();
		}
		
		public void DeserializeItems() {
			this.DeserializeItems(this.FilenameForSerialization);
		}
		public void DeserializeItems(string filename) {
			List<T> items;
			if (Utility.TryDeserializeSettings(filename, out items)) {
				this.items = items;
				this.OnChanged();
			}
		}
		public void SerializeItems() {
			this.SerializeItems(this.FilenameForSerialization);
		}
		public void SerializeItems(string filename) {
			Utility.SerializeSettings(filename, this.items);
		}
		
		protected abstract void OnChanged();
		protected virtual void OnAdditionRejectedBecauseOfExisting(T rejectedItem) {
		}
		
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
			if (this.items.Remove(item)) {
				this.OnChanged();
			}
		}
		public void RemoveAt(int idx) {
			this.items.RemoveAt(idx);
			this.OnChanged();
		}
		public void RemoveAll(Predicate<T> pred) {
			if(this.items.RemoveAll(pred) > 0) {
				this.OnChanged();
			}
		}
		public void AddIfNotExists(T item) {
			if (!this.items.Contains(item)) {
				this.items.Add(item);
				this.OnChanged();
			} else {
				this.OnAdditionRejectedBecauseOfExisting(item);
			}
		}
		public void Add(T item) {
			this.items.Add(item);
			this.OnChanged();
		}
		public void AddRange(IEnumerable<T> col) {
			this.items.AddRange(col);
			this.OnChanged();
		}
		public void Insert(int idx, T item) {
			this.items.Insert(idx, item);
			this.OnChanged();
		}
		public int IndexOf(T item) {
			return this.items.IndexOf(item);
		}
		public void Swap(int x, int y) {
			T cx = this.items[x];
			this.items[x] = this.items[y];
			this.items[y] = cx;
			this.OnChanged();
		}
		public void Clear() {
			this.items.Clear();
			this.OnChanged();
		}
		public void Sort(Comparison<T> comparison) {
			this.items.Sort(comparison);
			this.OnChanged();
		}
		public bool Contains(T item) {
			return this.items.Contains(item);
		}
		public void SetAll(IEnumerable<T> items) {
			this.items = new List<T>(items);
			this.OnChanged();
		}
		protected abstract string FilenameForSerialization { get;}
	}
}
