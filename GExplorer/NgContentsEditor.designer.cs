namespace Yusen.GExplorer {
	partial class NgContentsEditor {
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
			this.lvNgContents = new System.Windows.Forms.ListView();
			this.chComment = new System.Windows.Forms.ColumnHeader();
			this.chPropertyName = new System.Windows.Forms.ColumnHeader();
			this.chPredicate = new System.Windows.Forms.ColumnHeader();
			this.chWord = new System.Windows.Forms.ColumnHeader();
			this.chCreated = new System.Windows.Forms.ColumnHeader();
			this.chLastAbone = new System.Windows.Forms.ColumnHeader();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.grpAdd = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.txtWord = new System.Windows.Forms.TextBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.comboMethod = new System.Windows.Forms.ComboBox();
			this.comboProperty = new System.Windows.Forms.ComboBox();
			this.lblWord = new System.Windows.Forms.Label();
			this.lblMethod = new System.Windows.Forms.Label();
			this.lblProperty = new System.Windows.Forms.Label();
			this.lblComment = new System.Windows.Forms.Label();
			this.btnAdd = new System.Windows.Forms.Button();
			this.grpList = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.grpAdd.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.grpList.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvNgContents
			// 
			this.lvNgContents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chComment,
            this.chPropertyName,
            this.chPredicate,
            this.chWord,
            this.chCreated,
            this.chLastAbone});
			this.lvNgContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvNgContents.FullRowSelect = true;
			this.lvNgContents.HideSelection = false;
			this.lvNgContents.Location = new System.Drawing.Point(3, 15);
			this.lvNgContents.Name = "lvNgContents";
			this.lvNgContents.Size = new System.Drawing.Size(580, 128);
			this.lvNgContents.TabIndex = 101;
			this.lvNgContents.View = System.Windows.Forms.View.Details;
			this.lvNgContents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvNgContents_ColumnClick);
			this.lvNgContents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvNgContents_KeyDown);
			// 
			// chComment
			// 
			this.chComment.Text = "コメント";
			this.chComment.Width = 90;
			// 
			// chPropertyName
			// 
			this.chPropertyName.Text = "プロパティ名";
			this.chPropertyName.Width = 80;
			// 
			// chPredicate
			// 
			this.chPredicate.Text = "比較方法";
			this.chPredicate.Width = 65;
			// 
			// chWord
			// 
			this.chWord.Text = "NGワード";
			this.chWord.Width = 90;
			// 
			// chCreated
			// 
			this.chCreated.Text = "作成日時";
			this.chCreated.Width = 116;
			// 
			// chLastAbone
			// 
			this.chLastAbone.Text = "最終NG日時";
			this.chLastAbone.Width = 116;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.grpAdd, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.grpList, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(592, 224);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// grpAdd
			// 
			this.grpAdd.Controls.Add(this.tableLayoutPanel2);
			this.grpAdd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpAdd.Location = new System.Drawing.Point(3, 155);
			this.grpAdd.Name = "grpAdd";
			this.grpAdd.Size = new System.Drawing.Size(586, 66);
			this.grpAdd.TabIndex = 200;
			this.grpAdd.TabStop = false;
			this.grpAdd.Text = "NGコンテンツの追加";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 5;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.tableLayoutPanel2.Controls.Add(this.txtWord, 3, 1);
			this.tableLayoutPanel2.Controls.Add(this.txtComment, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.comboMethod, 2, 1);
			this.tableLayoutPanel2.Controls.Add(this.comboProperty, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.lblWord, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblMethod, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblProperty, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblComment, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnAdd, 4, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 15);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(580, 48);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// txtWord
			// 
			this.txtWord.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtWord.Location = new System.Drawing.Point(384, 27);
			this.txtWord.Name = "txtWord";
			this.txtWord.Size = new System.Drawing.Size(121, 19);
			this.txtWord.TabIndex = 241;
			// 
			// txtComment
			// 
			this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtComment.Location = new System.Drawing.Point(3, 27);
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(121, 19);
			this.txtComment.TabIndex = 211;
			// 
			// comboMethod
			// 
			this.comboMethod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.comboMethod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboMethod.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboMethod.FormattingEnabled = true;
			this.comboMethod.Location = new System.Drawing.Point(257, 27);
			this.comboMethod.Name = "comboMethod";
			this.comboMethod.Size = new System.Drawing.Size(121, 20);
			this.comboMethod.TabIndex = 231;
			// 
			// comboProperty
			// 
			this.comboProperty.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.comboProperty.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboProperty.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboProperty.FormattingEnabled = true;
			this.comboProperty.Location = new System.Drawing.Point(130, 27);
			this.comboProperty.Name = "comboProperty";
			this.comboProperty.Size = new System.Drawing.Size(121, 20);
			this.comboProperty.TabIndex = 221;
			// 
			// lblWord
			// 
			this.lblWord.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblWord.AutoSize = true;
			this.lblWord.Location = new System.Drawing.Point(384, 6);
			this.lblWord.Name = "lblWord";
			this.lblWord.Size = new System.Drawing.Size(110, 12);
			this.lblWord.TabIndex = 240;
			this.lblWord.Text = "目的語: NGワード (&W)";
			// 
			// lblMethod
			// 
			this.lblMethod.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblMethod.AutoSize = true;
			this.lblMethod.Location = new System.Drawing.Point(257, 6);
			this.lblMethod.Name = "lblMethod";
			this.lblMethod.Size = new System.Drawing.Size(102, 12);
			this.lblMethod.TabIndex = 230;
			this.lblMethod.Text = "述語: 比較方法 (&M)";
			// 
			// lblProperty
			// 
			this.lblProperty.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblProperty.AutoSize = true;
			this.lblProperty.Location = new System.Drawing.Point(130, 6);
			this.lblProperty.Name = "lblProperty";
			this.lblProperty.Size = new System.Drawing.Size(108, 12);
			this.lblProperty.TabIndex = 220;
			this.lblProperty.Text = "主語: プロパティ名 (&P)";
			// 
			// lblComment
			// 
			this.lblComment.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblComment.AutoSize = true;
			this.lblComment.Location = new System.Drawing.Point(3, 6);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(56, 12);
			this.lblComment.TabIndex = 210;
			this.lblComment.Text = "コメント (&C)";
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnAdd.Location = new System.Drawing.Point(511, 12);
			this.btnAdd.Name = "btnAdd";
			this.tableLayoutPanel2.SetRowSpan(this.btnAdd, 2);
			this.btnAdd.Size = new System.Drawing.Size(66, 23);
			this.btnAdd.TabIndex = 250;
			this.btnAdd.Text = "追加 (&A)";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// grpList
			// 
			this.grpList.Controls.Add(this.lvNgContents);
			this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpList.Location = new System.Drawing.Point(3, 3);
			this.grpList.Name = "grpList";
			this.grpList.Size = new System.Drawing.Size(586, 146);
			this.grpList.TabIndex = 100;
			this.grpList.TabStop = false;
			this.grpList.Text = "NGコンテンツ一覧 (Deleteキーで削除)";
			// 
			// NgContentsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 224);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(500, 150);
			this.Name = "NgContentsEditor";
			this.ShowInTaskbar = false;
			this.Text = "NgContentsEditor";
			this.Load += new System.EventHandler(this.NgContentsEditor_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.grpAdd.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.grpList.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lvNgContents;
		private System.Windows.Forms.ColumnHeader chComment;
		private System.Windows.Forms.ColumnHeader chPropertyName;
		private System.Windows.Forms.ColumnHeader chPredicate;
		private System.Windows.Forms.ColumnHeader chWord;
		private System.Windows.Forms.ColumnHeader chCreated;
		private System.Windows.Forms.ColumnHeader chLastAbone;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox grpAdd;
		private System.Windows.Forms.GroupBox grpList;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.Label lblProperty;
		private System.Windows.Forms.Label lblMethod;
		private System.Windows.Forms.Label lblWord;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.TextBox txtWord;
		private System.Windows.Forms.ComboBox comboProperty;
		private System.Windows.Forms.ComboBox comboMethod;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnAdd;
	}
}