using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yusen.GCrawler;
using WMPLib;

namespace Yusen.GExplorer {
	partial class BrowserForm : FormSettingsBase, IFormWithSettings<BrowserFormSettings>{
		private const string stylePackage = "border: 4px dotted blue !important;";
		private const string styleContent = "border: 4px dotted red !important;";
		
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
		
		private Dictionary<HtmlElement, string> dicPackage = new Dictionary<HtmlElement, string>();
		private Dictionary<HtmlElement, string> dicContent = new Dictionary<HtmlElement, string>();
		
		private BrowserForm() {
			InitializeComponent();
		}
		private void BrowserForm_Load(object sender, EventArgs e) {
			Utility.AppendHelpMenu(this.menuStrip1);
			
			this.tsmiGenres.DropDownItems.Clear();
			this.番組表IToolStripMenuItem.DropDownItems.Clear();
			foreach (GGenre g in GGenre.AllGenres) {
				ToolStripMenuItem mi;
				
				mi = new ToolStripMenuItem(g.GenreName);
				mi.Tag = g;
				mi.Click += new EventHandler(delegate(object sender2, EventArgs e2) {
					GGenre genre = (sender2 as ToolStripMenuItem).Tag as GGenre;
					this.DocumentUri = genre.TopPageUri;
				});
				this.tsmiGenres.DropDownItems.Add(mi);

				mi = new ToolStripMenuItem(g.GenreName);
				mi.Tag = g;
				mi.Click += new EventHandler(delegate(object sender2, EventArgs e2) {
					GGenre genre = (sender2 as ToolStripMenuItem).Tag as GGenre;
					this.DocumentUri = genre.TimetableUri;
				});
				this.番組表IToolStripMenuItem.DropDownItems.Add(mi);
			}
			
			//外部コマンド
			this.UserCommandsManager_UserCommandsChanged(null, EventArgs.Empty);
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			this.FormClosing += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			};
			
			Utility.LoadSettingsAndEnableSaveOnClosed(this);
		}
		public string FilenameForSettings {
			get { return @"BrowserFormSettings.xml"; }
		}
		public void FillSettings(BrowserFormSettings settings) {
			base.FillSettings(settings);
		}
		public void ApplySettings(BrowserFormSettings settings) {
			base.ApplySettings(settings);
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
				//TagにIDを仕込んでおく
				this.cmsPackage.Tag = this.dicPackage[sender as HtmlElement];
				this.cmsPackage.Location = Control.MousePosition;
				this.cmsPackage.Show();
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void AddToPackages(HtmlElement elem, string packageId) {
			this.dicPackage.Add(elem, packageId);
			elem.Style += BrowserForm.stylePackage;
			elem.Click += new HtmlElementEventHandler(this.Package_Click);
		}
		private void Content_Click(object sender, HtmlElementEventArgs e) {
			if(Keys.Alt != Control.ModifierKeys) {
				//TagにIDを仕込んでおく
				this.cmsContent.Tag = this.dicContent[sender as HtmlElement];
				this.cmsContent.Location = Control.MousePosition;
				this.cmsContent.Show();
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void AddToContents(HtmlElement elem, string contentId) {
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
			foreach(HtmlElement elem in (sender as WebBrowser).Document.Body.All) {
				string strUri;
				//URI選択
				switch(elem.TagName) {
					case "IMG":
						strUri = elem.GetAttribute("src");
						break;
					case "A":
					case "AREA":
						strUri = elem.GetAttribute("href");
						break;
					default:
						continue;
				}
				Uri uri;
				try {
					uri = new Uri(strUri);
				} catch {
					continue;
				}
				//パッケージ判定
				string packageId;
				if (GPackage.TryExtractPackageId(uri, out packageId)) {
					this.AddToPackages(elem, packageId);
					continue;
				}
				//コンテンツ判定
				string contentId;
				if (GContent.TryExtractContentId(uri, out contentId)) {
					this.AddToContents(elem, contentId);
					continue;
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
			this.DocumentUri = new Uri(this.tscbAddress.Text);
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

		private void ShowHelpOnHowToCancelMenu() {
			MessageBox.Show("Altキーを押していれば通常のクリックとして扱います．", "変なメニューが出てきて邪魔だよ．", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
			this.DocumentUri = GPackage.CreatePackagePageUri(this.cmsPackage.Tag as string);
		}
		private void tsmiPackageCancel_Click(object sender, EventArgs e) {
			this.ShowHelpOnHowToCancelMenu();
		}

		private void tsmiContentOpenDetail_Click(object sender, EventArgs e) {
			this.DocumentUri = GContent.CreateDetailPageUri(this.cmsContent.Tag as string);
		}
		private void tsmiContentAddToPlayList_Click(object sender, EventArgs e) {
			string contId = this.cmsContent.Tag as string;
			GContent cont;
			if (GContent.TryDownload(contId, out cont)) {
				ContentAdapter ca = new ContentAdapter(cont);
				PlayList.Instance.AddIfNotExists(ca);
			} else {
				MessageBox.Show("指定されたコンテンツIDに関する情報が取得できませんでした．", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void tsmiContentPlayWithoutAdding_Click(object sender, EventArgs e) {
			string contId = this.cmsContent.Tag as string;
			GContent cont;
			if (GContent.TryDownload(contId, out cont)) {
				ContentAdapter ca = new ContentAdapter(cont);
				PlayerForm.Play(ca);
			} else {
				MessageBox.Show("指定されたコンテンツIDに関する情報が取得できませんでした．", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void tsmiContentPlayWmp_Click(object sender, EventArgs e) {
			Utility.PlayWithWMP(GContent.CreateMediaFileUri(this.cmsContent.Tag as string, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate));
		}
		private void tsmiContentPlayIe_Click(object sender, EventArgs e) {
			Utility.BrowseWithIE(GContent.CreatePlayerPageUri(this.cmsContent.Tag as string, GlobalSettings.Instance.BitRate));
		}
		private void tsmiContentCancel_Click(object sender, EventArgs e) {
			this.ShowHelpOnHowToCancelMenu();
		}
		#endregion

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.tsmiContentCommands.DropDownItems.Clear();
			foreach(UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem mi = new ToolStripMenuItem(
					uc.Title, null,
					new EventHandler(delegate(object sender2, EventArgs e2) {
						string contId = this.cmsContent.Tag as string;
						GContent cont;
						if (GContent.TryDownload(contId, out cont)) {
							ContentAdapter ca = new ContentAdapter(cont);
							((sender2 as ToolStripMenuItem).Tag as UserCommand).Execute(
								new ContentAdapter[] { ca });
						} else {
							MessageBox.Show("指定されたコンテンツIDに関する情報が取得できませんでした．", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}));
				mi.Tag = uc;
				this.tsmiContentCommands.DropDownItems.Add(mi);
			}
			this.tsmiContentCommands.Enabled = this.tsmiContentCommands.HasDropDownItems;
		}
	}

	public class BrowserFormSettings : FormSettingsBaseSettings {
	}
}
