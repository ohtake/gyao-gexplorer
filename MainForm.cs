using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class MainForm : Form, IUsesUserSettings{
		private static MainForm instance = null;
		public static MainForm Instance {
			get{
				if(null == MainForm.instance || !MainForm.instance.CanFocus) {
					MainForm.instance = new MainForm();
				}
				return MainForm.instance;
			}
		}
		
		public event GenreListViewSelectedContentsChangedEventHandler SelectedContentsChanged;
		
		private MainForm() {
			InitializeComponent();
			this.LoadSettings();
			Utility.AppendHelpMenu(this.menuStrip1);
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = Application.ProductName + " " + Application.ProductVersion;
			this.tsslCategoryStat.Text = "";
			
			//ジャンルタブ
			this.tabGenre.TabPages.Clear();
			foreach(GGenre g in GGenre.AllGenres) {
				TabPage tab = new TabPage(g.GenreName);
				tab.Tag = g;
				this.tabGenre.TabPages.Add(tab);
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
				Utility.BrowseWithIE(new Uri("http://www.gyao.jp/"));
			};
			this.tsmiQuit.Click += delegate {
				this.Close();
			};
			this.tsmiContentProperty.Click += delegate {
				ContentPropertyViewer.Instance.Show();
				ContentPropertyViewer.Instance.Focus();
			};
			this.tsmiEditCommands.Click += delegate{
				UserCommandsEditor.Instance.Show();
				UserCommandsEditor.Instance.Focus();
			};
			this.tsmiUserSettings.Click += delegate {
				UserSettingsToolbox.Instance.Show();
				UserSettingsToolbox.Instance.Focus();
			};
			this.tsmiNgPackageEditor.Click += delegate {
				NgPackagesEditor.Instance.Show();
				NgPackagesEditor.Instance.Focus();
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
					this.GenreListView.ListView.View = (View)(sender as ToolStripMenuItem).Tag;
					this.RefleshLvViewDropDownItems();
					this.SaveSettings();
				};
				this.tsmiLvView.DropDownItems.Add(mi);
			}
			this.tsmiFullRowSelect.Click += delegate {
				this.GenreListView.ListView.FullRowSelect = this.tsmiFullRowSelect.Checked;
				this.SaveSettings();
			};
			this.tsmiMultiSelect.Click += delegate {
				this.GenreListView.ListView.MultiSelect = this.tsmiMultiSelect.Checked;
				this.SaveSettings();
			};
			//ステータスバー
			this.glvMain.GenreChanged += new GenreListViewGenreChangedEventHandler(
				delegate(GenreListView sender, GGenre genre, int cntCount) {
					this.tsslCategoryStat.Text =
						"[" + genre.GenreName + "]"
						+ " " + cntCount.ToString() + " 個のオブジェクト"
						+ " (最終読み込み時刻 " + genre.LastFetchTime.ToShortTimeString() + ")";
					this.tspbPackages.Value = this.tspbPackages.Minimum;
				});
			GGenre.LoadingPackages += new LoadingPackagesEventHandler(
				delegate(GGenre sender, int nume, int denom) {
					this.tspbPackages.Maximum = denom;
					this.tspbPackages.Value = nume;
					this.tsslCategoryStat.Text = "[" + sender.GenreName + "] 読み込み中";
					Application.DoEvents(); //ステータスバーのラベルを描画させるために必要？
				});
			//コンテンツの選択
			this.glvMain.SelectedContentsChanged += delegate(GenreListView sender, IEnumerable<GContent> contents) {
				if(null != this.SelectedContentsChanged) {
					this.SelectedContentsChanged(sender, contents);
				}
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
				return (GGenre)this.tabGenre.SelectedTab.Tag;
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
		
		private void RefleshAboneTypeDropDownItems() {
			foreach(ToolStripMenuItem m in this.tsmiAboneType.DropDownItems) {
				m.Checked = (this.GenreListView.AboneType == (AboneType)m.Tag);
			}
		}
		
		private void RefleshLvViewDropDownItems() {
			foreach(ToolStripMenuItem m in this.tsmiLvView.DropDownItems) {
				m.Checked = (this.GenreListView.ListView.View == (View)m.Tag);
			}
		}
		public void LoadSettings() {
			UserSettings.Instance.MainForm.ApplySettings(this);
			this.RefleshLvViewDropDownItems();
			this.RefleshAboneTypeDropDownItems();
			this.tsmiMultiSelect.Checked = this.GenreListView.ListView.MultiSelect;
			this.tsmiFullRowSelect.Checked = this.GenreListView.ListView.FullRowSelect;
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

