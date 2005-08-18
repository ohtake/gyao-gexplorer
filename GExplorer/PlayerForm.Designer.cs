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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPlayer = new System.Windows.Forms.TabPage();
			this.wmpMain = new AxWMPLib.AxWindowsMediaPlayer();
			this.tabBrowser = new System.Windows.Forms.TabPage();
			this.ieMain = new System.Windows.Forms.WebBrowser();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReload = new System.Windows.Forms.ToolStripMenuItem();
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
			this.tsmiWaitSecondsAfterLastCall = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFocusOnWmp = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiUserCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1.SuspendLayout();
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
			this.tabControl1.Controls.Add(this.tabPlayer);
			this.tabControl1.Controls.Add(this.tabBrowser);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(657, 577);
			this.tabControl1.TabIndex = 0;
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
			// tsmiWaitSecondsAfterLastCall
			// 
			this.tsmiWaitSecondsAfterLastCall.Checked = true;
			this.tsmiWaitSecondsAfterLastCall.CheckOnClick = true;
			this.tsmiWaitSecondsAfterLastCall.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiWaitSecondsAfterLastCall.Name = "tsmiWaitSecondsAfterLastCall";
			this.tsmiWaitSecondsAfterLastCall.Text = "ラストコール後に3秒待つ (&W)";
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
			// PlayerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 601);
			this.Controls.Add(this.toolStripContainer1);
			this.KeyPreview = true;
			this.Name = "PlayerForm";
			this.Text = "PlayerForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerForm_FormClosing);
			this.Load += new System.EventHandler(this.PlayerForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlayerForm_KeyDown);
			this.tabControl1.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripMenuItem tsmiRemovePlayedContent;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem tsmiNextContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrevContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiNextContentWithDelete;
		private System.Windows.Forms.ToolStripMenuItem tsmiWaitSecondsAfterLastCall;
	}
}