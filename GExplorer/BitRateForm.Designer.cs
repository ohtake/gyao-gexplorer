namespace Yusen.GExplorer {
	partial class BitRateForm {
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
			this.rdoSuperFine = new System.Windows.Forms.RadioButton();
			this.rdoStandard = new System.Windows.Forms.RadioButton();
			this.chkSkipNextTime = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// rdoSuperFine
			// 
			this.rdoSuperFine.AutoSize = true;
			this.rdoSuperFine.Location = new System.Drawing.Point(27, 41);
			this.rdoSuperFine.Name = "rdoSuperFine";
			this.rdoSuperFine.Size = new System.Drawing.Size(85, 16);
			this.rdoSuperFine.TabIndex = 0;
			this.rdoSuperFine.Text = "768 kbps(&H)";
			this.rdoSuperFine.UseVisualStyleBackColor = true;
			// 
			// rdoStandard
			// 
			this.rdoStandard.AutoSize = true;
			this.rdoStandard.Location = new System.Drawing.Point(27, 63);
			this.rdoStandard.Name = "rdoStandard";
			this.rdoStandard.Size = new System.Drawing.Size(83, 16);
			this.rdoStandard.TabIndex = 1;
			this.rdoStandard.Text = "384 kbps(&L)";
			this.rdoStandard.UseVisualStyleBackColor = true;
			// 
			// chkSkipNextTime
			// 
			this.chkSkipNextTime.AutoSize = true;
			this.chkSkipNextTime.Checked = true;
			this.chkSkipNextTime.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSkipNextTime.Location = new System.Drawing.Point(12, 85);
			this.chkSkipNextTime.Name = "chkSkipNextTime";
			this.chkSkipNextTime.Size = new System.Drawing.Size(169, 16);
			this.chkSkipNextTime.TabIndex = 2;
			this.chkSkipNextTime.Text = "次回起動からは表示しない(&D)";
			this.chkSkipNextTime.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(187, 78);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(238, 24);
			this.label1.TabIndex = 4;
			this.label1.Text = "ビットレートを選択してください．\r\nヒント: グローバル設定で変更することも可能です．";
			// 
			// BitRateForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(268, 108);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.chkSkipNextTime);
			this.Controls.Add(this.rdoStandard);
			this.Controls.Add(this.rdoSuperFine);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BitRateForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BitRateForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton rdoSuperFine;
		private System.Windows.Forms.RadioButton rdoStandard;
		private System.Windows.Forms.CheckBox chkSkipNextTime;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label1;
	}
}