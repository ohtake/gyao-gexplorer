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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.genreTabControl1 = new Yusen.GExplorer.GenreTabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.scListsAndDetail = new System.Windows.Forms.SplitContainer();
			this.scLists = new System.Windows.Forms.SplitContainer();
			this.crawlResultView1 = new Yusen.GExplorer.CrawlResultView();
			this.playListView1 = new Yusen.GExplorer.PlayListView();
			this.contentDetailView1 = new Yusen.GExplorer.ContentDetailView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowsePackage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseContent = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUncrawlableGenres = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiGlobalSettingsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUserCommandsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNgContentsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFocusOnResultAfterTabChanged = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMergeResults = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiClearCrawlResults = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemoveCaches = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemoveCachesUnreachable = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemoveCachesAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemoveDeadlineEntries = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemoveDeadlineEntriesUnreacheable = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemoveDeadlineEntriesAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiUserCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAbortCrawling = new System.Windows.Forms.ToolStripMenuItem();
			this.inputBoxDialog1 = new Yusen.GExplorer.InputBoxDialog();
			this.timerViewDetail = new System.Windows.Forms.Timer(this.components);
			this.timerCrawlProgress = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.genreTabControl1.SuspendLayout();
			this.scListsAndDetail.Panel1.SuspendLayout();
			this.scListsAndDetail.Panel2.SuspendLayout();
			this.scListsAndDetail.SuspendLayout();
			this.scLists.Panel1.SuspendLayout();
			this.scLists.Panel2.SuspendLayout();
			this.scLists.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbCrawl,
            this.tsslCrawl});
			this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip1.Size = new System.Drawing.Size(807, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tspbCrawl
			// 
			this.tspbCrawl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.tspbCrawl.Name = "tspbCrawl";
			this.tspbCrawl.Size = new System.Drawing.Size(100, 15);
			this.tspbCrawl.Text = "tspbCrawl";
			// 
			// tsslCrawl
			// 
			this.tsslCrawl.Name = "tsslCrawl";
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
			this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(807, 536);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.genreTabControl1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.scListsAndDetail, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(807, 490);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// genreTabControl1
			// 
			this.genreTabControl1.Controls.Add(this.tabPage1);
			this.genreTabControl1.Controls.Add(this.tabPage2);
			this.genreTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.genreTabControl1.HotTrack = true;
			this.genreTabControl1.Location = new System.Drawing.Point(0, 0);
			this.genreTabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.genreTabControl1.Name = "genreTabControl1";
			this.genreTabControl1.SelectedGenre = null;
			this.genreTabControl1.SelectedIndex = 0;
			this.genreTabControl1.Size = new System.Drawing.Size(807, 20);
			this.genreTabControl1.TabIndex = 0;
			this.genreTabControl1.GenreSelected += new System.EventHandler<Yusen.GExplorer.GenreTabPageEventArgs>(this.genreTabControl1_GenreSelected);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(799, 0);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(799, 0);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// scListsAndDetail
			// 
			this.scListsAndDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scListsAndDetail.Location = new System.Drawing.Point(0, 20);
			this.scListsAndDetail.Margin = new System.Windows.Forms.Padding(0);
			this.scListsAndDetail.Name = "scListsAndDetail";
			// 
			// scListsAndDetail.Panel1
			// 
			this.scListsAndDetail.Panel1.Controls.Add(this.scLists);
			// 
			// scListsAndDetail.Panel2
			// 
			this.scListsAndDetail.Panel2.Controls.Add(this.contentDetailView1);
			this.scListsAndDetail.Size = new System.Drawing.Size(807, 470);
			this.scListsAndDetail.SplitterDistance = 548;
			this.scListsAndDetail.TabIndex = 2;
			this.scListsAndDetail.Text = "splitContainer1";
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
			this.scLists.Panel1.Controls.Add(this.crawlResultView1);
			// 
			// scLists.Panel2
			// 
			this.scLists.Panel2.Controls.Add(this.playListView1);
			this.scLists.Size = new System.Drawing.Size(548, 470);
			this.scLists.SplitterDistance = 319;
			this.scLists.TabIndex = 2;
			this.scLists.Text = "splitContainer2";
			// 
			// crawlResultView1
			// 
			this.crawlResultView1.AboneType = Yusen.GExplorer.AboneType.Toumei;
			this.crawlResultView1.ClearFilterStringOnHideEnabled = true;
			this.crawlResultView1.CrawlResult = null;
			this.crawlResultView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.crawlResultView1.FilterEnabled = false;
			this.crawlResultView1.HoverSelect = false;
			this.crawlResultView1.Location = new System.Drawing.Point(0, 0);
			this.crawlResultView1.Margin = new System.Windows.Forms.Padding(0);
			this.crawlResultView1.MultiSelect = true;
			this.crawlResultView1.Name = "crawlResultView1";
			this.crawlResultView1.NewColor = System.Drawing.Color.Red;
			this.crawlResultView1.ShowPackages = true;
			this.crawlResultView1.Size = new System.Drawing.Size(548, 319);
			this.crawlResultView1.TabIndex = 1;
			this.crawlResultView1.ContentSelectionChanged += new System.EventHandler<Yusen.GExplorer.ContentSelectionChangedEventArgs>(this.crawlResultView1_ContentSelectionChanged);
			// 
			// playListView1
			// 
			this.playListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.playListView1.FocusedContent = null;
			this.playListView1.Location = new System.Drawing.Point(0, 0);
			this.playListView1.MultiSelectEnabled = true;
			this.playListView1.Name = "playListView1";
			this.playListView1.Size = new System.Drawing.Size(548, 147);
			this.playListView1.TabIndex = 0;
			this.playListView1.TopContent = null;
			this.playListView1.ContentSelectionChanged += new System.EventHandler<Yusen.GExplorer.ContentSelectionChangedEventArgs>(this.playListView1_ContentSelectionChanged);
			// 
			// contentDetailView1
			// 
			this.contentDetailView1.Content = null;
			this.contentDetailView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contentDetailView1.Location = new System.Drawing.Point(0, 0);
			this.contentDetailView1.Name = "contentDetailView1";
			this.contentDetailView1.Size = new System.Drawing.Size(255, 470);
			this.contentDetailView1.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowItemReorder = true;
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiUncrawlableGenres,
            this.tsmiSettings,
            this.tsmiTools,
            this.tsmiAbortCrawling});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(807, 24);
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
			this.tsmiFile.Text = "ファイル(&F)";
			// 
			// tsmiBrowseTop
			// 
			this.tsmiBrowseTop.Name = "tsmiBrowseTop";
			this.tsmiBrowseTop.Text = "トップページをウェブブラウザで開く(&T)";
			this.tsmiBrowseTop.Click += new System.EventHandler(this.tsmiBrowseTop_Click);
			// 
			// tsmiBrowsePackage
			// 
			this.tsmiBrowsePackage.Name = "tsmiBrowsePackage";
			this.tsmiBrowsePackage.Text = "パッケージIDを指定してウェブブラウザで開く(&P) ...";
			this.tsmiBrowsePackage.Click += new System.EventHandler(this.tsmiBrowsePackage_Click);
			// 
			// tsmiBrowseContent
			// 
			this.tsmiBrowseContent.Name = "tsmiBrowseContent";
			this.tsmiBrowseContent.Text = "コンテンツIDを指定してウェブブラウザで開く(&C) ...";
			this.tsmiBrowseContent.Click += new System.EventHandler(this.tsmiBrowseContent_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiQuit
			// 
			this.tsmiQuit.Name = "tsmiQuit";
			this.tsmiQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.tsmiQuit.Text = "終了(&Q)";
			this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
			// 
			// tsmiUncrawlableGenres
			// 
			this.tsmiUncrawlableGenres.Name = "tsmiUncrawlableGenres";
			this.tsmiUncrawlableGenres.Text = "未対応ジャンル(&U)";
			// 
			// tsmiSettings
			// 
			this.tsmiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGlobalSettingsEditor,
            this.tsmiUserCommandsEditor,
            this.tsmiNgContentsEditor,
            this.toolStripSeparator5,
            this.tsmiFocusOnResultAfterTabChanged});
			this.tsmiSettings.Name = "tsmiSettings";
			this.tsmiSettings.Text = "設定(&S)";
			// 
			// tsmiGlobalSettingsEditor
			// 
			this.tsmiGlobalSettingsEditor.Name = "tsmiGlobalSettingsEditor";
			this.tsmiGlobalSettingsEditor.Text = "グローバル設定エディタ(&G)";
			this.tsmiGlobalSettingsEditor.Click += new System.EventHandler(this.tsmiGlobalSettingsEditor_Click);
			// 
			// tsmiUserCommandsEditor
			// 
			this.tsmiUserCommandsEditor.Name = "tsmiUserCommandsEditor";
			this.tsmiUserCommandsEditor.Text = "外部コマンドエディタ(&C)";
			this.tsmiUserCommandsEditor.Click += new System.EventHandler(this.tsmiUserCommandsEditor_Click);
			// 
			// tsmiNgContentsEditor
			// 
			this.tsmiNgContentsEditor.Name = "tsmiNgContentsEditor";
			this.tsmiNgContentsEditor.Text = "NGコンテンツエディタ(&N)";
			this.tsmiNgContentsEditor.Click += new System.EventHandler(this.tsmiNgContentsEditor_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			// 
			// tsmiFocusOnResultAfterTabChanged
			// 
			this.tsmiFocusOnResultAfterTabChanged.Checked = true;
			this.tsmiFocusOnResultAfterTabChanged.CheckOnClick = true;
			this.tsmiFocusOnResultAfterTabChanged.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiFocusOnResultAfterTabChanged.Name = "tsmiFocusOnResultAfterTabChanged";
			this.tsmiFocusOnResultAfterTabChanged.Text = "タブ切り替えでフォーカス移動(&F)";
			// 
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMergeResults,
            this.toolStripSeparator4,
            this.tsmiClearCrawlResults,
            this.tsmiRemoveCaches,
            this.tsmiRemoveDeadlineEntries,
            this.toolStripSeparator2,
            this.tsmiUserCommands});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Text = "ツール(&T)";
			// 
			// tsmiMergeResults
			// 
			this.tsmiMergeResults.Name = "tsmiMergeResults";
			this.tsmiMergeResults.Text = "既得ジャンルを結合して表示(&M)";
			this.tsmiMergeResults.Click += new System.EventHandler(this.tsmiMergeResults_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			// 
			// tsmiClearCrawlResults
			// 
			this.tsmiClearCrawlResults.Name = "tsmiClearCrawlResults";
			this.tsmiClearCrawlResults.Text = "クロール結果の破棄(&D) ...";
			this.tsmiClearCrawlResults.Click += new System.EventHandler(this.tsmiClearCrawlResults_Click);
			// 
			// tsmiRemoveCaches
			// 
			this.tsmiRemoveCaches.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveCachesUnreachable,
            this.tsmiRemoveCachesAll});
			this.tsmiRemoveCaches.Name = "tsmiRemoveCaches";
			this.tsmiRemoveCaches.Text = "キャッシュの削除(&R)";
			// 
			// tsmiRemoveCachesUnreachable
			// 
			this.tsmiRemoveCachesUnreachable.Name = "tsmiRemoveCachesUnreachable";
			this.tsmiRemoveCachesUnreachable.Text = "到達不可キャッシュを削除(&U)";
			this.tsmiRemoveCachesUnreachable.Click += new System.EventHandler(this.tsmiRemoveCachesUnreachable_Click);
			// 
			// tsmiRemoveCachesAll
			// 
			this.tsmiRemoveCachesAll.Name = "tsmiRemoveCachesAll";
			this.tsmiRemoveCachesAll.Text = "全てのキャッシュを削除(&A)...";
			this.tsmiRemoveCachesAll.Click += new System.EventHandler(this.tsmiRemoveCachesAll_Click);
			// 
			// tsmiRemoveDeadlineEntries
			// 
			this.tsmiRemoveDeadlineEntries.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveDeadlineEntriesUnreacheable,
            this.tsmiRemoveDeadlineEntriesAll});
			this.tsmiRemoveDeadlineEntries.Name = "tsmiRemoveDeadlineEntries";
			this.tsmiRemoveDeadlineEntries.Text = "配信期限辞書の整理(&L)";
			// 
			// tsmiRemoveDeadlineEntriesUnreacheable
			// 
			this.tsmiRemoveDeadlineEntriesUnreacheable.Name = "tsmiRemoveDeadlineEntriesUnreacheable";
			this.tsmiRemoveDeadlineEntriesUnreacheable.Text = "到達不可エントリーを削除(&U)";
			this.tsmiRemoveDeadlineEntriesUnreacheable.Click += new System.EventHandler(this.tsmiRemoveDeadlineEntriesUnreacheable_Click);
			// 
			// tsmiRemoveDeadlineEntriesAll
			// 
			this.tsmiRemoveDeadlineEntriesAll.Name = "tsmiRemoveDeadlineEntriesAll";
			this.tsmiRemoveDeadlineEntriesAll.Text = "全てのエントリーを削除(&A)...";
			this.tsmiRemoveDeadlineEntriesAll.Click += new System.EventHandler(this.tsmiRemoveDeadlineEntriesAll_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiUserCommands
			// 
			this.tsmiUserCommands.Name = "tsmiUserCommands";
			this.tsmiUserCommands.Text = "外部コマンド(&U)";
			// 
			// tsmiAbortCrawling
			// 
			this.tsmiAbortCrawling.Enabled = false;
			this.tsmiAbortCrawling.Name = "tsmiAbortCrawling";
			this.tsmiAbortCrawling.Text = "<<クロール中止 (&A)>>";
			this.tsmiAbortCrawling.Click += new System.EventHandler(this.tsmiAbortCrawling_Click);
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(807, 536);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(300, 200);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.genreTabControl1.ResumeLayout(false);
			this.scListsAndDetail.Panel1.ResumeLayout(false);
			this.scListsAndDetail.Panel2.ResumeLayout(false);
			this.scListsAndDetail.ResumeLayout(false);
			this.scLists.Panel1.ResumeLayout(false);
			this.scLists.Panel2.ResumeLayout(false);
			this.scLists.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
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
		private System.Windows.Forms.SplitContainer scListsAndDetail;
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
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveCaches;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveCachesUnreachable;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
		private System.Windows.Forms.ToolStripMenuItem tsmiFocusOnResultAfterTabChanged;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveCachesAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiClearCrawlResults;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommands;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowsePackage;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiMergeResults;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiUncrawlableGenres;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveDeadlineEntries;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveDeadlineEntriesUnreacheable;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveDeadlineEntriesAll;
		private InputBoxDialog inputBoxDialog1;
		private System.Windows.Forms.Timer timerViewDetail;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiGlobalSettingsEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommandsEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiNgContentsEditor;
		private System.Windows.Forms.Timer timerCrawlProgress;


	}
}