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
		[Flags]
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
		private AboneType aboneType = AboneType.Toumei;
		
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
			this.tsmiWMP.Click += delegate{
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					Utility.PlayWithWMP((selItem.Tag as GContent).MediaFileUri);
				}
			};
			this.tsmiProperty.Click += delegate{
				foreach(ListViewItem lvi in this.lviewGenre.SelectedItems) {
					ContentPropertyViewer.View(lvi.Tag as GContent);
					break;
				}
			};
			this.tsmiDetail.Click += delegate{
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					BrowserForm.Browse(((GContent)selItem.Tag).DetailPageUri);
					break;
				}
			};
			this.tsmiPlayerPage.Click += delegate {
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					BrowserForm.Browse(((GContent)selItem.Tag).PlayerPageUri);
					break;
				}
			};
			this.tsmiPackage.Click += delegate {
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					BrowserForm.Browse(((GContent)selItem.Tag).Package.PackagePageUri);
					break;
				}
			};
			this.tsmiSpecial.Click += delegate{
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					GContent cont = (GContent)selItem.Tag;
					if(cont.Package.HasSpecialPage) {
						BrowserForm.Browse(cont.Package.SpecialPageUri);
						break;
					}
				}
			};
			this.tsmiNgPackageId.Click += delegate {
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					GPackage p = (selItem.Tag as GContent).Package;
					NgPackagesManager.Instance.Add(new NgPackage(
						p.PackageId, "PackageId", TwoStringsPredicateMethod.Equals, p.PackageId));
				}
			};
			this.tsmiNgPackageSubgenreName.Click += delegate {
				foreach(ListViewItem selItem in this.lviewGenre.SelectedItems) {
					GPackage p = (selItem.Tag as GContent).Package;
					NgPackagesManager.Instance.Add(new NgPackage(
						p.SubgenreName, "SubgenreName", TwoStringsPredicateMethod.Equals, p.SubgenreName));
				}
			};
			this.tsmiGenre.Click += delegate{
				BrowserForm.Browse(this.Genre.GenreTopPageUri);
			};
			//コンテキストメニューの標準項目
			this.tsmiPlay.Font = new Font(this.tsmiPlay.Font, FontStyle.Bold);
			//外部コマンド
			UserCommandsManager.Instance.UserCommandsChanged += new UserCommandsChangedEventHandler(this.LoadCommands);
			this.Disposed += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= this.LoadCommands;
			};
			//カラム幅の変更
			this.lviewGenre.ColumnWidthChanged += delegate {
				if(null != this.ColumnWidthChanged) {
					this.ColumnWidthChanged(this);
				}
			};
		}
		
		private void ClearItems(GGenre oldGenre) {
			this.lviewGenre.BeginUpdate();
			this.lviewGenre.Items.Clear();
			this.lviewGenre.Groups.Clear();
			this.lviewGenre.EndUpdate();
		}
		
		private void AddItems(GGenre genre) {
			this.lviewGenre.BeginUpdate();
			foreach(GPackage p in genre.Packages) {
				//NG処理
				switch(this.AboneType) {
					case AboneType.Sabori:
						break;
					case AboneType.Toumei:
						if(NgPackagesManager.Instance.IsNgPackage(p)) continue;
						break;
					case AboneType.Hakidame:
						if(!NgPackagesManager.Instance.IsNgPackage(p)) continue;
						break;
					default:
						throw new Exception("不明なあぼ～ん方法: " + this.AboneType.ToString());
				}
				
				ListViewGroup group = new ListViewGroup(
					"<" + p.PackageId + "> " + p.PackageName + " [" + p.SubgenreName + "]");
				group.Tag = p;
				this.lviewGenre.Groups.Add(group);
				
				//読み込み失敗への仮対応
				if(!p.HasLoaded) {
					ListViewItem item = new ListViewItem(
						new string[] { "読み込み失敗？", "", "", "読み込みなおしたり並列取得を無効にしたら直るかも" },
						group);
					item.ForeColor = SystemColors.GrayText;
					item.Tag = new GContent(p, 0, "", "", false, "");
					this.lviewGenre.Items.Add(item);
					continue;
				}
				
				//「もうすぐ登場」への仮対応
				if(p.IsComingSoon) {
					ListViewItem item = new ListViewItem(
						new string[] { "もうすぐ登場", "", "", "もうすぐ登場"},
						group);
					item.ForeColor = SystemColors.GrayText;
					item.Tag = new GContent(p, 0, "", "", false, "");
					this.lviewGenre.Items.Add(item);
					continue;
				}
				
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
			this.lviewGenre.EndUpdate();
			
			if(null != this.GenreChanged) this.GenreChanged(this, genre, this.lviewGenre.Items.Count);
		}
		
		public GGenre Genre {
			get {
				return this.genre;
			}
			set {
				if(null == value) {
					this.ClearItems(this.Genre);
					this.genre = null;
				} else {
					this.ClearItems(this.Genre);
					this.genre = value;
					this.AddItems(value);
				}
			}
		}
		public void RefleshView() {
			this.Genre = this.Genre;
		}
		
		private void Play(object sender, EventArgs e) {
			foreach(ListViewItem selitem in this.lviewGenre.SelectedItems) {
				PlayerForm.Play(selitem.Tag as GContent);
				break;
			}
		}
		
		public ListView ListView {
			get {
				return this.lviewGenre;
			}
		}
		public AboneType AboneType {
			get {
				return this.aboneType;
			}
			set {
				this.aboneType = value;
				this.RefleshView();
			}
		}
		private void cmsListView_Opening(object sender, CancelEventArgs e) {
			bool isSelected = (0 < this.lviewGenre.SelectedItems.Count);
			this.tsmiPlay.Enabled = isSelected;
			this.tsmiWMP.Enabled = isSelected;
			this.tsmiDetail.Enabled = isSelected;
			this.tsmiPlayerPage.Enabled = isSelected;
			this.tsmiPackage.Enabled = isSelected;
			this.tsmiNgPackage.Enabled = isSelected;
			this.tsmiProperty.Enabled = isSelected;
			this.tsmiCommands.Enabled = isSelected && 0 < this.tsmiCommands.DropDownItems.Count;
			
			this.tsmiSpecial.Enabled = false;
			foreach(ListViewItem item in this.lviewGenre.SelectedItems) {
				if((item.Tag as GContent).Package.HasSpecialPage) {
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
							contents.Add(lvi.Tag as GContent);
						}
						((sender as ToolStripMenuItem).Tag as UserCommand).Execute(contents);
					}));
				mi.Tag = uc;
				this.tsmiCommands.DropDownItems.Add(mi);
			}
			this.tsmiCommands.Enabled = 0 != this.tsmiCommands.DropDownItems.Count;
		}
	}
}
