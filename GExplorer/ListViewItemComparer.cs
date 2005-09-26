using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	class ListViewItemComparer : IComparer<ListViewItem>, IComparer, ICloneable {
		private int index;
		private SortOrder order = SortOrder.Ascending;

		public ListViewItemComparer()
			: this(0) {
		}
		public ListViewItemComparer(int index) {
			this.index = index;
		}

		public int Index {
			get { return this.index; }
			set { this.index = value; }
		}
		public SortOrder Order {
			get { return this.order; }
			set { this.order = value; }
		}

		public int Compare(ListViewItem x, ListViewItem y) {
			bool descending = false;
			switch (this.order) {
				case SortOrder.None:
					return 0;
				case SortOrder.Descending:
					descending = true;
					break;
			}
			int i = string.Compare(x.SubItems[this.index].Text, y.SubItems[this.index].Text);
			return descending ? -i : i;
		}
		public int Compare(object x, object y) {
			return this.Compare(x as ListViewItem, y as ListViewItem);
		}

		public bool SameIndexAs(ListViewItemComparer obj) {
			if (null == obj) return false;
			return this.index.Equals(obj.index);
		}
		public void Toggle() {
			switch (this.Order) {
				case SortOrder.Ascending:
					this.Order = SortOrder.Descending;
					break;
				case SortOrder.Descending:
					this.Order = SortOrder.Ascending;
					break;
			}
		}
		
		public object Clone() {
			return this.MemberwiseClone();
		}
	}
}
