namespace Yusen.GExplorer {
	partial class BrowserForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpenTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPageProperty = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPageSetup = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiGenres = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTimeTables = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.gwbMain = new Yusen.GExplorer.GWebBrowser();
			this.tsStandard = new System.Windows.Forms.ToolStrip();
			this.tsbBack = new System.Windows.Forms.ToolStripButton();
			this.tsbForward = new System.Windows.Forms.ToolStripButton();
			this.tsbStop = new System.Windows.Forms.ToolStripButton();
			this.tsAddress = new System.Windows.Forms.ToolStrip();
			this.tslAddress = new System.Windows.Forms.ToolStripLabel();
			this.tscbAddress = new System.Windows.Forms.ToolStripComboBox();
			this.tsbGo = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tsStandard.SuspendLayout();
			this.tsAddress.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowItemReorder = true;
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiGenres,
            this.tsmiTimeTables});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(772, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenTop,
            this.tsmiSaveAs,
            this.tsmiPageProperty,
            this.toolStripSeparator3,
            this.tsmiPageSetup,
            this.tsmiPrint,
            this.tsmiPrintPreview,
            this.toolStripSeparator4,
            this.tsmiClose});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Text = "ファイル(&F)";
			// 
			// tsmiOpenTop
			// 
			this.tsmiOpenTop.Name = "tsmiOpenTop";
			this.tsmiOpenTop.Text = "トップページを開く(T)";
			this.tsmiOpenTop.Click += new System.EventHandler(this.tsmiOpenTop_Click);
			// 
			// tsmiSaveAs
			// 
			this.tsmiSaveAs.Name = "tsmiSaveAs";
			this.tsmiSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.S)));
			this.tsmiSaveAs.Text = "名前をつけて保存(&A)...";
			this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
			// 
			// tsmiPageProperty
			// 
			this.tsmiPageProperty.Name = "tsmiPageProperty";
			this.tsmiPageProperty.Text = "ページのプロパティ(&R)...";
			this.tsmiPageProperty.Click += new System.EventHandler(this.tsmiPageProperty_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			// 
			// tsmiPageSetup
			// 
			this.tsmiPageSetup.Name = "tsmiPageSetup";
			this.tsmiPageSetup.Text = "ページ設定(&U)...";
			this.tsmiPageSetup.Click += new System.EventHandler(this.tsmiPageSetup_Click);
			// 
			// tsmiPrint
			// 
			this.tsmiPrint.Name = "tsmiPrint";
			this.tsmiPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.tsmiPrint.Text = "印刷(&P)...";
			this.tsmiPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
			// 
			// tsmiPrintPreview
			// 
			this.tsmiPrintPreview.Name = "tsmiPrintPreview";
			this.tsmiPrintPreview.Text = "印刷プレビュー(&V)...";
			this.tsmiPrintPreview.Click += new System.EventHandler(this.tsmiPrintPreview_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			// 
			// tsmiClose
			// 
			this.tsmiClose.Name = "tsmiClose";
			this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.tsmiClose.Text = "閉じる(&W)";
			this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
			// 
			// tsmiGenres
			// 
			this.tsmiGenres.Name = "tsmiGenres";
			this.tsmiGenres.Text = "ジャンルトップ(&T)";
			// 
			// tsmiTimeTables
			// 
			this.tsmiTimeTables.Name = "tsmiTimeTables";
			this.tsmiTimeTables.Text = "番組表(&I)";
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
			this.toolStripContainer1.ContentPanel.Controls.Add(this.gwbMain);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(772, 516);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsStandard);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsAddress);
			// 
			// statusStrip1
			// 
			this.statusStrip1.AllowItemReorder = true;
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
			this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip1.Size = new System.Drawing.Size(772, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
			this.toolStripProgressBar1.Text = "toolStripProgressBar1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// gwbMain
			// 
			this.gwbMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gwbMain.Location = new System.Drawing.Point(0, 0);
			this.gwbMain.Name = "gwbMain";
			this.gwbMain.Size = new System.Drawing.Size(772, 445);
			this.gwbMain.StatusTextChanged += new System.EventHandler(this.gwbMain_StatusTextChanged);
			this.gwbMain.CanGoForwardChanged += new System.EventHandler(this.gwbMain_CanGoForwardChanged);
			this.gwbMain.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.gwbMain_ProgressChanged);
			this.gwbMain.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.gwbMain_Navigating);
			this.gwbMain.CanGoBackChanged += new System.EventHandler(this.gwbMain_CanGoBackChanged);
			this.gwbMain.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.gwbMain_DocumentCompleted);
			this.gwbMain.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.gwbMain_Navigated);
			this.gwbMain.DocumentTitleChanged += new System.EventHandler(this.gwbMain_DocumentTitleChanged);
			// 
			// tsStandard
			// 
			this.tsStandard.AllowItemReorder = true;
			this.tsStandard.Dock = System.Windows.Forms.DockStyle.None;
			this.tsStandard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBack,
            this.tsbForward,
            this.tsbStop});
			this.tsStandard.Location = new System.Drawing.Point(0, 24);
			this.tsStandard.Name = "tsStandard";
			this.tsStandard.Size = new System.Drawing.Size(147, 25);
			this.tsStandard.TabIndex = 3;
			this.tsStandard.Text = "標準バー";
			// 
			// tsbBack
			// 
			this.tsbBack.Enabled = false;
			this.tsbBack.Image = ((System.Drawing.Image)(resources.GetObject("tsbBack.Image")));
			this.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbBack.Name = "tsbBack";
			this.tsbBack.Text = "戻る";
			this.tsbBack.Click += new System.EventHandler(this.tsbBack_Click);
			// 
			// tsbForward
			// 
			this.tsbForward.Enabled = false;
			this.tsbForward.Image = ((System.Drawing.Image)(resources.GetObject("tsbForward.Image")));
			this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbForward.Name = "tsbForward";
			this.tsbForward.Text = "進む";
			this.tsbForward.Click += new System.EventHandler(this.tsbForward_Click);
			// 
			// tsbStop
			// 
			this.tsbStop.Enabled = false;
			this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
			this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStop.Name = "tsbStop";
			this.tsbStop.Text = "中止";
			this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
			// 
			// tsAddress
			// 
			this.tsAddress.AllowItemReorder = true;
			this.tsAddress.Dock = System.Windows.Forms.DockStyle.None;
			this.tsAddress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslAddress,
            this.tscbAddress,
            this.tsbGo});
			this.tsAddress.Location = new System.Drawing.Point(147, 24);
			this.tsAddress.Name = "tsAddress";
			this.tsAddress.Size = new System.Drawing.Size(581, 25);
			this.tsAddress.TabIndex = 1;
			this.tsAddress.Text = "アドレスバー";
			// 
			// tslAddress
			// 
			this.tslAddress.Name = "tslAddress";
			this.tslAddress.Text = "アドレス(&D)";
			// 
			// tscbAddress
			// 
			this.tscbAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.tscbAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
			this.tscbAddress.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.tscbAddress.Name = "tscbAddress";
			this.tscbAddress.Size = new System.Drawing.Size(450, 25);
			this.tscbAddress.Text = "tscbAddress";
			this.tscbAddress.SelectedIndexChanged += new System.EventHandler(this.GoToAddressBarUri);
			this.tscbAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tscbAddress_KeyDown);
			// 
			// tsbGo
			// 
			this.tsbGo.Image = ((System.Drawing.Image)(resources.GetObject("tsbGo.Image")));
			this.tsbGo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbGo.Name = "tsbGo";
			this.tsbGo.Text = "移動(&G)";
			this.tsbGo.Click += new System.EventHandler(this.GoToAddressBarUri);
			// 
			// BrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(772, 516);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "BrowserForm";
			this.Text = "BrowserForm";
			this.Load += new System.EventHandler(this.BrowserForm_Load);
			this.menuStrip1.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.tsStandard.ResumeLayout(false);
			this.tsAddress.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip tsAddress;
		private System.Windows.Forms.ToolStripLabel tslAddress;
		private System.Windows.Forms.ToolStrip tsStandard;
		private System.Windows.Forms.ToolStripButton tsbBack;
		private System.Windows.Forms.ToolStripButton tsbForward;
		private System.Windows.Forms.ToolStripButton tsbGo;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;
		private System.Windows.Forms.ToolStripMenuItem tsmiGenres;
		private System.Windows.Forms.ToolStripButton tsbStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiPageProperty;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
		private System.Windows.Forms.ToolStripMenuItem tsmiPageSetup;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrintPreview;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripComboBox tscbAddress;
		private System.Windows.Forms.ToolStripMenuItem tsmiTimeTables;
		private GWebBrowser gwbMain;
	}
}