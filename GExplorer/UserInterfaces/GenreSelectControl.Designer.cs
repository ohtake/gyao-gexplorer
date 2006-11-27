namespace Yusen.GExplorer.UserInterfaces {
	partial class GenreSelectControl {
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
			this.tabcGsc = new System.Windows.Forms.TabControl();
			this.bwCrawl = new System.ComponentModel.BackgroundWorker();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiRoot = new System.Windows.Forms.ToolStripMenuItem();
			this.tsgmiAddAndSelectGenre = new Yusen.GExplorer.UserInterfaces.ToolStripGenreMenuItem();
			this.tsmiAddAllGenres = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiMergeAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMergeOpened = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiGoToPrevTab = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiGoToNextTab = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiStartCrawl = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiBrowseGenreTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseTimetableUpdated = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCloseThis = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCloseLeft = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCloseRight = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCloseButThis = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCloseAll = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsGenreTab = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiCmsStartCrawl = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCmsBrowseGenreTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsBrowseTimetableUpdated = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCmsCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsCopyNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tssSep8 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCmsCloseThis = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsCloseLeft = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsCloseRight = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsCloseButThis = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.cmsGenreTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabcGsc
			// 
			this.tabcGsc.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabcGsc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabcGsc.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabcGsc.HotTrack = true;
			this.tabcGsc.Location = new System.Drawing.Point(0, 0);
			this.tabcGsc.Multiline = true;
			this.tabcGsc.Name = "tabcGsc";
			this.tabcGsc.SelectedIndex = 0;
			this.tabcGsc.ShowToolTips = true;
			this.tabcGsc.Size = new System.Drawing.Size(400, 60);
			this.tabcGsc.TabIndex = 0;
			this.tabcGsc.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabcGsc_DrawItem);
			this.tabcGsc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabcGsc_MouseClick);
			this.tabcGsc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabcGsc_MouseDoubleClick);
			this.tabcGsc.Resize += new System.EventHandler(this.tabcGsc_Resize);
			this.tabcGsc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tabcGsc_KeyUp);
			this.tabcGsc.SelectedIndexChanged += new System.EventHandler(this.tabcGsc_SelectedIndexChanged);
			this.tabcGsc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabcGsc_KeyDown);
			// 
			// bwCrawl
			// 
			this.bwCrawl.WorkerReportsProgress = true;
			this.bwCrawl.WorkerSupportsCancellation = true;
			this.bwCrawl.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCrawl_DoWork);
			this.bwCrawl.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCrawl_RunWorkerCompleted);
			this.bwCrawl.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwCrawl_ProgressChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRoot});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip1.Size = new System.Drawing.Size(74, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			// 
			// tsmiRoot
			// 
			this.tsmiRoot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsgmiAddAndSelectGenre,
            this.tsmiAddAllGenres,
            this.toolStripMenuItem1,
            this.tsmiMergeAll,
            this.tsmiMergeOpened,
            this.tssSep1,
            this.tsmiGoToPrevTab,
            this.tsmiGoToNextTab,
            this.tssSep2,
            this.tsmiStartCrawl,
            this.tssSep3,
            this.tsmiBrowseGenreTop,
            this.tsmiBrowseTimetableUpdated,
            this.tssSep4,
            this.tsmiCopyName,
            this.tsmiCopyUri,
            this.tsmiCopyNameAndUri,
            this.tssSep5,
            this.tsmiCloseThis,
            this.tsmiCloseLeft,
            this.tsmiCloseRight,
            this.tsmiCloseButThis,
            this.tsmiCloseAll});
			this.tsmiRoot.Name = "tsmiRoot";
			this.tsmiRoot.Size = new System.Drawing.Size(66, 20);
			this.tsmiRoot.Text = "GenreTab";
			// 
			// tsgmiAddAndSelectGenre
			// 
			this.tsgmiAddAndSelectGenre.Name = "tsgmiAddAndSelectGenre";
			this.tsgmiAddAndSelectGenre.Size = new System.Drawing.Size(213, 22);
			this.tsgmiAddAndSelectGenre.Text = "ジャンルタブの追加・選択(&A)";
			this.tsgmiAddAndSelectGenre.GenreSelected += new System.EventHandler(this.tsgmiAddAndSelectGenre_GenreSelected);
			// 
			// tsmiAddAllGenres
			// 
			this.tsmiAddAllGenres.Name = "tsmiAddAllGenres";
			this.tsmiAddAllGenres.Size = new System.Drawing.Size(213, 22);
			this.tsmiAddAllGenres.Text = "全てのジャンルタブを追加(&I)";
			this.tsmiAddAllGenres.Click += new System.EventHandler(this.tsmiAddAllGenres_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(210, 6);
			// 
			// tsmiMergeAll
			// 
			this.tsmiMergeAll.Name = "tsmiMergeAll";
			this.tsmiMergeAll.Size = new System.Drawing.Size(213, 22);
			this.tsmiMergeAll.Text = "全ジャンルを結合(&W)";
			this.tsmiMergeAll.Click += new System.EventHandler(this.tsmiMergeAll_Click);
			// 
			// tsmiMergeOpened
			// 
			this.tsmiMergeOpened.Name = "tsmiMergeOpened";
			this.tsmiMergeOpened.Size = new System.Drawing.Size(213, 22);
			this.tsmiMergeOpened.Text = "開いているジャンルを結合(&O)";
			this.tsmiMergeOpened.Click += new System.EventHandler(this.tsmiMergeOpened_Click);
			// 
			// tssSep1
			// 
			this.tssSep1.Name = "tssSep1";
			this.tssSep1.Size = new System.Drawing.Size(210, 6);
			// 
			// tsmiGoToPrevTab
			// 
			this.tsmiGoToPrevTab.Name = "tsmiGoToPrevTab";
			this.tsmiGoToPrevTab.Size = new System.Drawing.Size(213, 22);
			this.tsmiGoToPrevTab.Text = "前のタブへ(&P)";
			this.tsmiGoToPrevTab.Click += new System.EventHandler(this.tsmiGoToPrevTab_Click);
			// 
			// tsmiGoToNextTab
			// 
			this.tsmiGoToNextTab.Name = "tsmiGoToNextTab";
			this.tsmiGoToNextTab.Size = new System.Drawing.Size(213, 22);
			this.tsmiGoToNextTab.Text = "次のタブへ(&N)";
			this.tsmiGoToNextTab.Click += new System.EventHandler(this.tsmiGoToNextTab_Click);
			// 
			// tssSep2
			// 
			this.tssSep2.Name = "tssSep2";
			this.tssSep2.Size = new System.Drawing.Size(210, 6);
			// 
			// tsmiStartCrawl
			// 
			this.tsmiStartCrawl.Image = global::Yusen.GExplorer.Properties.Resources.FormRun;
			this.tsmiStartCrawl.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiStartCrawl.Name = "tsmiStartCrawl";
			this.tsmiStartCrawl.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
			this.tsmiStartCrawl.Size = new System.Drawing.Size(213, 22);
			this.tsmiStartCrawl.Text = "クロール実行(&X)";
			this.tsmiStartCrawl.Click += new System.EventHandler(this.tsmiStartCrawl_Click);
			// 
			// tssSep3
			// 
			this.tssSep3.Name = "tssSep3";
			this.tssSep3.Size = new System.Drawing.Size(210, 6);
			// 
			// tsmiBrowseGenreTop
			// 
			this.tsmiBrowseGenreTop.Name = "tsmiBrowseGenreTop";
			this.tsmiBrowseGenreTop.Size = new System.Drawing.Size(213, 22);
			this.tsmiBrowseGenreTop.Text = "ジャンルトップページを開く(&G)";
			this.tsmiBrowseGenreTop.Click += new System.EventHandler(this.tsmiBrowseGenreTop_Click);
			// 
			// tsmiBrowseTimetableUpdated
			// 
			this.tsmiBrowseTimetableUpdated.Name = "tsmiBrowseTimetableUpdated";
			this.tsmiBrowseTimetableUpdated.Size = new System.Drawing.Size(213, 22);
			this.tsmiBrowseTimetableUpdated.Text = "更新日優先番組表を開く(&M)";
			this.tsmiBrowseTimetableUpdated.Click += new System.EventHandler(this.tsmiBrowseTimetableUpdated_Click);
			// 
			// tssSep4
			// 
			this.tssSep4.Name = "tssSep4";
			this.tssSep4.Size = new System.Drawing.Size(210, 6);
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.Size = new System.Drawing.Size(213, 22);
			this.tsmiCopyName.Text = "ジャンル名をコピー(&T)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyUri
			// 
			this.tsmiCopyUri.Name = "tsmiCopyUri";
			this.tsmiCopyUri.Size = new System.Drawing.Size(213, 22);
			this.tsmiCopyUri.Text = "URIをコピー(&U)";
			this.tsmiCopyUri.Click += new System.EventHandler(this.tsmiCopyUri_Click);
			// 
			// tsmiCopyNameAndUri
			// 
			this.tsmiCopyNameAndUri.Image = global::Yusen.GExplorer.Properties.Resources.Copy;
			this.tsmiCopyNameAndUri.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCopyNameAndUri.Name = "tsmiCopyNameAndUri";
			this.tsmiCopyNameAndUri.Size = new System.Drawing.Size(213, 22);
			this.tsmiCopyNameAndUri.Text = "ジャンル名とURIをコピー(&C)";
			this.tsmiCopyNameAndUri.Click += new System.EventHandler(this.tsmiCopyNameAndUri_Click);
			// 
			// tssSep5
			// 
			this.tssSep5.Name = "tssSep5";
			this.tssSep5.Size = new System.Drawing.Size(210, 6);
			// 
			// tsmiCloseThis
			// 
			this.tsmiCloseThis.Name = "tsmiCloseThis";
			this.tsmiCloseThis.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.tsmiCloseThis.Size = new System.Drawing.Size(213, 22);
			this.tsmiCloseThis.Text = "このタブを閉じる(&E)";
			this.tsmiCloseThis.Click += new System.EventHandler(this.tsmiCloseThis_Click);
			// 
			// tsmiCloseLeft
			// 
			this.tsmiCloseLeft.Name = "tsmiCloseLeft";
			this.tsmiCloseLeft.Size = new System.Drawing.Size(213, 22);
			this.tsmiCloseLeft.Text = "このタブより左を閉じる(&L)";
			this.tsmiCloseLeft.Click += new System.EventHandler(this.tsmiCloseLeft_Click);
			// 
			// tsmiCloseRight
			// 
			this.tsmiCloseRight.Name = "tsmiCloseRight";
			this.tsmiCloseRight.Size = new System.Drawing.Size(213, 22);
			this.tsmiCloseRight.Text = "このタブより右を閉じる(&R)";
			this.tsmiCloseRight.Click += new System.EventHandler(this.tsmiCloseRight_Click);
			// 
			// tsmiCloseButThis
			// 
			this.tsmiCloseButThis.Name = "tsmiCloseButThis";
			this.tsmiCloseButThis.Size = new System.Drawing.Size(213, 22);
			this.tsmiCloseButThis.Text = "このタブ以外を閉じる(&B)";
			this.tsmiCloseButThis.Click += new System.EventHandler(this.tsmiCloseButThis_Click);
			// 
			// tsmiCloseAll
			// 
			this.tsmiCloseAll.Name = "tsmiCloseAll";
			this.tsmiCloseAll.Size = new System.Drawing.Size(213, 22);
			this.tsmiCloseAll.Text = "全てのタブを閉じる(&D)";
			this.tsmiCloseAll.Click += new System.EventHandler(this.tsmiCloseAll_Click);
			// 
			// cmsGenreTab
			// 
			this.cmsGenreTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCmsStartCrawl,
            this.tssSep6,
            this.tsmiCmsBrowseGenreTop,
            this.tsmiCmsBrowseTimetableUpdated,
            this.tssSep7,
            this.tsmiCmsCopyName,
            this.tsmiCmsCopyUri,
            this.tsmiCmsCopyNameAndUri,
            this.tssSep8,
            this.tsmiCmsCloseThis,
            this.tsmiCmsCloseLeft,
            this.tsmiCmsCloseRight,
            this.tsmiCmsCloseButThis});
			this.cmsGenreTab.Name = "contextMenuStrip1";
			this.cmsGenreTab.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.cmsGenreTab.Size = new System.Drawing.Size(214, 242);
			// 
			// tsmiCmsStartCrawl
			// 
			this.tsmiCmsStartCrawl.Image = global::Yusen.GExplorer.Properties.Resources.FormRun;
			this.tsmiCmsStartCrawl.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsStartCrawl.Name = "tsmiCmsStartCrawl";
			this.tsmiCmsStartCrawl.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsStartCrawl.Text = "クロール実行(&X)";
			this.tsmiCmsStartCrawl.Click += new System.EventHandler(this.tsmiCmsStartCrawl_Click);
			// 
			// tssSep6
			// 
			this.tssSep6.Name = "tssSep6";
			this.tssSep6.Size = new System.Drawing.Size(210, 6);
			// 
			// tsmiCmsBrowseGenreTop
			// 
			this.tsmiCmsBrowseGenreTop.Name = "tsmiCmsBrowseGenreTop";
			this.tsmiCmsBrowseGenreTop.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsBrowseGenreTop.Text = "ジャンルトップページを開く(&G)";
			this.tsmiCmsBrowseGenreTop.Click += new System.EventHandler(this.tsmiCmsBrowseGenreTop_Click);
			// 
			// tsmiCmsBrowseTimetableUpdated
			// 
			this.tsmiCmsBrowseTimetableUpdated.Name = "tsmiCmsBrowseTimetableUpdated";
			this.tsmiCmsBrowseTimetableUpdated.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsBrowseTimetableUpdated.Text = "更新日優先番組表を開く(&M)";
			this.tsmiCmsBrowseTimetableUpdated.Click += new System.EventHandler(this.tsmiCmsBrowseTimetableUpdated_Click);
			// 
			// tssSep7
			// 
			this.tssSep7.Name = "tssSep7";
			this.tssSep7.Size = new System.Drawing.Size(210, 6);
			// 
			// tsmiCmsCopyName
			// 
			this.tsmiCmsCopyName.Name = "tsmiCmsCopyName";
			this.tsmiCmsCopyName.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsCopyName.Text = "ジャンル名をコピー(&T)";
			this.tsmiCmsCopyName.Click += new System.EventHandler(this.tsmiCmsCopyName_Click);
			// 
			// tsmiCmsCopyUri
			// 
			this.tsmiCmsCopyUri.Name = "tsmiCmsCopyUri";
			this.tsmiCmsCopyUri.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsCopyUri.Text = "URIをコピー(&U)";
			this.tsmiCmsCopyUri.Click += new System.EventHandler(this.tsmiCmsCopyUri_Click);
			// 
			// tsmiCmsCopyNameAndUri
			// 
			this.tsmiCmsCopyNameAndUri.Image = global::Yusen.GExplorer.Properties.Resources.Copy;
			this.tsmiCmsCopyNameAndUri.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsCopyNameAndUri.Name = "tsmiCmsCopyNameAndUri";
			this.tsmiCmsCopyNameAndUri.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsCopyNameAndUri.Text = "ジャンル名とURIをコピー(&C)";
			this.tsmiCmsCopyNameAndUri.Click += new System.EventHandler(this.tsmiCmsCopyNameAndUri_Click);
			// 
			// tssSep8
			// 
			this.tssSep8.Name = "tssSep8";
			this.tssSep8.Size = new System.Drawing.Size(210, 6);
			// 
			// tsmiCmsCloseThis
			// 
			this.tsmiCmsCloseThis.Name = "tsmiCmsCloseThis";
			this.tsmiCmsCloseThis.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsCloseThis.Text = "このタブを閉じる(&E)";
			this.tsmiCmsCloseThis.Click += new System.EventHandler(this.tsmiCmsCloseThis_Click);
			// 
			// tsmiCmsCloseLeft
			// 
			this.tsmiCmsCloseLeft.Name = "tsmiCmsCloseLeft";
			this.tsmiCmsCloseLeft.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsCloseLeft.Text = "このタブより左を閉じる(&L)";
			this.tsmiCmsCloseLeft.Click += new System.EventHandler(this.tsmiCmsCloseLeft_Click);
			// 
			// tsmiCmsCloseRight
			// 
			this.tsmiCmsCloseRight.Name = "tsmiCmsCloseRight";
			this.tsmiCmsCloseRight.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsCloseRight.Text = "このタブより右を閉じる(&R)";
			this.tsmiCmsCloseRight.Click += new System.EventHandler(this.tsmiCmsCloseRight_Click);
			// 
			// tsmiCmsCloseButThis
			// 
			this.tsmiCmsCloseButThis.Name = "tsmiCmsCloseButThis";
			this.tsmiCmsCloseButThis.Size = new System.Drawing.Size(213, 22);
			this.tsmiCmsCloseButThis.Text = "このタブ以外を閉じる(&B)";
			this.tsmiCmsCloseButThis.Click += new System.EventHandler(this.tsmiCmsCloseButThis_Click);
			// 
			// GenreSelectControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.tabcGsc);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "GenreSelectControl";
			this.Size = new System.Drawing.Size(400, 60);
			this.Load += new System.EventHandler(this.GenreSelctControl_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.cmsGenreTab.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabcGsc;
		private System.ComponentModel.BackgroundWorker bwCrawl;
		private System.Windows.Forms.ToolStripMenuItem tsmiRoot;
		private System.Windows.Forms.ToolStripMenuItem tsmiStartCrawl;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ContextMenuStrip cmsGenreTab;
		private System.Windows.Forms.ToolStripSeparator tssSep5;
		private System.Windows.Forms.ToolStripMenuItem tsmiCloseThis;
		private System.Windows.Forms.ToolStripMenuItem tsmiCloseLeft;
		private System.Windows.Forms.ToolStripMenuItem tsmiCloseRight;
		private System.Windows.Forms.ToolStripMenuItem tsmiCloseAll;
		private System.Windows.Forms.ToolStripSeparator tssSep2;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseGenreTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseTimetableUpdated;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyNameAndUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddAllGenres;
		private System.Windows.Forms.ToolStripSeparator tssSep4;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyUri;
		private ToolStripGenreMenuItem tsgmiAddAndSelectGenre;
		private System.Windows.Forms.ToolStripMenuItem tsmiCloseButThis;
		private System.Windows.Forms.ToolStripSeparator tssSep3;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsStartCrawl;
		private System.Windows.Forms.ToolStripSeparator tssSep6;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsBrowseGenreTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsBrowseTimetableUpdated;
		private System.Windows.Forms.ToolStripSeparator tssSep7;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCopyNameAndUri;
		private System.Windows.Forms.ToolStripSeparator tssSep8;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCloseThis;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCloseLeft;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCloseRight;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCloseButThis;
		private System.Windows.Forms.ToolStripSeparator tssSep1;
		private System.Windows.Forms.ToolStripMenuItem tsmiGoToPrevTab;
		private System.Windows.Forms.ToolStripMenuItem tsmiGoToNextTab;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiMergeAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiMergeOpened;
	}
}
