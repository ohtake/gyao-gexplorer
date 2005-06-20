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
			this.tabGenre.SelectedIndexChanged += new EventHandler(delegate(object sender, EventArgs e) {
				GGenre genre = (GGenre)this.tabGenre.SelectedTab.Tag;
				if(!genre.HasLoaded) genre.FetchAll();
				this.glvMain.Display(genre);
			});
			this.tabGenre.DoubleClick += new EventHandler(delegate(object sender, EventArgs e) {
				GGenre genre = (GGenre)this.tabGenre.SelectedTab.Tag;
				genre.FetchAll();
				this.glvMain.Display(genre);
			});
			
			//メニュー項目
			this.tsmiQuit.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.Close();
			});
			this.tsmiContentProperty.Click += new EventHandler(delegate(object sender, EventArgs e) {
				ContentPropertyViewer.Instance.Show();
				ContentPropertyViewer.Instance.Focus();
			});
			this.tsmiEditCommands.Click += new EventHandler(delegate(object sender, EventArgs e) {
				UserCommandsEditor.Instance.Show();
				UserCommandsEditor.Instance.Focus();
			});
			this.tsmiUserSettings.Click += new EventHandler(delegate(object sender, EventArgs e) {
				UserSettingsToolbox.Instance.Show();
				UserSettingsToolbox.Instance.Focus();
			});
			//ステータスバー
			this.glvMain.Refreshed += new GenreListViewRefleshedEventHandler(
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
			//ユーザ設定
			this.LocationChanged += new EventHandler(this.SaveToUserSettings);
			this.SizeChanged += new EventHandler(this.SaveToUserSettings);
			UserSettings.Instance.ChangeCompleted +=
				new UserSettingsChangeCompletedEventHandler(this.LoadSettings);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					UserSettings.Instance.ChangeCompleted -= this.LoadSettings;
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
		
		public void LoadSettings() {
			UserSettings settings = UserSettings.Instance;
			this.StartPosition = settings.MfStartPosition;
			this.WindowState = settings.MfWindowState;
			this.Location = settings.MfLocation;
			this.Size = settings.MfSize;
		}
		private void SaveToUserSettings(object sender, EventArgs e) {
			this.SaveSettings();
		}
		public void SaveSettings() {
			UserSettings settings = UserSettings.Instance;
			settings.MfStartPosition = this.StartPosition;
			settings.MfWindowState = this.WindowState;
			settings.MfLocation = this.Location;
			settings.MfSize = this.Size;
			settings.OnChangeCompleted();
		}
	}
}

