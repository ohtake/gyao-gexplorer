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
			this.wbDescription = new System.Windows.Forms.WebBrowser();
			this.tabpReview = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.scReview = new System.Windows.Forms.SplitContainer();
			this.lvReview = new Yusen.GExplorer.UserInterfaces.DoubleBufferedListView();
			this.chNeta = new System.Windows.Forms.ColumnHeader();
			this.chScore = new System.Windows.Forms.ColumnHeader();
			this.chRef = new System.Windows.Forms.ColumnHeader();
			this.chTitle = new System.Windows.Forms.ColumnHeader();
			this.chAuthor = new System.Windows.Forms.ColumnHeader();
			this.chDate = new System.Windows.Forms.ColumnHeader();
			this.chLength = new System.Windows.Forms.ColumnHeader();
			this.txtReview = new System.Windows.Forms.TextBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnReadMore = new System.Windows.Forms.Button();
			this.btnReviewList = new System.Windows.Forms.Button();
			this.btnReviewInput = new System.Windows.Forms.Button();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnVoteYes = new System.Windows.Forms.Button();
			this.btnVoteNo = new System.Windows.Forms.Button();
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
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSortReviewPosts = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReadMoreReviews = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpenReviewList = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpenReviewPost = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiVoteYes = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiVoteNo = new System.Windows.Forms.ToolStripMenuItem();
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
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
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
			this.scRoot.SplitterDistance = 189;
			this.scRoot.TabIndex = 0;
			this.scRoot.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scRoot_SplitterMoved);
			// 
			// pbImage
			// 
			this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbImage.Location = new System.Drawing.Point(0, 0);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(246, 189);
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
			this.tabControl1.Size = new System.Drawing.Size(246, 357);
			this.tabControl1.TabIndex = 0;
			// 
			// tabpDescription
			// 
			this.tabpDescription.Controls.Add(this.wbDescription);
			this.tabpDescription.Location = new System.Drawing.Point(4, 22);
			this.tabpDescription.Name = "tabpDescription";
			this.tabpDescription.Size = new System.Drawing.Size(238, 331);
			this.tabpDescription.TabIndex = 0;
			this.tabpDescription.Text = "説明文";
			this.tabpDescription.UseVisualStyleBackColor = true;
			// 
			// wbDescription
			// 
			this.wbDescription.AllowNavigation = false;
			this.wbDescription.AllowWebBrowserDrop = false;
			this.wbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wbDescription.Location = new System.Drawing.Point(0, 0);
			this.wbDescription.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbDescription.Name = "wbDescription";
			this.wbDescription.Size = new System.Drawing.Size(238, 331);
			this.wbDescription.TabIndex = 0;
			this.wbDescription.WebBrowserShortcutsEnabled = false;
			// 
			// tabpReview
			// 
			this.tabpReview.Controls.Add(this.tableLayoutPanel1);
			this.tabpReview.Location = new System.Drawing.Point(4, 22);
			this.tabpReview.Name = "tabpReview";
			this.tabpReview.Size = new System.Drawing.Size(238, 331);
			this.tabpReview.TabIndex = 1;
			this.tabpReview.Text = "レビュー新着分";
			this.tabpReview.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.scReview, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(238, 331);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// scReview
			// 
			this.scReview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scReview.Location = new System.Drawing.Point(3, 39);
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
			this.scReview.Size = new System.Drawing.Size(232, 253);
			this.scReview.SplitterDistance = 125;
			this.scReview.TabIndex = 0;
			this.scReview.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.scReview_SplitterMoved);
			// 
			// lvReview
			// 
			this.lvReview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNeta,
            this.chScore,
            this.chRef,
            this.chTitle,
            this.chAuthor,
            this.chDate,
            this.chLength});
			this.lvReview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvReview.FullRowSelect = true;
			this.lvReview.HideSelection = false;
			this.lvReview.Location = new System.Drawing.Point(0, 0);
			this.lvReview.MultiSelect = false;
			this.lvReview.Name = "lvReview";
			this.lvReview.ShowItemToolTips = true;
			this.lvReview.Size = new System.Drawing.Size(232, 125);
			this.lvReview.TabIndex = 0;
			this.lvReview.UseCompatibleStateImageBehavior = false;
			this.lvReview.View = System.Windows.Forms.View.Details;
			this.lvReview.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvReview_ColumnWidthChanged);
			this.lvReview.SelectedIndexChanged += new System.EventHandler(this.lvReview_SelectedIndexChanged);
			this.lvReview.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvReview_ColumnClick);
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
			// chLength
			// 
			this.chLength.Text = "文字数";
			this.chLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.chLength.Width = 36;
			// 
			// txtReview
			// 
			this.txtReview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtReview.Location = new System.Drawing.Point(0, 0);
			this.txtReview.Multiline = true;
			this.txtReview.Name = "txtReview";
			this.txtReview.ReadOnly = true;
			this.txtReview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtReview.Size = new System.Drawing.Size(232, 124);
			this.txtReview.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnReadMore);
			this.flowLayoutPanel1.Controls.Add(this.btnReviewList);
			this.flowLayoutPanel1.Controls.Add(this.btnReviewInput);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(232, 30);
			this.flowLayoutPanel1.TabIndex = 0;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// btnReadMore
			// 
			this.btnReadMore.Location = new System.Drawing.Point(3, 3);
			this.btnReadMore.Name = "btnReadMore";
			this.btnReadMore.Size = new System.Drawing.Size(82, 23);
			this.btnReadMore.TabIndex = 0;
			this.btnReadMore.Text = "追加取得(&E)";
			this.btnReadMore.UseVisualStyleBackColor = true;
			this.btnReadMore.Click += new System.EventHandler(this.btnReadMore_Click);
			// 
			// btnReviewList
			// 
			this.btnReviewList.Location = new System.Drawing.Point(91, 3);
			this.btnReviewList.Name = "btnReviewList";
			this.btnReviewList.Size = new System.Drawing.Size(58, 23);
			this.btnReviewList.TabIndex = 1;
			this.btnReviewList.Text = "一覧(&V)";
			this.btnReviewList.UseVisualStyleBackColor = true;
			this.btnReviewList.Click += new System.EventHandler(this.btnReviewList_Click);
			// 
			// btnReviewInput
			// 
			this.btnReviewInput.Location = new System.Drawing.Point(155, 3);
			this.btnReviewInput.Name = "btnReviewInput";
			this.btnReviewInput.Size = new System.Drawing.Size(58, 23);
			this.btnReviewInput.TabIndex = 2;
			this.btnReviewInput.Text = "投稿(&P)";
			this.btnReviewInput.UseVisualStyleBackColor = true;
			this.btnReviewInput.Click += new System.EventHandler(this.btnReviewInput_Click);
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btnVoteYes);
			this.flowLayoutPanel2.Controls.Add(this.btnVoteNo);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 298);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(232, 30);
			this.flowLayoutPanel2.TabIndex = 1;
			this.flowLayoutPanel2.WrapContents = false;
			// 
			// btnVoteYes
			// 
			this.btnVoteYes.Location = new System.Drawing.Point(3, 3);
			this.btnVoteYes.Name = "btnVoteYes";
			this.btnVoteYes.Size = new System.Drawing.Size(96, 23);
			this.btnVoteYes.TabIndex = 0;
			this.btnVoteYes.Text = "参考になった(&Y)";
			this.btnVoteYes.UseVisualStyleBackColor = true;
			this.btnVoteYes.Click += new System.EventHandler(this.btnVoteYes_Click);
			// 
			// btnVoteNo
			// 
			this.btnVoteNo.Location = new System.Drawing.Point(105, 3);
			this.btnVoteNo.Name = "btnVoteNo";
			this.btnVoteNo.Size = new System.Drawing.Size(96, 23);
			this.btnVoteNo.TabIndex = 1;
			this.btnVoteNo.Text = "ならなかった(&Z)";
			this.btnVoteNo.UseVisualStyleBackColor = true;
			this.btnVoteNo.Click += new System.EventHandler(this.btnVoteNo_Click);
			// 
			// tabpContent
			// 
			this.tabpContent.Controls.Add(this.pgProperty);
			this.tabpContent.Location = new System.Drawing.Point(4, 22);
			this.tabpContent.Name = "tabpContent";
			this.tabpContent.Size = new System.Drawing.Size(238, 331);
			this.tabpContent.TabIndex = 2;
			this.tabpContent.Text = "プロパティ";
			this.tabpContent.UseVisualStyleBackColor = true;
			// 
			// pgProperty
			// 
			this.pgProperty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgProperty.Location = new System.Drawing.Point(0, 0);
			this.pgProperty.Name = "pgProperty";
			this.pgProperty.Size = new System.Drawing.Size(238, 331);
			this.pgProperty.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDetailView});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(246, 26);
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
            this.tsmiCopyImage,
            this.toolStripMenuItem2,
            this.tsmiSortReviewPosts,
            this.tsmiReadMoreReviews,
            this.tsmiOpenReviewList,
            this.tsmiOpenReviewPost,
            this.toolStripMenuItem3,
            this.tsmiVoteYes,
            this.tsmiVoteNo});
			this.tsmiDetailView.Name = "tsmiDetailView";
			this.tsmiDetailView.Size = new System.Drawing.Size(82, 22);
			this.tsmiDetailView.Text = "DetailView";
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.Size = new System.Drawing.Size(318, 22);
			this.tsmiCopyName.Text = "名前をコピー(&N)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyUri
			// 
			this.tsmiCopyUri.Name = "tsmiCopyUri";
			this.tsmiCopyUri.Size = new System.Drawing.Size(318, 22);
			this.tsmiCopyUri.Text = "画像URIをコピー(&U)";
			this.tsmiCopyUri.Click += new System.EventHandler(this.tsmiCopyUri_Click);
			// 
			// tsmiCopyBoth
			// 
			this.tsmiCopyBoth.Name = "tsmiCopyBoth";
			this.tsmiCopyBoth.Size = new System.Drawing.Size(318, 22);
			this.tsmiCopyBoth.Text = "名前と画像URIをコピー(&B)";
			this.tsmiCopyBoth.Click += new System.EventHandler(this.tsmiCopyBoth_Click);
			// 
			// tsmiCopyTriple
			// 
			this.tsmiCopyTriple.Name = "tsmiCopyTriple";
			this.tsmiCopyTriple.Size = new System.Drawing.Size(318, 22);
			this.tsmiCopyTriple.Text = "名前，詳細ページURI，画像URIをコピー(&T)";
			this.tsmiCopyTriple.Click += new System.EventHandler(this.tsmiCopyTriple_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(315, 6);
			// 
			// tsmiCopyImage
			// 
			this.tsmiCopyImage.Name = "tsmiCopyImage";
			this.tsmiCopyImage.Size = new System.Drawing.Size(318, 22);
			this.tsmiCopyImage.Text = "画像をコピー(&I)";
			this.tsmiCopyImage.Click += new System.EventHandler(this.tsmiCopyImage_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(315, 6);
			// 
			// tsmiSortReviewPosts
			// 
			this.tsmiSortReviewPosts.Name = "tsmiSortReviewPosts";
			this.tsmiSortReviewPosts.Size = new System.Drawing.Size(318, 22);
			this.tsmiSortReviewPosts.Text = "レビューのソート(&S)";
			// 
			// tsmiReadMoreReviews
			// 
			this.tsmiReadMoreReviews.Name = "tsmiReadMoreReviews";
			this.tsmiReadMoreReviews.Size = new System.Drawing.Size(318, 22);
			this.tsmiReadMoreReviews.Text = "レビューの追加取得(&E)";
			this.tsmiReadMoreReviews.Click += new System.EventHandler(this.tsmiReadMoreReviews_Click);
			// 
			// tsmiOpenReviewList
			// 
			this.tsmiOpenReviewList.Name = "tsmiOpenReviewList";
			this.tsmiOpenReviewList.Size = new System.Drawing.Size(318, 22);
			this.tsmiOpenReviewList.Text = "レビュー一覧ページをブラウザで開く(&V)";
			this.tsmiOpenReviewList.Click += new System.EventHandler(this.tsmiOpenReviewList_Click);
			// 
			// tsmiOpenReviewPost
			// 
			this.tsmiOpenReviewPost.Name = "tsmiOpenReviewPost";
			this.tsmiOpenReviewPost.Size = new System.Drawing.Size(318, 22);
			this.tsmiOpenReviewPost.Text = "レビュー投稿ページをブラウザで開く(&P)";
			this.tsmiOpenReviewPost.Click += new System.EventHandler(this.tsmiOpenReviewPost_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(315, 6);
			// 
			// tsmiVoteYes
			// 
			this.tsmiVoteYes.Name = "tsmiVoteYes";
			this.tsmiVoteYes.Size = new System.Drawing.Size(318, 22);
			this.tsmiVoteYes.Text = "レビューが参考になった(&Y)";
			this.tsmiVoteYes.Click += new System.EventHandler(this.tsmiVoteYes_Click);
			// 
			// tsmiVoteNo
			// 
			this.tsmiVoteNo.Name = "tsmiVoteNo";
			this.tsmiVoteNo.Size = new System.Drawing.Size(318, 22);
			this.tsmiVoteNo.Text = "レビューが参考にならなかった(&Z)";
			this.tsmiVoteNo.Click += new System.EventHandler(this.tsmiVoteNo_Click);
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
			this.scReview.Panel1.ResumeLayout(false);
			this.scReview.Panel2.ResumeLayout(false);
			this.scReview.Panel2.PerformLayout();
			this.scReview.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyBoth;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyTriple;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyImage;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnReviewList;
		private System.Windows.Forms.Button btnReviewInput;
		private System.Windows.Forms.Button btnReadMore;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiSortReviewPosts;
		private System.Windows.Forms.ToolStripMenuItem tsmiReadMoreReviews;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenReviewList;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenReviewPost;
		private System.Windows.Forms.ColumnHeader chLength;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button btnVoteYes;
		private System.Windows.Forms.Button btnVoteNo;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiVoteYes;
		private System.Windows.Forms.ToolStripMenuItem tsmiVoteNo;
	}
}
