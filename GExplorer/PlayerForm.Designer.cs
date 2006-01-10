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
			this.wmpMain = new AxWMPLib.AxWindowsMediaPlayer();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsslId = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslChapter = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslSize = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslDuration = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslTitle = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.wbBanner = new System.Windows.Forms.WebBrowser();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlayChapter = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReload = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRemoveAndClose = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOperations = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlayPause = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiStop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPrevTrack = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNextTrack = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFastReverse = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFastForward = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPrevContent = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNextContent = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNextContentWithDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiHideUiOnDeactivated = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiStrechToFit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDisableScreenSaver = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiAutoSizeOnNormal = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAutoSizeOnCf = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiResizeZoomValue = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSkipCmLicense = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiChapterModeFromBegining = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemovePlayedContent = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiAutoVolume = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiVolumeNormal = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiVolumeCf = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFocusOnWmp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiResizeToVideoResolution = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiShowItemInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiBrowseDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseRecommended = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiUserCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.inputBoxDialog1 = new Yusen.GExplorer.InputBoxDialog();
			this.timerAutoVolume = new System.Windows.Forms.Timer(this.components);
			this.timerSkipCmLisence = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.wmpMain)).BeginInit();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// wmpMain
			// 
			this.wmpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wmpMain.Enabled = true;
			this.wmpMain.Location = new System.Drawing.Point(0, 0);
			this.wmpMain.Name = "wmpMain";
			this.wmpMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpMain.OcxState")));
			this.wmpMain.Size = new System.Drawing.Size(644, 548);
			this.wmpMain.TabIndex = 0;
			this.wmpMain.Text = "axWindowsMediaPlayer1";
			this.wmpMain.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.wmpMain_PlayStateChange);
			this.wmpMain.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(this.wmpMain_OpenStateChange);
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
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(644, 548);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(644, 585);
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
            this.tsslId,
            this.tsslChapter,
            this.tsslSize,
            this.tsslDuration,
            this.tsslTitle});
			this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip1.Size = new System.Drawing.Size(644, 17);
			this.statusStrip1.TabIndex = 0;
			// 
			// tsslId
			// 
			this.tsslId.Name = "tsslId";
			this.tsslId.Size = new System.Drawing.Size(33, 12);
			this.tsslId.Text = "tsslId";
			// 
			// tsslChapter
			// 
			this.tsslChapter.Name = "tsslChapter";
			this.tsslChapter.Size = new System.Drawing.Size(64, 12);
			this.tsslChapter.Text = "tsslChapter";
			// 
			// tsslSize
			// 
			this.tsslSize.Name = "tsslSize";
			this.tsslSize.Size = new System.Drawing.Size(45, 12);
			this.tsslSize.Text = "tsslSize";
			// 
			// tsslDuration
			// 
			this.tsslDuration.Name = "tsslDuration";
			this.tsslDuration.Size = new System.Drawing.Size(67, 12);
			this.tsslDuration.Text = "tsslDuration";
			// 
			// tsslTitle
			// 
			this.tsslTitle.Name = "tsslTitle";
			this.tsslTitle.Size = new System.Drawing.Size(47, 12);
			this.tsslTitle.Text = "tsslTitle";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.wmpMain);
			this.splitContainer1.Panel1MinSize = 0;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Panel2Collapsed = true;
			this.splitContainer1.Panel2MinSize = 120;
			this.splitContainer1.Size = new System.Drawing.Size(644, 548);
			this.splitContainer1.SplitterDistance = 523;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.wbBanner, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 600F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(96, 100);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// wbBanner
			// 
			this.wbBanner.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wbBanner.Location = new System.Drawing.Point(0, 0);
			this.wbBanner.Margin = new System.Windows.Forms.Padding(0);
			this.wbBanner.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbBanner.Name = "wbBanner";
			this.wbBanner.ScrollBarsEnabled = false;
			this.wbBanner.Size = new System.Drawing.Size(96, 600);
			this.wbBanner.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowItemReorder = true;
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiOperations,
            this.tsmiSettings,
            this.tsmiTools});
			this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(644, 20);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPlayChapter,
            this.tsmiReload,
            this.toolStripSeparator2,
            this.tsmiRemoveAndClose,
            this.tsmiClose});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Size = new System.Drawing.Size(66, 16);
			this.tsmiFile.Text = "ファイル(&F)";
			// 
			// tsmiPlayChapter
			// 
			this.tsmiPlayChapter.Name = "tsmiPlayChapter";
			this.tsmiPlayChapter.Size = new System.Drawing.Size(263, 22);
			this.tsmiPlayChapter.Text = "特定のチャプターから再生(&C)...";
			this.tsmiPlayChapter.Click += new System.EventHandler(this.tsmiPlayChapter_Click);
			// 
			// tsmiReload
			// 
			this.tsmiReload.Name = "tsmiReload";
			this.tsmiReload.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.tsmiReload.Size = new System.Drawing.Size(263, 22);
			this.tsmiReload.Text = "動画の再読み込み(&R)";
			this.tsmiReload.Click += new System.EventHandler(this.tsmiReload_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(260, 6);
			// 
			// tsmiRemoveAndClose
			// 
			this.tsmiRemoveAndClose.Name = "tsmiRemoveAndClose";
			this.tsmiRemoveAndClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.tsmiRemoveAndClose.Size = new System.Drawing.Size(263, 22);
			this.tsmiRemoveAndClose.Text = "プレイリストから削除して閉じる(&E)";
			this.tsmiRemoveAndClose.Click += new System.EventHandler(this.tsmiRemoveAndClose_Click);
			// 
			// tsmiClose
			// 
			this.tsmiClose.Name = "tsmiClose";
			this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.tsmiClose.Size = new System.Drawing.Size(263, 22);
			this.tsmiClose.Text = "閉じる(&W)";
			this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
			// 
			// tsmiOperations
			// 
			this.tsmiOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPlayPause,
            this.tsmiStop,
            this.toolStripSeparator5,
            this.tsmiPrevTrack,
            this.tsmiNextTrack,
            this.tsmiFastReverse,
            this.tsmiFastForward,
            this.toolStripSeparator7,
            this.tsmiPrevContent,
            this.tsmiNextContent,
            this.tsmiNextContentWithDelete});
			this.tsmiOperations.Name = "tsmiOperations";
			this.tsmiOperations.Size = new System.Drawing.Size(57, 16);
			this.tsmiOperations.Text = "操作(&O)";
			// 
			// tsmiPlayPause
			// 
			this.tsmiPlayPause.Name = "tsmiPlayPause";
			this.tsmiPlayPause.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.tsmiPlayPause.Size = new System.Drawing.Size(281, 22);
			this.tsmiPlayPause.Text = "再生/一時停止(&P)";
			this.tsmiPlayPause.Click += new System.EventHandler(this.tsmiPlayPause_Click);
			// 
			// tsmiStop
			// 
			this.tsmiStop.Name = "tsmiStop";
			this.tsmiStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.tsmiStop.Size = new System.Drawing.Size(281, 22);
			this.tsmiStop.Text = "停止(&S)";
			this.tsmiStop.Click += new System.EventHandler(this.tsmiStop_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(278, 6);
			// 
			// tsmiPrevTrack
			// 
			this.tsmiPrevTrack.Name = "tsmiPrevTrack";
			this.tsmiPrevTrack.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.tsmiPrevTrack.Size = new System.Drawing.Size(281, 22);
			this.tsmiPrevTrack.Text = "前へ(&B)";
			this.tsmiPrevTrack.Click += new System.EventHandler(this.tsmiPrevTrack_Click);
			// 
			// tsmiNextTrack
			// 
			this.tsmiNextTrack.Name = "tsmiNextTrack";
			this.tsmiNextTrack.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.tsmiNextTrack.Size = new System.Drawing.Size(281, 22);
			this.tsmiNextTrack.Text = "次へ(&F)";
			this.tsmiNextTrack.Click += new System.EventHandler(this.tsmiNextTrack_Click);
			// 
			// tsmiFastReverse
			// 
			this.tsmiFastReverse.Name = "tsmiFastReverse";
			this.tsmiFastReverse.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.B)));
			this.tsmiFastReverse.Size = new System.Drawing.Size(281, 22);
			this.tsmiFastReverse.Text = "巻き戻し(&R)";
			this.tsmiFastReverse.Click += new System.EventHandler(this.tsmiFastReverse_Click);
			// 
			// tsmiFastForward
			// 
			this.tsmiFastForward.Name = "tsmiFastForward";
			this.tsmiFastForward.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
			this.tsmiFastForward.Size = new System.Drawing.Size(281, 22);
			this.tsmiFastForward.Text = "早送り(&A)";
			this.tsmiFastForward.Click += new System.EventHandler(this.tsmiFastForward_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(278, 6);
			// 
			// tsmiPrevContent
			// 
			this.tsmiPrevContent.Name = "tsmiPrevContent";
			this.tsmiPrevContent.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
			this.tsmiPrevContent.Size = new System.Drawing.Size(281, 22);
			this.tsmiPrevContent.Text = "プレイリストで前のコンテンツ(E)";
			this.tsmiPrevContent.Click += new System.EventHandler(this.tsmiPrevContent_Click);
			// 
			// tsmiNextContent
			// 
			this.tsmiNextContent.Name = "tsmiNextContent";
			this.tsmiNextContent.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
			this.tsmiNextContent.Size = new System.Drawing.Size(281, 22);
			this.tsmiNextContent.Text = "プレイリストで次のコンテンツ(&N)";
			this.tsmiNextContent.Click += new System.EventHandler(this.tsmiNextContent_Click);
			// 
			// tsmiNextContentWithDelete
			// 
			this.tsmiNextContentWithDelete.Name = "tsmiNextContentWithDelete";
			this.tsmiNextContentWithDelete.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
			this.tsmiNextContentWithDelete.Size = new System.Drawing.Size(281, 22);
			this.tsmiNextContentWithDelete.Text = "プレイリストから削除して次(&D)";
			this.tsmiNextContentWithDelete.Click += new System.EventHandler(this.tsmiNextContentWithDelete_Click);
			// 
			// tsmiSettings
			// 
			this.tsmiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAlwaysOnTop,
            this.tsmiHideUiOnDeactivated,
            this.toolStripMenuItem1,
            this.tsmiStrechToFit,
            this.tsmiDisableScreenSaver,
            this.toolStripMenuItem2,
            this.tsmiAutoSizeOnNormal,
            this.tsmiAutoSizeOnCf,
            this.tsmiResizeZoomValue,
            this.toolStripSeparator6,
            this.tsmiSkipCmLicense,
            this.tsmiChapterModeFromBegining,
            this.tsmiRemovePlayedContent,
            this.toolStripSeparator4,
            this.tsmiAutoVolume,
            this.tsmiVolumeNormal,
            this.tsmiVolumeCf});
			this.tsmiSettings.Name = "tsmiSettings";
			this.tsmiSettings.Size = new System.Drawing.Size(56, 16);
			this.tsmiSettings.Text = "設定(&S)";
			// 
			// tsmiAlwaysOnTop
			// 
			this.tsmiAlwaysOnTop.CheckOnClick = true;
			this.tsmiAlwaysOnTop.Name = "tsmiAlwaysOnTop";
			this.tsmiAlwaysOnTop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.tsmiAlwaysOnTop.Size = new System.Drawing.Size(260, 22);
			this.tsmiAlwaysOnTop.Text = "常に手前に表示(&T)";
			this.tsmiAlwaysOnTop.Click += new System.EventHandler(this.tsmiAlwaysOnTop_Click);
			// 
			// tsmiHideUiOnDeactivated
			// 
			this.tsmiHideUiOnDeactivated.Checked = true;
			this.tsmiHideUiOnDeactivated.CheckOnClick = true;
			this.tsmiHideUiOnDeactivated.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiHideUiOnDeactivated.Name = "tsmiHideUiOnDeactivated";
			this.tsmiHideUiOnDeactivated.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
			this.tsmiHideUiOnDeactivated.Size = new System.Drawing.Size(260, 22);
			this.tsmiHideUiOnDeactivated.Text = "非アクティブ時にUIを隠す(&H)";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(257, 6);
			// 
			// tsmiStrechToFit
			// 
			this.tsmiStrechToFit.CheckOnClick = true;
			this.tsmiStrechToFit.Name = "tsmiStrechToFit";
			this.tsmiStrechToFit.Size = new System.Drawing.Size(260, 22);
			this.tsmiStrechToFit.Text = "ウィンドウサイズに合わせて拡大(&F)";
			this.tsmiStrechToFit.Click += new System.EventHandler(this.tsmiStrechToFit_Click);
			// 
			// tsmiDisableScreenSaver
			// 
			this.tsmiDisableScreenSaver.CheckOnClick = true;
			this.tsmiDisableScreenSaver.Name = "tsmiDisableScreenSaver";
			this.tsmiDisableScreenSaver.Size = new System.Drawing.Size(260, 22);
			this.tsmiDisableScreenSaver.Text = "アクティブ時にスクリーンセーバ抑止(&D)";
			this.tsmiDisableScreenSaver.Click += new System.EventHandler(this.tsmiDisableScreenSaver_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(257, 6);
			// 
			// tsmiAutoSizeOnNormal
			// 
			this.tsmiAutoSizeOnNormal.Checked = true;
			this.tsmiAutoSizeOnNormal.CheckOnClick = true;
			this.tsmiAutoSizeOnNormal.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiAutoSizeOnNormal.Name = "tsmiAutoSizeOnNormal";
			this.tsmiAutoSizeOnNormal.Size = new System.Drawing.Size(260, 22);
			this.tsmiAutoSizeOnNormal.Text = "本編開始時に動画解像度にリサイズ(&R)";
			// 
			// tsmiAutoSizeOnCf
			// 
			this.tsmiAutoSizeOnCf.Checked = true;
			this.tsmiAutoSizeOnCf.CheckOnClick = true;
			this.tsmiAutoSizeOnCf.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiAutoSizeOnCf.Name = "tsmiAutoSizeOnCf";
			this.tsmiAutoSizeOnCf.Size = new System.Drawing.Size(260, 22);
			this.tsmiAutoSizeOnCf.Text = "CF開始時に動画解像度にリサイズ(&E)";
			// 
			// tsmiResizeZoomValue
			// 
			this.tsmiResizeZoomValue.Name = "tsmiResizeZoomValue";
			this.tsmiResizeZoomValue.Size = new System.Drawing.Size(260, 22);
			this.tsmiResizeZoomValue.Text = "リサイズ時の倍率(&Z)...";
			this.tsmiResizeZoomValue.Click += new System.EventHandler(this.tsmiResizeZoomValue_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(257, 6);
			// 
			// tsmiSkipCmLicense
			// 
			this.tsmiSkipCmLicense.Checked = true;
			this.tsmiSkipCmLicense.CheckOnClick = true;
			this.tsmiSkipCmLicense.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiSkipCmLicense.Name = "tsmiSkipCmLicense";
			this.tsmiSkipCmLicense.Size = new System.Drawing.Size(260, 22);
			this.tsmiSkipCmLicense.Text = "cm_license の早期スキップ(&K)";
			// 
			// tsmiChapterModeFromBegining
			// 
			this.tsmiChapterModeFromBegining.CheckOnClick = true;
			this.tsmiChapterModeFromBegining.Name = "tsmiChapterModeFromBegining";
			this.tsmiChapterModeFromBegining.Size = new System.Drawing.Size(260, 22);
			this.tsmiChapterModeFromBegining.Text = "再生開始時からチャプターモード(&C)";
			// 
			// tsmiRemovePlayedContent
			// 
			this.tsmiRemovePlayedContent.CheckOnClick = true;
			this.tsmiRemovePlayedContent.Name = "tsmiRemovePlayedContent";
			this.tsmiRemovePlayedContent.Size = new System.Drawing.Size(260, 22);
			this.tsmiRemovePlayedContent.Text = "再生終了でプレイリストから削除(&P)";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(257, 6);
			// 
			// tsmiAutoVolume
			// 
			this.tsmiAutoVolume.Checked = true;
			this.tsmiAutoVolume.CheckOnClick = true;
			this.tsmiAutoVolume.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiAutoVolume.Name = "tsmiAutoVolume";
			this.tsmiAutoVolume.Size = new System.Drawing.Size(260, 22);
			this.tsmiAutoVolume.Text = "自動音量調整(&V)";
			this.tsmiAutoVolume.Click += new System.EventHandler(this.tsmiAutoVolume_Click);
			// 
			// tsmiVolumeNormal
			// 
			this.tsmiVolumeNormal.Name = "tsmiVolumeNormal";
			this.tsmiVolumeNormal.Size = new System.Drawing.Size(260, 22);
			this.tsmiVolumeNormal.Text = "自動音量調整における本編の音量(&O)...";
			this.tsmiVolumeNormal.Click += new System.EventHandler(this.tsmiVolumeNormal_Click);
			// 
			// tsmiVolumeCf
			// 
			this.tsmiVolumeCf.Name = "tsmiVolumeCf";
			this.tsmiVolumeCf.Size = new System.Drawing.Size(260, 22);
			this.tsmiVolumeCf.Text = "自動音量調整におけるCFの音量(&M)...";
			this.tsmiVolumeCf.Click += new System.EventHandler(this.tsmiVolumeCf_Click);
			// 
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFocusOnWmp,
            this.tsmiResizeToVideoResolution,
            this.tsmiShowItemInfo,
            this.toolStripMenuItem3,
            this.tsmiBrowseDetail,
            this.tsmiBrowseRecommended,
            this.toolStripSeparator1,
            this.tsmiUserCommands});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Size = new System.Drawing.Size(61, 16);
			this.tsmiTools.Text = "ツール(&T)";
			// 
			// tsmiFocusOnWmp
			// 
			this.tsmiFocusOnWmp.Name = "tsmiFocusOnWmp";
			this.tsmiFocusOnWmp.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.tsmiFocusOnWmp.Size = new System.Drawing.Size(241, 22);
			this.tsmiFocusOnWmp.Text = "WMP にフォーカスを送る(&S)";
			this.tsmiFocusOnWmp.Click += new System.EventHandler(this.tsmiFocusOnWmp_Click);
			// 
			// tsmiResizeToVideoResolution
			// 
			this.tsmiResizeToVideoResolution.Name = "tsmiResizeToVideoResolution";
			this.tsmiResizeToVideoResolution.Size = new System.Drawing.Size(241, 22);
			this.tsmiResizeToVideoResolution.Text = "動画の解像度に合わせてリサイズ(&F)";
			this.tsmiResizeToVideoResolution.Click += new System.EventHandler(this.tsmiResizeToVideoResolution_Click);
			// 
			// tsmiShowItemInfo
			// 
			this.tsmiShowItemInfo.Name = "tsmiShowItemInfo";
			this.tsmiShowItemInfo.Size = new System.Drawing.Size(241, 22);
			this.tsmiShowItemInfo.Text = "ItemInfo を表示(&I)...";
			this.tsmiShowItemInfo.Click += new System.EventHandler(this.tsmiShowItemInfo_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(238, 6);
			// 
			// tsmiBrowseDetail
			// 
			this.tsmiBrowseDetail.Name = "tsmiBrowseDetail";
			this.tsmiBrowseDetail.Size = new System.Drawing.Size(241, 22);
			this.tsmiBrowseDetail.Text = "詳細ページを表示(&D)";
			this.tsmiBrowseDetail.Click += new System.EventHandler(this.tsmiBrowseDetail_Click);
			// 
			// tsmiBrowseRecommended
			// 
			this.tsmiBrowseRecommended.Name = "tsmiBrowseRecommended";
			this.tsmiBrowseRecommended.Size = new System.Drawing.Size(241, 22);
			this.tsmiBrowseRecommended.Text = "おすすめページを表示(&R)";
			this.tsmiBrowseRecommended.Click += new System.EventHandler(this.tsmiBrowseRecommended_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(238, 6);
			// 
			// tsmiUserCommands
			// 
			this.tsmiUserCommands.Name = "tsmiUserCommands";
			this.tsmiUserCommands.Size = new System.Drawing.Size(241, 22);
			this.tsmiUserCommands.Text = "外部コマンド(&C)";
			// 
			// inputBoxDialog1
			// 
			this.inputBoxDialog1.Input = null;
			this.inputBoxDialog1.Message = null;
			this.inputBoxDialog1.Title = null;
			// 
			// timerAutoVolume
			// 
			this.timerAutoVolume.Tick += new System.EventHandler(this.timerAutoVolume_Tick);
			// 
			// timerSkipCmLisence
			// 
			this.timerSkipCmLisence.Tick += new System.EventHandler(this.timerSkipCmLisence_Tick);
			// 
			// PlayerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(644, 585);
			this.Controls.Add(this.toolStripContainer1);
			this.KeyPreview = true;
			this.Name = "PlayerForm";
			this.Text = "PlayerForm";
			this.Deactivate += new System.EventHandler(this.PlayerForm_Deactivate);
			this.Resize += new System.EventHandler(this.PlayerForm_Resize);
			this.Activated += new System.EventHandler(this.PlayerForm_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerForm_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlayerForm_KeyDown);
			this.Load += new System.EventHandler(this.PlayerForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.wmpMain)).EndInit();
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
			this.tableLayoutPanel1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private AxWMPLib.AxWindowsMediaPlayer wmpMain;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;
		private System.Windows.Forms.ToolStripMenuItem tsmiTools;
		private System.Windows.Forms.ToolStripMenuItem tsmiReload;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiFocusOnWmp;
		private System.Windows.Forms.ToolStripMenuItem tsmiOperations;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayPause;
		private System.Windows.Forms.ToolStripMenuItem tsmiStop;
		private System.Windows.Forms.ToolStripMenuItem tsmiNextTrack;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrevTrack;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiFastReverse;
		private System.Windows.Forms.ToolStripMenuItem tsmiFastForward;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommands;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem tsmiNextContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrevContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiNextContentWithDelete;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayChapter;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveAndClose;
		private InputBoxDialog inputBoxDialog1;
		private System.Windows.Forms.ToolStripMenuItem tsmiShowItemInfo;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
		private System.Windows.Forms.ToolStripMenuItem tsmiAlwaysOnTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiDisableScreenSaver;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemovePlayedContent;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem tsmiAutoVolume;
		private System.Windows.Forms.ToolStripMenuItem tsmiVolumeNormal;
		private System.Windows.Forms.ToolStripMenuItem tsmiVolumeCf;
		private System.Windows.Forms.ToolStripMenuItem tsmiSkipCmLicense;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiStrechToFit;
		private System.Windows.Forms.ToolStripMenuItem tsmiResizeToVideoResolution;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tsslId;
		private System.Windows.Forms.ToolStripStatusLabel tsslSize;
		private System.Windows.Forms.ToolStripStatusLabel tsslDuration;
		private System.Windows.Forms.ToolStripStatusLabel tsslChapter;
		private System.Windows.Forms.ToolStripStatusLabel tsslTitle;
		private System.Windows.Forms.ToolStripMenuItem tsmiChapterModeFromBegining;
		private System.Windows.Forms.Timer timerAutoVolume;
		private System.Windows.Forms.Timer timerSkipCmLisence;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiHideUiOnDeactivated;
		private System.Windows.Forms.ToolStripMenuItem tsmiAutoSizeOnNormal;
		private System.Windows.Forms.ToolStripMenuItem tsmiAutoSizeOnCf;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiResizeZoomValue;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.WebBrowser wbBanner;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseDetail;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseRecommended;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}