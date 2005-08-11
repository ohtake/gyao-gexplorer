namespace Yusen.GExplorer {
	partial class PlayerForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPlayList = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lviewPlaylist = new System.Windows.Forms.ListView();
			this.chId = new System.Windows.Forms.ColumnHeader();
			this.chDisplayName = new System.Windows.Forms.ColumnHeader();
			this.cmsPlaylist = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiMove = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMoveTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMoveUp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMoveDown = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMoveBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPlaylistCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.contentDetailView1 = new Yusen.GExplorer.ContentDetailView();
			this.tabPlayer = new System.Windows.Forms.TabPage();
			this.wmpMain = new AxWMPLib.AxWindowsMediaPlayer();
			this.tabBrowser = new System.Windows.Forms.TabPage();
			this.ieMain = new System.Windows.Forms.WebBrowser();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReload = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiExportAsAsx = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOperation = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlayPause = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiStop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPrevTrack = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNextTrack = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFastReverse = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFastForward = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiNextContent = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNextContentWithDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPrevContent = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFullscreen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTool = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAutoVolume = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMediaKeys = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemovePlayedContent = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAutoLoadPlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFocusOnWmp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiUserCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.tsmiWaitSecondsAfterLastCall = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1.SuspendLayout();
			this.tabPlayList.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.cmsPlaylist.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tabPlayer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.wmpMain)).BeginInit();
			this.tabBrowser.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPlayList);
			this.tabControl1.Controls.Add(this.tabPlayer);
			this.tabControl1.Controls.Add(this.tabBrowser);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(657, 577);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPlayList
			// 
			this.tabPlayList.Controls.Add(this.splitContainer1);
			this.tabPlayList.Location = new System.Drawing.Point(4, 21);
			this.tabPlayList.Name = "tabPlayList";
			this.tabPlayList.Size = new System.Drawing.Size(649, 552);
			this.tabPlayList.TabIndex = 2;
			this.tabPlayList.Text = "プレイリスト";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.contentDetailView1);
			this.splitContainer1.Size = new System.Drawing.Size(649, 552);
			this.splitContainer1.SplitterDistance = 346;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.Text = "splitContainer1";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.lviewPlaylist, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.593407F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.40659F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(346, 552);
			this.tableLayoutPanel1.TabIndex = 3;
			// 
			// lviewPlaylist
			// 
			this.lviewPlaylist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chDisplayName});
			this.lviewPlaylist.ContextMenuStrip = this.cmsPlaylist;
			this.lviewPlaylist.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lviewPlaylist.FullRowSelect = true;
			this.lviewPlaylist.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lviewPlaylist.HideSelection = false;
			this.lviewPlaylist.Location = new System.Drawing.Point(3, 39);
			this.lviewPlaylist.MultiSelect = false;
			this.lviewPlaylist.Name = "lviewPlaylist";
			this.lviewPlaylist.ShowGroups = false;
			this.lviewPlaylist.ShowItemToolTips = true;
			this.lviewPlaylist.Size = new System.Drawing.Size(340, 510);
			this.lviewPlaylist.TabIndex = 2;
			this.lviewPlaylist.View = System.Windows.Forms.View.Details;
			this.lviewPlaylist.SelectedIndexChanged += new System.EventHandler(this.lviewPlaylist_SelectedIndexChanged);
			this.lviewPlaylist.DoubleClick += new System.EventHandler(this.lviewPlaylist_DoubleClick);
			this.lviewPlaylist.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lviewPlaylist_KeyDown);
			// 
			// chId
			// 
			this.chId.Text = "contents_id";
			this.chId.Width = 80;
			// 
			// chDisplayName
			// 
			this.chDisplayName.Text = "表示名";
			this.chDisplayName.Width = 234;
			// 
			// cmsPlaylist
			// 
			this.cmsPlaylist.Enabled = true;
			this.cmsPlaylist.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsPlaylist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPlay,
            this.toolStripSeparator3,
            this.tsmiMove,
            this.tsmiDelete,
            this.toolStripSeparator6,
            this.tsmiPlaylistCommands});
			this.cmsPlaylist.Location = new System.Drawing.Point(25, 66);
			this.cmsPlaylist.Name = "cmsPlaylist";
			this.cmsPlaylist.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsPlaylist.Size = new System.Drawing.Size(161, 104);
			this.cmsPlaylist.Opening += new System.ComponentModel.CancelEventHandler(this.cmsPlaylist_Opening);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.Text = "再生 (&P)";
			this.tsmiPlay.Click += new System.EventHandler(this.tsmiPlay_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			// 
			// tsmiMove
			// 
			this.tsmiMove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMoveTop,
            this.tsmiMoveUp,
            this.tsmiMoveDown,
            this.tsmiMoveBottom});
			this.tsmiMove.Name = "tsmiMove";
			this.tsmiMove.Text = "リスト内を移動 (&M)";
			// 
			// tsmiMoveTop
			// 
			this.tsmiMoveTop.Name = "tsmiMoveTop";
			this.tsmiMoveTop.Text = "最上部へ (&T)";
			this.tsmiMoveTop.Click += new System.EventHandler(this.tsmiMoveTop_Click);
			// 
			// tsmiMoveUp
			// 
			this.tsmiMoveUp.Name = "tsmiMoveUp";
			this.tsmiMoveUp.Text = "一つ上へ (&U)";
			this.tsmiMoveUp.Click += new System.EventHandler(this.tsmiMoveUp_Click);
			// 
			// tsmiMoveDown
			// 
			this.tsmiMoveDown.Name = "tsmiMoveDown";
			this.tsmiMoveDown.Text = "一つ下へ (&D)";
			this.tsmiMoveDown.Click += new System.EventHandler(this.tsmiMoveDown_Click);
			// 
			// tsmiMoveBottom
			// 
			this.tsmiMoveBottom.Name = "tsmiMoveBottom";
			this.tsmiMoveBottom.Text = "最下部へ (&B)";
			this.tsmiMoveBottom.Click += new System.EventHandler(this.tsmiMoveBottom_Click);
			// 
			// tsmiDelete
			// 
			this.tsmiDelete.Name = "tsmiDelete";
			this.tsmiDelete.Text = "削除 (&D)";
			this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			// 
			// tsmiPlaylistCommands
			// 
			this.tsmiPlaylistCommands.Name = "tsmiPlaylistCommands";
			this.tsmiPlaylistCommands.Text = "外部コマンド (&C)";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnUp);
			this.flowLayoutPanel1.Controls.Add(this.btnDown);
			this.flowLayoutPanel1.Controls.Add(this.btnDelete);
			this.flowLayoutPanel1.Controls.Add(this.btnClear);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(340, 30);
			this.flowLayoutPanel1.TabIndex = 3;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// btnUp
			// 
			this.btnUp.Enabled = false;
			this.btnUp.Location = new System.Drawing.Point(3, 3);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(60, 23);
			this.btnUp.TabIndex = 0;
			this.btnUp.Text = "上へ (&U)";
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDown
			// 
			this.btnDown.Enabled = false;
			this.btnDown.Location = new System.Drawing.Point(69, 3);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(60, 23);
			this.btnDown.TabIndex = 1;
			this.btnDown.Text = "下へ (&D)";
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.Location = new System.Drawing.Point(135, 3);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(60, 23);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "削除 (&D)";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(201, 3);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(89, 23);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "全て削除 (&C)";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// contentDetailView1
			// 
			this.contentDetailView1.Content = null;
			this.contentDetailView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contentDetailView1.Location = new System.Drawing.Point(0, 0);
			this.contentDetailView1.Name = "contentDetailView1";
			this.contentDetailView1.Size = new System.Drawing.Size(299, 552);
			this.contentDetailView1.TabIndex = 0;
			// 
			// tabPlayer
			// 
			this.tabPlayer.Controls.Add(this.wmpMain);
			this.tabPlayer.Location = new System.Drawing.Point(4, 21);
			this.tabPlayer.Name = "tabPlayer";
			this.tabPlayer.Size = new System.Drawing.Size(649, 552);
			this.tabPlayer.TabIndex = 0;
			this.tabPlayer.Text = "プレーヤ";
			// 
			// wmpMain
			// 
			this.wmpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wmpMain.Enabled = true;
			this.wmpMain.Location = new System.Drawing.Point(0, 0);
			this.wmpMain.Name = "wmpMain";
			this.wmpMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpMain.OcxState")));
			this.wmpMain.Size = new System.Drawing.Size(649, 552);
			this.wmpMain.Text = "axWindowsMediaPlayer1";
			this.wmpMain.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(this.wmpMain_OpenStateChange);
			this.wmpMain.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.wmpMain_PlayStateChange);
			// 
			// tabBrowser
			// 
			this.tabBrowser.Controls.Add(this.ieMain);
			this.tabBrowser.Location = new System.Drawing.Point(4, 21);
			this.tabBrowser.Name = "tabBrowser";
			this.tabBrowser.Size = new System.Drawing.Size(649, 552);
			this.tabBrowser.TabIndex = 1;
			this.tabBrowser.Text = "詳細ページ";
			// 
			// ieMain
			// 
			this.ieMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ieMain.Location = new System.Drawing.Point(0, 0);
			this.ieMain.Name = "ieMain";
			this.ieMain.Size = new System.Drawing.Size(649, 552);
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(657, 601);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowItemReorder = true;
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiOperation,
            this.tsmiView,
            this.tsmiTool});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(657, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiReload,
            this.tsmiExportAsAsx,
            this.toolStripSeparator2,
            this.tsmiClose});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Text = "ファイル (&F)";
			// 
			// tsmiReload
			// 
			this.tsmiReload.Name = "tsmiReload";
			this.tsmiReload.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.tsmiReload.Text = "動画の再読み込み (&R)";
			this.tsmiReload.Click += new System.EventHandler(this.tsmiReload_Click);
			// 
			// tsmiExportAsAsx
			// 
			this.tsmiExportAsAsx.Name = "tsmiExportAsAsx";
			this.tsmiExportAsAsx.Text = "プレイリストをASXとしてイクスポート (&E) ...";
			this.tsmiExportAsAsx.Click += new System.EventHandler(this.tsmiExportAsAsx_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiClose
			// 
			this.tsmiClose.Name = "tsmiClose";
			this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.tsmiClose.Text = "閉じる (&C)";
			this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
			// 
			// tsmiOperation
			// 
			this.tsmiOperation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPlayPause,
            this.tsmiStop,
            this.toolStripSeparator5,
            this.tsmiPrevTrack,
            this.tsmiNextTrack,
            this.tsmiFastReverse,
            this.tsmiFastForward,
            this.toolStripSeparator7,
            this.tsmiNextContent,
            this.tsmiNextContentWithDelete,
            this.tsmiPrevContent});
			this.tsmiOperation.Name = "tsmiOperation";
			this.tsmiOperation.Text = "操作 (&O)";
			// 
			// tsmiPlayPause
			// 
			this.tsmiPlayPause.Name = "tsmiPlayPause";
			this.tsmiPlayPause.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.tsmiPlayPause.Text = "再生/一時停止 (&P)";
			this.tsmiPlayPause.Click += new System.EventHandler(this.tsmiPlayPause_Click);
			// 
			// tsmiStop
			// 
			this.tsmiStop.Name = "tsmiStop";
			this.tsmiStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.tsmiStop.Text = "停止 (&S)";
			this.tsmiStop.Click += new System.EventHandler(this.tsmiStop_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			// 
			// tsmiPrevTrack
			// 
			this.tsmiPrevTrack.Name = "tsmiPrevTrack";
			this.tsmiPrevTrack.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.tsmiPrevTrack.Text = "前へ (&B)";
			this.tsmiPrevTrack.Click += new System.EventHandler(this.tsmiPrevTrack_Click);
			// 
			// tsmiNextTrack
			// 
			this.tsmiNextTrack.Name = "tsmiNextTrack";
			this.tsmiNextTrack.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.tsmiNextTrack.Text = "次へ (&F)";
			this.tsmiNextTrack.Click += new System.EventHandler(this.tsmiNextTrack_Click);
			// 
			// tsmiFastReverse
			// 
			this.tsmiFastReverse.Name = "tsmiFastReverse";
			this.tsmiFastReverse.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.B)));
			this.tsmiFastReverse.Text = "巻き戻し (&R)";
			this.tsmiFastReverse.Click += new System.EventHandler(this.tsmiFastReverse_Click);
			// 
			// tsmiFastForward
			// 
			this.tsmiFastForward.Name = "tsmiFastForward";
			this.tsmiFastForward.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.F)));
			this.tsmiFastForward.Text = "早送り (&A)";
			this.tsmiFastForward.Click += new System.EventHandler(this.tsmiFastForward_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			// 
			// tsmiNextContent
			// 
			this.tsmiNextContent.Name = "tsmiNextContent";
			this.tsmiNextContent.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.N)));
			this.tsmiNextContent.Text = "次のコンテンツ (&N)";
			this.tsmiNextContent.Click += new System.EventHandler(this.tsmiNextContent_Click);
			// 
			// tsmiNextContentWithDelete
			// 
			this.tsmiNextContentWithDelete.Name = "tsmiNextContentWithDelete";
			this.tsmiNextContentWithDelete.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.D)));
			this.tsmiNextContentWithDelete.Text = "削除して次のコンテンツ (&D)";
			this.tsmiNextContentWithDelete.Click += new System.EventHandler(this.tsmiNextContentWithDelete_Click);
			// 
			// tsmiPrevContent
			// 
			this.tsmiPrevContent.Name = "tsmiPrevContent";
			this.tsmiPrevContent.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.P)));
			this.tsmiPrevContent.Text = "前のコンテンツ (E)";
			this.tsmiPrevContent.Click += new System.EventHandler(this.tsmiPrevContent_Click);
			// 
			// tsmiView
			// 
			this.tsmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAlwaysOnTop,
            this.tsmiFullscreen});
			this.tsmiView.Name = "tsmiView";
			this.tsmiView.Text = "表示 (&V)";
			// 
			// tsmiAlwaysOnTop
			// 
			this.tsmiAlwaysOnTop.CheckOnClick = true;
			this.tsmiAlwaysOnTop.Name = "tsmiAlwaysOnTop";
			this.tsmiAlwaysOnTop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.tsmiAlwaysOnTop.Text = "常に手前に表示 (&T)";
			this.tsmiAlwaysOnTop.Click += new System.EventHandler(this.tsmiAlwaysOnTop_Click);
			// 
			// tsmiFullscreen
			// 
			this.tsmiFullscreen.Name = "tsmiFullscreen";
			this.tsmiFullscreen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Return)));
			this.tsmiFullscreen.Text = "全画面 (&F)";
			this.tsmiFullscreen.Click += new System.EventHandler(this.tsmiFullscreen_Click);
			// 
			// tsmiTool
			// 
			this.tsmiTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAutoVolume,
            this.tsmiMediaKeys,
            this.tsmiRemovePlayedContent,
            this.tsmiAutoLoadPlaylist,
            this.tsmiWaitSecondsAfterLastCall,
            this.toolStripSeparator4,
            this.tsmiFocusOnWmp,
            this.toolStripSeparator1,
            this.tsmiUserCommands});
			this.tsmiTool.Name = "tsmiTool";
			this.tsmiTool.Text = "ツール (&T)";
			// 
			// tsmiAutoVolume
			// 
			this.tsmiAutoVolume.Checked = true;
			this.tsmiAutoVolume.CheckOnClick = true;
			this.tsmiAutoVolume.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiAutoVolume.Name = "tsmiAutoVolume";
			this.tsmiAutoVolume.Text = "自動音量調整 (&V)";
			// 
			// tsmiMediaKeys
			// 
			this.tsmiMediaKeys.Checked = true;
			this.tsmiMediaKeys.CheckOnClick = true;
			this.tsmiMediaKeys.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiMediaKeys.Name = "tsmiMediaKeys";
			this.tsmiMediaKeys.Text = "メディアキーを使う (&M)";
			// 
			// tsmiRemovePlayedContent
			// 
			this.tsmiRemovePlayedContent.Checked = true;
			this.tsmiRemovePlayedContent.CheckOnClick = true;
			this.tsmiRemovePlayedContent.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiRemovePlayedContent.Name = "tsmiRemovePlayedContent";
			this.tsmiRemovePlayedContent.Text = "再生の終了したコンテンツをリストから削除 (D)";
			// 
			// tsmiAutoLoadPlaylist
			// 
			this.tsmiAutoLoadPlaylist.Checked = true;
			this.tsmiAutoLoadPlaylist.CheckOnClick = true;
			this.tsmiAutoLoadPlaylist.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiAutoLoadPlaylist.Name = "tsmiAutoLoadPlaylist";
			this.tsmiAutoLoadPlaylist.Text = "前回のプレイリストを読み込む (&L)";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			// 
			// tsmiFocusOnWmp
			// 
			this.tsmiFocusOnWmp.Name = "tsmiFocusOnWmp";
			this.tsmiFocusOnWmp.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.tsmiFocusOnWmp.Text = "WMPにフォーカスを送る (&F)";
			this.tsmiFocusOnWmp.Click += new System.EventHandler(this.tsmiFocusOnWmp_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiUserCommands
			// 
			this.tsmiUserCommands.Name = "tsmiUserCommands";
			this.tsmiUserCommands.Text = "外部コマンド (&C)";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "asx";
			this.saveFileDialog1.Filter = "ASX files|*.asx|All files|*.*";
			this.saveFileDialog1.RestoreDirectory = true;
			// 
			// tsmiWaitSecondsAfterLastCall
			// 
			this.tsmiWaitSecondsAfterLastCall.Checked = true;
			this.tsmiWaitSecondsAfterLastCall.CheckOnClick = true;
			this.tsmiWaitSecondsAfterLastCall.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiWaitSecondsAfterLastCall.Name = "tsmiWaitSecondsAfterLastCall";
			this.tsmiWaitSecondsAfterLastCall.Text = "ラストコール後に3秒待つ (&W)";
			// 
			// PlayerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 601);
			this.Controls.Add(this.toolStripContainer1);
			this.KeyPreview = true;
			this.Name = "PlayerForm";
			this.Text = "PlayerForm";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlayerForm_KeyDown);
			this.tabControl1.ResumeLayout(false);
			this.tabPlayList.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.cmsPlaylist.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.tabPlayer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.wmpMain)).EndInit();
			this.tabBrowser.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPlayer;
		private System.Windows.Forms.TabPage tabBrowser;
		private AxWMPLib.AxWindowsMediaPlayer wmpMain;
		private System.Windows.Forms.WebBrowser ieMain;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;
		private System.Windows.Forms.ToolStripMenuItem tsmiView;
		private System.Windows.Forms.ToolStripMenuItem tsmiAlwaysOnTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiFullscreen;
		private System.Windows.Forms.ToolStripMenuItem tsmiTool;
		private System.Windows.Forms.ToolStripMenuItem tsmiAutoVolume;
		private System.Windows.Forms.ToolStripMenuItem tsmiReload;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiFocusOnWmp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiMediaKeys;
		private System.Windows.Forms.ToolStripMenuItem tsmiOperation;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayPause;
		private System.Windows.Forms.ToolStripMenuItem tsmiStop;
		private System.Windows.Forms.ToolStripMenuItem tsmiNextTrack;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrevTrack;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiFastReverse;
		private System.Windows.Forms.ToolStripMenuItem tsmiFastForward;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommands;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.TabPage tabPlayList;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private ContentDetailView contentDetailView1;
		private System.Windows.Forms.ListView lviewPlaylist;
		private System.Windows.Forms.ColumnHeader chId;
		private System.Windows.Forms.ColumnHeader chDisplayName;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.ContextMenuStrip cmsPlaylist;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiMove;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveUp;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveDown;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveBottom;
		private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlaylistCommands;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemovePlayedContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiAutoLoadPlaylist;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem tsmiNextContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrevContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiNextContentWithDelete;
		private System.Windows.Forms.ToolStripMenuItem tsmiExportAsAsx;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem tsmiWaitSecondsAfterLastCall;
	}
}