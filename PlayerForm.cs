using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AxWMPLib;
using WMPLib;

namespace Yusen.GExplorer {
	partial class PlayerForm : Form, IUsesUserSettings{
		private static PlayerForm instance = null;
		public static PlayerForm Instance {
			get {
				if(null == PlayerForm.instance || ! PlayerForm.instance.CanFocus) {
					PlayerForm.instance = new PlayerForm();
				}
				return PlayerForm.instance;
			}
		}
		
		private GContent content = null;
		private PlayerForm() {
			InitializeComponent();
			this.Icon = Utility.GetGExplorerIcon();
			Utility.AppendHelpMenu(this.menuStrip1);
			//ユーザ設定の読み込み
			this.LoadSettings();
			//メニュー選択時の動作
			this.tsmiOpenGenre.Click += new EventHandler(delegate(object sender, EventArgs e) {
				MainForm.Instance.Genre = this.Content.Genre;
				MainForm.Instance.Focus();
			});
			this.tsmiProperty.Click += new EventHandler(delegate(object sender, EventArgs e) {
				ContentPropertyViewer cpv = ContentPropertyViewer.Instance;
				cpv.Show();
				cpv.Content = this.Content;
				cpv.Focus();
			});
			this.tsmiReload.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.wmpMain.URL = this.content.MediaFileUri.AbsoluteUri;
			});
			this.tsmiClose.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.Close();
			});
			this.tsmiAlwaysOnTop.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.TopMost = this.tsmiAlwaysOnTop.Checked;
				this.SaveSettings();
			});
			this.tsmiFullscreen.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.wmpMain.fullScreen = !this.wmpMain.fullScreen;
				this.SaveSettings();
			});
			this.tsmiAutoVolume.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.SaveSettings();
			});
			this.tsmiFocusOnWmp.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.tabControl1.SelectedIndex = 0;
				this.wmpMain.Focus();
			});
			//外部コマンド
			this.LoadCommands();
			UserCommandsManager.Instance.UserCommandsChanged += new UserCommandsChangedEventHandler(this.LoadCommands);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					UserCommandsManager.Instance.UserCommandsChanged -= new UserCommandsChangedEventHandler(this.LoadCommands);
				});
			//音量の自動調整
			this.wmpMain.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(
				delegate(object sender, _WMPOCXEvents_OpenStateChangeEvent e) {
					// THANKSTO: http://pc8.2ch.net/test/read.cgi/esite/1116115226/81 の神
					if(this.tsmiAutoVolume.Checked && WMPOpenState.wmposMediaOpen == this.wmpMain.openState) {
						this.wmpMain.settings.volume =
							this.wmpMain.currentMedia.getItemInfo("WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL").StartsWith("Adv:") ? 10 : 100;
					}
				});
			//ユーザ設定
			this.SizeChanged += new EventHandler(this.SaveToUserSettings);
			this.LocationChanged += new EventHandler(this.SaveToUserSettings);
			UserSettings.Instance.ChangeCompleted +=
				new UserSettingsChangeCompletedEventHandler(this.LoadSettings);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					UserSettings.Instance.ChangeCompleted -= this.LoadSettings;
				});
		}
		
		public GContent Content {
			get {
				if(null == this.content) throw new InvalidOperationException();
				return this.content;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				Utility.SetTitlebarText(this, value.Package.PackageName + (("" == value.ContentName) ? "" : " / " + value.ContentName));
				this.wmpMain.URL = value.MediaFileUri.AbsoluteUri;
				this.ieMain.Navigate(value.DetailPageUri);
				this.content = value;
			}
		}
		
		public void LoadSettings() {
			UserSettings settings = UserSettings.Instance;
			this.tsmiAutoVolume.Checked = settings.PlayerAutoVolume;
			this.tsmiAlwaysOnTop.Checked = settings.PlayerAlwaysOnTop;
			this.Size = settings.PlayerSize;
			this.Location = settings.PlayerLocation;
			this.StartPosition = settings.PlayerStartPosition;
			
			this.TopMost = this.tsmiAlwaysOnTop.Checked;
		}
		private void SaveToUserSettings(object sender, EventArgs e) {
			this.SaveSettings();
		}
		public void SaveSettings() {
			UserSettings settings = UserSettings.Instance;
			settings.PlayerAutoVolume = this.tsmiAutoVolume.Checked;
			settings.PlayerAlwaysOnTop = this.tsmiAlwaysOnTop.Checked;
			settings.PlayerSize = this.Size;
			settings.PlayerLocation = this.Location;
			settings.PlayerStartPosition = this.StartPosition;

			settings.OnChangeCompleted();
		}
		
		private void LoadCommands() {
			this.tsmiCommands.DropDownItems.Clear();
			foreach(UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem mi = new ToolStripMenuItem(
					uc.Title, null,
					new EventHandler(delegate(object sender, EventArgs e) {
					((UserCommand)((ToolStripMenuItem)sender).Tag).Execute(
						new GContent[] { this.Content });
				}));
				mi.Tag = uc;
				this.tsmiCommands.DropDownItems.Add(mi);
			}
			this.tsmiCommands.Enabled = 0 != this.tsmiCommands.DropDownItems.Count;
		}
	}
}
