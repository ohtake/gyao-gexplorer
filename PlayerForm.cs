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
				if(null == PlayerForm.instance || PlayerForm.instance.IsDisposed) {
					PlayerForm.instance = new PlayerForm();
				}
				return PlayerForm.instance;
			}
		}
		public static void Play(GContent content) {
			PlayerForm.Instance.Show();
			PlayerForm.Instance.Focus();
			PlayerForm.Instance.Content = content;
		}
		
		private GContent content = null;
		
		private PlayerForm() {
			InitializeComponent();
			this.Icon = Utility.GetGExplorerIcon();
			Utility.AppendHelpMenu(this.menuStrip1);
			this.LoadSettings();
			//メニュー選択時の動作
			this.tsmiOpenGenre.Click += delegate{
				MainForm.Instance.Genre = this.Content.Genre;
				MainForm.Instance.Focus();
			};
			this.tsmiProperty.Click +=delegate {
				ContentPropertyViewer.View(this.Content);
			};
			this.tsmiReload.Click += delegate {
				this.wmpMain.URL = this.content.MediaFileUri.AbsoluteUri;
			};
			this.tsmiClose.Click += delegate {
				this.Close();
			};
			this.tsmiPlayPause.Click += delegate {
				if(WMPPlayState.wmppsPlaying == this.wmpMain.playState) {
					this.wmpMain.Ctlcontrols.pause();
				} else {
					this.wmpMain.Ctlcontrols.play();
				}
			};
			this.tsmiStop.Click += delegate {
				this.wmpMain.Ctlcontrols.stop();
			};
			this.tsmiPrevTrack.Click += delegate {
				this.wmpMain.Ctlcontrols.previous();
			};
			this.tsmiNextTrack.Click += delegate {
				this.wmpMain.Ctlcontrols.next();
			};
			this.tsmiFastForward.Click += delegate {
				this.wmpMain.Ctlcontrols.fastForward();
			};
			this.tsmiFastReverse.Click += delegate {
				this.wmpMain.Ctlcontrols.fastReverse();
			};
			this.tsmiAlwaysOnTop.Click += delegate {
				this.TopMost = this.tsmiAlwaysOnTop.Checked;
				this.SaveSettings();
			};
			this.tsmiFullscreen.Click += delegate{
				switch(this.wmpMain.playState) {
					case WMPPlayState.wmppsPlaying:
					case WMPPlayState.wmppsPaused:
						this.wmpMain.fullScreen = !this.wmpMain.fullScreen;
						break;
				}
			};
			this.tsmiAutoVolume.Click +=delegate {
				this.SaveSettings();
			};
			this.tsmiCloseOnEnd.Click += delegate {
				this.SaveSettings();
			};
			this.tsmiMediaKeys.Click += delegate {
				this.SaveSettings();
			};
			this.tsmiFocusOnWmp.Click += delegate {
				this.tabControl1.SelectedTab = this.tabPlayer;
				this.wmpMain.Focus();
			};
			//外部コマンド
			this.UserCommandsManager_UserCommandsChanged(null, EventArgs.Empty);
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			this.FormClosing += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			};
			//音量の自動調整
			this.wmpMain.OpenStateChange += delegate{
				// THANKSTO: http://pc8.2ch.net/test/read.cgi/esite/1116115226/81 の神
				if(this.tsmiAutoVolume.Checked && WMPOpenState.wmposMediaOpen == this.wmpMain.openState) {
					bool isCf = this.wmpMain.currentMedia.getItemInfo("WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL").StartsWith("Adv:");
					this.wmpMain.settings.volume = isCf ? 20 : 100;
					//謎の対応その3
					System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(delegate {
						System.Threading.Thread.Sleep(100);
						this.wmpMain.settings.volume = isCf ? 19 :  99;
						this.wmpMain.settings.volume = isCf ? 20 : 100;
					}));
					t.Start();
				}
			};
			//再生が終了した時の動作
			this.wmpMain.PlayStateChange += delegate {
				if(WMPPlayState.wmppsMediaEnded == this.wmpMain.playState) {
					if(this.CloseOnEnd) {
						this.Close();
					}
				}
			};
			//メディアキー
			this.KeyDown += new KeyEventHandler(delegate(object sender, KeyEventArgs e) {
				if(e.Handled) return;
				if(!this.tsmiMediaKeys.Checked) return;
				if(this.wmpMain.Focused) return;
				switch(e.KeyCode) {
					case Keys.MediaNextTrack:
						this.tsmiNextTrack.PerformClick();
						break;
					case Keys.MediaPlayPause:
						this.tsmiPlayPause.PerformClick();
						break;
					case Keys.MediaPreviousTrack:
						this.tsmiPrevTrack.PerformClick();
						break;
					case Keys.MediaStop:
						this.tsmiStop.PerformClick();
						break;
					default:
						return;
				}
				e.Handled = true;
			});
			//ユーザ設定
			this.SizeChanged += delegate {
				this.SaveSettings();
			};
			this.LocationChanged += delegate {
				this.SaveSettings();
			};
			UserSettings.Instance.PlayerForm.ChangeCompleted += this.LoadSettings;
			this.FormClosing += delegate{
				if(FormWindowState.Minimized == this.WindowState) {
					//最小化したまま終了されるとウィンドウ位置が変になるので元に戻す
					this.WindowState = FormWindowState.Normal;
				}
				UserSettings.Instance.PlayerForm.ChangeCompleted -= this.LoadSettings;
			};
		}
		
		public GContent Content {
			get {
				if(null == this.content) throw new InvalidOperationException();
				return this.content;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				if(null == value.Package) {
					Utility.SetTitlebarText(this, "<" + value.ContentId + ">");
				}else{
					Utility.SetTitlebarText(this, value.Package.PackageName + (("" == value.ContentName) ? "" : " / " + value.ContentName));
				}
				this.wmpMain.URL = value.MediaFileUri.AbsoluteUri;
				this.ieMain.Navigate(value.DetailPageUri);
				this.content = value;
			}
		}
		public bool AutoVolumeEnabled {
			get {
				return this.tsmiAutoVolume.Checked;
			}
			set {
				this.tsmiAutoVolume.Checked = value;
			}
		}
		public bool MediaKeysEnabled {
			get {
				return this.tsmiMediaKeys.Checked;
			}
			set {
				this.tsmiMediaKeys.Checked = value;
			}
		}
		public bool CloseOnEnd {
			get {
				return this.tsmiCloseOnEnd.Checked;
			}
			set {
				this.tsmiCloseOnEnd.Checked = value;
			}
		}
		public void LoadSettings() {
			UserSettings.Instance.PlayerForm.ApplySettings(this);
			this.tsmiAlwaysOnTop.Checked = this.TopMost;
		}
		public void SaveSettings() {
			UserSettings.Instance.PlayerForm.StoreSettings(this);
			UserSettings.Instance.PlayerForm.OnChangeCompleted();
		}

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.tsmiCommands.DropDownItems.Clear();
			foreach(UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem mi = new ToolStripMenuItem(
					uc.Title, null,
					new EventHandler(delegate(object sender2, EventArgs e2) {
					((sender2 as ToolStripMenuItem).Tag as UserCommand).Execute(
						new GContent[] { this.Content });
				}));
				mi.Tag = uc;
				this.tsmiCommands.DropDownItems.Add(mi);
			}
			this.tsmiCommands.Enabled = (0 != this.tsmiCommands.DropDownItems.Count);
		}
	}
}
