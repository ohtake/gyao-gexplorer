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
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUncrawableGenres = new System.Windows.Forms.ToolStripMenuItem();
			this.設定SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFocusOnResultAfterTabChanged = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiIgnoreCrawlErrors = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiGlobalSettingsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUserCommandsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNgContentsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRemoveCaches = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemoveCachesUnreachable = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemoveCachesAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAbortCrawling = new System.Windows.Forms.ToolStripMenuItem();
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
			this.statusStrip1.Size = new System.Drawing.Size(675, 22);
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
			this.toolStripContainer1.Size = new System.Drawing.Size(675, 476);
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(675, 430);
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
			this.genreTabControl1.Size = new System.Drawing.Size(675, 20);
			this.genreTabControl1.TabIndex = 0;
			this.genreTabControl1.GenreSelected += new System.EventHandler<Yusen.GExplorer.GenreTabPageEventArgs>(this.genreTabControl1_GenreSelected);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(667, 0);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(667, 0);
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
			this.scListsAndDetail.Size = new System.Drawing.Size(675, 410);
			this.scListsAndDetail.SplitterDistance = 421;
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
			this.scLists.Size = new System.Drawing.Size(421, 410);
			this.scLists.SplitterDistance = 277;
			this.scLists.TabIndex = 2;
			this.scLists.Text = "splitContainer2";
			// 
			// crawlResultView1
			// 
			this.crawlResultView1.AboneType = Yusen.GExplorer.AboneType.Toumei;
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
			this.crawlResultView1.Size = new System.Drawing.Size(421, 277);
			this.crawlResultView1.TabIndex = 1;
			this.crawlResultView1.ContentSelectionChanged += new System.EventHandler<Yusen.GExplorer.ContentSelectionChangedEventArgs>(this.crawlResultView1_ContentSelectionChanged);
			// 
			// playListView1
			// 
			this.playListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.playListView1.Location = new System.Drawing.Point(0, 0);
			this.playListView1.MultiSelectEnabled = true;
			this.playListView1.Name = "playListView1";
			this.playListView1.Size = new System.Drawing.Size(421, 129);
			this.playListView1.TabIndex = 0;
			this.playListView1.ContentSelectionChanged += new System.EventHandler<Yusen.GExplorer.ContentSelectionChangedEventArgs>(this.playListView1_ContentSelectionChanged);
			// 
			// contentDetailView1
			// 
			this.contentDetailView1.Content = null;
			this.contentDetailView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contentDetailView1.Location = new System.Drawing.Point(0, 0);
			this.contentDetailView1.Name = "contentDetailView1";
			this.contentDetailView1.Size = new System.Drawing.Size(250, 410);
			this.contentDetailView1.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowItemReorder = true;
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiUncrawableGenres,
            this.設定SToolStripMenuItem,
            this.tsmiTools,
            this.tsmiAbortCrawling});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(675, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBrowseTop,
            this.toolStripSeparator1,
            this.tsmiQuit});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Text = "ファイル (&F)";
			// 
			// tsmiBrowseTop
			// 
			this.tsmiBrowseTop.Name = "tsmiBrowseTop";
			this.tsmiBrowseTop.Text = "トップページをウェブブラウザで開く (&T)";
			this.tsmiBrowseTop.Click += new System.EventHandler(this.tsmiBrowseTop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiQuit
			// 
			this.tsmiQuit.Name = "tsmiQuit";
			this.tsmiQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.tsmiQuit.Text = "終了 (&Q)";
			this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
			// 
			// tsmiUncrawableGenres
			// 
			this.tsmiUncrawableGenres.Name = "tsmiUncrawableGenres";
			this.tsmiUncrawableGenres.Text = "未対応ジャンル (&U)";
			// 
			// 設定SToolStripMenuItem
			// 
			this.設定SToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFocusOnResultAfterTabChanged,
            this.tsmiIgnoreCrawlErrors});
			this.設定SToolStripMenuItem.Name = "設定SToolStripMenuItem";
			this.設定SToolStripMenuItem.Text = "設定 (&S)";
			// 
			// tsmiFocusOnResultAfterTabChanged
			// 
			this.tsmiFocusOnResultAfterTabChanged.Checked = true;
			this.tsmiFocusOnResultAfterTabChanged.CheckOnClick = true;
			this.tsmiFocusOnResultAfterTabChanged.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiFocusOnResultAfterTabChanged.Name = "tsmiFocusOnResultAfterTabChanged";
			this.tsmiFocusOnResultAfterTabChanged.Text = "ジャンルタブ切り替え後にクロール結果ビューにフォーカス (&F)";
			// 
			// tsmiIgnoreCrawlErrors
			// 
			this.tsmiIgnoreCrawlErrors.Checked = true;
			this.tsmiIgnoreCrawlErrors.CheckOnClick = true;
			this.tsmiIgnoreCrawlErrors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiIgnoreCrawlErrors.Name = "tsmiIgnoreCrawlErrors";
			this.tsmiIgnoreCrawlErrors.Text = "クロール時の無視可能エラーを全て無視 (&I)";
			// 
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGlobalSettingsEditor,
            this.tsmiUserCommandsEditor,
            this.tsmiNgContentsEditor,
            this.toolStripSeparator3,
            this.tsmiRemoveCaches});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Text = "ツール (&T)";
			// 
			// tsmiGlobalSettingsEditor
			// 
			this.tsmiGlobalSettingsEditor.Name = "tsmiGlobalSettingsEditor";
			this.tsmiGlobalSettingsEditor.Text = "グローバル設定エディタ (&G)";
			this.tsmiGlobalSettingsEditor.Click += new System.EventHandler(this.tsmiGlobalSettingsEditor_Click);
			// 
			// tsmiUserCommandsEditor
			// 
			this.tsmiUserCommandsEditor.Name = "tsmiUserCommandsEditor";
			this.tsmiUserCommandsEditor.Text = "外部コマンドエディタ (&C)";
			this.tsmiUserCommandsEditor.Click += new System.EventHandler(this.tsmiUserCommandsEditor_Click);
			// 
			// tsmiNgContentsEditor
			// 
			this.tsmiNgContentsEditor.Name = "tsmiNgContentsEditor";
			this.tsmiNgContentsEditor.Text = "NGコンテンツエディタ (&N)";
			this.tsmiNgContentsEditor.Click += new System.EventHandler(this.tsmiNgContentsEditor_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			// 
			// tsmiRemoveCaches
			// 
			this.tsmiRemoveCaches.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveCachesUnreachable,
            this.tsmiRemoveCachesAll});
			this.tsmiRemoveCaches.Name = "tsmiRemoveCaches";
			this.tsmiRemoveCaches.Text = "キャッシュの削除 (&R)";
			// 
			// tsmiRemoveCachesUnreachable
			// 
			this.tsmiRemoveCachesUnreachable.Name = "tsmiRemoveCachesUnreachable";
			this.tsmiRemoveCachesUnreachable.Text = "現在のクロール結果で到達不可能なキャッシュを削除 (&U) ...";
			this.tsmiRemoveCachesUnreachable.Click += new System.EventHandler(this.tsmiRemoveCachesUnreachable_Click);
			// 
			// tsmiRemoveCachesAll
			// 
			this.tsmiRemoveCachesAll.Name = "tsmiRemoveCachesAll";
			this.tsmiRemoveCachesAll.Text = "全て削除 (&A) ...";
			this.tsmiRemoveCachesAll.Click += new System.EventHandler(this.tsmiRemoveCachesAll_Click);
			// 
			// tsmiAbortCrawling
			// 
			this.tsmiAbortCrawling.Enabled = false;
			this.tsmiAbortCrawling.Name = "tsmiAbortCrawling";
			this.tsmiAbortCrawling.Text = "<<クロール中止 (&A)>>";
			this.tsmiAbortCrawling.Click += new System.EventHandler(this.tsmiAbortCrawling_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(675, 476);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "MainForm";
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
		private System.Windows.Forms.ToolStripMenuItem tsmiGlobalSettingsEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiNgContentsEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommandsEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiUncrawableGenres;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbortCrawling;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveCaches;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveCachesUnreachable;
		private System.Windows.Forms.ToolStripMenuItem 設定SToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiFocusOnResultAfterTabChanged;
		private System.Windows.Forms.ToolStripMenuItem tsmiIgnoreCrawlErrors;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveCachesAll;


	}
}