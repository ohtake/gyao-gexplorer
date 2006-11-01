using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.UserInterfaces {
	[Obsolete("やっぱりタブで")]
	public partial class ViewSelectControl : UserControl {
		private abstract class NodeItem {
			private string text;
			
			private NodeItem(){
			}
			protected NodeItem(string text){
				this.text = text;
			}
			public abstract void HandleDrawItem(ListBox lbox, DrawItemEventArgs e);
			public string Text {
				get { return this.text; }
				set { this.text = value; }
			}
		}
		private abstract class CategoryNodeItemBase : NodeItem {
			private bool isExpanded = true;
			protected CategoryNodeItemBase(string text)
				: base(text) {
			}
			public sealed override void HandleDrawItem(ListBox lbox, DrawItemEventArgs e) {
				string dispText = string.Format("{0} {1} ({2})", this.IsExpanded ? "-" : "+", base.Text, this.ChildNodeCount);
				using (Font font = new Font(e.Font, FontStyle.Bold)) {
					if ((e.State & DrawItemState.Selected) != DrawItemState.None) {
						e.Graphics.FillRectangle(lbox.Focused ? SystemBrushes.Highlight : SystemBrushes.Control, e.Bounds);
						e.Graphics.DrawString(dispText, font, lbox.Focused ? SystemBrushes.HighlightText : SystemBrushes.ControlText, e.Bounds.Location);
					} else {
						e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
						e.Graphics.DrawString(dispText, font, SystemBrushes.WindowText, e.Bounds.Location);
					}
				}
			}
			public bool IsExpanded {
				get { return this.isExpanded; }
				private set { this.isExpanded = value; }
			}
			public void Expand() {
				this.IsExpanded = true;
			}
			public void Collapse() {
				this.IsExpanded = false;
			}
			public void Toggle() {
				this.IsExpanded = !this.IsExpanded;
			}
			public abstract int ChildNodeCount {get; }
		}
		private sealed class CategoryNodeItem<T> : CategoryNodeItemBase where T : ChildNodeItem {
			private List<T> childNodes = new List<T>();
			public CategoryNodeItem(string text)
				: base(text) {
			}
			public void AddChildNode(T child) {
				this.childNodes.Add(child);
				child.ParentNode = this;
			}
			public bool RemoveChildNode(T child) {
				if (this.childNodes.Remove(child)) {
					child.ParentNode = null;
					return true;
				}
				return false;
			}
			public T GetChildNode(int index) {
				return this.childNodes[index];
			}
			public override int ChildNodeCount {
				get { return this.childNodes.Count; }
			}
			public void AppendToListBox(ListBox lbox) {
				lbox.Items.Add(this);
				if (this.IsExpanded) {
					lbox.Items.AddRange(this.childNodes.ToArray());
				}
			}
		}
		private abstract class ChildNodeItem : NodeItem {
			protected const int MarginLeft = 10;
			protected const int PaddingLeft = 4;
			private CategoryNodeItemBase parentNode;

			protected ChildNodeItem(string text)
				: base(text) {
			}
			private Color color = SystemColors.WindowText;
			protected Color Color {
				get { return this.color; }
				set { this.color = value; }
			}
			public CategoryNodeItemBase ParentNode {
				get { return this.parentNode; }
				set { this.parentNode = value; }
			}
			
			public sealed override void HandleDrawItem(ListBox lbox, DrawItemEventArgs e) {
				int height = e.Bounds.Height;
				if ((e.State & DrawItemState.Selected) != DrawItemState.None) {
					Color bgColor, fgColor;
					if (lbox.Focused) {
						bgColor = this.Color;
						fgColor = Color.White;
					} else {
						bgColor = SystemColors.Control;
						fgColor = SystemColors.ControlText;
					}
					
					using (SolidBrush bgBrush = new SolidBrush(bgColor)) {
						Rectangle bgRect = new Rectangle(e.Bounds.Left + ChildNodeItem.MarginLeft, e.Bounds.Top, e.Bounds.Width - ChildNodeItem.MarginLeft, height);
						e.Graphics.FillRectangle(bgBrush, bgRect);
					}
					if (! lbox.Focused) {
						using (SolidBrush bgBrush = new SolidBrush(this.Color)) {
							Rectangle bgRect = new Rectangle(e.Bounds.Left + ChildNodeItem.MarginLeft, e.Bounds.Top, ChildNodeItem.PaddingLeft, height);
							e.Graphics.FillRectangle(bgBrush, bgRect);
						}
					}
					using (SolidBrush fgBrush = new SolidBrush(fgColor)) {
						Point location = e.Bounds.Location;
						location.X += ChildNodeItem.MarginLeft + ChildNodeItem.PaddingLeft;
						e.Graphics.DrawString(base.Text, e.Font, fgBrush, location);
					}
				} else {
					e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
					using (SolidBrush bgBrush = new SolidBrush(this.Color)) {
						Rectangle bgRect = new Rectangle(e.Bounds.Left + ChildNodeItem.MarginLeft, e.Bounds.Top, ChildNodeItem.PaddingLeft, height);
						e.Graphics.FillRectangle(bgBrush, bgRect);
					}
					Point location = e.Bounds.Location;
					location.X += ChildNodeItem.MarginLeft + ChildNodeItem.PaddingLeft;
					e.Graphics.DrawString(base.Text, e.Font, SystemBrushes.WindowText, location);
				}
			}
		}
		private sealed class GenreNodeItem : ChildNodeItem {
			private GGenreClass genre;
			public GenreNodeItem(GGenreClass genre)
				: base(genre.GenreName) {
				this.genre = genre;
				base.Color = genre.GenreColor;
			}
			public GGenreClass Genre {
				get { return this.genre; }
			}
		}
		private sealed class VirtualNodeItem : ChildNodeItem {
			public VirtualNodeItem(string text)
				: base(text) {
			}
		}
		private sealed class PlaylistNodeItem : ChildNodeItem {
			public PlaylistNodeItem(string text)
				: base(text) {
			}
		}
		
		private CategoryNodeItem<GenreNodeItem> cnodeGGenre;
		private CategoryNodeItem<VirtualNodeItem> cnodeVGenre;
		private CategoryNodeItem<PlaylistNodeItem> cnodePCol;
		
		public ViewSelectControl() {
			InitializeComponent();

			if (base.DesignMode) return;
			
			this.splitContainer1.Panel2Collapsed = true;
		}

		private void ViewSelectView_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;

			this.cnodeGGenre = new CategoryNodeItem<GenreNodeItem>("GyaOのジャンル");
			foreach (GGenreClass genre in Program.CacheManager.GetEnumerableOfAllGenres()) {
				this.cnodeGGenre.AddChildNode(new GenreNodeItem(genre));
			}
			this.cnodeVGenre = new CategoryNodeItem<VirtualNodeItem>("仮想ジャンル");
			for(int i=0; i<5; i++){
				this.cnodeVGenre.AddChildNode(new VirtualNodeItem(string.Format("VirtualGenre {0}", i)));
			}
			this.cnodePCol = new CategoryNodeItem<PlaylistNodeItem>("プレイリストコレクション");
			for (int i = 0; i < 10; i++) {
				this.cnodePCol.AddChildNode(new PlaylistNodeItem(string.Format("Playlist {0}", i)));
			}
			this.ReInsertItems();
		}

		private void ReInsertItems() {
			object selObj = this.lboxVsc.SelectedItem;
			this.lboxVsc.BeginUpdate();
			this.lboxVsc.Items.Clear();
			this.cnodeGGenre.AppendToListBox(this.lboxVsc);
			this.cnodeVGenre.AppendToListBox(this.lboxVsc);
			this.cnodePCol.AppendToListBox(this.lboxVsc);
			this.lboxVsc.EndUpdate();
			
			if (null != selObj) {
				int newIndex = this.lboxVsc.Items.IndexOf(selObj);
				if (newIndex >= 0) {
					this.lboxVsc.SelectedIndex = newIndex;
					this.lboxVsc.TopIndex = newIndex;
				}
			}
		}
		
		private void lboxVsv_DrawItem(object sender, DrawItemEventArgs e) {
			ListBox lbox = sender as ListBox;
			NodeItem item = lbox.Items[e.Index] as NodeItem;
			item.HandleDrawItem(lbox, e);
		}

		private void lboxVsv_DoubleClick(object sender, EventArgs e) {
			NodeItem nodeItem = this.lboxVsc.SelectedItem as NodeItem;
			if (null == nodeItem) return;
			CategoryNodeItemBase cateBase = nodeItem as CategoryNodeItemBase;
			if (null != cateBase) {
				cateBase.Toggle();
				this.ReInsertItems();
				return;
			}
			//todo
		}

		private void lboxVsv_KeyDown(object sender, KeyEventArgs e) {
			bool leftFlag = false;
			switch (e.KeyCode) {
				case Keys.Left:
					leftFlag = true;
					goto case Keys.Right;
				case Keys.Right:
					NodeItem nodeItem = this.lboxVsc.SelectedItem as NodeItem;
					if (null == nodeItem) return;
					CategoryNodeItemBase cateBase = nodeItem as CategoryNodeItemBase;
					if (null != cateBase) {
						if (leftFlag && cateBase.IsExpanded) {
							cateBase.Collapse();
							this.ReInsertItems();
						} else if(!leftFlag && !cateBase.IsExpanded){
							cateBase.Expand();
							this.ReInsertItems();
						} else if (!leftFlag && cateBase.IsExpanded && cateBase.ChildNodeCount > 0) {
							cateBase.Expand();
							this.ReInsertItems();
							this.lboxVsc.SelectedIndex++;
						}
						e.Handled = true;
						return;
					}
					ChildNodeItem childNode = nodeItem as ChildNodeItem;
					if (null != childNode) {
						if (leftFlag) {
							this.lboxVsc.SelectedItem = childNode.ParentNode;
						}
						e.Handled = true;
						return;
					}
					break;
			}
			return;
		}
	}
}
