namespace Yusen.GExplorer.UserInterfaces {
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
			this.tsgmiGenreTop = new Yusen.GExplorer.UserInterfaces.ToolStripGenreMenuItem();
			this.tsgmiTimetableUpdated = new Yusen.GExplorer.UserInterfaces.ToolStripGenreMenuItem();
			this.tsmiTimetableDeadline = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiExportContentListToCrawlResultView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiExtractImages = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiTimetableExpandAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTimetableCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiGotoCampaign = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFillCampaignForm = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.tshmiHelp = new Yusen.GExplorer.UserInterfaces.ToolStripHelpMenuItem();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.gwbMain = new Yusen.GExplorer.UserInterfaces.GWebBrowser();
			this.tsStandard = new System.Windows.Forms.ToolStrip();
			this.tsbBack = new System.Windows.Forms.ToolStripButton();
			this.tsbForward = new System.Windows.Forms.ToolStripButton();
			this.tscbAddress = new System.Windows.Forms.ToolStripComboBox();
			this.tsbGo = new System.Windows.Forms.ToolStripButton();
			this.tsbStop = new System.Windows.Forms.ToolStripButton();
			this.tstbLivedoor = new System.Windows.Forms.ToolStripTextBox();
			this.tsbLivedoor = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tsStandard.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsgmiGenreTop,
            this.tsgmiTimetableUpdated,
            this.tsmiTimetableDeadline,
            this.tsmiTools,
            this.tshmiHelp});
			this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(842, 21);
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
			this.tsmiFile.Size = new System.Drawing.Size(67, 17);
			this.tsmiFile.Text = "ファイル(&F)";
			// 
			// tsmiOpenTop
			// 
			this.tsmiOpenTop.Image = global::Yusen.GExplorer.Properties.Resources.home;
			this.tsmiOpenTop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiOpenTop.Name = "tsmiOpenTop";
			this.tsmiOpenTop.Size = new System.Drawing.Size(254, 22);
			this.tsmiOpenTop.Text = "トップページを開く(T)";
			this.tsmiOpenTop.Click += new System.EventHandler(this.tsmiOpenTop_Click);
			// 
			// tsmiSaveAs
			// 
			this.tsmiSaveAs.Image = global::Yusen.GExplorer.Properties.Resources.Save;
			this.tsmiSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiSaveAs.Name = "tsmiSaveAs";
			this.tsmiSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
						| System.Windows.Forms.Keys.S)));
			this.tsmiSaveAs.Size = new System.Drawing.Size(254, 22);
			this.tsmiSaveAs.Text = "名前をつけて保存(&A)...";
			this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
			// 
			// tsmiPageProperty
			// 
			this.tsmiPageProperty.Image = global::Yusen.GExplorer.Properties.Resources.Properties;
			this.tsmiPageProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiPageProperty.Name = "tsmiPageProperty";
			this.tsmiPageProperty.Size = new System.Drawing.Size(254, 22);
			this.tsmiPageProperty.Text = "ページのプロパティ(&R)...";
			this.tsmiPageProperty.Click += new System.EventHandler(this.tsmiPageProperty_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(251, 6);
			// 
			// tsmiPageSetup
			// 
			this.tsmiPageSetup.Image = global::Yusen.GExplorer.Properties.Resources.PrintSetup;
			this.tsmiPageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiPageSetup.Name = "tsmiPageSetup";
			this.tsmiPageSetup.Size = new System.Drawing.Size(254, 22);
			this.tsmiPageSetup.Text = "ページ設定(&U)...";
			this.tsmiPageSetup.Click += new System.EventHandler(this.tsmiPageSetup_Click);
			// 
			// tsmiPrint
			// 
			this.tsmiPrint.Image = global::Yusen.GExplorer.Properties.Resources.Print;
			this.tsmiPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiPrint.Name = "tsmiPrint";
			this.tsmiPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.tsmiPrint.Size = new System.Drawing.Size(254, 22);
			this.tsmiPrint.Text = "印刷(&P)...";
			this.tsmiPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
			// 
			// tsmiPrintPreview
			// 
			this.tsmiPrintPreview.Image = global::Yusen.GExplorer.Properties.Resources.PrintPreview;
			this.tsmiPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiPrintPreview.Name = "tsmiPrintPreview";
			this.tsmiPrintPreview.Size = new System.Drawing.Size(254, 22);
			this.tsmiPrintPreview.Text = "印刷プレビュー(&V)...";
			this.tsmiPrintPreview.Click += new System.EventHandler(this.tsmiPrintPreview_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(251, 6);
			// 
			// tsmiClose
			// 
			this.tsmiClose.Name = "tsmiClose";
			this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.tsmiClose.Size = new System.Drawing.Size(254, 22);
			this.tsmiClose.Text = "閉じる(&W)";
			this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
			// 
			// tsgmiGenreTop
			// 
			this.tsgmiGenreTop.Name = "tsgmiGenreTop";
			this.tsgmiGenreTop.Size = new System.Drawing.Size(94, 17);
			this.tsgmiGenreTop.Text = "ジャンルトップ(&T)";
			this.tsgmiGenreTop.GenreSelected += new System.EventHandler(this.tsgmiGenreTop_GenreSelected);
			// 
			// tsgmiTimetableUpdated
			// 
			this.tsgmiTimetableUpdated.Name = "tsgmiTimetableUpdated";
			this.tsgmiTimetableUpdated.Size = new System.Drawing.Size(130, 17);
			this.tsgmiTimetableUpdated.Text = "更新日優先番組表(&U)";
			this.tsgmiTimetableUpdated.GenreSelected += new System.EventHandler(this.tsgmiTimetableUpdated_GenreSelected);
			// 
			// tsmiTimetableDeadline
			// 
			this.tsmiTimetableDeadline.Enabled = false;
			this.tsmiTimetableDeadline.Name = "tsmiTimetableDeadline";
			this.tsmiTimetableDeadline.Size = new System.Drawing.Size(138, 17);
			this.tsmiTimetableDeadline.Text = "残り日数優先番組表(&D)";
			// 
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExportContentListToCrawlResultView,
            this.tsmiExtractImages,
            this.toolStripMenuItem1,
            this.tsmiTimetableExpandAll,
            this.tsmiTimetableCollapseAll,
            this.toolStripMenuItem2,
            this.tsmiGotoCampaign,
            this.tsmiFillCampaignForm,
            this.toolStripMenuItem3,
            this.tsmiOptions});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Size = new System.Drawing.Size(62, 17);
			this.tsmiTools.Text = "ツール(&T)";
			// 
			// tsmiExportContentListToCrawlResultView
			// 
			this.tsmiExportContentListToCrawlResultView.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiExportContentListToCrawlResultView.Name = "tsmiExportContentListToCrawlResultView";
			this.tsmiExportContentListToCrawlResultView.Size = new System.Drawing.Size(333, 22);
			this.tsmiExportContentListToCrawlResultView.Text = "ページ内のコンテンツをクロール結果ビューに書き出す(&R)";
			this.tsmiExportContentListToCrawlResultView.Click += new System.EventHandler(this.tsmiExportContentListToCrawlResultView_Click);
			// 
			// tsmiExtractImages
			// 
			this.tsmiExtractImages.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiExtractImages.Name = "tsmiExtractImages";
			this.tsmiExtractImages.Size = new System.Drawing.Size(333, 22);
			this.tsmiExtractImages.Text = "ページ内の画像を抽出する(&I)";
			this.tsmiExtractImages.Click += new System.EventHandler(this.tsmiExtractImages_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(330, 6);
			// 
			// tsmiTimetableExpandAll
			// 
			this.tsmiTimetableExpandAll.Name = "tsmiTimetableExpandAll";
			this.tsmiTimetableExpandAll.Size = new System.Drawing.Size(333, 22);
			this.tsmiTimetableExpandAll.Text = "番組表の日程を全て展開する(&E)";
			this.tsmiTimetableExpandAll.Click += new System.EventHandler(this.tsmiTimetableExpandAll_Click);
			// 
			// tsmiTimetableCollapseAll
			// 
			this.tsmiTimetableCollapseAll.Name = "tsmiTimetableCollapseAll";
			this.tsmiTimetableCollapseAll.Size = new System.Drawing.Size(333, 22);
			this.tsmiTimetableCollapseAll.Text = "番組表の日程を全て折りたたむ(&C)";
			this.tsmiTimetableCollapseAll.Click += new System.EventHandler(this.tsmiTimetableCollapseAll_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(330, 6);
			// 
			// tsmiGotoCampaign
			// 
			this.tsmiGotoCampaign.Name = "tsmiGotoCampaign";
			this.tsmiGotoCampaign.Size = new System.Drawing.Size(333, 22);
			this.tsmiGotoCampaign.Text = "gotoCampaign関数をInvokeする(&G)";
			this.tsmiGotoCampaign.Click += new System.EventHandler(this.tsmiGotoCampaign_Click);
			// 
			// tsmiFillCampaignForm
			// 
			this.tsmiFillCampaignForm.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiFillCampaignForm.Name = "tsmiFillCampaignForm";
			this.tsmiFillCampaignForm.Size = new System.Drawing.Size(333, 22);
			this.tsmiFillCampaignForm.Text = "キャンペーン応募フォームにフィル(&F)";
			this.tsmiFillCampaignForm.Click += new System.EventHandler(this.tsmiFillCampaignForm_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(330, 6);
			// 
			// tsmiOptions
			// 
			this.tsmiOptions.Name = "tsmiOptions";
			this.tsmiOptions.Size = new System.Drawing.Size(333, 22);
			this.tsmiOptions.Text = "オプション(&O)";
			this.tsmiOptions.Click += new System.EventHandler(this.tsmiOptions_Click);
			// 
			// tshmiHelp
			// 
			this.tshmiHelp.Name = "tshmiHelp";
			this.tshmiHelp.Size = new System.Drawing.Size(63, 17);
			this.tshmiHelp.Text = "ヘルプ(&H)";
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
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.gwbMain);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(842, 528);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(842, 596);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsStandard);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
			this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip1.Size = new System.Drawing.Size(842, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 13);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// gwbMain
			// 
			this.gwbMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gwbMain.Location = new System.Drawing.Point(0, 0);
			this.gwbMain.MinimumSize = new System.Drawing.Size(20, 20);
			this.gwbMain.Name = "gwbMain";
			this.gwbMain.Size = new System.Drawing.Size(842, 528);
			this.gwbMain.TabIndex = 0;
			this.gwbMain.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.gwbMain_Navigated);
			this.gwbMain.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.gwbMain_ProgressChanged);
			this.gwbMain.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.gwbMain_Navigating);
			this.gwbMain.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.gwbMain_DocumentCompleted);
			// 
			// tsStandard
			// 
			this.tsStandard.Dock = System.Windows.Forms.DockStyle.None;
			this.tsStandard.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsStandard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBack,
            this.tsbForward,
            this.tscbAddress,
            this.tsbGo,
            this.tsbStop,
            this.tstbLivedoor,
            this.tsbLivedoor});
			this.tsStandard.Location = new System.Drawing.Point(3, 21);
			this.tsStandard.Name = "tsStandard";
			this.tsStandard.Size = new System.Drawing.Size(772, 25);
			this.tsStandard.TabIndex = 3;
			this.tsStandard.Text = "標準バー";
			// 
			// tsbBack
			// 
			this.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbBack.Enabled = false;
			this.tsbBack.Image = global::Yusen.GExplorer.Properties.Resources.NavBack;
			this.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbBack.Name = "tsbBack";
			this.tsbBack.Size = new System.Drawing.Size(23, 22);
			this.tsbBack.Text = "戻る";
			this.tsbBack.Click += new System.EventHandler(this.tsbBack_Click);
			// 
			// tsbForward
			// 
			this.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbForward.Enabled = false;
			this.tsbForward.Image = global::Yusen.GExplorer.Properties.Resources.NavForward;
			this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbForward.Name = "tsbForward";
			this.tsbForward.Size = new System.Drawing.Size(23, 22);
			this.tsbForward.Text = "進む";
			this.tsbForward.Click += new System.EventHandler(this.tsbForward_Click);
			// 
			// tscbAddress
			// 
			this.tscbAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.tscbAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
			this.tscbAddress.Name = "tscbAddress";
			this.tscbAddress.Size = new System.Drawing.Size(500, 25);
			this.tscbAddress.Text = "tscbAddress";
			this.tscbAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tscbAddress_KeyDown);
			this.tscbAddress.SelectedIndexChanged += new System.EventHandler(this.GoToAddressBarUri);
			// 
			// tsbGo
			// 
			this.tsbGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbGo.Image = global::Yusen.GExplorer.Properties.Resources.RefreshDocView;
			this.tsbGo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbGo.Name = "tsbGo";
			this.tsbGo.Size = new System.Drawing.Size(23, 22);
			this.tsbGo.Text = "移動";
			this.tsbGo.Click += new System.EventHandler(this.GoToAddressBarUri);
			// 
			// tsbStop
			// 
			this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbStop.Enabled = false;
			this.tsbStop.Image = global::Yusen.GExplorer.Properties.Resources.Refresh_Cancel;
			this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbStop.Name = "tsbStop";
			this.tsbStop.Size = new System.Drawing.Size(23, 22);
			this.tsbStop.Text = "中止";
			this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
			// 
			// tstbLivedoor
			// 
			this.tstbLivedoor.ForeColor = System.Drawing.SystemColors.GrayText;
			this.tstbLivedoor.Name = "tstbLivedoor";
			this.tstbLivedoor.Size = new System.Drawing.Size(150, 25);
			this.tstbLivedoor.Text = "livedoor動画でGyaO検索";
			this.tstbLivedoor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstbLivedoor_KeyDown);
			this.tstbLivedoor.Enter += new System.EventHandler(this.tstbLivedoor_Enter);
			this.tstbLivedoor.Leave += new System.EventHandler(this.tstbLivedoor_Leave);
			// 
			// tsbLivedoor
			// 
			this.tsbLivedoor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbLivedoor.Image = global::Yusen.GExplorer.Properties.Resources.searchWeb;
			this.tsbLivedoor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbLivedoor.Name = "tsbLivedoor";
			this.tsbLivedoor.Size = new System.Drawing.Size(23, 22);
			this.tsbLivedoor.Text = "livedoor動画でGyaO検索";
			this.tsbLivedoor.Click += new System.EventHandler(this.tsbLivedoor_Click);
			// 
			// BrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(842, 596);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "BrowserForm";
			this.Text = "BrowserForm";
			this.Load += new System.EventHandler(this.BrowserForm_Load);
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
			this.tsStandard.ResumeLayout(false);
			this.tsStandard.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip tsStandard;
		private System.Windows.Forms.ToolStripButton tsbBack;
		private System.Windows.Forms.ToolStripButton tsbForward;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;
		private System.Windows.Forms.ToolStripButton tsbStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiPageProperty;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
		private System.Windows.Forms.ToolStripMenuItem tsmiPageSetup;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrintPreview;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiTimetableDeadline;
		private System.Windows.Forms.ToolStripMenuItem tsmiTools;
		private System.Windows.Forms.ToolStripMenuItem tsmiGotoCampaign;
		private System.Windows.Forms.ToolStripMenuItem tsmiExtractImages;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFillCampaignForm;
		private Yusen.GExplorer.UserInterfaces.ToolStripHelpMenuItem tshmiHelp;
		private System.Windows.Forms.ToolStripMenuItem tsmiExportContentListToCrawlResultView;
		private System.Windows.Forms.ToolStripComboBox tscbAddress;
		private System.Windows.Forms.ToolStripButton tsbGo;
		private System.Windows.Forms.ToolStripTextBox tstbLivedoor;
		private System.Windows.Forms.ToolStripButton tsbLivedoor;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiTimetableExpandAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiTimetableCollapseAll;
		private GWebBrowser gwbMain;
		private ToolStripGenreMenuItem tsgmiGenreTop;
		private ToolStripGenreMenuItem tsgmiTimetableUpdated;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiOptions;
	}
}