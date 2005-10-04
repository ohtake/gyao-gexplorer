using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using WMPLib;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	sealed partial class BrowserForm : FormSettingsBase, IFormWithSettings<BrowserFormSettings>{
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
		
		private BrowserForm() {
			InitializeComponent();
		}
		private void BrowserForm_Load(object sender, EventArgs e) {
			Utility.AppendHelpMenu(this.menuStrip1);
			
			this.tsmiGenres.DropDownItems.Clear();
			this.tsmiTimeTables.DropDownItems.Clear();
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
					this.DocumentUri = genre.TimetableUri;
				});
				this.tsmiTimeTables.DropDownItems.Add(mi);

				this.tscbAddress.Items.Add(g.TopPageUri);
				this.tscbAddress.Items.Add(g.TimetableUri);
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
			this.toolStripStatusLabel1.Text = this.gwbMain.StatusText;
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
		#endregion
	}

	public class BrowserFormSettings : FormSettingsBaseSettings {
	}
}
