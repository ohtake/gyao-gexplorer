using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Web;
using System.Drawing;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.AppCore;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class BrowserForm : BaseForm{
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
		
		private bool isSearchTextboxEmpty = true;

		public BrowserForm() {
			InitializeComponent();

			this.gwbMain.StatusTextChanged += new EventHandler(this.gwbMain_StatusTextChanged);
			this.gwbMain.CanGoForwardChanged += new EventHandler(this.gwbMain_CanGoForwardChanged);
			this.gwbMain.CanGoBackChanged += new EventHandler(this.gwbMain_CanGoBackChanged);
			this.gwbMain.DocumentTitleChanged += new EventHandler(this.gwbMain_DocumentTitleChanged);
		}
		private void BrowserForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;

			if (Program.CacheController == null) return;
			this.tscbAddress.Items.Clear();
			foreach (GGenreClass genre in Program.CacheController.GetEnumerableOfAllGenres()) {
				this.tscbAddress.Items.Add(genre.GenreTopPageUri);
			}
			if (null == Program.RootOptions) return;
			Program.RootOptions.BrowserFormOptions.ApplyFormBaseOptionsAndTrackValues(this);
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
		public void ViewImageCatalog(IEnumerable<Uri> imageUris) {
			this.gwbMain.DocumentStream = new ImageCatalogStream(imageUris);
		}

		#region WebBrowserのイベントハンドラ
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
		#endregion

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
					this.DocumentUri = GUriBuilder.CreateLivedoorVideoGyaoSearchUri(query);
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
			this.DocumentUri = GUriBuilder.TopPageUri;
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
		private void tsgmiGenreTop_GenreSelected(object sender, EventArgs e) {
			GGenreClass genre = this.tsgmiGenreTop.LastSelectedGenre;
			this.DocumentUri = genre.GenreTopPageUri;
		}
		private void tsgmiTimetableUpdated_GenreSelected(object sender, EventArgs e) {
			GGenreClass genre = this.tsgmiTimetableUpdated.LastSelectedGenre;
			this.DocumentUri = genre.TimetableRecentlyUpdatedFirstUri;
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
			int ignoreCount = 0;
			List<GContentClass> conts = new List<GContentClass>(keys.Count);
			foreach (int key in keys) {
				GContentClass cont;
				if (Program.CacheController.TryFindContent(key, out cont)) {
					conts.Add(cont);
				} else {
					ignoreCount++;
				}
			}
			//表示
			if (ignoreCount > 0) {
				MessageBox.Show(string.Format("{0}個のコンテンツはキャッシュがなかったため無視しました．", ignoreCount), "キャッシュなし", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			Program.AddVirtualGenre(new SimpleContentsVirtualGenre(conts, "ブラウザ", "ブラウザからの書き出し" + Environment.NewLine + this.gwbMain.Url.AbsoluteUri));
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
			this.ViewImageCatalog(images);
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
			AppBasicOptions abo = Program.RootOptions.AppBasicOptions;
			if (!abo.IsValidFormData) {
				MessageBox.Show("ユーザ情報が設定されていません．\nオプションの基本設定でユーザ情報を入力してから実行してください．", "応募フォームのフィル", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try {
				this.SetFormText("name1", abo.NameFamily);
				this.SetFormText("name2", abo.NameFirst);
				this.SetFormText("email1", abo.EmailAddress);
				this.SetFormText("email2", abo.EmailAddress);
			} catch (Exception ex) {
				MessageBox.Show("フィル失敗\n\n" + ex.Message, "応募フォームのフィル", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void tsmiOptions_Click(object sender, EventArgs e) {
			Program.ShowOptionsForm();
		}
		#endregion

		private void SetFormText(string htmlId, string innerText) {
			HtmlElement elem = this.gwbMain.Document.GetElementById(htmlId);
			if (null == elem) {
				throw new InvalidOperationException(htmlId + " のIDを持つ要素が見つからなかった．");
			}
			elem.InnerText = innerText;
		}

	}

	public sealed class BrowserFormOptions : FormOptionsBase {
	}
}
