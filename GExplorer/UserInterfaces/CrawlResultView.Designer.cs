namespace Yusen.GExplorer.UserInterfaces {
	partial class CrawlResultView {
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
			this.tsddbDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiAddToThePlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.tspmiAddToAnotherPlaylist = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tscpmiCopyOtherProperties = new Yusen.GExplorer.UserInterfaces.ToolStripContentPropertyMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsecmiCommand = new Yusen.GExplorer.UserInterfaces.ToolStripExternalCommandMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbLine1 = new System.Windows.Forms.ToolStripButton();
			this.tsbLine2 = new System.Windows.Forms.ToolStripButton();
			this.tsbLine4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tslDestPlaylistName = new System.Windows.Forms.ToolStripLabel();
			this.tscbDestPlaylistName = new System.Windows.Forms.ToolStripComboBox();
			this.tslTime = new System.Windows.Forms.ToolStripLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.lboxCrv = new Yusen.GExplorer.UserInterfaces.DoubleBufferedListBox();
			this.cmsResult = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiCmsAddToThePlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.tspmiCmsAddToAnotherPlaylist = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			this.tsmiCmsPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCmsCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsCopyNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tscpmiCmsCopyOtherProperties = new Yusen.GExplorer.UserInterfaces.ToolStripContentPropertyMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsecmiCmsCommand = new Yusen.GExplorer.UserInterfaces.ToolStripExternalCommandMenuItem();
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
            this.tsbLine1,
            this.tsbLine2,
            this.tsbLine4,
            this.toolStripSeparator2,
            this.tslDestPlaylistName,
            this.tscbDestPlaylistName,
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
			// tsddbDropDown
			// 
			this.tsddbDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddToThePlaylist,
            this.tspmiAddToAnotherPlaylist,
            this.tsmiPlay,
            this.toolStripMenuItem3,
            this.tsmiCopyName,
            this.tsmiCopyUri,
            this.tsmiCopyNameAndUri,
            this.tscpmiCopyOtherProperties,
            this.toolStripMenuItem4,
            this.tsecmiCommand});
			this.tsddbDropDown.Name = "tsddbDropDown";
			this.tsddbDropDown.Size = new System.Drawing.Size(42, 22);
			this.tsddbDropDown.Text = "CRV";
			this.tsddbDropDown.Visible = false;
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
			// tspmiAddToAnotherPlaylist
			// 
			this.tspmiAddToAnotherPlaylist.Name = "tspmiAddToAnotherPlaylist";
			this.tspmiAddToAnotherPlaylist.Size = new System.Drawing.Size(212, 22);
			this.tspmiAddToAnotherPlaylist.Text = "他のプレイリストに追加(&D)";
			this.tspmiAddToAnotherPlaylist.PlaylistSelected += new System.EventHandler(this.tspmiAddToAnotherPlaylist_PlaylistSelected);
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
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(209, 6);
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.Size = new System.Drawing.Size(212, 22);
			this.tsmiCopyName.Text = "名前をコピー(&N)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyUri
			// 
			this.tsmiCopyUri.Name = "tsmiCopyUri";
			this.tsmiCopyUri.Size = new System.Drawing.Size(212, 22);
			this.tsmiCopyUri.Text = "URIをコピー(&U)";
			this.tsmiCopyUri.Click += new System.EventHandler(this.tsmiCopyUri_Click);
			// 
			// tsmiCopyNameAndUri
			// 
			this.tsmiCopyNameAndUri.Image = global::Yusen.GExplorer.Properties.Resources.Copy;
			this.tsmiCopyNameAndUri.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCopyNameAndUri.Name = "tsmiCopyNameAndUri";
			this.tsmiCopyNameAndUri.Size = new System.Drawing.Size(212, 22);
			this.tsmiCopyNameAndUri.Text = "名前とURIをコピー(&B)";
			this.tsmiCopyNameAndUri.Click += new System.EventHandler(this.tsmiCopyNameAndUri_Click);
			// 
			// tscpmiCopyOtherProperties
			// 
			this.tscpmiCopyOtherProperties.Name = "tscpmiCopyOtherProperties";
			this.tscpmiCopyOtherProperties.Size = new System.Drawing.Size(212, 22);
			this.tscpmiCopyOtherProperties.Text = "その他のコピー(&O)";
			this.tscpmiCopyOtherProperties.PropertySelected += new System.EventHandler(this.tscpmiCopyOtherProperties_PropertySelected);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(209, 6);
			// 
			// tsecmiCommand
			// 
			this.tsecmiCommand.Name = "tsecmiCommand";
			this.tsecmiCommand.Size = new System.Drawing.Size(212, 22);
			this.tsecmiCommand.Text = "外部コマンド(&X)";
			this.tsecmiCommand.ExternalCommandSelected += new System.EventHandler(this.tsecmiCommand_ExternalCommandSelected);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbLine1
			// 
			this.tsbLine1.AutoToolTip = false;
			this.tsbLine1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbLine1.Name = "tsbLine1";
			this.tsbLine1.Size = new System.Drawing.Size(27, 22);
			this.tsbLine1.Text = "1行";
			this.tsbLine1.Click += new System.EventHandler(this.tsbLine1_Click);
			// 
			// tsbLine2
			// 
			this.tsbLine2.AutoToolTip = false;
			this.tsbLine2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbLine2.Name = "tsbLine2";
			this.tsbLine2.Size = new System.Drawing.Size(27, 22);
			this.tsbLine2.Text = "2行";
			this.tsbLine2.Click += new System.EventHandler(this.tsbLine2_Click);
			// 
			// tsbLine4
			// 
			this.tsbLine4.AutoToolTip = false;
			this.tsbLine4.Checked = true;
			this.tsbLine4.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsbLine4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbLine4.Name = "tsbLine4";
			this.tsbLine4.Size = new System.Drawing.Size(27, 22);
			this.tsbLine4.Text = "4行";
			this.tsbLine4.Click += new System.EventHandler(this.tsbLine4_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tslDestPlaylistName
			// 
			this.tslDestPlaylistName.Name = "tslDestPlaylistName";
			this.tslDestPlaylistName.Size = new System.Drawing.Size(41, 22);
			this.tslDestPlaylistName.Text = "追加先";
			// 
			// tscbDestPlaylistName
			// 
			this.tscbDestPlaylistName.Name = "tscbDestPlaylistName";
			this.tscbDestPlaylistName.Size = new System.Drawing.Size(100, 25);
			this.tscbDestPlaylistName.Text = "My Playlist 1";
			this.tscbDestPlaylistName.DropDown += new System.EventHandler(this.tscbDestPlaylistName_DropDown);
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
			// cmsResult
			// 
			this.cmsResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCmsAddToThePlaylist,
            this.tspmiCmsAddToAnotherPlaylist,
            this.tsmiCmsPlay,
            this.toolStripMenuItem1,
            this.tsmiCmsCopyName,
            this.tsmiCmsCopyUri,
            this.tsmiCmsCopyNameAndUri,
            this.tscpmiCmsCopyOtherProperties,
            this.toolStripMenuItem2,
            this.tsecmiCmsCommand});
			this.cmsResult.Name = "cmsResult";
			this.cmsResult.Size = new System.Drawing.Size(213, 192);
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
			// tspmiCmsAddToAnotherPlaylist
			// 
			this.tspmiCmsAddToAnotherPlaylist.Name = "tspmiCmsAddToAnotherPlaylist";
			this.tspmiCmsAddToAnotherPlaylist.Size = new System.Drawing.Size(212, 22);
			this.tspmiCmsAddToAnotherPlaylist.Text = "他のプレイリストに追加(&D)";
			this.tspmiCmsAddToAnotherPlaylist.PlaylistSelected += new System.EventHandler(this.tspmiCmsAddToAnotherPlaylist_PlaylistSelected);
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
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(209, 6);
			// 
			// tsmiCmsCopyName
			// 
			this.tsmiCmsCopyName.Name = "tsmiCmsCopyName";
			this.tsmiCmsCopyName.Size = new System.Drawing.Size(212, 22);
			this.tsmiCmsCopyName.Text = "名前をコピー(&N)";
			this.tsmiCmsCopyName.Click += new System.EventHandler(this.tsmiCmsCopyName_Click);
			// 
			// tsmiCmsCopyUri
			// 
			this.tsmiCmsCopyUri.Name = "tsmiCmsCopyUri";
			this.tsmiCmsCopyUri.Size = new System.Drawing.Size(212, 22);
			this.tsmiCmsCopyUri.Text = "URIをコピー(&U)";
			this.tsmiCmsCopyUri.Click += new System.EventHandler(this.tsmiCmsCopyUri_Click);
			// 
			// tsmiCmsCopyNameAndUri
			// 
			this.tsmiCmsCopyNameAndUri.Image = global::Yusen.GExplorer.Properties.Resources.Copy;
			this.tsmiCmsCopyNameAndUri.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsCopyNameAndUri.Name = "tsmiCmsCopyNameAndUri";
			this.tsmiCmsCopyNameAndUri.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsmiCmsCopyNameAndUri.Size = new System.Drawing.Size(212, 22);
			this.tsmiCmsCopyNameAndUri.Text = "名前とURIをコピー(&B)";
			this.tsmiCmsCopyNameAndUri.Click += new System.EventHandler(this.tsmiCmsCopyNameAndUri_Click);
			// 
			// tscpmiCmsCopyOtherProperties
			// 
			this.tscpmiCmsCopyOtherProperties.Name = "tscpmiCmsCopyOtherProperties";
			this.tscpmiCmsCopyOtherProperties.Size = new System.Drawing.Size(212, 22);
			this.tscpmiCmsCopyOtherProperties.Text = "その他をコピー(&O)";
			this.tscpmiCmsCopyOtherProperties.PropertySelected += new System.EventHandler(this.tscpmiCmsCopyOtherProperties_PropertySelected);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(209, 6);
			// 
			// tsecmiCmsCommand
			// 
			this.tsecmiCmsCommand.Name = "tsecmiCmsCommand";
			this.tsecmiCmsCommand.Size = new System.Drawing.Size(212, 22);
			this.tsecmiCmsCommand.Text = "外部コマンド(&X)";
			this.tsecmiCmsCommand.ExternalCommandSelected += new System.EventHandler(this.tsecmiCmsCommand_ExternalCommandSelected);
			// 
			// CrawlResultView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "CrawlResultView";
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
		private System.Windows.Forms.ContextMenuStrip cmsResult;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsAddToThePlaylist;
		private ToolStripPlaylistMenuItem tspmiCmsAddToAnotherPlaylist;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsPlay;
		private System.Windows.Forms.ToolStripDropDownButton tsddbDropDown;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddToThePlaylist;
		private ToolStripPlaylistMenuItem tspmiAddToAnotherPlaylist;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
		private System.Windows.Forms.ToolStripButton tsbLine1;
		private System.Windows.Forms.ToolStripButton tsbLine2;
		private System.Windows.Forms.ToolStripButton tsbLine4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCopyNameAndUri;
		private ToolStripContentPropertyMenuItem tscpmiCmsCopyOtherProperties;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private ToolStripExternalCommandMenuItem tsecmiCmsCommand;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyNameAndUri;
		private ToolStripContentPropertyMenuItem tscpmiCopyOtherProperties;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private ToolStripExternalCommandMenuItem tsecmiCommand;
	}
}
