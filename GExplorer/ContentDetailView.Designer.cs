namespace Yusen.GExplorer {
	partial class ContentDetailView {
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.picboxImage = new System.Windows.Forms.PictureBox();
			this.cmsImage = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiCopyImageUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyNameAndImageUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyNameDetailImageUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsucmiCommand = new Yusen.GExplorer.ToolStripUserCommandMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiTest = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTestLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTestLoadAsync = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTestCancelAsync = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiTestNewPictureBox = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabpSummary2 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtId = new System.Windows.Forms.TextBox();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.txtEpisode = new System.Windows.Forms.TextBox();
			this.txtSubtitle = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDuration = new System.Windows.Forms.TextBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtDeadline = new System.Windows.Forms.TextBox();
			this.tabpDetail = new System.Windows.Forms.TabPage();
			this.propgDetail = new System.Windows.Forms.PropertyGrid();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picboxImage)).BeginInit();
			this.cmsImage.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabpSummary2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tabpDetail.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.picboxImage);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(224, 387);
			this.splitContainer1.SplitterDistance = 157;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.Text = "splitContainer1";
			// 
			// picboxImage
			// 
			this.picboxImage.ContextMenuStrip = this.cmsImage;
			this.picboxImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picboxImage.Location = new System.Drawing.Point(0, 0);
			this.picboxImage.Name = "picboxImage";
			this.picboxImage.Size = new System.Drawing.Size(224, 157);
			this.picboxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picboxImage.TabIndex = 1;
			this.picboxImage.TabStop = false;
			this.picboxImage.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.picboxImage_LoadCompleted);
			// 
			// cmsImage
			// 
			this.cmsImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyImageUri,
            this.tsmiCopyNameAndImageUri,
            this.tsmiCopyNameDetailImageUri,
            this.tsmiCopyImage,
            this.toolStripSeparator2,
            this.tsucmiCommand,
            this.toolStripMenuItem2,
            this.tsmiTest});
			this.cmsImage.Name = "cmsImage";
			this.cmsImage.Size = new System.Drawing.Size(249, 170);
			this.cmsImage.Opening += new System.ComponentModel.CancelEventHandler(this.cmsImage_Opening);
			// 
			// tsmiCopyImageUri
			// 
			this.tsmiCopyImageUri.Name = "tsmiCopyImageUri";
			this.tsmiCopyImageUri.Size = new System.Drawing.Size(248, 22);
			this.tsmiCopyImageUri.Text = "画像URIをコピー(&U)";
			this.tsmiCopyImageUri.Click += new System.EventHandler(this.tsmiCopyImageUri_Click);
			// 
			// tsmiCopyNameAndImageUri
			// 
			this.tsmiCopyNameAndImageUri.Name = "tsmiCopyNameAndImageUri";
			this.tsmiCopyNameAndImageUri.Size = new System.Drawing.Size(248, 22);
			this.tsmiCopyNameAndImageUri.Text = "名前と画像URIをコピー(&B)";
			this.tsmiCopyNameAndImageUri.Click += new System.EventHandler(this.tsmiCopyNameAndImageUri_Click);
			// 
			// tsmiCopyNameDetailImageUri
			// 
			this.tsmiCopyNameDetailImageUri.Name = "tsmiCopyNameDetailImageUri";
			this.tsmiCopyNameDetailImageUri.Size = new System.Drawing.Size(248, 22);
			this.tsmiCopyNameDetailImageUri.Text = "名前，詳細URI，画像URIをコピー(&A)";
			this.tsmiCopyNameDetailImageUri.Click += new System.EventHandler(this.tsmiCopyNameDetailImageUri_Click);
			// 
			// tsmiCopyImage
			// 
			this.tsmiCopyImage.Name = "tsmiCopyImage";
			this.tsmiCopyImage.Size = new System.Drawing.Size(248, 22);
			this.tsmiCopyImage.Text = "画像をコピー(&I)";
			this.tsmiCopyImage.Click += new System.EventHandler(this.tsmiCopyImage_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(245, 6);
			// 
			// tsucmiCommand
			// 
			this.tsucmiCommand.Name = "tsucmiCommand";
			this.tsucmiCommand.Size = new System.Drawing.Size(248, 22);
			this.tsucmiCommand.Text = "外部コマンド(&C)";
			this.tsucmiCommand.UserCommandSelected += new System.EventHandler<Yusen.GExplorer.UserCommandSelectedEventArgs>(this.tsucmiCommand_UserCommandSelected);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(245, 6);
			// 
			// tsmiTest
			// 
			this.tsmiTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTestLoad,
            this.tsmiTestLoadAsync,
            this.tsmiTestCancelAsync,
            this.toolStripMenuItem3,
            this.tsmiTestNewPictureBox});
			this.tsmiTest.Name = "tsmiTest";
			this.tsmiTest.Size = new System.Drawing.Size(248, 22);
			this.tsmiTest.Text = "テスト(&T)";
			// 
			// tsmiTestLoad
			// 
			this.tsmiTestLoad.Name = "tsmiTestLoad";
			this.tsmiTestLoad.Size = new System.Drawing.Size(150, 22);
			this.tsmiTestLoad.Text = "&Load";
			this.tsmiTestLoad.Click += new System.EventHandler(this.tsmiTestLoad_Click);
			// 
			// tsmiTestLoadAsync
			// 
			this.tsmiTestLoadAsync.Name = "tsmiTestLoadAsync";
			this.tsmiTestLoadAsync.Size = new System.Drawing.Size(150, 22);
			this.tsmiTestLoadAsync.Text = "Load&Async";
			this.tsmiTestLoadAsync.Click += new System.EventHandler(this.tsmiTestLoadAsync_Click);
			// 
			// tsmiTestCancelAsync
			// 
			this.tsmiTestCancelAsync.Name = "tsmiTestCancelAsync";
			this.tsmiTestCancelAsync.Size = new System.Drawing.Size(150, 22);
			this.tsmiTestCancelAsync.Text = "&CancelAsync";
			this.tsmiTestCancelAsync.Click += new System.EventHandler(this.tsmiTestCancelAsync_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(147, 6);
			// 
			// tsmiTestNewPictureBox
			// 
			this.tsmiTestNewPictureBox.Name = "tsmiTestNewPictureBox";
			this.tsmiTestNewPictureBox.Size = new System.Drawing.Size(150, 22);
			this.tsmiTestNewPictureBox.Text = "&new PictureBox";
			this.tsmiTestNewPictureBox.Click += new System.EventHandler(this.tsmiTestNewPictureBox_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabpSummary2);
			this.tabControl1.Controls.Add(this.tabpDetail);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(224, 226);
			this.tabControl1.TabIndex = 0;
			// 
			// tabpSummary2
			// 
			this.tabpSummary2.Controls.Add(this.tableLayoutPanel2);
			this.tabpSummary2.Location = new System.Drawing.Point(4, 21);
			this.tabpSummary2.Name = "tabpSummary2";
			this.tabpSummary2.Size = new System.Drawing.Size(216, 201);
			this.tabpSummary2.TabIndex = 0;
			this.tabpSummary2.Text = "簡易";
			this.tabpSummary2.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.txtId, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.txtTitle, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.txtEpisode, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.txtSubtitle, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.txtDuration, 1, 4);
			this.tableLayoutPanel2.Controls.Add(this.txtDescription, 1, 5);
			this.tableLayoutPanel2.Controls.Add(this.label6, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.txtDeadline, 1, 5);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 7;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(216, 201);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(28, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "ID";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "タイトル";
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "シリ番";
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 3;
			this.label4.Text = "サブタイ";
			// 
			// txtId
			// 
			this.txtId.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtId.Location = new System.Drawing.Point(50, 3);
			this.txtId.Name = "txtId";
			this.txtId.ReadOnly = true;
			this.txtId.Size = new System.Drawing.Size(163, 19);
			this.txtId.TabIndex = 6;
			// 
			// txtTitle
			// 
			this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTitle.Location = new System.Drawing.Point(50, 27);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.ReadOnly = true;
			this.txtTitle.Size = new System.Drawing.Size(163, 19);
			this.txtTitle.TabIndex = 7;
			// 
			// txtEpisode
			// 
			this.txtEpisode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEpisode.Location = new System.Drawing.Point(50, 51);
			this.txtEpisode.Name = "txtEpisode";
			this.txtEpisode.ReadOnly = true;
			this.txtEpisode.Size = new System.Drawing.Size(163, 19);
			this.txtEpisode.TabIndex = 8;
			// 
			// txtSubtitle
			// 
			this.txtSubtitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSubtitle.Location = new System.Drawing.Point(50, 75);
			this.txtSubtitle.Name = "txtSubtitle";
			this.txtSubtitle.ReadOnly = true;
			this.txtSubtitle.Size = new System.Drawing.Size(163, 19);
			this.txtSubtitle.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 102);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 12);
			this.label5.TabIndex = 4;
			this.label5.Text = "時間";
			// 
			// txtDuration
			// 
			this.txtDuration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDuration.Location = new System.Drawing.Point(50, 99);
			this.txtDuration.Name = "txtDuration";
			this.txtDuration.ReadOnly = true;
			this.txtDuration.Size = new System.Drawing.Size(163, 19);
			this.txtDuration.TabIndex = 10;
			// 
			// txtDescription
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.txtDescription, 2);
			this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDescription.Location = new System.Drawing.Point(3, 147);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ReadOnly = true;
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(210, 51);
			this.txtDescription.TabIndex = 12;
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(15, 126);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 12);
			this.label6.TabIndex = 5;
			this.label6.Text = "期限";
			// 
			// txtDeadline
			// 
			this.txtDeadline.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDeadline.Location = new System.Drawing.Point(50, 123);
			this.txtDeadline.Name = "txtDeadline";
			this.txtDeadline.ReadOnly = true;
			this.txtDeadline.Size = new System.Drawing.Size(163, 19);
			this.txtDeadline.TabIndex = 11;
			// 
			// tabpDetail
			// 
			this.tabpDetail.Controls.Add(this.propgDetail);
			this.tabpDetail.Location = new System.Drawing.Point(4, 21);
			this.tabpDetail.Name = "tabpDetail";
			this.tabpDetail.Size = new System.Drawing.Size(216, 201);
			this.tabpDetail.TabIndex = 1;
			this.tabpDetail.Text = "詳細";
			this.tabpDetail.UseVisualStyleBackColor = true;
			// 
			// propgDetail
			// 
			this.propgDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propgDetail.Location = new System.Drawing.Point(0, 0);
			this.propgDetail.Name = "propgDetail";
			this.propgDetail.Size = new System.Drawing.Size(216, 201);
			this.propgDetail.TabIndex = 0;
			// 
			// ContentDetailView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ContentDetailView";
			this.Size = new System.Drawing.Size(224, 387);
			this.Load += new System.EventHandler(this.ContentDetailView_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picboxImage)).EndInit();
			this.cmsImage.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabpSummary2.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.tabpDetail.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabpSummary2;
		private System.Windows.Forms.TabPage tabpDetail;
		private System.Windows.Forms.PropertyGrid propgDetail;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.PictureBox picboxImage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.TextBox txtEpisode;
		private System.Windows.Forms.TextBox txtSubtitle;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.ContextMenuStrip cmsImage;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyImageUri;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtDuration;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyNameAndImageUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyNameDetailImageUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyImage;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtDeadline;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiTest;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestLoad;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestLoadAsync;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestCancelAsync;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestNewPictureBox;
		private ToolStripUserCommandMenuItem tsucmiCommand;
	}
}
