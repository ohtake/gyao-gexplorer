using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using AxWMPLib;
using WMPLib;

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
		public static void Play(ContentAdapter contentAdapter) {
			PlayerForm p = PlayerForm.Instance;
			p.CurrentContent = contentAdapter;
			p.Show();
			p.Focus();
		}

		private ContentAdapter currentContent = null;
		
		private PlayerForm() {
			InitializeComponent();
		}

		private void CreateUserCommandsMenuItems() {
			//メインメニュー
			this.tsmiUserCommands.DropDownItems.Clear();
			foreach (UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(uc.Title);
				tsmi.Tag = uc;
				tsmi.Click += delegate(object sender, EventArgs e) {
					ToolStripMenuItem tsmi2 = sender as ToolStripMenuItem;
					if (null != tsmi2) {
						UserCommand uc2 = tsmi2.Tag as UserCommand;
						if (null != uc2) {
							uc2.Execute(new ContentAdapter[] { this.CurrentContent });
						}
					}
				};
				this.tsmiUserCommands.DropDownItems.Add(tsmi);
			}
			this.tsmiUserCommands.Enabled = this.tsmiUserCommands.HasDropDownItems;
		}
		public void FillSettings(PlayerFormSettings settings) {
			base.FillSettings(settings);
			settings.AutoVolumeEnabled = this.AutoVolumeEnabled;
			settings.MediaKeysEnabled = this.MediaKeysEnabled;
			settings.RemovePlayedContentEnabled = this.RemovePlayedContentEnabled;
			settings.MainTabIndex = this.tabControl1.SelectedIndex;
		}

		public void ApplySettings(PlayerFormSettings settings) {
			base.ApplySettings(settings);
			this.AutoVolumeEnabled = settings.AutoVolumeEnabled ?? this.AutoVolumeEnabled;
			this.MediaKeysEnabled = settings.MediaKeysEnabled ?? this.MediaKeysEnabled;
			this.RemovePlayedContentEnabled = settings.RemovePlayedContentEnabled ?? this.RemovePlayedContentEnabled;
			this.tabControl1.SelectedIndex = settings.MainTabIndex ?? this.tabControl1.SelectedIndex;
			
			this.tsmiAlwaysOnTop.Checked = this.TopMost;
		}

		public string FilenameForSettings {
			get { return @"PlayerFormSettings.xml"; }
		}

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateUserCommandsMenuItems();
		}

		public ContentAdapter CurrentContent {
			get {
				return this.currentContent;
			}
			private set {
				this.currentContent = value;
				if (null == value) {
					Utility.SetTitlebarText(this, "PlayerForm");
					this.wmpMain.close();
					this.ieMain.Url = new Uri("about:blank");
				} else {
					Utility.SetTitlebarText(this, value.DisplayName);
					this.wmpMain.URL = value.MediaFileUri.AbsoluteUri;
					this.ieMain.Navigate(value.DetailPageUri);
				}
				PlayList.Instance.CurrentContent = value;
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
		public bool RemovePlayedContentEnabled {
			get { return this.tsmiRemovePlayedContent.Checked; }
			set { this.tsmiRemovePlayedContent.Checked = value; }
		}

		private void PlayerForm_Load(object sender, EventArgs e) {
			Utility.AppendHelpMenu(this.menuStrip1);
			
			this.CreateUserCommandsMenuItems();
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
			this.FormClosing += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			};
			
			Utility.LoadSettingsAndEnableSaveOnClosed(this);
		}
		private void PlayerForm_FormClosing(object sender, FormClosingEventArgs e) {
			this.CurrentContent = null;
		}
		private void tsmiReload_Click(object sender, EventArgs e) {
			this.wmpMain.URL = this.currentContent.MediaFileUri.AbsoluteUri;
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
		private void tsmiNextContent_Click(object sender, EventArgs e) {
			ContentAdapter cont = PlayList.Instance.NextContentOf(this.CurrentContent);
			this.CurrentContent = cont;
		}
		private void tsmiNextContentWithDelete_Click(object sender, EventArgs e) {
			ContentAdapter cont = PlayList.Instance.NextContentOf(this.CurrentContent);
			PlayList.Instance.Remove(this.CurrentContent);
			this.CurrentContent = cont;
		}
		private void tsmiPrevContent_Click(object sender, EventArgs e) {
			ContentAdapter cont = PlayList.Instance.PrevContentOf(this.CurrentContent);
			this.CurrentContent = cont;
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
		private void tsmiFocusOnWmp_Click(object sender, EventArgs e) {
			this.tabControl1.SelectedTab = this.tabPlayer;
			this.wmpMain.Focus();
		}
		
		private void wmpMain_OpenStateChange(object sender, _WMPOCXEvents_OpenStateChangeEvent e) {
			// THANKSTO: http://pc8.2ch.net/test/read.cgi/esite/1116115226/81 の神
			if (this.tsmiAutoVolume.Checked && WMPOpenState.wmposMediaOpen == this.wmpMain.openState) {
				bool isCf = this.wmpMain.currentMedia.getItemInfo("WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL").StartsWith("Adv:");
				this.wmpMain.settings.volume = isCf ? 20 : 100;
				//謎の対応その3
				Thread t = new Thread(new ThreadStart(delegate {
					Thread.Sleep(100);
					if (!this.IsDisposed && !this.wmpMain.IsDisposed) {
						this.wmpMain.settings.volume = isCf ? 19 :  99;
						this.wmpMain.settings.volume = isCf ? 20 : 100;
					}
				}));
				t.Start();
			}
		}
		private void wmpMain_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e) {
			if (WMPPlayState.wmppsMediaEnded == this.wmpMain.playState) {
				if (this.RemovePlayedContentEnabled) {
					this.tsmiNextContentWithDelete.PerformClick();
				} else {
					this.tsmiNextContent.PerformClick();
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
		private bool? removePlayedContentEnabled;
		private int? mainTabIndex;
		
		public bool? AutoVolumeEnabled {
			get { return this.autoVolumeEnabled; }
			set { this.autoVolumeEnabled = value; }
		}
		public bool? MediaKeysEnabled {
			get { return this.mediaKeysEnabled; }
			set { this.mediaKeysEnabled = value; }
		}
		public bool? RemovePlayedContentEnabled {
			get { return this.removePlayedContentEnabled; }
			set { this.removePlayedContentEnabled = value; }
		}
		public int? MainTabIndex {
			get { return this.mainTabIndex; }
			set { this.mainTabIndex = value; }
		}
	}
}
