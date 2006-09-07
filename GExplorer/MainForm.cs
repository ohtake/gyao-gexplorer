using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Yusen.GCrawler;
using System.Xml.Serialization;
using System.Text;
using System.Web;

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

			[Category("動作")]
			[DisplayName("タブ切り替えでフォーカス移動")]
			[Description("ジャンルタブでタブを切り替えるとクロール結果ビューにフォーカスを移動させます．")]
			[DefaultValue(true)]
			public bool FocusOnResultAfterGenreChanged {
				get { return this.focusOnResultAfterGenreChanged; }
				set { this.focusOnResultAfterGenreChanged = value; }
			}
			private bool focusOnResultAfterGenreChanged = true;

			[Category("表示")]
			[DisplayName("ジャンルメニューの色分け")]
			[Description("ジャンルメニューをジャンルごとに色分けして表示します．")]
			[DefaultValue(true)]
			public bool MenuGenreColored {
				get {
					if (this.HasOwner) {
						return this.owner.tsgmiSelectGenre.GenreColored;
					} else {
						return this.menuGenreColored;
					}
				}
				set {
					if (this.HasOwner) {
						this.owner.tsgmiSelectGenre.GenreColored = value;
						this.owner.tsgmiCrawlGenre.GenreColored = value;
						this.owner.tsgmiUncrawlables.GenreColored = value;
					} else {
						this.menuGenreColored = value;
					}
				}
			}
			private bool menuGenreColored = true;

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
			public override Uri RootUri {
				get { return new Uri("about:blank"); }
			}
			public override bool IsCrawlable {
				get { return false;}
			}
		}
		
		private sealed class FlatContentsGenre : GGenre {
			public FlatContentsGenre() : base(0, "dummy", "一覧", Color.Black) {
			}
			public override Uri TopPageUri {
				get { return new Uri("about:blank"); }
			}
			public override Uri RootUri {
				get { return new Uri("about:blank"); }
			}
			public override bool IsCrawlable {
				get { return false; }
			}
		}

		private static MainForm instance = null;
		public static MainForm Instance {
			get {
				if (!MainForm.HasInstance) {
					MainForm.instance = new MainForm();
				}
				return MainForm.instance;
			}
		}
		public static bool HasInstance {
			get { return null != MainForm.instance && !MainForm.instance.IsDisposed; }
		}
		
		private Crawler crawler;
		
		private ContentAdapter seletedCont = null;
		private CrawlProgressEventArgs crawlProgressEventArgs = null;

		private MainFormSettings settings;

		private MainForm() {
			InitializeComponent();

			this.Text = Program.ApplicationName;
			this.tsmiSettingsGlobal.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsMainForm.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsGenreTab.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsResultView.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsPlaylistView.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;
			this.tsmiSettingsDetailView.DropDown.Closing += ToolStripPropertyGrid.CancelDropDownClosingIfEditingPropertyGrid;

			this.tsmiAbortCrawling.Font = new Font(this.tsmiAbortCrawling.Font, FontStyle.Bold);
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
		
		private void ChangeVisibilityOfAbortCrawling(bool abortVisible) {
			if (this.InvokeRequired) {
				this.Invoke(new ParameterizedThreadStart(delegate(object param) {
					this.ChangeVisibilityOfAbortCrawling((bool)param);
				}), abortVisible);
			} else {
				this.tsgmiCrawlGenre.Visible = !abortVisible;
				this.tsmiAbortCrawling.Visible = abortVisible;
			}
		}
		private void ViewCrawlResult(CrawlResult result) {
			if (this.InvokeRequired) {
				this.Invoke(new ParameterizedThreadStart(delegate(object param){
					this.ViewCrawlResult(param as CrawlResult);
				}), result);
			} else {
				this.crawlResultView1.CrawlResult = result;
				if (null == result) {
					this.SetStatutBarTextTemporary("選択されたジャンルはまだクロールされていません．タブをダブルクリックするとクロールを実行します．");
				} else {
					this.genreTabControl1.SelectedGenre = result.Genre;
					this.ClearStatusBarInfo();
				}
				if (this.settings.FocusOnResultAfterGenreChanged) {
					this.crawlResultView1.Focus();
				}
			}
		}
		public void ViewFlatCrawlResult(IList<GContent> list) {
			CrawlResult result = CrawlResult.CreateFlatResult(new FlatContentsGenre(), list);
			this.ViewCrawlResult(result);
		}
		
		private void CheckInvalidContentPredicates(ContentPredicatesManager manager, string cpName, bool showResultOnSuccess) {
			string title = "妥当でない" + cpName;
			ContentPredicate[] preds = manager.GetInvalidPredicates();
			if (preds.Length > 0) {
				string separator = "-------------------------------------------------";
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(separator);
				sb.AppendLine("コメント\t主語\t述語\t目的語\t作成日時");
				sb.AppendLine(separator);
				foreach (ContentPredicate cp in preds) {
					sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", cp.Comment, cp.SubjectName, cp.PredicateName, cp.ObjectValue, cp.CreatedTime));
				}
				sb.AppendLine(separator);
				switch (MessageBox.Show(string.Format("妥当でない{0}が {1} 個見つかりました．\n\n{2}\n削除しますか？", cpName, preds.Length, sb.ToString()), title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) {
					case DialogResult.Yes:
						int removeCnt = manager.RemoveInvalidPredicates();
						MessageBox.Show(string.Format("妥当でなかった{0}を {1} 個削除しました．", cpName, removeCnt), title, MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case DialogResult.No:
						MessageBox.Show(string.Format("妥当でない{0}を保持したまま判定処理が行われるとエラーが起こりえます．\n{0}エディタで妥当でない{0}の削除を行ってください．", cpName), title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
				}
			} else if (showResultOnSuccess) {
				MessageBox.Show(string.Format("妥当でない{0}はありませんでした．", cpName), title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		public string FilenameForSettings {
			get { return @"MainFormSettings.xml"; }
		}

		private void MainForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			this.crawler = new Crawler(new HtmlParserRegex(), Cache.Instance.ContentCacheController);
			
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
			foreach (GGenre genre in GGenre.AllGenres) {
				if (genre.IsCrawlable) {
					this.genreTabControl1.AddGenre(genre);
				}
			}
			this.genreTabControl1.SelectedGenre = null;

			this.ChangeVisibilityOfAbortCrawling(false);
			this.tsgmiUncrawlables.Visible = this.tsgmiUncrawlables.HasAvailableSubmenus;
			
			this.ClearStatusBarInfo();
			this.CheckInvalidContentPredicates(ContentPredicatesManager.NgManager, "NGコンテンツ", false);
			this.CheckInvalidContentPredicates(ContentPredicatesManager.FavManager, "FAVコンテンツ", false);
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
		private void tsgmiSelectGenre_GenreSelected(object sender, GenreMenuItemSelectedEventArgs e) {
			this.genreTabControl1.SelectedGenre = e.SelectedGenre;
		}
		private void tsgmiCrawlGenre_GenreSelected(object sender, GenreMenuItemSelectedEventArgs e) {
			this.genreTabControl1.SelectedGenre = e.SelectedGenre;
			this.CrawlGenre(e.SelectedGenre);
		}
		private void tsgmiUncrawlables_GenreSelected(object sender, GenreMenuItemSelectedEventArgs e) {
			Utility.Browse(e.SelectedGenre.TopPageUri);
		}
		private void genreTabControl1_GenreSelected(object sender, GenreTabSelectedEventArgs e) {
			if (e.ForceReload) {
				this.CrawlGenre(e.Genre);
			} else {
				this.SelectGenre(e.Genre);
			}
		}
		private void SelectGenre(GGenre genre) {
			CrawlResult result;
			if (Cache.Instance.ResultsDictionary.TryGetValue(genre, out result)) {
				this.ViewCrawlResult(result);
			} else {
				this.ViewCrawlResult(null);
			}
		}
		private void CrawlGenre(GGenre genre) {
			if (this.bwCrawl.IsBusy) {
				MessageBox.Show("多重クロールは禁止．", Program.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			} else {
				this.bwCrawl.RunWorkerAsync(genre);
			}
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
					Utility.Browse(GContent.CreateDetailPageUri(GContent.ConvertToKeyFromId(this.inputBoxDialog1.Input)));
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
		private void tsmiNgFavContentsEditor_Click(object sender, EventArgs e) {
			NgFavContentsEditor nfce = NgFavContentsEditor.Instance;
			nfce.Owner = this;
			nfce.Show();
			nfce.Focus();
		}
		private void tsmiMergeResults_Click(object sender, EventArgs e) {
			GGenre mergedGenre = new MergedGenre();
			CrawlResult mergedResult = CrawlResult.Merge(mergedGenre, Cache.Instance.ResultsDictionary.Values);
			this.ViewCrawlResult(mergedResult);
			this.genreTabControl1.SelectedGenre = null;
		}
		private void tsmiSearchCache_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "キャッシュから検索";
			this.inputBoxDialog1.Message = "検索する語句を入力してください．";
			this.inputBoxDialog1.Input = string.Empty;

			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					string query = this.inputBoxDialog1.Input;
					if (string.IsNullOrEmpty(query)) return;

					IList<GContent> list = Cache.Instance.FindCaches(query);
					this.ViewFlatCrawlResult(list);
					break;
			}
		}
		private void tsmiSearchLivedoorGyaO_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "livedoor動画でGyaO検索";
			this.inputBoxDialog1.Message = "検索する語句を入力してください．";
			this.inputBoxDialog1.Input = string.Empty;

			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					string query = this.inputBoxDialog1.Input;
					if (string.IsNullOrEmpty(query)) return;
					
					Utility.Browse(Utility.CreateLivedoorVideoGyaoSearchUri(query));
					break;
			}
		}
		private void tsucmiCommand_UserCommandSelected(object sender, UserCommandSelectedEventArgs e) {
			e.UserCommand.Execute(new ContentAdapter[] { });
		}
		private void tsmiSerializeSettingsNow_Click(object sender, EventArgs e) {
			switch (MessageBox.Show("アプリケーションを終了せずに終了処理の真似事をします．\nあまりお勧めできませんが実行しますか？", "設定ファイルとキャッシュの強制的な書き出し", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) {
				case DialogResult.Yes:
					break;
				default:
					return;
			}
			
			this.tsmiAbortCrawling.PerformClick();
			
			this.Enabled = false;
			this.SetStatutBarTextTemporary("各フォームごとの設定ファイルを書き出し中．");
			if (PlayerForm.HasInstance) Utility.SerializeSettings(PlayerForm.Instance.FilenameForSettings, PlayerForm.Instance.Settings);
			if (BrowserForm.HasInstance) Utility.SerializeSettings(BrowserForm.Instance.FilenameForSettings, BrowserForm.Instance.Settings);
			if (UserCommandsEditor.HasInstance) Utility.SerializeSettings(UserCommandsEditor.Instance.FilenameForSettings, UserCommandsEditor.Instance.Settings);
			if (NgFavContentsEditor.HasInstance) Utility.SerializeSettings(NgFavContentsEditor.Instance.FilenameForSettings, NgFavContentsEditor.Instance.Settings);
			Utility.SerializeSettings(this.FilenameForSettings, this.Settings);
			
			Program.SerializeSettings();
			
			this.Enabled = true;
			
			this.ClearStatusBarInfo();
			this.SetStatutBarTextTemporary("設定ファイルとキャッシュの強制的な書き出しを完了しました．");
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
			this.ChangeVisibilityOfAbortCrawling(true);
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
			this.ChangeVisibilityOfAbortCrawling(false);
			this.timerCrawlProgress.Stop();
			this.ClearStatusBarInfo();
			if (e.Cancelled) {
				this.SetStatutBarTextTemporary("クロールを中止しました．");
				return;
			}
			if (null != e.Error) {
				Program.DisplayException("クロール中にキャッチされなかった例外", e.Error);
				return;
			}
			//クロール結果の表示
			CrawlResult result = e.Result as CrawlResult;
			Cache.Instance.ResultsDictionary[result.Genre] = result;
			this.ViewCrawlResult(result);

			//新着FAVコンテンツをプレイリストに追加
			if (GlobalSettings.Instance.AutomaticallyAddFavAndNewContents) {
				//新着FAVコンテンツの抽出
				List<ContentAdapter> favs = new List<ContentAdapter>();
				foreach (GPackage package in result.Packages) {
					foreach (GContent content in package.Contents) {
						if (!content.FromCache) {//処理速度のため先に FromCache で仕分け
							ContentAdapter ca = new ContentAdapter(content);
							if (ca.IsNew && ContentPredicatesManager.FavManager.IsTrueFor(ca)) {
								favs.Add(ca);
							}
						}
					}
				}
				//追加済みのを除いてから追加する
				favs.RemoveAll(PlayList.Instance.Contains);
				if (favs.Count > 0) {
					PlayList.Instance.AddRange(favs);
					this.SetStatutBarTextTemporary(string.Format("FAVコンテンツに該当した {0} 個の新着コンテンツをプレイリストに追加しました．", favs.Count));
				}
			}
		}
		
		#region IHasNewSettings<MainFormSettings> Members
		public MainForm.MainFormSettings Settings {
			get { return this.settings; }
		}
		#endregion

		private void genreTabControl1_RequiredHeightChanged(object sender, EventArgs e) {
			this.tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Absolute, this.genreTabControl1.RequiredHeight);
			this.tableLayoutPanel1.ResumeLayout(false);
		}
	}
}
