using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	class ListViewItemComparer : IComparer<ListViewItem>, IComparer, ICloneable {
		private int index;
		private bool descending = false;

		public ListViewItemComparer()
			: this(0) {
		}

		public ListViewItemComparer(int index) {
			this.index = index;
		}

		public int Compare(ListViewItem x, ListViewItem y) {
			int i = string.Compare(x.SubItems[this.index].Text, y.SubItems[this.index].Text);
			return this.descending ? -i : i;
		}
		public int Compare(object x, object y) {
			return this.Compare(x as ListViewItem, y as ListViewItem);
		}

		public bool SameIndexAs(ListViewItemComparer obj) {
			if (null == obj) return false;
			return this.index.Equals(obj.index);
		}
		public void Toggle() {
			this.descending = ! this.descending;
		}
		
		public object Clone() {
			return this.MemberwiseClone();
		}

	}
}
