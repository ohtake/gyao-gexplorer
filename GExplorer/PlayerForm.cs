using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using AxWMPLib;
using WMPLib;
using System.ComponentModel;
using System.Drawing;
using System.IO;

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
		public static void AddToPlayList(ContentAdapter content) {
			PlayerForm p = PlayerForm.Instance;
			p.AddContent(content);
			p.Show();
			p.Focus();
		}
		public static void ShowAndFocus() {
			PlayerForm.Instance.Show();
			PlayerForm.Instance.Focus();
		}

		private event EventHandler PlaylistChanged;
		private event EventHandler CurrentContentChanged;
		
		private ContentAdapter currentContent = null;
		private List<ContentAdapter> playlist = new List<ContentAdapter>();
		
		private PlayerForm() {
			InitializeComponent();
			Utility.AppendHelpMenu(this.menuStrip1);
			
			this.CreateUserCommandsMenuItems();
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
			this.FormClosing += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			};

			Utility.LoadSettingsAndEnableSaveOnClosed(this);
			
			this.PlaylistChanged += new EventHandler(PlayerForm_PlaylistChanged);
			this.CurrentContentChanged += new EventHandler(PlayerForm_CurrentContentChanged);

			this.PlayerForm_PlaylistChanged(null, EventArgs.Empty);
			this.PlayerForm_CurrentContentChanged(null, EventArgs.Empty);
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
							uc2.Execute(this.CurrentContent);
						}
					}
				};
				this.tsmiUserCommands.DropDownItems.Add(tsmi);
			}
			this.tsmiUserCommands.Enabled = this.HasCurrentContent && this.tsmiUserCommands.HasDropDownItems;
			//プレイリストのコンテキストメニュー
			this.tsmiPlaylistCommands.DropDownItems.Clear();
			foreach (UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(uc.Title);
				tsmi.Tag = uc;
				tsmi.Click += delegate(object sender, EventArgs e) {
					ToolStripMenuItem tsmi2 = sender as ToolStripMenuItem;
					if (null != tsmi2) {
						UserCommand uc2 = tsmi2.Tag as UserCommand;
						if (null != uc2) {
							uc2.Execute(this.SelectedContentInPlaylistView);
						}
					}
				};
				this.tsmiPlaylistCommands.DropDownItems.Add(tsmi);
			}
			this.tsmiPlaylistCommands.Enabled = this.tsmiPlaylistCommands.HasDropDownItems;
		}
		public void FillSettings(PlayerFormSettings settings) {
			base.FillSettings(settings);
			settings.AutoVolumeEnabled = this.AutoVolumeEnabled;
			settings.MediaKeysEnabled = this.MediaKeysEnabled;
			settings.RemovePlayedContentEnabled = this.RemovePlayedContentEnabled;
			settings.AutoLoadPlaylistEnabled = this.AutoLoadPlaylistEnabled;
			settings.WaitSecondsAfterLastCallEnabled = this.WaitSecondsAfterLastCallEnabled;
			settings.CurrentContent = this.CurrentContent;
			settings.PlayList = new List<ContentAdapter>(this.playlist);
			settings.MainTabIndex = this.tabControl1.SelectedIndex;
			settings.PlayListColumnId = this.lviewPlaylist.Columns[0].Width;
			settings.PlayListColumnName = this.lviewPlaylist.Columns[1].Width;
			settings.PlayListWidth = this.splitContainer1.SplitterDistance;
			this.contentDetailView1.FillSettings(settings.ContentDetailViewSettings);
		}

		public void ApplySettings(PlayerFormSettings settings) {
			base.ApplySettings(settings);
			this.AutoVolumeEnabled = settings.AutoVolumeEnabled ?? this.AutoVolumeEnabled;
			this.MediaKeysEnabled = settings.MediaKeysEnabled ?? this.MediaKeysEnabled;
			this.RemovePlayedContentEnabled = settings.RemovePlayedContentEnabled ?? this.RemovePlayedContentEnabled;
			this.AutoLoadPlaylistEnabled = settings.AutoLoadPlaylistEnabled ?? this.AutoLoadPlaylistEnabled;
			this.WaitSecondsAfterLastCallEnabled = settings.WaitSecondsAfterLastCallEnabled ?? this.WaitSecondsAfterLastCallEnabled;
			this.tabControl1.SelectedIndex = settings.MainTabIndex ?? this.tabControl1.SelectedIndex;
			this.lviewPlaylist.Columns[0].Width = settings.PlayListColumnId ?? this.lviewPlaylist.Columns[0].Width;
			this.lviewPlaylist.Columns[1].Width = settings.PlayListColumnName ?? this.lviewPlaylist.Columns[1].Width;
			this.splitContainer1.SplitterDistance = settings.PlayListWidth ?? this.splitContainer1.SplitterDistance;
			this.contentDetailView1.ApplySettings(settings.ContentDetailViewSettings);
			
			if (this.AutoLoadPlaylistEnabled) {
				this.playlist = settings.PlayList;
				this.CurrentContent = settings.CurrentContent;
			}
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
				if (value == this.CurrentContent) return;
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
				this.OnCurrentContentChanged();
			}
		}
		private bool HasCurrentContent {
			get { return null != this.CurrentContent; }
		}
		private int PlaylistLength {
			get { return this.playlist.Count; }
		}
		private int PlaylistCurrentPosition {
			get { return this.PlaylistIndexOf(this.CurrentContent); }
		}
		private int PlaylistIndexOf(ContentAdapter cont) {
			return this.playlist.IndexOf(cont);
		}
		private ContentAdapter SelectedContentInPlaylistView {
			get {
				if (this.lviewPlaylist.SelectedItems.Count > 0) {
					return this.lviewPlaylist.SelectedItems[0].Tag as ContentAdapter;
				} else {
					return null;
				}
			}
		}
		private bool HasSelectedContentInPlaylistView {
			get{return null != this.SelectedContentInPlaylistView;}
		}

		public void AddContent(ContentAdapter cont) {
			if (!this.playlist.Contains(cont)) {
				this.playlist.Add(cont);
				this.OnPlaylistChanged();
			}
			if (!this.HasCurrentContent) {
				this.CurrentContent = cont;
			}
		}
		public void SwapContents(int idx1, int idx2) {
			if(idx1 == idx2) return;
			ContentAdapter temp = this.playlist[idx1];
			this.playlist[idx1] = this.playlist[idx2];
			this.playlist[idx2] = temp;
			this.OnPlaylistChanged();
		}
		public void RemoveContent(ContentAdapter cont) {
			if (cont.Equals(this.CurrentContent)) {
				int length = this.PlaylistLength;
				int idx = this.PlaylistCurrentPosition;
				if (1 == length) {
					this.CurrentContent = null;
				} else if (idx+1 == length) {
					this.CurrentContent = this.playlist[idx-1];
				} else {
					this.CurrentContent = this.playlist[idx+1];
				}
			}
			if (this.playlist.Remove(cont)){
				this.OnPlaylistChanged();
			}
		}
		public void ClearContents() {
			this.CurrentContent = null;
			if (this.playlist.Count > 0) {
				this.playlist.Clear();
				this.OnPlaylistChanged();
			}
		}
		
		protected void OnPlaylistChanged() {
			if (null != this.PlaylistChanged) {
				this.PlaylistChanged(this, EventArgs.Empty);
			}
		}
		protected void OnCurrentContentChanged() {
			if (null != this.CurrentContentChanged) {
				this.CurrentContentChanged(this, EventArgs.Empty);
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
		public bool AutoLoadPlaylistEnabled {
			get { return this.tsmiAutoLoadPlaylist.Checked; }
			set { this.tsmiAutoLoadPlaylist.Checked = value; }
		}
		public bool WaitSecondsAfterLastCallEnabled {
			get { return this.tsmiWaitSecondsAfterLastCall.Checked; }
			set { this.tsmiWaitSecondsAfterLastCall.Checked = value; }
		}

		private void RefleshPlaylistView() {
			this.lviewPlaylist.BeginUpdate();
			this.lviewPlaylist.Items.Clear();
			foreach (ContentAdapter cont in this.playlist) {
				ListViewItem lvi = new ListViewItem(new string[] { cont.ContentId, cont.DisplayName });
				lvi.Tag = cont;
				if (cont.Equals(this.CurrentContent)) {
					lvi.Font = new Font(lvi.Font, FontStyle.Bold);
				}
				this.lviewPlaylist.Items.Add(lvi);
			}
			this.lviewPlaylist.EndUpdate();
		}
		private void PlayerForm_PlaylistChanged(object sender, EventArgs e) {
			this.RefleshPlaylistView();
		}
		private void PlayerForm_CurrentContentChanged(object sender, EventArgs e) {
			this.RefleshPlaylistView();
			
			bool hasCurrenContent = this.HasCurrentContent;
			this.tsmiReload.Enabled = hasCurrenContent;
			this.tsmiUserCommands.Enabled = hasCurrenContent && this.tsmiUserCommands.HasDropDownItems;
		}
		private void tsmiReload_Click(object sender, EventArgs e) {
			this.wmpMain.URL = this.currentContent.MediaFileUri.AbsoluteUri;
		}
		private void tsmiExportAsAsx_Click(object sender, EventArgs e) {
			if (DialogResult.OK == this.saveFileDialog1.ShowDialog()) {
				string filename = this.saveFileDialog1.FileName;
				using (TextWriter writer = new StreamWriter(filename)) {
					writer.WriteLine("<ASX VERSION=\"3.0\">");
					foreach (ContentAdapter cont in this.playlist) {
						writer.WriteLine("<Entry>");
						writer.WriteLine("\t<ref href=\"" + cont.MediaFileUri.AbsoluteUri + "\"/>");
						writer.WriteLine("</Entry>");
					}
					writer.WriteLine("</ASX>");
				}
			}
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
			int length = this.PlaylistLength;
			int idx = this.PlaylistCurrentPosition;
			if (idx+1 < length) {
				this.CurrentContent = this.playlist[idx+1];
			} else {
				this.CurrentContent = null;
			}
		}
		private void tsmiNextContentWithDelete_Click(object sender, EventArgs e) {
			this.RemoveContent(this.CurrentContent);
		}
		private void tsmiPrevContent_Click(object sender, EventArgs e) {
			int idx = this.PlaylistCurrentPosition;
			if (idx > 0) {
				this.CurrentContent = this.playlist[idx-1];
			} else {
				this.CurrentContent = null;
			}
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
					this.wmpMain.settings.volume = isCf ? 19 :  99;
					this.wmpMain.settings.volume = isCf ? 20 : 100;
				}));
				t.Start();
			}
		}
		private void wmpMain_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e) {
			if (WMPPlayState.wmppsMediaEnded == this.wmpMain.playState) {
				Thread t = new Thread(new ThreadStart(delegate{
					if (this.WaitSecondsAfterLastCallEnabled) {
						Thread.Sleep(3000);
					}
					if (this.RemovePlayedContentEnabled) {
						this.tsmiNextContentWithDelete.PerformClick();
					} else {
						this.tsmiNextContent.PerformClick();
					}
				}));
				t.Start();
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
		
		private void lviewPlaylist_SelectedIndexChanged(object sender, EventArgs e) {
			bool hasSelection = this.HasSelectedContentInPlaylistView;
			if (hasSelection) {
				this.contentDetailView1.Content = this.SelectedContentInPlaylistView;
			}
			if (hasSelection) {
				int selIdx = this.lviewPlaylist.SelectedIndices[0];
				int items = this.lviewPlaylist.Items.Count;
				this.btnUp.Enabled = selIdx > 0;
				this.btnDown.Enabled = selIdx +1 < items;
				this.btnDelete.Enabled = true;
			} else {
				this.btnUp.Enabled = false;
				this.btnDown.Enabled = false;
				this.btnDelete.Enabled = false;
			}
		}
		
		private void lviewPlaylist_DoubleClick(object sender, EventArgs e) {
			if (this.HasSelectedContentInPlaylistView) {
				this.CurrentContent = this.SelectedContentInPlaylistView;
				this.tsmiFocusOnWmp.PerformClick();
			}
		}
		
		private void lviewPlaylist_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					if (this.HasSelectedContentInPlaylistView) {
						this.CurrentContent = this.SelectedContentInPlaylistView;
						this.tsmiFocusOnWmp.PerformClick();
					}
					break;
				case Keys.Delete:
					if (this.HasSelectedContentInPlaylistView) {
						this.RemoveContent(this.SelectedContentInPlaylistView);
					}
					break;
			}
		}

		private void btnUp_Click(object sender, EventArgs e) {
			int selIdx = this.lviewPlaylist.SelectedIndices[0];
			this.SwapContents(selIdx, selIdx-1);
			this.lviewPlaylist.Items[selIdx-1].Selected = true;
		}

		private void btnDown_Click(object sender, EventArgs e) {
			int selIdx = this.lviewPlaylist.SelectedIndices[0];
			this.SwapContents(selIdx, selIdx+1);
			this.lviewPlaylist.Items[selIdx+1].Selected = true;
		}

		private void btnDelete_Click(object sender, EventArgs e) {
			this.RemoveContent(this.SelectedContentInPlaylistView);
			this.contentDetailView1.Content = null;
		}

		private void btnClear_Click(object sender, EventArgs e) {
			this.ClearContents();
			this.contentDetailView1.Content = null;
		}

		private void tsmiPlay_Click(object sender, EventArgs e) {
			this.CurrentContent = this.SelectedContentInPlaylistView;
			this.tsmiFocusOnWmp.PerformClick();
		}

		private void tsmiMoveTop_Click(object sender, EventArgs e) {
			int selIdx = this.lviewPlaylist.SelectedIndices[0];
			this.SwapContents(selIdx, 0);
			this.lviewPlaylist.Items[0].Selected = true;
		}

		private void tsmiMoveUp_Click(object sender, EventArgs e) {
			this.btnUp.PerformClick();
		}

		private void tsmiMoveDown_Click(object sender, EventArgs e) {
			this.btnDown.PerformClick();
		}

		private void tsmiMoveBottom_Click(object sender, EventArgs e) {
			int selIdx = this.lviewPlaylist.SelectedIndices[0];
			this.SwapContents(selIdx, this.lviewPlaylist.Items.Count -1);
			this.lviewPlaylist.Items[this.lviewPlaylist.Items.Count -1].Selected = true;
		}

		private void tsmiDelete_Click(object sender, EventArgs e) {
			this.btnDelete.PerformClick();
		}

		private void cmsPlaylist_Opening(object sender, CancelEventArgs e) {
			if (!this.HasSelectedContentInPlaylistView) {
				e.Cancel = true;
				return;
			}
			this.tsmiMoveTop.Enabled = this.btnUp.Enabled;
			this.tsmiMoveUp.Enabled = this.btnUp.Enabled;
			this.tsmiMoveDown.Enabled = this.btnDown.Enabled;
			this.tsmiMoveBottom.Enabled = this.btnDown.Enabled;
			this.tsmiDelete.Enabled = this.btnDelete.Enabled;
		}
	}
	
	public class PlayerFormSettings : FormSettingsBaseSettings {
		private bool? autoVolumeEnabled;
		private bool? mediaKeysEnabled;
		private bool? removePlayedContentEnabled;
		private bool? autoLoadPlaylistEnabled;
		private bool? waitSecondsAfterLastCallEnabled;
		private ContentAdapter currentContent;
		private List<ContentAdapter> playList = new List<ContentAdapter>();
		private int? mainTabIndex;
		private int? playListColumnId;
		private int? playListColumnName;
		private int? playListWidth;
		private ContentDetailViewSettings contentDetailViewSettings = new ContentDetailViewSettings();
		
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
		public bool? AutoLoadPlaylistEnabled {
			get { return this.autoLoadPlaylistEnabled; }
			set { this.autoLoadPlaylistEnabled = value; }
		}
		public bool? WaitSecondsAfterLastCallEnabled {
			get { return this.waitSecondsAfterLastCallEnabled; }
			set { this.waitSecondsAfterLastCallEnabled = value; }
		}
		public ContentAdapter CurrentContent {
			get { return this.currentContent; }
			set { this.currentContent = value; }
		}
		public List<ContentAdapter> PlayList {
			get { return this.playList; }
			set { this.playList = value; }
		}
		public int? MainTabIndex {
			get { return this.mainTabIndex; }
			set { this.mainTabIndex = value; }
		}
		public int? PlayListColumnId {
			get { return this.playListColumnId; }
			set { this.playListColumnId = value; }
		}
		public int? PlayListColumnName {
			get { return this.playListColumnName; }
			set { this.playListColumnName = value; }
		}
		public int? PlayListWidth {
			get { return this.playListWidth; }
			set { this.playListWidth = value; }
		}
		public ContentDetailViewSettings ContentDetailViewSettings {
			get { return this.contentDetailViewSettings; }
			set { this.contentDetailViewSettings = value; }
		}
	}
}
