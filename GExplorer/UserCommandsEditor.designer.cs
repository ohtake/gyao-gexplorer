namespace Yusen.GExplorer {
	partial class UserCommandsEditor {
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
			this.grpDetail = new System.Windows.Forms.GroupBox();
			this.btnInsert = new System.Windows.Forms.Button();
			this.btnModify = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.lblFile = new System.Windows.Forms.Label();
			this.btnSeparator = new System.Windows.Forms.Button();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.lblArg = new System.Windows.Forms.Label();
			this.txtArg = new System.Windows.Forms.TextBox();
			this.btnArg = new System.Windows.Forms.Button();
			this.cmsArgs = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiLiterals = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCodepages = new System.Windows.Forms.ToolStripMenuItem();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.grpList = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.lboxCommands = new System.Windows.Forms.ListBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.cmsDummy = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tscapmiProperty = new Yusen.GExplorer.ToolStripCAPropertyMenuItem();
			this.grpDetail.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.cmsArgs.SuspendLayout();
			this.panel1.SuspendLayout();
			this.grpList.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.cmsDummy.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpDetail
			// 
			this.grpDetail.Controls.Add(this.btnInsert);
			this.grpDetail.Controls.Add(this.btnModify);
			this.grpDetail.Controls.Add(this.tableLayoutPanel1);
			this.grpDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpDetail.Location = new System.Drawing.Point(3, 152);
			this.grpDetail.Name = "grpDetail";
			this.grpDetail.Size = new System.Drawing.Size(346, 139);
			this.grpDetail.TabIndex = 1;
			this.grpDetail.TabStop = false;
			this.grpDetail.Text = "外部コマンドの挿入と変更(&E)";
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(6, 110);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(90, 23);
			this.btnInsert.TabIndex = 1;
			this.btnInsert.Text = "挿入(&I)";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// btnModify
			// 
			this.btnModify.Location = new System.Drawing.Point(102, 110);
			this.btnModify.Name = "btnModify";
			this.btnModify.Size = new System.Drawing.Size(90, 23);
			this.btnModify.TabIndex = 2;
			this.btnModify.Text = "変更(&M)";
			this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtTitle, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblFile, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnSeparator, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtFile, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnBrowse, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblArg, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtArg, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnArg, 2, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 15);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(340, 90);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblTitle.AutoSize = true;
			this.lblTitle.Location = new System.Drawing.Point(31, 9);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(56, 12);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "表示名(&T)";
			// 
			// txtTitle
			// 
			this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTitle.Location = new System.Drawing.Point(93, 3);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(144, 19);
			this.txtTitle.TabIndex = 1;
			// 
			// lblFile
			// 
			this.lblFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblFile.AutoSize = true;
			this.lblFile.Location = new System.Drawing.Point(9, 39);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(78, 12);
			this.lblFile.TabIndex = 3;
			this.lblFile.Text = "実行ファイル(&F)";
			// 
			// btnSeparator
			// 
			this.btnSeparator.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnSeparator.Location = new System.Drawing.Point(245, 3);
			this.btnSeparator.Name = "btnSeparator";
			this.btnSeparator.Size = new System.Drawing.Size(89, 23);
			this.btnSeparator.TabIndex = 2;
			this.btnSeparator.Text = "セパレータ(&S)";
			this.btnSeparator.UseVisualStyleBackColor = true;
			this.btnSeparator.Click += new System.EventHandler(this.btnSeparator_Click);
			// 
			// txtFile
			// 
			this.txtFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
			this.txtFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtFile.Location = new System.Drawing.Point(93, 33);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(144, 19);
			this.txtFile.TabIndex = 4;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnBrowse.Location = new System.Drawing.Point(245, 33);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(89, 23);
			this.btnBrowse.TabIndex = 5;
			this.btnBrowse.Text = "参照(&B)...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// lblArg
			// 
			this.lblArg.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblArg.AutoSize = true;
			this.lblArg.Location = new System.Drawing.Point(42, 69);
			this.lblArg.Name = "lblArg";
			this.lblArg.Size = new System.Drawing.Size(45, 12);
			this.lblArg.TabIndex = 6;
			this.lblArg.Text = "引数(&A)";
			// 
			// txtArg
			// 
			this.txtArg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtArg.Location = new System.Drawing.Point(93, 63);
			this.txtArg.Name = "txtArg";
			this.txtArg.Size = new System.Drawing.Size(144, 19);
			this.txtArg.TabIndex = 7;
			// 
			// btnArg
			// 
			this.btnArg.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnArg.Location = new System.Drawing.Point(245, 63);
			this.btnArg.Name = "btnArg";
			this.btnArg.Size = new System.Drawing.Size(89, 23);
			this.btnArg.TabIndex = 8;
			this.btnArg.Text = "入力補助(&U)>>";
			this.btnArg.Click += new System.EventHandler(this.btnArg_Click);
			// 
			// cmsArgs
			// 
			this.cmsArgs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.tsmiLiterals,
            this.toolStripMenuItem2,
            this.tsmiCodepages});
			this.cmsArgs.Name = "cmsArgs";
			this.cmsArgs.Size = new System.Drawing.Size(156, 60);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 6);
			// 
			// tsmiLiterals
			// 
			this.tsmiLiterals.Name = "tsmiLiterals";
			this.tsmiLiterals.Size = new System.Drawing.Size(155, 22);
			this.tsmiLiterals.Text = "リテラル文字(&L)";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 6);
			// 
			// tsmiCodepages
			// 
			this.tsmiCodepages.Name = "tsmiCodepages";
			this.tsmiCodepages.Size = new System.Drawing.Size(155, 22);
			this.tsmiCodepages.Text = "コードページ名(&C)";
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point(3, 3);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(75, 23);
			this.btnUp.TabIndex = 0;
			this.btnUp.Text = "上へ(&U)";
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point(3, 32);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(75, 23);
			this.btnDown.TabIndex = 1;
			this.btnDown.Text = "下へ(&D)";
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(3, 70);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "削除(&R)";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panel1.Controls.Add(this.btnUp);
			this.panel1.Controls.Add(this.btnDown);
			this.panel1.Controls.Add(this.btnDelete);
			this.panel1.Location = new System.Drawing.Point(255, 14);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(82, 96);
			this.panel1.TabIndex = 6;
			// 
			// grpList
			// 
			this.grpList.Controls.Add(this.tableLayoutPanel2);
			this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpList.Location = new System.Drawing.Point(3, 3);
			this.grpList.Name = "grpList";
			this.grpList.Size = new System.Drawing.Size(346, 143);
			this.grpList.TabIndex = 0;
			this.grpList.TabStop = false;
			this.grpList.Text = "外部コマンド一覧(&L)";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.lboxCommands, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 15);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(340, 125);
			this.tableLayoutPanel2.TabIndex = 7;
			// 
			// lboxCommands
			// 
			this.lboxCommands.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lboxCommands.FormattingEnabled = true;
			this.lboxCommands.ItemHeight = 12;
			this.lboxCommands.Location = new System.Drawing.Point(3, 3);
			this.lboxCommands.Name = "lboxCommands";
			this.lboxCommands.Size = new System.Drawing.Size(246, 112);
			this.lboxCommands.TabIndex = 0;
			this.lboxCommands.SelectedIndexChanged += new System.EventHandler(this.lboxCommands_SelectedIndexChanged);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.grpList, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.grpDetail, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(352, 294);
			this.tableLayoutPanel3.TabIndex = 8;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "実行ファイル (*.exe)|*.exe|全てのファイル|*.*";
			this.openFileDialog1.RestoreDirectory = true;
			// 
			// cmsDummy
			// 
			this.cmsDummy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscapmiProperty});
			this.cmsDummy.Name = "cmsDummy";
			this.cmsDummy.Size = new System.Drawing.Size(225, 26);
			// 
			// tscapmiProperty
			// 
			this.tscapmiProperty.Name = "tscapmiProperty";
			this.tscapmiProperty.Size = new System.Drawing.Size(224, 22);
			this.tscapmiProperty.Text = "ToolStripCAPropertyMenuItem";
			this.tscapmiProperty.PropertySelected += new System.EventHandler<Yusen.GExplorer.OldApp.CAPropertySelectedEventArgs>(this.tscapmiProperty_PropertySelected);
			// 
			// UserCommandsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 294);
			this.Controls.Add(this.tableLayoutPanel3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(360, 320);
			this.Name = "UserCommandsEditor";
			this.ShowInTaskbar = false;
			this.Text = "外部コマンドエディタ";
			this.Load += new System.EventHandler(this.UserCommandsEditor_Load);
			this.grpDetail.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.cmsArgs.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.grpList.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.cmsDummy.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpDetail;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.TextBox txtFile;
		private System.Windows.Forms.Button btnModify;
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label lblArg;
		private System.Windows.Forms.TextBox txtArg;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox grpList;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.ListBox lboxCommands;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnArg;
		private System.Windows.Forms.ContextMenuStrip cmsArgs;
		private System.Windows.Forms.ContextMenuStrip cmsDummy;
		private ToolStripCAPropertyMenuItem tscapmiProperty;
		private System.Windows.Forms.Button btnSeparator;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiLiterals;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiCodepages;
	}
}