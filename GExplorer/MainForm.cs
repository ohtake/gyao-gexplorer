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
			crawler.CrawlProgress += new EventHandler<CrawlProgressEventArgs>(crawler_CrawlProgress);

			Utility.LoadSettingsAndEnableSaveOnClosed(this);
		}

		private void ClearStatusBarInfo() {
			this.toolStripProgressBar1.Value = 0;
			this.toolStripStatusLabel1.Text = "";
		}
		public void FillSettings(MainFormSettings settings) {
			base.FillSettings(settings);
			settings.ListViewWidth = this.splitContainer1.SplitterDistance;
			this.genreListView1.FillSettings(settings.GenreListViewSettings);
			this.contentDetailView1.FillSettings(settings.ContentDetailViewSettings);
		}
		public void ApplySettings(MainFormSettings settings) {
			base.ApplySettings(settings);
			this.splitContainer1.SplitterDistance = settings.ListViewWidth ?? this.splitContainer1.SplitterDistance;
			this.genreListView1.ApplySettings(settings.GenreListViewSettings);
			this.contentDetailView1.ApplySettings(settings.ContentDetailViewSettings);
		}
		public string FilenameForSettings {
			get { return @"MainFormSettings.xml"; }
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

		private void genreListView1_SelectedContentChanged(object sender, SelectedContentChangedEventArgs e) {
			this.contentDetailView1.Content = e.Content;
		}

		private void genreTabControl1_GenreDoubleClick(object sender, GenreTabPageEventArgs e) {
			GGenre genre = e.Genre;
			if (null == genre) return;
			this.crawler.Crawl(genre);
			this.genreListView1.Genre = genre;
			this.ClearStatusBarInfo();
		}

		private void genreTabControl1_GenreSelected(object sender, GenreTabPageEventArgs e) {
			GGenre genre = e.Genre;
			if (null == genre) return;
			if (!genre.HasCrawled) {
				this.crawler.Crawl(genre);
			}
			this.genreListView1.Genre = genre;
			this.ClearStatusBarInfo();
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
		private int? listViewWidth;
		private GenreListViewSettings genreListViewSettings = new GenreListViewSettings();
		private ContentDetailViewSettings contentDetailViewSettings = new ContentDetailViewSettings();
		
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
