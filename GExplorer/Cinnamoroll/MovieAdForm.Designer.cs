namespace Yusen.GExplorer.Cinnamoroll {
	partial class MovieAdForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovieAdForm));
			this.lblCinnamon = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblCinnamon
			// 
			this.lblCinnamon.AutoSize = true;
			this.lblCinnamon.Font = new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblCinnamon.Location = new System.Drawing.Point(12, 9);
			this.lblCinnamon.Name = "lblCinnamon";
			this.lblCinnamon.Size = new System.Drawing.Size(476, 80);
			this.lblCinnamon.TabIndex = 2;
			this.lblCinnamon.Text = resources.GetString("lblCinnamon.Text");
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(13, 118);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(155, 12);
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.sanriomovie.com/";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(410, 107);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "&OK";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// MovieAdForm
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(497, 144);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.lblCinnamon);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MovieAdForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "2007”N12ŒŽ22“ú‚Í‰f‰æ‚ÌŒöŠJ“ú";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblCinnamon;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Button button1;
	}
}