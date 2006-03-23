using System;
using System.Collections.Generic;
using System.Text;

namespace Yusen.GExplorer {
	class PlayList : ItemsManagerBase<ContentAdapter> {
		private static PlayList instance = new PlayList();
		public static PlayList Instance {
			get { return PlayList.instance; }
		}
		
		public event EventHandler PlayListChanged;
		public event EventHandler CurrentContentChanged;
		public event EventHandler AdditionRejected;

		private bool updating = false;
		private bool updated = false;
		private bool rejectedWhileUpdating = false;
		private ContentAdapter currentContent = null;
		private List<ContentAdapter> lastAdditionRejectedContents = new List<ContentAdapter>();

		public void MoveToTop(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx > 0) {
				this.items.Remove(cont);
				this.items.Insert(0, cont);
				this.OnChanged();
			}
		}
		public void MoveUp(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx > 0) {
				base.Swap(contIdx, contIdx-1);
			}
		}
		public void MoveDown(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx < this.items.Count -1) {
				base.Swap(contIdx, contIdx+1);
			}
		}
		public void MoveToBottom(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			if (contIdx < this.items.Count -1) {
				this.items.Remove(cont);
				this.items.Add(cont);
				this.OnChanged();
			}
		}

		public ContentAdapter NextContentOf(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			//リストにない
			if (contIdx < 0) {
				if (this.items.Count > 0) {
					return this.items[0];
				} else {
					return null;
				}
			}
			//リスト末尾
			if (contIdx+1 == this.items.Count) {
				return null;
			}
			return this.items[contIdx+1];
		}
		public ContentAdapter PrevContentOf(ContentAdapter cont) {
			int contIdx = this.items.IndexOf(cont);
			//リストにない
			if (contIdx < 0) {
				if (this.items.Count > 0) {
					return this.items[this.items.Count -1];
				} else {
					return null;
				}
			}
			//リスト先頭
			if (0 == contIdx) {
				return null;
			}
			return this.items[contIdx-1];
		}
		
		public void BeginUpdate() {
			lock (this) {
				if (this.updating) throw new InvalidOperationException("updatingなのにBeginUpdate呼び出し");
				this.updating = true;
				this.updated = false;
				this.rejectedWhileUpdating = false;
				this.lastAdditionRejectedContents.Clear();
			}
		}
		public void EndUpdate() {
			lock (this) {
				if (!this.updating) throw new InvalidOperationException("非updatingなのにEndUpdate呼び出し");
				this.updating = false;
				if (this.updated) this.OnChanged();
				if (this.rejectedWhileUpdating) this.OnAdditionRejectedBecauseOfExisting();
			}
		}
		public ContentAdapter CurrentContent {
			get { return this.currentContent; }
			set {
				if (value != this.currentContent) {
					this.currentContent = value;
					this.OnCurrentContentChanged();
				}
			}
		}
		public ContentAdapter[] LastAdditionRejectedContents {
			get { return this.lastAdditionRejectedContents.ToArray(); }
		}
		
		public bool HasCurrentContent {
			get {
				return null != this.currentContent;
			}
		}
		public bool IsCurrentContent(ContentAdapter cont) {
			return cont.Equals(this.currentContent);
		}
		private void OnCurrentContentChanged() {
			EventHandler handler = this.CurrentContentChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnAdditionRejectedBecauseOfExisting() {
			EventHandler handler = this.AdditionRejected;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		protected override void OnChanged() {
			if (this.updating) {
				this.updated = true;
				return;
			}
			if (null != this.PlayListChanged) {
				this.PlayListChanged(this, EventArgs.Empty);
			}
		}
		protected override void OnAdditionRejectedBecauseOfExisting(ContentAdapter rejectedItem) {
			if (this.updating) {
				this.lastAdditionRejectedContents.Add(rejectedItem);
				this.rejectedWhileUpdating = true;
			} else {
				this.lastAdditionRejectedContents.Clear();
				this.lastAdditionRejectedContents.Add(rejectedItem);
				this.OnAdditionRejectedBecauseOfExisting();
			}
			base.OnAdditionRejectedBecauseOfExisting(rejectedItem);
		}

		protected override string FilenameForSerialization {
			get { return @"PlayList.xml"; }
		}
	}
}

