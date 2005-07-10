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
			this.statstripMain = new System.Windows.Forms.StatusStrip();
			this.tspbPackages = new System.Windows.Forms.ToolStripProgressBar();
			this.tsslCategoryStat = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.glvMain = new Yusen.GExplorer.GenreListView();
			this.tabGenre = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiGyaoTop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUneploeableGenres = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiListView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAboneType = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiLvView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFullRowSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMultiSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlayer = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowser = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiUserSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiContentProperty = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiEditCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNgPackageEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.statstripMain.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabGenre.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statstripMain
			// 
			this.statstripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbPackages,
            this.tsslCategoryStat});
			this.statstripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
			this.statstripMain.Location = new System.Drawing.Point(0, 393);
			this.statstripMain.Name = "statstripMain";
			this.statstripMain.Size = new System.Drawing.Size(592, 23);
			this.statstripMain.TabIndex = 4;
			// 
			// tspbPackages
			// 
			this.tspbPackages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.tspbPackages.Name = "tspbPackages";
			this.tspbPackages.Size = new System.Drawing.Size(100, 16);
			this.tspbPackages.Text = "toolStripProgressBar1";
			// 
			// tsslCategoryStat
			// 
			this.tsslCategoryStat.Name = "tsslCategoryStat";
			this.tsslCategoryStat.Text = "tsslListViewItemsCount";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(592, 393);
			this.toolStripContainer1.TabIndex = 5;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.glvMain, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tabGenre, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(592, 369);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// glvMain
			// 
			this.glvMain.AboneType = Yusen.GExplorer.AboneType.Toumei;
			this.glvMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glvMain.Genre = null;
			this.glvMain.Location = new System.Drawing.Point(0, 20);
			this.glvMain.Margin = new System.Windows.Forms.Padding(0);
			this.glvMain.Name = "glvMain";
			this.glvMain.Size = new System.Drawing.Size(592, 349);
			this.glvMain.TabIndex = 0;
			// 
			// tabGenre
			// 
			this.tabGenre.Controls.Add(this.tabPage1);
			this.tabGenre.Controls.Add(this.tabPage2);
			this.tabGenre.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabGenre.Location = new System.Drawing.Point(0, 0);
			this.tabGenre.Margin = new System.Windows.Forms.Padding(0);
			this.tabGenre.Name = "tabGenre";
			this.tabGenre.SelectedIndex = 0;
			this.tabGenre.Size = new System.Drawing.Size(592, 20);
			this.tabGenre.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(584, 0);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(584, 0);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiUneploeableGenres,
            this.tsmiListView,
            this.tsmiWindow});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(592, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGyaoTop,
            this.toolStripSeparator1,
            this.tsmiQuit});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Text = "ファイル (&F)";
			// 
			// tsmiGyaoTop
			// 
			this.tsmiGyaoTop.Name = "tsmiGyaoTop";
			this.tsmiGyaoTop.Text = "GyaO のトップページをブラウザで開く (&T)";
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
			// 
			// tsmiUneploeableGenres
			// 
			this.tsmiUneploeableGenres.Name = "tsmiUneploeableGenres";
			this.tsmiUneploeableGenres.Text = "未対応ジャンル (&U)";
			// 
			// tsmiListView
			// 
			this.tsmiListView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAboneType,
            this.tsmiLvView,
            this.tsmiFullRowSelect,
            this.tsmiMultiSelect});
			this.tsmiListView.Name = "tsmiListView";
			this.tsmiListView.Text = "リストビュー (&L)";
			// 
			// tsmiAboneType
			// 
			this.tsmiAboneType.Name = "tsmiAboneType";
			this.tsmiAboneType.Text = "あぼ～ん方法 (&A)";
			// 
			// tsmiLvView
			// 
			this.tsmiLvView.Name = "tsmiLvView";
			this.tsmiLvView.Text = "表示形式 (&V)";
			// 
			// tsmiFullRowSelect
			// 
			this.tsmiFullRowSelect.Checked = true;
			this.tsmiFullRowSelect.CheckOnClick = true;
			this.tsmiFullRowSelect.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiFullRowSelect.Name = "tsmiFullRowSelect";
			this.tsmiFullRowSelect.Text = "行全体で選択 (&F)";
			// 
			// tsmiMultiSelect
			// 
			this.tsmiMultiSelect.CheckOnClick = true;
			this.tsmiMultiSelect.Name = "tsmiMultiSelect";
			this.tsmiMultiSelect.Text = "複数項目の選択可能 (&M)";
			// 
			// tsmiWindow
			// 
			this.tsmiWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPlayer,
            this.tsmiBrowser,
            this.toolStripSeparator2,
            this.tsmiUserSettings,
            this.tsmiContentProperty,
            this.tsmiEditCommands,
            this.tsmiNgPackageEditor});
			this.tsmiWindow.Name = "tsmiWindow";
			this.tsmiWindow.Text = "ウィンドウ (&W)";
			// 
			// tsmiPlayer
			// 
			this.tsmiPlayer.Name = "tsmiPlayer";
			this.tsmiPlayer.Text = "プレーヤ (&P)";
			// 
			// tsmiBrowser
			// 
			this.tsmiBrowser.Name = "tsmiBrowser";
			this.tsmiBrowser.Text = "ブラウザ (&B)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiUserSettings
			// 
			this.tsmiUserSettings.Name = "tsmiUserSettings";
			this.tsmiUserSettings.Text = "ユーザ設定ツールボックス (&U)";
			// 
			// tsmiContentProperty
			// 
			this.tsmiContentProperty.Name = "tsmiContentProperty";
			this.tsmiContentProperty.Text = "コンテンツプロパティビューア (&P)";
			// 
			// tsmiEditCommands
			// 
			this.tsmiEditCommands.Name = "tsmiEditCommands";
			this.tsmiEditCommands.Text = "外部コマンドエディタ (&C)";
			// 
			// tsmiNgPackageEditor
			// 
			this.tsmiNgPackageEditor.Name = "tsmiNgPackageEditor";
			this.tsmiNgPackageEditor.Text = "NGパッケージエディタ (&N)";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 416);
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.statstripMain);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.statstripMain.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabGenre.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statstripMain;
		private System.Windows.Forms.ToolStripStatusLabel tsslCategoryStat;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
		private System.Windows.Forms.ToolStripProgressBar tspbPackages;
		private System.Windows.Forms.ToolStripMenuItem tsmiWindow;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserSettings;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditCommands;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentProperty;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TabControl tabGenre;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.ToolStripMenuItem tsmiListView;
		private System.Windows.Forms.ToolStripMenuItem tsmiLvView;
		private System.Windows.Forms.ToolStripMenuItem tsmiFullRowSelect;
		private System.Windows.Forms.ToolStripMenuItem tsmiMultiSelect;
		private System.Windows.Forms.ToolStripMenuItem tsmiGyaoTop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private GenreListView glvMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiNgPackageEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiAboneType;
		private System.Windows.Forms.ToolStripMenuItem tsmiUneploeableGenres;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayer;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowser;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}

