using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class MainForm : Form, IUsesUserSettings {
		private static MainForm instance = null;
		public static MainForm Instance {
			get{
				if(null == MainForm.instance || MainForm.instance.IsDisposed) {
					MainForm.instance = new MainForm();
				}
				return MainForm.instance;
			}
		}
		
		public event EventHandler<GenreListViewSelectedContentsChangedEventArgs> SelectedContentsChanged;
		
		private MainForm() {
			InitializeComponent();
			this.LoadSettings();
			Utility.AppendHelpMenu(this.menuStrip1);
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = Application.ProductName + " " + Application.ProductVersion;
			this.tsslCategoryStat.Text = "";
			
			//ジャンルの設定
			this.tsmiUneploeableGenres.DropDownItems.Clear();
			this.tabGenre.TabPages.Clear();
			foreach(GGenre g in GGenre.AllGenres) {
				if(g.CanBeExplorerable) {
					TabPage tab = new TabPage(g.GenreName);
					tab.Tag = g;
					this.tabGenre.TabPages.Add(tab);
				} else {
					ToolStripMenuItem mi = new ToolStripMenuItem(g.GenreName);
					mi.Tag = g;
					mi.Click += new EventHandler(delegate(object sender, EventArgs e) {
						GGenre genre = (sender as ToolStripMenuItem).Tag as GGenre;
						BrowserForm.Browse(genre.GenreTopPageUri);
					});
					this.tsmiUneploeableGenres.DropDownItems.Add(mi);
				}
			}
			this.tabGenre.SelectedIndex = -1;
			this.tabGenre.SelectedIndexChanged += delegate {
				GGenre genre = (GGenre)this.tabGenre.SelectedTab.Tag;
				if(!genre.HasLoaded) genre.FetchAll();
				this.glvMain.Genre = genre;
			};
			this.tabGenre.DoubleClick += delegate {
				GGenre genre = (GGenre)this.tabGenre.SelectedTab.Tag;
				genre.FetchAll();
				this.glvMain.Genre = genre;
			};
			
			//メニュー項目
			this.tsmiGyaoTop.Click += delegate {
				BrowserForm.Browse(new Uri("http://www.gyao.jp/"));
			};
			this.tsmiQuit.Click += delegate {
				this.Close();
			};
			//メニュー項目 (リストビュー)
			foreach(AboneType at in Enum.GetValues(typeof(AboneType))) {
				ToolStripMenuItem mi = new ToolStripMenuItem(at.ToString());
				mi.Tag = at;
				mi.Click += delegate(object sender, EventArgs e) {
					this.GenreListView.AboneType = (AboneType)(sender as ToolStripMenuItem).Tag;
					this.RefleshAboneTypeDropDownItems();
					this.SaveSettings();
				};
				this.tsmiAboneType.DropDownItems.Add(mi);
			}
			foreach(View v in Enum.GetValues(typeof(View))){
				ToolStripMenuItem mi = new ToolStripMenuItem(v.ToString());
				mi.Tag = v;
				mi.Click += delegate(object sender, EventArgs e) {
					this.GenreListView.View = (View)(sender as ToolStripMenuItem).Tag;
					this.RefleshLvViewDropDownItems();
					this.SaveSettings();
				};
				this.tsmiLvView.DropDownItems.Add(mi);
			}
			this.tsmiFullRowSelect.Click += delegate {
				this.GenreListView.FullRowSelect = this.tsmiFullRowSelect.Checked;
				this.SaveSettings();
			};
			this.tsmiMultiSelect.Click += delegate {
				this.GenreListView.MultiSelect = this.tsmiMultiSelect.Checked;
				this.SaveSettings();
			};
			//メニュー項目 (ウィンドウ)
			this.tsmiPlayer.Click += delegate {
				this.ShowAndFocus(PlayerForm.Instance);
			};
			this.tsmiBrowser.Click += delegate {
				this.ShowAndFocus(BrowserForm.Instance);
			};
			this.tsmiContentProperty.Click += delegate {
				this.ShowAndFocus(ContentPropertyViewer.Instance);
			};
			this.tsmiEditCommands.Click += delegate {
				this.ShowAndFocus(UserCommandsEditor.Instance);
			};
			this.tsmiUserSettings.Click += delegate {
				this.ShowAndFocus(UserSettingsToolbox.Instance);
			};
			this.tsmiNgPackageEditor.Click += delegate {
				this.ShowAndFocus(NgPackagesEditor.Instance);
			};
			//ステータスバー
			this.glvMain.GenreChanged += delegate(object sender, GenreListViewGenreChangedEventArgs e) {
					this.tsslCategoryStat.Text =
						"[" + e.NewGenre.GenreName + "]"
						+ " " + e.NumberOfContents.ToString() + " 個のオブジェクト"
						+ " (最終読み込み時刻 " + e.NewGenre.LastFetchTime.ToShortTimeString() + ")";
					this.tspbPackages.Value = this.tspbPackages.Minimum;
				};
			GGenre.LoadingPackages += new EventHandler<LoadingPackagesEventArgs>(
				delegate(object sender, LoadingPackagesEventArgs e) {
					this.tspbPackages.Maximum = e.Denominator;
					this.tspbPackages.Value = e.Numerator;
					string statusText = "[" + (sender as GGenre).GenreName + "] 読み込み中";
					if (null != e.LoadedPackage) {
						statusText += "(読み込んだパッケージ: <" + e.LoadedPackage.PackageId + ">" + e.LoadedPackage.PackageName + ")";
					}
					this.tsslCategoryStat.Text = statusText;
					Application.DoEvents(); //ステータスバーのラベルを描画させるために必要？
				});
			//コンテンツの選択
			this.glvMain.SelectedContentsChanged += delegate(object sender, GenreListViewSelectedContentsChangedEventArgs e) {
				if(null != this.SelectedContentsChanged) {
					this.SelectedContentsChanged(sender, e);
				}
			};
			//NG更新
			NgPackagesManager.Instance.NgPackagesChanged += new EventHandler(this.NgPackagesManager_NgPackagesChanged);
			this.FormClosing += delegate {
				NgPackagesManager.Instance.NgPackagesChanged -= new EventHandler(this.NgPackagesManager_NgPackagesChanged);
			};
			//ユーザ設定
			this.LocationChanged += delegate {
				this.SaveSettings();
			};
			this.SizeChanged += delegate {
				this.SaveSettings();
			};
			this.glvMain.ColumnWidthChanged += delegate {
				this.SaveSettings();
			};
			UserSettings.Instance.MainForm.ChangeCompleted +=
				new UserSettingsChangeCompletedEventHandler(this.LoadSettings);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					if(FormWindowState.Minimized == this.WindowState) {
						//最小化したまま終了されるとウィンドウ位置が変になるので元に戻す
						this.WindowState = FormWindowState.Normal;
					}
					UserSettings.Instance.MainForm.ChangeCompleted -= this.LoadSettings;
				});
		}
		public GGenre Genre {
			get {
				if(null == this.tabGenre.SelectedTab) return null;
				return this.tabGenre.SelectedTab.Tag as GGenre;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				foreach(TabPage tp in this.tabGenre.TabPages) {
					if(value == (GGenre)tp.Tag) {
						this.tabGenre.SelectedTab = tp;
						break;
					}
				}
			}
		}
		private void ShowAndFocus(Form form) {
			form.Show();
			form.Focus();
		}
		private void NgPackagesManager_NgPackagesChanged(object sender, EventArgs e) {
			this.GenreListView.RefleshView();
		}
		
		private void RefleshAboneTypeDropDownItems() {
			foreach(ToolStripMenuItem m in this.tsmiAboneType.DropDownItems) {
				m.Checked = (this.GenreListView.AboneType == (AboneType)m.Tag);
			}
		}
		private void RefleshLvViewDropDownItems() {
			foreach(ToolStripMenuItem m in this.tsmiLvView.DropDownItems) {
				m.Checked = (this.GenreListView.View == (View)m.Tag);
			}
		}
		public void LoadSettings() {
			UserSettings.Instance.MainForm.ApplySettings(this);
			this.RefleshLvViewDropDownItems();
			this.RefleshAboneTypeDropDownItems();
			this.tsmiMultiSelect.Checked = this.GenreListView.MultiSelect;
			this.tsmiFullRowSelect.Checked = this.GenreListView.FullRowSelect;
		}
		public void SaveSettings() {
			UserSettings.Instance.MainForm.StoreSettings(this);
			UserSettings.Instance.MainForm.OnChangeCompleted();
		}
		
		public GenreListView GenreListView {
			get {
				return this.glvMain;
			}
		}
	}
}

