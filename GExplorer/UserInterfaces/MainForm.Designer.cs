﻿namespace Yusen.GExplorer.UserInterfaces {
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCreateNewPlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiBrowseTopPage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowsePackagePage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseContentPage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiGenreTab = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCrawlResultView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlaylistCollection = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDetailView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSearchLivedoorGyaO = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCacheViewer = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRuleEditForm = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiExternalCommandsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOptionsForm = new System.Windows.Forms.ToolStripMenuItem();
			this.tshmiHelp = new Yusen.GExplorer.UserInterfaces.ToolStripHelpMenuItem();
			this.tsmiAbortCrawl = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tspbProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.tsslMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.genreSelctControl1 = new Yusen.GExplorer.UserInterfaces.GenreSelectControl();
			this.crawlResultView = new Yusen.GExplorer.UserInterfaces.CrawlResultView();
			this.playlistsView1 = new Yusen.GExplorer.UserInterfaces.PlaylistsView();
			this.detailView1 = new Yusen.GExplorer.UserInterfaces.DetailView();
			this.inputBoxDialog1 = new Yusen.GExplorer.UserInterfaces.InputBoxDialog();
			this.timerContentSelect = new System.Windows.Forms.Timer(this.components);
			this.timerMessage = new System.Windows.Forms.Timer(this.components);
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.menuStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiGenreTab,
            this.tsmiCrawlResultView,
            this.tsmiPlaylistCollection,
            this.tsmiPlaylist,
            this.tsmiDetailView,
            this.tsmiTools,
            this.tshmiHelp,
            this.tsmiAbortCrawl});
			this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip1.Size = new System.Drawing.Size(792, 21);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCreateNewPlaylist,
            this.toolStripMenuItem2,
            this.tsmiBrowseTopPage,
            this.tsmiBrowsePackagePage,
            this.tsmiBrowseContentPage,
            this.toolStripMenuItem1,
            this.tsmiQuit});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Size = new System.Drawing.Size(67, 17);
			this.tsmiFile.Text = "ファイル(&F)";
			// 
			// tsmiCreateNewPlaylist
			// 
			this.tsmiCreateNewPlaylist.Name = "tsmiCreateNewPlaylist";
			this.tsmiCreateNewPlaylist.Size = new System.Drawing.Size(226, 22);
			this.tsmiCreateNewPlaylist.Text = "プレイリストの新規作成(&N)...";
			this.tsmiCreateNewPlaylist.Click += new System.EventHandler(this.tsmiCreateNewPlaylist_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(223, 6);
			// 
			// tsmiBrowseTopPage
			// 
			this.tsmiBrowseTopPage.Image = global::Yusen.GExplorer.Properties.Resources.home;
			this.tsmiBrowseTopPage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiBrowseTopPage.Name = "tsmiBrowseTopPage";
			this.tsmiBrowseTopPage.Size = new System.Drawing.Size(226, 22);
			this.tsmiBrowseTopPage.Text = "トップページを開く(&T)";
			this.tsmiBrowseTopPage.Click += new System.EventHandler(this.tsmiBrowseTopPage_Click);
			// 
			// tsmiBrowsePackagePage
			// 
			this.tsmiBrowsePackagePage.Name = "tsmiBrowsePackagePage";
			this.tsmiBrowsePackagePage.Size = new System.Drawing.Size(226, 22);
			this.tsmiBrowsePackagePage.Text = "パッケージIDを指定して開く(&P)...";
			this.tsmiBrowsePackagePage.Click += new System.EventHandler(this.tsmiBrowsePackagePage_Click);
			// 
			// tsmiBrowseContentPage
			// 
			this.tsmiBrowseContentPage.Name = "tsmiBrowseContentPage";
			this.tsmiBrowseContentPage.Size = new System.Drawing.Size(226, 22);
			this.tsmiBrowseContentPage.Text = "コンテンツIDを指定して開く(&C)...";
			this.tsmiBrowseContentPage.Click += new System.EventHandler(this.tsmiBrowseContentPage_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(223, 6);
			// 
			// tsmiQuit
			// 
			this.tsmiQuit.Name = "tsmiQuit";
			this.tsmiQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.tsmiQuit.Size = new System.Drawing.Size(226, 22);
			this.tsmiQuit.Text = "終了(&Q)";
			this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
			// 
			// tsmiGenreTab
			// 
			this.tsmiGenreTab.Name = "tsmiGenreTab";
			this.tsmiGenreTab.Size = new System.Drawing.Size(88, 17);
			this.tsmiGenreTab.Text = "ジャンルタブ(&G)";
			// 
			// tsmiCrawlResultView
			// 
			this.tsmiCrawlResultView.Name = "tsmiCrawlResultView";
			this.tsmiCrawlResultView.Size = new System.Drawing.Size(98, 17);
			this.tsmiCrawlResultView.Text = "クロール結果(&W)";
			// 
			// tsmiPlaylistCollection
			// 
			this.tsmiPlaylistCollection.Name = "tsmiPlaylistCollection";
			this.tsmiPlaylistCollection.Size = new System.Drawing.Size(109, 17);
			this.tsmiPlaylistCollection.Text = "プレイリスト一覧(&C)";
			// 
			// tsmiPlaylist
			// 
			this.tsmiPlaylist.Name = "tsmiPlaylist";
			this.tsmiPlaylist.Size = new System.Drawing.Size(84, 17);
			this.tsmiPlaylist.Text = "プレイリスト(&P)";
			// 
			// tsmiDetailView
			// 
			this.tsmiDetailView.Name = "tsmiDetailView";
			this.tsmiDetailView.Size = new System.Drawing.Size(84, 17);
			this.tsmiDetailView.Text = "詳細ビュー(&D)";
			// 
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSearchLivedoorGyaO,
            this.toolStripMenuItem3,
            this.tsmiCacheViewer,
            this.tsmiRuleEditForm,
            this.tsmiExternalCommandsEditor,
            this.toolStripMenuItem4,
            this.tsmiOptionsForm});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Size = new System.Drawing.Size(62, 17);
			this.tsmiTools.Text = "ツール(&T)";
			// 
			// tsmiSearchLivedoorGyaO
			// 
			this.tsmiSearchLivedoorGyaO.Image = global::Yusen.GExplorer.Properties.Resources.searchWeb;
			this.tsmiSearchLivedoorGyaO.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiSearchLivedoorGyaO.Name = "tsmiSearchLivedoorGyaO";
			this.tsmiSearchLivedoorGyaO.Size = new System.Drawing.Size(222, 22);
			this.tsmiSearchLivedoorGyaO.Text = "livedoor動画でGyaO検索(&L)...";
			this.tsmiSearchLivedoorGyaO.Click += new System.EventHandler(this.tsmiSearchLivedoorGyaO_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(219, 6);
			// 
			// tsmiCacheViewer
			// 
			this.tsmiCacheViewer.Name = "tsmiCacheViewer";
			this.tsmiCacheViewer.Size = new System.Drawing.Size(222, 22);
			this.tsmiCacheViewer.Text = "キャッシュビューア(&C)";
			this.tsmiCacheViewer.Click += new System.EventHandler(this.tsmiCacheViewer_Click);
			// 
			// tsmiRuleEditForm
			// 
			this.tsmiRuleEditForm.Name = "tsmiRuleEditForm";
			this.tsmiRuleEditForm.Size = new System.Drawing.Size(222, 22);
			this.tsmiRuleEditForm.Text = "仕分けルールエディタ(&R)";
			this.tsmiRuleEditForm.Click += new System.EventHandler(this.tsmiRuleEditForm_Click);
			// 
			// tsmiExternalCommandsEditor
			// 
			this.tsmiExternalCommandsEditor.Name = "tsmiExternalCommandsEditor";
			this.tsmiExternalCommandsEditor.Size = new System.Drawing.Size(222, 22);
			this.tsmiExternalCommandsEditor.Text = "外部コマンドエディタ(&X)";
			this.tsmiExternalCommandsEditor.Click += new System.EventHandler(this.tsmiExternalCommandsEditor_Click);
			// 
			// tsmiOptionsForm
			// 
			this.tsmiOptionsForm.Name = "tsmiOptionsForm";
			this.tsmiOptionsForm.Size = new System.Drawing.Size(222, 22);
			this.tsmiOptionsForm.Text = "オプション(&O)";
			this.tsmiOptionsForm.Click += new System.EventHandler(this.tsmiOptionsForm_Click);
			// 
			// tshmiHelp
			// 
			this.tshmiHelp.Name = "tshmiHelp";
			this.tshmiHelp.Size = new System.Drawing.Size(63, 17);
			this.tshmiHelp.Text = "ヘルプ(&H)";
			// 
			// tsmiAbortCrawl
			// 
			this.tsmiAbortCrawl.Enabled = false;
			this.tsmiAbortCrawl.Name = "tsmiAbortCrawl";
			this.tsmiAbortCrawl.Size = new System.Drawing.Size(127, 17);
			this.tsmiAbortCrawl.Text = "<<クロール中止(&A)>>";
			this.tsmiAbortCrawl.Click += new System.EventHandler(this.tsmiAbortCrawl_Click);
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
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(792, 523);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(792, 566);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbProgress,
            this.tsslMessage});
			this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.statusStrip1.Size = new System.Drawing.Size(792, 22);
			this.statusStrip1.TabIndex = 0;
			// 
			// tspbProgress
			// 
			this.tspbProgress.Name = "tspbProgress";
			this.tspbProgress.Size = new System.Drawing.Size(100, 16);
			// 
			// tsslMessage
			// 
			this.tsslMessage.Name = "tsslMessage";
			this.tsslMessage.Size = new System.Drawing.Size(109, 13);
			this.tsslMessage.Text = "toolStripStatusLabel1";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.detailView1);
			this.splitContainer1.Size = new System.Drawing.Size(792, 523);
			this.splitContainer1.SplitterDistance = 546;
			this.splitContainer1.TabIndex = 1;
			this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.playlistsView1);
			this.splitContainer2.Size = new System.Drawing.Size(546, 523);
			this.splitContainer2.SplitterDistance = 343;
			this.splitContainer2.TabIndex = 0;
			this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer3.IsSplitterFixed = true;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.genreSelctControl1);
			this.splitContainer3.Panel1MinSize = 0;
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.crawlResultView);
			this.splitContainer3.Panel2MinSize = 0;
			this.splitContainer3.Size = new System.Drawing.Size(546, 343);
			this.splitContainer3.SplitterDistance = 46;
			this.splitContainer3.SplitterWidth = 1;
			this.splitContainer3.TabIndex = 1;
			// 
			// genreSelctControl1
			// 
			this.genreSelctControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.genreSelctControl1.Location = new System.Drawing.Point(0, 0);
			this.genreSelctControl1.Margin = new System.Windows.Forms.Padding(0);
			this.genreSelctControl1.Name = "genreSelctControl1";
			this.genreSelctControl1.Size = new System.Drawing.Size(546, 46);
			this.genreSelctControl1.TabIndex = 0;
			this.genreSelctControl1.CrawlStarted += new System.EventHandler(this.genreSelctControl1_CrawlStarted);
			this.genreSelctControl1.CrawlResultSelected += new System.EventHandler(this.genreSelctControl1_CrawlResultSelected);
			this.genreSelctControl1.CrawlProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.genreSelctControl1_CrawlProgressChanged);
			this.genreSelctControl1.StatusMessageChanged += new System.EventHandler(this.genreSelctControl1_StatusMessageChanged);
			this.genreSelctControl1.RequiredHeightChanged += new System.EventHandler(this.genreSelctControl1_RequiredHeightChanged);
			this.genreSelctControl1.CrawlEnded += new System.EventHandler(this.genreSelctControl1_CrawlEnded);
			// 
			// crawlResultView
			// 
			this.crawlResultView.CaseInsensitiveFilter = true;
			this.crawlResultView.ColorFilter = System.Drawing.Color.LemonChiffon;
			this.crawlResultView.ColorModified = System.Drawing.Color.Orange;
			this.crawlResultView.ColorNew = System.Drawing.Color.Red;
			this.crawlResultView.ColWidthDeadline = 80;
			this.crawlResultView.ColWidthDuration = 52;
			this.crawlResultView.ColWidthId = 66;
			this.crawlResultView.ColWidthSeriesNumber = 50;
			this.crawlResultView.ColWidthSubtitle = 70;
			this.crawlResultView.ColWidthSummary = 135;
			this.crawlResultView.ColWidthTitle = 55;
			this.crawlResultView.ContentVisibilities = ((Yusen.GExplorer.UserInterfaces.ContentVisibilities)(((((Yusen.GExplorer.UserInterfaces.ContentVisibilities.NgFalse | Yusen.GExplorer.UserInterfaces.ContentVisibilities.FavTrue)
						| Yusen.GExplorer.UserInterfaces.ContentVisibilities.FavFalse)
						| Yusen.GExplorer.UserInterfaces.ContentVisibilities.NewTrue)
						| Yusen.GExplorer.UserInterfaces.ContentVisibilities.NewFalse)));
			this.crawlResultView.CrvView = Yusen.GExplorer.UserInterfaces.CrawlResultViewView.Details;
			this.crawlResultView.DestinationPlaylistName = "My Playlist 1";
			this.crawlResultView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.crawlResultView.FilterBarVisible = false;
			this.crawlResultView.FilterType = Yusen.GExplorer.UserInterfaces.CrawlResultViewFilterType.Normal;
			this.crawlResultView.GroupingAtTheBegining = true;
			this.crawlResultView.IncrementalFilterEnabled = true;
			this.crawlResultView.LastSelectingPlaylist = null;
			this.crawlResultView.Location = new System.Drawing.Point(0, 0);
			this.crawlResultView.Name = "crawlResultView";
			this.crawlResultView.Size = new System.Drawing.Size(546, 296);
			this.crawlResultView.TabIndex = 0;
			this.crawlResultView.LastSelectedContentChanged += new System.EventHandler(this.crawlResultView_LastSelectedContentChanged);
			this.crawlResultView.LastSelectingPlaylistChanged += new System.EventHandler(this.crawlResultView_LastSelectingPlaylistChanged);
			// 
			// playlistsView1
			// 
			this.playlistsView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.playlistsView1.Location = new System.Drawing.Point(0, 0);
			this.playlistsView1.Name = "playlistsView1";
			this.playlistsView1.PlaylistCollectionWidth = 77;
			this.playlistsView1.Size = new System.Drawing.Size(546, 176);
			this.playlistsView1.TabIndex = 0;
			this.playlistsView1.LastSelectedContentChanged += new System.EventHandler(this.playlistsView1_LastSelectedContentChanged);
			// 
			// detailView1
			// 
			this.detailView1.ColWidthAuthor = 50;
			this.detailView1.ColWidthNetabare = 14;
			this.detailView1.ColWidthPosted = 72;
			this.detailView1.ColWidthRef = 42;
			this.detailView1.ColWidthScore = 24;
			this.detailView1.ColWidthTitle = 80;
			this.detailView1.Description1Style = "font-size:12px;";
			this.detailView1.Description2Style = "font-size:12px;";
			this.detailView1.Description3Style = "font-size:10px;";
			this.detailView1.Description4Style = "font-size:10px; text-align:right;";
			this.detailView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.detailView1.ImageHeight = 135;
			this.detailView1.LoadImage = true;
			this.detailView1.LoadPage = true;
			this.detailView1.Location = new System.Drawing.Point(0, 0);
			this.detailView1.Name = "detailView1";
			this.detailView1.ReviewListHeight = 173;
			this.detailView1.Size = new System.Drawing.Size(242, 523);
			this.detailView1.TabIndex = 0;
			this.detailView1.StatusMessageChanged += new System.EventHandler(this.detailView1_StatusMessageChanged);
			// 
			// inputBoxDialog1
			// 
			this.inputBoxDialog1.Input = null;
			this.inputBoxDialog1.Message = null;
			this.inputBoxDialog1.Title = null;
			// 
			// timerContentSelect
			// 
			this.timerContentSelect.Interval = 10;
			this.timerContentSelect.Tick += new System.EventHandler(this.timerContentSelect_Tick);
			// 
			// timerMessage
			// 
			this.timerMessage.Interval = 15000;
			this.timerMessage.Tick += new System.EventHandler(this.timerMessage_Tick);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(219, 6);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 566);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private ToolStripHelpMenuItem tshmiHelp;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private DetailView detailView1;
		private GenreSelectControl genreSelctControl1;
		private PlaylistsView playlistsView1;
		private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
		private System.Windows.Forms.ToolStripMenuItem tsmiCreateNewPlaylist;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private InputBoxDialog inputBoxDialog1;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.ToolStripProgressBar tspbProgress;
		private System.Windows.Forms.ToolStripStatusLabel tsslMessage;
		private System.Windows.Forms.ToolStripMenuItem tsmiGenreTab;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbortCrawl;
		private System.Windows.Forms.ToolStripMenuItem tsmiTools;
		private System.Windows.Forms.ToolStripMenuItem tsmiCacheViewer;
		private System.Windows.Forms.ToolStripMenuItem tsmiOptionsForm;
		private System.Windows.Forms.ToolStripMenuItem tsmiCrawlResultView;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlaylistCollection;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlaylist;
		private System.Windows.Forms.ToolStripMenuItem tsmiDetailView;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseTopPage;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowsePackagePage;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseContentPage;
		private System.Windows.Forms.ToolStripMenuItem tsmiSearchLivedoorGyaO;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.Timer timerContentSelect;
		private System.Windows.Forms.ToolStripMenuItem tsmiExternalCommandsEditor;
		private CrawlResultView crawlResultView;
		private System.Windows.Forms.Timer timerMessage;
		private System.Windows.Forms.ToolStripMenuItem tsmiRuleEditForm;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
	}
}