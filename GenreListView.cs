using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Yusen.GExplorer {
	delegate void GenreListViewRefleshedEventHandler(GenreListView sender, GGenre genre, int cntCount);
	
	partial class GenreListView : UserControl, IUsesUserSettings{
		private enum ImageIndex : int{
			IsNew = 1,
			IsNotNew = 0,
			HasSpecial = 2,
			HasNotSpecial = 0,
		}
		
		public event GenreListViewRefleshedEventHandler Refreshed;
		private GGenre genre = null;
		
		public GenreListView() {
			this.InitializeComponent();
			this.LoadSettings();
			this.LoadCommands();
			
			//項目の選択
			this.lviewGenre.SelectedIndexChanged += new EventHandler(delegate(object sender, EventArgs e) {
				if(this.lviewGenre.SelectedItems.Count > 0) {
					ContentPropertyViewer.Instance.Content = 
						(GContent)this.lviewGenre.SelectedItems[0].Tag;
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
			//表示オプション
			this.tsmiMultipulSelect.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.lviewGenre.MultiSelect = this.tsmiMultipulSelect.Checked;
				this.SaveSettings();
			});
			this.tsmiFullRowSelect.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.lviewGenre.FullRowSelect = this.tsmiFullRowSelect.Checked;
				this.SaveSettings();
			});
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
					this.SaveSettings();
				});
				item.Checked = (v == this.lviewGenre.View);
				this.tsmiView.DropDownItems.Add(item);
			}
			//コンテキストメニューの標準項目
			this.tsmiPlay.Font = new Font(this.tsmiPlay.Font, FontStyle.Bold);
			//ユーザ設定
			this.lviewGenre.ColumnWidthChanged +=
				new ColumnWidthChangedEventHandler(this.SaveToUserSettings);
			UserSettings.Instance.ChangeCompleted +=
				new UserSettingsChangeCompletedEventHandler(this.LoadSettings);
			this.Disposed += new EventHandler(delegate(object sender, EventArgs e) {
				UserSettings.Instance.ChangeCompleted -= this.LoadSettings;
			});
			//外部コマンド
			UserCommandsManager.Instance.UserCommandsChanged += new UserCommandsChangedEventHandler(this.LoadCommands);
			this.Disposed += new EventHandler(delegate(object sender, EventArgs e) {
				UserCommandsManager.Instance.UserCommandsChanged -= this.LoadCommands;
			});
		}
		
		public void Clear() {
			this.lviewGenre.Items.Clear();
			this.lviewGenre.Groups.Clear();
		}
		
		public void Display(GGenre genre) {
			this.genre = genre;
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
			if(null != this.Refreshed) this.Refreshed(this, genre, this.lviewGenre.Items.Count);
		}
		
		private void Play(object sender, EventArgs e) {
			foreach(ListViewItem selitem in this.lviewGenre.SelectedItems) {
				PlayerForm.Instance.Show();
				PlayerForm.Instance.Content = (GContent)selitem.Tag;
				PlayerForm.Instance.Focus();
				break;//再生ウィンドウを一つのみしか表示しないようにしてみる
				//new PlayerForm((GContent)selitem.Tag).Show();
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

		public void LoadSettings(){
			UserSettings settings = UserSettings.Instance;
			this.lviewGenre.MultiSelect = settings.LvMultiSelect;
			this.lviewGenre.FullRowSelect = settings.LvFullRowSelect;
			this.lviewGenre.View = settings.LvView;
			if(this.chId.Width != settings.LvColWidthId) this.chId.Width = settings.LvColWidthId;
			if(this.chLimit.Width != settings.LvColWidthLimit) this.chLimit.Width = settings.LvColWidthLimit;
			if(this.chEpisode.Width != settings.LvColWidthEpisode) this.chEpisode.Width = settings.LvColWidthEpisode;
			if(this.chLead.Width != settings.LvColWidthLead) this.chLead.Width = settings.LvColWidthLead;
			
			this.tsmiMultipulSelect.Checked = this.lviewGenre.MultiSelect;
			this.tsmiFullRowSelect.Checked = this.lviewGenre.FullRowSelect;
			foreach(ToolStripMenuItem mi in this.tsmiView.DropDownItems) {
				mi.Checked = ((View)mi.Tag == this.lviewGenre.View);
			}
		}
		private void SaveToUserSettings(object sender, EventArgs e) {
			this.SaveSettings();
		}
		public void SaveSettings(){
			UserSettings settings = UserSettings.Instance;
			settings.LvMultiSelect = this.lviewGenre.MultiSelect;
			settings.LvFullRowSelect = this.lviewGenre.FullRowSelect;
			settings.LvView = this.lviewGenre.View;
			settings.LvColWidthId = this.chId.Width;
			settings.LvColWidthLimit = this.chLimit.Width;
			settings.LvColWidthEpisode = this.chEpisode.Width;
			settings.LvColWidthLead = this.chLead.Width;
			
			settings.OnChangeCompleted();
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
