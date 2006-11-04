namespace Yusen.GExplorer.UserInterfaces {
	partial class PlaylistsView {
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
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tslTitle = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tslPlaylistsCount = new System.Windows.Forms.ToolStripLabel();
			this.tslGrandTotalContentsCount = new System.Windows.Forms.ToolStripLabel();
			this.tslGrandTotalTime = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tslPlaylistName = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tslSubtotalContentCount = new System.Windows.Forms.ToolStripLabel();
			this.tslSubtotalTime = new System.Windows.Forms.ToolStripLabel();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.pvcControl = new Yusen.GExplorer.UserInterfaces.PlaylistsViewCControl();
			this.pvpControl = new Yusen.GExplorer.UserInterfaces.PlaylistsViewPControl();
			this.toolStrip1.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslTitle,
            this.toolStripSeparator2,
            this.tslPlaylistsCount,
            this.tslGrandTotalContentsCount,
            this.tslGrandTotalTime,
            this.toolStripSeparator3,
            this.tslPlaylistName,
            this.toolStripSeparator4,
            this.tslSubtotalContentCount,
            this.tslSubtotalTime});
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
			this.tslTitle.Size = new System.Drawing.Size(56, 22);
			this.tslTitle.Text = "プレイリスト";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tslPlaylistsCount
			// 
			this.tslPlaylistsCount.Name = "tslPlaylistsCount";
			this.tslPlaylistsCount.Size = new System.Drawing.Size(29, 22);
			this.tslPlaylistsCount.Text = "1/23";
			// 
			// tslGrandTotalContentsCount
			// 
			this.tslGrandTotalContentsCount.Name = "tslGrandTotalContentsCount";
			this.tslGrandTotalContentsCount.Size = new System.Drawing.Size(41, 22);
			this.tslGrandTotalContentsCount.Text = "12/345";
			// 
			// tslGrandTotalTime
			// 
			this.tslGrandTotalTime.Name = "tslGrandTotalTime";
			this.tslGrandTotalTime.Size = new System.Drawing.Size(99, 22);
			this.tslGrandTotalTime.Text = "12:34:56/1.22:33:44";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tslPlaylistName
			// 
			this.tslPlaylistName.Name = "tslPlaylistName";
			this.tslPlaylistName.Size = new System.Drawing.Size(68, 22);
			this.tslPlaylistName.Text = "プレイリスト名";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tslSubtotalContentCount
			// 
			this.tslSubtotalContentCount.Name = "tslSubtotalContentCount";
			this.tslSubtotalContentCount.Size = new System.Drawing.Size(35, 22);
			this.tslSubtotalContentCount.Text = "12/34";
			// 
			// tslSubtotalTime
			// 
			this.tslSubtotalTime.Name = "tslSubtotalTime";
			this.tslSubtotalTime.Size = new System.Drawing.Size(85, 22);
			this.tslSubtotalTime.Text = "1:23:45/12:34:56";
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(600, 175);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(600, 200);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.pvcControl);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pvpControl);
			this.splitContainer1.Size = new System.Drawing.Size(600, 175);
			this.splitContainer1.SplitterDistance = 140;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
			// 
			// pvcControl
			// 
			this.pvcControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pvcControl.Location = new System.Drawing.Point(0, 0);
			this.pvcControl.Name = "pvcControl";
			this.pvcControl.Size = new System.Drawing.Size(140, 175);
			this.pvcControl.TabIndex = 0;
			this.pvcControl.ViewPlaylistRequested += new System.EventHandler(this.pvcControl_ViewPlaylistRequested);
			this.pvcControl.StatusMessagesChanged += new System.EventHandler(this.pvcControl_StatusMessagesChanged);
			// 
			// pvpControl
			// 
			this.pvpControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pvpControl.Location = new System.Drawing.Point(0, 0);
			this.pvpControl.Name = "pvpControl";
			this.pvpControl.Size = new System.Drawing.Size(456, 175);
			this.pvpControl.TabIndex = 0;
			this.pvpControl.LastSelectedContentChanged += new System.EventHandler(this.pvpControl_LastSelectedContentChanged);
			this.pvpControl.StatusMessagesChanged += new System.EventHandler(this.pvpControl_StatusMessagesChanged);
			// 
			// PlaylistsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "PlaylistsView";
			this.Size = new System.Drawing.Size(600, 200);
			this.Load += new System.EventHandler(this.PlaylistsView_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel tslTitle;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStripLabel tslPlaylistsCount;
		private System.Windows.Forms.ToolStripLabel tslGrandTotalContentsCount;
		private System.Windows.Forms.ToolStripLabel tslGrandTotalTime;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel tslSubtotalContentCount;
		private System.Windows.Forms.ToolStripLabel tslSubtotalTime;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStripLabel tslPlaylistName;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private PlaylistsViewCControl pvcControl;
		private PlaylistsViewPControl pvpControl;
	}
}
