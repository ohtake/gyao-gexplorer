using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using AxWMPLib;
using WMPLib;
using System.Text;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer {
	partial class PlayerForm : FormSettingsBase, IFormWithSettings<PlayerFormSettings>{
		private const string AttribNameEntryUrl = "WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL";
		private const int VolumeMax = 100;
		private const int VolumeMin = 0;
		private const long OrdMax = 10000000000000000;
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
		private bool fullScreenAtPrev = false;
		
		private int volumeNormal = 100;
		private int volumeCf = 20;

		private Random rand = new Random();

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
			settings.KeepFullScreenEnabled = this.KeepFullScreenEnabled;
			settings.AutoVolumeEnabled = this.AutoVolumeEnabled;
			settings.MediaKeysEnabled = this.MediaKeysEnabled;
			settings.RemovePlayedContentEnabled = this.RemovePlayedContentEnabled;
			settings.PlayListLoopEnabled = this.PlayListLoopEnabled;
			settings.MainTabIndex = this.tabControl1.SelectedIndex;
			settings.VolumeNormal = this.VolumeNormal;
			settings.VolumeCf = this.VolumeCf;
		}

		public void ApplySettings(PlayerFormSettings settings) {
			base.ApplySettings(settings);
			this.KeepFullScreenEnabled = settings.KeepFullScreenEnabled ?? this.KeepFullScreenEnabled;
			this.AutoVolumeEnabled = settings.AutoVolumeEnabled ?? this.AutoVolumeEnabled;
			this.MediaKeysEnabled = settings.MediaKeysEnabled ?? this.MediaKeysEnabled;
			this.RemovePlayedContentEnabled = settings.RemovePlayedContentEnabled ?? this.RemovePlayedContentEnabled;
			this.PlayListLoopEnabled = settings.PlayListLoopEnabled ?? this.PlayListLoopEnabled;
			this.tabControl1.SelectedIndex = settings.MainTabIndex ?? this.tabControl1.SelectedIndex;
			this.VolumeNormal = settings.VolumeNormal ?? this.VolumeNormal;
			this.VolumeCf = settings.VolumeCf ?? this.VolumeCf;
			
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
				this.currentChapter = null;
				
				if (null == value) {
					Utility.SetTitlebarText(this, "PlayerForm");
					this.wmpMain.close();
					Uri blankUri = new Uri("about:blank");
					this.gwbDetail.Url = blankUri;
					this.gwbRecommend.Url = blankUri;
				} else {
					Utility.SetTitlebarText(this, value.DisplayName);
					this.wmpMain.URL = value.MediaFileUri.AbsoluteUri;
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
					if (value.HasValue) {
						this.wmpMain.URL = this.CurrentContent.ChapterMediaFileUriOf(value.Value).AbsoluteUri;
					} else {
						this.wmpMain.URL = this.CurrentContent.MediaFileUri.AbsoluteUri;
					}
				}
			}
		}
		public bool KeepFullScreenEnabled {
			get { return this.tsmiKeepFullScreen.Checked; }
			set { this.tsmiKeepFullScreen.Checked = value; }
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
		public bool PlayListLoopEnabled {
			get { return this.tsmiLoopPlayList.Checked; }
			set { this.tsmiLoopPlayList.Checked = value; }
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
		private long GetRandomOrd() {
			return (long)(this.rand.NextDouble() * PlayerForm.OrdMax);
		}

		private void PlayerForm_Load(object sender, EventArgs e) {
			Utility.AppendHelpMenu(this.menuStrip1);
			this.tsmiSettings.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;

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
		private void tsmiPlayChapter_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "特定のチャプターから再生";
			this.inputBoxDialog1.Message = "チャプター番号の入力．空白の場合は通常再生．";
			this.inputBoxDialog1.Input = this.CurrentChapter.HasValue ? this.CurrentChapter.Value.ToString() : string.Empty;
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					int? chapter = null;
					string input = this.inputBoxDialog1.Input;
					if (! string.IsNullOrEmpty(input)) {
						chapter = int.Parse(input);
					}
					this.CurrentChapter = chapter;
					break;
			}
		}
		private void tsmiReload_Click(object sender, EventArgs e) {
			if (this.CurrentChapter.HasValue) {
				this.wmpMain.URL = this.CurrentContent.ChapterMediaFileUriOf(this.CurrentChapter.Value).AbsoluteUri;
			} else {
				this.wmpMain.URL = this.CurrentContent.MediaFileUri.AbsoluteUri;
			}
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
		private void tsmiVolumeNormal_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "自動音量調整における本編の音量";
			this.inputBoxDialog1.Message = "本編の音量を入力してください．" + "[" + PlayerForm.VolumeMin.ToString() + "-" + PlayerForm.VolumeMax.ToString() + "]";
			this.inputBoxDialog1.Input = this.VolumeNormal.ToString();
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					this.VolumeNormal = int.Parse(this.inputBoxDialog1.Input);
					break;
			}
		}
		private void tsmiVolumeCf_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "自動音量調整におけるCFの音量";
			this.inputBoxDialog1.Message = "CFの音量を入力してください．" + "[" + PlayerForm.VolumeMin.ToString() + "-" + PlayerForm.VolumeMax.ToString() + "]";
			this.inputBoxDialog1.Input = this.VolumeCf.ToString();
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					this.VolumeCf = int.Parse(this.inputBoxDialog1.Input);
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
						string dartTag = dartTagMatch.Groups[1].Value;
						string ordStr = this.GetRandomOrd().ToString();
						
						this.splitContainer1.Panel1Collapsed = false;
						this.pboxBanner.LoadAsync("http://ad.jp.doubleclick.net/ad/gyao.vision.spot/" + dartTag + ";sz=468x60;ord=" + ordStr + "?");
						this.pboxBanner.Tag = "http://ad.jp.doubleclick.net/jump/gyao.vision.spot/" + dartTag + ";sz=468x60;ord=" + ordStr + "?";
					} else {
						this.splitContainer1.Panel1Collapsed = true;
						this.pboxBanner.Image = null;
						this.pboxBanner.Tag = null;
					}
					break;
			}
		}
		private void wmpMain_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e) {
			switch((WMPPlayState)e.newState){
				case WMPPlayState.wmppsMediaEnded:
					this.fullScreenAtPrev = this.wmpMain.fullScreen;
					Thread t = new Thread(new ThreadStart(delegate {
						Thread.Sleep(100);//適当すぎ
						if (!this.IsDisposed && !this.wmpMain.IsDisposed) {
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
						}
					}));
					t.Start();
					break;
				case WMPPlayState.wmppsPlaying:
					if (this.KeepFullScreenEnabled && this.fullScreenAtPrev) {
						this.wmpMain.fullScreen = true;
						this.fullScreenAtPrev = false;
					}
					break;
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
			string jumpUri = this.pboxBanner.Tag as string;
			if (!string.IsNullOrEmpty(jumpUri)) {
				Utility.Browse(new Uri(jumpUri));
			}
		}
	}

	public class PlayerFormSettings : FormSettingsBaseSettings {
		private bool? keepFullScreenEnabled;
		private bool? autoVolumeEnabled;
		private bool? mediaKeysEnabled;
		private bool? removePlayedContentEnabled;
		private bool? playListLoopEnabled;
		private int? mainTabIndex;
		private int? volumeNormal;
		private int? volumeCf;

		public bool? KeepFullScreenEnabled {
			get { return this.keepFullScreenEnabled; }
			set { this.keepFullScreenEnabled = value; }
		}
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
		public bool? PlayListLoopEnabled {
			get { return this.playListLoopEnabled; }
			set { this.playListLoopEnabled = value; }
		}
		public int? MainTabIndex {
			get { return this.mainTabIndex; }
			set { this.mainTabIndex = value; }
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
