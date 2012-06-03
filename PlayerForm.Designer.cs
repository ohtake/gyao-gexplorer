namespace Yusen.GExplorer {
	partial class PlayerForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPlayer = new System.Windows.Forms.TabPage();
			this.wmpMain = new AxWMPLib.AxWindowsMediaPlayer();
			this.tabDescription = new System.Windows.Forms.TabPage();
			this.ieMain = new System.Windows.Forms.WebBrowser();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFullscreen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTool = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAutoVolume = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1.SuspendLayout();
			this.tabPlayer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.wmpMain)).BeginInit();
			this.tabDescription.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPlayer);
			this.tabControl1.Controls.Add(this.tabDescription);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(662, 582);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPlayer
			// 
			this.tabPlayer.Controls.Add(this.wmpMain);
			this.tabPlayer.Location = new System.Drawing.Point(4, 21);
			this.tabPlayer.Name = "tabPlayer";
			this.tabPlayer.Padding = new System.Windows.Forms.Padding(3);
			this.tabPlayer.Size = new System.Drawing.Size(654, 557);
			this.tabPlayer.TabIndex = 0;
			this.tabPlayer.Text = "�v���[��";
			// 
			// wmpMain
			// 
			this.wmpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wmpMain.Enabled = true;
			this.wmpMain.Location = new System.Drawing.Point(3, 3);
			this.wmpMain.Name = "wmpMain";
			this.wmpMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpMain.OcxState")));
			this.wmpMain.Size = new System.Drawing.Size(648, 551);
			this.wmpMain.Text = "axWindowsMediaPlayer1";
			// 
			// tabDescription
			// 
			this.tabDescription.Controls.Add(this.ieMain);
			this.tabDescription.Location = new System.Drawing.Point(4, 21);
			this.tabDescription.Name = "tabDescription";
			this.tabDescription.Padding = new System.Windows.Forms.Padding(3);
			this.tabDescription.Size = new System.Drawing.Size(654, 557);
			this.tabDescription.TabIndex = 1;
			this.tabDescription.Text = "����y�[�W";
			// 
			// ieMain
			// 
			this.ieMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ieMain.Location = new System.Drawing.Point(3, 3);
			this.ieMain.Name = "ieMain";
			this.ieMain.Size = new System.Drawing.Size(648, 551);
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl1);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(662, 606);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiView,
            this.tsmiTool});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(662, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClose});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Text = "�t�@�C�� (&F)";
			// 
			// tsmiClose
			// 
			this.tsmiClose.Name = "tsmiClose";
			this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.tsmiClose.Text = "���� (&C)";
			// 
			// tsmiView
			// 
			this.tsmiView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAlwaysOnTop,
            this.tsmiFullscreen});
			this.tsmiView.Name = "tsmiView";
			this.tsmiView.Text = "�\�� (&V)";
			// 
			// tsmiAlwaysOnTop
			// 
			this.tsmiAlwaysOnTop.CheckOnClick = true;
			this.tsmiAlwaysOnTop.Name = "tsmiAlwaysOnTop";
			this.tsmiAlwaysOnTop.Text = "��Ɏ�O�ɕ\�� (&T)";
			// 
			// tsmiFullscreen
			// 
			this.tsmiFullscreen.Name = "tsmiFullscreen";
			this.tsmiFullscreen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Return)));
			this.tsmiFullscreen.Text = "�S��� (&F)";
			// 
			// tsmiTool
			// 
			this.tsmiTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAutoVolume});
			this.tsmiTool.Name = "tsmiTool";
			this.tsmiTool.Text = "�c�[�� (&T)";
			// 
			// tsmiAutoVolume
			// 
			this.tsmiAutoVolume.Checked = true;
			this.tsmiAutoVolume.CheckOnClick = true;
			this.tsmiAutoVolume.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiAutoVolume.Name = "tsmiAutoVolume";
			this.tsmiAutoVolume.Text = "CM�̉��ʂ����������� (&V)";
			// 
			// PlayerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(662, 606);
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "PlayerForm";
			this.Text = "DetailForm";
			this.tabControl1.ResumeLayout(false);
			this.tabPlayer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.wmpMain)).EndInit();
			this.tabDescription.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPlayer;
		private System.Windows.Forms.TabPage tabDescription;
		private AxWMPLib.AxWindowsMediaPlayer wmpMain;
		private System.Windows.Forms.WebBrowser ieMain;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;
		private System.Windows.Forms.ToolStripMenuItem tsmiView;
		private System.Windows.Forms.ToolStripMenuItem tsmiAlwaysOnTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiFullscreen;
		private System.Windows.Forms.ToolStripMenuItem tsmiTool;
		private System.Windows.Forms.ToolStripMenuItem tsmiAutoVolume;
	}
}