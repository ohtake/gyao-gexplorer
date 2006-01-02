using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using AxWMPLib;
using WMPLib;

namespace Yusen.GExplorer {
	sealed partial class PlayerForm : FormSettingsBase, IFormWithSettings<PlayerFormSettings>{
		private sealed class BannerTag {
			private const long OrdMax = 10000000000000000;
			private static readonly Random rand = new Random();
			
			private readonly string dart;
			private readonly long ord;
			private readonly Uri page;

			public BannerTag(string dart) {
				this.dart = dart;
				this.ord = (long)(BannerTag.rand.NextDouble() * BannerTag.OrdMax);
				this.page = new Uri("http://ad.jp.doubleclick.net/adi/gyao.spot.sky/" + this.dart + ";sz=120x600;ord=" + this.ord.ToString() + "?");
			}
			public string Dart {
				get { return this.dart; }
			}
			public long Ord {
				get { return this.ord; }
			}
			public Uri PageUri {
				get { return this.page; }
			}
		}

		private const string AttribNameEntryUrl = "WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL";
		private const int VolumeMax = 100;
		private const int VolumeMin = 0;
		private const int ZoomMax = 200;
		private const int ZoomMin = 25;
		private static readonly Size WmpUiSize = new Size(0, 64);
		private static readonly Size WmpMarginSize = new Size(3, 3);
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
			p.Show();
			p.Focus();
			p.CurrentContent = contentAdapter;
		}

		private ContentAdapter currentContent = null;
		private int? currentChapter = null;
		private Dictionary<string, string> currentAttribs = new Dictionary<string, string>();
		private bool? endFlag = null;

		private int zoomRatioOnResize = 100;
		private int volumeNormal = 100;
		private int volumeCf = 20;

		private ScreenSaveListener ssl;

		private PlayerForm() {
			InitializeComponent();
		}

		private void OpenVideo() {
			if(null == this.CurrentContent) {
				this.wmpMain.currentMedia = null;
			}
			IWMPMedia media;
			if(this.CurrentChapter.HasValue) {
				media = this.wmpMain.newMedia(this.CurrentContent.ChapterPlayListUriOf(this.CurrentChapter.Value).AbsoluteUri);
			} else {
				media = this.wmpMain.newMedia(this.CurrentContent.PlayListUri.AbsoluteUri);
			}
			this.wmpMain.currentPlaylist.appendItem(media);
			if(WMPPlayState.wmppsMediaEnded != this.wmpMain.playState) {
				//手動で切り替えた場合では強制的に再生させる
				this.wmpMain.currentMedia = media;
				this.wmpMain.Ctlcontrols.play();
			}
			this.UpdateStatusbatText();
		}
		private void CreateUserCommandsMenuItems() {
			this.tsmiUserCommands.DropDownItems.Clear();
			List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();
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
				menuItems.Add(tsmi);
			}
			this.tsmiUserCommands.DropDownItems.AddRange(menuItems.ToArray());
			this.tsmiUserCommands.Enabled = this.tsmiUserCommands.HasDropDownItems;
		}
		private void UpdateStatusbatText() {
			if(null != this.CurrentContent) {
				IWMPMedia curMedia = this.wmpMain.currentMedia;
				this.tsslId.Text = this.CurrentContent.ContentId;
				this.tsslChapter.Text = this.CurrentChapter.HasValue ?
					"チャプター" + this.CurrentChapter.Value.ToString() + "(endFlag=" + this.endFlag.ToString() + ")"
					: "通常";
				this.tsslSize.Text = curMedia.imageSourceWidth.ToString() + "x" + curMedia.imageSourceHeight.ToString();
				this.tsslDuration.Text = new TimeSpan((long)(10000000 * curMedia.duration)).ToString();
				this.tsslTitle.Text = curMedia.name;
			}
		}
		private void ModifyVolume() {
			bool? isCf = this.IsCf;
			if(isCf.HasValue) {
				this.wmpMain.settings.volume = isCf.Value ? this.VolumeCf : this.VolumeNormal;
			}
		}
		private void HideUi() {
			Point loc = this.PointToClient(this.splitContainer1.PointToScreen(new Point()));
			loc.X += SystemInformation.SizingBorderWidth;
			loc.Y += SystemInformation.SizingBorderWidth + SystemInformation.CaptionHeight;
			loc += PlayerForm.WmpMarginSize;
			Size size = this.splitContainer1.ClientSize;
			size -= PlayerForm.WmpUiSize;
			this.splitContainer2.Panel2Collapsed = false;
			this.Region = new Region(new Rectangle(loc, size));
		}
		private void ShowUi() {
			this.splitContainer2.Panel2Collapsed = true;
			this.Region = null;
		}
		public void FillSettings(PlayerFormSettings settings) {
			base.FillSettings(settings);
			settings.HideUiOnDeactivatedEnabled = this.HideUiOnDeactivatedEnabled;
			settings.AutoSizeOnNormalEnabled = this.AutoSizeOnNormalEnabled;
			settings.AutoSizeOnCfEnabled = this.AutoSizeOnCfEnabled;
			settings.StrechToFitEnabled = this.StreachToFitEnabled;
			settings.DisableScreenSaverEnabled = this.DisableScreenSaverEnabled;
			settings.RemovePlayedContentEnabled = this.RemovePlayedContentEnabled;
			settings.SkipCmLicenseEnabled = this.SkipCmLicenseEnabled;
			settings.ChapterModeFromBegining = this.ChapterModeFromBegining;
			settings.AutoVolumeEnabled = this.AutoVolumeEnabled;
			settings.ZoomRatioOnResize = this.ZoomRatioOnResize;
			settings.VolumeNormal = this.VolumeNormal;
			settings.VolumeCf = this.VolumeCf;
		}
		
		public void ApplySettings(PlayerFormSettings settings) {
			base.ApplySettings(settings);
			this.HideUiOnDeactivatedEnabled = settings.HideUiOnDeactivatedEnabled ?? this.HideUiOnDeactivatedEnabled;
			this.AutoSizeOnNormalEnabled = settings.AutoSizeOnNormalEnabled ?? this.AutoSizeOnNormalEnabled;
			this.AutoSizeOnCfEnabled = settings.AutoSizeOnCfEnabled ?? this.AutoSizeOnCfEnabled;
			this.StreachToFitEnabled = settings.StrechToFitEnabled ?? this.StreachToFitEnabled;
			this.DisableScreenSaverEnabled = settings.DisableScreenSaverEnabled ?? this.DisableScreenSaverEnabled;
			this.RemovePlayedContentEnabled = settings.RemovePlayedContentEnabled ?? this.RemovePlayedContentEnabled;
			this.SkipCmLicenseEnabled = settings.SkipCmLicenseEnabled ?? this.SkipCmLicenseEnabled;
			this.ChapterModeFromBegining = settings.ChapterModeFromBegining ?? this.ChapterModeFromBegining;
			this.AutoVolumeEnabled = settings.AutoVolumeEnabled ?? this.AutoVolumeEnabled;
			this.ZoomRatioOnResize = settings.ZoomRatioOnResize ?? this.ZoomRatioOnResize;
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
				this.currentChapter = this.ChapterModeFromBegining ? 1 : (int?)null;
				this.endFlag = null;
				
				if (null == value) {
					this.Text = "PlayerForm";
					this.wmpMain.close();
				} else {
					this.Text = value.DisplayName;
					this.OpenVideo();
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
					this.endFlag = null;
					this.OpenVideo();
				}
			}
		}
		public bool HideUiOnDeactivatedEnabled {
			get { return this.tsmiHideUiOnDeactivated.Checked; }
			set { this.tsmiHideUiOnDeactivated.Checked = value; }
		}
		public bool AutoSizeOnNormalEnabled {
			get { return this.tsmiAutoSizeOnNormal.Checked; }
			set { this.tsmiAutoSizeOnNormal.Checked = value; }
		}
		public bool AutoSizeOnCfEnabled {
			get { return this.tsmiAutoSizeOnCf.Checked; }
			set { this.tsmiAutoSizeOnCf.Checked = value; }
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
		public bool SkipCmLicenseEnabled {
			get { return this.tsmiSkipCmLicense.Checked; }
			set { this.tsmiSkipCmLicense.Checked = value; }
		}
		public bool ChapterModeFromBegining {
			get { return this.tsmiChapterModeFromBegining.Checked; }
			set { this.tsmiChapterModeFromBegining.Checked = value; }
		}
		public int ZoomRatioOnResize {
			get { return this.zoomRatioOnResize; }
			set {
				if(PlayerForm.ZoomMin <= value && value <= PlayerForm.ZoomMax) {
					this.zoomRatioOnResize = value;
				} else {
					throw new ArgumentOutOfRangeException("ZoomRatioOnResize");
				}
			}
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
		private bool? IsCf {
			get {
				string entryUrl = this.currentAttribs[PlayerForm.AttribNameEntryUrl];
				if(entryUrl.StartsWith("Adv:")) {
					return true;
				} else if(entryUrl.StartsWith("Cnt:")) {
					return false;
				} else {
					return null;
				}
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
		private void PlayerForm_Activated(object sender, EventArgs e) {
			this.ShowUi();
		}
		private void PlayerForm_Deactivate(object sender, EventArgs e) {
			if(this.HideUiOnDeactivatedEnabled) {
				this.HideUi();
			}
		}
		private void PlayerForm_Resize(object sender, EventArgs e) {
			if(null != this.Region) {
				this.HideUi();
			}
		}
		private void PlayerForm_KeyDown(object sender, KeyEventArgs e) {
			if(e.Handled) return;
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
		}
		
		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateUserCommandsMenuItems();
		}
		private void ssl_ScreenSaverRaising(object sender, System.ComponentModel.CancelEventArgs e) {
			if (this.DisableScreenSaverEnabled) {
				e.Cancel = true;
			}
		}
		#region メインメニュー
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
			this.endFlag = null;
			this.OpenVideo();
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
			if (null == nextCont) {
				nextCont = PlayList.Instance.NextContentOf(null);
			}
			this.CurrentContent = nextCont;
		}
		private void tsmiNextContentWithDelete_Click(object sender, EventArgs e) {
			ContentAdapter nextCont = PlayList.Instance.NextContentOf(this.CurrentContent);
			if (null == nextCont) {
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
			if (null == prevCont) {
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
		private void tsmiResizeZoomValue_Click(object sender, EventArgs e) {
			string title = "リサイズ時の倍率";
			this.inputBoxDialog1.Title =title;
			this.inputBoxDialog1.Message = "サイズの倍率を入力してください．" + "[" + PlayerForm.ZoomMin.ToString() + "-" + PlayerForm.ZoomMax.ToString() + "]";
			this.inputBoxDialog1.Input = this.ZoomRatioOnResize.ToString();
			switch(this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					try {
						this.ZoomRatioOnResize = int.Parse(this.inputBoxDialog1.Input);
					} catch(Exception ex) {
						MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					break;
			}
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
			this.wmpMain.Focus();
		}
		private void tsmiResizeToVideoResolution_Click(object sender, EventArgs e) {
			this.WindowState = FormWindowState.Normal;
			Size videoSize = new Size(this.wmpMain.currentMedia.imageSourceWidth, this.wmpMain.currentMedia.imageSourceHeight);
			Size distSize = new Size(videoSize.Width*this.ZoomRatioOnResize/100, videoSize.Height*this.ZoomRatioOnResize/100);
			for(int i=0; i<2; i++) {
				//リサイズによってメニューバーやステータスバーの高さが変わることがあるので
				//2回リサイズを試みる
				Size delta = distSize + PlayerForm.WmpUiSize - this.wmpMain.Size;
				this.Size += delta;
			}
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
		private void tsmiBrowseDetail_Click(object sender, EventArgs e) {
			Utility.Browse(this.CurrentContent.DetailPageUri);
		}
		private void tsmiBrowseRecommended_Click(object sender, EventArgs e) {
			Utility.Browse(this.CurrentContent.RecommendPageUri);
		}
		#endregion
		#region WMPのイベント
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
						//一旦音量を代入
						this.ModifyVolume();
						//音量が変わらないことがあるのでタイマーで後から再代入
						this.timerAutoVolume.Start();
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
						this.wbBanner.Navigate(bt.PageUri);
						this.splitContainer1.Panel2Collapsed = false;
					} else {
						this.splitContainer1.Panel2Collapsed = true;
						this.wbBanner.Navigate("about:blank");
					}
					//cm_licenseの早期スキップ
					if (this.SkipCmLicenseEnabled && this.IsCmLicense) {
						this.timerSkipCmLisence.Start();
					}
					//ステータスバー更新
					this.UpdateStatusbatText();
					//リサイズ
					if(this.IsCf.HasValue && FormWindowState.Normal == this.WindowState) {
						if(this.IsCf.Value) {
							if(this.AutoSizeOnCfEnabled) {
								this.tsmiResizeToVideoResolution.PerformClick();
							}
						} else {
							if(this.AutoSizeOnNormalEnabled) {
								this.tsmiResizeToVideoResolution.PerformClick();
							}
						}
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
		#endregion
		
		private void timerAutoVolume_Tick(object sender, EventArgs e) {
			this.timerAutoVolume.Stop();
			this.ModifyVolume();
		}
		private void timerSkipCmLisence_Tick(object sender, EventArgs e) {
			this.timerSkipCmLisence.Stop();
			this.tsmiNextTrack.PerformClick();
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
		private bool? hideUiOnDeactivatedEnabled;
		private bool? autoSizeOnNormalEnabled;
		private bool? autoSizeOnCfEnabled;
		private bool? strechToFitEnabled;
		private bool? disableScreenSaverEnabled;
		private bool? removePlayedContentEnabled;
		private bool? skipCmLicenseEnabled;
		private bool? chapterModeFromBegining;
		private bool? autoVolumeEnabled;
		private int? zoomRatioOnResize;
		private int? volumeNormal;
		private int? volumeCf;

		public bool? HideUiOnDeactivatedEnabled {
			get { return this.hideUiOnDeactivatedEnabled; }
			set { this.hideUiOnDeactivatedEnabled = value; }
		}
		public bool? AutoSizeOnNormalEnabled {
			get { return this.autoSizeOnNormalEnabled; }
			set { this.autoSizeOnNormalEnabled = value; }
		}
		public bool? AutoSizeOnCfEnabled {
			get { return this.autoSizeOnCfEnabled; }
			set { this.autoSizeOnCfEnabled = value; }
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
		public bool? SkipCmLicenseEnabled {
			get { return this.skipCmLicenseEnabled; }
			set { this.skipCmLicenseEnabled = value; }
		}
		public bool? ChapterModeFromBegining {
			get { return this.chapterModeFromBegining; }
			set { this.chapterModeFromBegining = value; }
		}
		public bool? AutoVolumeEnabled {
			get { return this.autoVolumeEnabled; }
			set { this.autoVolumeEnabled = value; }
		}
		public int? ZoomRatioOnResize {
			get { return this.zoomRatioOnResize; }
			set { this.zoomRatioOnResize = value; }
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
