namespace Yusen.GExplorer {
	partial class GenreTabPage {
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
			this.cmsGenre = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiReload = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiBrowseTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseTimetableRecentlyUpdatedFirst = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseTimetableDeadlineNearFirst = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyGenreName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyGenreNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsGenre.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsGenre
			// 
			this.cmsGenre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiReload,
            this.toolStripSeparator1,
            this.tsmiBrowseTop,
            this.tsmiBrowseTimetableRecentlyUpdatedFirst,
            this.tsmiBrowseTimetableDeadlineNearFirst,
            this.toolStripSeparator2,
            this.tsmiCopyGenreName,
            this.tsmiCopyUri,
            this.tsmiCopyGenreNameAndUri});
			this.cmsGenre.Name = "cmsGenre";
			this.cmsGenre.Size = new System.Drawing.Size(191, 170);
			// 
			// tsmiReload
			// 
			this.tsmiReload.Name = "tsmiReload";
			this.tsmiReload.Size = new System.Drawing.Size(190, 22);
			this.tsmiReload.Text = "再クロール(&R)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
			// 
			// tsmiBrowseTop
			// 
			this.tsmiBrowseTop.Name = "tsmiBrowseTop";
			this.tsmiBrowseTop.Size = new System.Drawing.Size(190, 22);
			this.tsmiBrowseTop.Text = "ジャンルトップ(&T)";
			// 
			// tsmiBrowseTimetableRecentlyUpdatedFirst
			// 
			this.tsmiBrowseTimetableRecentlyUpdatedFirst.Name = "tsmiBrowseTimetableRecentlyUpdatedFirst";
			this.tsmiBrowseTimetableRecentlyUpdatedFirst.Size = new System.Drawing.Size(190, 22);
			this.tsmiBrowseTimetableRecentlyUpdatedFirst.Text = "更新日優先番組表(&U)";
			// 
			// tsmiBrowseTimetableDeadlineNearFirst
			// 
			this.tsmiBrowseTimetableDeadlineNearFirst.Enabled = false;
			this.tsmiBrowseTimetableDeadlineNearFirst.Name = "tsmiBrowseTimetableDeadlineNearFirst";
			this.tsmiBrowseTimetableDeadlineNearFirst.Size = new System.Drawing.Size(190, 22);
			this.tsmiBrowseTimetableDeadlineNearFirst.Text = "残り日数優先番組表(&D)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(187, 6);
			// 
			// tsmiCopyGenreName
			// 
			this.tsmiCopyGenreName.Name = "tsmiCopyGenreName";
			this.tsmiCopyGenreName.Size = new System.Drawing.Size(190, 22);
			this.tsmiCopyGenreName.Text = "名前をコピー(&N)";
			// 
			// tsmiCopyUri
			// 
			this.tsmiCopyUri.Name = "tsmiCopyUri";
			this.tsmiCopyUri.Size = new System.Drawing.Size(190, 22);
			this.tsmiCopyUri.Text = "URIをコピー(&U)";
			// 
			// tsmiCopyGenreNameAndUri
			// 
			this.tsmiCopyGenreNameAndUri.Name = "tsmiCopyGenreNameAndUri";
			this.tsmiCopyGenreNameAndUri.Size = new System.Drawing.Size(190, 22);
			this.tsmiCopyGenreNameAndUri.Text = "名前とURIをコピー(&B)";
			// 
			// GenreTabPage
			// 
			this.ContextMenuStrip = this.cmsGenre;
			this.UseVisualStyleBackColor = true;
			this.cmsGenre.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip cmsGenre;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyGenreName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyGenreNameAndUri;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseTimetableDeadlineNearFirst;
		private System.Windows.Forms.ToolStripMenuItem tsmiReload;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseTimetableRecentlyUpdatedFirst;
	}
}
