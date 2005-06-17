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
			this.grpDetail = new System.Windows.Forms.GroupBox();
			this.btnInsert = new System.Windows.Forms.Button();
			this.btnModify = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.lblFile = new System.Windows.Forms.Label();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.lblArg = new System.Windows.Forms.Label();
			this.txtArg = new System.Windows.Forms.TextBox();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnSort = new System.Windows.Forms.Button();
			this.grpList = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.lboxCommands = new System.Windows.Forms.ListBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.grpDetail.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.grpList.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpDetail
			// 
			this.grpDetail.Controls.Add(this.btnInsert);
			this.grpDetail.Controls.Add(this.btnModify);
			this.grpDetail.Controls.Add(this.tableLayoutPanel1);
			this.grpDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpDetail.Location = new System.Drawing.Point(3, 169);
			this.grpDetail.Name = "grpDetail";
			this.grpDetail.Size = new System.Drawing.Size(345, 139);
			this.grpDetail.TabIndex = 200;
			this.grpDetail.TabStop = false;
			this.grpDetail.Text = "外部コマンドの挿入と変更 (&E)";
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(6, 110);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(75, 23);
			this.btnInsert.TabIndex = 221;
			this.btnInsert.Text = "挿入 (&I)";
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// btnModify
			// 
			this.btnModify.Location = new System.Drawing.Point(84, 110);
			this.btnModify.Name = "btnModify";
			this.btnModify.Size = new System.Drawing.Size(75, 23);
			this.btnModify.TabIndex = 222;
			this.btnModify.Text = "変更 (&M)";
			this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtTitle, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblFile, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtFile, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnBrowse, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblArg, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtArg, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 15);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(339, 90);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// lblTitle
			// 
			this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblTitle.AutoSize = true;
			this.lblTitle.Location = new System.Drawing.Point(29, 9);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(58, 12);
			this.lblTitle.TabIndex = 211;
			this.lblTitle.Text = "表示名 (&T)";
			// 
			// txtTitle
			// 
			this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTitle.Location = new System.Drawing.Point(93, 3);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(163, 19);
			this.txtTitle.TabIndex = 212;
			// 
			// lblFile
			// 
			this.lblFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblFile.AutoSize = true;
			this.lblFile.Location = new System.Drawing.Point(7, 39);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(80, 12);
			this.lblFile.TabIndex = 213;
			this.lblFile.Text = "実行ファイル (&F)";
			// 
			// txtFile
			// 
			this.txtFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtFile.Location = new System.Drawing.Point(93, 33);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(163, 19);
			this.txtFile.TabIndex = 214;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnBrowse.Location = new System.Drawing.Point(264, 33);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(69, 23);
			this.btnBrowse.TabIndex = 215;
			this.btnBrowse.Text = "参照 (&B)...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// lblArg
			// 
			this.lblArg.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblArg.AutoSize = true;
			this.lblArg.Location = new System.Drawing.Point(40, 69);
			this.lblArg.Name = "lblArg";
			this.lblArg.Size = new System.Drawing.Size(47, 12);
			this.lblArg.TabIndex = 216;
			this.lblArg.Text = "引数 (&A)";
			// 
			// txtArg
			// 
			this.txtArg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtArg.Location = new System.Drawing.Point(93, 63);
			this.txtArg.Name = "txtArg";
			this.txtArg.Size = new System.Drawing.Size(163, 19);
			this.txtArg.TabIndex = 217;
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point(3, 3);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(75, 23);
			this.btnUp.TabIndex = 121;
			this.btnUp.Text = "上へ (&U)";
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point(3, 32);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(75, 23);
			this.btnDown.TabIndex = 122;
			this.btnDown.Text = "下へ (&D)";
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(3, 70);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 123;
			this.btnDelete.Text = "削除 (&R)";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panel1.Controls.Add(this.btnUp);
			this.panel1.Controls.Add(this.btnDown);
			this.panel1.Controls.Add(this.btnDelete);
			this.panel1.Controls.Add(this.btnSort);
			this.panel1.Location = new System.Drawing.Point(254, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(82, 136);
			this.panel1.TabIndex = 6;
			// 
			// btnSort
			// 
			this.btnSort.Location = new System.Drawing.Point(3, 110);
			this.btnSort.Name = "btnSort";
			this.btnSort.Size = new System.Drawing.Size(75, 23);
			this.btnSort.TabIndex = 124;
			this.btnSort.Text = "ソート (&S)";
			this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
			// 
			// grpList
			// 
			this.grpList.Controls.Add(this.tableLayoutPanel2);
			this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpList.Location = new System.Drawing.Point(3, 3);
			this.grpList.Name = "grpList";
			this.grpList.Size = new System.Drawing.Size(345, 160);
			this.grpList.TabIndex = 100;
			this.grpList.TabStop = false;
			this.grpList.Text = "外部コマンド一覧 (&L)";
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
			this.tableLayoutPanel2.Size = new System.Drawing.Size(339, 142);
			this.tableLayoutPanel2.TabIndex = 7;
			// 
			// lboxCommands
			// 
			this.lboxCommands.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lboxCommands.FormattingEnabled = true;
			this.lboxCommands.ItemHeight = 12;
			this.lboxCommands.Location = new System.Drawing.Point(3, 3);
			this.lboxCommands.Name = "lboxCommands";
			this.lboxCommands.Size = new System.Drawing.Size(245, 136);
			this.lboxCommands.TabIndex = 110;
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
			this.tableLayoutPanel3.Size = new System.Drawing.Size(351, 311);
			this.tableLayoutPanel3.TabIndex = 8;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Filter = "実行ファイル (*.exe)|*.exe|全てのファイル|*.*";
			// 
			// UserCommandsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(351, 311);
			this.Controls.Add(this.tableLayoutPanel3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "UserCommandsEditor";
			this.Text = "UserCommandsEditor";
			this.grpDetail.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.grpList.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
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
		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.ListBox lboxCommands;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}