using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class MainForm : Form, IUsesUserSettings{
		public MainForm() {
			InitializeComponent();
			this.LoadFromUserSettings();
			Utility.AddHelpMenu(this.menuStrip1);
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = Application.ProductName + " " + Application.ProductVersion;
			this.tsslCategoryStat.Text = "";
			
			//メニュー項目
			this.tsmiQuit.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.Close();
			});
			this.tsmiUserSettings.Click += new EventHandler(delegate(object sender, EventArgs e){
				UserSettings.Instance.ShowUserSettingsForm();
			});
			this.tsmiEditCommands.Click += new EventHandler(delegate(object sender, EventArgs e) {
				UserCommandsManager.Instance.ShowEditor();
			});
			//ステータスバー
			this.genreViewer1.Refreshed += new GenreListViewRefleshedEventHandler(
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
			this.Resize += new EventHandler(this.SaveToUserSettings);
			UserSettings.Instance.ChangeCompleted +=
				new UserSettingsChangeCompletedEventHandler(this.LoadFromUserSettings);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					UserSettings.Instance.ChangeCompleted -= this.LoadFromUserSettings;
				});
		}

		public void LoadFromUserSettings() {
			UserSettings settings = UserSettings.Instance;
			this.StartPosition = settings.MfStartPosition;
			this.WindowState = settings.MfWindowState;
			this.Location = settings.MfLocation;
			this.Size = settings.MfSize;
		}
		private void SaveToUserSettings(object sender, EventArgs e) {
			this.SaveToUserSettings();
		}
		public void SaveToUserSettings() {
			UserSettings settings = UserSettings.Instance;
			settings.MfStartPosition = this.StartPosition;
			settings.MfWindowState = this.WindowState;
			settings.MfLocation = this.Location;
			settings.MfSize = this.Size;
			settings.OnChangeCompleted();
		}
	}
}

