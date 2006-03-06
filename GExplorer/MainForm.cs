using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	sealed partial class MainForm : FormSettingsBase, IFormWithSettings<MainFormSettings> {
		private sealed class MergedGenre : GGenre {
			public MergedGenre() : base(0, "dummy", "(既得ジャンル)", Color.Black){
			}
			public override Uri TopPageUri {
				get {return new Uri("about:blank");}
			}
			public override bool IsCrawlable {
				get { return false;}
			}
		}
		private delegate void ViewCrawlResultDelegate(CrawlResult result);
		private delegate void SetStatusBarTextDelegate(string text);

		private Crawler crawler;
		private Thread threadCrawler = null;

		private ContentAdapter seletedCont = null;
		private CrawlProgressEventArgs crawlProgressEventArgs = null;

		public MainForm() {
			InitializeComponent();
		}

		private void ClearStatusBarInfo() {
			if (this.InvokeRequired) {
				this.Invoke(new MethodInvoker(delegate {
					this.ClearStatusBarInfo();
				}));
			} else {
				this.tspbCrawl.Value = 0;
				this.tsslCrawl.Text = string.Empty;
			}
		}
		private void SetStatutBarTextTemporary(string text) {
			if(this.InvokeRequired) {
				this.Invoke(new SetStatusBarTextDelegate(delegate(string text2) {
					this.SetStatutBarTextTemporary(text2);
				}));
			} else {
				this.timerClearStatusText.Stop();
				this.tsslCrawl.Text = text;
				this.timerClearStatusText.Start();
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
				this.genreTabControl1.SelectedGenre = result.Genre;
			}
		}
		public void FillSettings(MainFormSettings settings) {
			base.FillSettings(settings);
			settings.FocusOnResultAfterGenreChanged = this.FocusOnResultAfterGenreChanged;
			settings.ListViewWidth = this.scListsAndDetail.SplitterDistance;
			settings.ListViewHeight = this.scLists.SplitterDistance;
			this.crawlResultView1.FillSettings(settings.GenreListViewSettings);
			this.playListView1.FillSettings(settings.PlayListViewSettings);
			this.contentDetailView1.FillSettings(settings.ContentDetailViewSettings);
		}
		public void ApplySettings(MainFormSettings settings) {
			base.ApplySettings(settings);
			this.FocusOnResultAfterGenreChanged = settings.FocusOnResultAfterGenreChanged ?? this.FocusOnResultAfterGenreChanged;
			this.scListsAndDetail.SplitterDistance = settings.ListViewWidth ?? this.scListsAndDetail.SplitterDistance;
			this.scLists.SplitterDistance = settings.ListViewHeight ?? this.scLists.SplitterDistance;
			if (null != settings.GenreListViewSettings) this.crawlResultView1.ApplySettings(settings.GenreListViewSettings);
			if (null != settings.PlayListViewSettings) this.playListView1.ApplySettings(settings.PlayListViewSettings);
			if (null != settings.ContentDetailViewSettings) this.contentDetailView1.ApplySettings(settings.ContentDetailViewSettings);
		}
		public string FilenameForSettings {
			get { return @"MainFormSettings.xml"; }
		}

		public bool FocusOnResultAfterGenreChanged {
			get { return this.tsmiFocusOnResultAfterTabChanged.Checked; }
			set { this.tsmiFocusOnResultAfterTabChanged.Checked = value; }
		}
		
		private void CreateUserCommandsMenuItems() {
			this.tsmiUserCommands.DropDownItems.Clear();
			List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();
			foreach (UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(uc.Title);
				tsmi.Tag = uc;
				tsmi.Click += delegate(object sender, EventArgs e) {
					ToolStripMenuItem tsmi2 = sender as ToolStripMenuItem;
					if (null != tsmi2) {
						UserCommand uc2 = tsmi2.Tag as UserCommand;
						if (null != uc2) {
							uc2.Execute(new ContentAdapter[] { });
						}
					}
				};
				menuItems.Add(tsmi);
			}
			this.tsmiUserCommands.DropDownItems.AddRange(menuItems.ToArray());
			this.tsmiUserCommands.Enabled = this.tsmiUserCommands.HasDropDownItems;
		}
		
		private void MainForm_Load(object sender, EventArgs e) {
			this.crawler = new Crawler(new HtmlParserRegex(), Cache.Instance.ContentCacheController, Cache.Instance.DeadlineTable);
			
			this.Text = Program.ApplicationName;
			Utility.AppendHelpMenu(this.menuStrip1);
			this.tsmiSettings.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;

			this.genreTabControl1.TabPages.Clear();
			this.tsmiUncrawlableGenres.DropDownItems.Clear();
			foreach (GGenre genre in GGenre.AllGenres) {
				if (genre.IsCrawlable) {
					this.genreTabControl1.AddGenre(genre);
				} else {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(genre.GenreName);
					tsmi.Tag = genre;
					tsmi.Click += delegate(object sender2, EventArgs e2) {
						Utility.Browse(((sender2 as ToolStripMenuItem).Tag as GGenre).TopPageUri);
					};
					this.tsmiUncrawlableGenres.DropDownItems.Add(tsmi);
				}
			}
			this.genreTabControl1.SelectedGenre = null;
			this.tsmiUncrawlableGenres.Enabled = this.tsmiUncrawlableGenres.HasDropDownItems;
			this.tsmiUncrawlableGenres.Visible = this.tsmiUncrawlableGenres.HasDropDownItems;

			this.tsmiSettingsCrawlResultView.DropDown = this.crawlResultView1.SettingsDropDown;
			this.crawlResultView1.SettingsVisible = false;
			this.tsmiSettingsPlaylistView.DropDown = this.playListView1.SettingsDropDown;
			this.playListView1.SettingsVisible = false;
			this.tsmiSettingsDetailView.DropDown = this.contentDetailView1.SettingsDropDown;
			this.contentDetailView1.SettingsVisible = false;
			
			this.crawler.CrawlProgress += new EventHandler<CrawlProgressEventArgs>(crawler_CrawlProgress);
			
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
			this.CreateUserCommandsMenuItems();
			
			Utility.LoadSettingsAndEnableSaveOnClosed(this);
			this.ClearStatusBarInfo();

			Cache.Instance.CacheRearranged += new EventHandler<CacheEventArgs>(Cache_CacheRearranged);
			Program.ProgramSerializationProgress += new EventHandler<ProgramSerializationProgressEventArgs>(this.Program_ProgramSerializationProgress);
		}


		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			switch (e.CloseReason) {
				case CloseReason.UserClosing:
					if (PlayList.Instance.HasCurrentContent) {
						switch (MessageBox.Show("再生中ですがアプリケーションを終了しますか？", Program.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
							case DialogResult.No:
								e.Cancel = true;
								break;
						}
					}
					break;
			}
		}
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
			UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			
			this.tsmiAbortCrawling.PerformClick();
			
			this.Enabled = false;
			if(PlayerForm.HasInstance) PlayerForm.Instance.Close();
			if(BrowserForm.HasInstance) BrowserForm.Instance.Close();
			
			Program.SerializeSettings();
		}
		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateUserCommandsMenuItems();
		}
		private void Program_ProgramSerializationProgress(object sender, ProgramSerializationProgressEventArgs e) {
			this.tspbCrawl.Maximum = e.Max;
			this.tspbCrawl.Value = e.Current;
			this.tsslCrawl.Text = string.Format("終了処理 {0}/{1}: {2}", e.Current, e.Max, e.Message);
			Application.DoEvents();
		}
		void Cache_CacheRearranged(object sender, CacheEventArgs e) {
			this.SetStatutBarTextTemporary(e.Message);
		}

		private void crawler_CrawlProgress(object sender, CrawlProgressEventArgs e) {
			if (this.InvokeRequired) {
				this.Invoke(
					new EventHandler<CrawlProgressEventArgs>(this.crawler_CrawlProgress),
					new object[] { sender, e });
			} else {
				this.timerCrawlProgress.Start();
				this.crawlProgressEventArgs = e;
			}
		}

		private void genreTabControl1_GenreSelected(object sender, GenreTabPageEventArgs e) {
			GGenre genre = e.Genre;
			if (null == genre) return;
			CrawlResult result;
			if (e.ForceReload || !Cache.Instance.ResultsDictionary.TryGetValue(genre, out result)) {
				Thread t;
				lock (this.crawler) {
					if (null != this.threadCrawler) {
						MessageBox.Show(
							"多重クロールは禁止．", Program.ApplicationName,
							MessageBoxButtons.OK, MessageBoxIcon.Stop);
						return;
					}
					t = new Thread(new ThreadStart(delegate {
						try {
							result = this.crawler.Crawl(GlobalSettings.Instance.GetCrawlSettings(), genre);
						} finally {
							lock (this.crawler) {
								this.threadCrawler = null;
								this.DisableTsmiAbortCrawling();
							}
						}
						if (result.Success) {
							this.crawlProgressEventArgs = null;
							this.ClearStatusBarInfo();
							Cache.Instance.ResultsDictionary[genre] = result;
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

		private void crawlResultView1_ManuallyCacheDeleted(object sender, ManuallyCacheDeletedEventArgs e) {
			this.SetStatutBarTextTemporary(string.Format("キャッシュの削除    成功: {0}    失敗: {1}", e.Succeeded, e.Failed));
		}
		private void crawlResultView1_ContentSelectionChanged(object sender, ContentSelectionChangedEventArgs e) {
			if (e.IsSelected) {
				this.seletedCont = e.Content;
				this.timerViewDetail.Start();
			}
		}
		private void playListView1_ContentSelectionChanged(object sender, ContentSelectionChangedEventArgs e) {
			if (e.IsSelected) {
				this.seletedCont = e.Content;
				this.timerViewDetail.Start();
			}
		}
		private void contentDetailView1_ImageLoadError(object sender, ImageLoadErrorEventArgs e) {
			this.SetStatutBarTextTemporary("詳細ビューでの画像読み込みエラー: " + e.Exception.Message);
		}
		
		private void tsmiBrowseTop_Click(object sender, EventArgs e) {
			Utility.Browse(new Uri("http://www.gyao.jp/"));
		}
		private void tsmiBrowsePackage_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "パッケージIDを指定してウェブブラウザで開く";
			this.inputBoxDialog1.Message = "パッケージIDを入力してください．";
			this.inputBoxDialog1.Input = "pac0000000";
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					Utility.Browse(GPackage.CreatePackagePageUri(this.inputBoxDialog1.Input));
					break;
			}
		}
		private void tsmiBrowseContent_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "コンテンツIDを指定してウェブブラウザで開く";
			this.inputBoxDialog1.Message = "コンテンツIDを入力してください．";
			this.inputBoxDialog1.Input = "cnt0000000";
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					Utility.Browse(GContent.CreateDetailPageUri(this.inputBoxDialog1.Input));
					break;
			}
		}
		private void tsmiQuit_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void tsmiGlobalSettingsEditor_Click(object sender, EventArgs e) {
			GlobalSettingsEditor gse = GlobalSettingsEditor.Instance;
			gse.Owner = this;
			gse.Show();
			gse.Focus();
		}
		private void tsmiUserCommandsEditor_Click(object sender, EventArgs e) {
			UserCommandsEditor uce = UserCommandsEditor.Instance;
			uce.Owner = this;
			uce.Show();
			uce.Focus();
		}
		private void tsmiNgContentsEditor_Click(object sender, EventArgs e) {
			NgContentsEditor nce = NgContentsEditor.Instance;
			nce.Owner = this;
			nce.Show();
			nce.Focus();
		}
		private void tsmiMergeResults_Click(object sender, EventArgs e) {
			GGenre mergedGenre = new MergedGenre();
			CrawlResult mergedResult = CrawlResult.Merge(mergedGenre, Cache.Instance.ResultsDictionary.Values);
			this.ViewCrawlResult(mergedResult);
		}
		private void tsmiClearCrawlResults_Click(object sender, EventArgs e) {
			switch (MessageBox.Show(
					"全ジャンルのクロール結果を破棄します．よろしいですか？", "クロール結果の破棄",
					MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)) {
				case DialogResult.Yes:
					Cache.Instance.ClearCrawlResults();
					break;
			}
		}
		private void tsmiRemoveCachesUnreachable_Click(object sender, EventArgs e) {
			Cache.Instance.RemoveCachesUnreachable();
		}
		private void tsmiRemoveCachesAll_Click(object sender, EventArgs e) {
			switch (MessageBox.Show(
					"全てのキャッシュを削除します．よろしいですか？",
					"全てのキャッシュを削除", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)) {
				case DialogResult.Yes:
					Cache.Instance.RemoveCachesAll();
					break;
			}
		}
		private void tsmiRemoveDeadlineEntriesUnreacheable_Click(object sender, EventArgs e) {
			Cache.Instance.RemoveDeadlineEntriesUnreacheable();
		}
		private void tsmiRemoveDeadlineEntriesAll_Click(object sender, EventArgs e) {
			switch (MessageBox.Show(
					"全てのエントリーを削除します．\nよろしいですか？",
					"全てのエントリーを削除", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)) {
				case DialogResult.Yes:
					Cache.Instance.RemoveDeadlineEntriesAll();
					break;
			}
		}
		private void tsmiDeleteNgContentsWeek_Click(object sender, EventArgs e) {
			NgContentsManager manager = NgContentsManager.Instance;
			int cntAll = manager.Count;
			NgContentsManager.Instance.RemoveAll(new Predicate<NgContent>(delegate(NgContent ng) {
				return ng.LastAbone < DateTime.Now.AddDays(-7);
			}));
			int cntLast = manager.Count;
			this.SetStatutBarTextTemporary(string.Format("NGコンテンツの削除    削除数: {0}    残り: {1}", cntAll - cntLast, cntLast));
		}
		private void tsmiDeleteNgContentsAll_Click(object sender, EventArgs e) {
			switch(MessageBox.Show(
				"全てのNGコンテンツを削除します．\nよろしいですか？",
				"全てのNGコンテンツを削除", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)) {
				case DialogResult.Yes:
					int num = NgContentsManager.Instance.Count;
					NgContentsManager.Instance.Clear();
					this.SetStatutBarTextTemporary(string.Format("NGコンテンツの削除    削除数: {0}", num));
					break;
			}
		}
		private void tsmiGetProfile_Click(object sender, EventArgs e) {
			string title = "ユーザIDに対応するプロファイルを取得";
			string profile;
			if(Utility.TryGetUserProfileOf(GlobalSettings.Instance.UserNo, out profile)) {
				MessageBox.Show(profile, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			} else {
				MessageBox.Show("ユーザプロファイルの取得に失敗しました．", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void tsmiAbortCrawling_Click(object sender, EventArgs e) {
			lock (this.crawler) {
				if (null != this.threadCrawler) {
					this.threadCrawler.Abort();
					this.tsmiAbortCrawling.Enabled = false;
					this.threadCrawler = null;
					
					this.crawlProgressEventArgs = null;
					this.ClearStatusBarInfo();
					this.SetStatutBarTextTemporary("クロールを中止しました．");
				}
			}
		}

		private void timerViewDetail_Tick(object sender, EventArgs e) {
			this.timerViewDetail.Stop();
			this.contentDetailView1.Content = this.seletedCont;
		}

		private void timerCrawlProgress_Tick(object sender, EventArgs e) {
			this.timerCrawlProgress.Stop();
			if(null != this.crawlProgressEventArgs){
				this.tspbCrawl.Maximum = this.crawlProgressEventArgs.Visited + this.crawlProgressEventArgs.Waiting;
				this.tspbCrawl.Value = this.crawlProgressEventArgs.Visited;
				this.tsslCrawl.Text = this.crawlProgressEventArgs.Message;
			}
			Application.DoEvents();
		}
		private void timerClearStatusText_Tick(object sender, EventArgs e) {
			this.timerClearStatusText.Stop();
			this.tsslCrawl.Text = string.Empty;
		}
	}

	public class MainFormSettings : FormSettingsBaseSettings {
		private bool? focusOnResultAfterGenreChanged;
		private int? listViewWidth;
		private int? listViewHeight;
		private GenreListViewSettings genreListViewSettings = new GenreListViewSettings();
		private PlayListViewSettings playListViewSettings = new PlayListViewSettings();
		private ContentDetailViewSettings contentDetailViewSettings = new ContentDetailViewSettings();

		public bool? FocusOnResultAfterGenreChanged {
			get { return this.focusOnResultAfterGenreChanged; }
			set { this.focusOnResultAfterGenreChanged = value; }
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
