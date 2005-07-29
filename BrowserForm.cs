using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WMPLib;

namespace Yusen.GExplorer {
	public partial class BrowserForm : Form, IUsesUserSettings {
		private const string stylePackage = "border: 4px dotted blue !important;";
		private static readonly Regex regexPackImgSrc =
			new Regex(@"/pac([0-9]{7})_[sml]\.jpg", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexPackAnchorHref =
			new Regex(@"(?:javascript:gotoList\((?:%20| )?'|/pac_id/)pac([0-9]{7})['/]", RegexOptions.Compiled | RegexOptions.Singleline);
		
		private const string styleContent = "border: 4px dotted red !important;";
		private static readonly Regex regexContImgSrc =
			new Regex(@"/cnt([0-9]{7})_[sml]\.jpg", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexContAnchorHref =
			new Regex(@"(?:javascript:gotoDetail\((?:%20| )?'|/contents_id/)cnt([0-9]{7})['/]", RegexOptions.Compiled | RegexOptions.Singleline);
		
		private static BrowserForm instance = null;
		public static BrowserForm Instance {
			get {
				if(null == BrowserForm.instance || BrowserForm.instance.IsDisposed) {
					BrowserForm.instance = new BrowserForm();
				}
				return BrowserForm.instance;
			}
		}
		public static void Browse(Uri uri) {
			BrowserForm.Instance.Show();
			BrowserForm.Instance.Focus();
			BrowserForm.Instance.DocumentUri = uri;
		}
		
		private Dictionary<HtmlElement, int> dicPackage = new Dictionary<HtmlElement, int>();
		private Dictionary<HtmlElement, int> dicContent = new Dictionary<HtmlElement, int>();
		
		private BrowserForm() {
			InitializeComponent();
			this.LoadSettings();
			this.Icon = Utility.GetGExplorerIcon();
			Utility.AppendHelpMenu(this.menuStrip1);
			
			this.tsmiGenres.DropDownItems.Clear();
			this.tscbAddress.Items.Clear();
			this.tscbAddress.Items.Add("http://www.gyao.jp/");
			foreach(GGenre g in GGenre.AllGenres) {
				ToolStripMenuItem mi = new ToolStripMenuItem(g.GenreName);
				mi.Tag = g;
				mi.Click += new EventHandler(delegate(object sender, EventArgs e) {
					GGenre genre = (sender as ToolStripMenuItem).Tag as GGenre;
					this.DocumentUri = genre.GenreTopPageUri;
				});
				this.tsmiGenres.DropDownItems.Add(mi);
				this.tscbAddress.Items.Add(g.GenreTopPageUri.AbsoluteUri);
			}

			//外部コマンド
			this.UserCommandsManager_UserCommandsChanged(null, EventArgs.Empty);
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			this.FormClosing += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			};

			//ユーザ設定
			this.LocationChanged += delegate {
				this.SaveSettings();
			};
			this.SizeChanged += delegate {
				this.SaveSettings();
			};
			UserSettings.Instance.BrowserForm.ChangeCompleted +=
				new UserSettingsChangeCompletedEventHandler(this.LoadSettings);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					if(FormWindowState.Minimized == this.WindowState) {
						//最小化したまま終了されるとウィンドウ位置が変になるので元に戻す
						this.WindowState = FormWindowState.Normal;
					}
					UserSettings.Instance.BrowserForm.ChangeCompleted -= this.LoadSettings;
				});

			this.tscbAddress.ComboBox.KeyDown += this.tscbAddress_KeyDown;
		}
		public Uri DocumentUri {
			get {
				return this.wbMain.Url;
			}
			set {
				if (null == value) throw new ArgumentNullException();
				this.wbMain.Url = value;
			}
		}
		
		private void wbMain_CanGoBackChanged(object sender, EventArgs e) {
			this.tsbBack.Enabled = this.wbMain.CanGoBack;
		}
		private void wbMain_CanGoForwardChanged(object sender, EventArgs e) {
			this.tsbForward.Enabled = this.wbMain.CanGoForward;
		}
		private void wbMain_StatusTextChanged(object sender, EventArgs e) {
			this.toolStripStatusLabel1.Text = this.wbMain.StatusText;
		}
		private void wbMain_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e) {
			this.toolStripProgressBar1.Maximum = (int)e.MaximumProgress;
			this.toolStripProgressBar1.Value = (int)e.CurrentProgress;
		}
		private void wbMain_DocumentTitleChanged(object sender, EventArgs e) {
			this.Text = (sender as WebBrowser).DocumentTitle;
		}
		
		private void Package_Click(object sender, HtmlElementEventArgs e) {
			if(Keys.Alt != Control.ModifierKeys) {
				//Tagにダミーのパッケージを仕込んでおく
				this.cmsPackage.Tag = GPackage.CreateDummyPackage(this.dicPackage[sender as HtmlElement]);
				this.cmsPackage.Location = Control.MousePosition;
				this.cmsPackage.Show();
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void AddToPackages(HtmlElement elem, int packageId) {
			this.dicPackage.Add(elem, packageId);
			elem.Style += BrowserForm.stylePackage;
			elem.Click += new HtmlElementEventHandler(this.Package_Click);
		}
		private void Content_Click(object sender, HtmlElementEventArgs e) {
			if(Keys.Alt != Control.ModifierKeys) {
				//Tagにダミーのコンテンツを仕込んでおく
				this.cmsContent.Tag = GContent.CreateDummyContent(this.dicContent[sender as HtmlElement]);
				this.cmsContent.Location = Control.MousePosition;
				this.cmsContent.Show();
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void AddToContents(HtmlElement elem, int contentId) {
			this.dicContent.Add(elem, contentId);
			elem.Style += BrowserForm.styleContent;
			elem.Click += new HtmlElementEventHandler(this.Content_Click);
		}
		
		private void wbMain_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			this.tsbStop.Enabled = true;
		}
		private void wbMain_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
			this.tscbAddress.Text = this.wbMain.Url.AbsoluteUri;
		}
		private void wbMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			this.tsbStop.Enabled = false;
			this.dicPackage.Clear();
			this.dicContent.Clear();
			
			//パッケージとコンテンツにメニューをくっつける
			Match match;
			foreach(HtmlElement elem in (sender as WebBrowser).Document.Body.All) {
				switch(elem.TagName) {
					case "IMG":
						string src = elem.GetAttribute("src");
						match = BrowserForm.regexPackImgSrc.Match(src);
						if(match.Success) {
							this.AddToPackages(elem, int.Parse(match.Groups[1].Value));
							break;
						}
						match = BrowserForm.regexContImgSrc.Match(src);
						if(match.Success) {
							this.AddToContents(elem, int.Parse(match.Groups[1].Value));
							break;
						}
						break;
					case "A":
						string href = elem.GetAttribute("href");
						match = BrowserForm.regexPackAnchorHref.Match(href);
						if(match.Success) {
							this.AddToPackages(elem, int.Parse(match.Groups[1].Value));
							break;
						}
						match = BrowserForm.regexContAnchorHref.Match(href);
						if(match.Success) {
							this.AddToContents(elem, int.Parse(match.Groups[1].Value));
							break;
						}
						break;
				}
			}
		}
		
		private void tsbBack_Click(object sender, EventArgs e) {
			this.wbMain.GoBack();
		}
		private void tsbForward_Click(object sender, EventArgs e) {
			this.wbMain.GoForward();
		}
		private void tsbStop_Click(object sender, EventArgs e) {
			this.wbMain.Stop();
		}
		private void GoToAddressBarUri(object sender, EventArgs e) {
			try {
				this.DocumentUri = new Uri(this.tscbAddress.Text);
			} catch(Exception ex) {
				Utility.DisplayException(ex);
			}
		}
		private void tscbAddress_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Return:
					this.GoToAddressBarUri(sender, e);
					break;
				default:
					return;
			}
			e.Handled = true;
		}

		#region メニューとコンテキストメニュー
		private void tsmiOpenTop_Click(object sender, EventArgs e) {
			this.DocumentUri = new Uri("http://www.gyao.jp/");
		}
		private void tsmiSaveAs_Click(object sender, EventArgs e) {
			this.wbMain.ShowSaveAsDialog();
		}
		private void tsmiPageProperty_Click(object sender, EventArgs e) {
			this.wbMain.ShowPropertiesDialog();
		}
		private void tsmiPageSetup_Click(object sender, EventArgs e) {
			this.wbMain.ShowPageSetupDialog();
		}
		private void tsmiPrint_Click(object sender, EventArgs e) {
			this.wbMain.ShowPrintDialog();
		}
		private void tsmiPrintPreview_Click(object sender, EventArgs e) {
			this.wbMain.ShowPrintPreviewDialog();
		}
		private void tsmiClose_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void tsmiPackageOpen_Click(object sender, EventArgs e) {
			this.DocumentUri = (this.cmsPackage.Tag as GPackage).PackagePageUri;
		}
		private void tsmiContentOpenDetail_Click(object sender, EventArgs e) {
			this.DocumentUri = (this.cmsContent.Tag as GContent).DetailPageUri;
		}
		private void tsmiContentOpenPlayPage_Click(object sender, EventArgs e) {
			this.DocumentUri = (this.cmsContent.Tag as GContent).PlayerPageUri;
		}
		private void tsmiContentPlayGPlayer_Click(object sender, EventArgs e) {
			PlayerForm.Play(this.cmsContent.Tag as GContent);
		}
		private void tsmiContentPlayWmp_Click(object sender, EventArgs e) {
			Utility.PlayWithWMP((this.cmsContent.Tag as GContent).MediaFileUri);
		}
		private void tsmiContentPlayIe_Click(object sender, EventArgs e) {
			Utility.BrowseWithIE((this.cmsContent.Tag as GContent).PlayerPageUri);
		}
		private void tsmiContentProperty_Click(object sender, EventArgs e) {
			try {
				ContentPropertyViewer.View(this.cmsContent.Tag as GContent);
			} catch(Exception ex) {
				Utility.DisplayException(ex);
			}
		}
		#endregion

		public void LoadSettings() {
			UserSettings.Instance.BrowserForm.ApplySettings(this);
		}
		public void SaveSettings() {
			UserSettings.Instance.BrowserForm.StoreSettings(this);
			UserSettings.Instance.BrowserForm.OnChangeCompleted();
		}
		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.tsmiContentCommands.DropDownItems.Clear();
			foreach(UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem mi = new ToolStripMenuItem(
					uc.Title, null,
					new EventHandler(delegate(object sender2, EventArgs e2) {
						((sender2 as ToolStripMenuItem).Tag as UserCommand).Execute(
							new GContent[] { this.cmsContent.Tag as GContent });
					}));
				mi.Tag = uc;
				this.tsmiContentCommands.DropDownItems.Add(mi);
			}
			this.tsmiContentCommands.Enabled = (0 != this.tsmiContentCommands.DropDownItems.Count);
		}
		#region 再生ページでのテスト
		private void tsmiTestBody_Click(object sender, EventArgs e) {
			HtmlElement body = this.wbMain.Document.Body;
			body.SetAttribute("onselectstart", "return true;");
			body.SetAttribute("oncontextmenu", "return true;");
		}
		private void tsmiTestSize_Click(object sender, EventArgs e) {
			HtmlElement player = this.wbMain.Document.GetElementById("player");
			player.SetAttribute("width", "100%");
			player.SetAttribute("height", "100%");
			HtmlElement elem = player;
			do{
				elem.Style += "padding:0; margin:0;";
				elem = elem.Parent;
			}while(null != elem);
			player.ScrollIntoView(true);
		}
		private void tsmiTestClick_Click(object sender, EventArgs e) {
			foreach(HtmlElement eScript in this.wbMain.Document.GetElementsByTagName("script")) {
				if("player" == eScript.GetAttribute("htmlFor") && "click()" == eScript.GetAttribute("event")) {
					eScript.SetAttribute("event", "");
				}
			}
		}
		private void tsmiTestContextMenu_Click(object sender, EventArgs e) {
			HtmlElement player = this.wbMain.Document.GetElementById("player");
			player.SetAttribute("enableContextMenu", "1");
		}

		private void tsmiTestVolume_Click(object sender, EventArgs e) {
			HtmlElement player = this.wbMain.Document.GetElementById("player");
			player.AttachEventHandler("OpenStateChange", new EventHandler(this.OpenStateChanged));
		}
		private void tsmiTestVolumeDisable_Click(object sender, EventArgs e) {
			HtmlElement player = this.wbMain.Document.GetElementById("player");
			player.DetachEventHandler("OpenStateChange", new EventHandler(this.OpenStateChanged));
		}
		
		private void OpenStateChanged(object sender, EventArgs e) {
			try {
				HtmlElement player = this.wbMain.Document.GetElementById("player");
				IWMPPlayer wmp = (IWMPPlayer)player.InvokeMember("getAttribute", "object");
				WMPOpenState opState = (WMPOpenState)player.InvokeMember("getAttribute", "openState");
				if(WMPOpenState.wmposMediaOpen == opState){
					bool isCf = wmp.currentMedia.getItemInfo("WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL").StartsWith("Adv:");
					wmp.settings.volume = isCf ? 20 : 100;
				}
			} catch(Exception ex) {
				Utility.DisplayException(ex);
			}
		}
		private void tsmiTestStrechToFit_Click(object sender, EventArgs e) {
			try {
				HtmlElement player = this.wbMain.Document.GetElementById("player");
				IWMPPlayer2 wmp = (IWMPPlayer2)player.InvokeMember("getAttribute", "object");
				wmp.stretchToFit = !wmp.stretchToFit;
			} catch(Exception ex) {
				Utility.DisplayException(ex);
			}
		}
		private void tsmiTestScrolBars_Click(object sender, EventArgs e) {
			this.wbMain.ScrollBarsEnabled = !this.wbMain.ScrollBarsEnabled;
		}
		private void tsmiTestCampaign_Click(object sender, EventArgs e) {
			this.wbMain.Document.InvokeScript("gotoCampaign");
		}
		#endregion
	}
}
