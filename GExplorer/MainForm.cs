using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	partial class MainForm : FormSettingsBase, IFormWithSettings<MainFormSettings> {
		private delegate void ViewCrawlResultDelegate(CrawlResult result);

		private Crawler crawler = new Crawler(new HtmlParserRegex(), new ContentCacheControllerXml("Cache"));
		private Dictionary<GGenre, CrawlResult> results = new Dictionary<GGenre, CrawlResult>();
		private Thread threadCrawler = null;

		public MainForm() {
			InitializeComponent();
		}

		private void ClearStatusBarInfo() {
			if (this.InvokeRequired) {
				this.Invoke(new ThreadStart(delegate {
					this.ClearStatusBarInfo();
				}));
			} else {
				this.tspbCrawl.Value = 0;
				this.tsslCrawl.Text = "";
			}
		}
		private void DisableTsmiAbortCrawling() {
			if (this.InvokeRequired) {
				this.Invoke(new ThreadStart(delegate {
					this.DisableTsmiAbortCrawling();
				}));
			} else {
				this.tsmiAbortCrawling.Enabled = false;
			}
		}
		private void ViewCrawlResult(CrawlResult result) {
			if (this.InvokeRequired) {
				this.Invoke(new ViewCrawlResultDelegate(this.ViewCrawlResult), result);
			} else {
				this.crawlResultView1.CrawlResult = result;
				if (this.FocusOnResultAfterGenreChanged) {
					this.crawlResultView1.Focus();
				}
			}
		}
		public void FillSettings(MainFormSettings settings) {
			base.FillSettings(settings);
			settings.FocusOnResultAfterGenreChanged = this.FocusOnResultAfterGenreChanged;
			settings.IgnoreCrawlErrors = this.IgnoreCrawlErrors;
			settings.ListViewWidth = this.scListsAndDetail.SplitterDistance;
			settings.ListViewHeight = this.scLists.SplitterDistance;
			this.crawlResultView1.FillSettings(settings.GenreListViewSettings);
			this.playListView1.FillSettings(settings.PlayListViewSettings);
			this.contentDetailView1.FillSettings(settings.ContentDetailViewSettings);
		}
		public void ApplySettings(MainFormSettings settings) {
			base.ApplySettings(settings);
			this.FocusOnResultAfterGenreChanged = settings.FocusOnResultAfterGenreChanged ?? this.FocusOnResultAfterGenreChanged;
			this.IgnoreCrawlErrors = settings.IgnoreCrawlErrors ?? this.IgnoreCrawlErrors;
			this.scListsAndDetail.SplitterDistance = settings.ListViewWidth ?? this.scListsAndDetail.SplitterDistance;
			this.scLists.SplitterDistance = settings.ListViewHeight ?? this.scLists.SplitterDistance;
			this.crawlResultView1.ApplySettings(settings.GenreListViewSettings);
			this.playListView1.ApplySettings(settings.PlayListViewSettings);
			this.contentDetailView1.ApplySettings(settings.ContentDetailViewSettings);
		}
		public string FilenameForSettings {
			get { return @"MainFormSettings.xml"; }
		}

		public bool FocusOnResultAfterGenreChanged {
			get { return this.tsmiFocusOnResultAfterTabChanged.Checked; }
			set { this.tsmiFocusOnResultAfterTabChanged.Checked = value; }
		}
		public bool IgnoreCrawlErrors {
			get { return this.tsmiIgnoreCrawlErrors.Checked; }
			set { this.tsmiIgnoreCrawlErrors.Checked = value; }
		}

		private void MainForm_Load(object sender, EventArgs e) {
			this.Text = Application.ProductName + " " + Application.ProductVersion;
			Utility.AppendHelpMenu(this.menuStrip1);

			this.genreTabControl1.TabPages.Clear();
			this.tsmiUncrawableGenres.DropDownItems.Clear();
			foreach (GGenre genre in GGenre.AllGenres) {
				if (genre.IsCrawlable) {
					this.genreTabControl1.AddGenre(genre);
				} else {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(genre.GenreName);
					tsmi.Tag = genre;
					tsmi.Click += delegate(object sender2, EventArgs e2) {
						Utility.Browse(((sender2 as ToolStripMenuItem).Tag as GGenre).TopPageUri);
					};
					this.tsmiUncrawableGenres.DropDownItems.Add(tsmi);
				}
			}
			this.genreTabControl1.SelectedIndex = -1;

			this.crawler.CrawlProgress += new EventHandler<CrawlProgressEventArgs>(crawler_CrawlProgress);
			this.crawler.IgnorableErrorOccured += new EventHandler<IgnorableErrorOccuredEventArgs>(crawler_IgnorableErrorOccured);

			Utility.LoadSettingsAndEnableSaveOnClosed(this);
			this.ClearStatusBarInfo();
		}

		private void crawler_CrawlProgress(object sender, CrawlProgressEventArgs e) {
			if (this.InvokeRequired) {
			this.Invoke(new EventHandler<CrawlProgressEventArgs>(
				delegate(object sender2, CrawlProgressEventArgs e2) {
					this.crawler_CrawlProgress(sender2, e2);
				}),
				sender, e);
			} else {
				this.tspbCrawl.Maximum = e.Visited+e.Waiting;
				this.tspbCrawl.Value = e.Visited;
				this.tsslCrawl.Text = e.Message;
			}
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

		private void genreTabControl1_GenreSelected(object sender, GenreTabPageEventArgs e) {
			GGenre genre = e.Genre;
			if (null == genre) return;
			CrawlResult result;
			if (e.ForceReload || !this.results.TryGetValue(genre, out result)) {
				Thread t;
				lock (this.crawler) {
					if (null != this.threadCrawler) {
						MessageBox.Show("多重クロールは禁止．", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
						return;
					}
					t = new Thread(new ThreadStart(delegate {
						result = this.crawler.Crawl(genre);
						lock (this.crawler) {
							this.threadCrawler = null;
							this.DisableTsmiAbortCrawling();
						}
						if (result.Success) {
							this.ClearStatusBarInfo();
							this.results[genre] = result;
							this.ViewCrawlResult(result);
						}
					}));
					this.threadCrawler = t;
					this.tsmiAbortCrawling.Enabled = true;
					t.Start();
				}
			} else {
				this.ViewCrawlResult(result);
			}
		}

		private void crawlResultView1_ContentSelectionChanged(object sender, ContentSelectionChangedEventArgs e) {
			if (e.IsSelected) {
				this.contentDetailView1.Content = e.Content;
			}
		}
		private void playListView1_ContentSelectionChanged(object sender, ContentSelectionChangedEventArgs e) {
			if (e.IsSelected) {
				this.contentDetailView1.Content = e.Content;
			}
		}


		private void tsmiQuit_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void tsmiBrowseTop_Click(object sender, EventArgs e) {
			Utility.Browse(new Uri("http://www.gyao.jp/"));
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
		private void tsmiAbortCrawling_Click(object sender, EventArgs e) {
			lock (this.crawler) {
				if (null != this.threadCrawler) {
					this.threadCrawler.Abort();
					this.tsmiAbortCrawling.Enabled = false;
					this.threadCrawler = null;

					this.ClearStatusBarInfo();
					this.tsslCrawl.Text = "クロールを中止しました．";
				}
			}
		}
	}

	public class MainFormSettings : FormSettingsBaseSettings {
		private bool? focusOnResultAfterGenreChanged;
		private bool? ignoreCrawlErrors;
		private int? listViewWidth;
		private int? listViewHeight;
		private GenreListViewSettings genreListViewSettings = new GenreListViewSettings();
		private PlayListViewSettings playListViewSettings = new PlayListViewSettings();
		private ContentDetailViewSettings contentDetailViewSettings = new ContentDetailViewSettings();

		public bool? FocusOnResultAfterGenreChanged {
			get { return this.focusOnResultAfterGenreChanged; }
			set { this.focusOnResultAfterGenreChanged = value; }
		}
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
