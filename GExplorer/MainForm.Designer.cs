namespace Yusen.GExplorer {
	partial class MainForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tspbCrawl = new System.Windows.Forms.ToolStripProgressBar();
			this.tsslCrawl = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.scListsAndDetail = new System.Windows.Forms.SplitContainer();
			this.scLists = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.crawlResultView1 = new Yusen.GExplorer.CrawlResultView();
			this.genreTabControl1 = new Yusen.GExplorer.GenreTabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.playListView1 = new Yusen.GExplorer.PlayListView();
			this.contentDetailView1 = new Yusen.GExplorer.ContentDetailView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowsePackage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseContent = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsgmiSelectGenre = new Yusen.GExplorer.ToolStripGenreMenuItem();
			this.tsgmiCrawlGenre = new Yusen.GExplorer.ToolStripGenreMenuItem();
			this.tsmiAbortCrawling = new System.Windows.Forms.ToolStripMenuItem();
			this.tsgmiUncrawlables = new Yusen.GExplorer.ToolStripGenreMenuItem();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMergeResults = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSearchCache = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSearchLivedoorGyaO = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSerializeSettingsNow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsucmiCommand = new Yusen.GExplorer.ToolStripUserCommandMenuItem();
			this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSettingsGlobal = new System.Windows.Forms.ToolStripMenuItem();
			this.tspgGlobal = new Yusen.GExplorer.ToolStripPropertyGrid();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiUserCommandsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNgFavContentsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSettingsMainForm = new System.Windows.Forms.ToolStripMenuItem();
			this.tspgMainForm = new Yusen.GExplorer.ToolStripPropertyGrid();
			this.tsmiSettingsGenreTab = new System.Windows.Forms.ToolStripMenuItem();
			this.tspgGenreTab = new Yusen.GExplorer.ToolStripPropertyGrid();
			this.tsmiSettingsResultView = new System.Windows.Forms.ToolStripMenuItem();
			this.tspgResultView = new Yusen.GExplorer.ToolStripPropertyGrid();
			this.tsmiSettingsPlaylistView = new System.Windows.Forms.ToolStripMenuItem();
			this.tspgPlaylistView = new Yusen.GExplorer.ToolStripPropertyGrid();
			this.tsmiSettingsDetailView = new System.Windows.Forms.ToolStripMenuItem();
			this.tspgDetailView = new Yusen.GExplorer.ToolStripPropertyGrid();
			this.tshmiHelp = new Yusen.GExplorer.ToolStripHelpMenuItem();
			this.inputBoxDialog1 = new Yusen.GExplorer.InputBoxDialog();
			this.timerViewDetail = new System.Windows.Forms.Timer(this.components);
			this.timerCrawlProgress = new System.Windows.Forms.Timer(this.components);
			this.timerClearStatusText = new System.Windows.Forms.Timer(this.components);
			this.bwCrawl = new System.ComponentModel.BackgroundWorker();
			this.statusStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.scListsAndDetail.Panel1.SuspendLayout();
			this.scListsAndDetail.Panel2.SuspendLayout();
			this.scListsAndDetail.SuspendLayout();
			this.scLists.Panel1.SuspendLayout();
			this.scLists.Panel2.SuspendLayout();
			this.scLists.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.genreTabControl1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbCrawl,
            this.tsslCrawl});
			this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip1.Size = new System.Drawing.Size(872, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tspbCrawl
			// 
			this.tspbCrawl.Name = "tspbCrawl";
			this.tspbCrawl.Size = new System.Drawing.Size(100, 16);
			// 
			// tsslCrawl
			// 
			this.tsslCrawl.Name = "tsslCrawl";
			this.tsslCrawl.Size = new System.Drawing.Size(53, 12);
			this.tsslCrawl.Text = "tsslCrawl";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.scListsAndDetail);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(872, 564);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(872, 606);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// scListsAndDetail
			// 
			this.scListsAndDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scListsAndDetail.Location = new System.Drawing.Point(0, 0);
			this.scListsAndDetail.Name = "scListsAndDetail";
			// 
			// scListsAndDetail.Panel1
			// 
			this.scListsAndDetail.Panel1.Controls.Add(this.scLists);
			// 
			// scListsAndDetail.Panel2
			// 
			this.scListsAndDetail.Panel2.Controls.Add(this.contentDetailView1);
			this.scListsAndDetail.Size = new System.Drawing.Size(872, 564);
			this.scListsAndDetail.SplitterDistance = 600;
			this.scListsAndDetail.TabIndex = 1;
			// 
			// scLists
			// 
			this.scLists.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scLists.Location = new System.Drawing.Point(0, 0);
			this.scLists.Name = "scLists";
			this.scLists.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scLists.Panel1
			// 
			this.scLists.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// scLists.Panel2
			// 
			this.scLists.Panel2.Controls.Add(this.playListView1);
			this.scLists.Size = new System.Drawing.Size(600, 564);
			this.scLists.SplitterDistance = 373;
			this.scLists.TabIndex = 2;
			this.scLists.Text = "splitContainer2";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.crawlResultView1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.genreTabControl1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 373);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// crawlResultView1
			// 
			this.crawlResultView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.crawlResultView1.Location = new System.Drawing.Point(0, 20);
			this.crawlResultView1.Margin = new System.Windows.Forms.Padding(0);
			this.crawlResultView1.Name = "crawlResultView1";
			this.crawlResultView1.Size = new System.Drawing.Size(600, 353);
			this.crawlResultView1.TabIndex = 1;
			this.crawlResultView1.ContentSelectionChanged += new System.EventHandler<Yusen.GExplorer.ContentSelectionChangedEventArgs>(this.crawlResultView1_ContentSelectionChanged);
			// 
			// genreTabControl1
			// 
			this.genreTabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.genreTabControl1.Controls.Add(this.tabPage1);
			this.genreTabControl1.Controls.Add(this.tabPage2);
			this.genreTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.genreTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.genreTabControl1.HotTrack = true;
			this.genreTabControl1.ItemSize = new System.Drawing.Size(65, 20);
			this.genreTabControl1.Location = new System.Drawing.Point(0, 0);
			this.genreTabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.genreTabControl1.Multiline = true;
			this.genreTabControl1.Name = "genreTabControl1";
			this.genreTabControl1.SelectedGenre = null;
			this.genreTabControl1.SelectedIndex = 0;
			this.genreTabControl1.ShowToolTips = true;
			this.genreTabControl1.Size = new System.Drawing.Size(600, 20);
			this.genreTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.genreTabControl1.TabIndex = 0;
			this.genreTabControl1.GenreSelected += new System.EventHandler<Yusen.GExplorer.GenreTabSelectedEventArgs>(this.genreTabControl1_GenreSelected);
			this.genreTabControl1.RequiredHeightChanged += new System.EventHandler(this.genreTabControl1_RequiredHeightChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(592, 0);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(592, -8);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// playListView1
			// 
			this.playListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.playListView1.Location = new System.Drawing.Point(0, 0);
			this.playListView1.Name = "playListView1";
			this.playListView1.Size = new System.Drawing.Size(600, 187);
			this.playListView1.TabIndex = 0;
			this.playListView1.ContentSelectionChanged += new System.EventHandler<Yusen.GExplorer.ContentSelectionChangedEventArgs>(this.playListView1_ContentSelectionChanged);
			// 
			// contentDetailView1
			// 
			this.contentDetailView1.Content = null;
			this.contentDetailView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contentDetailView1.Location = new System.Drawing.Point(0, 0);
			this.contentDetailView1.Name = "contentDetailView1";
			this.contentDetailView1.Size = new System.Drawing.Size(268, 564);
			this.contentDetailView1.TabIndex = 0;
			this.contentDetailView1.ImageLoadError += new System.EventHandler<Yusen.GExplorer.ImageLoadErrorEventArgs>(this.contentDetailView1_ImageLoadError);
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowItemReorder = true;
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsgmiSelectGenre,
            this.tsgmiCrawlGenre,
            this.tsmiAbortCrawling,
            this.tsgmiUncrawlables,
            this.tsmiTools,
            this.tsmiSettings,
            this.tshmiHelp});
			this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(872, 20);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBrowseTop,
            this.tsmiBrowsePackage,
            this.tsmiBrowseContent,
            this.toolStripSeparator1,
            this.tsmiQuit});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Size = new System.Drawing.Size(66, 16);
			this.tsmiFile.Text = "ファイル(&F)";
			// 
			// tsmiBrowseTop
			// 
			this.tsmiBrowseTop.Image = global::Yusen.GExplorer.Properties.Resources.home;
			this.tsmiBrowseTop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiBrowseTop.Name = "tsmiBrowseTop";
			this.tsmiBrowseTop.Size = new System.Drawing.Size(217, 22);
			this.tsmiBrowseTop.Text = "トップページを開く(&T)";
			this.tsmiBrowseTop.Click += new System.EventHandler(this.tsmiBrowseTop_Click);
			// 
			// tsmiBrowsePackage
			// 
			this.tsmiBrowsePackage.Name = "tsmiBrowsePackage";
			this.tsmiBrowsePackage.Size = new System.Drawing.Size(217, 22);
			this.tsmiBrowsePackage.Text = "パッケージIDを指定して開く(&P)...";
			this.tsmiBrowsePackage.Click += new System.EventHandler(this.tsmiBrowsePackage_Click);
			// 
			// tsmiBrowseContent
			// 
			this.tsmiBrowseContent.Name = "tsmiBrowseContent";
			this.tsmiBrowseContent.Size = new System.Drawing.Size(217, 22);
			this.tsmiBrowseContent.Text = "コンテンツIDを指定して開く(&C)...";
			this.tsmiBrowseContent.Click += new System.EventHandler(this.tsmiBrowseContent_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(214, 6);
			// 
			// tsmiQuit
			// 
			this.tsmiQuit.Name = "tsmiQuit";
			this.tsmiQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.tsmiQuit.Size = new System.Drawing.Size(217, 22);
			this.tsmiQuit.Text = "終了(&Q)";
			this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
			// 
			// tsgmiSelectGenre
			// 
			this.tsgmiSelectGenre.MenuVisibility = Yusen.GExplorer.GenreMenuVisibility.Crawlable;
			this.tsgmiSelectGenre.Name = "tsgmiSelectGenre";
			this.tsgmiSelectGenre.Size = new System.Drawing.Size(94, 16);
			this.tsgmiSelectGenre.Text = "ジャンル選択(&G)";
			this.tsgmiSelectGenre.GenreSelected += new System.EventHandler<Yusen.GExplorer.GenreMenuItemSelectedEventArgs>(this.tsgmiSelectGenre_GenreSelected);
			// 
			// tsgmiCrawlGenre
			// 
			this.tsgmiCrawlGenre.MenuVisibility = Yusen.GExplorer.GenreMenuVisibility.Crawlable;
			this.tsgmiCrawlGenre.Name = "tsgmiCrawlGenre";
			this.tsgmiCrawlGenre.Size = new System.Drawing.Size(94, 16);
			this.tsgmiCrawlGenre.Text = "クロール実行(&A)";
			this.tsgmiCrawlGenre.GenreSelected += new System.EventHandler<Yusen.GExplorer.GenreMenuItemSelectedEventArgs>(this.tsgmiCrawlGenre_GenreSelected);
			// 
			// tsmiAbortCrawling
			// 
			this.tsmiAbortCrawling.Name = "tsmiAbortCrawling";
			this.tsmiAbortCrawling.Size = new System.Drawing.Size(118, 16);
			this.tsmiAbortCrawling.Text = "<<クロール中止(&A)>>";
			this.tsmiAbortCrawling.Click += new System.EventHandler(this.tsmiAbortCrawling_Click);
			// 
			// tsgmiUncrawlables
			// 
			this.tsgmiUncrawlables.MenuVisibility = Yusen.GExplorer.GenreMenuVisibility.UnCrawlable;
			this.tsgmiUncrawlables.Name = "tsgmiUncrawlables";
			this.tsgmiUncrawlables.Size = new System.Drawing.Size(106, 16);
			this.tsgmiUncrawlables.Text = "未対応ジャンル(&U)";
			this.tsgmiUncrawlables.GenreSelected += new System.EventHandler<Yusen.GExplorer.GenreMenuItemSelectedEventArgs>(this.tsgmiUncrawlables_GenreSelected);
			// 
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMergeResults,
            this.tsmiSearchCache,
            this.tsmiSearchLivedoorGyaO,
            this.toolStripMenuItem1,
            this.tsmiSerializeSettingsNow,
            this.toolStripMenuItem4,
            this.tsucmiCommand});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Size = new System.Drawing.Size(61, 16);
			this.tsmiTools.Text = "ツール(&T)";
			// 
			// tsmiMergeResults
			// 
			this.tsmiMergeResults.Name = "tsmiMergeResults";
			this.tsmiMergeResults.Size = new System.Drawing.Size(297, 22);
			this.tsmiMergeResults.Text = "既得ジャンルを結合して表示(&M)";
			this.tsmiMergeResults.Click += new System.EventHandler(this.tsmiMergeResults_Click);
			// 
			// tsmiSearchCache
			// 
			this.tsmiSearchCache.Image = global::Yusen.GExplorer.Properties.Resources.SearchInFolder;
			this.tsmiSearchCache.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiSearchCache.Name = "tsmiSearchCache";
			this.tsmiSearchCache.Size = new System.Drawing.Size(297, 22);
			this.tsmiSearchCache.Text = "キャッシュから検索(&F)...";
			this.tsmiSearchCache.Click += new System.EventHandler(this.tsmiSearchCache_Click);
			// 
			// tsmiSearchLivedoorGyaO
			// 
			this.tsmiSearchLivedoorGyaO.Image = global::Yusen.GExplorer.Properties.Resources.searchWeb;
			this.tsmiSearchLivedoorGyaO.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiSearchLivedoorGyaO.Name = "tsmiSearchLivedoorGyaO";
			this.tsmiSearchLivedoorGyaO.Size = new System.Drawing.Size(297, 22);
			this.tsmiSearchLivedoorGyaO.Text = "livedoor動画でGyaO検索(&L)...";
			this.tsmiSearchLivedoorGyaO.Click += new System.EventHandler(this.tsmiSearchLivedoorGyaO_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(294, 6);
			// 
			// tsmiSerializeSettingsNow
			// 
			this.tsmiSerializeSettingsNow.Name = "tsmiSerializeSettingsNow";
			this.tsmiSerializeSettingsNow.Size = new System.Drawing.Size(297, 22);
			this.tsmiSerializeSettingsNow.Text = "設定ファイルとキャッシュを強制的に書き出す(&S)...";
			this.tsmiSerializeSettingsNow.Click += new System.EventHandler(this.tsmiSerializeSettingsNow_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(294, 6);
			// 
			// tsucmiCommand
			// 
			this.tsucmiCommand.Name = "tsucmiCommand";
			this.tsucmiCommand.Size = new System.Drawing.Size(297, 22);
			this.tsucmiCommand.Text = "引数置換なしの外部コマンド(&C)";
			this.tsucmiCommand.UserCommandSelected += new System.EventHandler<Yusen.GExplorer.UserCommandSelectedEventArgs>(this.tsucmiCommand_UserCommandSelected);
			// 
			// tsmiSettings
			// 
			this.tsmiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSettingsGlobal,
            this.toolStripMenuItem2,
            this.tsmiUserCommandsEditor,
            this.tsmiNgFavContentsEditor,
            this.toolStripMenuItem3,
            this.tsmiSettingsMainForm,
            this.tsmiSettingsGenreTab,
            this.tsmiSettingsResultView,
            this.tsmiSettingsPlaylistView,
            this.tsmiSettingsDetailView});
			this.tsmiSettings.Name = "tsmiSettings";
			this.tsmiSettings.Size = new System.Drawing.Size(56, 16);
			this.tsmiSettings.Text = "設定(&S)";
			// 
			// tsmiSettingsGlobal
			// 
			this.tsmiSettingsGlobal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspgGlobal});
			this.tsmiSettingsGlobal.Name = "tsmiSettingsGlobal";
			this.tsmiSettingsGlobal.Size = new System.Drawing.Size(209, 22);
			this.tsmiSettingsGlobal.Text = "グローバル設定(&G)";
			this.tsmiSettingsGlobal.DropDownOpened += new System.EventHandler(this.tsmiSettingsGlobal_DropDownOpened);
			// 
			// tspgGlobal
			// 
			this.tspgGlobal.Name = "tspgGlobal";
			this.tspgGlobal.SelectedObject = null;
			this.tspgGlobal.Size = new System.Drawing.Size(200, 300);
			this.tspgGlobal.Text = "toolStripPropertyGrid1";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(206, 6);
			// 
			// tsmiUserCommandsEditor
			// 
			this.tsmiUserCommandsEditor.Name = "tsmiUserCommandsEditor";
			this.tsmiUserCommandsEditor.Size = new System.Drawing.Size(209, 22);
			this.tsmiUserCommandsEditor.Text = "外部コマンドエディタ(&C)";
			this.tsmiUserCommandsEditor.Click += new System.EventHandler(this.tsmiUserCommandsEditor_Click);
			// 
			// tsmiNgFavContentsEditor
			// 
			this.tsmiNgFavContentsEditor.Name = "tsmiNgFavContentsEditor";
			this.tsmiNgFavContentsEditor.Size = new System.Drawing.Size(209, 22);
			this.tsmiNgFavContentsEditor.Text = "NG/FAVコンテンツエディタ(&N)";
			this.tsmiNgFavContentsEditor.Click += new System.EventHandler(this.tsmiNgFavContentsEditor_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(206, 6);
			// 
			// tsmiSettingsMainForm
			// 
			this.tsmiSettingsMainForm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspgMainForm});
			this.tsmiSettingsMainForm.Name = "tsmiSettingsMainForm";
			this.tsmiSettingsMainForm.Size = new System.Drawing.Size(209, 22);
			this.tsmiSettingsMainForm.Text = "メインフォーム(&M)";
			this.tsmiSettingsMainForm.DropDownOpened += new System.EventHandler(this.tsmiSettingsMainForm_DropDownOpened);
			// 
			// tspgMainForm
			// 
			this.tspgMainForm.Name = "tspgMainForm";
			this.tspgMainForm.SelectedObject = null;
			this.tspgMainForm.Size = new System.Drawing.Size(200, 300);
			this.tspgMainForm.Text = "toolStripPropertyGrid1";
			// 
			// tsmiSettingsGenreTab
			// 
			this.tsmiSettingsGenreTab.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspgGenreTab});
			this.tsmiSettingsGenreTab.Name = "tsmiSettingsGenreTab";
			this.tsmiSettingsGenreTab.Size = new System.Drawing.Size(209, 22);
			this.tsmiSettingsGenreTab.Text = "ジャンルタブ(&T)";
			// 
			// tspgGenreTab
			// 
			this.tspgGenreTab.Name = "tspgGenreTab";
			this.tspgGenreTab.SelectedObject = null;
			this.tspgGenreTab.Size = new System.Drawing.Size(200, 300);
			this.tspgGenreTab.Text = "toolStripPropertyGrid1";
			// 
			// tsmiSettingsResultView
			// 
			this.tsmiSettingsResultView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspgResultView});
			this.tsmiSettingsResultView.Name = "tsmiSettingsResultView";
			this.tsmiSettingsResultView.Size = new System.Drawing.Size(209, 22);
			this.tsmiSettingsResultView.Text = "クロール結果ビュー(&R)";
			this.tsmiSettingsResultView.DropDownOpened += new System.EventHandler(this.tsmiSettingsResultView_DropDownOpened);
			// 
			// tspgResultView
			// 
			this.tspgResultView.Name = "tspgResultView";
			this.tspgResultView.SelectedObject = null;
			this.tspgResultView.Size = new System.Drawing.Size(200, 300);
			this.tspgResultView.Text = "toolStripPropertyGrid1";
			// 
			// tsmiSettingsPlaylistView
			// 
			this.tsmiSettingsPlaylistView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspgPlaylistView});
			this.tsmiSettingsPlaylistView.Name = "tsmiSettingsPlaylistView";
			this.tsmiSettingsPlaylistView.Size = new System.Drawing.Size(209, 22);
			this.tsmiSettingsPlaylistView.Text = "プレイリストビュー(&P)";
			this.tsmiSettingsPlaylistView.DropDownOpened += new System.EventHandler(this.tsmiSettingsPlaylistView_DropDownOpened);
			// 
			// tspgPlaylistView
			// 
			this.tspgPlaylistView.Name = "tspgPlaylistView";
			this.tspgPlaylistView.SelectedObject = null;
			this.tspgPlaylistView.Size = new System.Drawing.Size(200, 300);
			this.tspgPlaylistView.Text = "toolStripPropertyGrid1";
			// 
			// tsmiSettingsDetailView
			// 
			this.tsmiSettingsDetailView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspgDetailView});
			this.tsmiSettingsDetailView.Name = "tsmiSettingsDetailView";
			this.tsmiSettingsDetailView.Size = new System.Drawing.Size(209, 22);
			this.tsmiSettingsDetailView.Text = "詳細ビュー(&D)";
			this.tsmiSettingsDetailView.DropDownOpened += new System.EventHandler(this.tsmiSettingsDetailView_DropDownOpened);
			// 
			// tspgDetailView
			// 
			this.tspgDetailView.Name = "tspgDetailView";
			this.tspgDetailView.SelectedObject = null;
			this.tspgDetailView.Size = new System.Drawing.Size(200, 300);
			this.tspgDetailView.Text = "toolStripPropertyGrid1";
			// 
			// tshmiHelp
			// 
			this.tshmiHelp.Name = "tshmiHelp";
			this.tshmiHelp.Size = new System.Drawing.Size(62, 16);
			this.tshmiHelp.Text = "ヘルプ(&H)";
			// 
			// inputBoxDialog1
			// 
			this.inputBoxDialog1.Input = null;
			this.inputBoxDialog1.Message = null;
			this.inputBoxDialog1.Title = null;
			// 
			// timerViewDetail
			// 
			this.timerViewDetail.Interval = 10;
			this.timerViewDetail.Tick += new System.EventHandler(this.timerViewDetail_Tick);
			// 
			// timerCrawlProgress
			// 
			this.timerCrawlProgress.Interval = 50;
			this.timerCrawlProgress.Tick += new System.EventHandler(this.timerCrawlProgress_Tick);
			// 
			// timerClearStatusText
			// 
			this.timerClearStatusText.Interval = 10000;
			this.timerClearStatusText.Tick += new System.EventHandler(this.timerClearStatusText_Tick);
			// 
			// bwCrawl
			// 
			this.bwCrawl.WorkerReportsProgress = true;
			this.bwCrawl.WorkerSupportsCancellation = true;
			this.bwCrawl.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCrawl_DoWork);
			this.bwCrawl.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCrawl_RunWorkerCompleted);
			this.bwCrawl.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwCrawl_ProgressChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(872, 606);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.scListsAndDetail.Panel1.ResumeLayout(false);
			this.scListsAndDetail.Panel2.ResumeLayout(false);
			this.scListsAndDetail.ResumeLayout(false);
			this.scLists.Panel1.ResumeLayout(false);
			this.scLists.Panel2.ResumeLayout(false);
			this.scLists.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.genreTabControl1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStripProgressBar tspbCrawl;
		private System.Windows.Forms.ToolStripStatusLabel tsslCrawl;
		private GenreTabControl genreTabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private CrawlResultView crawlResultView1;
		private ContentDetailView contentDetailView1;
		private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseTop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiTools;
		private System.Windows.Forms.SplitContainer scLists;
		private PlayListView playListView1;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbortCrawling;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowsePackage;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiMergeResults;
		private InputBoxDialog inputBoxDialog1;
		private System.Windows.Forms.Timer timerViewDetail;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommandsEditor;
		private System.Windows.Forms.Timer timerCrawlProgress;
		private System.Windows.Forms.Timer timerClearStatusText;
		private System.ComponentModel.BackgroundWorker bwCrawl;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettingsGlobal;
		private ToolStripPropertyGrid tspgGlobal;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettingsMainForm;
		private ToolStripPropertyGrid tspgMainForm;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettingsResultView;
		private ToolStripPropertyGrid tspgResultView;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettingsPlaylistView;
		private ToolStripPropertyGrid tspgPlaylistView;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettingsDetailView;
		private ToolStripPropertyGrid tspgDetailView;
		private ToolStripUserCommandMenuItem tsucmiCommand;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettingsGenreTab;
		private ToolStripPropertyGrid tspgGenreTab;
		private ToolStripGenreMenuItem tsgmiSelectGenre;
		private ToolStripGenreMenuItem tsgmiCrawlGenre;
		private ToolStripGenreMenuItem tsgmiUncrawlables;
		private System.Windows.Forms.ToolStripMenuItem tsmiSerializeSettingsNow;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiNgFavContentsEditor;
		private ToolStripHelpMenuItem tshmiHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem tsmiSearchCache;
		private System.Windows.Forms.ToolStripMenuItem tsmiSearchLivedoorGyaO;
		private System.Windows.Forms.SplitContainer scListsAndDetail;


	}
}