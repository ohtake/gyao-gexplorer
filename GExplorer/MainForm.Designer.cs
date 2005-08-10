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
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.genreTabControl1 = new Yusen.GExplorer.GenreTabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.genreListView1 = new Yusen.GExplorer.GenreListView();
			this.contentDetailView1 = new Yusen.GExplorer.ContentDetailView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseTop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiIgnoreCrawlErrors = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiGlobalSettingsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiUserCommandsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNgContentsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.genreTabControl1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
			this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip1.Size = new System.Drawing.Size(683, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 15);
			this.toolStripProgressBar1.Text = "toolStripProgressBar1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
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
			this.toolStripContainer1.Size = new System.Drawing.Size(683, 484);
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
			this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(683, 438);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// genreTabControl1
			// 
			this.genreTabControl1.Controls.Add(this.tabPage1);
			this.genreTabControl1.Controls.Add(this.tabPage2);
			this.genreTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.genreTabControl1.Location = new System.Drawing.Point(0, 0);
			this.genreTabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.genreTabControl1.Name = "genreTabControl1";
			this.genreTabControl1.SelectedGenre = null;
			this.genreTabControl1.SelectedIndex = 0;
			this.genreTabControl1.Size = new System.Drawing.Size(683, 20);
			this.genreTabControl1.TabIndex = 0;
			this.genreTabControl1.GenreSelected += new System.EventHandler<Yusen.GExplorer.GenreTabPageEventArgs>(this.genreTabControl1_GenreSelected);
			this.genreTabControl1.GenreDoubleClick += new System.EventHandler<Yusen.GExplorer.GenreTabPageEventArgs>(this.genreTabControl1_GenreDoubleClick);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(675, 0);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(675, 0);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 20);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.genreListView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.contentDetailView1);
			this.splitContainer1.Size = new System.Drawing.Size(683, 418);
			this.splitContainer1.SplitterDistance = 422;
			this.splitContainer1.TabIndex = 2;
			this.splitContainer1.Text = "splitContainer1";
			// 
			// genreListView1
			// 
			this.genreListView1.AboneType = Yusen.GExplorer.AboneType.Toumei;
			this.genreListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.genreListView1.Genre = null;
			this.genreListView1.Location = new System.Drawing.Point(0, 0);
			this.genreListView1.Margin = new System.Windows.Forms.Padding(0);
			this.genreListView1.Name = "genreListView1";
			this.genreListView1.NewColor = System.Drawing.Color.Red;
			this.genreListView1.Size = new System.Drawing.Size(422, 418);
			this.genreListView1.TabIndex = 1;
			this.genreListView1.SelectedContentChanged += new System.EventHandler<Yusen.GExplorer.SelectedContentChangedEventArgs>(this.genreListView1_SelectedContentChanged);
			this.genreListView1.GenreShowed += new System.EventHandler<Yusen.GExplorer.GenreListViewGenreShowedEventArgs>(this.genreListView1_GenreShowed);
			// 
			// contentDetailView1
			// 
			this.contentDetailView1.Content = null;
			this.contentDetailView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.contentDetailView1.Location = new System.Drawing.Point(0, 0);
			this.contentDetailView1.Name = "contentDetailView1";
			this.contentDetailView1.Size = new System.Drawing.Size(257, 418);
			this.contentDetailView1.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowItemReorder = true;
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiTools});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(683, 24);
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
			this.tsmiBrowseTop.Text = "トップページをIEで開く";
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
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiIgnoreCrawlErrors,
            this.toolStripSeparator2,
            this.tsmiGlobalSettingsEditor,
            this.toolStripSeparator3,
            this.tsmiUserCommandsEditor,
            this.tsmiNgContentsEditor});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Text = "ツール (&T)";
			// 
			// tsmiIgnoreCrawlErrors
			// 
			this.tsmiIgnoreCrawlErrors.CheckOnClick = true;
			this.tsmiIgnoreCrawlErrors.Name = "tsmiIgnoreCrawlErrors";
			this.tsmiIgnoreCrawlErrors.Text = "クロール時の無視可能エラーを全て無視 (&I)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiGlobalSettingsEditor
			// 
			this.tsmiGlobalSettingsEditor.Name = "tsmiGlobalSettingsEditor";
			this.tsmiGlobalSettingsEditor.Text = "グローバル設定エディタ (&G)";
			this.tsmiGlobalSettingsEditor.Click += new System.EventHandler(this.tsmiGlobalSettingsEditor_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
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
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(683, 484);
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
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private GenreTabControl genreTabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private GenreListView genreListView1;
		private ContentDetailView contentDetailView1;
		private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
		private System.Windows.Forms.ToolStripMenuItem tsmiTools;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommandsEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseTop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiNgContentsEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiGlobalSettingsEditor;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiIgnoreCrawlErrors;


	}
}