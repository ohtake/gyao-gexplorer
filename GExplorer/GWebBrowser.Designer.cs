namespace Yusen.GExplorer {
	partial class GWebBrowser {
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
			this.cmsContent = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiContentOpenDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentAddToPlayList = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiContentPlayWithoutAdding = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentPlayWmp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiContentPlayBrowser = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentCancel = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsPackage = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiPackageOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPackageCancel = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsContent.SuspendLayout();
			this.cmsPackage.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsContent
			// 
			this.cmsContent.Enabled = true;
			this.cmsContent.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsContent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiContentOpenDetail,
            this.toolStripSeparator1,
            this.tsmiContentAddToPlayList,
            this.tsmiContentPlayWithoutAdding,
            this.toolStripSeparator2,
            this.tsmiContentPlayWmp,
            this.tsmiContentPlayBrowser,
            this.toolStripSeparator8,
            this.tsmiContentCommands,
            this.toolStripSeparator6,
            this.tsmiContentCancel});
			this.cmsContent.Location = new System.Drawing.Point(9, 50);
			this.cmsContent.Name = "cmsContent";
			this.cmsContent.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsContent.Size = new System.Drawing.Size(249, 201);
			this.cmsContent.Visible = true;
			// 
			// tsmiContentOpenDetail
			// 
			this.tsmiContentOpenDetail.Name = "tsmiContentOpenDetail";
			this.tsmiContentOpenDetail.Text = "詳細ページを開く (&D)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiContentAddToPlayList
			// 
			this.tsmiContentAddToPlayList.Name = "tsmiContentAddToPlayList";
			this.tsmiContentAddToPlayList.Text = "プレイリストに追加 (&A)";
			// 
			// tsmiContentPlayWithoutAdding
			// 
			this.tsmiContentPlayWithoutAdding.Name = "tsmiContentPlayWithoutAdding";
			this.tsmiContentPlayWithoutAdding.Text = "プレイリストに追加せずに再生 (&P)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiContentPlayWmp
			// 
			this.tsmiContentPlayWmp.Name = "tsmiContentPlayWmp";
			this.tsmiContentPlayWmp.Text = "WMPで再生 (&W)";
			// 
			// tsmiContentPlayBrowser
			// 
			this.tsmiContentPlayBrowser.Name = "tsmiContentPlayBrowser";
			this.tsmiContentPlayBrowser.Text = "ブラウザで再生 (&B)";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			// 
			// tsmiContentCommands
			// 
			this.tsmiContentCommands.Name = "tsmiContentCommands";
			this.tsmiContentCommands.Text = "外部コマンド (&C)";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			// 
			// tsmiContentCancel
			// 
			this.tsmiContentCancel.Name = "tsmiContentCancel";
			this.tsmiContentCancel.Text = "変なメニューが出てきて邪魔だよ (&A) ...";
			// 
			// cmsPackage
			// 
			this.cmsPackage.Enabled = true;
			this.cmsPackage.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsPackage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPackageOpen,
            this.toolStripSeparator5,
            this.tsmiPackageCancel});
			this.cmsPackage.Location = new System.Drawing.Point(9, 50);
			this.cmsPackage.Name = "cmsPackage";
			this.cmsPackage.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsPackage.Size = new System.Drawing.Size(249, 54);
			// 
			// tsmiPackageOpen
			// 
			this.tsmiPackageOpen.Name = "tsmiPackageOpen";
			this.tsmiPackageOpen.Text = "パッケージページを開く (&O)";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			// 
			// tsmiPackageCancel
			// 
			this.tsmiPackageCancel.Name = "tsmiPackageCancel";
			this.tsmiPackageCancel.Text = "変なメニューが出てきて邪魔だよ (&A) ...";
			// 
			// GWebBrowser
			// 
			this.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.GWebBrowser_DocumentCompleted);
			this.cmsContent.ResumeLayout(false);
			this.cmsPackage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip cmsContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentOpenDetail;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentAddToPlayList;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentPlayWithoutAdding;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentPlayWmp;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentPlayBrowser;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentCommands;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentCancel;
		private System.Windows.Forms.ContextMenuStrip cmsPackage;
		private System.Windows.Forms.ToolStripMenuItem tsmiPackageOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiPackageCancel;

	}
}
