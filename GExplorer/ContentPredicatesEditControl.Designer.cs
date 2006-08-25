namespace Yusen.GExplorer {
	partial class ContentPredicatesEditControl {
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.grpAdd = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.txtObject = new System.Windows.Forms.TextBox();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.comboPredicate = new System.Windows.Forms.ComboBox();
			this.comboSubject = new System.Windows.Forms.ComboBox();
			this.lblObject = new System.Windows.Forms.Label();
			this.lblPredicate = new System.Windows.Forms.Label();
			this.lblSubject = new System.Windows.Forms.Label();
			this.lblComment = new System.Windows.Forms.Label();
			this.btnAdd = new System.Windows.Forms.Button();
			this.grpList = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.listview1 = new Yusen.GExplorer.DoubleBufferedListView();
			this.chComment = new System.Windows.Forms.ColumnHeader();
			this.chSubject = new System.Windows.Forms.ColumnHeader();
			this.chPredicate = new System.Windows.Forms.ColumnHeader();
			this.chObject = new System.Windows.Forms.ColumnHeader();
			this.chCreated = new System.Windows.Forms.ColumnHeader();
			this.chLastTrue = new System.Windows.Forms.ColumnHeader();
			this.cmsPredicate = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.lblCount = new System.Windows.Forms.Label();
			this.timerItemSelect = new System.Windows.Forms.Timer(this.components);
			this.timerLastTrue = new System.Windows.Forms.Timer(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.grpAdd.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.grpList.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.cmsPredicate.SuspendLayout();
			this.SuspendLayout();
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
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 268);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// grpAdd
			// 
			this.grpAdd.Controls.Add(this.tableLayoutPanel2);
			this.grpAdd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpAdd.Location = new System.Drawing.Point(3, 199);
			this.grpAdd.Name = "grpAdd";
			this.grpAdd.Size = new System.Drawing.Size(594, 66);
			this.grpAdd.TabIndex = 200;
			this.grpAdd.TabStop = false;
			this.grpAdd.Text = "条件の追加";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 5;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
			this.tableLayoutPanel2.Controls.Add(this.txtObject, 3, 1);
			this.tableLayoutPanel2.Controls.Add(this.txtComment, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.comboPredicate, 2, 1);
			this.tableLayoutPanel2.Controls.Add(this.comboSubject, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.lblObject, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblPredicate, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblSubject, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblComment, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnAdd, 4, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 15);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(588, 48);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// txtObject
			// 
			this.txtObject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtObject.Location = new System.Drawing.Point(390, 27);
			this.txtObject.Name = "txtObject";
			this.txtObject.Size = new System.Drawing.Size(123, 19);
			this.txtObject.TabIndex = 241;
			// 
			// txtComment
			// 
			this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtComment.Location = new System.Drawing.Point(3, 27);
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(123, 19);
			this.txtComment.TabIndex = 211;
			// 
			// comboPredicate
			// 
			this.comboPredicate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.comboPredicate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboPredicate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboPredicate.FormattingEnabled = true;
			this.comboPredicate.Location = new System.Drawing.Point(261, 27);
			this.comboPredicate.Name = "comboPredicate";
			this.comboPredicate.Size = new System.Drawing.Size(123, 20);
			this.comboPredicate.TabIndex = 231;
			// 
			// comboSubject
			// 
			this.comboSubject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.comboSubject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.comboSubject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboSubject.FormattingEnabled = true;
			this.comboSubject.Location = new System.Drawing.Point(132, 27);
			this.comboSubject.Name = "comboSubject";
			this.comboSubject.Size = new System.Drawing.Size(123, 20);
			this.comboSubject.TabIndex = 221;
			// 
			// lblObject
			// 
			this.lblObject.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblObject.AutoSize = true;
			this.lblObject.Location = new System.Drawing.Point(390, 6);
			this.lblObject.Name = "lblObject";
			this.lblObject.Size = new System.Drawing.Size(111, 12);
			this.lblObject.TabIndex = 240;
			this.lblObject.Text = "目的語: 対象語句(&O)";
			// 
			// lblPredicate
			// 
			this.lblPredicate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblPredicate.AutoSize = true;
			this.lblPredicate.Location = new System.Drawing.Point(261, 6);
			this.lblPredicate.Name = "lblPredicate";
			this.lblPredicate.Size = new System.Drawing.Size(98, 12);
			this.lblPredicate.TabIndex = 230;
			this.lblPredicate.Text = "述語: 比較方法(&P)";
			// 
			// lblSubject
			// 
			this.lblSubject.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblSubject.AutoSize = true;
			this.lblSubject.Location = new System.Drawing.Point(132, 6);
			this.lblSubject.Name = "lblSubject";
			this.lblSubject.Size = new System.Drawing.Size(106, 12);
			this.lblSubject.TabIndex = 220;
			this.lblSubject.Text = "主語: プロパティ名(&S)";
			// 
			// lblComment
			// 
			this.lblComment.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblComment.AutoSize = true;
			this.lblComment.Location = new System.Drawing.Point(3, 6);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(54, 12);
			this.lblComment.TabIndex = 210;
			this.lblComment.Text = "コメント(&C)";
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnAdd.Location = new System.Drawing.Point(519, 12);
			this.btnAdd.Name = "btnAdd";
			this.tableLayoutPanel2.SetRowSpan(this.btnAdd, 2);
			this.btnAdd.Size = new System.Drawing.Size(66, 23);
			this.btnAdd.TabIndex = 250;
			this.btnAdd.Text = "追加(&A)";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// grpList
			// 
			this.grpList.Controls.Add(this.tableLayoutPanel3);
			this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpList.Location = new System.Drawing.Point(3, 3);
			this.grpList.Name = "grpList";
			this.grpList.Size = new System.Drawing.Size(594, 190);
			this.grpList.TabIndex = 100;
			this.grpList.TabStop = false;
			this.grpList.Text = "条件一覧";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.listview1, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.lblCount, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 15);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(588, 172);
			this.tableLayoutPanel3.TabIndex = 102;
			// 
			// listview1
			// 
			this.listview1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chComment,
            this.chSubject,
            this.chPredicate,
            this.chObject,
            this.chCreated,
            this.chLastTrue});
			this.listview1.ContextMenuStrip = this.cmsPredicate;
			this.listview1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listview1.FullRowSelect = true;
			this.listview1.HideSelection = false;
			this.listview1.Location = new System.Drawing.Point(0, 14);
			this.listview1.Margin = new System.Windows.Forms.Padding(0);
			this.listview1.Name = "listview1";
			this.listview1.ShowItemToolTips = true;
			this.listview1.Size = new System.Drawing.Size(588, 158);
			this.listview1.TabIndex = 101;
			this.listview1.UseCompatibleStateImageBehavior = false;
			this.listview1.View = System.Windows.Forms.View.Details;
			this.listview1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listview1_KeyDown);
			this.listview1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listview1_ColumnClick);
			this.listview1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listview1_ItemSelectionChanged);
			// 
			// chComment
			// 
			this.chComment.Text = "コメント";
			this.chComment.Width = 90;
			// 
			// chSubject
			// 
			this.chSubject.Text = "主語";
			this.chSubject.Width = 80;
			// 
			// chPredicate
			// 
			this.chPredicate.Text = "述語";
			this.chPredicate.Width = 65;
			// 
			// chObject
			// 
			this.chObject.Text = "目的語";
			this.chObject.Width = 90;
			// 
			// chCreated
			// 
			this.chCreated.Text = "作成日時";
			this.chCreated.Width = 116;
			// 
			// chLastTrue
			// 
			this.chLastTrue.Text = "最終True日時";
			this.chLastTrue.Width = 116;
			// 
			// cmsPredicate
			// 
			this.cmsPredicate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDelete});
			this.cmsPredicate.Name = "cmsPredicate";
			this.cmsPredicate.Size = new System.Drawing.Size(153, 48);
			// 
			// tsmiDelete
			// 
			this.tsmiDelete.Image = global::Yusen.GExplorer.Properties.Resources.Delete;
			this.tsmiDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiDelete.Name = "tsmiDelete";
			this.tsmiDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiDelete.Size = new System.Drawing.Size(152, 22);
			this.tsmiDelete.Text = "削除(&D)";
			this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
			// 
			// lblCount
			// 
			this.lblCount.AutoSize = true;
			this.lblCount.Location = new System.Drawing.Point(3, 0);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(47, 12);
			this.lblCount.TabIndex = 102;
			this.lblCount.Text = "lblCount";
			// 
			// timerItemSelect
			// 
			this.timerItemSelect.Interval = 10;
			this.timerItemSelect.Tick += new System.EventHandler(this.timerItemSelect_Tick);
			// 
			// timerLastTrue
			// 
			this.timerLastTrue.Tick += new System.EventHandler(this.timerLastTrue_Tick);
			// 
			// ContentPredicatesEditControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ContentPredicatesEditControl";
			this.Size = new System.Drawing.Size(600, 268);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.grpAdd.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.grpList.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.cmsPredicate.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox grpAdd;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TextBox txtObject;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.ComboBox comboPredicate;
		private System.Windows.Forms.ComboBox comboSubject;
		private System.Windows.Forms.Label lblObject;
		private System.Windows.Forms.Label lblPredicate;
		private System.Windows.Forms.Label lblSubject;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.GroupBox grpList;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private DoubleBufferedListView listview1;
		private System.Windows.Forms.ColumnHeader chComment;
		private System.Windows.Forms.ColumnHeader chSubject;
		private System.Windows.Forms.ColumnHeader chPredicate;
		private System.Windows.Forms.ColumnHeader chObject;
		private System.Windows.Forms.ColumnHeader chCreated;
		private System.Windows.Forms.ColumnHeader chLastTrue;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Timer timerItemSelect;
		private System.Windows.Forms.Timer timerLastTrue;
		private System.Windows.Forms.ContextMenuStrip cmsPredicate;
		private System.Windows.Forms.ToolStripMenuItem tsmiDelete;

	}
}
