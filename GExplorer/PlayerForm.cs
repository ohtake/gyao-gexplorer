using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AxWMPLib;
using WMPLib;
using System.ComponentModel;

namespace Yusen.GExplorer {
	partial class PlayerForm : FormSettingsBase, IFormWithSettings<PlayerFormSettings>{
		private static PlayerForm instance = null;
		public static PlayerForm Instance {
			get {
				if(null == PlayerForm.instance || PlayerForm.instance.IsDisposed) {
					PlayerForm.instance = new PlayerForm();
				}
				return PlayerForm.instance;
			}
		}
		public static void Play(ContentAdapter content) {
			PlayerForm.Instance.Show();
			PlayerForm.Instance.Focus();
			PlayerForm.Instance.Content = content;
		}

		private ContentAdapter content = null;
		
		private PlayerForm() {
			InitializeComponent();
			Utility.AppendHelpMenu(this.menuStrip1);
			
			this.CreateUserCommandsMenuItems();
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
			this.FormClosing += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			};

			Utility.LoadSettingsAndEnableSaveOnClosed(this);
		}
		private void CreateUserCommandsMenuItems() {
			this.tsmiUserCommands.DropDownItems.Clear();
			foreach (UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(uc.Title);
				tsmi.Tag = uc;
				tsmi.Click += delegate(object sender, EventArgs e) {
					ToolStripMenuItem tsmi2 = sender as ToolStripMenuItem;
					if (null != tsmi2) {
						UserCommand uc2 = tsmi2.Tag as UserCommand;
						if (null != uc2) {
							uc2.Execute(this.Content);
						}
					}
				};
				this.tsmiUserCommands.DropDownItems.Add(tsmi);
			}
			this.tsmiUserCommands.Enabled = (this.tsmiUserCommands.DropDownItems.Count > 0);
		}
		public void FillSettings(PlayerFormSettings settings) {
			base.FillSettings(settings);
			settings.AutoVolumeEnabled = this.AutoVolumeEnabled;
			settings.MediaKeysEnabled = this.MediaKeysEnabled;
			settings.CloseOnEnd = this.CloseOnEnd;
		}

		public void ApplySettings(PlayerFormSettings settings) {
			base.ApplySettings(settings);
			this.AutoVolumeEnabled = settings.AutoVolumeEnabled ?? this.AutoVolumeEnabled;
			this.MediaKeysEnabled = settings.MediaKeysEnabled ?? this.MediaKeysEnabled;
			this.CloseOnEnd = settings.CloseOnEnd ?? this.CloseOnEnd;

			this.tsmiAlwaysOnTop.Checked = this.TopMost;
		}

		public string FilenameForSettings {
			get { return @"PlayerFormSettings.xml"; }
		}

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateUserCommandsMenuItems();
		}

		public ContentAdapter Content {
			get {
				return this.content;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				Utility.SetTitlebarText(this, value.DisplayName);
				this.wmpMain.URL = value.MediaFileUri.AbsoluteUri;
				this.ieMain.Navigate(value.DetailPageUri);
				this.content = value;
			}
		}
		public bool AutoVolumeEnabled {
			get {return this.tsmiAutoVolume.Checked;}
			set {this.tsmiAutoVolume.Checked = value;}
		}
		public bool MediaKeysEnabled {
			get {return this.tsmiMediaKeys.Checked;}
			set {this.tsmiMediaKeys.Checked = value;}
		}
		public bool CloseOnEnd {
			get {return this.tsmiCloseOnEnd.Checked;}
			set {this.tsmiCloseOnEnd.Checked = value;}
		}

		private void tsmiReload_Click(object sender, EventArgs e) {
			this.wmpMain.URL = this.content.MediaFileUri.AbsoluteUri;
		}
		private void tsmiClose_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void tsmiPlayPause_Click(object sender, EventArgs e) {
			if (WMPPlayState.wmppsPlaying == this.wmpMain.playState) {
				this.wmpMain.Ctlcontrols.pause();
			} else {
				this.wmpMain.Ctlcontrols.play();
			}
		}
		private void tsmiStop_Click(object sender, EventArgs e) {
			this.wmpMain.Ctlcontrols.stop();
		}
		private void tsmiPrevTrack_Click(object sender, EventArgs e) {
			this.wmpMain.Ctlcontrols.previous();
		}
		private void tsmiNextTrack_Click(object sender, EventArgs e) {
			this.wmpMain.Ctlcontrols.next();
		}
		private void tsmiFastReverse_Click(object sender, EventArgs e) {
			this.wmpMain.Ctlcontrols.fastForward();
		}
		private void tsmiFastForward_Click(object sender, EventArgs e) {
			this.wmpMain.Ctlcontrols.fastReverse();
		}
		private void tsmiAlwaysOnTop_Click(object sender, EventArgs e) {
			this.TopMost = this.tsmiAlwaysOnTop.Checked;
		}
		private void tsmiFullscreen_Click(object sender, EventArgs e) {
			switch (this.wmpMain.playState) {
				case WMPPlayState.wmppsPlaying:
				case WMPPlayState.wmppsPaused:
					this.wmpMain.fullScreen = !this.wmpMain.fullScreen;
					break;
			}
		}
		private void tsmiAutoVolume_Click(object sender, EventArgs e) {
		}
		private void tsmiMediaKeys_Click(object sender, EventArgs e) {
		}
		private void tsmiCloseOnEnd_Click(object sender, EventArgs e) {
		}
		private void tsmiFocusOnWmp_Click(object sender, EventArgs e) {
			this.tabControl1.SelectedTab = this.tabPlayer;
			this.wmpMain.Focus();
		}
		
		private void wmpMain_OpenStateChange(object sender, _WMPOCXEvents_OpenStateChangeEvent e) {
			// THANKSTO: http://pc8.2ch.net/test/read.cgi/esite/1116115226/81 ÇÃê_
			if (this.tsmiAutoVolume.Checked && WMPOpenState.wmposMediaOpen == this.wmpMain.openState) {
				bool isCf = this.wmpMain.currentMedia.getItemInfo("WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL").StartsWith("Adv:");
				this.wmpMain.settings.volume = isCf ? 20 : 100;
				//ì‰ÇÃëŒâûÇªÇÃ3
				System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(delegate {
					System.Threading.Thread.Sleep(100);
					this.wmpMain.settings.volume = isCf ? 19 :  99;
					this.wmpMain.settings.volume = isCf ? 20 : 100;
				}));
				t.Start();
			}
		}
		private void wmpMain_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e) {
			if (WMPPlayState.wmppsMediaEnded == this.wmpMain.playState) {
				if (this.CloseOnEnd) {
					this.Close();
				}
			}
		}

		private void PlayerForm_KeyDown(object sender, KeyEventArgs e) {
			if (e.Handled) return;
			if (!this.tsmiMediaKeys.Checked) return;
			if (this.wmpMain.Focused) return;
			switch (e.KeyCode) {
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
		}
	}
	public class PlayerFormSettings : FormSettingsBaseSettings {
		private bool? autoVolumeEnabled;
		private bool? mediaKeysEnabled;
		private bool? closeOnEnd;
		
		public bool? AutoVolumeEnabled {
			get { return this.autoVolumeEnabled; }
			set { this.autoVolumeEnabled = value; }
		}
		public bool? MediaKeysEnabled {
			get { return this.mediaKeysEnabled; }
			set { this.mediaKeysEnabled = value; }
		}
		public bool? CloseOnEnd {
			get { return this.closeOnEnd; }
			set { this.closeOnEnd = value; }
		}
	}
}
