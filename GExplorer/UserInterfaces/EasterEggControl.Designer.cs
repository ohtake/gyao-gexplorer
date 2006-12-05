namespace Yusen.GExplorer.UserInterfaces {
	partial class EasterEggControl {
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
			this.lblMessage = new System.Windows.Forms.Label();
			this.lblCinnamon = new System.Windows.Forms.Label();
			this.lblNotes = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(14, 105);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(90, 24);
			this.lblMessage.TabIndex = 3;
			this.lblMessage.Text = "ハッピーになってね\r\nシナモンがいるから";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblCinnamon
			// 
			this.lblCinnamon.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblCinnamon.AutoSize = true;
			this.lblCinnamon.Font = new System.Drawing.Font("MS PGothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.lblCinnamon.Location = new System.Drawing.Point(7, 45);
			this.lblCinnamon.Name = "lblCinnamon";
			this.lblCinnamon.Size = new System.Drawing.Size(104, 48);
			this.lblCinnamon.TabIndex = 2;
			this.lblCinnamon.Text = "　＿　　　 ＿\r\n（＿(,,・ω・)＿）\r\n　　 ＠＿）";
			// 
			// lblNotes
			// 
			this.lblNotes.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblNotes.AutoSize = true;
			this.lblNotes.Location = new System.Drawing.Point(0, 161);
			this.lblNotes.Name = "lblNotes";
			this.lblNotes.Size = new System.Drawing.Size(118, 48);
			this.lblNotes.TabIndex = 5;
			this.lblNotes.Text = "アプリケーション作成者と\r\nこのキャラクタの\r\n著作関係者とは\r\n一切関係はありません";
			// 
			// EasterEggControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblNotes);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.lblCinnamon);
			this.Name = "EasterEggControl";
			this.Size = new System.Drawing.Size(120, 264);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Label lblCinnamon;
		private System.Windows.Forms.Label lblNotes;
	}
}
