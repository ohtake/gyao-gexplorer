using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using AxWMPLib;
using WMPLib;
using DRegion=System.Drawing.Region;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public sealed partial class PlayerForm : FormSettingsBase, IFormWithNewSettings<PlayerForm.PlayerFormSettings> {
		public sealed class PlayerFormSettings : INewSettings<PlayerFormSettings>{
			private const int VolumeMin = 0;
			private const int VolumeMax = 100;
			private const int ZoomMin = 25;
			private const int ZoomMax = 200;
			private const string VolumeRangeMessage = "[0-100]";
			private const string ZoomRangeMessage = "[25-200]";

			private PlayerForm owner;
			
			public PlayerFormSettings() : this(null){
			}
			internal PlayerFormSettings(PlayerForm owner){
				this.owner = owner;
				this.formSettingsBaseSettings = new FormSettingsBaseSettings(owner);
			}

			[Browsable(false)]
			[XmlIgnore]
			private bool HasOwner {
				get { return null != this.owner; }
			}

			[ReadOnly(true)]
			[Category("位置やサイズ")]
			[DisplayName("フォームの基本設定")]
			[Description("フォームの基本的な設定です．")]
			public FormSettingsBaseSettings FormSettingsBaseSettings {
				get { return this.formSettingsBaseSettings; }
				set { this.formSettingsBaseSettings.ApplySettings(value); }
			}
			private FormSettingsBaseSettings formSettingsBaseSettings;

			[Category("表示")]
			[DisplayName("最前面")]
			[Description("フォームを最前面に表示します．")]
			[DefaultValue(false)]
			public bool TopmostForm {
				get { return this.topmost;}
				set {
					this.topmost = value;
					if (this.HasOwner) {
						this.owner.TopMost = value;
						this.owner.tsmiViewTopmost.Checked = value;
					}
				}
			}
			private bool topmost = false;

			[Category("表示")]
			[DisplayName("非アクティブ時にUI非表示")]
			[Description("プレーヤフォームがアクティブでないときにUIを非表示にします．")]
			[DefaultValue(false)]
			public bool HideUiOnDeactivatedEnabled {
				get { return this.hideUiOnDeactivatedEnabled; }
				set {
					this.hideUiOnDeactivatedEnabled = value;
					if(this.HasOwner) this.owner.tsmiViewAutoHide.Checked = value;
				}
			}
			private bool hideUiOnDeactivatedEnabled = false;

			[Category("表示")]
			[DisplayName("表示領域にあわせて拡大")]
			[Description("動画解像度が表示領域よりも小さい場合に動画を拡大します．")]
			[DefaultValue(false)]
			public bool StrechToFitEnabled {
				get {
					if (this.HasOwner) return this.owner.wmpMain.stretchToFit;
					else return this.strechToFitEnabled;
				}
				set {
					if (this.HasOwner) this.owner.wmpMain.stretchToFit = value;
					else this.strechToFitEnabled = value;
					if (this.HasOwner) this.owner.wmpMain.stretchToFit = value;
				}
			}
			private bool strechToFitEnabled = false;

			[Category("表示")]
			[DisplayName("スクリーンセーバ抑止")]
			[Description("本アプリケーションがアクティブのときにスクリーンセーバが立ち上がらないようにします．")]
			[DefaultValue(false)]
			public bool SuspendScreenSaveEnabled {
				get {
					if (this.HasOwner) return this.owner.SuspendScreenSaveEnabled;
					else return this.suspendScreenSaveEnabled;
				}
				set {
					if (this.HasOwner) this.owner.SuspendScreenSaveEnabled = value;
					else this.suspendScreenSaveEnabled = value;
				}
			}
			private bool suspendScreenSaveEnabled = false;

			[Category("自動リサイズ")]
			[DisplayName("本編で自動リサイズ")]
			[Description("本編の動画が始まったときにウィンドウのサイズを動画の解像度に合わせます．")]
			[DefaultValue(true)]
			public bool AutoSizeOnNormalEnabled {
				get { return this.autoSizeOnNormalEnabled; }
				set { this.autoSizeOnNormalEnabled = value; }
			}
			private bool autoSizeOnNormalEnabled = true;

			[Category("自動リサイズ")]
			[DisplayName("CFで自動リサイズ")]
			[Description("CFの動画が始まったときにウィンドウのサイズを動画の解像度に合わせます．")]
			[DefaultValue(true)]
			public bool AutoSizeOnCfEnabled {
				get { return this.autoSizeOnCfEnabled; }
				set { this.autoSizeOnCfEnabled = value; }
			}
			private bool autoSizeOnCfEnabled = true;

			[Category("自動リサイズ")]
			[DisplayName("自動リサイズの倍率")]
			[Description("ウィンドウの自動リサイズを行うときの動画解像度に対する倍率をパーセント値で指定します．" + PlayerFormSettings.ZoomRangeMessage)]
			[DefaultValue(100)]
			public int ZoomRatioOnResize {
				get { return this.zoomRatioOnResize; }
				set {
					if (value < PlayerFormSettings.ZoomMin || PlayerFormSettings.ZoomMax < value) {
						throw new ArgumentOutOfRangeException("ZoomRatioOnResize");
					}
					this.zoomRatioOnResize = value;
				}
			}
			private int zoomRatioOnResize = 100;

			[Category("再生")]
			[DisplayName("再生終了でリストから削除")]
			[Description("再生の終了したコンテンツをプレイリストから削除します．")]
			[DefaultValue(false)]
			public bool RemovePlayedContentEnabled {
				get { return this.removePlayedContentEnabled; }
				set { this.removePlayedContentEnabled = value; }
			}
			private bool removePlayedContentEnabled = false;

			[Category("再生")]
			[DisplayName("krmの早期スキップ")]
			[Description("krmを再生直後にスキップすることでステーションコールまでの待ち時間を短くします．")]
			[DefaultValue(true)]
			public bool SkipKrmEnabled {
				get { return this.skipKrmEnabled; }
				set { this.skipKrmEnabled = value; }
			}
			private bool skipKrmEnabled = true;

			[Category("再生")]
			[DisplayName("最初からチャプタモード")]
			[Description("再生開始時から常にチャプタモードで再生を開始します．")]
			[DefaultValue(false)]
			public bool ChapterModeFromBegining {
				get { return this.chapterModeFromBegining; }
				set { this.chapterModeFromBegining = value; }
			}
			private bool chapterModeFromBegining = false;

			[Category("自動音量調整")]
			[DisplayName("自動音量調整")]
			[Description("本編やCFに切り替わったときに音量を自動的に調節します．設定の如何によらずミュートの時には音量調整は行いません．")]
			[DefaultValue(true)]
			public bool AutoVolumeEnabled {
				get { return this.autoVolumeEnabled; }
				set { this.autoVolumeEnabled = value; }
			}
			private bool autoVolumeEnabled = true;

			[Category("自動音量調整")]
			[DisplayName("本編の音量")]
			[Description("自動音量調節を有効にしている場合で，本編に切り替わったときに音量を設定します．" + PlayerFormSettings.VolumeRangeMessage)]
			[DefaultValue(100)]
			public int VolumeNormal {
				get { return this.volumeNormal; }
				set {
					if (value < PlayerFormSettings.VolumeMin || PlayerFormSettings.VolumeMax < value) {
						throw new ArgumentOutOfRangeException("VolumeNormal");
					}
					this.volumeNormal = value;
				}
			}
			private int volumeNormal = 100;

			[Category("自動音量調整")]
			[DisplayName("CFの音量")]
			[Description("自動音量調節を有効にしている場合で，CFに切り替わったときに音量を設定します．" + PlayerFormSettings.VolumeRangeMessage)]
			[DefaultValue(20)]
			public int VolumeCf {
				get { return this.volumeCf; }
				set {
					if (value < PlayerFormSettings.VolumeMin || PlayerFormSettings.VolumeMax < value) {
						throw new ArgumentOutOfRangeException("VolumeCf");
					}
					this.volumeCf = value;
				}
			}
			private int volumeCf = 20;
			
			#region INewSettings<PlayerFormSettings> Members
			public void ApplySettings(PlayerFormSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}
		
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
		private static readonly Regex regexDartTag = new Regex(":dartTag=([^:=]*)");
		private static readonly Regex regexClipNo = new Regex(":clipNo=([^:]*)");
		private static readonly Regex regexClipBegin = new Regex(":clipBegin=([^:]*)");
		
		private static PlayerForm instance = null;
		public static PlayerForm Instance {
			get {
				if(! PlayerForm.HasInstance) {
					PlayerForm.instance = new PlayerForm();
				}
				return PlayerForm.instance;
			}
		}
		public static bool HasInstance {
			get { return null != PlayerForm.instance && !PlayerForm.instance.IsDisposed; }
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

		private ScreenSaveListener ssl;

		private PlayerFormSettings settings;
		
		private PlayerForm() {
			InitializeComponent();
			this.tsmiViewFullScreen.ShortcutKeys = Keys.Alt | Keys.Enter;
			
			this.tsmiSettings.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
		}

		private void OpenVideo() {
			if(null == this.CurrentContent) {
				this.wmpMain.close();
				this.wmpMain.currentMedia = null;
				return;
			}
			IWMPMedia media;
			if(this.CurrentChapter.HasValue) {
				media = this.wmpMain.newMedia(this.CurrentContent.ChapterPlaylistUriOf(this.CurrentChapter.Value).AbsoluteUri);
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
		private void UpdateStatusbatText() {
			if(null != this.CurrentContent) {
				IWMPMedia curMedia = this.wmpMain.currentMedia;
				string entryUrl;
				Match matchClipNo = null;
				Match matchClipBegin = null;
				if (this.currentAttribs.TryGetValue(PlayerForm.AttribNameEntryUrl, out entryUrl)) {
					matchClipNo = PlayerForm.regexClipNo.Match(entryUrl);
					matchClipBegin = PlayerForm.regexClipBegin.Match(entryUrl);
				}

				this.tsslId.Text = this.CurrentContent.ContentId;
				this.tsslClipInfo.Text = 
					((null != matchClipNo && matchClipNo.Success) ?
						"clipNo="+matchClipNo.Groups[1].Value : string.Empty)
					+ ((null != matchClipBegin && matchClipBegin.Success) ?
						"&&clipBegin=" + matchClipBegin.Groups[1].Value : string.Empty);
				this.tsslChapter.Text = this.CurrentChapter.HasValue ?
					"チャプタ" + this.CurrentChapter.Value.ToString()
					: "通常";
				this.tsslDuration.Text = new TimeSpan((long)(10000000 * curMedia.duration)).ToString();
				this.tsslSize.Text = curMedia.imageSourceWidth.ToString() + "x" + curMedia.imageSourceHeight.ToString();
				this.tsslTitle.Text = curMedia.name;
			}
		}
		private void ModifyVolume() {
			if(this.IsCf) {
				this.wmpMain.settings.volume = this.settings.VolumeCf;
			}else if(this.IsMain){
				this.wmpMain.settings.volume = this.settings.VolumeNormal;
			}
		}
		private void HideUi() {
			//WMPの座標計算
			Point loc = this.PointToClient(this.splitContainer1.PointToScreen(new Point()));
			loc.X += SystemInformation.SizingBorderWidth;
			loc.Y += SystemInformation.SizingBorderWidth + SystemInformation.CaptionHeight;
			loc += PlayerForm.WmpMarginSize;
			//WMPのビデオ部分のサイズ計算
			Size size = this.splitContainer1.ClientSize;
			size -= PlayerForm.WmpUiSize;
			//リージョン設定
			DRegion oldRegion = this.Region;
			this.Region = new Region(new Rectangle(loc, size));
			if(null != oldRegion) {
				oldRegion.Dispose();
			}
		}
		private void ShowUi() {
			if(null != this.Region) {
				DRegion oldRegion = this.Region;
				this.Region = null;
				oldRegion.Dispose();
			}
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
				this.currentChapter = this.settings.ChapterModeFromBegining ? 1 : (int?)null;
				
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
					this.OpenVideo();
				}
			}
		}
		private bool IsKrm {
			get {
				return this.wmpMain.currentMedia.sourceURL.Contains("/krm");
			}
		}
		private bool IsCf {
			get {
				string entryUrl = this.currentAttribs[PlayerForm.AttribNameEntryUrl];
				return entryUrl.StartsWith("Adv:");
			}
		}
		private bool IsMain {
			get {
				string entryUrl = this.currentAttribs[PlayerForm.AttribNameEntryUrl];
				return entryUrl.StartsWith("Cnt:");
			}
		}
		private bool IsLastEntry {
			get {
				string attrib = this.currentAttribs["IsLastEntry"];
				return "1".Equals(attrib);
			}
		}
		private bool SuspendScreenSaveEnabled {
			get { return this.ssl.Enabled; }
			set { this.ssl.Enabled = value; }
		}

		private void PlayerForm_Load(object sender, EventArgs e) {
			this.ssl = new ScreenSaveListener();
			this.ssl.ScreenSaverRaising += this.ssl_ScreenSaverRaising;
			this.FormClosed += delegate {
				this.ssl.ScreenSaverRaising -= this.ssl_ScreenSaverRaising;
				this.ssl.Dispose();
			};
			
			this.settings = new PlayerFormSettings(this);
			Utility.LoadSettingsAndEnableSaveOnClosedNew(this);
			
			this.tspgPlayerFormSettings.SelectedObject = this.settings;
		}
		private void PlayerForm_FormClosing(object sender, FormClosingEventArgs e) {
			this.CurrentContent = null;
		}
		private void PlayerForm_Activated(object sender, EventArgs e) {
			this.ShowUi();
		}
		private void PlayerForm_Deactivate(object sender, EventArgs e) {
			if(this.settings.HideUiOnDeactivatedEnabled) {
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
		
		private void ssl_ScreenSaverRaising(object sender, CancelEventArgs e) {
			if (this.settings.SuspendScreenSaveEnabled) {
				e.Cancel = true;
			}
		}
		#region メインメニュー
		private void tsmiPlayChapter_Click(object sender, EventArgs e) {
			string title =  "特定のチャプタから再生";
			this.inputBoxDialog1.Title =title;
			this.inputBoxDialog1.Message = "チャプタ番号の入力．空白の場合は通常再生．";
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
		private void tsmiViewFullScreen_Click(object sender, EventArgs e) {
			try {
				this.wmpMain.fullScreen = true;
			} catch {
			}
		}
		private void tsmiViewTopmost_Click(object sender, EventArgs e) {
			this.settings.TopmostForm = !this.settings.TopmostForm;
		}
		private void tsmiViewAutoHide_Click(object sender, EventArgs e) {
			this.settings.HideUiOnDeactivatedEnabled = !this.settings.HideUiOnDeactivatedEnabled;
		}
		private void tsmiPrevContent_Click(object sender, EventArgs e) {
			ContentAdapter prevCont = PlayList.Instance.PrevContentOf(this.CurrentContent);
			if (null == prevCont) {
				prevCont = PlayList.Instance.PrevContentOf(null);
			}
			this.CurrentContent = prevCont;
		}
		private void tsmiResizeToVideoResolution_Click(object sender, EventArgs e) {
			this.WindowState = FormWindowState.Normal;
			Size videoSize = new Size(this.wmpMain.currentMedia.imageSourceWidth, this.wmpMain.currentMedia.imageSourceHeight);
			Size distSize = new Size(videoSize.Width*this.settings.ZoomRatioOnResize/100, videoSize.Height*this.settings.ZoomRatioOnResize/100);
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
		private void tsnfmiNgFav_SubmenuSelected(object sender, ContentSelectionRequiredEventArgs e) {
			e.Selection = new ContentAdapter[] { this.CurrentContent };
		}
		private void tsucmiCommand_UserCommandSelected(object sender, UserCommandSelectedEventArgs e) {
			if (null != this.CurrentContent) {
				e.UserCommand.Execute(new ContentAdapter[] { this.CurrentContent });
			}
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
					if (this.settings.AutoVolumeEnabled && !this.wmpMain.settings.mute) {
						//一旦音量を代入
						this.ModifyVolume();
						//音量が変わらないことがあるのでタイマーで後から再代入
						this.timerAutoVolume.Start();
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
					if (this.settings.SkipKrmEnabled && this.IsKrm) {
						this.timerSkipKrm.Start();
					}
					//ステータスバー更新
					this.UpdateStatusbatText();
					//リサイズ
					if(FormWindowState.Normal == this.WindowState) {
						if(this.IsCf) {
							if(this.settings.AutoSizeOnCfEnabled) {
								this.tsmiResizeToVideoResolution.PerformClick();
							}
						} else if(this.IsMain){
							if(this.settings.AutoSizeOnNormalEnabled) {
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
					if (this.IsLastEntry) {
						if (this.CurrentChapter.HasValue) {
							//チャプタモードなら次のチャプタ
							this.CurrentChapter++;
						} else {
							//次のコンテンツに推移
							if (this.settings.RemovePlayedContentEnabled) {
								this.tsmiNextContentWithDelete.PerformClick();
							} else {
								this.tsmiNextContent.PerformClick();
							}
						}
					}
					break;
			}
		}
		#endregion

		private void tsmiSettings_DropDownOpened(object sender, EventArgs e) {
			this.tspgPlayerFormSettings.RefreshPropertyGrid();
		}
		
		private void timerAutoVolume_Tick(object sender, EventArgs e) {
			this.timerAutoVolume.Stop();
			this.ModifyVolume();
		}
		private void timerSkipKrm_Tick(object sender, EventArgs e) {
			this.timerSkipKrm.Stop();
			this.tsmiNextTrack.PerformClick();
		}

		#region IHasNewSettings<PlayerFormSettings> Members
		public PlayerFormSettings Settings {
			get { return this.settings; }
		}
		#endregion

	}

#if false
	public enum PlayMode {
		Playlist,
		SingleRepeat,
		Shuffle,
	}
#endif
}
