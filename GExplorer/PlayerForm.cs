using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using AxWMPLib;
using WMPLib;

namespace Yusen.GExplorer {
	sealed partial class PlayerForm : FormSettingsBase, IFormWithSettings<PlayerFormSettings>{
		private sealed class BannerTag {
			private const long OrdMax = 10000000000000000;
			private static readonly Random rand = new Random();
			
			private readonly string dart;
			private readonly long ord;
			private readonly Uri image;
			private readonly Uri jump;

			public BannerTag(string dart) {
				this.dart = dart;
				this.ord = (long)(BannerTag.rand.NextDouble() * BannerTag.OrdMax);
				this.image = new Uri("http://ad.jp.doubleclick.net/ad/gyao.vision.spot/" + this.dart + ";sz=468x60;ord=" + this.ord.ToString() + "?");
				this.jump = new Uri("http://ad.jp.doubleclick.net/jump/gyao.vision.spot/" + this.dart + ";sz=468x60;ord=" + this.ord.ToString() + "?");
			}
			public string Dart {
				get { return this.dart; }
			}
			public long Ord {
				get { return this.ord; }
			}
			public Uri ImageUri {
				get { return this.image; }
			}
			public Uri JumpUri {
				get { return this.jump; }
			}
			
		}

		private const string AttribNameEntryUrl = "WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL";
		private const int VolumeMax = 100;
		private const int VolumeMin = 0;
		private static readonly Regex regexEndFlag = new Regex(":endFlg=([^:=]*)");
		private static readonly Regex regexDartTag = new Regex(":dartTag=([^:=]*)");

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
		private int? currentChapter = null;
		private Dictionary<string, string> currentAttribs = new Dictionary<string, string>();
		private bool? endFlag = null;
		
		private int volumeNormal = 100;
		private int volumeCf = 20;

		private ScreenSaveListener ssl;
		
		private PlayerForm() {
			InitializeComponent();
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
			settings.MainTabIndex = this.tabControl1.SelectedIndex;
			settings.StrechToFitEnabled = this.StreachToFitEnabled;
			settings.DisableScreenSaverEnabled = this.DisableScreenSaverEnabled;
			settings.RemovePlayedContentEnabled = this.RemovePlayedContentEnabled;
			settings.PlayListLoopEnabled = this.PlayListLoopEnabled;
			settings.SkipCmLicenseEnabled = this.SkipCmLicenseEnabled;
			settings.AutoVolumeEnabled = this.AutoVolumeEnabled;
			settings.VolumeNormal = this.VolumeNormal;
			settings.VolumeCf = this.VolumeCf;
		}

		public void ApplySettings(PlayerFormSettings settings) {
			base.ApplySettings(settings);
			this.tabControl1.SelectedIndex = settings.MainTabIndex ?? this.tabControl1.SelectedIndex;
			this.StreachToFitEnabled = settings.StrechToFitEnabled ?? this.StreachToFitEnabled;
			this.DisableScreenSaverEnabled = settings.DisableScreenSaverEnabled ?? this.DisableScreenSaverEnabled;
			this.RemovePlayedContentEnabled = settings.RemovePlayedContentEnabled ?? this.RemovePlayedContentEnabled;
			this.PlayListLoopEnabled = settings.PlayListLoopEnabled ?? this.PlayListLoopEnabled;
			this.SkipCmLicenseEnabled = settings.SkipCmLicenseEnabled ?? this.SkipCmLicenseEnabled;
			this.AutoVolumeEnabled = settings.AutoVolumeEnabled ?? this.AutoVolumeEnabled;
			this.VolumeNormal = settings.VolumeNormal ?? this.VolumeNormal;
			this.VolumeCf = settings.VolumeCf ?? this.VolumeCf;
			
			this.tsmiAlwaysOnTop.Checked = this.TopMost;
		}

		public string FilenameForSettings {
			get { return @"PlayerFormSettings.xml"; }
		}

		public ContentAdapter CurrentContent {
			get {
				return this.currentContent;
			}
			private set {
				this.currentContent = value;
				this.currentChapter = null;
				
				if (null == value) {
					Utility.SetTitlebarText(this, "PlayerForm");
					this.wmpMain.close();
					Uri blankUri = new Uri("about:blank");
					this.gwbDetail.Url = blankUri;
					this.gwbRecommend.Url = blankUri;
				} else {
					Utility.SetTitlebarText(this, value.DisplayName);
					IWMPMedia media = this.wmpMain.newMedia(value.PlayListUri.AbsoluteUri);
					this.wmpMain.currentPlaylist.appendItem(media);
					if (WMPPlayState.wmppsMediaEnded != this.wmpMain.playState) {
						//手動で切り替えた場合では強制的に再生させる
						this.wmpMain.currentMedia = media;
						this.wmpMain.Ctlcontrols.play();
					}
					this.gwbDetail.Navigate(value.DetailPageUri);
					this.gwbRecommend.Navigate(value.RecommendPageUri);
				}
				PlayList.Instance.CurrentContent = value;
			}
		}
		public int? CurrentChapter {
			get {
				return this.currentChapter;
			}
			private set {
				if (! value.Equals(this.currentChapter)) {
					this.currentChapter = value;
					IWMPMedia media;
					if (value.HasValue) {
						media = this.wmpMain.newMedia(this.CurrentContent.ChapterPlayListUriOf(value.Value).AbsoluteUri);
					} else {
						media = this.wmpMain.newMedia(this.CurrentContent.PlayListUri.AbsoluteUri);
					}
					this.wmpMain.currentPlaylist.appendItem(media);
					if (WMPPlayState.wmppsMediaEnded != this.wmpMain.playState) {
						//手動で切り替えた場合では強制的に再生させる
						this.wmpMain.currentMedia = media;
						this.wmpMain.Ctlcontrols.play();
					}
				}
			}
		}
		public bool StreachToFitEnabled {
			get { return this.tsmiStrechToFit.Checked; }
			set {
				this.tsmiStrechToFit.Checked = value;
				this.wmpMain.stretchToFit = value;
			}
		}
		public bool DisableScreenSaverEnabled {
			get { return this.tsmiDisableScreenSaver.Checked; }
			set {
				this.tsmiDisableScreenSaver.Checked = value;
				this.ssl.Enabled = value;
			}
		}
		public bool RemovePlayedContentEnabled {
			get { return this.tsmiRemovePlayedContent.Checked; }
			set { this.tsmiRemovePlayedContent.Checked = value; }
		}
		public bool PlayListLoopEnabled {
			get { return this.tsmiLoopPlayList.Checked; }
			set { this.tsmiLoopPlayList.Checked = value; }
		}
		public bool SkipCmLicenseEnabled {
			get { return this.tsmiSkipCmLicense.Checked; }
			set { this.tsmiSkipCmLicense.Checked = value; }
		}
		public bool AutoVolumeEnabled {
			get { return this.tsmiAutoVolume.Checked; }
			set {
				this.tsmiAutoVolume.Checked = value;
				this.tsmiVolumeNormal.Enabled = value;
				this.tsmiVolumeCf.Enabled = value;
			}
		}
		public int VolumeNormal {
			get { return this.volumeNormal; }
			set {
				if (PlayerForm.VolumeMin <= value && value <= PlayerForm.VolumeMax) {
					this.volumeNormal = value;
				} else {
					throw new ArgumentOutOfRangeException("VolumeNormal");
				}
			}
		}
		public int VolumeCf {
			get { return this.volumeCf; }
			set {
				if (PlayerForm.VolumeMin <= value && value <= PlayerForm.VolumeMax) {
					this.volumeCf = value;
				} else {
					throw new ArgumentOutOfRangeException("VolumeCf");
				}
			}
		}
		private bool IsCmLicense {
			get {
				return this.wmpMain.currentMedia.sourceURL.Contains("cm_license");
			}
		}

		private void PlayerForm_Load(object sender, EventArgs e) {
			Utility.AppendHelpMenu(this.menuStrip1);
			this.tsmiSettings.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;

			this.CreateUserCommandsMenuItems();
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
			this.FormClosed += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			};
			
			this.ssl = new ScreenSaveListener();
			this.ssl.ScreenSaverRaising += this.ssl_ScreenSaverRaising;
			this.FormClosed += delegate {
				this.ssl.ScreenSaverRaising -= this.ssl_ScreenSaverRaising;
				this.ssl.Dispose();
			};
			
			Utility.LoadSettingsAndEnableSaveOnClosed(this);
		}
		private void PlayerForm_FormClosing(object sender, FormClosingEventArgs e) {
			this.CurrentContent = null;
		}
		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateUserCommandsMenuItems();
		}

		private void ssl_ScreenSaverRaising(object sender, System.ComponentModel.CancelEventArgs e) {
			if (this.DisableScreenSaverEnabled) {
				e.Cancel = true;
			}
		}
		private void tsmiPlayChapter_Click(object sender, EventArgs e) {
			string title =  "特定のチャプターから再生";
			this.inputBoxDialog1.Title =title;
			this.inputBoxDialog1.Message = "チャプター番号の入力．空白の場合は通常再生．";
			this.inputBoxDialog1.Input = this.CurrentChapter.HasValue ? this.CurrentChapter.Value.ToString() : string.Empty;
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					int? chapter = null;
					string input = this.inputBoxDialog1.Input;
					if (! string.IsNullOrEmpty(input)) {
						try {
							chapter = int.Parse(input);
						} catch (Exception ex) {
							MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
							break;
						}
					}
					this.CurrentChapter = chapter;
					break;
			}
		}
		private void tsmiReload_Click(object sender, EventArgs e) {
			this.wmpMain.currentMedia = this.wmpMain.currentMedia;
			this.wmpMain.Ctlcontrols.play();
		}
		private void tsmiRemoveAndClose_Click(object sender, EventArgs e) {
			PlayList.Instance.Remove(this.CurrentContent);
			this.Close();
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
			ContentAdapter nextCont = PlayList.Instance.NextContentOf(this.CurrentContent);
			if (null == nextCont && this.PlayListLoopEnabled) {
				nextCont = PlayList.Instance.NextContentOf(null);
			}
			this.CurrentContent = nextCont;
		}
		private void tsmiNextContentWithDelete_Click(object sender, EventArgs e) {
			ContentAdapter nextCont = PlayList.Instance.NextContentOf(this.CurrentContent);
			if (null == nextCont && this.PlayListLoopEnabled) {
				nextCont = PlayList.Instance.NextContentOf(null);
			}
			PlayList.Instance.Remove(this.CurrentContent);
			if (this.CurrentContent.Equals(nextCont)) {
				this.CurrentContent = null;
			} else {
				this.CurrentContent = nextCont;
			}
		}
		private void tsmiPrevContent_Click(object sender, EventArgs e) {
			ContentAdapter prevCont = PlayList.Instance.PrevContentOf(this.CurrentContent);
			if (null == prevCont && this.PlayListLoopEnabled) {
				prevCont = PlayList.Instance.PrevContentOf(null);
			}
			this.CurrentContent = prevCont;
		}
		private void tsmiAlwaysOnTop_Click(object sender, EventArgs e) {
			this.TopMost = this.tsmiAlwaysOnTop.Checked;
		}
		private void tsmiStrechToFit_Click(object sender, EventArgs e) {
			this.StreachToFitEnabled = this.StreachToFitEnabled;
		}
		private void tsmiDisableScreenSaver_Click(object sender, EventArgs e) {
			this.DisableScreenSaverEnabled = this.DisableScreenSaverEnabled;
		}
		private void tsmiAutoVolume_Click(object sender, EventArgs e) {
			this.AutoVolumeEnabled = this.AutoVolumeEnabled;
		}
		private void tsmiVolumeNormal_Click(object sender, EventArgs e) {
			string title = "自動音量調整における本編の音量";
			this.inputBoxDialog1.Title =title;
			this.inputBoxDialog1.Message = "本編の音量を入力してください．" + "[" + PlayerForm.VolumeMin.ToString() + "-" + PlayerForm.VolumeMax.ToString() + "]";
			this.inputBoxDialog1.Input = this.VolumeNormal.ToString();
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					try {
						this.VolumeNormal = int.Parse(this.inputBoxDialog1.Input);
					} catch (Exception ex) {
						MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					break;
			}
		}
		private void tsmiVolumeCf_Click(object sender, EventArgs e) {
			string title = "自動音量調整におけるCFの音量";
			this.inputBoxDialog1.Title = title;
			this.inputBoxDialog1.Message = "CFの音量を入力してください．" + "[" + PlayerForm.VolumeMin.ToString() + "-" + PlayerForm.VolumeMax.ToString() + "]";
			this.inputBoxDialog1.Input = this.VolumeCf.ToString();
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					try{
						this.VolumeCf = int.Parse(this.inputBoxDialog1.Input);
					} catch (Exception ex) {
						MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					break;
			}
		}
		private void tsmiFocusOnWmp_Click(object sender, EventArgs e) {
			this.tabControl1.SelectedTab = this.tabPlayer;
			this.wmpMain.Focus();
		}
		private void tsmiShowItemInfo_Click(object sender, EventArgs e) {
			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<string, string> attrib in this.currentAttribs) {
				if (sb.Length > 0) {
					sb.Append("\n");
				}
				sb.Append(attrib.Key + "\t" + attrib.Value);
			}
			MessageBox.Show(sb.ToString(), "ItemInfo の表示", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		
		private void wmpMain_OpenStateChange(object sender, _WMPOCXEvents_OpenStateChangeEvent e) {
			switch ((WMPOpenState)e.newState) {
				case WMPOpenState.wmposMediaOpen:
					// ItemInfo の読み取り
					this.currentAttribs.Clear();
					int attribCount = this.wmpMain.currentMedia.attributeCount;
					for (int i=0; i<attribCount; i++) {
						string attribName = this.wmpMain.currentMedia.getAttributeName(i);
						this.currentAttribs.Add(attribName, this.wmpMain.currentMedia.getItemInfo(attribName));
					}
					// entry url のよみとり
					string entryUrl = this.currentAttribs[PlayerForm.AttribNameEntryUrl];
					//音量調整
					//THANKSTO: http://pc8.2ch.net/test/read.cgi/esite/1116115226/81 の神
					if (this.tsmiAutoVolume.Checked && !this.wmpMain.settings.mute) {
						bool isCf = entryUrl.StartsWith("Adv:");
						
						this.wmpMain.settings.volume = isCf ? this.VolumeCf : this.VolumeNormal;
						//謎の対応その3
						Thread t = new Thread(new ThreadStart(delegate {
							Thread.Sleep(100);
							if (!this.IsDisposed && !this.wmpMain.IsDisposed) {
								this.wmpMain.settings.volume = isCf ?  this.VolumeCf : this.VolumeNormal;
							}
						}));
						t.Start();
					}
					//endFlag の読み取り
					Match endFlagMatch = PlayerForm.regexEndFlag.Match(entryUrl);
					if (endFlagMatch.Success) {
						switch (endFlagMatch.Groups[1].Value) {
							case "0":
								this.endFlag = false;
								break;
							case "1":
								this.endFlag = true;
								break;
						}
					}
					//バナー表示
					Match dartTagMatch = PlayerForm.regexDartTag.Match(entryUrl);
					if (dartTagMatch.Success && !string.IsNullOrEmpty(dartTagMatch.Groups[1].Value)) {
						BannerTag bt = new BannerTag(dartTagMatch.Groups[1].Value);
						this.pboxBanner.Tag = bt;
						this.pboxBanner.LoadAsync(bt.ImageUri.AbsoluteUri);
						this.splitContainer1.Panel1Collapsed = false;
					} else {
						this.splitContainer1.Panel1Collapsed = true;
						this.pboxBanner.Image = null;
						this.pboxBanner.Tag = null;
					}
					//cm_licenseの早期スキップ
					if (this.SkipCmLicenseEnabled && this.IsCmLicense) {
						Thread t = new Thread(new ThreadStart(delegate {
							Thread.Sleep(100);
							if (!this.IsDisposed && !this.wmpMain.IsDisposed) {
								this.tsmiNextTrack.PerformClick();
							}
						}));
						t.Start();
					}
					break;
			}
		}
		private void wmpMain_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e) {
			switch((WMPPlayState)e.newState){
				case WMPPlayState.wmppsMediaEnded:
					if(this.IsCmLicense){
						//cm_licenseなら無視
						break;
					}
					if (this.CurrentChapter.HasValue && (!this.endFlag.HasValue || !this.endFlag.Value)) {
						//チャプターモードでエンドフラグが不明かFalse
						this.CurrentChapter++;
					} else {
						if (this.RemovePlayedContentEnabled) {
							this.tsmiNextContentWithDelete.PerformClick();
						} else {
							this.tsmiNextContent.PerformClick();
						}
					}
					this.endFlag = null;
					break;
			}
		}

		private void PlayerForm_KeyDown(object sender, KeyEventArgs e) {
			if (e.Handled) return;
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
		
		private void gwbDetail_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			switch (e.Url.Scheme) {
				case "javascript":
				case "mailto":
					break;
				default:
					if (!e.Url.Equals(this.CurrentContent.DetailPageUri)) {
						e.Cancel = true;
						Utility.Browse(e.Url);
					}
					break;
			}
		}
		private void gwbRecommend_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			switch (e.Url.Scheme) {
				case "javascript":
				case "mailto":
					break;
				default:
					if (!e.Url.Equals(this.CurrentContent.RecommendPageUri)) {
						e.Cancel = true;
						Utility.Browse(e.Url);
					}
					break;
			}
		}
		private void pboxBanner_Click(object sender, EventArgs e) {
			BannerTag bt = this.pboxBanner.Tag as BannerTag;
			if (null != bt) {
				Utility.Browse(bt.JumpUri);
			}
		}
		private void tsmiBannerCopyJumpUri_Click(object sender, EventArgs e) {
			BannerTag bt = this.pboxBanner.Tag as BannerTag;
			if (null != bt) {
				Clipboard.SetText(bt.JumpUri.AbsoluteUri);
			}
		}
		private void tsmiBannerCopyImageUri_Click(object sender, EventArgs e) {
			BannerTag bt = this.pboxBanner.Tag as BannerTag;
			if (null != bt) {
				Clipboard.SetText(bt.ImageUri.AbsoluteUri);
			}
		}
		private void tsmiBannerCopyImage_Click(object sender, EventArgs e) {
			if (null != this.pboxBanner.Image) {
				Clipboard.SetImage(this.pboxBanner.Image);
			}
		}
	}

#if false
	public enum PlayMode {
		Playlist,
		PlaylistLoop,
		SingleRepeat,
		Shuffle,
	}
#endif

	public class PlayerFormSettings : FormSettingsBaseSettings {
		private int? mainTabIndex;
		private bool? strechToFitEnabled;
		private bool? disableScreenSaverEnabled;
		private bool? removePlayedContentEnabled;
		private bool? playListLoopEnabled;
		private bool? skipCmLicenseEnabled;
		private bool? autoVolumeEnabled;
		private int? volumeNormal;
		private int? volumeCf;

		public int? MainTabIndex {
			get { return this.mainTabIndex; }
			set { this.mainTabIndex = value; }
		}
		public bool? StrechToFitEnabled {
			get { return this.strechToFitEnabled; }
			set { this.strechToFitEnabled = value; }
		}
		public bool? DisableScreenSaverEnabled {
			get { return this.disableScreenSaverEnabled; }
			set { this.disableScreenSaverEnabled = value; }
		}
		public bool? RemovePlayedContentEnabled {
			get { return this.removePlayedContentEnabled; }
			set { this.removePlayedContentEnabled = value; }
		}
		public bool? PlayListLoopEnabled {
			get { return this.playListLoopEnabled; }
			set { this.playListLoopEnabled = value; }
		}
		public bool? SkipCmLicenseEnabled {
			get { return this.skipCmLicenseEnabled; }
			set { this.skipCmLicenseEnabled = value; }
		}
		public bool? AutoVolumeEnabled {
			get { return this.autoVolumeEnabled; }
			set { this.autoVolumeEnabled = value; }
		}
		public int? VolumeNormal {
			get { return this.volumeNormal; }
			set { this.volumeNormal = value; }
		}
		public int? VolumeCf {
			get { return this.volumeCf; }
			set { this.volumeCf = value; }
		}
	}
}
