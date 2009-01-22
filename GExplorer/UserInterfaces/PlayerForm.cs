using System;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using AxWMPLib;
using WMPLib;
using Yusen.GExplorer.Utilities;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.AppCore;
using Marshal = System.Runtime.InteropServices.Marshal;
using DRegion = System.Drawing.Region;
using System.Net.Cache;

namespace Yusen.GExplorer.UserInterfaces {
	public sealed partial class PlayerForm : BaseForm, INotifyPropertyChanged, IPlayerFormBindingContract {
		private const string StreamingServerName = "wms.cd.gyao.jp";
		
		private sealed class PageReaderWithCookie {
			private static readonly Encoding PageEncoding = Encoding.GetEncoding("Shift_JIS");
			
			private readonly CookieContainer cc;
			private readonly int timeout;
			
			public PageReaderWithCookie(CookieContainer cc, int timeout) {
				this.cc = cc;
				this.timeout = timeout;
			}
			
			public string GetResponseText(Uri uri) {
				HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
				req.CookieContainer = this.cc;
				req.Timeout = this.timeout;
				
				using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
				using (Stream stream = res.GetResponseStream())
				using (StreamReader reader = new StreamReader(stream, PageReaderWithCookie.PageEncoding)) {
					return reader.ReadToEnd();
				}
			}
			public void DownloadToFile(Uri uri, string filename) {
				HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
				req.CookieContainer = this.cc;
				
				using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
				using (Stream stream = res.GetResponseStream())
				using (FileStream dest = File.OpenWrite(filename)){
					dest.SetLength(0);
					byte[] buf = new byte[1024];
					while(true){
						int len = stream.Read(buf, 0, buf.Length);
						if (0 == len) break;
						dest.Write(buf, 0, len);
					}
				}
			}
		}

		private const string AttribNameEntryUrl = "WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL";
		private const string AdultErrorString = @"<a href=""/sityou/adult_error.php"" target=""_self""><img src=""/sityou/img/bt_no.gif"" width=""186"" height=""38"" border=""0""></a>";
		private const string LastCallTitle = "ラストコール";
		private const long OrdMax = 10000000000000000;
		
		private static readonly Size WmpUiSize = new Size(0, 64);
		private static readonly Size WmpMarginSize = new Size(3, 3);
		private static readonly Regex regexDartTag = new Regex(":dartTag=([^:=]*)");
		private static readonly Regex regexClipNo = new Regex(":clipNo=([^:]*)");
		private static readonly Regex regexClipBegin = new Regex(":clipBegin=([^:]*)");
		private static readonly Regex regexAsxPhp = new Regex(@"http://www\.gyao\.jp/sityou/asx\.php\?[^""]+");
		private static readonly Regex regexBannerKeyValue = new Regex(@"var\s+keyValue\s*=\s*""(.+?)""");
		private static readonly Regex regexPlayerRedirection = new Regex(@"""(http://www\.gyao\.jp/sityou/movie/[^""]{0,200}?)""");
		private static readonly string tempAsxFilename = "AsxPhp.asx";
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		private Playlist currentPlaylist = null;
		private GContentClass currentContent = null;
		private int? currentChapter = null;
		
		private Dictionary<string, string> currentAttribs = new Dictionary<string, string>();
		private string bannerKeyValue = string.Empty;
		private bool isAlwaysBottomMost = false;
		private bool hideUiOnDeactivatedEnabled = false;

		private PlayerFormOptions options;
		private readonly ScreenSaveListener ssl = new ScreenSaveListener();
		private readonly Random rand = new Random();
		
		public PlayerForm() {
			InitializeComponent();
			
			this.tsmiViewFullScreen.ShortcutKeys = Keys.Alt | Keys.Enter;
		}
		#region プレイリストや再生に関するコアの部分
		public void PlayContent(GContentClass cont, Playlist playlist) {
			Program.PlaylistsManager.SetCurrentContentAndPlaylist(cont, playlist);
			
			this.AttachToPlaylist(playlist);
			if (null != playlist) {
				this.playlistsView1.SelectPlaylist(playlist);
			}
			
			this.currentContent = cont;
			this.currentChapter = this.options.ChapterModeFromBegining ? 1 : (int?)null;
			
			if (null == this.currentContent) {
				this.tsddbPlaylist.Text = "プレイリスト";
			} else {
				this.tsddbPlaylist.Text = this.currentContent.ContentId;
			}
			
			this.tspmddbMode.ClearClipInfo();
			this.UpdateTitlebarText();
			this.UpdateEnabilityDependingOnContentPlaylistPossesstion();
			this.OpenVideo();
		}
		private void currentPlaylist_PlaylistChanged(object sender, EventArgs e) {
			this.UpdateTitlebarText();
		}
		private void currentPlaylist_PlaylistDestroyed(object sender, EventArgs e) {
			this.DetachFromPlaylist();
			this.UpdateEnabilityDependingOnContentPlaylistPossesstion();
			this.UpdateTitlebarText();
		}
		private void AttachToPlaylist(Playlist pl) {
			if (null != this.currentPlaylist) {
				this.currentPlaylist.PlaylistDestroyed -= this.currentPlaylist_PlaylistDestroyed;
				this.currentPlaylist = null;
			}
			this.currentPlaylist = pl;
			if (null != this.currentPlaylist) {
				this.currentPlaylist.PlaylistDestroyed += this.currentPlaylist_PlaylistDestroyed;
				this.currentPlaylist.PlaylistChanged += currentPlaylist_PlaylistChanged;
			}
			this.UpdateTitlebarText();
		}
		private void DetachFromPlaylist() {
			if (null != this.currentPlaylist) {
				this.currentPlaylist.PlaylistDestroyed -= this.currentPlaylist_PlaylistDestroyed;
				this.currentPlaylist.PlaylistChanged -= currentPlaylist_PlaylistChanged;
				this.currentPlaylist = null;
			}
		}
		
		private void OpenVideo() {
			if (null == this.currentContent) {
				this.wmpMain.close();
				this.wmpMain.currentPlaylist.clear();
				return;
			}

			if (WMPPlayState.wmppsMediaEnded != this.wmpMain.playState) {
				//手動で切り替えた場合ではいったん再生停止
				this.wmpMain.close();
				this.wmpMain.currentPlaylist.clear();
				//プレイリストも非表示
				this.HidePlaylist();
				Application.DoEvents();
			}
			
			PageReaderWithCookie pageReader = new PageReaderWithCookie(Program.CookieContainer, this.options.PlaylistTimeout);
			
			//再生ページ取得
			Uri playerPageUri = this.currentContent.PlayerLargeUri;
			Application.DoEvents();
			string body = pageReader.GetResponseText(playerPageUri);
			Application.DoEvents();
			
			//年齢チェック
			int? age = AdultUtility.FindAdultThresholdInContent(body);
			if (age.HasValue) {
				if (!this.options.DisableAdultCheckDialog) {
					//ダイアログ表示前に停止
					this.wmpMain.close();
					this.wmpMain.currentPlaylist.clear();
					switch (MessageBox.Show(this, string.Format("あなたは{0}才以上ですか？", age.Value), "年齢制限", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
						case DialogResult.Yes:
							break;
						default:
							this.PlayContent(null, this.currentPlaylist);
							return;
					}
				}
				Uri playerPage2Uri = new Uri(playerPageUri.AbsoluteUri + "&check_flg=0");
				Application.DoEvents();
				body = pageReader.GetResponseText(playerPage2Uri);
				Application.DoEvents();
			}
			
			//asx.phpを検索
			bool retryingAutobahn = false;
		retryAutobahn:
			Match matchAsxPhp = PlayerForm.regexAsxPhp.Match(body);
			if (!retryingAutobahn && !matchAsxPhp.Success) {
				// Autobahn の可能性
				retryingAutobahn = true;
				Match matchRedirection = PlayerForm.regexPlayerRedirection.Match(body);
				if (matchRedirection.Success) {
					body = pageReader.GetResponseText(new Uri(matchRedirection.Groups[1].Value));
					Application.DoEvents();
					goto retryAutobahn;
				} else {
					throw new Exception("asx.php のアドレスを取得できず Autobahn 後の遷移先も見つからなかった．");
				}
			} else if (!matchAsxPhp.Success) {
				throw new Exception("Autobahn 後の遷移先を見つけたが asx.php のアドレスを取得できなかった．");
			}
			string asxPhpAddr = matchAsxPhp.Value;
			//チャプタ
			if (this.currentChapter.HasValue) {
				asxPhpAddr += "&chapterNo=" + this.currentChapter.Value.ToString();
			}
			
			// asx をダウンロード
			string tempFile = Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.Temp) , PlayerForm.tempAsxFilename);
			Application.DoEvents();
			pageReader.DownloadToFile(new Uri(asxPhpAddr), tempFile);
			Application.DoEvents();
			
			//名前解決ができない不具合対策用
			if (!string.IsNullOrEmpty(this.options.StreamingServerAltAddress)) {
				string altAddr = this.options.StreamingServerAltAddress;
				string asxText;
				using (StreamReader reader = new StreamReader(tempFile)) {
					asxText = reader.ReadToEnd();
				}
				using (StreamWriter writer = new StreamWriter(tempFile)) {
					asxText = asxText.Replace(PlayerForm.StreamingServerName, altAddr);
					writer.Write(asxText);
				}
			}
			
			// WMP に読み込ませる
			Application.DoEvents();
			IWMPPlaylist plist = this.wmpMain.newPlaylist(string.Format("<{0}> {1} | {2} | {3}", this.currentContent.ContentId, this.currentContent.Title, this.currentContent.SeriesNumber, this.currentContent.Subtitle), tempFile);
			Application.DoEvents();
			
			if (WMPPlayState.wmppsMediaEnded != this.wmpMain.playState) {
				//手動で切り替えた場合では強制的に再生させる
				this.wmpMain.currentPlaylist = plist;
				this.wmpMain.Ctlcontrols.play();
			} else {
				//コンテンツ末尾に達した場合はリストの末尾にmedia追加
				for (int i = 0; i < plist.count; i++) {
					this.wmpMain.currentPlaylist.appendItem(plist.get_Item(i));
				}
			}
			
			this.currentAttribs.Clear();
			this.UpdateStatusbarText();

			Match matchBannerKeyValue = PlayerForm.regexBannerKeyValue.Match(body);
			if (!matchBannerKeyValue.Success) {
				throw new Exception("再生ページからバナー用の個人情報を取得できなかった．");
			}
			this.bannerKeyValue = matchBannerKeyValue.Groups[1].Value;
		}
		#endregion
		private void UpdateEnabilityDependingOnContentPlaylistPossesstion() {
			bool hasContent = (null != this.currentContent);
			bool hasPlaylist = (null != this.currentPlaylist);
			bool hasBoth = hasContent && hasPlaylist;

			this.tsmiReload.Enabled = hasContent;
			this.tsmiRemoveAndClose.Enabled = hasBoth;
			this.tsmiNextContent.Enabled = hasPlaylist;
			this.tsmiPrevContent.Enabled = hasPlaylist;
			this.tsmiNextContentWithDelete.Enabled = hasBoth;
			this.tsmiBrowseDetail.Enabled = hasContent;
			this.tsmiBrowseRecommended.Enabled = hasContent;
			this.tsmiBrowseReviewList.Enabled = hasContent;
			this.tsmiBrowseReviewInput.Enabled = hasContent;
		}
		private void ModifyVolume() {
			if(this.IsCm) {
				this.wmpMain.settings.volume = this.options.AutoVolumeCmVolume;
			}else if(this.IsMain){
				this.wmpMain.settings.volume = this.options.AutoVolumeMainVolume;
			}
		}
		private void HideUi() {
			//WMPの座標計算
			Point loc = this.PointToClient(this.splitContainer2.PointToScreen(new Point()));
			loc.X += SystemInformation.SizingBorderWidth;
			loc.Y += SystemInformation.SizingBorderWidth + SystemInformation.CaptionHeight;
			loc += PlayerForm.WmpMarginSize;
			//WMPのビデオ部分のサイズ計算
			Size size = this.splitContainer2.ClientSize;
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
		private Uri CreateBannerUri(string dartTag) {
			long ord = (long)(this.rand.NextDouble() * PlayerForm.OrdMax);
			return new Uri(
				"http://www1.gyao.jp/html.ng/site=gyao.cm.widesky"
				+ this.bannerKeyValue
				+ "&zone=" + dartTag
				+ "&ord=" + ord.ToString() + "?");
		}

		private void UpdateTitlebarText() {
			if (null == this.currentContent) {
				if (null == this.currentPlaylist) {
					this.Text = "PlayerForm";
				} else {
					this.Text = string.Format("[{0}]", this.currentPlaylist.Name);
				}
			} else {
				string contentName = string.Format("{0} | {1} | {2}", this.currentContent.Title, this.currentContent.SeriesNumber, this.currentContent.Subtitle);
				if (null == this.currentPlaylist) {
					this.Text = contentName;
				} else {
					this.Text = string.Format("[{0}] {1}", this.currentPlaylist.Name, contentName);
				}
			}
		}
		
		private bool IsGyaoCm {
			get {
				return this.wmpMain.currentMedia.sourceURL.Contains("/gyaocm");
			}
		}
		private bool IsCm {
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

		private void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (null != handler) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#region PlayerFormのイベントハンドラ
		private void PlayerForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			this.ssl.ScreenSaverRaising += this.ssl_ScreenSaverRaising;
			this.FormClosed += delegate {
				this.ssl.ScreenSaverRaising -= this.ssl_ScreenSaverRaising;
				this.ssl.Dispose();
			};
			
			if (null == Program.RootOptions) return;
			this.options = Program.RootOptions.PlayerFormOptions;
			this.options.ApplyFormBaseOptionsAndTrackValues(this);
			BindingContractUtility.BindAllProperties<PlayerForm, IPlayerFormBindingContract>(this, this.options);
			this.playlistsView1.BindToOptions(this.options.PlaylistsViewOptions);
		}
		private void PlayerForm_FormClosing(object sender, FormClosingEventArgs e) {
			this.wmpMain.close();
			this.DetachFromPlaylist();
			Program.PlaylistsManager.SetCurrentContentAndPlaylist(null, null);
		}
		private void PlayerForm_Activated(object sender, EventArgs e) {
			this.ShowUi();
		}
		private void PlayerForm_Deactivate(object sender, EventArgs e) {
			this.HidePlaylist();
			if(this.HideUiOnDeactivatedEnabled) {
				this.HideUi();
			}
		}
		private void PlayerForm_Resize(object sender, EventArgs e) {
			this.HidePlaylist();
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
				case Keys.Escape:
					this.HidePlaylist();
					break;
				default:
					return;
			}
			e.Handled = true;
		}
		#endregion

		private void ssl_ScreenSaverRaising(object sender, CancelEventArgs e) {
			e.Cancel = true;
		}
		#region メインメニュー
		private void tsmiPlayChapter_Click(object sender, EventArgs e) {
			string title =  "特定のチャプタから再生";
			this.inputBoxDialog1.Title =title;
			this.inputBoxDialog1.Message = "チャプタ番号の入力．空文字列の場合は通常再生．";
			this.inputBoxDialog1.Input = this.currentChapter.HasValue ? this.currentChapter.Value.ToString() : string.Empty;
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
					this.currentChapter = chapter;
					this.OpenVideo();
					break;
			}
		}
		private void tsmiReload_Click(object sender, EventArgs e) {
			this.OpenVideo();
		}
		private void tsmiRemoveAndClose_Click(object sender, EventArgs e) {
			this.currentPlaylist.RemoveContent(this.currentContent);
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
			this.wmpMain.Ctlcontrols.fastReverse();
		}
		private void tsmiFastForward_Click(object sender, EventArgs e) {
			this.wmpMain.Ctlcontrols.fastForward();
		}
		private void tsmiRate_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "再生速度の指定";
			this.inputBoxDialog1.Message = "再生速度を小数で入力してください．";
			this.inputBoxDialog1.Input = this.wmpMain.settings.rate.ToString();
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					this.wmpMain.settings.rate = double.Parse(this.inputBoxDialog1.Input);
					break;
			}
		}
		private void tsmiPrevContent_Click(object sender, EventArgs e) {
			int curIdx = this.currentPlaylist.IndexOf(this.currentContent);
			if (curIdx < 0) {
				if (this.currentPlaylist.ContentCount > 0) {
					this.PlayContent(this.currentPlaylist[this.currentPlaylist.ContentCount - 1], this.currentPlaylist);
				} else {
					this.PlayContent(null, this.currentPlaylist);
				}
			}else if (curIdx > 0) {
				this.PlayContent(this.currentPlaylist[curIdx - 1], this.currentPlaylist);
			} else {
				this.PlayContent(null, this.currentPlaylist);
			}
		}
		private void tsmiNextContent_Click(object sender, EventArgs e) {
			int curIdx = this.currentPlaylist.IndexOf(this.currentContent);
			if (curIdx < 0) {
				if (this.currentPlaylist.ContentCount > 0) {
					this.PlayContent(this.currentPlaylist[0], this.currentPlaylist);
				} else {
					this.PlayContent(null, this.currentPlaylist);
				}
			}else if (curIdx+1 < this.currentPlaylist.ContentCount) {
				this.PlayContent(this.currentPlaylist[curIdx + 1], this.currentPlaylist);
			} else {
				this.PlayContent(null, this.currentPlaylist);
			}
		}
		private void tsmiNextContentWithDelete_Click(object sender, EventArgs e) {
			int curIdx = this.currentPlaylist.IndexOf(this.currentContent);
			if (curIdx < 0) {
				this.PlayContent(null, this.currentPlaylist);
			} else{
				this.currentPlaylist.RemoveAt(curIdx);
				if (this.currentPlaylist.ContentCount > curIdx) {
					this.PlayContent(this.currentPlaylist[curIdx], this.currentPlaylist);
				} else {
					this.PlayContent(null, this.currentPlaylist);
				}
			}
		}
		private void tsmiViewFullScreen_Click(object sender, EventArgs e) {
			try {
				this.wmpMain.fullScreen = true;
			} catch {
			}
		}
		private void tsmiViewTopmost_Click(object sender, EventArgs e) {
			this.IsAlwaysTopMost = !this.IsAlwaysTopMost;
		}
		private void tsmiViewBottomMost_Click(object sender, EventArgs e) {
			this.IsAlwaysBottomMost = !this.IsAlwaysBottomMost;
		}
		private void tsmiViewAutoHide_Click(object sender, EventArgs e) {
			this.HideUiOnDeactivatedEnabled = !this.HideUiOnDeactivatedEnabled;
		}
		private void tsmiResizeToVideoResolution_Click(object sender, EventArgs e) {
			this.WindowState = FormWindowState.Normal;
			Size videoSize = new Size(this.wmpMain.currentMedia.imageSourceWidth, this.wmpMain.currentMedia.imageSourceHeight);
			int ratio = this.options.ZoomRatioOnResize;
			Size distSize = new Size(videoSize.Width*ratio/100, videoSize.Height*ratio/100);
			
			if (Size.Empty == videoSize) return;
			
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
			Program.BrowsePage(this.currentContent.ContentDetailUri);
		}
		private void tsmiBrowseRecommended_Click(object sender, EventArgs e) {
			Program.BrowsePage(this.currentContent.RecommendationLargeUri);
		}
		private void tsmiBrowseReviewList_Click(object sender, EventArgs e) {
			Program.BrowsePage(this.currentContent.ReviewListUri);
		}
		private void tsmiBrowseReviewInput_Click(object sender, EventArgs e) {
			Program.BrowsePage(this.currentContent.ReviewInputUri);
		}
		private void tsmiShowOptionsForm_Click(object sender, EventArgs e) {
			Program.ShowOptionsForm();
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
					if (this.options.AutoVolumeEnabled && !this.wmpMain.settings.mute) {
						//一旦音量を代入
						this.ModifyVolume();
						//音量が変わらないことがあるのでタイマーで後から再代入
						this.timerAutoVolume.Start();
					}
					//バナー表示
					Match dartTagMatch = PlayerForm.regexDartTag.Match(entryUrl);
					if (dartTagMatch.Success && !string.IsNullOrEmpty(dartTagMatch.Groups[1].Value)) {
						string dartTag = dartTagMatch.Groups[1].Value;
						dartTag = dartTag.Split(new string[] { "AR" }, StringSplitOptions.None)[0];
						this.wbBanner.Navigate(this.CreateBannerUri(dartTag));
						this.splitContainer2.Panel2Collapsed = false;
					} else {
						this.splitContainer2.Panel2Collapsed = true;
						this.wbBanner.Navigate("about:blank");
					}
					//cm_licenseとkrmの早期スキップ
					if (this.options.SkipGyaoCmEnabled && this.IsGyaoCm) {
						this.timerSkipGyaoCm.Start();
					}
					//ステータスバー更新
					this.UpdateStatusbarText();
					//リサイズ
					if(FormWindowState.Normal == this.WindowState) {
						if(this.IsCm) {
							if (this.options.AutoSizeOnCmEnabled) {
								this.tsmiResizeToVideoResolution.PerformClick();
							}
						} else if(this.IsMain){
							if (this.options.AutoSizeOnNormalEnabled) {
								this.tsmiResizeToVideoResolution.PerformClick();
							}
						}
					}
					break;
			}
		}
		private void wmpMain_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e) {
			switch((WMPPlayState)e.newState){
				case WMPPlayState.wmppsPlaying:
					IWMPMedia currentMedia = this.wmpMain.currentMedia;
					while (true) {
						//再生済みのを削除
						IWMPMedia item0 = this.wmpMain.currentPlaylist.get_Item(0);
						if(item0.get_isIdentical(currentMedia)) break;
						this.wmpMain.currentPlaylist.removeItem(item0);
					}
					break;
				case WMPPlayState.wmppsMediaEnded:
					if (this.IsLastEntry) {
						if (this.currentChapter.HasValue) {//チャプタモード
							if (PlayerForm.LastCallTitle.Equals(this.wmpMain.currentMedia.name)) {
								goto endOfCont;
							} else {
								//次のチャプタ
								this.currentChapter++;
								this.OpenVideo();
							}
						} else {
							goto endOfCont;
						}
					}
					break;
				endOfCont:
					if(null == this.currentPlaylist) goto stop;
					if (this.currentPlaylist.Contains(this.currentContent)) {
						if (this.options.RemovePlayedContent) {
							this.tsmiNextContentWithDelete.PerformClick();
						} else {
							this.tsmiNextContent.PerformClick();
						}
					} else {
						goto stop;
					}
					break;
				stop:
					this.PlayContent(null, this.currentPlaylist);
					break;
			}
		}
		#endregion

		#region ステータスバー
		private void UpdateStatusbarText() {
			if (null != this.currentContent) {
				IWMPMedia curMedia = this.wmpMain.currentMedia;
				string entryUrl;
				Match matchClipNo = null;
				Match matchClipBegin = null;
				if (this.currentAttribs.TryGetValue(PlayerForm.AttribNameEntryUrl, out entryUrl)) {
					matchClipNo = PlayerForm.regexClipNo.Match(entryUrl);
					matchClipBegin = PlayerForm.regexClipBegin.Match(entryUrl);
				}

				if (null != matchClipNo && matchClipNo.Success) {
					int clipNo = int.Parse(matchClipNo.Groups[1].Value);
					if (null != matchClipBegin && matchClipBegin.Success) {
						int clipBegin = int.Parse(matchClipBegin.Groups[1].Value);
						TimeSpan begin = new TimeSpan(0, 0, clipBegin);
						TimeSpan duration = TimeSpan.FromSeconds(curMedia.duration);
						this.tspmddbMode.AddClipInfo(clipNo, begin, duration);
					}
					this.tspmddbMode.NotifyCurrentModeAndNumber(this.currentChapter, clipNo);
				} else {
					this.tspmddbMode.NotifyCurrentModeAndNumber(this.currentChapter, null);
				}

				this.tsddbDuration.Text = TimeSpan.FromSeconds(curMedia.duration).ToString();
				this.tsddbSize.Text = curMedia.imageSourceWidth.ToString() + "x" + curMedia.imageSourceHeight.ToString();
				this.tsddbTitle.Text = curMedia.name.Replace("&", "&&");
			}
		}
		private void HidePlaylist() {
			this.playlistsView1.Visible = false;
		}
		private void ShowPlaylist() {
			Size oldSize = this.playlistsView1.Size;
			Size newSize = new Size((int)(0.9 * this.wmpMain.Size.Width), (int)(0.4 * this.wmpMain.Size.Height));
			this.playlistsView1.Location = new Point(this.playlistsView1.Location.X, this.playlistsView1.Location.Y + (oldSize - newSize).Height);
			this.playlistsView1.Size = newSize;
			this.playlistsView1.Visible = true;
			this.playlistsView1.Focus();
		}
		
		private void tsddbPlaylist_Click(object sender, EventArgs e) {
			if (this.playlistsView1.Visible) {
				this.HidePlaylist();
			}else{
				this.ShowPlaylist();
			}
		}
		private void tspmddbMode_PlayModeSelected(object sender, PlayModeSelectedEventArgs e) {
			if (e.IsNormalMode) {
				this.currentChapter = null;
				this.OpenVideo();
			} else {
				this.currentChapter = e.ChapterNumber;
				this.OpenVideo();
			}
		}
		private void tspmddbMode_GoToChapterRequested(object sender, EventArgs e) {
			this.tsmiPlayChapter.PerformClick();
		}
		private void tsmiCopyClipDuration_Click(object sender, EventArgs e) {
			if (!string.IsNullOrEmpty(this.tsddbTitle.Text)) {
				Clipboard.SetText(this.tsddbDuration.Text.Replace("&&", "&"));
			}
		}
		private void tsmiCopyClipResolution_Click(object sender, EventArgs e) {
			if (!string.IsNullOrEmpty(this.tsddbTitle.Text)) {
				Clipboard.SetText(this.tsddbSize.Text.Replace("&&", "&"));
			}
		}
		private void tsmiCopyClipTitle_Click(object sender, EventArgs e) {
			if (!string.IsNullOrEmpty(this.tsddbTitle.Text)) {
				Clipboard.SetText(this.tsddbTitle.Text.Replace("&&", "&"));
			}
		}
		private void playListView1_Leave(object sender, EventArgs e) {
			this.HidePlaylist();
		}
		#endregion

		private void playlistsView1_Leave(object sender, EventArgs e) {
			this.HidePlaylist();
		}
		private void wbBanner_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			this.wbBanner.Document.Body.Style += ";margin:0px;padding:0px;";
		}
		private void timerAutoVolume_Tick(object sender, EventArgs e) {
			this.timerAutoVolume.Stop();
			this.ModifyVolume();
		}
		private void timerSkipGyaoCm_Tick(object sender, EventArgs e) {
			this.timerSkipGyaoCm.Stop();
			this.tsmiNextTrack.PerformClick();
		}
		
		protected override void WndProc(ref Message m) {
			switch ((WM)m.Msg) {
				case WM.WINDOWPOSCHANGING:
					if (this.IsAlwaysBottomMost) {
						//最背面処理
						WINDOWPOS wp = (WINDOWPOS)m.GetLParam(typeof(WINDOWPOS));
						wp.hwndInsertAfter = new IntPtr((int)SetWindowPosInsertAfterSpecialValues.BOTTOM);
						Marshal.StructureToPtr(wp, m.LParam, true);
						m.Result = IntPtr.Zero;
						return;
					}
					break;
			}
			base.WndProc(ref m);
		}

		#region IPlayerFormBindingContract Members
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsAlwaysTopMost {
			get {
				return this.TopMost;
			}
			set {
				if (this.IsAlwaysTopMost == value) return;
				this.TopMost = value;
				this.tsmiViewTopmost.Checked = value;

				if (value) {
					this.IsAlwaysBottomMost = false;
					base.BringToFront();
				}
				this.OnPropertyChanged("IsAlwaysTopMost");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsAlwaysBottomMost {
			get {
				return this.isAlwaysBottomMost;
			}
			set {
				if (this.IsAlwaysBottomMost == value) return;
				this.isAlwaysBottomMost = value;
				this.tsmiViewBottomMost.Checked = value;

				if (value) {
					this.IsAlwaysTopMost = false;
					base.SendToBack();
				} else {
					base.BringToFront();
				}
				this.OnPropertyChanged("IsAlwaysBottomMost");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool HideUiOnDeactivatedEnabled {
			get { return this.hideUiOnDeactivatedEnabled; }
			set {
				if (this.HideUiOnDeactivatedEnabled == value) return;
				
				this.hideUiOnDeactivatedEnabled = value;
				this.tsmiViewAutoHide.Checked = value;
				this.OnPropertyChanged("HideUiOnDeactivatedEnabled");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool SuspendScreenSaverEnabled {
			get { return this.ssl.Enabled;}
			set {
				if (this.ssl.Enabled == value) return;
				this.ssl.Enabled = value;
				this.OnPropertyChanged("SuspendScreenSaverEnabled");
			}
		}
		#endregion
	}
	
	interface IPlayerFormBindingContract : IBindingContract{
		bool IsAlwaysTopMost { get; set; }
		bool IsAlwaysBottomMost { get; set; }
		bool HideUiOnDeactivatedEnabled { get; set; }
		bool SuspendScreenSaverEnabled { get;set;}
	}

	public sealed class PlayerFormOptions : FormOptionsBase, IPlayerFormBindingContract {
		private const int VolumeMax = 100;
		private const int VolumeMin = 0;
		
		public PlayerFormOptions() {
		}

		#region IPlayerFormOptionsBindable Members
		private bool isAlwaysTopMost = false;
		[Category("表示")]
		[DisplayName("最前面")]
		[Description("他のウィンドウよりも手前に表示します．")]
		[DefaultValue(false)]
		public bool IsAlwaysTopMost {
			get { return this.isAlwaysTopMost; }
			set {
				this.isAlwaysTopMost = value;
				if (value) {
					this.IsAlwaysBottomMost = false;
				}
			}
		}

		private bool isAlwaysBottomMost = false;
		[Category("表示")]
		[DisplayName("最背面")]
		[Description("他のウィンドウよりも奥に表示します．")]
		[DefaultValue(false)]
		public bool IsAlwaysBottomMost {
			get { return this.isAlwaysBottomMost; }
			set {
				this.isAlwaysBottomMost = value;
				if (value) {
					this.IsAlwaysTopMost = false;
				}
			}
		}

		private bool hideUiOnDeactivatedEnabled = false;
		[Category("表示")]
		[DisplayName("非アクティブ時にUI非表示")]
		[Description("ウィンドウが非アクティブになったらUIを非表示にします．")]
		[DefaultValue(false)]
		public bool HideUiOnDeactivatedEnabled {
			get { return this.hideUiOnDeactivatedEnabled; }
			set { this.hideUiOnDeactivatedEnabled = value; }
		}
		
		private bool suspendScreenSaverEnabled = false;
		[Category("表示")]
		[DisplayName("スクリーンセーバを抑止")]
		[Description("アプリケーションがアクティブなときにスクリーンセーバの起動を抑止します．")]
		[DefaultValue(false)]
		public bool SuspendScreenSaverEnabled {
			get { return this.suspendScreenSaverEnabled; }
			set { this.suspendScreenSaverEnabled = value; }
		}
		#endregion
		
		private bool skipGyaoCmEnabled = true;
		[Category("再生")]
		[DisplayName("gyaocmの早期スキップ")]
		[Description("cm_licenseやkrmを再生直後にスキップすることでステーションコールまでの待ち時間を短くします．")]
		[DefaultValue(true)]
		public bool SkipGyaoCmEnabled {
			get { return this.skipGyaoCmEnabled; }
			set { this.skipGyaoCmEnabled = value; }
		}
		private bool chapterModeFromBegining = false;
		[Category("再生")]
		[DisplayName("最初からチャプタモード")]
		[Description("再生開始時から常にチャプタモードで再生を開始します．")]
		[DefaultValue(false)]
		public bool ChapterModeFromBegining {
			get { return this.chapterModeFromBegining; }
			set { this.chapterModeFromBegining = value; }
		}

		private bool autoVolumeEnabled = true;
		[Category("音量調整")]
		[DisplayName("自動音量調整を有効にする")]
		[Description("自動音量調整を有効にします．")]
		[DefaultValue(true)]
		public bool AutoVolumeEnabled {
			get { return this.autoVolumeEnabled; }
			set { this.autoVolumeEnabled = value; }
		}

		private int autoVolumeCmVolume = 40;
		[Category("音量調整")]
		[DisplayName("CMの音量")]
		[Description("自動音量調整を有効にしている場合に，CM時になったらこの音量に設定します．")]
		[DefaultValue(40)]
		public int AutoVolumeCmVolume {
			get { return this.autoVolumeCmVolume; }
			set {
				if (value < PlayerFormOptions.VolumeMin || PlayerFormOptions.VolumeMax < value) {
					throw new InvalidOperationException();
				}
				this.autoVolumeCmVolume = value;
			}
		}

		private int autoVolumeMainVolume = 100;
		[Category("音量調整")]
		[DisplayName("本編の音量")]
		[Description("自動音量調整を有効にしている場合に，本編になったらこの音量に設定します．")]
		[DefaultValue(100)]
		public int AutoVolumeMainVolume {
			get { return this.autoVolumeMainVolume; }
			set {
				if (value < PlayerFormOptions.VolumeMin || PlayerFormOptions.VolumeMax < value) {
					throw new InvalidOperationException();
				}
				this.autoVolumeMainVolume = value;
			}
		}

		private bool autoSizeOnNormalEnabled = true;
		[Category("リサイズ")]
		[DisplayName("本編で自動リサイズ")]
		[Description("本編の動画が始まったときにプレーヤのサイズを動画の解像度に合わせます．")]
		[DefaultValue(true)]
		public bool AutoSizeOnNormalEnabled {
			get { return this.autoSizeOnNormalEnabled; }
			set { this.autoSizeOnNormalEnabled = value; }
		}

		private bool autoSizeOnCmEnabled = true;
		[Category("リサイズ")]
		[DisplayName("CMで自動リサイズ")]
		[Description("CMの動画が始まったときにプレーヤのサイズを動画の解像度に合わせます．")]
		[DefaultValue(true)]
		public bool AutoSizeOnCmEnabled {
			get { return this.autoSizeOnCmEnabled; }
			set { this.autoSizeOnCmEnabled = value; }
		}

		private int zoomRatioOnResize = 100;
		[Category("リサイズ")]
		[DisplayName("リサイズのパーセンテージ")]
		[Description("ウィンドウのリサイズを行うときの動画解像度に対する倍率をパーセント値で指定します．[25-200]")]
		[DefaultValue(100)]
		public int ZoomRatioOnResize {
			get { return this.zoomRatioOnResize; }
			set {
				if (value < 25 || 200 < value) {
					throw new ArgumentOutOfRangeException();
				}
				this.zoomRatioOnResize = value;
			}
		}

		private bool removePlayedContent = false;
		[Category("再生")]
		[DisplayName("再生終了で削除")]
		[Description("再生が終了したコンテンツをプレイリストから削除します．")]
		[DefaultValue(false)]
		public bool RemovePlayedContent {
			get { return this.removePlayedContent; }
			set { this.removePlayedContent = value; }
		}

		private bool disableAdultCheckDialog = false;
		[Category("再生")]
		[DisplayName("年齢制限を確認しない")]
		[Description("年齢制限の確認のダイアログを表示せずに無条件でYESと返答します．")]
		[DefaultValue(false)]
		public bool DisableAdultCheckDialog {
			get { return this.disableAdultCheckDialog; }
			set { this.disableAdultCheckDialog = value; }
		}

		private int playlistTimeout = 8000;
		[Category("通信")]
		[DisplayName("プレイリストのタイムアウト")]
		[Description("プレイリストを取得するさいのタイムアウトをミリ秒で指定します．")]
		[DefaultValue(8000)]
		public int PlaylistTimeout {
			get { return this.playlistTimeout; }
			set { this.playlistTimeout = value; }
		}
		
		private string streamingServerAltAddress = string.Empty;
		[Category("不具合対策")]
		[DisplayName("wms.cd.gyao.jp のIPアドレス")]
		[Description("asx.php の中にある \"wms.cd.gyao.jp\" の文字列を指定した IP アドレスに置換します．基本的には何も設定せずに空文字列としてください．値を変更したらメニューから動画の再読み込みを実行してください．アプリケーションを終了させるとこの設定は消えます．")]
		[DefaultValue("")]
		[XmlIgnore]
		public string StreamingServerAltAddress {
			get { return this.streamingServerAltAddress; }
			set {
				if (string.IsNullOrEmpty(value)) value = string.Empty;
				this.streamingServerAltAddress = value.Trim();
			}
		}
		
		private PlaylistsViewOptions playlistsViewOptions = new PlaylistsViewOptions();
		[Browsable(false)]
		[SubOptions("プレイリストビュー", "プレイリストビューに関する設定")]
		public PlaylistsViewOptions PlaylistsViewOptions {
			get { return this.playlistsViewOptions; }
			set { this.playlistsViewOptions = value; }
		}
	}
}
