namespace Yusen.GExplorer.UserInterfaces {
	partial class CrawlResultView2 {
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
			this.components = new System.ComponentModel.Container();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tslTitle = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tslDestPlaylistName = new System.Windows.Forms.ToolStripLabel();
			this.tscbDestPlaylistName = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tslTime = new System.Windows.Forms.ToolStripLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.cmsResult = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiCmsAddToThePlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAddToThePlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.tsddbDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.lboxCrv = new Yusen.GExplorer.UserInterfaces.DoubleBufferedListBox();
			this.tspmiAddToAnotherPlaylist = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			this.tspmiCmsAddToAnotherPlaylist = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			this.toolStrip1.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.cmsResult.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslTitle,
            this.tsddbDropDown,
            this.toolStripSeparator1,
            this.tslDestPlaylistName,
            this.tscbDestPlaylistName,
            this.toolStripLabel1,
            this.tslTime});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.toolStrip1.Size = new System.Drawing.Size(600, 25);
			this.toolStrip1.Stretch = true;
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tslTitle
			// 
			this.tslTitle.Name = "tslTitle";
			this.tslTitle.Size = new System.Drawing.Size(66, 22);
			this.tslTitle.Text = "クロール結果";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tslDestPlaylistName
			// 
			this.tslDestPlaylistName.Name = "tslDestPlaylistName";
			this.tslDestPlaylistName.Size = new System.Drawing.Size(114, 22);
			this.tslDestPlaylistName.Text = "追加先のプレイリスト名";
			// 
			// tscbDestPlaylistName
			// 
			this.tscbDestPlaylistName.Name = "tscbDestPlaylistName";
			this.tscbDestPlaylistName.Size = new System.Drawing.Size(100, 25);
			this.tscbDestPlaylistName.Text = "My Playlist 1";
			this.tscbDestPlaylistName.DropDown += new System.EventHandler(this.tscbDestPlaylistName_DropDown);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(90, 22);
			this.toolStripLabel1.Text = "P12/23 C23/123";
			// 
			// tslTime
			// 
			this.tslTime.Name = "tslTime";
			this.tslTime.Size = new System.Drawing.Size(85, 22);
			this.tslTime.Text = "12/23(月) 12:34";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.lboxCrv);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(600, 325);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(600, 350);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// cmsResult
			// 
			this.cmsResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCmsAddToThePlaylist,
            this.tspmiCmsAddToAnotherPlaylist,
            this.tsmiCmsPlay});
			this.cmsResult.Name = "cmsResult";
			this.cmsResult.Size = new System.Drawing.Size(213, 70);
			// 
			// tsmiCmsAddToThePlaylist
			// 
			this.tsmiCmsAddToThePlaylist.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_add;
			this.tsmiCmsAddToThePlaylist.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsAddToThePlaylist.Name = "tsmiCmsAddToThePlaylist";
			this.tsmiCmsAddToThePlaylist.Size = new System.Drawing.Size(212, 22);
			this.tsmiCmsAddToThePlaylist.Text = "指定したプレイリストに追加(&A)";
			this.tsmiCmsAddToThePlaylist.Click += new System.EventHandler(this.tsmiCmsAddToThePlaylist_Click);
			// 
			// tsmiCmsPlay
			// 
			this.tsmiCmsPlay.Image = global::Yusen.GExplorer.Properties.Resources.Play;
			this.tsmiCmsPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsPlay.Name = "tsmiCmsPlay";
			this.tsmiCmsPlay.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.tsmiCmsPlay.Size = new System.Drawing.Size(212, 22);
			this.tsmiCmsPlay.Text = "追加せずに再生(&P)";
			this.tsmiCmsPlay.Click += new System.EventHandler(this.tsmiCmsPlay_Click);
			// 
			// tsmiAddToThePlaylist
			// 
			this.tsmiAddToThePlaylist.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_add;
			this.tsmiAddToThePlaylist.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiAddToThePlaylist.Name = "tsmiAddToThePlaylist";
			this.tsmiAddToThePlaylist.Size = new System.Drawing.Size(212, 22);
			this.tsmiAddToThePlaylist.Text = "指定したプレイリストに追加(&A)";
			this.tsmiAddToThePlaylist.Click += new System.EventHandler(this.tsmiAddToThePlaylist_Click);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Image = global::Yusen.GExplorer.Properties.Resources.Play;
			this.tsmiPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.Size = new System.Drawing.Size(212, 22);
			this.tsmiPlay.Text = "追加せずに再生(&P)";
			this.tsmiPlay.Click += new System.EventHandler(this.tsmiPlay_Click);
			// 
			// tsddbDropDown
			// 
			this.tsddbDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddToThePlaylist,
            this.tspmiAddToAnotherPlaylist,
            this.tsmiPlay});
			this.tsddbDropDown.Name = "tsddbDropDown";
			this.tsddbDropDown.Size = new System.Drawing.Size(42, 22);
			this.tsddbDropDown.Text = "CRV";
			this.tsddbDropDown.Visible = false;
			// 
			// lboxCrv
			// 
			this.lboxCrv.ContextMenuStrip = this.cmsResult;
			this.lboxCrv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lboxCrv.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lboxCrv.ItemHeight = 62;
			this.lboxCrv.Location = new System.Drawing.Point(0, 0);
			this.lboxCrv.Name = "lboxCrv";
			this.lboxCrv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lboxCrv.Size = new System.Drawing.Size(600, 314);
			this.lboxCrv.TabIndex = 0;
			this.lboxCrv.Leave += new System.EventHandler(this.lboxCrv_Leave);
			this.lboxCrv.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lboxCrv_DrawItem);
			this.lboxCrv.DoubleClick += new System.EventHandler(this.lboxCrv_DoubleClick);
			this.lboxCrv.Enter += new System.EventHandler(this.lboxCrv_Enter);
			this.lboxCrv.SelectedIndexChanged += new System.EventHandler(this.lboxCrv_SelectedIndexChanged);
			this.lboxCrv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lboxCrv_KeyDown);
			// 
			// tspmiAddToAnotherPlaylist
			// 
			this.tspmiAddToAnotherPlaylist.Name = "tspmiAddToAnotherPlaylist";
			this.tspmiAddToAnotherPlaylist.Size = new System.Drawing.Size(212, 22);
			this.tspmiAddToAnotherPlaylist.Text = "他のプレイリストに追加(&D)";
			this.tspmiAddToAnotherPlaylist.PlaylistSelected += new System.EventHandler(this.tspmiAddToAnotherPlaylist_PlaylistSelected);
			// 
			// tspmiCmsAddToAnotherPlaylist
			// 
			this.tspmiCmsAddToAnotherPlaylist.Name = "tspmiCmsAddToAnotherPlaylist";
			this.tspmiCmsAddToAnotherPlaylist.Size = new System.Drawing.Size(212, 22);
			this.tspmiCmsAddToAnotherPlaylist.Text = "他のプレイリストに追加(&D)";
			this.tspmiCmsAddToAnotherPlaylist.PlaylistSelected += new System.EventHandler(this.tspmiCmsAddToAnotherPlaylist_PlaylistSelected);
			// 
			// CrawlResultView2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "CrawlResultView2";
			this.Size = new System.Drawing.Size(600, 350);
			this.Load += new System.EventHandler(this.CrawlResultView_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.cmsResult.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStripLabel tslTitle;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private Yusen.GExplorer.UserInterfaces.DoubleBufferedListBox lboxCrv;
		private System.Windows.Forms.ToolStripLabel tslDestPlaylistName;
		private System.Windows.Forms.ToolStripComboBox tscbDestPlaylistName;
		private System.Windows.Forms.ToolStripLabel tslTime;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ContextMenuStrip cmsResult;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsAddToThePlaylist;
		private ToolStripPlaylistMenuItem tspmiCmsAddToAnotherPlaylist;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsPlay;
		private System.Windows.Forms.ToolStripDropDownButton tsddbDropDown;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddToThePlaylist;
		private ToolStripPlaylistMenuItem tspmiAddToAnotherPlaylist;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
	}
}
