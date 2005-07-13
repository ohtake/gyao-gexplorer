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
			this.components = new System.ComponentModel.Container();
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
			this.tsmiTest = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTestBody = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTestSize = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTestClick = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTestContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiTestVolumeEnable = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTestVolumeDisable = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiTestStrechToFit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiTestScrolBars = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiTestCampaign = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.wbMain = new System.Windows.Forms.WebBrowser();
			this.tsStandard = new System.Windows.Forms.ToolStrip();
			this.tsbBack = new System.Windows.Forms.ToolStripButton();
			this.tsbForward = new System.Windows.Forms.ToolStripButton();
			this.tsbStop = new System.Windows.Forms.ToolStripButton();
			this.tsAddress = new System.Windows.Forms.ToolStrip();
			this.tslAddress = new System.Windows.Forms.ToolStripLabel();
			this.tscbAddress = new System.Windows.Forms.ToolStripComboBox();
			this.tsbGo = new System.Windows.Forms.ToolStripButton();
			this.cmsContent = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiContentOpenDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiContentOpenPlayPage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentPlayGPlayer = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiContentPlayWmp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiContentPlayIe = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentProperty = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsPackage = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiPackageOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.tsStandard.SuspendLayout();
			this.tsAddress.SuspendLayout();
			this.cmsContent.SuspendLayout();
			this.cmsPackage.SuspendLayout();
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
            this.tsmiTest});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(777, 24);
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
			this.tsmiFile.Text = "ファイル (&F)";
			// 
			// tsmiOpenTop
			// 
			this.tsmiOpenTop.Name = "tsmiOpenTop";
			this.tsmiOpenTop.Text = "トップページを開く (T)";
			this.tsmiOpenTop.Click += new System.EventHandler(this.tsmiOpenTop_Click);
			// 
			// tsmiSaveAs
			// 
			this.tsmiSaveAs.Name = "tsmiSaveAs";
			this.tsmiSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control)
						| System.Windows.Forms.Keys.S)));
			this.tsmiSaveAs.Text = "名前をつけて保存 (&A)...";
			this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
			// 
			// tsmiPageProperty
			// 
			this.tsmiPageProperty.Name = "tsmiPageProperty";
			this.tsmiPageProperty.Text = "ページのプロパティ (&R)...";
			this.tsmiPageProperty.Click += new System.EventHandler(this.tsmiPageProperty_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			// 
			// tsmiPageSetup
			// 
			this.tsmiPageSetup.Name = "tsmiPageSetup";
			this.tsmiPageSetup.Text = "ページ設定 (&U)...";
			this.tsmiPageSetup.Click += new System.EventHandler(this.tsmiPageSetup_Click);
			// 
			// tsmiPrint
			// 
			this.tsmiPrint.Name = "tsmiPrint";
			this.tsmiPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.tsmiPrint.Text = "印刷 (&P)...";
			this.tsmiPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
			// 
			// tsmiPrintPreview
			// 
			this.tsmiPrintPreview.Name = "tsmiPrintPreview";
			this.tsmiPrintPreview.Text = "印刷プレビュー (&V)...";
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
			this.tsmiClose.Text = "閉じる (&W)";
			this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
			// 
			// tsmiGenres
			// 
			this.tsmiGenres.Name = "tsmiGenres";
			this.tsmiGenres.Text = "ジャンル (&E)";
			// 
			// tsmiTest
			// 
			this.tsmiTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTestBody,
            this.tsmiTestSize,
            this.tsmiTestClick,
            this.tsmiTestContextMenu,
            this.toolStripSeparator5,
            this.tsmiTestVolumeEnable,
            this.tsmiTestVolumeDisable,
            this.toolStripSeparator6,
            this.tsmiTestStrechToFit,
            this.toolStripSeparator7,
            this.tsmiTestScrolBars,
            this.toolStripSeparator9,
            this.tsmiTestCampaign});
			this.tsmiTest.Name = "tsmiTest";
			this.tsmiTest.Text = "再生ページでのテスト (&P)";
			// 
			// tsmiTestBody
			// 
			this.tsmiTestBody.Name = "tsmiTestBody";
			this.tsmiTestBody.Text = "BODY の onselectstart と oncontextmenu を書き換えてみる (&B)";
			this.tsmiTestBody.Click += new System.EventHandler(this.tsmiTestBody_Click);
			// 
			// tsmiTestSize
			// 
			this.tsmiTestSize.Name = "tsmiTestSize";
			this.tsmiTestSize.Text = "player のサイズを100%にし，自身と親要素のマージン等を0にしてから ScrollIntoView (&S)";
			this.tsmiTestSize.Click += new System.EventHandler(this.tsmiTestSize_Click);
			// 
			// tsmiTestClick
			// 
			this.tsmiTestClick.Name = "tsmiTestClick";
			this.tsmiTestClick.Text = "player のクリック禁止を無効にしてみる (&C)";
			this.tsmiTestClick.Click += new System.EventHandler(this.tsmiTestClick_Click);
			// 
			// tsmiTestContextMenu
			// 
			this.tsmiTestContextMenu.Name = "tsmiTestContextMenu";
			this.tsmiTestContextMenu.Text = "player のコンテキストメニューを有効にしてみる (&R)";
			this.tsmiTestContextMenu.Click += new System.EventHandler(this.tsmiTestContextMenu_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			// 
			// tsmiTestVolumeEnable
			// 
			this.tsmiTestVolumeEnable.Name = "tsmiTestVolumeEnable";
			this.tsmiTestVolumeEnable.Text = "音量の自動調整のイベントハンドラを登録してみる (&V)";
			this.tsmiTestVolumeEnable.Click += new System.EventHandler(this.tsmiTestVolume_Click);
			// 
			// tsmiTestVolumeDisable
			// 
			this.tsmiTestVolumeDisable.Name = "tsmiTestVolumeDisable";
			this.tsmiTestVolumeDisable.Text = "音量の自動調整のイベントハンドラを解除してみる (&O)";
			this.tsmiTestVolumeDisable.Click += new System.EventHandler(this.tsmiTestVolumeDisable_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			// 
			// tsmiTestStrechToFit
			// 
			this.tsmiTestStrechToFit.Name = "tsmiTestStrechToFit";
			this.tsmiTestStrechToFit.Text = "player の strechToFit をトグル (&F)";
			this.tsmiTestStrechToFit.Click += new System.EventHandler(this.tsmiTestStrechToFit_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			// 
			// tsmiTestScrolBars
			// 
			this.tsmiTestScrolBars.Name = "tsmiTestScrolBars";
			this.tsmiTestScrolBars.Text = "スクロールバーの表示非表示のトグル (&B)";
			this.tsmiTestScrolBars.Click += new System.EventHandler(this.tsmiTestScrolBars_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			// 
			// tsmiTestCampaign
			// 
			this.tsmiTestCampaign.Name = "tsmiTestCampaign";
			this.tsmiTestCampaign.Text = "gotoCampaign 関数を無理やり invoke (&G)";
			this.tsmiTestCampaign.Click += new System.EventHandler(this.tsmiTestCampaign_Click);
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
			this.toolStripContainer1.ContentPanel.Controls.Add(this.wbMain);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(777, 415);
			this.toolStripContainer1.TabIndex = 1;
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
			this.statusStrip1.Size = new System.Drawing.Size(777, 23);
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
			// wbMain
			// 
			this.wbMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wbMain.Location = new System.Drawing.Point(0, 0);
			this.wbMain.Name = "wbMain";
			this.wbMain.Size = new System.Drawing.Size(777, 343);
			this.wbMain.Url = new System.Uri("about:blank", System.UriKind.Absolute);
			this.wbMain.DocumentTitleChanged += new System.EventHandler(this.wbMain_DocumentTitleChanged);
			this.wbMain.CanGoForwardChanged += new System.EventHandler(this.wbMain_CanGoForwardChanged);
			this.wbMain.CanGoBackChanged += new System.EventHandler(this.wbMain_CanGoBackChanged);
			this.wbMain.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.wbMain_ProgressChanged);
			this.wbMain.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbMain_Navigating);
			this.wbMain.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbMain_DocumentCompleted);
			this.wbMain.StatusTextChanged += new System.EventHandler(this.wbMain_StatusTextChanged);
			this.wbMain.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wbMain_Navigated);
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
			this.tsAddress.Size = new System.Drawing.Size(589, 25);
			this.tsAddress.TabIndex = 1;
			this.tsAddress.Text = "アドレスバー";
			// 
			// tslAddress
			// 
			this.tslAddress.Name = "tslAddress";
			this.tslAddress.Text = "アドレス (&D)";
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
			this.tsbGo.Text = "移動 (&G)";
			this.tsbGo.Click += new System.EventHandler(this.GoToAddressBarUri);
			// 
			// cmsContent
			// 
			this.cmsContent.Enabled = true;
			this.cmsContent.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsContent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiContentOpenDetail,
            this.tsmiContentOpenPlayPage,
            this.toolStripSeparator1,
            this.tsmiContentPlayGPlayer,
            this.tsmiContentPlayWmp,
            this.tsmiContentPlayIe,
            this.toolStripSeparator2,
            this.tsmiContentCommands,
            this.toolStripSeparator8,
            this.tsmiContentProperty});
			this.cmsContent.Location = new System.Drawing.Point(25, 66);
			this.cmsContent.Name = "cmsContent";
			this.cmsContent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsContent.Size = new System.Drawing.Size(189, 176);
			// 
			// tsmiContentOpenDetail
			// 
			this.tsmiContentOpenDetail.Name = "tsmiContentOpenDetail";
			this.tsmiContentOpenDetail.Text = "詳細ページを開く (&D)";
			this.tsmiContentOpenDetail.Click += new System.EventHandler(this.tsmiContentOpenDetail_Click);
			// 
			// tsmiContentOpenPlayPage
			// 
			this.tsmiContentOpenPlayPage.Name = "tsmiContentOpenPlayPage";
			this.tsmiContentOpenPlayPage.Text = "再生ページを開く (&P)";
			this.tsmiContentOpenPlayPage.Click += new System.EventHandler(this.tsmiContentOpenPlayPage_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiContentPlayGPlayer
			// 
			this.tsmiContentPlayGPlayer.Name = "tsmiContentPlayGPlayer";
			this.tsmiContentPlayGPlayer.Text = "専用プレーヤで再生 (&G)";
			this.tsmiContentPlayGPlayer.Click += new System.EventHandler(this.tsmiContentPlayGPlayer_Click);
			// 
			// tsmiContentPlayWmp
			// 
			this.tsmiContentPlayWmp.Name = "tsmiContentPlayWmp";
			this.tsmiContentPlayWmp.Text = "WMPで再生 (&W)";
			this.tsmiContentPlayWmp.Click += new System.EventHandler(this.tsmiContentPlayWmp_Click);
			// 
			// tsmiContentPlayIe
			// 
			this.tsmiContentPlayIe.Name = "tsmiContentPlayIe";
			this.tsmiContentPlayIe.Text = "IEで再生 (&I)";
			this.tsmiContentPlayIe.Click += new System.EventHandler(this.tsmiContentPlayIe_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiContentCommands
			// 
			this.tsmiContentCommands.Name = "tsmiContentCommands";
			this.tsmiContentCommands.Text = "外部コマンド (&C)";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			// 
			// tsmiContentProperty
			// 
			this.tsmiContentProperty.Name = "tsmiContentProperty";
			this.tsmiContentProperty.Text = "コンテンツのプロパティ (&R)";
			this.tsmiContentProperty.Click += new System.EventHandler(this.tsmiContentProperty_Click);
			// 
			// cmsPackage
			// 
			this.cmsPackage.Enabled = true;
			this.cmsPackage.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsPackage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPackageOpen});
			this.cmsPackage.Location = new System.Drawing.Point(25, 66);
			this.cmsPackage.Name = "cmsPackage";
			this.cmsPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsPackage.Size = new System.Drawing.Size(194, 26);
			// 
			// tsmiPackageOpen
			// 
			this.tsmiPackageOpen.Name = "tsmiPackageOpen";
			this.tsmiPackageOpen.Text = "パッケージページを開く (&O)";
			this.tsmiPackageOpen.Click += new System.EventHandler(this.tsmiPackageOpen_Click);
			// 
			// BrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(777, 415);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "BrowserForm";
			this.Text = "BrowserForm";
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
			this.cmsContent.ResumeLayout(false);
			this.cmsPackage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip tsAddress;
		private System.Windows.Forms.ToolStripLabel tslAddress;
		private System.Windows.Forms.WebBrowser wbMain;
		private System.Windows.Forms.ToolStrip tsStandard;
		private System.Windows.Forms.ToolStripButton tsbBack;
		private System.Windows.Forms.ToolStripButton tsbForward;
		private System.Windows.Forms.ToolStripButton tsbGo;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ContextMenuStrip cmsPackage;
		private System.Windows.Forms.ContextMenuStrip cmsContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiPackageOpen;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentOpenDetail;
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;
		private System.Windows.Forms.ToolStripMenuItem tsmiGenres;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentPlayGPlayer;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentPlayWmp;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentPlayIe;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentOpenPlayPage;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentCommands;
		private System.Windows.Forms.ToolStripButton tsbStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiPageProperty;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
		private System.Windows.Forms.ToolStripMenuItem tsmiPageSetup;
		private System.Windows.Forms.ToolStripMenuItem tsmiPrintPreview;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiTest;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestBody;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestSize;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestClick;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestContextMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestVolumeEnable;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestVolumeDisable;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestStrechToFit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestScrolBars;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentProperty;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestCampaign;
		private System.Windows.Forms.ToolStripComboBox tscbAddress;
	}
}