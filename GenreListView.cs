using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace Yusen.GExplorer {
	partial class GenreListView : UserControl{
		[Flags]
		private enum ImageIndex : int{
			IsNew = 1,
			IsNotNew = 0,
			HasSpecial = 2,
			HasNotSpecial = 0,
		}
		
		public event EventHandler<GenreListViewGenreChangedEventArgs> GenreChanged;
		public event EventHandler<GenreListViewSelectedContentsChangedEventArgs> SelectedContentsChanged;
		public event ColumnWidthChangedEventHandler ColumnWidthChanged;
		public event EventHandler ViewChanged;
		public event EventHandler FullRowSelectChanged;
		public event EventHandler MultiSelectChanged;
		public event EventHandler AboneTypeChanged;
		
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
					this.SelectedContentsChanged(this, new GenreListViewSelectedContentsChangedEventArgs(contents));
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
			this.lviewGenre.ColumnWidthChanged += delegate(object sender, ColumnWidthChangedEventArgs e) {
				if(null != this.ColumnWidthChanged) {
					this.ColumnWidthChanged(this, e);
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

			if(null != this.GenreChanged) {
				this.GenreChanged(
					this,
					new GenreListViewGenreChangedEventArgs(genre, this.lviewGenre.Items.Count));
			}
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
		
		public AboneType AboneType {
			get {
				return this.aboneType;
			}
			set {
				if(value != this.aboneType) {
					this.aboneType = value;
					this.RefleshView();
					if(null != this.AboneTypeChanged) {
						this.AboneTypeChanged(this, EventArgs.Empty);
					}
				}
			}
		}
		[DefaultValue(View.Details)]
		public View View {
			get {
				return this.lviewGenre.View;
			}
			set {
				if(value != this.lviewGenre.View) {
					this.lviewGenre.View = value;
					if(null != this.ViewChanged) {
						this.ViewChanged(this, EventArgs.Empty);
					}
				}
			}
		}
		[DefaultValue(true)]
		public bool FullRowSelect {
			get { return this.lviewGenre.FullRowSelect; }
			set {
				if(value != this.lviewGenre.FullRowSelect) {
					this.lviewGenre.FullRowSelect = value;
					if(null != this.FullRowSelectChanged) {
						this.FullRowSelectChanged(this, EventArgs.Empty);
					}
				}
			}
		}
		[DefaultValue(false)]
		public bool MultiSelect {
			get { return this.lviewGenre.MultiSelect; }
			set {
				if(value != this.lviewGenre.MultiSelect) {
					this.lviewGenre.MultiSelect = value;
					if(null != this.MultiSelectChanged) {
						this.MultiSelectChanged(this, EventArgs.Empty);
					}
				}
			}
		}
		
		public int ColWidthId {
			get { return this.GetColWidth(0); }
			set { this.SetColWidthIfDifferent(0, value); }
		}
		public int ColWidthLimit {
			get { return this.GetColWidth(1); }
			set { this.SetColWidthIfDifferent(1, value); }
		}
		public int ColWidthEpisode {
			get { return this.GetColWidth(2); }
			set { this.SetColWidthIfDifferent(2, value); }
		}
		public int ColWidthLead {
			get { return this.GetColWidth(3); }
			set { this.SetColWidthIfDifferent(3, value); }
		}
		private int GetColWidth(int colIdx) {
			return this.lviewGenre.Columns[colIdx].Width;
		}
		private void SetColWidthIfDifferent(int colIdx, int width) {
			if(width != this.lviewGenre.Columns[colIdx].Width) {
				this.lviewGenre.Columns[colIdx].Width = width;
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
	
	class GenreListViewGenreChangedEventArgs : EventArgs {
		private readonly GGenre newGenre;
		private readonly int numberOfContents;
		public GenreListViewGenreChangedEventArgs(GGenre newGenre, int numberOfContents) {
			this.newGenre = newGenre;
			this.numberOfContents = numberOfContents;
		}
		public GGenre NewGenre {
			get { return this.newGenre; }
		}
		public int NumberOfContents {
			get { return this.numberOfContents; }
		}
	}
	class GenreListViewSelectedContentsChangedEventArgs : EventArgs {
		private readonly IEnumerable<GContent> selectedContents;
		public GenreListViewSelectedContentsChangedEventArgs(IEnumerable<GContent> selectedContents) {
			this.selectedContents = selectedContents;
		}
		public IEnumerable<GContent> SelectedContents {
			get { return this.selectedContents; }
		}
	}
}
