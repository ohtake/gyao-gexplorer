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
		private ActionOnMediaEnded actionOnEnd = ActionOnMediaEnded.CloseForm;
		
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
				ContentPropertyViewer cpv = ContentPropertyViewer.Instance;
				cpv.Show();
				cpv.Content = this.Content;
				cpv.Focus();
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
			this.tsmiActionOnMediaEnded.Click += delegate {
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
			this.LoadCommands();
			UserCommandsManager.Instance.UserCommandsChanged += new UserCommandsChangedEventHandler(this.LoadCommands);
			this.FormClosing += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new UserCommandsChangedEventHandler(this.LoadCommands);
			};
			//音量の自動調整
			this.wmpMain.OpenStateChange += delegate{
				// THANKSTO: http://pc8.2ch.net/test/read.cgi/esite/1116115226/81 の神
				if(this.tsmiAutoVolume.Checked && WMPOpenState.wmposMediaOpen == this.wmpMain.openState) {
					bool isCf = this.wmpMain.currentMedia.getItemInfo("WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL").StartsWith("Adv:");
					this.wmpMain.settings.volume = isCf ? 20 : 100;
					//謎の対応
					this.wmpMain.settings.mute = true;
					this.wmpMain.settings.mute = false;
				}
			};
			//再生が終了した時の動作
			foreach(ActionOnMediaEnded action in Enum.GetValues(typeof(ActionOnMediaEnded))){
				ToolStripMenuItem mi = new ToolStripMenuItem(action.ToString());
				mi.Tag = action;
				mi.Click += new EventHandler(delegate(object sender, EventArgs e) {
					this.ActionOnEnd = (ActionOnMediaEnded)(sender as ToolStripMenuItem).Tag;
					this.SaveSettings();
				});
				this.tsmiActionOnMediaEnded.DropDownItems.Add(mi);
			}
			this.ActionOnEnd = this.ActionOnEnd;//チェックをつけるため
			this.wmpMain.PlayStateChange += delegate {
				if(WMPPlayState.wmppsMediaEnded == this.wmpMain.playState) {
					switch(this.ActionOnEnd) {
						case ActionOnMediaEnded.DoNothing:
							break;
						case ActionOnMediaEnded.CloseForm:
							this.Close();
							break;
						case ActionOnMediaEnded.GoToCampaign:
							this.ieMain.Navigated += new WebBrowserNavigatedEventHandler(this.NavigateToCampaign);
							this.ieMain.Navigate(this.Content.PlayerPageUri);
							break;
						case ActionOnMediaEnded.RepeatPlaying:
							this.tsmiReload.PerformClick();
							break;
						default:
							throw new Exception("この設定での終了時の動作は定義されてないよ．");
					}
				}
			};
			//メディアキー
			this.KeyDown += new KeyEventHandler(delegate(object sender, KeyEventArgs e) {
				if(e.Handled) return;
				if(!this.tsmiMediaKeys.Checked) return;
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
		
		private void NavigateToCampaign(object sender, WebBrowserNavigatedEventArgs e) {
			this.ieMain.Navigated -= this.NavigateToCampaign;
			this.ieMain.Navigate("http://www.gyao.jp/sityou_campaign/top/");
			this.tabControl1.SelectedTab = this.tabBrowser;
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
		public ActionOnMediaEnded ActionOnEnd {
			get {
				return this.actionOnEnd;
			}
			set {
				this.actionOnEnd = value;
				foreach(ToolStripMenuItem mi in this.tsmiActionOnMediaEnded.DropDownItems) {
					mi.Checked = (value == (ActionOnMediaEnded)mi.Tag);
				}
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
		
		private void LoadCommands() {
			this.tsmiCommands.DropDownItems.Clear();
			foreach(UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem mi = new ToolStripMenuItem(
					uc.Title, null,
					new EventHandler(delegate(object sender, EventArgs e) {
					((sender as ToolStripMenuItem).Tag as UserCommand).Execute(
						new GContent[] { this.Content });
				}));
				mi.Tag = uc;
				this.tsmiCommands.DropDownItems.Add(mi);
			}
			this.tsmiCommands.Enabled = (0 != this.tsmiCommands.DropDownItems.Count);
		}
	}
}
