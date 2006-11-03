namespace Yusen.GExplorer.OldApp {
	partial class OldMainForm {
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tspbCrawl = new System.Windows.Forms.ToolStripProgressBar();
			this.tsslCrawl = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.crawlResultView1 = new Yusen.GExplorer.OldApp.OldCrawlResultView();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiTools = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSearchCache = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNgFavContentsEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbCrawl,
            this.tsslCrawl});
			this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.statusStrip1.Location = new System.Drawing.Point(0, 0);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			this.statusStrip1.Size = new System.Drawing.Size(709, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tspbCrawl
			// 
			this.tspbCrawl.Name = "tspbCrawl";
			this.tspbCrawl.Size = new System.Drawing.Size(100, 16);
			// 
			// tsslCrawl
			// 
			this.tsslCrawl.Name = "tsslCrawl";
			this.tsslCrawl.Size = new System.Drawing.Size(53, 12);
			this.tsslCrawl.Text = "tsslCrawl";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.crawlResultView1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(709, 387);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(709, 429);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			// 
			// crawlResultView1
			// 
			this.crawlResultView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.crawlResultView1.Location = new System.Drawing.Point(0, 0);
			this.crawlResultView1.Margin = new System.Windows.Forms.Padding(0);
			this.crawlResultView1.Name = "crawlResultView1";
			this.crawlResultView1.Size = new System.Drawing.Size(709, 387);
			this.crawlResultView1.TabIndex = 1;
			// 
			// menuStrip1
			// 
			this.menuStrip1.AllowItemReorder = true;
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTools,
            this.tsmiSettings});
			this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(709, 20);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tsmiTools
			// 
			this.tsmiTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSearchCache});
			this.tsmiTools.Name = "tsmiTools";
			this.tsmiTools.Size = new System.Drawing.Size(61, 16);
			this.tsmiTools.Text = "ツール(&T)";
			// 
			// tsmiSearchCache
			// 
			this.tsmiSearchCache.Image = global::Yusen.GExplorer.Properties.Resources.SearchInFolder;
			this.tsmiSearchCache.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiSearchCache.Name = "tsmiSearchCache";
			this.tsmiSearchCache.Size = new System.Drawing.Size(209, 22);
			this.tsmiSearchCache.Text = "[DEL] キャッシュから検索(&F)...";
			// 
			// tsmiSettings
			// 
			this.tsmiSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNgFavContentsEditor});
			this.tsmiSettings.Name = "tsmiSettings";
			this.tsmiSettings.Size = new System.Drawing.Size(56, 16);
			this.tsmiSettings.Text = "設定(&S)";
			// 
			// tsmiNgFavContentsEditor
			// 
			this.tsmiNgFavContentsEditor.Name = "tsmiNgFavContentsEditor";
			this.tsmiNgFavContentsEditor.Size = new System.Drawing.Size(209, 22);
			this.tsmiNgFavContentsEditor.Text = "NG/FAVコンテンツエディタ(&N)";
			this.tsmiNgFavContentsEditor.Click += new System.EventHandler(this.tsmiNgFavContentsEditor_Click);
			// 
			// OldMainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(709, 429);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "OldMainForm";
			this.Text = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripProgressBar tspbCrawl;
		private System.Windows.Forms.ToolStripStatusLabel tsslCrawl;
		private OldCrawlResultView crawlResultView1;
		private System.Windows.Forms.ToolStripMenuItem tsmiTools;
		private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
		private System.Windows.Forms.ToolStripMenuItem tsmiNgFavContentsEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiSearchCache;


	}
}