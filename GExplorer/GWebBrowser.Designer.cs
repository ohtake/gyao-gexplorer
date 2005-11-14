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
			this.tsmiContentAddToPlayListWithComment = new System.Windows.Forms.ToolStripMenuItem();
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
			this.ttId = new System.Windows.Forms.ToolTip(this.components);
			this.inputBoxDialog1 = new Yusen.GExplorer.InputBoxDialog();
			this.cmsContent.SuspendLayout();
			this.cmsPackage.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsContent
			// 
			this.cmsContent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiContentOpenDetail,
            this.toolStripSeparator1,
            this.tsmiContentAddToPlayList,
            this.tsmiContentAddToPlayListWithComment,
            this.tsmiContentPlayWithoutAdding,
            this.toolStripSeparator2,
            this.tsmiContentPlayWmp,
            this.tsmiContentPlayBrowser,
            this.toolStripSeparator8,
            this.tsmiContentCommands,
            this.toolStripSeparator6,
            this.tsmiContentCancel});
			this.cmsContent.Name = "cmsContent";
			this.cmsContent.Size = new System.Drawing.Size(204, 204);
			// 
			// tsmiContentOpenDetail
			// 
			this.tsmiContentOpenDetail.Name = "tsmiContentOpenDetail";
			this.tsmiContentOpenDetail.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentOpenDetail.Text = "詳細ページを開く(&O)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
			// 
			// tsmiContentAddToPlayList
			// 
			this.tsmiContentAddToPlayList.Name = "tsmiContentAddToPlayList";
			this.tsmiContentAddToPlayList.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentAddToPlayList.Text = "プレイリストに追加(&A)";
			// 
			// tsmiContentAddToPlayListWithComment
			// 
			this.tsmiContentAddToPlayListWithComment.Name = "tsmiContentAddToPlayListWithComment";
			this.tsmiContentAddToPlayListWithComment.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentAddToPlayListWithComment.Text = "コメント付きで追加(&L)...";
			// 
			// tsmiContentPlayWithoutAdding
			// 
			this.tsmiContentPlayWithoutAdding.Name = "tsmiContentPlayWithoutAdding";
			this.tsmiContentPlayWithoutAdding.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentPlayWithoutAdding.Text = "追加せずに再生(&P)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(200, 6);
			// 
			// tsmiContentPlayWmp
			// 
			this.tsmiContentPlayWmp.Name = "tsmiContentPlayWmp";
			this.tsmiContentPlayWmp.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentPlayWmp.Text = "WMPで再生(&W)";
			// 
			// tsmiContentPlayBrowser
			// 
			this.tsmiContentPlayBrowser.Name = "tsmiContentPlayBrowser";
			this.tsmiContentPlayBrowser.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentPlayBrowser.Text = "ブラウザで再生(&B)";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(200, 6);
			// 
			// tsmiContentCommands
			// 
			this.tsmiContentCommands.Name = "tsmiContentCommands";
			this.tsmiContentCommands.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentCommands.Text = "外部コマンド(&C)";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(200, 6);
			// 
			// tsmiContentCancel
			// 
			this.tsmiContentCancel.Name = "tsmiContentCancel";
			this.tsmiContentCancel.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentCancel.Text = "ティップやメニューが邪魔(&D)...";
			// 
			// cmsPackage
			// 
			this.cmsPackage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPackageOpen,
            this.toolStripSeparator5,
            this.tsmiPackageCancel});
			this.cmsPackage.Name = "cmsPackage";
			this.cmsPackage.Size = new System.Drawing.Size(204, 54);
			// 
			// tsmiPackageOpen
			// 
			this.tsmiPackageOpen.Name = "tsmiPackageOpen";
			this.tsmiPackageOpen.Size = new System.Drawing.Size(203, 22);
			this.tsmiPackageOpen.Text = "パッケージページを開く(&O)";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(200, 6);
			// 
			// tsmiPackageCancel
			// 
			this.tsmiPackageCancel.Name = "tsmiPackageCancel";
			this.tsmiPackageCancel.Size = new System.Drawing.Size(203, 22);
			this.tsmiPackageCancel.Text = "ティップやメニューが邪魔(&D)...";
			// 
			// ttId
			// 
			this.ttId.IsBalloon = true;
			this.ttId.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			// 
			// inputBoxDialog1
			// 
			this.inputBoxDialog1.Input = null;
			this.inputBoxDialog1.Message = null;
			this.inputBoxDialog1.Title = null;
			// 
			// GWebBrowser
			// 
			this.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.GWebBrowser_Navigating);
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
		private System.Windows.Forms.ToolStripMenuItem tsmiContentAddToPlayListWithComment;
		private System.Windows.Forms.ToolTip ttId;
		private InputBoxDialog inputBoxDialog1;

	}
}
