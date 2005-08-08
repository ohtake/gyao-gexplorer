namespace Yusen.GExplorer {
	partial class GenreListView {
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.chId = new System.Windows.Forms.ColumnHeader();
			this.chTitle = new System.Windows.Forms.ColumnHeader();
			this.chEpisode = new System.Windows.Forms.ColumnHeader();
			this.chSubTitle = new System.Windows.Forms.ColumnHeader();
			this.chDuration = new System.Windows.Forms.ColumnHeader();
			this.chDescription = new System.Windows.Forms.ColumnHeader();
			this.cmsContent = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
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
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiAboneType = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNewColor = new System.Windows.Forms.ToolStripMenuItem();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.cmsContent.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
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
			this.listView1.HotTracking = true;
			this.listView1.HoverSelection = true;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new System.Drawing.Size(418, 208);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
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
			this.chTitle.Width = 67;
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
			this.chDuration.Width = 53;
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
            this.tsmiPlay,
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
            this.tsmiUserCommands,
            this.toolStripSeparator4,
            this.tsmiAboneType,
            this.tsmiNewColor});
			this.cmsContent.Location = new System.Drawing.Point(21, 36);
			this.cmsContent.Name = "contextMenuStrip1";
			this.cmsContent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsContent.Size = new System.Drawing.Size(263, 289);
			this.cmsContent.Visible = true;
			this.cmsContent.Opening += new System.ComponentModel.CancelEventHandler(this.cmsContent_Opening);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.Text = "専用プレーヤで再生 (&P)";
			this.tsmiPlay.Click += new System.EventHandler(this.tsmiPlay_Click);
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
			this.tsmiBroseDetailWithIe.Text = "IEで詳細ページ (&D)";
			this.tsmiBroseDetailWithIe.Click += new System.EventHandler(this.tsmiBroseDetailWithIe_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.Text = "コンテンツ名をコピー (&C)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyDetailUri
			// 
			this.tsmiCopyDetailUri.Name = "tsmiCopyDetailUri";
			this.tsmiCopyDetailUri.Text = "詳細ページURIをコピー (&O)";
			this.tsmiCopyDetailUri.Click += new System.EventHandler(this.tsmiCopyDetailUri_Click);
			// 
			// tsmiCopyNameAndDetailUri
			// 
			this.tsmiCopyNameAndDetailUri.Name = "tsmiCopyNameAndDetailUri";
			this.tsmiCopyNameAndDetailUri.Text = "コンテンツ名と詳細ページURIをコピー (&Y)";
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
			this.tsmiAddNg.Text = "NGコンテンツに簡易追加 (&N)";
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
			this.tsmiUserCommands.Text = "外部コマンド (&U)";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			// 
			// tsmiAboneType
			// 
			this.tsmiAboneType.Name = "tsmiAboneType";
			this.tsmiAboneType.Text = "あぼ～ん方法の設定 (&A)";
			// 
			// tsmiNewColor
			// 
			this.tsmiNewColor.Name = "tsmiNewColor";
			this.tsmiNewColor.Text = "新着の色を設定 (&E) ...";
			this.tsmiNewColor.Click += new System.EventHandler(this.tsmiNewColor_Click);
			// 
			// colorDialog1
			// 
			this.colorDialog1.AnyColor = true;
			this.colorDialog1.FullOpen = true;
			// 
			// GenreListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listView1);
			this.Name = "GenreListView";
			this.Size = new System.Drawing.Size(418, 208);
			this.cmsContent.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
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
		private System.Windows.Forms.ToolStripMenuItem tsmiAboneType;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNg;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNgWithId;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNgWithTitle;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.ToolStripMenuItem tsmiNewColor;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
	}
}
