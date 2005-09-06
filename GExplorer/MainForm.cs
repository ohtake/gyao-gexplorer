using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Yusen.GCrawler;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace Yusen.GExplorer {
	partial class MainForm : FormSettingsBase, IFormWithSettings<MainFormSettings> {
		private delegate void ViewCrawlResultDelegate(CrawlResult result);
		private const string cacheDir = @"Cache";
		private const string cacheResults = @"CrawlResults.bin";
		
		private IContentCacheController cacheController;
		private Crawler crawler;
		private Dictionary<GGenre, CrawlResult> results = new Dictionary<GGenre, CrawlResult>();
		private Thread threadCrawler = null;

		public MainForm() {
			InitializeComponent();
			this.cacheController = new ContentCacheControllerXml(MainForm.cacheDir);
			this.crawler = new Crawler(new HtmlParserRegex(), this.cacheController);
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

		private void SerializeCrawlResults() {
			using (Stream stream = new FileStream(Path.Combine(MainForm.cacheDir, MainForm.cacheResults), FileMode.Create)) {
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, this.results);
			}
		}
		private bool TryDeserializeCrawlResults() {
			try {
				using (Stream stream = new FileStream(Path.Combine(MainForm.cacheDir, MainForm.cacheResults), FileMode.Open)) {
					IFormatter formatter = new BinaryFormatter();
					this.results = (Dictionary<GGenre, CrawlResult>)formatter.Deserialize(stream);
				}
				
				//かつてのバージョンではあったが新しいバージョンではなくなったジャンルを削除
				List<GGenre> newGenres = new List<GGenre>();
				foreach (GGenre genre in GGenre.AllGenres) {
					if (genre.IsCrawlable) {
						newGenres.Add(genre);
					}
				}
				List<GGenre> oldGenres = new List<GGenre>();
				foreach (GGenre genre in this.results.Keys) {
					if (!newGenres.Contains(genre)) {
						oldGenres.Add(genre);
					}
				}
				foreach (GGenre genre in oldGenres) {
					this.results.Remove(genre);
				}
				
				return true;
			} catch {
				return false;
			}
		}

		private void MainForm_Load(object sender, EventArgs e) {
			this.TryDeserializeCrawlResults();

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
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
			this.SerializeCrawlResults();
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
						MessageBox.Show(
							"多重クロールは禁止．", Application.ProductName,
							MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
		private void tsmiRemoveCachesUnreachable_Click(object sender, EventArgs e) {
			string title = "クロール結果で到達不可能なキャッシュを削除";
			switch (MessageBox.Show(
					"各ジャンルのクロール結果に表示されていないキャッシュを削除します．\nよろしいですか？",
					title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
				case DialogResult.Yes:
					List<string> reachable = new List<string>();
					foreach(CrawlResult result in this.results.Values){
						foreach (GPackage package in result.Packages) {
							foreach (GContent content in package.Contents) {
								reachable.Add(content.ContentId);
							}
						}
					}
					reachable.Sort();
					
					int success = 0;
					int failed = 0;
					int ignored = 0;
					foreach (string key in this.cacheController.ListAllCacheKeys()) {
						if (reachable.BinarySearch(key) >= 0) {
							ignored++;
						} else {
							if (this.cacheController.RemoveCache(key)) {
								success++;
							} else {
								failed++;
							}
						}
					}
					MessageBox.Show(
						ignored.ToString() + " 個のキャッシュは到達可能により削除しませんでした．\n"
						+ success.ToString() + " 個のキャッシュを削除しました．\n"
						+ failed.ToString() + " 個のキャッシュの削除に失敗しました．",
						title, MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
			}
		}
		private void tsmiRemoveCachesAll_Click(object sender, EventArgs e) {
			string title = "全てのキャッシュを削除";
			switch (MessageBox.Show(
					"全てのキャッシュを削除します．\nよろしいですか？",
					title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) {
				case DialogResult.Yes:
					int success = 0;
					int failed = 0;
					foreach (string key in this.cacheController.ListAllCacheKeys()) {
						if (this.cacheController.RemoveCache(key)) {
							success++;
						} else {
							failed++;
						}
					}
					MessageBox.Show(
						success.ToString() + " 個のキャッシュを削除しました．\n"
						+ failed.ToString() + " 個のキャッシュの削除に失敗しました．",
						title, MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
			}
		}
		private void tsmiClearCrawlResults_Click(object sender, EventArgs e) {
			string title = "クロール結果の破棄";
			switch (MessageBox.Show(
					"全ジャンルのクロール結果を破棄します．よろしいですか？",
					title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
				case DialogResult.Yes:
					int numResults = this.results.Count;
					this.results.Clear();
					MessageBox.Show(
						numResults.ToString() + " 個のクロール結果を破棄しました．",
						title, MessageBoxButtons.OK, MessageBoxIcon.Information);
					break;
			}
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
