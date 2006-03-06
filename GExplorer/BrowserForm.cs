using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yusen.GCrawler;
using System.IO;
using System.Text;

namespace Yusen.GExplorer {
	sealed partial class BrowserForm : FormSettingsBase, IFormWithSettings<BrowserFormSettings>{
		private sealed class ImageCatalogStream : Stream {
			private MemoryStream memStream;

			public ImageCatalogStream(IEnumerable<Uri> images) {
				this.memStream = new MemoryStream(Encoding.Default.GetBytes(this.CreateCatalogHtml(images)));
			}

			private string CreateCatalogHtml(IEnumerable<Uri> images) {
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("<html>");
				sb.AppendLine("<head><title>画像一覧</title></head>");
				sb.AppendLine("<body><div>");
				foreach (Uri image in images) {
					sb.AppendLine(string.Format(@"<img src=""{0}""/>", image.AbsoluteUri));
				}
				sb.AppendLine("</div></body>");
				sb.AppendLine("</html>");
				return sb.ToString();
			}

			public override bool CanRead {
				get { return this.memStream.CanRead; }
			}
			public override bool CanSeek {
				get { return this.memStream.CanSeek; }
			}
			public override bool CanWrite {
				get { return this.memStream.CanWrite; }
			}
			public override void Flush() {
				this.memStream.Flush();
			}
			public override long Length {
				get { return this.memStream.Length; }
			}
			public override long Position {
				get { return this.memStream.Position; }
				set { this.memStream.Position = value; }
			}
			public override int Read(byte[] buffer, int offset, int count) {
				return this.memStream.Read(buffer, offset, count);
			}
			public override long Seek(long offset, SeekOrigin origin) {
				return this.memStream.Seek(offset, origin);
			}
			public override void SetLength(long value) {
				this.memStream.SetLength(value);
			}
			public override void Write(byte[] buffer, int offset, int count) {
				this.memStream.Write(buffer, offset, count);
			}

			protected override void Dispose(bool disposing) {
				if (disposing) {
					this.memStream.Dispose();
				}
				base.Dispose(disposing);
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
		
		private BrowserForm() {
			InitializeComponent();
		}
		private void BrowserForm_Load(object sender, EventArgs e) {
			Utility.AppendHelpMenu(this.menuStrip1);
			
			this.tsmiGenres.DropDownItems.Clear();
			this.tsmiTimeTablesUpdated.DropDownItems.Clear();
			this.tscbAddress.Items.Clear();
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
					this.DocumentUri = genre.TimetableRecentlyUpdatedFirstUri;
				});
				this.tsmiTimeTablesUpdated.DropDownItems.Add(mi);

				mi = new ToolStripMenuItem(g.GenreName);
				mi.Tag = g;
				mi.Click += new EventHandler(delegate(object sender2, EventArgs e2) {
					GGenre genre = (sender2 as ToolStripMenuItem).Tag as GGenre;
					this.DocumentUri = genre.TimetableDeadlineNearFirstUri;
				});
				this.tsmiTimeTablesDeadline.DropDownItems.Add(mi);
				
				this.tscbAddress.Items.Add(g.TopPageUri);
			}
			
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
		private void tsmiGotoCampaign_Click(object sender, EventArgs e) {
			this.gwbMain.GotoCampaign();
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
		private void tsmiFillCampaignForm_Click(object sender, EventArgs e) {
			GlobalSettings gs = GlobalSettings.Instance;
			if (!gs.IsValidFormData) {
				MessageBox.Show("ユーザ情報が設定されていません．\nグローバル設定エディタでユーザ情報を入力してから実行してください．", "応募フォームのフィル", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
	}

	public class BrowserFormSettings : FormSettingsBaseSettings {
	}
}
