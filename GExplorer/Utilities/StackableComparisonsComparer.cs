using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Yusen.GExplorer.Utilities {
	public sealed class StackableComparisonsComparer<T> : IComparer<T> {
		public const int DefaultMaxStackSize = 3;
		private readonly int maxStackSize;
		private readonly List<Comparison<T>> comparisons;
		private readonly List<ListSortDirection> directions;

		public StackableComparisonsComparer() : this(StackableComparisonsComparer<T>.DefaultMaxStackSize){
		}
		public StackableComparisonsComparer(int maxStackSize) {
			this.maxStackSize = maxStackSize;
			this.comparisons = new List<Comparison<T>>(maxStackSize);
			this.directions = new List<ListSortDirection>(maxStackSize);
		}

		public void PushComparison(Comparison<T> comparison) {
			int idx = this.comparisons.IndexOf(comparison);
			if (idx == 0) {
				switch (this.directions[0]) {
					case ListSortDirection.Ascending:
						this.directions[0] = ListSortDirection.Descending;
						break;
					case ListSortDirection.Descending:
						this.directions[0] = ListSortDirection.Ascending;
						break;
				}
				return;
			} else if (idx > 0) {
				this.comparisons.RemoveAt(idx);
				this.directions.RemoveAt(idx);
			} else if (this.comparisons.Count == this.maxStackSize) {
				this.comparisons.RemoveAt(this.maxStackSize - 1);
				this.directions.RemoveAt(this.maxStackSize - 1);
			} else {
			}
			this.comparisons.Insert(0, comparison);
			this.directions.Insert(0, ListSortDirection.Ascending);
		}
		public void ClearComparisons() {
			this.comparisons.Clear();
		}

		#region IComparer<T> Members
		public int Compare(T x, T y) {
			for (int i = 0; i < this.comparisons.Count; i++) {
				int result = this.comparisons[i](x, y);
				if (0 == result) continue;
				if (this.directions[i] == ListSortDirection.Descending) result = -result;
				return result;
			}
			return 0;
		}
		#endregion
	}
}
