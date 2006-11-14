namespace Yusen.GExplorer.UserInterfaces {
	partial class DetailView {
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
			this.scRoot = new System.Windows.Forms.SplitContainer();
			this.pbImage = new System.Windows.Forms.PictureBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabpDescription = new System.Windows.Forms.TabPage();
			this.tabpReview = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.scReview = new System.Windows.Forms.SplitContainer();
			this.txtReview = new System.Windows.Forms.TextBox();
			this.lblReviewSummary = new System.Windows.Forms.Label();
			this.tabpContent = new System.Windows.Forms.TabPage();
			this.pgProperty = new System.Windows.Forms.PropertyGrid();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiDetailView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyBoth = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyTriple = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyImage = new System.Windows.Forms.ToolStripMenuItem();
			this.wbDescription = new System.Windows.Forms.WebBrowser();
			this.lvReview = new Yusen.GExplorer.UserInterfaces.DoubleBufferedListView();
			this.chNeta = new System.Windows.Forms.ColumnHeader();
			this.chScore = new System.Windows.Forms.ColumnHeader();
			this.chRef = new System.Windows.Forms.ColumnHeader();
			this.chTitle = new System.Windows.Forms.ColumnHeader();
			this.chAuthor = new System.Windows.Forms.ColumnHeader();
			this.chDate = new System.Windows.Forms.ColumnHeader();
			this.scRoot.Panel1.SuspendLayout();
			this.scRoot.Panel2.SuspendLayout();
			this.scRoot.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabpDescription.SuspendLayout();
			this.tabpReview.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.scReview.Panel1.SuspendLayout();
			this.scReview.Panel2.SuspendLayout();
			this.scReview.SuspendLayout();
			this.tabpContent.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// scRoot
			// 
			this.scRoot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scRoot.Location = new System.Drawing.Point(0, 0);
			this.scRoot.Name = "scRoot";
			this.scRoot.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scRoot.Panel1
			// 
			this.scRoot.Panel1.Controls.Add(this.pbImage);
			// 
			// scRoot.Panel2
			// 
			this.scRoot.Panel2.Controls.Add(this.tabControl1);
			this.scRoot.Size = new System.Drawing.Size(246, 550);
			this.scRoot.SplitterDistance = 191;
			this.scRoot.TabIndex = 0;
			this.scRoot.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scRoot_SplitterMoved);
			// 
			// pbImage
			// 
			this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbImage.Location = new System.Drawing.Point(0, 0);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(246, 191);
			this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbImage.TabIndex = 0;
			this.pbImage.TabStop = false;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabpDescription);
			this.tabControl1.Controls.Add(this.tabpReview);
			this.tabControl1.Controls.Add(this.tabpContent);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(246, 355);
			this.tabControl1.TabIndex = 0;
			// 
			// tabpDescription
			// 
			this.tabpDescription.Controls.Add(this.wbDescription);
			this.tabpDescription.Location = new System.Drawing.Point(4, 21);
			this.tabpDescription.Name = "tabpDescription";
			this.tabpDescription.Size = new System.Drawing.Size(238, 330);
			this.tabpDescription.TabIndex = 0;
			this.tabpDescription.Text = "説明文";
			this.tabpDescription.UseVisualStyleBackColor = true;
			// 
			// tabpReview
			// 
			this.tabpReview.Controls.Add(this.tableLayoutPanel1);
			this.tabpReview.Location = new System.Drawing.Point(4, 21);
			this.tabpReview.Name = "tabpReview";
			this.tabpReview.Size = new System.Drawing.Size(238, 330);
			this.tabpReview.TabIndex = 1;
			this.tabpReview.Text = "レビュー新着分";
			this.tabpReview.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.scReview, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblReviewSummary, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(238, 330);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// scReview
			// 
			this.scReview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scReview.Location = new System.Drawing.Point(3, 21);
			this.scReview.Name = "scReview";
			this.scReview.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scReview.Panel1
			// 
			this.scReview.Panel1.Controls.Add(this.lvReview);
			// 
			// scReview.Panel2
			// 
			this.scReview.Panel2.Controls.Add(this.txtReview);
			this.scReview.Size = new System.Drawing.Size(232, 306);
			this.scReview.SplitterDistance = 153;
			this.scReview.TabIndex = 0;
			this.scReview.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scReview_SplitterMoved);
			// 
			// txtReview
			// 
			this.txtReview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtReview.Location = new System.Drawing.Point(0, 0);
			this.txtReview.Multiline = true;
			this.txtReview.Name = "txtReview";
			this.txtReview.ReadOnly = true;
			this.txtReview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtReview.Size = new System.Drawing.Size(232, 149);
			this.txtReview.TabIndex = 0;
			// 
			// lblReviewSummary
			// 
			this.lblReviewSummary.AutoSize = true;
			this.lblReviewSummary.Location = new System.Drawing.Point(3, 0);
			this.lblReviewSummary.Name = "lblReviewSummary";
			this.lblReviewSummary.Size = new System.Drawing.Size(101, 12);
			this.lblReviewSummary.TabIndex = 1;
			this.lblReviewSummary.Text = "lblReviewSummary";
			// 
			// tabpContent
			// 
			this.tabpContent.Controls.Add(this.pgProperty);
			this.tabpContent.Location = new System.Drawing.Point(4, 21);
			this.tabpContent.Name = "tabpContent";
			this.tabpContent.Size = new System.Drawing.Size(238, 330);
			this.tabpContent.TabIndex = 2;
			this.tabpContent.Text = "プロパティ";
			this.tabpContent.UseVisualStyleBackColor = true;
			// 
			// pgProperty
			// 
			this.pgProperty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgProperty.Location = new System.Drawing.Point(0, 0);
			this.pgProperty.Name = "pgProperty";
			this.pgProperty.Size = new System.Drawing.Size(238, 330);
			this.pgProperty.TabIndex = 0;
			this.pgProperty.ToolbarVisible = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDetailView});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(246, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			// 
			// tsmiDetailView
			// 
			this.tsmiDetailView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyName,
            this.tsmiCopyUri,
            this.tsmiCopyBoth,
            this.tsmiCopyTriple,
            this.toolStripMenuItem1,
            this.tsmiCopyImage});
			this.tsmiDetailView.Name = "tsmiDetailView";
			this.tsmiDetailView.Size = new System.Drawing.Size(68, 20);
			this.tsmiDetailView.Text = "DetailView";
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.Size = new System.Drawing.Size(278, 22);
			this.tsmiCopyName.Text = "名前をコピー(&N)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyUri
			// 
			this.tsmiCopyUri.Name = "tsmiCopyUri";
			this.tsmiCopyUri.Size = new System.Drawing.Size(278, 22);
			this.tsmiCopyUri.Text = "画像URIをコピー(&U)";
			this.tsmiCopyUri.Click += new System.EventHandler(this.tsmiCopyUri_Click);
			// 
			// tsmiCopyBoth
			// 
			this.tsmiCopyBoth.Name = "tsmiCopyBoth";
			this.tsmiCopyBoth.Size = new System.Drawing.Size(278, 22);
			this.tsmiCopyBoth.Text = "名前と画像URIをコピー(&B)";
			this.tsmiCopyBoth.Click += new System.EventHandler(this.tsmiCopyBoth_Click);
			// 
			// tsmiCopyTriple
			// 
			this.tsmiCopyTriple.Name = "tsmiCopyTriple";
			this.tsmiCopyTriple.Size = new System.Drawing.Size(278, 22);
			this.tsmiCopyTriple.Text = "名前，詳細ページURI，画像URIをコピー(&T)";
			this.tsmiCopyTriple.Click += new System.EventHandler(this.tsmiCopyTriple_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(275, 6);
			// 
			// tsmiCopyImage
			// 
			this.tsmiCopyImage.Name = "tsmiCopyImage";
			this.tsmiCopyImage.Size = new System.Drawing.Size(278, 22);
			this.tsmiCopyImage.Text = "画像をコピー(&I)";
			this.tsmiCopyImage.Click += new System.EventHandler(this.tsmiCopyImage_Click);
			// 
			// wbDescription
			// 
			this.wbDescription.AllowNavigation = false;
			this.wbDescription.AllowWebBrowserDrop = false;
			this.wbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wbDescription.Location = new System.Drawing.Point(0, 0);
			this.wbDescription.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbDescription.Name = "wbDescription";
			this.wbDescription.Size = new System.Drawing.Size(238, 330);
			this.wbDescription.TabIndex = 0;
			this.wbDescription.WebBrowserShortcutsEnabled = false;
			// 
			// lvReview
			// 
			this.lvReview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNeta,
            this.chScore,
            this.chRef,
            this.chTitle,
            this.chAuthor,
            this.chDate});
			this.lvReview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvReview.FullRowSelect = true;
			this.lvReview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvReview.HideSelection = false;
			this.lvReview.Location = new System.Drawing.Point(0, 0);
			this.lvReview.MultiSelect = false;
			this.lvReview.Name = "lvReview";
			this.lvReview.ShowItemToolTips = true;
			this.lvReview.Size = new System.Drawing.Size(232, 153);
			this.lvReview.TabIndex = 0;
			this.lvReview.UseCompatibleStateImageBehavior = false;
			this.lvReview.View = System.Windows.Forms.View.Details;
			this.lvReview.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvReview_ColumnWidthChanged);
			this.lvReview.SelectedIndexChanged += new System.EventHandler(this.lvReview_SelectedIndexChanged);
			// 
			// chNeta
			// 
			this.chNeta.Text = "ネタバレ";
			this.chNeta.Width = 14;
			// 
			// chScore
			// 
			this.chScore.Text = "評価";
			this.chScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.chScore.Width = 24;
			// 
			// chRef
			// 
			this.chRef.Text = "参考";
			this.chRef.Width = 42;
			// 
			// chTitle
			// 
			this.chTitle.Text = "タイトル";
			this.chTitle.Width = 80;
			// 
			// chAuthor
			// 
			this.chAuthor.Text = "投稿者";
			this.chAuthor.Width = 50;
			// 
			// chDate
			// 
			this.chDate.Text = "投稿日";
			this.chDate.Width = 72;
			// 
			// DetailView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.scRoot);
			this.Controls.Add(this.menuStrip1);
			this.Name = "DetailView";
			this.Size = new System.Drawing.Size(246, 550);
			this.Load += new System.EventHandler(this.DetailView_Load);
			this.scRoot.Panel1.ResumeLayout(false);
			this.scRoot.Panel2.ResumeLayout(false);
			this.scRoot.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabpDescription.ResumeLayout(false);
			this.tabpReview.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.scReview.Panel1.ResumeLayout(false);
			this.scReview.Panel2.ResumeLayout(false);
			this.scReview.Panel2.PerformLayout();
			this.scReview.ResumeLayout(false);
			this.tabpContent.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.SplitContainer scRoot;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabpDescription;
		private System.Windows.Forms.TabPage tabpReview;
		private System.Windows.Forms.TabPage tabpContent;
		private System.Windows.Forms.SplitContainer scReview;
		private System.Windows.Forms.TextBox txtReview;
		private Yusen.GExplorer.UserInterfaces.DoubleBufferedListView lvReview;
		private System.Windows.Forms.ColumnHeader chNeta;
		private System.Windows.Forms.ColumnHeader chTitle;
		private System.Windows.Forms.ColumnHeader chAuthor;
		private System.Windows.Forms.ColumnHeader chDate;
		private System.Windows.Forms.ColumnHeader chScore;
		private System.Windows.Forms.ColumnHeader chRef;
		private System.Windows.Forms.WebBrowser wbDescription;
		private System.Windows.Forms.PropertyGrid pgProperty;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiDetailView;
		private System.Windows.Forms.PictureBox pbImage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblReviewSummary;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyBoth;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyTriple;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyImage;
	}
}
