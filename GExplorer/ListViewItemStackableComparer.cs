using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	public class ListViewItemStackableComparer : IComparer<ListViewItem>, IComparer, ICloneable {
		private const int DefaultMaxLength = 3;

		private readonly int maxLength;
		private List<int> columns;
		private List<SortOrder> orders;
		
		public ListViewItemStackableComparer()
			: this(ListViewItemStackableComparer.DefaultMaxLength) {
		}
		private ListViewItemStackableComparer(int maxLength) {
			this.maxLength = maxLength;
			this.columns = new List<int>(maxLength);
			this.orders = new List<SortOrder>(maxLength);
		}

		public void StackColumnIndex(int colIndex) {
			int stackIdx = this.columns.IndexOf(colIndex);
			if (0 == stackIdx) {//同一カラムでソート指定なのでソート順序反転
				this.orders[0] = this.orders[0] == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
			} else if (stackIdx > 0) {//最重要ではなかったので最重要カラムに持ってくる
				this.columns.RemoveAt(stackIdx);
				this.orders.RemoveAt(stackIdx);
				this.columns.Insert(0, colIndex);
				this.orders.Insert(0, SortOrder.Ascending);
			} else {//ソートしていなかったカラムを追加
				if (this.maxLength == this.columns.Count) {//スタックあふれ
					this.columns.RemoveAt(this.maxLength - 1);
					this.orders.RemoveAt(this.maxLength - 1);
				}
				this.columns.Insert(0, colIndex);
				this.orders.Insert(0, SortOrder.Ascending);
			}
		}

		public int Compare(ListViewItem x, ListViewItem y) {
			for (int i = 0; i < this.columns.Count; i++) {
				int colIdx = this.columns[i];
				bool descending = false;
				switch (this.orders[i]) {
					case SortOrder.None:
						continue;
					case SortOrder.Descending:
						descending = true;
						break;
				}
				int r = string.Compare(x.SubItems[colIdx].Text, y.SubItems[colIdx].Text);
				if (0 == r) continue;
				return descending ? -r : r;
			}
			return 0;
		}
		public int Compare(object x, object y) {
			return this.Compare(x as ListViewItem, y as ListViewItem);
		}

		public ListViewItemStackableComparer Clone() {
			ListViewItemStackableComparer c = this.MemberwiseClone() as ListViewItemStackableComparer;
			c.columns = new List<int>(c.columns);
			c.orders = new List<SortOrder>(c.orders);
			return c;
		}
		
		object ICloneable.Clone(){
			return this.Clone();
		}
	}
}
