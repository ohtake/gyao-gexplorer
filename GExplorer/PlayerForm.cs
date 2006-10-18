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
using Marshal = System.Runtime.InteropServices.Marshal;
using DRegion = System.Drawing.Region;

namespace Yusen.GExplorer {
	public sealed partial class PlayerForm : FormSettingsBase, IFormWithNewSettings<PlayerForm.PlayerFormSettings> {
		private const string StreamingServerName = "wms.cd.gyao.jp";
		
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
				if (this.HasOwner) {
					this.playListViewSettings = new PlayListView.PlayListViewSettings(owner.playListView1);
				} else {
					this.playListViewSettings = new PlayListView.PlayListViewSettings();
				}
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
			[DisplayName("常に最前面")]
			[Description("フォームを常に最前面に表示します．")]
			[DefaultValue(false)]
			public bool TopmostForm {
				get {
					if (this.HasOwner) return this.owner.IsAlwaysTopMost;
					else return this.topmost;
				}
				set {
					if (this.HasOwner) this.owner.IsAlwaysTopMost = value;
					else this.topmost = value;
				}
			}
			private bool topmost = false;

			[Category("表示")]
			[DisplayName("常に最背面")]
			[Description("フォームを常に最背面に表示します．")]
			[DefaultValue(false)]
			public bool IsAlwaysBottomMost {
				get {
					if (this.HasOwner) return this.owner.IsAlwaysBottomMost;
					else return this.isAlwaysBottomMost;
				}
				set {
					if (this.HasOwner) this.owner.IsAlwaysBottomMost = value;
					else this.isAlwaysBottomMost = value;
				}
			}
			private bool isAlwaysBottomMost = false;

			[Category("表示")]
			[DisplayName("非アクティブ時にUI非表示")]
			[Description("フォームがアクティブでないときにUIを非表示にします．")]
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
			[DefaultValue(true)]
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
			private bool strechToFitEnabled = true;

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
			[DisplayName("gyaocmの早期スキップ")]
			[Description("cm_licenseやkrmを再生直後にスキップすることでステーションコールまでの待ち時間を短くします．")]
			[DefaultValue(true)]
			public bool SkipGyaoCmEnabled {
				get { return this.skipGyaoCmEnabled; }
				set { this.skipGyaoCmEnabled = value; }
			}
			private bool skipGyaoCmEnabled = true;

			[Category("再生")]
			[DisplayName("最初からチャプタモード")]
			[Description("再生開始時から常にチャプタモードで再生を開始します．")]
			[DefaultValue(false)]
			public bool ChapterModeFromBegining {
				get { return this.chapterModeFromBegining; }
				set { this.chapterModeFromBegining = value; }
			}
			private bool chapterModeFromBegining = false;

			[Category("再生")]
			[DisplayName("年齢制限の確認には無条件でYES")]
			[Description("年齢制限の確認のダイアログを表示せずに無条件でYESと返答します．")]
			[DefaultValue(false)]
			public bool DisableAdultCheckDialog {
				get { return this.disableAdultCheckDialog; }
				set { this.disableAdultCheckDialog = value; }
			}
			private bool disableAdultCheckDialog = false;


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
			[DefaultValue(40)]
			public int VolumeCf {
				get { return this.volumeCf; }
				set {
					if (value < PlayerFormSettings.VolumeMin || PlayerFormSettings.VolumeMax < value) {
						throw new ArgumentOutOfRangeException("VolumeCf");
					}
					this.volumeCf = value;
				}
			}
			private int volumeCf = 40;
			
			[Category("不具合対策")]
			[DisplayName(PlayerForm.StreamingServerName + " のIPアドレス")]
			[Description("asx.php の中にある \"" + PlayerForm.StreamingServerName + "\" の文字列を指定した IP アドレスに置換します．基本的には何も設定せずに空文字列としてください．値を変更したらメニューから動画の再読み込みを実行してください．フォームを閉じたらこの設定は消えます．")]
			[DefaultValue("")]
			[XmlIgnore]
			public string StreamingServerAltAddress {
				get { return this.streamingServerAltAddress; }
				set {
					if (string.IsNullOrEmpty(value)) value = string.Empty;
					this.streamingServerAltAddress = value.Trim();
				}
			}
			private string streamingServerAltAddress = string.Empty;
			
			[Browsable(false)]
			public PlayListView.PlayListViewSettings PlayListViewSettings {
				get { return this.playListViewSettings; }
				set { this.playListViewSettings.ApplySettings(value); }
			}
			private readonly PlayListView.PlayListViewSettings playListViewSettings;

			#region INewSettings<PlayerFormSettings> Members
			public void ApplySettings(PlayerFormSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}

		private sealed class PageReaderWithCookie {
			private static readonly Uri TopPageUri = new Uri("http://www.gyao.jp/");
			private static readonly Encoding PageEncoding = Encoding.GetEncoding("Shift_JIS");
			
			private readonly CookieContainer cc;
			
			public PageReaderWithCookie() {
				int cookieSize = 0;
				if (!WindowsFunctions.InternetGetCookie(PageReaderWithCookie.TopPageUri.AbsoluteUri, null, null, ref cookieSize)) {
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
				StringBuilder cookieSb = new StringBuilder(cookieSize);
				if (!WindowsFunctions.InternetGetCookie(PageReaderWithCookie.TopPageUri.AbsoluteUri, null, cookieSb, ref cookieSize)) {
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				}
				this.cc = new CookieContainer();
				this.cc.SetCookies(PageReaderWithCookie.TopPageUri, cookieSb.ToString().Replace(';', ','));
			}
			
			public string GetResponseText(Uri uri) {
				HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
				req.CookieContainer = this.cc;

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
		private static readonly Regex regexAgeThreshold = new Regex(@"<div align=""center""><img src=""/cinema/rating/img/r(\d+)");
		private static readonly string tempAsxDirectory = Path.Combine(Application.StartupPath, "Temp");
		private static readonly string tempAsxFilename = "AsxPhp.asx";
		
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
		private string bannerKeyValue = string.Empty;
		private bool isAlwaysBottomMost = false;

		private readonly ScreenSaveListener ssl = new ScreenSaveListener();
		private readonly Random rand = new Random();

		private PlayerFormSettings settings;
		
		private PlayerForm() {
			InitializeComponent();
			
			this.tsmiViewFullScreen.ShortcutKeys = Keys.Alt | Keys.Enter;
			this.tsmiSettings.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
		}

		private void OpenVideo() {
			if (null == this.CurrentContent) {
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
			
			PageReaderWithCookie pageReader = new PageReaderWithCookie();
			
			//再生ページ取得
			Uri playerPageUri = this.CurrentContent.PlayerPageUri;
			Application.DoEvents();
			string body = pageReader.GetResponseText(playerPageUri);
			Application.DoEvents();
			
			//年齢チェック
			Match matchAge = PlayerForm.regexAgeThreshold.Match(body);
			if (matchAge.Success) {
				if (!this.settings.DisableAdultCheckDialog) {
					//ダイアログ表示前に停止
					this.wmpMain.close();
					this.wmpMain.currentPlaylist.clear();
					int ageThreshold = int.Parse(matchAge.Groups[1].Value);
					switch (MessageBox.Show(this, string.Format("あなたは{0}才以上ですか？", ageThreshold), "年齢制限", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
						case DialogResult.Yes:
							break;
						default:
							this.CurrentContent = null;
							return;
					}
				}
				Uri playerPage2Uri = new Uri(playerPageUri.AbsoluteUri + "&check_flg=0");
				Application.DoEvents();
				body = pageReader.GetResponseText(playerPage2Uri);
				Application.DoEvents();
			}
			
			//asx.phpを検索
			Match matchAsxPhp = PlayerForm.regexAsxPhp.Match(body);
			if (!matchAsxPhp.Success) {
				throw new Exception("再生ページから asx.php のアドレスを取得できなかった．");
			}
			string asxPhpAddr = matchAsxPhp.Value;
			//チャプタ
			if (this.CurrentChapter.HasValue) {
				asxPhpAddr += "&chapterNo=" + this.CurrentChapter.Value.ToString();
			}
			
			// asx をダウンロード
			DirectoryInfo diTemp = new DirectoryInfo(PlayerForm.tempAsxDirectory);
			if (!diTemp.Exists) diTemp.Create();
			string tempFile = Path.Combine(PlayerForm.tempAsxDirectory, PlayerForm.tempAsxFilename);
			Application.DoEvents();
			pageReader.DownloadToFile(new Uri(asxPhpAddr), tempFile);
			Application.DoEvents();
			
			//名前解決ができない不具合対策用
			if (!string.IsNullOrEmpty(this.settings.StreamingServerAltAddress)) {
				string altHost = this.settings.StreamingServerAltAddress;
				string asxText;
				using (StreamReader reader = new StreamReader(tempFile)) {
					asxText = reader.ReadToEnd();
				}
				using (StreamWriter writer = new StreamWriter(tempFile)) {
					asxText = asxText.Replace(PlayerForm.StreamingServerName, altHost);
					writer.Write(asxText);
				}
			}
			
			// WMP に読み込ませる
			Application.DoEvents();
			IWMPPlaylist plist = this.wmpMain.newPlaylist(this.Text, tempFile);
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
		private void ModifyVolume() {
			if(this.IsCf) {
				this.wmpMain.settings.volume = this.settings.VolumeCf;
			}else if(this.IsMain){
				this.wmpMain.settings.volume = this.settings.VolumeNormal;
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
				"http://www1.gyao.jp/html.ng/site=gyao.cm.sky"
				+ this.bannerKeyValue
				+ "&zone=" + dartTag
				+ "&ord=" + ord.ToString() + "?");
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
				
				PlayList.Instance.CurrentContent = value;
				if (null == value) {
					this.Text = "PlayerForm";
					this.tsddbPlaylist.Text = "リスト(&L)";
				} else {
					this.Text = value.DisplayName;
					this.tsddbPlaylist.Text = value.ContentId;
				}
				this.tspmddbMode.ClearClipInfo();
				this.tspmddbMode.NotifyCurrentModeAndNumber(this.CurrentChapter, null);
				
				this.OpenVideo();
			}
		}
		public int? CurrentChapter {
			get {
				return this.currentChapter;
			}
			private set {
				if (! value.Equals(this.currentChapter)) {
					this.currentChapter = value;
					this.tspmddbMode.NotifyCurrentModeAndNumber(this.CurrentChapter, null);
					
					this.OpenVideo();
				}
			}
		}
		private bool IsGyaoCm {
			get {
				return this.wmpMain.currentMedia.sourceURL.Contains("/gyaocm");
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
		private bool IsAlwaysTopMost {
			get {
				return this.TopMost;
			}
			set {
				this.TopMost = value;
				this.tsmiViewTopmost.Checked = value;

				if (value) {
					this.settings.IsAlwaysBottomMost = false;
					base.BringToFront();
				}
			}
		}
		private bool IsAlwaysBottomMost {
			get {
				return this.isAlwaysBottomMost;
			}
			set {
				this.isAlwaysBottomMost = value;
				this.tsmiViewBottomMost.Checked = value;

				if (value) {
					this.settings.TopmostForm = false;
					base.SendToBack();
				} else {
					base.BringToFront();
				}
			}
		}
		
		private void PlayerForm_Load(object sender, EventArgs e) {
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
			this.HidePlaylist();
			if(this.settings.HideUiOnDeactivatedEnabled) {
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
		
		private void ssl_ScreenSaverRaising(object sender, CancelEventArgs e) {
			if (this.settings.SuspendScreenSaveEnabled) {
				e.Cancel = true;
			}
		}
		#region メインメニュー
		private void tsmiPlayChapter_Click(object sender, EventArgs e) {
			string title =  "特定のチャプタから再生";
			this.inputBoxDialog1.Title =title;
			this.inputBoxDialog1.Message = "チャプタ番号の入力．空文字列の場合は通常再生．";
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
			ContentAdapter prevCont = PlayList.Instance.PrevContentOf(this.CurrentContent);
			this.CurrentContent = prevCont;
		}
		private void tsmiNextContent_Click(object sender, EventArgs e) {
			ContentAdapter nextCont = PlayList.Instance.NextContentOf(this.CurrentContent);
			this.CurrentContent = nextCont;
		}
		private void tsmiNextContentWithDelete_Click(object sender, EventArgs e) {
			ContentAdapter nextCont = PlayList.Instance.NextContentOf(this.CurrentContent);
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
		private void tsmiViewBottomMost_Click(object sender, EventArgs e) {
			this.settings.IsAlwaysBottomMost = !this.settings.IsAlwaysBottomMost;
		}
		private void tsmiViewAutoHide_Click(object sender, EventArgs e) {
			this.settings.HideUiOnDeactivatedEnabled = !this.settings.HideUiOnDeactivatedEnabled;
		}
		private void tsmiResizeToVideoResolution_Click(object sender, EventArgs e) {
			this.WindowState = FormWindowState.Normal;
			Size videoSize = new Size(this.wmpMain.currentMedia.imageSourceWidth, this.wmpMain.currentMedia.imageSourceHeight);
			Size distSize = new Size(videoSize.Width*this.settings.ZoomRatioOnResize/100, videoSize.Height*this.settings.ZoomRatioOnResize/100);
			
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
						string dartTag = dartTagMatch.Groups[1].Value;
						dartTag = dartTag.Split(new string[] { "AR" }, StringSplitOptions.None)[0];
						this.wbBanner.Navigate(this.CreateBannerUri(dartTag));
						this.splitContainer2.Panel2Collapsed = false;
					} else {
						this.splitContainer2.Panel2Collapsed = true;
						this.wbBanner.Navigate("about:blank");
					}
					//cm_licenseとkrmの早期スキップ
					if (this.settings.SkipGyaoCmEnabled && this.IsGyaoCm) {
						this.timerSkipGyaoCm.Start();
					}
					//ステータスバー更新
					this.UpdateStatusbarText();
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
						if (this.CurrentChapter.HasValue) {//チャプタモード
							if (PlayerForm.LastCallTitle.Equals(this.wmpMain.currentMedia.name)) {
								goto endOfCont;
							} else {
								//次のチャプタ
								this.CurrentChapter++;
							}
						} else {
							goto endOfCont;
						}
					}
					break;
				endOfCont:
					if (PlayList.Instance.Contains(this.CurrentContent)) {//プレイリストで次
						if (this.settings.RemovePlayedContentEnabled) {
							this.tsmiNextContentWithDelete.PerformClick();
						} else {
							this.tsmiNextContent.PerformClick();
						}
					} else {//プレイリスト外なら再生終了
						this.CurrentContent = null;
					}
					break;
			}
		}
		#endregion


		#region IHasNewSettings<PlayerFormSettings> Members
		public PlayerFormSettings Settings {
			get { return this.settings; }
		}
		#endregion

		#region ステータスバー
		private void UpdateStatusbarText() {
			if (null != this.CurrentContent) {
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
					this.tspmddbMode.NotifyCurrentModeAndNumber(this.CurrentChapter, clipNo);
				} else {
					this.tspmddbMode.NotifyCurrentModeAndNumber(this.CurrentChapter, null);
				}

				this.tsddbDuration.Text = TimeSpan.FromSeconds(curMedia.duration).ToString();
				this.tsddbSize.Text = curMedia.imageSourceWidth.ToString() + "x" + curMedia.imageSourceHeight.ToString();
				this.tsddbTitle.Text = curMedia.name.Replace("&", "&&");
			}
		}
		private void HidePlaylist() {
			this.playListView1.Visible = false;
		}
		private void ShowPlaylist() {
			Size oldSize = this.playListView1.Size;
			Size newSize = new Size((int)(0.9 * this.wmpMain.Size.Width), (int)(0.4 * this.wmpMain.Size.Height));
			this.playListView1.Location = new Point(this.playListView1.Location.X, this.playListView1.Location.Y + (oldSize - newSize).Height);
			this.playListView1.Size = newSize;
			this.playListView1.Visible = true;
			this.playListView1.Focus();
		}
		
		private void tsddbPlaylist_Click(object sender, EventArgs e) {
			if (this.playListView1.Visible) {
				this.HidePlaylist();
			}else{
				this.ShowPlaylist();
			}
		}
		private void tspmddbMode_PlayModeSelected(object sender, PlayModeSelectedEventArgs e) {
			if (e.IsNormalMode) {
				this.CurrentChapter = null;
			} else {
				this.CurrentChapter = e.ChapterNumber;
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

		private void tsmiSettings_DropDownOpened(object sender, EventArgs e) {
			this.tspgPlayerFormSettings.RefreshPropertyGrid();
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
					if (!this.IsAlwaysBottomMost) break;
					//最背面処理
					WINDOWPOS wp = (WINDOWPOS)m.GetLParam(typeof(WINDOWPOS));
					wp.hwndInsertAfter = new IntPtr((int)SetWindowPosInsertAfterSpecialValues.BOTTOM);
					Marshal.StructureToPtr(wp, m.LParam, true);
					m.Result = IntPtr.Zero;
					return;
			}
			base.WndProc(ref m);
		}
	}
}
