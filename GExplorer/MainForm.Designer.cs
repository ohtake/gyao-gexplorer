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
			this.contentDetailView1 = new Yusen.GExplorer.ContentDetailView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSearchCache = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSearchLivedoorGyaO = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSerializeSettingsNow = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUserCommandsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNgFavContentsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.inputBoxDialog1 = new Yusen.GExplorer.UserInterfaces.InputBoxDialog();
			this.timerViewDetail = new System.Windows.Forms.Timer(this.components);
			this.timerClearStatusText = new System.Windows.Forms.Timer(this.components);
			this.statusStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.scListsAndDetail.Panel1.SuspendLayout();
			this.scListsAndDetail.Panel2.SuspendLayout();
			this.scListsAndDetail.SuspendLayout();
			this.scLists.Panel1.SuspendLayout();
			this.scLists.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
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
			this.scLists.Size = new System.Drawing.Size(600, 564);
			this.scLists.SplitterDistance = 372;
			this.scLists.TabIndex = 2;
			this.scLists.Text = "splitContainer2";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.crawlResultView1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 372);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// crawlResultView1
			// 
			this.crawlResultView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.crawlResultView1.Location = new System.Drawing.Point(0, 20);
			this.crawlResultView1.Margin = new System.Windows.Forms.Padding(0);
			this.crawlResultView1.Name = "crawlResultView1";
			this.crawlResultView1.Size = new System.Drawing.Size(600, 352);
			this.crawlResultView1.TabIndex = 1;
			this.crawlResultView1.ContentSelectionChanged += new System.EventHandler<Yusen.GExplorer.OldApp.ContentSelectionChangedEventArgs>(this.crawlResultView1_ContentSelectionChanged);
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
            this.tsmiTools,
            this.tsmiSettings});
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
            this.tsmiQuit});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Size = new System.Drawing.Size(66, 16);
			this.tsmiFile.Text = "ファイル(&F)";
			// 
			// tsmiQuit
			// 
			this.tsmiQuit.Name = "tsmiQuit";
			this.tsmiQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.tsmiQuit.Size = new System.Drawing.Size(148, 22);
			this.tsmiQuit.Text = "終了(&Q)";
			this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
			// 
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSearchCache,
            this.tsmiSearchLivedoorGyaO,
            this.toolStripMenuItem1,
            this.tsmiSerializeSettingsNow});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Size = new System.Drawing.Size(61, 16);
			this.tsmiTools.Text = "ツール(&T)";
			// 
			// tsmiSearchCache
			// 
			this.tsmiSearchCache.Image = global::Yusen.GExplorer.Properties.Resources.SearchInFolder;
			this.tsmiSearchCache.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiSearchCache.Name = "tsmiSearchCache";
			this.tsmiSearchCache.Size = new System.Drawing.Size(297, 22);
			this.tsmiSearchCache.Text = "[DEL] キャッシュから検索(&F)...";
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
			// tsmiSettings
			// 
			this.tsmiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUserCommandsEditor,
            this.tsmiNgFavContentsEditor});
			this.tsmiSettings.Name = "tsmiSettings";
			this.tsmiSettings.Size = new System.Drawing.Size(56, 16);
			this.tsmiSettings.Text = "設定(&S)";
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
			// timerClearStatusText
			// 
			this.timerClearStatusText.Interval = 10000;
			this.timerClearStatusText.Tick += new System.EventHandler(this.timerClearStatusText_Tick);
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
			this.scLists.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
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
		private CrawlResultView crawlResultView1;
		private ContentDetailView contentDetailView1;
		private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
		private System.Windows.Forms.ToolStripMenuItem tsmiTools;
		private System.Windows.Forms.SplitContainer scLists;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
		private Yusen.GExplorer.UserInterfaces.InputBoxDialog inputBoxDialog1;
		private System.Windows.Forms.Timer timerViewDetail;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommandsEditor;
		private System.Windows.Forms.Timer timerClearStatusText;
		private System.Windows.Forms.ToolStripMenuItem tsmiSerializeSettingsNow;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiNgFavContentsEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiSearchCache;
		private System.Windows.Forms.ToolStripMenuItem tsmiSearchLivedoorGyaO;
		private System.Windows.Forms.SplitContainer scListsAndDetail;


	}
}