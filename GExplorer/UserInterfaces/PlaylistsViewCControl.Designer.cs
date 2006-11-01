namespace Yusen.GExplorer.UserInterfaces {
	partial class PlaylistsViewCControl {
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
			this.cmsPlaylists = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiCmsViewPlaylistContents = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCmsPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCmsRenamePlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsRearrange = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsRearrangeToTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsRearrangeUp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsRearrangeDown = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsRearrangeToBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.pVCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiViewContents = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRename = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRearrange = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRearrangeToTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRearrangeUp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRearrangeDown = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRearrangeToBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSort = new System.Windows.Forms.ToolStripMenuItem();
			this.lvPlaylists = new Yusen.GExplorer.UserInterfaces.DoubleBufferedListView();
			this.chName = new System.Windows.Forms.ColumnHeader();
			this.chCount = new System.Windows.Forms.ColumnHeader();
			this.chTime = new System.Windows.Forms.ColumnHeader();
			this.chCreated = new System.Windows.Forms.ColumnHeader();
			this.chModified = new System.Windows.Forms.ColumnHeader();
			this.cmsPlaylists.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsPlaylists
			// 
			this.cmsPlaylists.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCmsViewPlaylistContents,
            this.toolStripMenuItem1,
            this.tsmiCmsPlay,
            this.toolStripMenuItem2,
            this.tsmiCmsRenamePlaylist,
            this.tsmiCmsRearrange,
            this.tsmiCmsDelete});
			this.cmsPlaylists.Name = "cmsPlaylists";
			this.cmsPlaylists.Size = new System.Drawing.Size(201, 126);
			// 
			// tsmiCmsViewPlaylistContents
			// 
			this.tsmiCmsViewPlaylistContents.Name = "tsmiCmsViewPlaylistContents";
			this.tsmiCmsViewPlaylistContents.Size = new System.Drawing.Size(200, 22);
			this.tsmiCmsViewPlaylistContents.Text = "内容を表示(&V)";
			this.tsmiCmsViewPlaylistContents.Click += new System.EventHandler(this.tsmiCmsViewPlaylistContents_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(197, 6);
			// 
			// tsmiCmsPlay
			// 
			this.tsmiCmsPlay.Image = global::Yusen.GExplorer.Properties.Resources.Play;
			this.tsmiCmsPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsPlay.Name = "tsmiCmsPlay";
			this.tsmiCmsPlay.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.tsmiCmsPlay.Size = new System.Drawing.Size(200, 22);
			this.tsmiCmsPlay.Text = "再生(&P)";
			this.tsmiCmsPlay.Click += new System.EventHandler(this.tsmiCmsPlay_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(197, 6);
			// 
			// tsmiCmsRenamePlaylist
			// 
			this.tsmiCmsRenamePlaylist.Name = "tsmiCmsRenamePlaylist";
			this.tsmiCmsRenamePlaylist.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.tsmiCmsRenamePlaylist.Size = new System.Drawing.Size(200, 22);
			this.tsmiCmsRenamePlaylist.Text = "プレイリスト名を変更(&N)";
			this.tsmiCmsRenamePlaylist.Click += new System.EventHandler(this.tsmiCmsRenamePlaylist_Click);
			// 
			// tsmiCmsRearrange
			// 
			this.tsmiCmsRearrange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCmsRearrangeToTop,
            this.tsmiCmsRearrangeUp,
            this.tsmiCmsRearrangeDown,
            this.tsmiCmsRearrangeToBottom});
			this.tsmiCmsRearrange.Name = "tsmiCmsRearrange";
			this.tsmiCmsRearrange.Size = new System.Drawing.Size(200, 22);
			this.tsmiCmsRearrange.Text = "コレクション内を移動(&A)";
			// 
			// tsmiCmsRearrangeToTop
			// 
			this.tsmiCmsRearrangeToTop.Name = "tsmiCmsRearrangeToTop";
			this.tsmiCmsRearrangeToTop.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
						| System.Windows.Forms.Keys.Up)));
			this.tsmiCmsRearrangeToTop.Size = new System.Drawing.Size(211, 22);
			this.tsmiCmsRearrangeToTop.Text = "最上部へ(&T)";
			this.tsmiCmsRearrangeToTop.Click += new System.EventHandler(this.tsmiCmsRearrangeToTop_Click);
			// 
			// tsmiCmsRearrangeUp
			// 
			this.tsmiCmsRearrangeUp.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_moveup;
			this.tsmiCmsRearrangeUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsRearrangeUp.Name = "tsmiCmsRearrangeUp";
			this.tsmiCmsRearrangeUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
			this.tsmiCmsRearrangeUp.Size = new System.Drawing.Size(211, 22);
			this.tsmiCmsRearrangeUp.Text = "上へ(&U)";
			this.tsmiCmsRearrangeUp.Click += new System.EventHandler(this.tsmiCmsRearrangeUp_Click);
			// 
			// tsmiCmsRearrangeDown
			// 
			this.tsmiCmsRearrangeDown.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_movedown;
			this.tsmiCmsRearrangeDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsRearrangeDown.Name = "tsmiCmsRearrangeDown";
			this.tsmiCmsRearrangeDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
			this.tsmiCmsRearrangeDown.Size = new System.Drawing.Size(211, 22);
			this.tsmiCmsRearrangeDown.Text = "下へ(&D)";
			this.tsmiCmsRearrangeDown.Click += new System.EventHandler(this.tsmiCmsRearrangeDown_Click);
			// 
			// tsmiCmsRearrangeToBottom
			// 
			this.tsmiCmsRearrangeToBottom.Name = "tsmiCmsRearrangeToBottom";
			this.tsmiCmsRearrangeToBottom.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
						| System.Windows.Forms.Keys.Down)));
			this.tsmiCmsRearrangeToBottom.Size = new System.Drawing.Size(211, 22);
			this.tsmiCmsRearrangeToBottom.Text = "最下部へ(&B)";
			this.tsmiCmsRearrangeToBottom.Click += new System.EventHandler(this.tsmiCmsRearrangeToBottom_Click);
			// 
			// tsmiCmsDelete
			// 
			this.tsmiCmsDelete.Image = global::Yusen.GExplorer.Properties.Resources.Delete;
			this.tsmiCmsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsDelete.Name = "tsmiCmsDelete";
			this.tsmiCmsDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiCmsDelete.Size = new System.Drawing.Size(200, 22);
			this.tsmiCmsDelete.Text = "削除(&D)";
			this.tsmiCmsDelete.Click += new System.EventHandler(this.tsmiCmsDelete_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pVCToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip1.Size = new System.Drawing.Size(368, 24);
			this.menuStrip1.Stretch = false;
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			// 
			// pVCToolStripMenuItem
			// 
			this.pVCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSort,
            this.toolStripMenuItem5,
            this.tsmiViewContents,
            this.toolStripMenuItem3,
            this.tsmiPlay,
            this.toolStripMenuItem4,
            this.tsmiRename,
            this.tsmiRearrange,
            this.tsmiDelete});
			this.pVCToolStripMenuItem.Name = "pVCToolStripMenuItem";
			this.pVCToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.pVCToolStripMenuItem.Text = "PVC";
			// 
			// tsmiViewContents
			// 
			this.tsmiViewContents.Name = "tsmiViewContents";
			this.tsmiViewContents.Size = new System.Drawing.Size(182, 22);
			this.tsmiViewContents.Text = "内容を表示(&V)";
			this.tsmiViewContents.Click += new System.EventHandler(this.tsmiViewContents_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(179, 6);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Image = global::Yusen.GExplorer.Properties.Resources.Play;
			this.tsmiPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.Size = new System.Drawing.Size(182, 22);
			this.tsmiPlay.Text = "再生(&P)";
			this.tsmiPlay.Click += new System.EventHandler(this.tsmiPlay_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(179, 6);
			// 
			// tsmiRename
			// 
			this.tsmiRename.Name = "tsmiRename";
			this.tsmiRename.Size = new System.Drawing.Size(182, 22);
			this.tsmiRename.Text = "プレイリスト名を変更(&N)";
			this.tsmiRename.Click += new System.EventHandler(this.tsmiRename_Click);
			// 
			// tsmiRearrange
			// 
			this.tsmiRearrange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRearrangeToTop,
            this.tsmiRearrangeUp,
            this.tsmiRearrangeDown,
            this.tsmiRearrangeToBottom});
			this.tsmiRearrange.Name = "tsmiRearrange";
			this.tsmiRearrange.Size = new System.Drawing.Size(182, 22);
			this.tsmiRearrange.Text = "コレクション内を移動(&R)";
			// 
			// tsmiRearrangeToTop
			// 
			this.tsmiRearrangeToTop.Name = "tsmiRearrangeToTop";
			this.tsmiRearrangeToTop.Size = new System.Drawing.Size(132, 22);
			this.tsmiRearrangeToTop.Text = "最上部へ(&T)";
			this.tsmiRearrangeToTop.Click += new System.EventHandler(this.tsmiRearrangeToTop_Click);
			// 
			// tsmiRearrangeUp
			// 
			this.tsmiRearrangeUp.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_moveup;
			this.tsmiRearrangeUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiRearrangeUp.Name = "tsmiRearrangeUp";
			this.tsmiRearrangeUp.Size = new System.Drawing.Size(132, 22);
			this.tsmiRearrangeUp.Text = "上へ(&U)";
			this.tsmiRearrangeUp.Click += new System.EventHandler(this.tsmiRearrangeUp_Click);
			// 
			// tsmiRearrangeDown
			// 
			this.tsmiRearrangeDown.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_movedown;
			this.tsmiRearrangeDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiRearrangeDown.Name = "tsmiRearrangeDown";
			this.tsmiRearrangeDown.Size = new System.Drawing.Size(132, 22);
			this.tsmiRearrangeDown.Text = "下へ(&D)";
			this.tsmiRearrangeDown.Click += new System.EventHandler(this.tsmiRearrangeDown_Click);
			// 
			// tsmiRearrangeToBottom
			// 
			this.tsmiRearrangeToBottom.Name = "tsmiRearrangeToBottom";
			this.tsmiRearrangeToBottom.Size = new System.Drawing.Size(132, 22);
			this.tsmiRearrangeToBottom.Text = "最下部へ(&B)";
			this.tsmiRearrangeToBottom.Click += new System.EventHandler(this.tsmiRearrangeToBottom_Click);
			// 
			// tsmiDelete
			// 
			this.tsmiDelete.Image = global::Yusen.GExplorer.Properties.Resources.Delete;
			this.tsmiDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiDelete.Name = "tsmiDelete";
			this.tsmiDelete.Size = new System.Drawing.Size(182, 22);
			this.tsmiDelete.Text = "削除(&D)";
			this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(179, 6);
			// 
			// tsmiSort
			// 
			this.tsmiSort.Name = "tsmiSort";
			this.tsmiSort.Size = new System.Drawing.Size(182, 22);
			this.tsmiSort.Text = "ソート(&S)";
			// 
			// lvPlaylists
			// 
			this.lvPlaylists.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chCount,
            this.chTime,
            this.chCreated,
            this.chModified});
			this.lvPlaylists.ContextMenuStrip = this.cmsPlaylists;
			this.lvPlaylists.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPlaylists.FullRowSelect = true;
			this.lvPlaylists.HideSelection = false;
			this.lvPlaylists.LabelEdit = true;
			this.lvPlaylists.Location = new System.Drawing.Point(0, 0);
			this.lvPlaylists.Name = "lvPlaylists";
			this.lvPlaylists.ShowGroups = false;
			this.lvPlaylists.ShowItemToolTips = true;
			this.lvPlaylists.Size = new System.Drawing.Size(368, 181);
			this.lvPlaylists.TabIndex = 0;
			this.lvPlaylists.UseCompatibleStateImageBehavior = false;
			this.lvPlaylists.View = System.Windows.Forms.View.Details;
			this.lvPlaylists.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvPlaylists_ColumnWidthChanged);
			this.lvPlaylists.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPlaylists_MouseDoubleClick);
			this.lvPlaylists.SelectedIndexChanged += new System.EventHandler(this.lvPlaylists_SelectedIndexChanged);
			this.lvPlaylists.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvPlaylists_KeyDown);
			this.lvPlaylists.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvPlaylists_ColumnClick);
			this.lvPlaylists.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvPlaylists_AfterLabelEdit);
			this.lvPlaylists.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvPlaylists_BeforeLabelEdit);
			// 
			// chName
			// 
			this.chName.Text = "プレイリスト名";
			this.chName.Width = 80;
			// 
			// chCount
			// 
			this.chCount.Text = "数";
			this.chCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.chCount.Width = 24;
			// 
			// chTime
			// 
			this.chTime.Text = "時間";
			this.chTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.chTime.Width = 52;
			// 
			// chCreated
			// 
			this.chCreated.Text = "作成日時";
			this.chCreated.Width = 78;
			// 
			// chModified
			// 
			this.chModified.Text = "最終更新日時";
			this.chModified.Width = 78;
			// 
			// PlaylistsViewCControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.lvPlaylists);
			this.Name = "PlaylistsViewCControl";
			this.Size = new System.Drawing.Size(368, 181);
			this.Load += new System.EventHandler(this.PlaylistsViewCControl_Load);
			this.cmsPlaylists.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DoubleBufferedListView lvPlaylists;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ColumnHeader chCount;
		private System.Windows.Forms.ColumnHeader chTime;
		private System.Windows.Forms.ColumnHeader chCreated;
		private System.Windows.Forms.ColumnHeader chModified;
		private System.Windows.Forms.ContextMenuStrip cmsPlaylists;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsViewPlaylistContents;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsPlay;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRenamePlaylist;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrange;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsDelete;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrangeToTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrangeUp;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrangeDown;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrangeToBottom;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem pVCToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiViewContents;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem tsmiRename;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrange;
		private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrangeToTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrangeUp;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrangeDown;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrangeToBottom;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem tsmiSort;
	}
}
