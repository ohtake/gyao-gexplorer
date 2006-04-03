using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Yusen.GCrawler;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public sealed partial class MainForm : FormSettingsBase, IFormWithNewSettings<MainForm.MainFormSettings> {
		public sealed class MainFormSettings : INewSettings<MainFormSettings> {
			private readonly MainForm owner;
			
			public MainFormSettings() : this(null) {
			}
			internal MainFormSettings(MainForm owner) {
				this.owner = owner;
				this.formSettingsBaseSettings = new FormSettingsBaseSettings(owner);
				if (this.HasOwner) {
					this.genreTabControlSettings = new GenreTabControl.GenreTabControlSettings(owner.genreTabControl1);
					this.crawlResultViewSettings = new CrawlResultView.CrawlResultViewSettings(owner.crawlResultView1);
					this.playListViewSettings = new PlayListView.PlayListViewSettings(owner.playListView1);
					this.contentDetailViewSettings = new ContentDetailView.ContentDetailViewSettings(owner.contentDetailView1);
				} else {
					this.genreTabControlSettings = new GenreTabControl.GenreTabControlSettings();
					this.crawlResultViewSettings = new CrawlResultView.CrawlResultViewSettings();
					this.playListViewSettings = new PlayListView.PlayListViewSettings();
					this.contentDetailViewSettings = new ContentDetailView.ContentDetailViewSettings();
				}
			}

			[Browsable(false)]
			[XmlIgnore]
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
			private readonly FormSettingsBaseSettings formSettingsBaseSettings;

			[Category("動作")]
			[DisplayName("タブ切り替えでフォーカス移動")]
			[Description("ジャンルタブでタブを切り替えるとクロール結果ビューにフォーカスを移動させます．")]
			[DefaultValue(true)]
			public bool FocusOnResultAfterGenreChanged {
				get { return this.focusOnResultAfterGenreChanged; }
				set { this.focusOnResultAfterGenreChanged = value; }
			}
			private bool focusOnResultAfterGenreChanged = true;
			
			[Category("位置とサイズ")]
			[DisplayName("クロール結果ビューの幅")]
			[Description("クロール結果ビューの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ListViewWidth {
				get {
					if (this.HasOwner) return this.owner.scListsAndDetail.SplitterDistance;
					else return this.listViewWidth;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.scListsAndDetail.SplitterDistance = value.Value;
					else this.listViewWidth = value;
				}
			}
			private int? listViewWidth;

			[Category("位置とサイズ")]
			[DisplayName("クロール結果ビューの高さ")]
			[Description("クロール結果ビューの高さをピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ListViewHeight {
				get {
					if (this.HasOwner) return this.owner.scLists.SplitterDistance;
					else return this.listViewHeight;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.scLists.SplitterDistance = value.Value;
					else this.listViewHeight = value;
				}
			}
			private int? listViewHeight;

			[Browsable(false)]
			public GenreTabControl.GenreTabControlSettings GenreTabControlSettings {
				get { return this.genreTabControlSettings; }
				set { this.genreTabControlSettings.ApplySettings(value); }
			}
			private readonly GenreTabControl.GenreTabControlSettings genreTabControlSettings;

			[Browsable(false)]
			public CrawlResultView.CrawlResultViewSettings CrawlResultViewSettings {
				get { return this.crawlResultViewSettings; }
				set { this.crawlResultViewSettings.ApplySettings(value); }
			}
			private readonly CrawlResultView.CrawlResultViewSettings crawlResultViewSettings;

			[Browsable(false)]
			public PlayListView.PlayListViewSettings PlayListViewSettings {
				get { return this.playListViewSettings; }
				set { this.playListViewSettings.ApplySettings(value); }
			}
			private readonly PlayListView.PlayListViewSettings playListViewSettings;
			
			[Browsable(false)]
			public ContentDetailView.ContentDetailViewSettings ContentDetailViewSettings {
				get { return this.contentDetailViewSettings; }
				set { this.contentDetailViewSettings.ApplySettings(value); }
			}
			private readonly ContentDetailView.ContentDetailViewSettings contentDetailViewSettings;

			#region INewSettings<MainFormSettings> Members
			public void ApplySettings(MainFormSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}

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
		
		private Crawler crawler;
		
		private ContentAdapter seletedCont = null;
		private CrawlProgressEventArgs crawlProgressEventArgs = null;

		private MainFormSettings settings;

		public MainForm() {
			InitializeComponent();

			this.Text = Program.ApplicationName;
			Utility.AppendHelpMenu(this.menuStrip1);
			this.tsmiSettingsGlobal.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsMainForm.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsGenreTab.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsResultView.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsPlaylistView.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsDetailView.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
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
				this.Invoke(new ParameterizedThreadStart(delegate(object param) {
					this.SetStatutBarTextTemporary(param as string);
				}), text);
			} else {
				this.timerClearStatusText.Stop();
				this.tsslCrawl.Text = text;
				this.timerClearStatusText.Start();
			}
		}
		
		private void ChangeEnabilityOfTsmiAbortCrawling(bool enabled) {
			if (this.InvokeRequired) {
				this.Invoke(new ParameterizedThreadStart(delegate(object param) {
					this.ChangeEnabilityOfTsmiAbortCrawling((bool)param);
				}), enabled);
			} else {
				this.tsmiAbortCrawling.Enabled = enabled;
			}
		}
		private void ViewCrawlResult(CrawlResult result) {
			if (this.InvokeRequired) {
				this.Invoke(new ParameterizedThreadStart(delegate(object param){
					this.ViewCrawlResult(param as CrawlResult);
				}), result);
			} else {
				DateTime begin = DateTime.Now;
				this.crawlResultView1.CrawlResult = result;
				DateTime end = DateTime.Now;
				if (null == result) {
					this.SetStatutBarTextTemporary("選択されたジャンルはまだクロールされていません．タブをダブルクリックするとクロールを実行します．");
				} else {
					this.ClearStatusBarInfo();
				}
				if (this.settings.FocusOnResultAfterGenreChanged) {
					this.crawlResultView1.Focus();
				}
			}
		}
		public string FilenameForSettings {
			get { return @"MainFormSettings.xml"; }
		}

		private void MainForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			this.crawler = new Crawler(new HtmlParserRegex(), Cache.Instance.ContentCacheController, Cache.Instance.DeadlineTable);
			
			Cache.Instance.CacheRearranged += new EventHandler<CacheEventArgs>(Cache_CacheRearranged);
			Program.ProgramSerializationProgress += new EventHandler<ProgramSerializationProgressEventArgs>(this.Program_ProgramSerializationProgress);

			this.settings = new MainFormSettings(this);
			this.tspgGlobal.SelectedObject = GlobalSettings.Instance;
			this.tspgMainForm.SelectedObject = this.settings;
			this.tspgGenreTab.SelectedObject = this.settings.GenreTabControlSettings;
			this.tspgResultView.SelectedObject = this.settings.CrawlResultViewSettings;
			this.tspgPlaylistView.SelectedObject = this.settings.PlayListViewSettings;
			this.tspgDetailView.SelectedObject = this.settings.ContentDetailViewSettings;
			Utility.LoadSettingsAndEnableSaveOnClosedNew(this);
			
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
			
			this.ClearStatusBarInfo();
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
			this.tsmiAbortCrawling.PerformClick();
			
			this.Enabled = false;
			if(PlayerForm.HasInstance) PlayerForm.Instance.Close();
			if(BrowserForm.HasInstance) BrowserForm.Instance.Close();
			
			Program.SerializeSettings();
		}
		private void Program_ProgramSerializationProgress(object sender, ProgramSerializationProgressEventArgs e) {
			this.tspbCrawl.Maximum = e.Max;
			this.tspbCrawl.Value = e.Current;
			this.tsslCrawl.Text = string.Format("終了処理 {0}/{1}: {2}", e.Current, e.Max, e.Message);
			Application.DoEvents();
		}
		private void Cache_CacheRearranged(object sender, CacheEventArgs e) {
			this.SetStatutBarTextTemporary(e.Message);
		}

		private void genreTabControl1_GenreSelected(object sender, GenreSelectedEventArgs e) {
			GGenre genre = e.Genre;
			if (null == genre) return;
			CrawlResult result = null;
			if (e.ForceReload) {
				if (this.bwCrawl.IsBusy) {
					MessageBox.Show("多重クロールは禁止．", Program.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				} else {
					this.bwCrawl.RunWorkerAsync(genre);
				}
			} else if (Cache.Instance.ResultsDictionary.TryGetValue(genre, out result)) {
				this.ViewCrawlResult(result);
			} else {
				this.ViewCrawlResult(null);
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
			this.genreTabControl1.SelectedGenre = null;
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
		private void tsucmiCommand_UserCommandSelected(object sender, UserCommandSelectedEventArgs e) {
			e.UserCommand.Execute(new ContentAdapter[] { });
		}
		private void tsmiAbortCrawling_Click(object sender, EventArgs e) {
			this.bwCrawl.CancelAsync();
		}

		private void tsmiSettingsGlobal_DropDownOpened(object sender, EventArgs e) {
			this.tspgGlobal.RefreshPropertyGrid();
		}
		private void tsmiSettingsMainForm_DropDownOpened(object sender, EventArgs e) {
			this.tspgMainForm.RefreshPropertyGrid();
		}
		private void tsmiSettingsResultView_DropDownOpened(object sender, EventArgs e) {
			this.tspgResultView.RefreshPropertyGrid();
		}
		private void tsmiSettingsPlaylistView_DropDownOpened(object sender, EventArgs e) {
			this.tspgPlaylistView.RefreshPropertyGrid();
		}
		private void tsmiSettingsDetailView_DropDownOpened(object sender, EventArgs e) {
			this.tspgDetailView.RefreshPropertyGrid();
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

		private void bwCrawl_DoWork(object sender, DoWorkEventArgs e) {
			this.ChangeEnabilityOfTsmiAbortCrawling(true);
			e.Result = this.crawler.Crawl(GlobalSettings.Instance.CreateCrawlSettings(), e.Argument as GGenre, this.bwCrawl);
			if (null == e.Result) {
				e.Cancel = true;
			}
		}
		private void bwCrawl_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			this.timerCrawlProgress.Start();
			this.crawlProgressEventArgs = e.UserState as CrawlProgressEventArgs;
		}
		private void bwCrawl_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			this.ChangeEnabilityOfTsmiAbortCrawling(false);
			this.timerCrawlProgress.Stop();
			this.ClearStatusBarInfo();
			if (e.Cancelled) {
				this.SetStatutBarTextTemporary("クロールを中止しました．");
				return;
			}
			if (null != e.Error) {
				throw e.Error;
			}
			CrawlResult result = e.Result as CrawlResult;
			Cache.Instance.ResultsDictionary[result.Genre] = result;
			this.ViewCrawlResult(result);
		}

		#region IHasNewSettings<MainFormSettings> Members
		public MainForm.MainFormSettings Settings {
			get { return this.settings; }
		}
		#endregion
	}
}
