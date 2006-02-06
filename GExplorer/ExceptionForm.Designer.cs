namespace Yusen.GExplorer {
	partial class ExceptionForm {
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblProdVer = new System.Windows.Forms.Label();
			this.lblClrVer = new System.Windows.Forms.Label();
			this.lblOsVer = new System.Windows.Forms.Label();
			this.lblExceptionType = new System.Windows.Forms.Label();
			this.lblExceptionMessage = new System.Windows.Forms.Label();
			this.lblStackTrace = new System.Windows.Forms.Label();
			this.lblInnerException = new System.Windows.Forms.Label();
			this.txtProdVer = new System.Windows.Forms.TextBox();
			this.txtClrVer = new System.Windows.Forms.TextBox();
			this.txtOsVer = new System.Windows.Forms.TextBox();
			this.txtExceptionType = new System.Windows.Forms.TextBox();
			this.txtExceptionMessage = new System.Windows.Forms.TextBox();
			this.txtStackTrace = new System.Windows.Forms.TextBox();
			this.llblInnerException = new System.Windows.Forms.LinkLabel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnContinue = new System.Windows.Forms.Button();
			this.btnAbort = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.lblProdVer, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblClrVer, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblOsVer, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.lblExceptionType, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.lblExceptionMessage, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.lblStackTrace, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.lblInnerException, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.txtProdVer, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtClrVer, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtOsVer, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtExceptionType, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.txtExceptionMessage, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.txtStackTrace, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.llblInnerException, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 7);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(344, 311);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// lblProdVer
			// 
			this.lblProdVer.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblProdVer.AutoSize = true;
			this.lblProdVer.Location = new System.Drawing.Point(3, 6);
			this.lblProdVer.Name = "lblProdVer";
			this.lblProdVer.Size = new System.Drawing.Size(74, 12);
			this.lblProdVer.TabIndex = 10;
			this.lblProdVer.Text = "製品バージョン";
			// 
			// lblClrVer
			// 
			this.lblClrVer.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblClrVer.AutoSize = true;
			this.lblClrVer.Location = new System.Drawing.Point(3, 30);
			this.lblClrVer.Name = "lblClrVer";
			this.lblClrVer.Size = new System.Drawing.Size(72, 12);
			this.lblClrVer.TabIndex = 12;
			this.lblClrVer.Text = "CLRバージョン";
			// 
			// lblOsVer
			// 
			this.lblOsVer.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblOsVer.AutoSize = true;
			this.lblOsVer.Location = new System.Drawing.Point(3, 54);
			this.lblOsVer.Name = "lblOsVer";
			this.lblOsVer.Size = new System.Drawing.Size(65, 12);
			this.lblOsVer.TabIndex = 14;
			this.lblOsVer.Text = "OSバージョン";
			// 
			// lblExceptionType
			// 
			this.lblExceptionType.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblExceptionType.AutoSize = true;
			this.lblExceptionType.Location = new System.Drawing.Point(3, 78);
			this.lblExceptionType.Name = "lblExceptionType";
			this.lblExceptionType.Size = new System.Drawing.Size(51, 12);
			this.lblExceptionType.TabIndex = 16;
			this.lblExceptionType.Text = "例外の型";
			// 
			// lblExceptionMessage
			// 
			this.lblExceptionMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblExceptionMessage.AutoSize = true;
			this.lblExceptionMessage.Location = new System.Drawing.Point(3, 102);
			this.lblExceptionMessage.Name = "lblExceptionMessage";
			this.lblExceptionMessage.Size = new System.Drawing.Size(84, 12);
			this.lblExceptionMessage.TabIndex = 18;
			this.lblExceptionMessage.Text = "例外のメッセージ";
			// 
			// lblStackTrace
			// 
			this.lblStackTrace.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblStackTrace.AutoSize = true;
			this.lblStackTrace.Location = new System.Drawing.Point(3, 181);
			this.lblStackTrace.Name = "lblStackTrace";
			this.lblStackTrace.Size = new System.Drawing.Size(73, 12);
			this.lblStackTrace.TabIndex = 19;
			this.lblStackTrace.Text = "スタックトレース";
			// 
			// lblInnerException
			// 
			this.lblInnerException.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblInnerException.AutoSize = true;
			this.lblInnerException.Location = new System.Drawing.Point(3, 259);
			this.lblInnerException.Name = "lblInnerException";
			this.lblInnerException.Size = new System.Drawing.Size(53, 12);
			this.lblInnerException.TabIndex = 21;
			this.lblInnerException.Text = "内部例外";
			// 
			// txtProdVer
			// 
			this.txtProdVer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtProdVer.Location = new System.Drawing.Point(93, 3);
			this.txtProdVer.Name = "txtProdVer";
			this.txtProdVer.ReadOnly = true;
			this.txtProdVer.Size = new System.Drawing.Size(248, 19);
			this.txtProdVer.TabIndex = 11;
			// 
			// txtClrVer
			// 
			this.txtClrVer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtClrVer.Location = new System.Drawing.Point(93, 27);
			this.txtClrVer.Name = "txtClrVer";
			this.txtClrVer.ReadOnly = true;
			this.txtClrVer.Size = new System.Drawing.Size(248, 19);
			this.txtClrVer.TabIndex = 13;
			// 
			// txtOsVer
			// 
			this.txtOsVer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOsVer.Location = new System.Drawing.Point(93, 51);
			this.txtOsVer.Name = "txtOsVer";
			this.txtOsVer.ReadOnly = true;
			this.txtOsVer.Size = new System.Drawing.Size(248, 19);
			this.txtOsVer.TabIndex = 15;
			// 
			// txtExceptionType
			// 
			this.txtExceptionType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtExceptionType.Location = new System.Drawing.Point(93, 75);
			this.txtExceptionType.Name = "txtExceptionType";
			this.txtExceptionType.ReadOnly = true;
			this.txtExceptionType.Size = new System.Drawing.Size(248, 19);
			this.txtExceptionType.TabIndex = 17;
			// 
			// txtExceptionMessage
			// 
			this.txtExceptionMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtExceptionMessage.Location = new System.Drawing.Point(93, 99);
			this.txtExceptionMessage.Name = "txtExceptionMessage";
			this.txtExceptionMessage.ReadOnly = true;
			this.txtExceptionMessage.Size = new System.Drawing.Size(248, 19);
			this.txtExceptionMessage.TabIndex = 18;
			// 
			// txtStackTrace
			// 
			this.txtStackTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtStackTrace.Location = new System.Drawing.Point(93, 123);
			this.txtStackTrace.Multiline = true;
			this.txtStackTrace.Name = "txtStackTrace";
			this.txtStackTrace.ReadOnly = true;
			this.txtStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtStackTrace.Size = new System.Drawing.Size(248, 129);
			this.txtStackTrace.TabIndex = 20;
			this.txtStackTrace.WordWrap = false;
			// 
			// llblInnerException
			// 
			this.llblInnerException.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.llblInnerException.AutoSize = true;
			this.llblInnerException.Enabled = false;
			this.llblInnerException.Location = new System.Drawing.Point(93, 259);
			this.llblInnerException.Name = "llblInnerException";
			this.llblInnerException.Size = new System.Drawing.Size(23, 12);
			this.llblInnerException.TabIndex = 22;
			this.llblInnerException.TabStop = true;
			this.llblInnerException.Text = "null";
			this.llblInnerException.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblInnerException_LinkClicked);
			// 
			// flowLayoutPanel1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
			this.flowLayoutPanel1.Controls.Add(this.btnContinue);
			this.flowLayoutPanel1.Controls.Add(this.btnAbort);
			this.flowLayoutPanel1.Controls.Add(this.btnCopy);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 278);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(338, 30);
			this.flowLayoutPanel1.TabIndex = 14;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// btnContinue
			// 
			this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.Ignore;
			this.btnContinue.Location = new System.Drawing.Point(3, 3);
			this.btnContinue.Name = "btnContinue";
			this.btnContinue.Size = new System.Drawing.Size(99, 23);
			this.btnContinue.TabIndex = 1;
			this.btnContinue.Text = "無視して続行(&C)";
			// 
			// btnAbort
			// 
			this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.btnAbort.Enabled = false;
			this.btnAbort.Location = new System.Drawing.Point(108, 3);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(88, 23);
			this.btnAbort.TabIndex = 2;
			this.btnAbort.Text = "強制終了(&A)";
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(202, 3);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(130, 23);
			this.btnCopy.TabIndex = 3;
			this.btnCopy.Text = "クリップボードにコピー(&L)";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// ExceptionForm
			// 
			this.AcceptButton = this.btnContinue;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnContinue;
			this.ClientSize = new System.Drawing.Size(344, 311);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExceptionForm";
			this.ShowInTaskbar = false;
			this.Text = "ExceptionForm";
			this.Shown += new System.EventHandler(this.ExceptionForm_Shown);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblProdVer;
		private System.Windows.Forms.Label lblClrVer;
		private System.Windows.Forms.Label lblOsVer;
		private System.Windows.Forms.Label lblExceptionType;
		private System.Windows.Forms.Label lblExceptionMessage;
		private System.Windows.Forms.Label lblStackTrace;
		private System.Windows.Forms.Label lblInnerException;
		private System.Windows.Forms.TextBox txtProdVer;
		private System.Windows.Forms.TextBox txtClrVer;
		private System.Windows.Forms.TextBox txtOsVer;
		private System.Windows.Forms.TextBox txtExceptionType;
		private System.Windows.Forms.TextBox txtExceptionMessage;
		private System.Windows.Forms.TextBox txtStackTrace;
		private System.Windows.Forms.LinkLabel llblInnerException;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnContinue;
		private System.Windows.Forms.Button btnAbort;
	}
}