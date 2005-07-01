using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Yusen.GExplorer {
	abstract class ItemsManagerBase<T> : IEnumerable<T>{
		protected List<T> items;
		
		protected ItemsManagerBase() {
			this.items = new List<T>();
		}
		
		public void Deserialize() {
			TextReader tr = null;
			try {
				XmlSerializer xs = new XmlSerializer(typeof(List<T>));
				tr = new StreamReader(this.XmlFileName);
				this.items = xs.Deserialize(tr) as List<T>;
			} catch(FileNotFoundException) {
				return;
			} catch(Exception e) {
				Utility.DisplayException(e);
			} finally {
				if(null != tr) {
					tr.Close();
				}
				this.OnChanged();
			}
		}
		public void Serialize() {
			TextWriter tw = null;
			try {
				XmlSerializer xs = new XmlSerializer(typeof(List<T>));
				tw = new StreamWriter(this.XmlFileName);
				xs.Serialize(tw, this.items);
			} catch(Exception e) {
				Utility.DisplayException(e);
			} finally {
				if(null != tw) {
					tw.Close();
				}
			}
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
		
		abstract protected string XmlFileName { get;}
	}
}
