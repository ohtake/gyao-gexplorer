namespace Yusen.GExplorer.UserInterfaces {
	partial class ContentClassificationRuleEditForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.grpList = new System.Windows.Forms.GroupBox();
			this.lvRules = new Yusen.GExplorer.UserInterfaces.DoubleBufferedListView();
			this.chComment = new System.Windows.Forms.ColumnHeader();
			this.chSubject = new System.Windows.Forms.ColumnHeader();
			this.chPredicate = new System.Windows.Forms.ColumnHeader();
			this.chObject = new System.Windows.Forms.ColumnHeader();
			this.chDestination = new System.Windows.Forms.ColumnHeader();
			this.chCreated = new System.Windows.Forms.ColumnHeader();
			this.chLastApplied = new System.Windows.Forms.ColumnHeader();
			this.cmsList = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiRemoveRule = new System.Windows.Forms.ToolStripMenuItem();
			this.grpAdd = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.lblComment = new System.Windows.Forms.Label();
			this.lblSubject = new System.Windows.Forms.Label();
			this.lblPredicate = new System.Windows.Forms.Label();
			this.lblObject = new System.Windows.Forms.Label();
			this.lblDestination = new System.Windows.Forms.Label();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.cmbSubject = new System.Windows.Forms.ComboBox();
			this.cmbPredicate = new System.Windows.Forms.ComboBox();
			this.txtObject = new System.Windows.Forms.TextBox();
			this.cmbDestination = new System.Windows.Forms.ComboBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.timerSelectionDelay = new System.Windows.Forms.Timer(this.components);
			this.timerUpdateDelay = new System.Windows.Forms.Timer(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.grpList.SuspendLayout();
			this.cmsList.SuspendLayout();
			this.grpAdd.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.Controls.Add(this.grpList, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.grpAdd, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(632, 266);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// grpList
			// 
			this.grpList.Controls.Add(this.lvRules);
			this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpList.Location = new System.Drawing.Point(3, 3);
			this.grpList.Name = "grpList";
			this.grpList.Size = new System.Drawing.Size(626, 190);
			this.grpList.TabIndex = 0;
			this.grpList.TabStop = false;
			this.grpList.Text = "ルール一覧";
			// 
			// lvRules
			// 
			this.lvRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chComment,
            this.chSubject,
            this.chPredicate,
            this.chObject,
            this.chDestination,
            this.chCreated,
            this.chLastApplied});
			this.lvRules.ContextMenuStrip = this.cmsList;
			this.lvRules.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvRules.FullRowSelect = true;
			this.lvRules.HideSelection = false;
			this.lvRules.Location = new System.Drawing.Point(3, 15);
			this.lvRules.Name = "lvRules";
			this.lvRules.ShowItemToolTips = true;
			this.lvRules.Size = new System.Drawing.Size(620, 172);
			this.lvRules.TabIndex = 0;
			this.lvRules.UseCompatibleStateImageBehavior = false;
			this.lvRules.View = System.Windows.Forms.View.Details;
			this.lvRules.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvRules_ColumnClick);
			this.lvRules.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvRules_ItemSelectionChanged);
			// 
			// chComment
			// 
			this.chComment.Text = "コメント";
			this.chComment.Width = 70;
			// 
			// chSubject
			// 
			this.chSubject.Text = "主語";
			this.chSubject.Width = 90;
			// 
			// chPredicate
			// 
			this.chPredicate.Text = "述語";
			this.chPredicate.Width = 90;
			// 
			// chObject
			// 
			this.chObject.Text = "目的語";
			this.chObject.Width = 90;
			// 
			// chDestination
			// 
			this.chDestination.Text = "仕分け先";
			this.chDestination.Width = 80;
			// 
			// chCreated
			// 
			this.chCreated.Text = "作成日時";
			this.chCreated.Width = 80;
			// 
			// chLastApplied
			// 
			this.chLastApplied.Text = "最終適用日時";
			this.chLastApplied.Width = 80;
			// 
			// cmsList
			// 
			this.cmsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRemoveRule});
			this.cmsList.Name = "cmsList";
			this.cmsList.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.cmsList.Size = new System.Drawing.Size(136, 26);
			// 
			// tsmiRemoveRule
			// 
			this.tsmiRemoveRule.Image = global::Yusen.GExplorer.Properties.Resources.Delete;
			this.tsmiRemoveRule.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiRemoveRule.Name = "tsmiRemoveRule";
			this.tsmiRemoveRule.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiRemoveRule.Size = new System.Drawing.Size(135, 22);
			this.tsmiRemoveRule.Text = "削除(&D)";
			this.tsmiRemoveRule.Click += new System.EventHandler(this.tsmiRemoveRule_Click);
			// 
			// grpAdd
			// 
			this.grpAdd.Controls.Add(this.tableLayoutPanel2);
			this.grpAdd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpAdd.Location = new System.Drawing.Point(3, 199);
			this.grpAdd.Name = "grpAdd";
			this.grpAdd.Size = new System.Drawing.Size(626, 64);
			this.grpAdd.TabIndex = 1;
			this.grpAdd.TabStop = false;
			this.grpAdd.Text = "ルールの追加";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 6;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
			this.tableLayoutPanel2.Controls.Add(this.lblComment, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblSubject, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblPredicate, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblObject, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.lblDestination, 4, 0);
			this.tableLayoutPanel2.Controls.Add(this.txtComment, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.cmbSubject, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.cmbPredicate, 2, 1);
			this.tableLayoutPanel2.Controls.Add(this.txtObject, 3, 1);
			this.tableLayoutPanel2.Controls.Add(this.cmbDestination, 4, 1);
			this.tableLayoutPanel2.Controls.Add(this.btnAdd, 5, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 15);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(620, 46);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// lblComment
			// 
			this.lblComment.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblComment.AutoSize = true;
			this.lblComment.Location = new System.Drawing.Point(3, 5);
			this.lblComment.Name = "lblComment";
			this.lblComment.Size = new System.Drawing.Size(54, 12);
			this.lblComment.TabIndex = 0;
			this.lblComment.Text = "コメント(&C)";
			// 
			// lblSubject
			// 
			this.lblSubject.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblSubject.AutoSize = true;
			this.lblSubject.Location = new System.Drawing.Point(112, 5);
			this.lblSubject.Name = "lblSubject";
			this.lblSubject.Size = new System.Drawing.Size(44, 12);
			this.lblSubject.TabIndex = 1;
			this.lblSubject.Text = "主語(&S)";
			// 
			// lblPredicate
			// 
			this.lblPredicate.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblPredicate.AutoSize = true;
			this.lblPredicate.Location = new System.Drawing.Point(221, 5);
			this.lblPredicate.Name = "lblPredicate";
			this.lblPredicate.Size = new System.Drawing.Size(44, 12);
			this.lblPredicate.TabIndex = 2;
			this.lblPredicate.Text = "述語(&P)";
			// 
			// lblObject
			// 
			this.lblObject.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblObject.AutoSize = true;
			this.lblObject.Location = new System.Drawing.Point(330, 5);
			this.lblObject.Name = "lblObject";
			this.lblObject.Size = new System.Drawing.Size(57, 12);
			this.lblObject.TabIndex = 3;
			this.lblObject.Text = "目的語(&O)";
			// 
			// lblDestination
			// 
			this.lblDestination.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblDestination.AutoSize = true;
			this.lblDestination.Location = new System.Drawing.Point(439, 5);
			this.lblDestination.Name = "lblDestination";
			this.lblDestination.Size = new System.Drawing.Size(67, 12);
			this.lblDestination.TabIndex = 4;
			this.lblDestination.Text = "仕分け先(&D)";
			// 
			// txtComment
			// 
			this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtComment.Location = new System.Drawing.Point(3, 26);
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(103, 19);
			this.txtComment.TabIndex = 6;
			// 
			// cmbSubject
			// 
			this.cmbSubject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cmbSubject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbSubject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmbSubject.FormattingEnabled = true;
			this.cmbSubject.Location = new System.Drawing.Point(112, 26);
			this.cmbSubject.Name = "cmbSubject";
			this.cmbSubject.Size = new System.Drawing.Size(103, 20);
			this.cmbSubject.TabIndex = 7;
			// 
			// cmbPredicate
			// 
			this.cmbPredicate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cmbPredicate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbPredicate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmbPredicate.FormattingEnabled = true;
			this.cmbPredicate.Location = new System.Drawing.Point(221, 26);
			this.cmbPredicate.Name = "cmbPredicate";
			this.cmbPredicate.Size = new System.Drawing.Size(103, 20);
			this.cmbPredicate.TabIndex = 8;
			// 
			// txtObject
			// 
			this.txtObject.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtObject.Location = new System.Drawing.Point(330, 26);
			this.txtObject.Name = "txtObject";
			this.txtObject.Size = new System.Drawing.Size(103, 19);
			this.txtObject.TabIndex = 9;
			// 
			// cmbDestination
			// 
			this.cmbDestination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cmbDestination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbDestination.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmbDestination.FormattingEnabled = true;
			this.cmbDestination.Location = new System.Drawing.Point(439, 26);
			this.cmbDestination.Name = "cmbDestination";
			this.cmbDestination.Size = new System.Drawing.Size(103, 20);
			this.cmbDestination.TabIndex = 10;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnAdd.Location = new System.Drawing.Point(552, 11);
			this.btnAdd.Name = "btnAdd";
			this.tableLayoutPanel2.SetRowSpan(this.btnAdd, 2);
			this.btnAdd.Size = new System.Drawing.Size(60, 23);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "追加(&A)";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// timerSelectionDelay
			// 
			this.timerSelectionDelay.Interval = 50;
			this.timerSelectionDelay.Tick += new System.EventHandler(this.timerSelectionDelay_Tick);
			// 
			// timerUpdateDelay
			// 
			this.timerUpdateDelay.Tick += new System.EventHandler(this.timerUpdateDelay_Tick);
			// 
			// ContentClassificationRuleEditForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(632, 266);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ContentClassificationRuleEditForm";
			this.Text = "仕分けルールエディタ";
			this.Load += new System.EventHandler(this.ContentClassificationRuleEditForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.grpList.ResumeLayout(false);
			this.cmsList.ResumeLayout(false);
			this.grpAdd.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox grpList;
		private System.Windows.Forms.GroupBox grpAdd;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private DoubleBufferedListView lvRules;
		private System.Windows.Forms.ColumnHeader chComment;
		private System.Windows.Forms.ColumnHeader chSubject;
		private System.Windows.Forms.ColumnHeader chPredicate;
		private System.Windows.Forms.ColumnHeader chObject;
		private System.Windows.Forms.ColumnHeader chDestination;
		private System.Windows.Forms.ColumnHeader chCreated;
		private System.Windows.Forms.ColumnHeader chLastApplied;
		private System.Windows.Forms.Label lblComment;
		private System.Windows.Forms.Label lblSubject;
		private System.Windows.Forms.Label lblPredicate;
		private System.Windows.Forms.Label lblObject;
		private System.Windows.Forms.Label lblDestination;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.ComboBox cmbSubject;
		private System.Windows.Forms.ComboBox cmbPredicate;
		private System.Windows.Forms.TextBox txtObject;
		private System.Windows.Forms.ComboBox cmbDestination;
		private System.Windows.Forms.ContextMenuStrip cmsList;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveRule;
		private System.Windows.Forms.Timer timerSelectionDelay;
		private System.Windows.Forms.Timer timerUpdateDelay;
	}
}