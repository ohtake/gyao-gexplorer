namespace Yusen.GExplorer {
	partial class DetailForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPlayer = new System.Windows.Forms.TabPage();
			this.wmpMain = new AxWMPLib.AxWindowsMediaPlayer();
			this.tabDescription = new System.Windows.Forms.TabPage();
			this.ieMain = new System.Windows.Forms.WebBrowser();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.lblDocument = new System.Windows.Forms.Label();
			this.txtDocument = new System.Windows.Forms.TextBox();
			this.lblPlayList = new System.Windows.Forms.Label();
			this.lblMediaFile = new System.Windows.Forms.Label();
			this.txtMediaFile = new System.Windows.Forms.TextBox();
			this.txtPlayList = new System.Windows.Forms.TextBox();
			this.chkAutoVolume = new System.Windows.Forms.CheckBox();
			this.tabControl1.SuspendLayout();
			this.tabPlayer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.wmpMain)).BeginInit();
			this.tabDescription.SuspendLayout();
			this.tabMisc.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPlayer);
			this.tabControl1.Controls.Add(this.tabDescription);
			this.tabControl1.Controls.Add(this.tabMisc);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(662, 586);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPlayer
			// 
			this.tabPlayer.Controls.Add(this.wmpMain);
			this.tabPlayer.Location = new System.Drawing.Point(4, 21);
			this.tabPlayer.Name = "tabPlayer";
			this.tabPlayer.Padding = new System.Windows.Forms.Padding(3);
			this.tabPlayer.Size = new System.Drawing.Size(654, 561);
			this.tabPlayer.TabIndex = 0;
			this.tabPlayer.Text = "プレーヤ";
			// 
			// wmpMain
			// 
			this.wmpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wmpMain.Enabled = true;
			this.wmpMain.Location = new System.Drawing.Point(3, 3);
			this.wmpMain.Name = "wmpMain";
			this.wmpMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpMain.OcxState")));
			this.wmpMain.Size = new System.Drawing.Size(648, 555);
			this.wmpMain.Text = "axWindowsMediaPlayer1";
			// 
			// tabDescription
			// 
			this.tabDescription.Controls.Add(this.ieMain);
			this.tabDescription.Location = new System.Drawing.Point(4, 21);
			this.tabDescription.Name = "tabDescription";
			this.tabDescription.Padding = new System.Windows.Forms.Padding(3);
			this.tabDescription.Size = new System.Drawing.Size(654, 561);
			this.tabDescription.TabIndex = 1;
			this.tabDescription.Text = "解説ページ";
			// 
			// ieMain
			// 
			this.ieMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ieMain.Location = new System.Drawing.Point(3, 3);
			this.ieMain.Name = "ieMain";
			this.ieMain.Size = new System.Drawing.Size(648, 555);
			// 
			// tabMisc
			// 
			this.tabMisc.Controls.Add(this.lblDocument);
			this.tabMisc.Controls.Add(this.txtDocument);
			this.tabMisc.Controls.Add(this.lblPlayList);
			this.tabMisc.Controls.Add(this.lblMediaFile);
			this.tabMisc.Controls.Add(this.txtMediaFile);
			this.tabMisc.Controls.Add(this.txtPlayList);
			this.tabMisc.Controls.Add(this.chkAutoVolume);
			this.tabMisc.Location = new System.Drawing.Point(4, 21);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Size = new System.Drawing.Size(654, 561);
			this.tabMisc.TabIndex = 2;
			this.tabMisc.Text = "その他";
			// 
			// lblDocument
			// 
			this.lblDocument.AutoSize = true;
			this.lblDocument.Location = new System.Drawing.Point(20, 94);
			this.lblDocument.Name = "lblDocument";
			this.lblDocument.Size = new System.Drawing.Size(104, 12);
			this.lblDocument.TabIndex = 6;
			this.lblDocument.Text = "本来の再生ウィンドウ";
			// 
			// txtDocument
			// 
			this.txtDocument.Location = new System.Drawing.Point(130, 91);
			this.txtDocument.Name = "txtDocument";
			this.txtDocument.ReadOnly = true;
			this.txtDocument.Size = new System.Drawing.Size(501, 19);
			this.txtDocument.TabIndex = 5;
			// 
			// lblPlayList
			// 
			this.lblPlayList.AutoSize = true;
			this.lblPlayList.Location = new System.Drawing.Point(20, 119);
			this.lblPlayList.Name = "lblPlayList";
			this.lblPlayList.Size = new System.Drawing.Size(54, 12);
			this.lblPlayList.TabIndex = 4;
			this.lblPlayList.Text = "プレイリスト";
			// 
			// lblMediaFile
			// 
			this.lblMediaFile.AutoSize = true;
			this.lblMediaFile.Location = new System.Drawing.Point(20, 144);
			this.lblMediaFile.Name = "lblMediaFile";
			this.lblMediaFile.Size = new System.Drawing.Size(71, 12);
			this.lblMediaFile.TabIndex = 3;
			this.lblMediaFile.Text = "メディアファイル";
			// 
			// txtMediaFile
			// 
			this.txtMediaFile.Location = new System.Drawing.Point(130, 141);
			this.txtMediaFile.Name = "txtMediaFile";
			this.txtMediaFile.ReadOnly = true;
			this.txtMediaFile.Size = new System.Drawing.Size(501, 19);
			this.txtMediaFile.TabIndex = 2;
			// 
			// txtPlayList
			// 
			this.txtPlayList.Location = new System.Drawing.Point(130, 116);
			this.txtPlayList.Name = "txtPlayList";
			this.txtPlayList.ReadOnly = true;
			this.txtPlayList.Size = new System.Drawing.Size(501, 19);
			this.txtPlayList.TabIndex = 1;
			// 
			// chkAutoVolume
			// 
			this.chkAutoVolume.AutoSize = true;
			this.chkAutoVolume.Checked = true;
			this.chkAutoVolume.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoVolume.Location = new System.Drawing.Point(24, 24);
			this.chkAutoVolume.Name = "chkAutoVolume";
			this.chkAutoVolume.Size = new System.Drawing.Size(319, 16);
			this.chkAutoVolume.TabIndex = 0;
			this.chkAutoVolume.Text = "CMと思わしき動画の音量を10にして，そうじゃないのを100にする";
			// 
			// DetailForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(662, 586);
			this.Controls.Add(this.tabControl1);
			this.Name = "DetailForm";
			this.Text = "DetailForm";
			this.tabControl1.ResumeLayout(false);
			this.tabPlayer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.wmpMain)).EndInit();
			this.tabDescription.ResumeLayout(false);
			this.tabMisc.ResumeLayout(false);
			this.tabMisc.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPlayer;
		private System.Windows.Forms.TabPage tabDescription;
		private AxWMPLib.AxWindowsMediaPlayer wmpMain;
		private System.Windows.Forms.WebBrowser ieMain;
		private System.Windows.Forms.TabPage tabMisc;
		private System.Windows.Forms.CheckBox chkAutoVolume;
		private System.Windows.Forms.TextBox txtMediaFile;
		private System.Windows.Forms.TextBox txtPlayList;
		private System.Windows.Forms.Label lblPlayList;
		private System.Windows.Forms.Label lblMediaFile;
		private System.Windows.Forms.Label lblDocument;
		private System.Windows.Forms.TextBox txtDocument;
	}
}