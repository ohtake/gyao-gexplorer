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
			this.tsmiCopyGenreName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyGenreNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiBrowseTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseTimetable = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsGenre.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsGenre
			// 
			this.cmsGenre.Enabled = true;
			this.cmsGenre.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsGenre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBrowseTop,
            this.tsmiBrowseTimetable,
            this.toolStripSeparator1,
            this.tsmiCopyGenreName,
            this.tsmiCopyUri,
            this.tsmiCopyGenreNameAndUri});
			this.cmsGenre.Location = new System.Drawing.Point(9, 50);
			this.cmsGenre.Name = "cmsGenre";
			this.cmsGenre.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsGenre.Size = new System.Drawing.Size(203, 139);
			this.cmsGenre.Visible = true;
			// 
			// tsmiCopyGenreName
			// 
			this.tsmiCopyGenreName.Name = "tsmiCopyGenreName";
			this.tsmiCopyGenreName.Text = "ジャンル名をコピー (&N)";
			// 
			// tsmiCopyUri
			// 
			this.tsmiCopyUri.Name = "tsmiCopyUri";
			this.tsmiCopyUri.Text = "URIをコピー (&U)";
			// 
			// tsmiCopyGenreNameAndUri
			// 
			this.tsmiCopyGenreNameAndUri.Name = "tsmiCopyGenreNameAndUri";
			this.tsmiCopyGenreNameAndUri.Text = "ジャンル名とURIをコピー (&B)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiBrowseTop
			// 
			this.tsmiBrowseTop.Name = "tsmiBrowseTop";
			this.tsmiBrowseTop.Text = "ジャンルトップをウェブブラウザで開く (&T)";
			// 
			// tsmiBrowseTimetable
			// 
			this.tsmiBrowseTimetable.Name = "tsmiBrowseTimetable";
			this.tsmiBrowseTimetable.Text = "番組表をウェブブラウザで開く (&T)";
			// 
			// GenreTabPage
			// 
			this.ContextMenuStrip = this.cmsGenre;
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
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseTimetable;
	}
}
