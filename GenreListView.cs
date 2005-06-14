using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	delegate void GenreListViewRefleshedEventHandler(GenreListView sender, GGenre genre, int cntCount);
	
	partial class GenreListView : UserControl {
		private enum ImageIndex : int{
			IsNew = 1,
			IsNotNew = 0,
			HasSpecial = 2,
			HasNotSpecial = 0,
		}

		public event GenreListViewRefleshedEventHandler Refreshed;
		private GGenre genre = null;
		
		public GenreListView() {
			InitializeComponent();
			
			// 表示形式の選択
			foreach(View v in Enum.GetValues(typeof(View))) {
				ToolStripMenuItem item = new ToolStripMenuItem(v.ToString());
				item.Tag = v;
				item.Click += new EventHandler(delegate(object sender, EventArgs e) {
					ToolStripMenuItem selItem = (ToolStripMenuItem)sender;
					foreach(ToolStripMenuItem mi in this.tsmiView.DropDownItems) {
						mi.Checked = false;
					}
					selItem.Checked = true;
					this.lviewGenre.View = (View)selItem.Tag;
				});
				item.Checked = (v == this.lviewGenre.View);
				this.tsmiView.DropDownItems.Add(item);
			}
			//項目をダブルクリック
			this.lviewGenre.DoubleClick += new EventHandler(this.Play);
			//Enterキーでも再生
			this.lviewGenre.KeyDown += new KeyEventHandler(lviewGenre_KeyDown);
			//コンテキストメニューのイベント
			this.cmsListView.Opening += new CancelEventHandler(cmsListView_Opening);
			this.tsmiPlay.Click += new EventHandler(this.Play);
			this.tsmiDetail.Click += new EventHandler(delegate(object sender, EventArgs e) {
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					Utility.BrowseByIE(((GContent)selItem.Tag).DetailPageUri);
				}
			});
			this.tsmiPackage.Click += new EventHandler(delegate(object sender, EventArgs e) {
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					Utility.BrowseByIE(((GContent)selItem.Tag).Package.PackagePageUri);
				}
			});
			this.tsmiSpecial.Click += new EventHandler(delegate(object sender, EventArgs e){
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					GContent cont = (GContent)selItem.Tag;
					if(cont.Package.HasSpecialPage) {
						Utility.BrowseByIE(cont.Package.SpecialPageUri);
					}
				}
			});
			this.tsmiGenre.Click += new EventHandler(delegate(object sender, EventArgs e) {
				Utility.BrowseByIE(this.genre.GenreTopPageUri);
			});
			//コンテキストメニューの標準項目
			this.tsmiPlay.Font = new Font(this.tsmiPlay.Font, FontStyle.Bold);
		}
		
		void cmsListView_Opening(object sender, CancelEventArgs e) {
			bool isSelected = (0 < this.lviewGenre.SelectedItems.Count);
			this.tsmiPlay.Enabled = isSelected;
			this.tsmiDetail.Enabled = isSelected;
			this.tsmiPackage.Enabled = isSelected;
			
			this.tsmiSpecial.Enabled = isSelected;
			
			this.tsmiGenre.Enabled = (null != this.genre);
		}
		
		public void Clear() {
			this.lviewGenre.Items.Clear();
			this.lviewGenre.Groups.Clear();
		}
		
		public void Display(GGenre genre) {
			this.genre = genre;
			this.Clear();
			if(!genre.IsLoaded) genre.FetchAll();
			foreach(GPackage p in genre.Packages) {
				ListViewGroup group = new ListViewGroup(p.ToString());
				group.Tag = p;
				this.lviewGenre.Groups.Add(group);
				foreach(GContent c in p.Contents) {
					ListViewItem item = new ListViewItem(
						new string[] { c.ContentId, c.Limit, c.ContentName, c.Lead },
						group);
					item.ImageIndex =
						(int)(c.IsNew ? ImageIndex.IsNew : ImageIndex.IsNotNew)
						| (int)(c.Package.HasSpecialPage? ImageIndex.HasSpecial : ImageIndex.HasNotSpecial);
					item.Tag = c;
					
					this.lviewGenre.Items.Add(item);
				}
			}
			if(null != this.Refreshed) this.Refreshed(this, genre, this.lviewGenre.Items.Count);
		}
		
		private void lviewGenre_KeyDown(object sender, KeyEventArgs e) {
			if(Keys.Enter == e.KeyCode) {
				this.Play(sender, e);
			}
		}
		private void Play(object sender, EventArgs e) {
			foreach(ListViewItem selitem in this.lviewGenre.SelectedItems) {
				new PlayerForm((GContent)selitem.Tag).Show();
			}
		}
	}
}
