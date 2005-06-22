using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Yusen.GExplorer {
	delegate void GenreListViewGenreChangedEventHandler(GenreListView sender, GGenre genre, int cntCount);
	delegate void GenreListViewSelectedContentsChangedEventHandler(GenreListView sender, IEnumerable<GContent> contents);
	delegate void GenreListViewColumnWidthChanged(GenreListView sender);
	
	partial class GenreListView : UserControl{
		private enum ImageIndex : int{
			IsNew = 1,
			IsNotNew = 0,
			HasSpecial = 2,
			HasNotSpecial = 0,
		}
		
		public event GenreListViewGenreChangedEventHandler GenreChanged;
		public event GenreListViewSelectedContentsChangedEventHandler SelectedContentsChanged;
		public event GenreListViewColumnWidthChanged ColumnWidthChanged;
		
		private GGenre genre = null;
		
		public GenreListView() {
			this.InitializeComponent();
			this.LoadCommands();
			
			//項目の選択
			this.lviewGenre.SelectedIndexChanged += new EventHandler(delegate(object sender, EventArgs e) {
				if(null != this.SelectedContentsChanged){
					List<GContent> contents = new List<GContent>();
					foreach(ListViewItem item in this.lviewGenre.SelectedItems) {
						contents.Add(item.Tag as GContent);
					}
					this.SelectedContentsChanged(this, contents);
				}
			});
			//項目をダブルクリック
			this.lviewGenre.DoubleClick += new EventHandler(this.Play);
			//Enterキーでも再生
			this.lviewGenre.KeyDown += new KeyEventHandler(
				delegate(object sender, KeyEventArgs e) {
					if(Keys.Enter == e.KeyCode) {
						this.Play(sender, e);
					}
				});
			//コンテキストメニューのアイテム有効無効切り替え
			this.cmsListView.Opening += new CancelEventHandler(this.cmsListView_Opening);
			//コンテキストメニューのイベント
			this.tsmiPlay.Click += new EventHandler(this.Play);
			this.tsmiWMP.Click += new EventHandler(delegate(object sender, EventArgs e) {
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					Utility.PlayWithWMP(((GContent)selItem.Tag).MediaFileUri);
				}
			});
			this.tsmiProperty.Click += new EventHandler(delegate(object sender, EventArgs e) {
				if(this.lviewGenre.SelectedItems.Count > 0) {
					ContentPropertyViewer.Instance.Show();
					ContentPropertyViewer.Instance.Content = (GContent)this.lviewGenre.SelectedItems[0].Tag;
					ContentPropertyViewer.Instance.Focus();
				}
			});
			this.tsmiDetail.Click += new EventHandler(delegate(object sender, EventArgs e) {
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					Utility.BrowseWithIE(((GContent)selItem.Tag).DetailPageUri);
				}
			});
			this.tsmiPackage.Click += new EventHandler(delegate(object sender, EventArgs e) {
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					Utility.BrowseWithIE(((GContent)selItem.Tag).Package.PackagePageUri);
				}
			});
			this.tsmiSpecial.Click += new EventHandler(delegate(object sender, EventArgs e){
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					GContent cont = (GContent)selItem.Tag;
					if(cont.Package.HasSpecialPage) {
						Utility.BrowseWithIE(cont.Package.SpecialPageUri);
					}
				}
			});
			this.tsmiGenre.Click += new EventHandler(delegate(object sender, EventArgs e) {
				Utility.BrowseWithIE(this.genre.GenreTopPageUri);
			});
			//コンテキストメニューの標準項目
			this.tsmiPlay.Font = new Font(this.tsmiPlay.Font, FontStyle.Bold);
			//外部コマンド
			UserCommandsManager.Instance.UserCommandsChanged += new UserCommandsChangedEventHandler(this.LoadCommands);
			this.Disposed += new EventHandler(delegate(object sender, EventArgs e) {
				UserCommandsManager.Instance.UserCommandsChanged -= this.LoadCommands;
			});
			//カラム幅の変更
			this.lviewGenre.ColumnWidthChanged += delegate {
				if(null != this.ColumnWidthChanged) {
					this.ColumnWidthChanged(this);
				}
			};
		}
		
		private void Clear() {
			this.lviewGenre.Items.Clear();
			this.lviewGenre.Groups.Clear();
		}
		
		private void Display(GGenre genre) {
			this.Clear();
			foreach(GPackage p in genre.Packages) {
				ListViewGroup group = new ListViewGroup(
					"<" + p.PackageId + "> " + p.PackageName + " [" + p.SubgenreName + "]");
				group.Tag = p;
				this.lviewGenre.Groups.Add(group);
				foreach(GContent c in p.Contents) {
					ListViewItem item = new ListViewItem(
						new string[] { c.ContentId, c.Limit, c.ContentName, c.Lead },
						group);
					item.ImageIndex = (int)
						((c.IsNew ? ImageIndex.IsNew : ImageIndex.IsNotNew)
						 | (c.Package.HasSpecialPage? ImageIndex.HasSpecial : ImageIndex.HasNotSpecial));
					item.Tag = c;
					
					this.lviewGenre.Items.Add(item);
				}
			}
			if(null != this.GenreChanged) this.GenreChanged(this, genre, this.lviewGenre.Items.Count);
		}
		
		public GGenre Genre {
			get {
				return this.genre;
			}
			set {
				if(null == value) {
					this.Clear();
					this.genre = null;
				} else {
					this.Display(value);
					this.genre = value;
				}
			}
		}
		
		private void Play(object sender, EventArgs e) {
			foreach(ListViewItem selitem in this.lviewGenre.SelectedItems) {
				PlayerForm.Instance.Show();
				PlayerForm.Instance.Content = (GContent)selitem.Tag;
				PlayerForm.Instance.Focus();
				break;
			}
		}
		
		public ListView ListView {
			get {
				return this.lviewGenre;
			}
		}
		
		private void cmsListView_Opening(object sender, CancelEventArgs e) {
			bool isSelected = (0 < this.lviewGenre.SelectedItems.Count);
			this.tsmiPlay.Enabled = isSelected;
			this.tsmiWMP.Enabled = isSelected;
			this.tsmiProperty.Enabled = isSelected;
			this.tsmiDetail.Enabled = isSelected;
			this.tsmiPackage.Enabled = isSelected;
			this.tsmiCommands.Enabled = isSelected && 0<this.tsmiCommands.DropDownItems.Count;
			
			this.tsmiSpecial.Enabled = false;
			foreach(ListViewItem item in this.lviewGenre.SelectedItems) {
				if(((GContent)item.Tag).Package.HasSpecialPage) {
					this.tsmiSpecial.Enabled = true;
					break;
				}
			}
			this.tsmiGenre.Enabled = (null != this.genre);
		}
		
		private void LoadCommands() {
			this.tsmiCommands.DropDownItems.Clear();
			foreach(UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem mi = new ToolStripMenuItem(
					uc.Title, null,
					new EventHandler(delegate(object sender, EventArgs e) {
						List<GContent> contents = new List<GContent>();
						foreach(ListViewItem lvi in this.lviewGenre.SelectedItems) {
							contents.Add((GContent)lvi.Tag);
						}
						((UserCommand)((ToolStripMenuItem)sender).Tag).Execute(contents);
					}));
				mi.Tag = uc;
				this.tsmiCommands.DropDownItems.Add(mi);
			}
			this.tsmiCommands.Enabled = 0 != this.tsmiCommands.DropDownItems.Count;
		}
	}
}
