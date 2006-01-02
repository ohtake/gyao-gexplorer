namespace Yusen.GExplorer {
	partial class SplashForm {
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.progbTotal = new System.Windows.Forms.ProgressBar();
			this.btnAbort = new System.Windows.Forms.Button();
			this.lblMessage = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.Controls.Add(this.txtLog, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.progbTotal, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnAbort, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.lblMessage, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(257, 131);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// txtLog
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.txtLog, 2);
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Location = new System.Drawing.Point(3, 23);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(251, 77);
			this.txtLog.TabIndex = 0;
			// 
			// progbTotal
			// 
			this.progbTotal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progbTotal.Location = new System.Drawing.Point(3, 106);
			this.progbTotal.Name = "progbTotal";
			this.progbTotal.Size = new System.Drawing.Size(151, 22);
			this.progbTotal.TabIndex = 1;
			// 
			// btnAbort
			// 
			this.btnAbort.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnAbort.Location = new System.Drawing.Point(166, 106);
			this.btnAbort.Name = "btnAbort";
			this.btnAbort.Size = new System.Drawing.Size(82, 22);
			this.btnAbort.TabIndex = 2;
			this.btnAbort.Text = "ã≠êßèIóπ(&A)";
			this.btnAbort.UseVisualStyleBackColor = true;
			this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblMessage.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.lblMessage, 2);
			this.lblMessage.Location = new System.Drawing.Point(3, 4);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(62, 12);
			this.lblMessage.TabIndex = 3;
			this.lblMessage.Text = "lblMessage";
			// 
			// SplashForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(257, 131);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplashForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SplashForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SplashForm_FormClosing);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.ProgressBar progbTotal;
		private System.Windows.Forms.Button btnAbort;
		private System.Windows.Forms.Label lblMessage;
	}
}