﻿namespace Yusen.GExplorer {
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
			this.tsmiCrawl = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCatalog = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCatalogPackage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCatalogContent = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCatalogBoth = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiBrowseTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseTimetableRecentlyUpdatedFirst = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseTimetableDeadlineNearFirst = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyGenreName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyGenreNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRemoveCrawlResult = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsGenre.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsGenre
			// 
			this.cmsGenre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCrawl,
            this.tsmiCatalog,
            this.toolStripSeparator1,
            this.tsmiBrowseTop,
            this.tsmiBrowseTimetableRecentlyUpdatedFirst,
            this.tsmiBrowseTimetableDeadlineNearFirst,
            this.toolStripSeparator2,
            this.tsmiCopyGenreName,
            this.tsmiCopyUri,
            this.tsmiCopyGenreNameAndUri,
            this.toolStripSeparator3,
            this.tsmiRemoveCrawlResult});
			this.cmsGenre.Name = "cmsGenre";
			this.cmsGenre.Size = new System.Drawing.Size(191, 220);
			this.cmsGenre.Opening += new System.ComponentModel.CancelEventHandler(this.cmsGenre_Opening);
			// 
			// tsmiCrawl
			// 
			this.tsmiCrawl.Image = global::Yusen.GExplorer.Properties.Resources.FormRun;
			this.tsmiCrawl.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCrawl.Name = "tsmiCrawl";
			this.tsmiCrawl.Size = new System.Drawing.Size(190, 22);
			this.tsmiCrawl.Text = "クロール実行(&A)";
			// 
			// tsmiCatalog
			// 
			this.tsmiCatalog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCatalogPackage,
            this.tsmiCatalogContent,
            this.tsmiCatalogBoth});
			this.tsmiCatalog.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCatalog.Name = "tsmiCatalog";
			this.tsmiCatalog.Size = new System.Drawing.Size(190, 22);
			this.tsmiCatalog.Text = "カタログ表示(&C)";
			// 
			// tsmiCatalogPackage
			// 
			this.tsmiCatalogPackage.Name = "tsmiCatalogPackage";
			this.tsmiCatalogPackage.Size = new System.Drawing.Size(152, 22);
			this.tsmiCatalogPackage.Text = "パッケージのみ(&P)";
			// 
			// tsmiCatalogContent
			// 
			this.tsmiCatalogContent.Name = "tsmiCatalogContent";
			this.tsmiCatalogContent.Size = new System.Drawing.Size(152, 22);
			this.tsmiCatalogContent.Text = "コンテンツのみ(&C)";
			// 
			// tsmiCatalogBoth
			// 
			this.tsmiCatalogBoth.Name = "tsmiCatalogBoth";
			this.tsmiCatalogBoth.Size = new System.Drawing.Size(152, 22);
			this.tsmiCatalogBoth.Text = "両方(&B)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
			// 
			// tsmiBrowseTop
			// 
			this.tsmiBrowseTop.ImageTransparentColor = System.Drawing.Color.Magenta;
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
			this.tsmiCopyGenreNameAndUri.Image = global::Yusen.GExplorer.Properties.Resources.Copy;
			this.tsmiCopyGenreNameAndUri.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCopyGenreNameAndUri.Name = "tsmiCopyGenreNameAndUri";
			this.tsmiCopyGenreNameAndUri.Size = new System.Drawing.Size(190, 22);
			this.tsmiCopyGenreNameAndUri.Text = "名前とURIをコピー(&B)";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(187, 6);
			// 
			// tsmiRemoveCrawlResult
			// 
			this.tsmiRemoveCrawlResult.Image = global::Yusen.GExplorer.Properties.Resources.Delete;
			this.tsmiRemoveCrawlResult.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiRemoveCrawlResult.Name = "tsmiRemoveCrawlResult";
			this.tsmiRemoveCrawlResult.Size = new System.Drawing.Size(190, 22);
			this.tsmiRemoveCrawlResult.Text = "クロール結果を破棄(&R)...";
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
		private System.Windows.Forms.ToolStripMenuItem tsmiCrawl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseTimetableRecentlyUpdatedFirst;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveCrawlResult;
		private System.Windows.Forms.ToolStripMenuItem tsmiCatalog;
		private System.Windows.Forms.ToolStripMenuItem tsmiCatalogPackage;
		private System.Windows.Forms.ToolStripMenuItem tsmiCatalogContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiCatalogBoth;
	}
}
