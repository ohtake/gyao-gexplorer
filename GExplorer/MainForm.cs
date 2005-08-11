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
			settings.ListViewWidth = this.splitContainer1.SplitterDistance;
			this.genreListView1.FillSettings(settings.GenreListViewSettings);
			this.contentDetailView1.FillSettings(settings.ContentDetailViewSettings);
		}
		public void ApplySettings(MainFormSettings settings) {
			base.ApplySettings(settings);
			this.IgnoreCrawlErrors = settings.IgnoreCrawlErrors??this.IgnoreCrawlErrors;
			this.splitContainer1.SplitterDistance = settings.ListViewWidth ?? this.splitContainer1.SplitterDistance;
			this.genreListView1.ApplySettings(settings.GenreListViewSettings);
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

		private void genreListView1_SelectedContentChanged(object sender, SelectedContentChangedEventArgs e) {
			if (e.IsSelected) {
				this.contentDetailView1.Content = e.Content;
			}
		}
		private void genreListView1_GenreShowed(object sender, GenreListViewGenreShowedEventArgs e) {
			if (null != e.Genre) {
				this.toolStripProgressBar1.Value = 0;
				this.toolStripStatusLabel1.Text =
					"[" + e.Genre.GenreName + "]"
					+ " " + e.NumberOfContents.ToString() + "個のコンテンツ"
					+ " (クロール時刻 " + e.Genre.LastCrawlTime.ToShortTimeString() + ")";
			}
		}

		private void genreTabControl1_GenreDoubleClick(object sender, GenreTabPageEventArgs e) {
			GGenre genre = e.Genre;
			if (null == genre) return;
			CrawlResult result = this.crawler.Crawl(genre);
			this.ClearStatusBarInfo();
			if (result.Success) {
				this.genreListView1.Genre = genre;
			}
		}
		
		private void genreTabControl1_GenreSelected(object sender, GenreTabPageEventArgs e) {
			GGenre genre = e.Genre;
			if (null == genre) return;
			if (!genre.HasCrawled) {
				CrawlResult result = this.crawler.Crawl(genre);
				this.ClearStatusBarInfo();
				if (result.Success) {
					this.genreListView1.Genre = genre;
				}
			} else {
				this.genreListView1.Genre = genre;
			}
		}

		private void tsmiQuit_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void tsmiBrowseTop_Click(object sender, EventArgs e) {
			Utility.BrowseWithIE(new Uri("http://www.gyao.jp/"));
		}
		private void tsmiPlayerForm_Click(object sender, EventArgs e) {
			PlayerForm.ShowAndFocus();
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
		private GenreListViewSettings genreListViewSettings = new GenreListViewSettings();
		private ContentDetailViewSettings contentDetailViewSettings = new ContentDetailViewSettings();

		public bool? IgnoreCrawlErrors {
			get { return this.ignoreCrawlErrors; }
			set { this.ignoreCrawlErrors = value; }
		}
		public int? ListViewWidth {
			get { return this.listViewWidth; }
			set { this.listViewWidth = value; }
		}
		public GenreListViewSettings GenreListViewSettings {
			get { return this.genreListViewSettings; }
			set { this.genreListViewSettings = value; }
		}
		public ContentDetailViewSettings ContentDetailViewSettings {
			get { return this.contentDetailViewSettings; }
			set { this.contentDetailViewSettings = value; }
		}
	}
}
