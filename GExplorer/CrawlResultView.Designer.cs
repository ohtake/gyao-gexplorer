namespace Yusen.GExplorer {
	partial class CrawlResultView {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrawlResultView));
			this.listView1 = new System.Windows.Forms.ListView();
			this.chId = new System.Windows.Forms.ColumnHeader();
			this.chTitle = new System.Windows.Forms.ColumnHeader();
			this.chEpisode = new System.Windows.Forms.ColumnHeader();
			this.chSubTitle = new System.Windows.Forms.ColumnHeader();
			this.chDuration = new System.Windows.Forms.ColumnHeader();
			this.chDescription = new System.Windows.Forms.ColumnHeader();
			this.cmsContent = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPlayWithWmp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlayWithIe = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBroseDetailWithIe = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyDetailUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyNameAndDetailUri = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiAddNg = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAddNgWithId = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAddNgWithTitle = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiUserCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tslTitle = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbShowFilter = new System.Windows.Forms.ToolStripButton();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiAboneType = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiShowPackages = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiHoverSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMultiSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiNewColor = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tslGenre = new System.Windows.Forms.ToolStripLabel();
			this.tslMessage = new System.Windows.Forms.ToolStripLabel();
			this.tsFilter = new System.Windows.Forms.ToolStrip();
			this.tslFilter = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.tstbFilter = new System.Windows.Forms.ToolStripTextBox();
			this.tstbMigemoAnswer = new System.Windows.Forms.ToolStripTextBox();
			this.cmsContent.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.tsMain.SuspendLayout();
			this.tsFilter.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chTitle,
            this.chEpisode,
            this.chSubTitle,
            this.chDuration,
            this.chDescription});
			this.listView1.ContextMenuStrip = this.cmsContent;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new System.Drawing.Size(418, 158);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			// 
			// chId
			// 
			this.chId.Text = "contents_id";
			this.chId.Width = 74;
			// 
			// chTitle
			// 
			this.chTitle.Text = "タイトル";
			this.chTitle.Width = 74;
			// 
			// chEpisode
			// 
			this.chEpisode.Text = "話数";
			this.chEpisode.Width = 53;
			// 
			// chSubTitle
			// 
			this.chSubTitle.Text = "サブタイトル";
			this.chSubTitle.Width = 55;
			// 
			// chDuration
			// 
			this.chDuration.Text = "正味時間";
			// 
			// chDescription
			// 
			this.chDescription.Text = "説明";
			this.chDescription.Width = 72;
			// 
			// cmsContent
			// 
			this.cmsContent.Enabled = true;
			this.cmsContent.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsContent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiPlay,
            this.toolStripSeparator6,
            this.tsmiPlayWithWmp,
            this.tsmiPlayWithIe,
            this.tsmiBroseDetailWithIe,
            this.toolStripSeparator1,
            this.tsmiCopyName,
            this.tsmiCopyDetailUri,
            this.tsmiCopyNameAndDetailUri,
            this.toolStripSeparator2,
            this.tsmiAddNg,
            this.toolStripSeparator3,
            this.tsmiUserCommands});
			this.cmsContent.Location = new System.Drawing.Point(21, 36);
			this.cmsContent.Name = "contextMenuStrip1";
			this.cmsContent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsContent.Size = new System.Drawing.Size(310, 248);
			this.cmsContent.Opening += new System.ComponentModel.CancelEventHandler(this.cmsContent_Opening);
			// 
			// tsmiAdd
			// 
			this.tsmiAdd.Name = "tsmiAdd";
			this.tsmiAdd.Text = "プレイリストに追加 (&A)";
			this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.tsmiPlay.Text = "プレイリストに追加せずに再生 (&P)";
			this.tsmiPlay.Click += new System.EventHandler(this.tsmiPlay_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			// 
			// tsmiPlayWithWmp
			// 
			this.tsmiPlayWithWmp.Name = "tsmiPlayWithWmp";
			this.tsmiPlayWithWmp.Text = "WMPで再生 (&W)";
			this.tsmiPlayWithWmp.Click += new System.EventHandler(this.tsmiPlayWithWmp_Click);
			// 
			// tsmiPlayWithIe
			// 
			this.tsmiPlayWithIe.Name = "tsmiPlayWithIe";
			this.tsmiPlayWithIe.Text = "IEで再生 (&I)";
			this.tsmiPlayWithIe.Click += new System.EventHandler(this.tsmiPlayWithIe_Click);
			// 
			// tsmiBroseDetailWithIe
			// 
			this.tsmiBroseDetailWithIe.Name = "tsmiBroseDetailWithIe";
			this.tsmiBroseDetailWithIe.Text = "IEで詳細ページ (&E)";
			this.tsmiBroseDetailWithIe.Click += new System.EventHandler(this.tsmiBroseDetailWithIe_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.C)));
			this.tsmiCopyName.Text = "コンテンツ名をコピー (&N)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyDetailUri
			// 
			this.tsmiCopyDetailUri.Name = "tsmiCopyDetailUri";
			this.tsmiCopyDetailUri.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.C)));
			this.tsmiCopyDetailUri.Text = "詳細ページURIをコピー (&D)";
			this.tsmiCopyDetailUri.Click += new System.EventHandler(this.tsmiCopyDetailUri_Click);
			// 
			// tsmiCopyNameAndDetailUri
			// 
			this.tsmiCopyNameAndDetailUri.Name = "tsmiCopyNameAndDetailUri";
			this.tsmiCopyNameAndDetailUri.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsmiCopyNameAndDetailUri.Text = "コンテンツ名と詳細ページURIをコピー (&B)";
			this.tsmiCopyNameAndDetailUri.Click += new System.EventHandler(this.tsmiCopyNameAndDetailUri_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiAddNg
			// 
			this.tsmiAddNg.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddNgWithId,
            this.tsmiAddNgWithTitle});
			this.tsmiAddNg.Name = "tsmiAddNg";
			this.tsmiAddNg.Text = "NGコンテンツに簡易追加 (&G)";
			// 
			// tsmiAddNgWithId
			// 
			this.tsmiAddNgWithId.Name = "tsmiAddNgWithId";
			this.tsmiAddNgWithId.Text = "コンテンツのIDでNG (&I)";
			this.tsmiAddNgWithId.Click += new System.EventHandler(this.tsmiAddNgWithId_Click);
			// 
			// tsmiAddNgWithTitle
			// 
			this.tsmiAddNgWithTitle.Name = "tsmiAddNgWithTitle";
			this.tsmiAddNgWithTitle.Text = "コンテンツのタイトルでNG (&T)";
			this.tsmiAddNgWithTitle.Click += new System.EventHandler(this.tsmiAddNgWithTitle_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			// 
			// tsmiUserCommands
			// 
			this.tsmiUserCommands.Name = "tsmiUserCommands";
			this.tsmiUserCommands.Text = "外部コマンド (&C)";
			// 
			// colorDialog1
			// 
			this.colorDialog1.AnyColor = true;
			this.colorDialog1.FullOpen = true;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.listView1);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(418, 208);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsMain);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsFilter);
			// 
			// tsMain
			// 
			this.tsMain.AllowItemReorder = true;
			this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
			this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslTitle,
            this.toolStripSeparator4,
            this.tsbShowFilter,
            this.toolStripDropDownButton1,
            this.toolStripSeparator5,
            this.tslGenre,
            this.tslMessage});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(418, 25);
			this.tsMain.Stretch = true;
			this.tsMain.TabIndex = 2;
			this.tsMain.Text = "toolStrip1";
			// 
			// tslTitle
			// 
			this.tslTitle.Name = "tslTitle";
			this.tslTitle.Text = "クロール結果";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			// 
			// tsbShowFilter
			// 
			this.tsbShowFilter.CheckOnClick = true;
			this.tsbShowFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbShowFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbShowFilter.Name = "tsbShowFilter";
			this.tsbShowFilter.Text = "Migemoフィルタ (&M)";
			this.tsbShowFilter.Click += new System.EventHandler(this.tsbShowFilter_Click);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAboneType,
            this.toolStripSeparator8,
            this.tsmiShowPackages,
            this.tsmiHoverSelect,
            this.tsmiMultiSelect,
            this.toolStripSeparator7,
            this.tsmiNewColor});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Text = "設定 (&C)";
			// 
			// tsmiAboneType
			// 
			this.tsmiAboneType.Name = "tsmiAboneType";
			this.tsmiAboneType.Text = "あぼ～ん方法 (&A)";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			// 
			// tsmiShowPackages
			// 
			this.tsmiShowPackages.Checked = true;
			this.tsmiShowPackages.CheckOnClick = true;
			this.tsmiShowPackages.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiShowPackages.Name = "tsmiShowPackages";
			this.tsmiShowPackages.Text = "パッケージごとに分類 (&G)";
			this.tsmiShowPackages.Click += new System.EventHandler(this.tsmiShowPackages_Click);
			// 
			// tsmiHoverSelect
			// 
			this.tsmiHoverSelect.CheckOnClick = true;
			this.tsmiHoverSelect.Name = "tsmiHoverSelect";
			this.tsmiHoverSelect.Text = "マウスホバーで選択 (&H)";
			this.tsmiHoverSelect.Click += new System.EventHandler(this.tsmiHoverSelect_Click);
			// 
			// tsmiMultiSelect
			// 
			this.tsmiMultiSelect.Checked = true;
			this.tsmiMultiSelect.CheckOnClick = true;
			this.tsmiMultiSelect.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiMultiSelect.Name = "tsmiMultiSelect";
			this.tsmiMultiSelect.Text = "複数選択を有効 (&M)";
			this.tsmiMultiSelect.Click += new System.EventHandler(this.tsmiMultiSelect_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			// 
			// tsmiNewColor
			// 
			this.tsmiNewColor.Name = "tsmiNewColor";
			this.tsmiNewColor.Text = "新着の色 (&N) ...";
			this.tsmiNewColor.Click += new System.EventHandler(this.tsmiNewColor_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			// 
			// tslGenre
			// 
			this.tslGenre.Name = "tslGenre";
			this.tslGenre.Text = "tslGenre";
			// 
			// tslMessage
			// 
			this.tslMessage.Name = "tslMessage";
			this.tslMessage.Text = "tslMessage";
			// 
			// tsFilter
			// 
			this.tsFilter.Dock = System.Windows.Forms.DockStyle.None;
			this.tsFilter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslFilter,
            this.toolStripSeparator9,
            this.tstbFilter,
            this.tstbMigemoAnswer});
			this.tsFilter.Location = new System.Drawing.Point(0, 25);
			this.tsFilter.Name = "tsFilter";
			this.tsFilter.Size = new System.Drawing.Size(418, 25);
			this.tsFilter.Stretch = true;
			this.tsFilter.TabIndex = 3;
			this.tsFilter.Text = "tsFilter";
			// 
			// tslFilter
			// 
			this.tslFilter.Name = "tslFilter";
			this.tslFilter.Text = "Migemoフィルタ";
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			// 
			// tstbFilter
			// 
			this.tstbFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.tstbFilter.Name = "tstbFilter";
			this.tstbFilter.Size = new System.Drawing.Size(150, 25);
			this.tstbFilter.TextChanged += new System.EventHandler(this.tstbFilter_TextChanged);
			// 
			// tstbMigemoAnswer
			// 
			this.tstbMigemoAnswer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.tstbMigemoAnswer.Name = "tstbMigemoAnswer";
			this.tstbMigemoAnswer.ReadOnly = true;
			this.tstbMigemoAnswer.Size = new System.Drawing.Size(150, 25);
			// 
			// CrawlResultView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "CrawlResultView";
			this.Size = new System.Drawing.Size(418, 208);
			this.cmsContent.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.tsMain.ResumeLayout(false);
			this.tsFilter.ResumeLayout(false);
			this.tsFilter.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader chId;
		private System.Windows.Forms.ColumnHeader chTitle;
		private System.Windows.Forms.ColumnHeader chEpisode;
		private System.Windows.Forms.ColumnHeader chSubTitle;
		private System.Windows.Forms.ColumnHeader chDuration;
		private System.Windows.Forms.ColumnHeader chDescription;
		private System.Windows.Forms.ContextMenuStrip cmsContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayWithWmp;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayWithIe;
		private System.Windows.Forms.ToolStripMenuItem tsmiBroseDetailWithIe;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyDetailUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyNameAndDetailUri;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommands;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNg;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNgWithId;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNgWithTitle;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ToolStripLabel tslTitle;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripLabel tslMessage;
		private System.Windows.Forms.ToolStripMenuItem tsmiAboneType;
		private System.Windows.Forms.ToolStripMenuItem tsmiNewColor;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem tsmiShowPackages;
		private System.Windows.Forms.ToolStripMenuItem tsmiMultiSelect;
		private System.Windows.Forms.ToolStripMenuItem tsmiHoverSelect;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripLabel tslGenre;
		private System.Windows.Forms.ToolStrip tsFilter;
		private System.Windows.Forms.ToolStripLabel tslFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripTextBox tstbFilter;
		private System.Windows.Forms.ToolStripButton tsbShowFilter;
		private System.Windows.Forms.ToolStripTextBox tstbMigemoAnswer;
	}
}
