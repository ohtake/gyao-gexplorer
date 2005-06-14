namespace Yusen.GExplorer {
	partial class MainForm {
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
			this.statstripMain = new System.Windows.Forms.StatusStrip();
			this.tspbPackages = new System.Windows.Forms.ToolStripProgressBar();
			this.tsslCategoryStat = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.genreViewer1 = new Yusen.GExplorer.GenreViewer();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.statstripMain.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statstripMain
			// 
			this.statstripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbPackages,
            this.tsslCategoryStat});
			this.statstripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
			this.statstripMain.Location = new System.Drawing.Point(0, 393);
			this.statstripMain.Name = "statstripMain";
			this.statstripMain.Size = new System.Drawing.Size(592, 23);
			this.statstripMain.TabIndex = 4;
			// 
			// tspbPackages
			// 
			this.tspbPackages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
			this.tspbPackages.Name = "tspbPackages";
			this.tspbPackages.Size = new System.Drawing.Size(100, 16);
			this.tspbPackages.Text = "toolStripProgressBar1";
			// 
			// tsslCategoryStat
			// 
			this.tsslCategoryStat.Name = "tsslCategoryStat";
			this.tsslCategoryStat.Text = "tsslListViewItemsCount";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.genreViewer1);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(592, 393);
			this.toolStripContainer1.TabIndex = 5;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// genreViewer1
			// 
			this.genreViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.genreViewer1.Location = new System.Drawing.Point(0, 0);
			this.genreViewer1.Name = "genreViewer1";
			this.genreViewer1.Size = new System.Drawing.Size(592, 369);
			this.genreViewer1.TabIndex = 0;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip1.Size = new System.Drawing.Size(592, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// ファイルFToolStripMenuItem
			// 
			this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiQuit});
			this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
			this.ファイルFToolStripMenuItem.Text = "ファイル (&F)";
			// 
			// tsmiQuit
			// 
			this.tsmiQuit.Name = "tsmiQuit";
			this.tsmiQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.tsmiQuit.Text = "終了 (&Q)";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(592, 416);
			this.Controls.Add(this.toolStripContainer1);
			this.Controls.Add(this.statstripMain);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.statstripMain.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statstripMain;
		private System.Windows.Forms.ToolStripStatusLabel tsslCategoryStat;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
		private System.Windows.Forms.ToolStripProgressBar tspbPackages;
		private GenreViewer genreViewer1;
	}
}

