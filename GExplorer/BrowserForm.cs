using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yusen.GCrawler;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Web;
using System.Drawing;

namespace Yusen.GExplorer {
	public sealed partial class BrowserForm : FormSettingsBase, IFormWithNewSettings<BrowserForm.BrowserFormSettings> {
		public sealed class BrowserFormSettings : INewSettings<BrowserFormSettings> {
			private readonly BrowserForm owner;

			public BrowserFormSettings() : this(null) {
			}
			internal BrowserFormSettings(BrowserForm owner) {
				this.owner = owner;
				this.formSettingsBaseSettings = new FormSettingsBaseSettings(owner);
			}

			[XmlIgnore]
			[Browsable(false)]
			private bool HasOwner {
				get { return null != this.owner; }
			}

			[ReadOnly(true)]
			[Category("位置とサイズ")]
			[DisplayName("フォームの基本設定")]
			[Description("フォームの基本的な設定です．")]
			public FormSettingsBaseSettings FormSettingsBaseSettings {
				get { return this.formSettingsBaseSettings; }
				set { this.FormSettingsBaseSettings.ApplySettings(value); }
			}
			private FormSettingsBaseSettings formSettingsBaseSettings;

			[Category("表示")]
			[DisplayName("ジャンルメニューの色分け")]
			[Description("ジャンルメニューをジャンルごとに色分けして表示します．")]
			[DefaultValue(true)]
			public bool MenuGenreColored {
				get {
					if (this.HasOwner) {
						return this.owner.tsgmiGenreTop.GenreColored;
					} else {
						return this.menuGenreColored;
					}
				}
				set {
					if (this.HasOwner) {
						this.owner.tsgmiGenreTop.GenreColored = value;
						this.owner.tsgmiTimetableUpdate.GenreColored = value;
					} else {
						this.menuGenreColored = value;
					}
				}
			}
			private bool menuGenreColored = true;
			
			#region INewSettings<BrowserFormSettings> Members
			public void ApplySettings(BrowserFormSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}

		private abstract class CatalogStream : Stream {
			private MemoryStream memStream;

			public CatalogStream(string html) {
				this.memStream = new MemoryStream(Encoding.Default.GetBytes(html));
			}

			public sealed override bool CanRead {
				get { return this.memStream.CanRead; }
			}
			public sealed override bool CanSeek {
				get { return this.memStream.CanSeek; }
			}
			public sealed override bool CanWrite {
				get { return this.memStream.CanWrite; }
			}
			public sealed override void Flush() {
				this.memStream.Flush();
			}
			public sealed override long Length {
				get { return this.memStream.Length; }
			}
			public sealed override long Position {
				get { return this.memStream.Position; }
				set { this.memStream.Position = value; }
			}
			public sealed override int Read(byte[] buffer, int offset, int count) {
				return this.memStream.Read(buffer, offset, count);
			}
			public sealed override long Seek(long offset, SeekOrigin origin) {
				return this.memStream.Seek(offset, origin);
			}
			public sealed override void SetLength(long value) {
				this.memStream.SetLength(value);
			}
			public sealed override void Write(byte[] buffer, int offset, int count) {
				this.memStream.Write(buffer, offset, count);
			}

			protected override void Dispose(bool disposing) {
				if (disposing) {
					this.memStream.Dispose();
				}
				base.Dispose(disposing);
			}
		}
		
		private sealed class ImageCatalogStream : CatalogStream {
			private static string CreateCatalogHtml(IEnumerable<Uri> images) {
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("<html>");
				sb.AppendLine("<head><title>画像カタログ</title></head>");
				sb.AppendLine("<h1>画像カタログ</h1>");
				sb.AppendLine("<body><div>");
				foreach (Uri image in images) {
					sb.AppendLine(string.Format(@"<img src=""{0}""/>", image.AbsoluteUri));
				}
				sb.AppendLine("</div></body>");
				sb.AppendLine("</html>");
				return sb.ToString();
			}
			public ImageCatalogStream(IEnumerable<Uri> images)
				: base(ImageCatalogStream.CreateCatalogHtml(images)) {
			}
		}

		private sealed class PackageCatalogStream : CatalogStream {
			private static string CreateCatalogHtml(IEnumerable<PackageAdapter> pas, bool expandContents) {
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("<html>");
				sb.AppendLine("<head><title>パッケージカタログ</title></head>");
				sb.AppendLine("<body>");
				sb.AppendLine("<h1>パッケージカタログ</h1>");
				foreach (PackageAdapter pa in pas) {
					if (expandContents) {
						sb.AppendLine(@"<hr style=""clear:both;""/>");
					}
					sb.Append(@"<div style=""clear:both;"">");
					sb.AppendFormat(@"<img src=""{0}"" style=""float:left;""/>", pa.ImageMiddleUri.AbsoluteUri);
					sb.AppendFormat(@"<div style=""float:left;""><a href=""{0}"">&lt;{1}&gt; {2}</a><br/>{3}<p>{4}</p></div>",
						pa.PackagePageUri.AbsoluteUri, pa.PackageId, HttpUtility.HtmlEncode(pa.PackageName), HttpUtility.HtmlEncode(pa.CatchCopy), HttpUtility.HtmlEncode(pa.PackageText1));
					sb.AppendLine("</div>");
					if (expandContents) {
						foreach (ContentAdapter ca in pa.ContentAdapters) {
							sb.Append(@"<div style=""clear:both;margin-top:1px;"">");
							sb.AppendFormat(@"<img src=""{0}"" style=""float:left;""/>", ca.ImageSmallUri.AbsoluteUri);
							sb.AppendFormat(@"<div style=""float:left;""><a href=""{0}"">&lt;{1}&gt; {2} {3}</a><br/>{4}<br/><small>{5}&nbsp;&nbsp;&nbsp;&nbsp;{6}</small></div>",
								ca.DetailPageUri.AbsoluteUri, ca.ContentId, HttpUtility.HtmlEncode(ca.SeriesNumber), HttpUtility.HtmlEncode(ca.Subtitle), HttpUtility.HtmlEncode(ca.Summary), HttpUtility.HtmlEncode(ca.Duration), HttpUtility.HtmlEncode(ca.Deadline));
							sb.AppendLine("</div>");
						}
					}
				}
				sb.AppendLine("</body>");
				sb.AppendLine("</html>");
				return sb.ToString();
			}
			public PackageCatalogStream(IEnumerable<PackageAdapter> pas, bool expandContents)
				: base(PackageCatalogStream.CreateCatalogHtml(pas, expandContents)) {
			}
		}
		private sealed class ContentCatalogStream : CatalogStream {
			private static string CreateCatalogHtml(IEnumerable<ContentAdapter> cas) {
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("<html>");
				sb.AppendLine("<head><title>コンテンツカタログ</title></head>");
				sb.AppendLine("<body>");
				sb.AppendLine("<h1>コンテンツカタログ</h1>");
				foreach (ContentAdapter ca in cas) {
					sb.Append(@"<div style=""clear:both;"">");
					sb.AppendFormat(@"<img src=""{0}"" style=""float:left;""/>", ca.ImageSmallUri.AbsoluteUri);
					sb.AppendFormat(@"<div style=""float:left;""><a href=""{0}"">&lt;{1}&gt; {2}</a><br/>{3}<br/><small>{4}&nbsp;&nbsp;&nbsp;&nbsp;{5}</small></div>",
						ca.DetailPageUri.AbsoluteUri, ca.ContentId, HttpUtility.HtmlEncode(ca.DisplayName), HttpUtility.HtmlEncode(ca.Summary), HttpUtility.HtmlEncode(ca.Duration), HttpUtility.HtmlEncode(ca.Deadline));
					sb.AppendLine(@"</div>");
				}
				sb.AppendLine("</body>");
				sb.AppendLine("</html>");
				return sb.ToString();
			}
			public ContentCatalogStream(IEnumerable<ContentAdapter> cas)
				: base(ContentCatalogStream.CreateCatalogHtml(cas)) {
			}
		}
		
		private static BrowserForm instance = null;
		public static BrowserForm Instance {
			get {
				if(! BrowserForm.HasInstance) {
					BrowserForm.instance = new BrowserForm();
				}
				return BrowserForm.instance;
			}
		}
		public static bool HasInstance {
			get { return null != BrowserForm.instance && !BrowserForm.instance.IsDisposed; }
		}
		public static void Browse(Uri uri) {
			BrowserForm.Instance.Show();
			BrowserForm.Instance.Focus();
			BrowserForm.Instance.DocumentUri = uri;
		}
		public static void Browse(IEnumerable<Uri> images) {
			BrowserForm.Instance.Show();
			BrowserForm.Instance.Focus();
			BrowserForm.Instance.gwbMain.DocumentStream = new ImageCatalogStream(images);
		}
		public static void Browse(IEnumerable<PackageAdapter> pas, bool expandContents) {
			BrowserForm.Instance.Show();
			BrowserForm.Instance.Focus();
			BrowserForm.Instance.gwbMain.DocumentStream = new PackageCatalogStream(pas, expandContents);
		}
		public static void Browse(IEnumerable<ContentAdapter> cas) {
			BrowserForm.Instance.Show();
			BrowserForm.Instance.Focus();
			BrowserForm.Instance.gwbMain.DocumentStream = new ContentCatalogStream(cas);
		}
		
		private bool isSearchTextboxEmpty = true;
		private BrowserFormSettings settings;

		private BrowserForm() {
			InitializeComponent();
			this.tsmiSettings.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			
			if (base.DesignMode) return;

			this.tscbAddress.Items.Clear();
			foreach (GGenre g in GGenre.AllGenres) {
				this.tscbAddress.Items.Add(g.TopPageUri);
			}
		}
		private void BrowserForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;

			this.settings = new BrowserFormSettings(this);
			this.tspgBrowserForm.SelectedObject = this.settings;
			Utility.LoadSettingsAndEnableSaveOnClosedNew(this);
		}
		public string FilenameForSettings {
			get { return @"BrowserFormSettings.xml"; }
		}
		
		public Uri DocumentUri {
			get {
				return this.gwbMain.Url;
			}
			set {
				if (null == value) throw new ArgumentNullException();
				this.gwbMain.Url = value;
			}
		}
		
		private void gwbMain_CanGoBackChanged(object sender, EventArgs e) {
			this.tsbBack.Enabled = this.gwbMain.CanGoBack;
		}
		private void gwbMain_CanGoForwardChanged(object sender, EventArgs e) {
			this.tsbForward.Enabled = this.gwbMain.CanGoForward;
		}
		private void gwbMain_StatusTextChanged(object sender, EventArgs e) {
			string status = this.gwbMain.StatusText;
			if (status.Contains("&")) {
				status = status.Replace("&", "&&");
			}
			this.toolStripStatusLabel1.Text = status;
		}
		private void gwbMain_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e) {
			this.toolStripProgressBar1.Maximum = (int)e.MaximumProgress;
			this.toolStripProgressBar1.Value = (int)e.CurrentProgress;
		}
		private void gwbMain_DocumentTitleChanged(object sender, EventArgs e) {
			WebBrowser wb = sender as WebBrowser;
			string title = wb.DocumentTitle;
			if (string.IsNullOrEmpty(title)) {
				title = "<" + wb.Url.AbsoluteUri + ">";
			}
			this.Text = title;
		}
		
		private void gwbMain_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			this.tsbStop.Enabled = true;
		}
		private void gwbMain_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
			//this.tscbAddress.Text = e.Url.AbsoluteUri; //フレームページでもイベントが起きるっぽい
			this.tscbAddress.Text = this.gwbMain.Url.AbsoluteUri;
		}
		private void gwbMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			this.tsbStop.Enabled = false;
		}

		#region ツールバー

		private void tsbBack_Click(object sender, EventArgs e) {
			this.gwbMain.GoBack();
		}
		private void tsbForward_Click(object sender, EventArgs e) {
			this.gwbMain.GoForward();
		}
		private void tsbStop_Click(object sender, EventArgs e) {
			this.gwbMain.Stop();
		}
		private void GoToAddressBarUri(object sender, EventArgs e) {
			this.DocumentUri = new Uri(this.tscbAddress.Text);
			this.gwbMain.Focus();
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
		private void tstbLivedoor_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					this.SearchGyaoViaLivedoorVideoWithSearchText();
					break;
			}
		}
		private void tsbLivedoor_Click(object sender, EventArgs e) {
			this.SearchGyaoViaLivedoorVideoWithSearchText();
		}
		private void SearchGyaoViaLivedoorVideoWithSearchText() {
			if (this.isSearchTextboxEmpty) {
				this.tstbLivedoor.Focus();
			} else {
				string query = this.tstbLivedoor.Text;
				query = query.Trim();
				this.tstbLivedoor.Text = query;
				if (string.IsNullOrEmpty(query)) {
					this.tstbLivedoor.Focus();
				} else {
					this.DocumentUri = Utility.CreateLivedoorVideoGyaoSearchUri(query);
					this.gwbMain.Focus();
				}
			}
		}
		private void tstbLivedoor_Enter(object sender, EventArgs e) {
			if (this.isSearchTextboxEmpty) {
				this.tstbLivedoor.Text = string.Empty;
				this.tstbLivedoor.ForeColor = SystemColors.WindowText;
				this.isSearchTextboxEmpty = false;
			}
		}

		private void tstbLivedoor_Leave(object sender, EventArgs e) {
			if (string.IsNullOrEmpty(this.tstbLivedoor.Text)) {
				this.tstbLivedoor.Text = "livedoor動画でGyaO検索";
				this.tstbLivedoor.ForeColor = SystemColors.GrayText;
				this.isSearchTextboxEmpty = true;
			} else {
				this.isSearchTextboxEmpty = false;
			}
		}

		#endregion

		#region メニュー
		private void tsmiOpenTop_Click(object sender, EventArgs e) {
			this.DocumentUri = new Uri("http://www.gyao.jp/");
		}
		private void tsmiSaveAs_Click(object sender, EventArgs e) {
			this.gwbMain.ShowSaveAsDialog();
		}
		private void tsmiPageProperty_Click(object sender, EventArgs e) {
			this.gwbMain.ShowPropertiesDialog();
		}
		private void tsmiPageSetup_Click(object sender, EventArgs e) {
			this.gwbMain.ShowPageSetupDialog();
		}
		private void tsmiPrint_Click(object sender, EventArgs e) {
			this.gwbMain.ShowPrintDialog();
		}
		private void tsmiPrintPreview_Click(object sender, EventArgs e) {
			this.gwbMain.ShowPrintPreviewDialog();
		}
		private void tsmiClose_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void tsgmiGenreTop_GenreSelected(object sender, GenreMenuItemSelectedEventArgs e) {
			this.DocumentUri = e.SelectedGenre.TopPageUri;
		}
		private void tsgmiTimetableUpdate_GenreSelected(object sender, GenreMenuItemSelectedEventArgs e) {
			this.DocumentUri = e.SelectedGenre.TimetableRecentlyUpdatedFirstUri;
		}
		private void tsmiExportContentListToCrawlResultView_Click(object sender, EventArgs e) {
			//重複なしでキーを並べる
			List<int> keys = new List<int>();
			using (IEnumerator<int> allkeys = this.gwbMain.GetContentKeyEnumerator()) {
				while (allkeys.MoveNext()) {
					if (!keys.Contains(allkeys.Current)) {
						keys.Add(allkeys.Current);
					}
				}
			}
			//キャッシュから取り出す
			List<GContent> conts = new List<GContent>(keys.Count);
			foreach (int key in keys) {
				ContentCache cache;
				if (Cache.Instance.ContentCacheController.TryGetCache(key, out cache)) {
					conts.Add(cache.Content);
				} else {
					conts.Add(GContent.CreateDummyContent(key, ContentAdapter.UnknownGenre.Default, "当該IDと同一のキャッシュを持っていない"));
				}
			}
			//表示
			MainForm.Instance.ViewFlatCrawlResult(conts);
		}
		private void tsmiExtractImages_Click(object sender, EventArgs e) {
			List<Uri> images = new List<Uri>();
			foreach (HtmlElement elem in this.gwbMain.Document.Images) {
				string src = elem.GetAttribute("src");
				if (string.IsNullOrEmpty(src)) continue;
				try {
					images.Add(new Uri(src));
				} catch (UriFormatException) {
					continue;
				}
			}
			BrowserForm.Browse(images);
		}
		private void tsmiTimetableExpandAll_Click(object sender, EventArgs e) {
			this.gwbMain.Url = new Uri("javascript:var c=document.getElementsByTagName('input');for(i in c){var n=c[i].name;if(n!=null&&n.indexOf('symbol[pac')==0)c[i].value='minus';}document.all['form2'].submit();");
		}
		private void tsmiTimetableCollapseAll_Click(object sender, EventArgs e) {
			this.gwbMain.Url = new Uri("javascript:var c=document.getElementsByTagName('input');for(i in c){var n=c[i].name;if(n!=null&&n.indexOf('symbol[pac')==0)c[i].value='plus';}document.all['form2'].submit();");
		}
		private void tsmiGotoCampaign_Click(object sender, EventArgs e) {
			this.gwbMain.Url = new Uri("javascript:gotoCampaign();");
		}
		private void tsmiFillCampaignForm_Click(object sender, EventArgs e) {
			GlobalSettings gs = GlobalSettings.Instance;
			if (!gs.IsValidFormData) {
				MessageBox.Show("ユーザ情報が設定されていません．\nグローバル設定でユーザ情報を入力してから実行してください．", "応募フォームのフィル", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try {
				this.SetFormText("name1", gs.NameFamily);
				this.SetFormText("name2", gs.NameFirst);
				this.SetFormText("email1", gs.EmailAddress);
				this.SetFormText("email2", gs.EmailAddress);
			} catch (Exception ex) {
				MessageBox.Show("フィル失敗\n\n" + ex.Message, "応募フォームのフィル", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion

		private void SetFormText(string htmlId, string innerText) {
			HtmlElement elem = this.gwbMain.Document.GetElementById(htmlId);
			if (null == elem) {
				throw new InvalidOperationException(htmlId + " のIDを持つ要素が見つからなかった．");
			}
			elem.InnerText = innerText;
		}

		#region IHasNewSettings<BrowserFormSettings> Members
		public BrowserForm.BrowserFormSettings Settings {
			get { return this.settings; }
		}
		#endregion

	}
}
