using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	partial class MainForm : FormSettingsBase , IFormWithSettings<MainFormSettings>{
		private Crawler crawler = new Crawler(new HtmlParserRegex(), new ContentCacheControllerXml("Cache"));
		private Dictionary<GGenre, CrawlResult> results = new Dictionary<GGenre, CrawlResult>();
		
		public MainForm() {
			InitializeComponent();
			this.Text = Application.ProductName + " " + Application.ProductVersion;
			Utility.AppendHelpMenu(this.menuStrip1);
			
			this.genreTabControl1.TabPages.Clear();
			foreach (GGenre genre in GGenre.AllGenres) {
				this.genreTabControl1.AddGenre(genre);
			}
			this.genreTabControl1.SelectedIndex = -1;

			this.crawler.CrawlProgress += new EventHandler<CrawlProgressEventArgs>(crawler_CrawlProgress);
			this.crawler.IgnorableErrorOccured += new EventHandler<IgnorableErrorOccuredEventArgs>(crawler_IgnorableErrorOccured);
			
			Utility.LoadSettingsAndEnableSaveOnClosed(this);
		}
		
		private void ClearStatusBarInfo() {
			this.toolStripProgressBar1.Value = 0;
			this.toolStripStatusLabel1.Text = "";
		}
		public void FillSettings(MainFormSettings settings) {
			base.FillSettings(settings);
			settings.IgnoreCrawlErrors = this.IgnoreCrawlErrors;
			settings.ListViewWidth = this.scListsAndDetail.SplitterDistance;
			settings.ListViewHeight = this.scLists.SplitterDistance;
			this.crawlResultView1.FillSettings(settings.GenreListViewSettings);
			this.playListView1.FillSettings(settings.PlayListViewSettings);
			this.contentDetailView1.FillSettings(settings.ContentDetailViewSettings);
		}
		public void ApplySettings(MainFormSettings settings) {
			base.ApplySettings(settings);
			this.IgnoreCrawlErrors = settings.IgnoreCrawlErrors??this.IgnoreCrawlErrors;
			this.scListsAndDetail.SplitterDistance = settings.ListViewWidth ?? this.scListsAndDetail.SplitterDistance;
			this.scLists.SplitterDistance = settings.ListViewHeight ?? this.scLists.SplitterDistance;
			this.crawlResultView1.ApplySettings(settings.GenreListViewSettings);
			this.playListView1.ApplySettings(settings.PlayListViewSettings);
			this.contentDetailView1.ApplySettings(settings.ContentDetailViewSettings);
		}
		public string FilenameForSettings {
			get { return @"MainFormSettings.xml"; }
		}
		
		public bool IgnoreCrawlErrors {
			get {return this.tsmiIgnoreCrawlErrors.Checked;}
			set {this.tsmiIgnoreCrawlErrors.Checked = value;}
		}
		
		private void MainForm_Load(object sender, EventArgs e) {
			this.ClearStatusBarInfo();
		}

		private void crawler_CrawlProgress(object sender, CrawlProgressEventArgs e) {
			this.toolStripProgressBar1.Maximum = e.Visited+e.Waiting;
			this.toolStripProgressBar1.Value = e.Visited;
			this.toolStripStatusLabel1.Text = e.Message;
			Application.DoEvents();
		}
		void crawler_IgnorableErrorOccured(object sender, IgnorableErrorOccuredEventArgs e) {
			if (!this.IgnoreCrawlErrors) {
				if (DialogResult.No == MessageBox.Show(
						"クロール時に以下の無視可能エラーが起きました．無視して続行しますか？\n\n"
						+ e.Message,
						"クロール時の無視可能エラー",
						MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
					e.Ignore = false;
				}
			}
		}

		private void crawlResultView1_SelectedContentsChanged(object sender, SelectedContentsChangedEventArgs e) {
			if (e.Contents.Count > 0) {
				this.contentDetailView1.Content = e.Contents[0];
			}
		}
		private void playListView1_SelectedContentsChanged(object sender, SelectedContentsChangedEventArgs e) {
			if (e.Contents.Count > 0) {
				this.contentDetailView1.Content = e.Contents[0];
			}
		}

		private void genreTabControl1_GenreDoubleClick(object sender, GenreTabPageEventArgs e) {
			GGenre genre = e.Genre;
			if (null == genre) return;
			CrawlResult result = this.crawler.Crawl(genre);
			this.ClearStatusBarInfo();
			if (result.Success) {
				this.results[genre] = result;
				this.crawlResultView1.CrawlResult = result;
			}
		}
		
		private void genreTabControl1_GenreSelected(object sender, GenreTabPageEventArgs e) {
			GGenre genre = e.Genre;
			if (null == genre) return;
			CrawlResult result;
			if (this.results.TryGetValue(genre, out result)) {
				this.crawlResultView1.CrawlResult = result;
			} else {
				result = this.crawler.Crawl(genre);
				this.ClearStatusBarInfo();
				if (result.Success) {
					this.results[genre] = result;
					this.crawlResultView1.CrawlResult = result;
				}
			}
		}

		private void tsmiQuit_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void tsmiBrowseTop_Click(object sender, EventArgs e) {
			Utility.BrowseWithIE(new Uri("http://www.gyao.jp/"));
		}
		private void tsmiGlobalSettingsEditor_Click(object sender, EventArgs e) {
			GlobalSettingsEditor.Instance.Show();
			GlobalSettingsEditor.Instance.Focus();
		}
		private void tsmiUserCommandsEditor_Click(object sender, EventArgs e) {
			UserCommandsEditor.Instance.Show();
			UserCommandsEditor.Instance.Focus();
		}
		private void tsmiNgContentsEditor_Click(object sender, EventArgs e) {
			NgContentsEditor.Instance.Show();
			NgContentsEditor.Instance.Focus();
		}
	}

	public class MainFormSettings : FormSettingsBaseSettings {
		private bool? ignoreCrawlErrors;
		private int? listViewWidth;
		private int? listViewHeight;
		private GenreListViewSettings genreListViewSettings = new GenreListViewSettings();
		private PlayListViewSettings playListViewSettings = new PlayListViewSettings();
		private ContentDetailViewSettings contentDetailViewSettings = new ContentDetailViewSettings();

		public bool? IgnoreCrawlErrors {
			get { return this.ignoreCrawlErrors; }
			set { this.ignoreCrawlErrors = value; }
		}
		public int? ListViewWidth {
			get { return this.listViewWidth; }
			set { this.listViewWidth = value; }
		}
		public int? ListViewHeight {
			get { return this.listViewHeight; }
			set { this.listViewHeight = value; }
		}
		public GenreListViewSettings GenreListViewSettings {
			get { return this.genreListViewSettings; }
			set { this.genreListViewSettings = value; }
		}
		public PlayListViewSettings PlayListViewSettings {
			get { return this.playListViewSettings; }
			set { this.playListViewSettings = value; }
		}
		public ContentDetailViewSettings ContentDetailViewSettings {
			get { return this.contentDetailViewSettings; }
			set { this.contentDetailViewSettings = value; }
		}
	}
}
