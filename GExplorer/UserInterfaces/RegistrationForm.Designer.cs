namespace Yusen.GExplorer.UserInterfaces {
	partial class RegistrationForm {
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
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnOpenHttps = new System.Windows.Forms.Button();
			this.btnOpenHttp = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnQuit = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.webBrowser1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(842, 566);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// webBrowser1
			// 
			this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser1.Location = new System.Drawing.Point(0, 0);
			this.webBrowser1.Margin = new System.Windows.Forms.Padding(0);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(842, 532);
			this.webBrowser1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnOpenHttps);
			this.flowLayoutPanel1.Controls.Add(this.btnOpenHttp);
			this.flowLayoutPanel1.Controls.Add(this.btnClose);
			this.flowLayoutPanel1.Controls.Add(this.btnQuit);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 532);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(842, 34);
			this.flowLayoutPanel1.TabIndex = 2;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// btnOpenHttps
			// 
			this.btnOpenHttps.Location = new System.Drawing.Point(3, 3);
			this.btnOpenHttps.Name = "btnOpenHttps";
			this.btnOpenHttps.Size = new System.Drawing.Size(190, 23);
			this.btnOpenHttps.TabIndex = 3;
			this.btnOpenHttps.Text = "HTTPSで視聴設定ページを開く(&S)";
			this.btnOpenHttps.UseVisualStyleBackColor = true;
			this.btnOpenHttps.Click += new System.EventHandler(this.btnOpenHttps_Click);
			// 
			// btnOpenHttp
			// 
			this.btnOpenHttp.Location = new System.Drawing.Point(199, 3);
			this.btnOpenHttp.Name = "btnOpenHttp";
			this.btnOpenHttp.Size = new System.Drawing.Size(180, 23);
			this.btnOpenHttp.TabIndex = 4;
			this.btnOpenHttp.Text = "HTTPで視聴設定ページを開く(&H)";
			this.btnOpenHttp.UseVisualStyleBackColor = true;
			this.btnOpenHttp.Click += new System.EventHandler(this.btnOpenHttp_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(385, 3);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(242, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "視聴設定が終わったのでウィンドウを閉じる(&C)";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnQuit
			// 
			this.btnQuit.Location = new System.Drawing.Point(633, 3);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(165, 23);
			this.btnQuit.TabIndex = 2;
			this.btnQuit.Text = "アプリケーションを終了する(&Q)";
			this.btnQuit.UseVisualStyleBackColor = true;
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// RegistrationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(842, 566);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimizeBox = false;
			this.Name = "RegistrationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RegistrationForm";
			this.Load += new System.EventHandler(this.RegistrationForm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.Button btnOpenHttps;
		private System.Windows.Forms.Button btnOpenHttp;
	}
}