﻿namespace Yusen.GExplorer.UserInterfaces {
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
			this.tsmiContentPerformClick = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentOpenDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentPlayWithoutAdding = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiContentCancel = new System.Windows.Forms.ToolStripMenuItem();
			this.cmsPackage = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiPackagePerformClick = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPackageOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPackageCancel = new System.Windows.Forms.ToolStripMenuItem();
			this.ttId = new System.Windows.Forms.ToolTip(this.components);
			this.timerIgnoreMenu = new System.Windows.Forms.Timer(this.components);
			this.inputBoxDialog1 = new Yusen.GExplorer.UserInterfaces.InputBoxDialog();
			this.tspmiAddToPlaylist = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			this.cmsContent.SuspendLayout();
			this.cmsPackage.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsContent
			// 
			this.cmsContent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiContentPerformClick,
            this.toolStripSeparator3,
            this.tsmiContentOpenDetail,
            this.toolStripSeparator1,
            this.tspmiAddToPlaylist,
            this.tsmiContentPlayWithoutAdding,
            this.toolStripSeparator2,
            this.tsmiContentCancel});
			this.cmsContent.Name = "cmsContent";
			this.cmsContent.Size = new System.Drawing.Size(204, 132);
			// 
			// tsmiContentPerformClick
			// 
			this.tsmiContentPerformClick.Name = "tsmiContentPerformClick";
			this.tsmiContentPerformClick.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentPerformClick.Text = "通常のクリックとして扱う(&E)";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(200, 6);
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
			// tsmiContentPlayWithoutAdding
			// 
			this.tsmiContentPlayWithoutAdding.Image = global::Yusen.GExplorer.Properties.Resources.Play;
			this.tsmiContentPlayWithoutAdding.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiContentPlayWithoutAdding.Name = "tsmiContentPlayWithoutAdding";
			this.tsmiContentPlayWithoutAdding.Size = new System.Drawing.Size(203, 22);
			this.tsmiContentPlayWithoutAdding.Text = "追加せずに再生(&P)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(200, 6);
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
            this.tsmiPackagePerformClick,
            this.toolStripSeparator4,
            this.tsmiPackageOpen,
            this.toolStripSeparator5,
            this.tsmiPackageCancel});
			this.cmsPackage.Name = "cmsPackage";
			this.cmsPackage.Size = new System.Drawing.Size(205, 82);
			// 
			// tsmiPackagePerformClick
			// 
			this.tsmiPackagePerformClick.Name = "tsmiPackagePerformClick";
			this.tsmiPackagePerformClick.Size = new System.Drawing.Size(204, 22);
			this.tsmiPackagePerformClick.Text = "通常のクリックとして扱う(&E)";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(201, 6);
			// 
			// tsmiPackageOpen
			// 
			this.tsmiPackageOpen.Name = "tsmiPackageOpen";
			this.tsmiPackageOpen.Size = new System.Drawing.Size(204, 22);
			this.tsmiPackageOpen.Text = "シリーズ一覧ページを開く(&O)";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(201, 6);
			// 
			// tsmiPackageCancel
			// 
			this.tsmiPackageCancel.Name = "tsmiPackageCancel";
			this.tsmiPackageCancel.Size = new System.Drawing.Size(204, 22);
			this.tsmiPackageCancel.Text = "ティップやメニューが邪魔(&D)...";
			// 
			// ttId
			// 
			this.ttId.IsBalloon = true;
			this.ttId.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			// 
			// timerIgnoreMenu
			// 
			this.timerIgnoreMenu.Tick += new System.EventHandler(this.timerIgnoreMenu_Tick);
			// 
			// inputBoxDialog1
			// 
			this.inputBoxDialog1.Input = null;
			this.inputBoxDialog1.Message = null;
			this.inputBoxDialog1.Title = null;
			// 
			// tspmiAddToPlaylist
			// 
			this.tspmiAddToPlaylist.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_add;
			this.tspmiAddToPlaylist.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tspmiAddToPlaylist.Name = "tspmiAddToPlaylist";
			this.tspmiAddToPlaylist.Size = new System.Drawing.Size(203, 22);
			this.tspmiAddToPlaylist.Text = "プレイリストに追加(&A)";
			// 
			// GWebBrowser
			// 
			this.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.GWebBrowser_Navigated);
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
		private System.Windows.Forms.ToolStripMenuItem tsmiContentPlayWithoutAdding;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentCancel;
		private System.Windows.Forms.ContextMenuStrip cmsPackage;
		private System.Windows.Forms.ToolStripMenuItem tsmiPackageOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiPackageCancel;
		private System.Windows.Forms.ToolTip ttId;
		private Yusen.GExplorer.UserInterfaces.InputBoxDialog inputBoxDialog1;
		private System.Windows.Forms.Timer timerIgnoreMenu;
		private System.Windows.Forms.ToolStripMenuItem tsmiContentPerformClick;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiPackagePerformClick;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private ToolStripPlaylistMenuItem tspmiAddToPlaylist;

	}
}
