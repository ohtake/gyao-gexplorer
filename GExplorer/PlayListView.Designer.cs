namespace Yusen.GExplorer {
	partial class PlayListView {
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
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.listView1 = new System.Windows.Forms.ListView();
			this.chId = new System.Windows.Forms.ColumnHeader();
			this.chName = new System.Windows.Forms.ColumnHeader();
			this.chDuration = new System.Windows.Forms.ColumnHeader();
			this.chDeadLine = new System.Windows.Forms.ColumnHeader();
			this.chComment = new System.Windows.Forms.ColumnHeader();
			this.cmsPlayListItem = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSetComment = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMove = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMoveToTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMoveUp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMoveDown = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMoveToBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRemoveItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPlayWithWmp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlayWithBrowser = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBrowseDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tslTitle = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsddbOperation = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiAddById = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiImport = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSerializePlayListNow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiExportAsAsx = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiClearPlayList = new System.Windows.Forms.ToolStripMenuItem();
			this.tsddbSettings = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiMultiSelectEnabled = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tslMessage = new System.Windows.Forms.ToolStripLabel();
			this.sfdAsx = new System.Windows.Forms.SaveFileDialog();
			this.ofdXml = new System.Windows.Forms.OpenFileDialog();
			this.sfdXml = new System.Windows.Forms.SaveFileDialog();
			this.tsmiSetDeadlines = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRefleshView = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.cmsPlayListItem.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.listView1);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(544, 161);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chName,
            this.chDuration,
            this.chDeadLine,
            this.chComment});
			this.listView1.ContextMenuStrip = this.cmsPlayListItem;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.ShowGroups = false;
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new System.Drawing.Size(544, 136);
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			// 
			// chId
			// 
			this.chId.Text = "contents_id";
			this.chId.Width = 80;
			// 
			// chName
			// 
			this.chName.Text = "�R���e���c��";
			this.chName.Width = 240;
			// 
			// chDuration
			// 
			this.chDuration.Text = "��������";
			this.chDuration.Width = 66;
			// 
			// chDeadLine
			// 
			this.chDeadLine.Text = "�z�M����";
			this.chDeadLine.Width = 66;
			// 
			// chComment
			// 
			this.chComment.Text = "�R�����g";
			this.chComment.Width = 66;
			// 
			// cmsPlayListItem
			// 
			this.cmsPlayListItem.Enabled = true;
			this.cmsPlayListItem.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsPlayListItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPlay,
            this.toolStripSeparator1,
            this.tsmiSetComment,
            this.tsmiMove,
            this.tsmiRemoveItem,
            this.toolStripSeparator2,
            this.tsmiPlayWithWmp,
            this.tsmiPlayWithBrowser,
            this.tsmiBrowseDetail,
            this.toolStripSeparator3,
            this.tsmiCopyName,
            this.tsmiCopyUri,
            this.tsmiCopyNameAndUri,
            this.toolStripSeparator4,
            this.tsmiCommands});
			this.cmsPlayListItem.Location = new System.Drawing.Point(21, 36);
			this.cmsPlayListItem.Name = "cmsPlayListItem";
			this.cmsPlayListItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsPlayListItem.Size = new System.Drawing.Size(310, 270);
			this.cmsPlayListItem.Opening += new System.ComponentModel.CancelEventHandler(this.cmsPlayListItem_Opening);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.Text = "�Đ� (&P)";
			this.tsmiPlay.Click += new System.EventHandler(this.tsmiPlay_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiSetComment
			// 
			this.tsmiSetComment.Name = "tsmiSetComment";
			this.tsmiSetComment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.tsmiSetComment.Text = "�R�����g����� (&L) ...";
			this.tsmiSetComment.Click += new System.EventHandler(this.tsmiSetComment_Click);
			// 
			// tsmiMove
			// 
			this.tsmiMove.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMoveToTop,
            this.tsmiMoveUp,
            this.tsmiMoveDown,
            this.tsmiMoveToBottom});
			this.tsmiMove.Name = "tsmiMove";
			this.tsmiMove.Text = "���X�g�����ړ� (&M)";
			// 
			// tsmiMoveToTop
			// 
			this.tsmiMoveToTop.Name = "tsmiMoveToTop";
			this.tsmiMoveToTop.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.Up)));
			this.tsmiMoveToTop.Text = "�ŏ㕔�� (&T)";
			this.tsmiMoveToTop.Click += new System.EventHandler(this.tsmiMoveToTop_Click);
			// 
			// tsmiMoveUp
			// 
			this.tsmiMoveUp.Name = "tsmiMoveUp";
			this.tsmiMoveUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
			this.tsmiMoveUp.Text = "��� (&U)";
			this.tsmiMoveUp.Click += new System.EventHandler(this.tsmiMoveUp_Click);
			// 
			// tsmiMoveDown
			// 
			this.tsmiMoveDown.Name = "tsmiMoveDown";
			this.tsmiMoveDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
			this.tsmiMoveDown.Text = "���� (&D)";
			this.tsmiMoveDown.Click += new System.EventHandler(this.tsmiMoveDown_Click);
			// 
			// tsmiMoveToBottom
			// 
			this.tsmiMoveToBottom.Name = "tsmiMoveToBottom";
			this.tsmiMoveToBottom.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.Down)));
			this.tsmiMoveToBottom.Text = "�ŉ����� (&B)";
			this.tsmiMoveToBottom.Click += new System.EventHandler(this.tsmiMoveToBottom_Click);
			// 
			// tsmiRemoveItem
			// 
			this.tsmiRemoveItem.Name = "tsmiRemoveItem";
			this.tsmiRemoveItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiRemoveItem.Text = "���X�g����폜 (&R)";
			this.tsmiRemoveItem.Click += new System.EventHandler(this.tsmiRemoveItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiPlayWithWmp
			// 
			this.tsmiPlayWithWmp.Name = "tsmiPlayWithWmp";
			this.tsmiPlayWithWmp.Text = "WMP�ōĐ� (&W)";
			this.tsmiPlayWithWmp.Click += new System.EventHandler(this.tsmiPlayWithWmp_Click);
			// 
			// tsmiPlayWithBrowser
			// 
			this.tsmiPlayWithBrowser.Name = "tsmiPlayWithBrowser";
			this.tsmiPlayWithBrowser.Text = "�E�F�u�u���E�U�ōĐ� (&I)";
			this.tsmiPlayWithBrowser.Click += new System.EventHandler(this.tsmiPlayWithBrowser_Click);
			// 
			// tsmiBrowseDetail
			// 
			this.tsmiBrowseDetail.Name = "tsmiBrowseDetail";
			this.tsmiBrowseDetail.Text = "�E�F�u�u���E�U�ŏڍ׃y�[�W (&E)";
			this.tsmiBrowseDetail.Click += new System.EventHandler(this.tsmiBrowseDetail_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.C)));
			this.tsmiCopyName.Text = "�R���e���c�����R�s�[ (&N)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyUri
			// 
			this.tsmiCopyUri.Name = "tsmiCopyUri";
			this.tsmiCopyUri.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Control) 
            | System.Windows.Forms.Keys.C)));
			this.tsmiCopyUri.Text = "�ڍ׃y�[�WURI���R�s�[ (&D)";
			this.tsmiCopyUri.Click += new System.EventHandler(this.tsmiCopyUri_Click);
			// 
			// tsmiCopyNameAndUri
			// 
			this.tsmiCopyNameAndUri.Name = "tsmiCopyNameAndUri";
			this.tsmiCopyNameAndUri.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsmiCopyNameAndUri.Text = "�R���e���c���Əڍ׃y�[�WURI���R�s�[ (&B)";
			this.tsmiCopyNameAndUri.Click += new System.EventHandler(this.tsmiCopyNameAndUri_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			// 
			// tsmiCommands
			// 
			this.tsmiCommands.Name = "tsmiCommands";
			this.tsmiCommands.Text = "�O���R�}���h (&C)";
			// 
			// toolStrip1
			// 
			this.toolStrip1.AllowItemReorder = true;
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslTitle,
            this.toolStripSeparator6,
            this.tsddbOperation,
            this.tsddbSettings,
            this.toolStripSeparator7,
            this.tslMessage});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(544, 25);
			this.toolStrip1.Stretch = true;
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tslTitle
			// 
			this.tslTitle.Name = "tslTitle";
			this.tslTitle.Text = "�v���C���X�g";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			// 
			// tsddbOperation
			// 
			this.tsddbOperation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbOperation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddById,
            this.tsmiSetDeadlines,
            this.tsmiRefleshView,
            this.toolStripSeparator9,
            this.tsmiExport,
            this.tsmiImport,
            this.tsmiSerializePlayListNow,
            this.toolStripSeparator5,
            this.tsmiExportAsAsx,
            this.toolStripSeparator8,
            this.tsmiClearPlayList});
			this.tsddbOperation.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbOperation.Name = "tsddbOperation";
			this.tsddbOperation.Text = "���� (&O)";
			// 
			// tsmiAddById
			// 
			this.tsmiAddById.Name = "tsmiAddById";
			this.tsmiAddById.Text = "ID�w��Ŏ蓮�ǉ� (&A) ...";
			this.tsmiAddById.Click += new System.EventHandler(this.tsmiAddById_Click);
			// 
			// tsmiExport
			// 
			this.tsmiExport.Name = "tsmiExport";
			this.tsmiExport.Text = "�v���C���X�g���C�N�X�|�[�g (&E) ...";
			this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
			// 
			// tsmiImport
			// 
			this.tsmiImport.Name = "tsmiImport";
			this.tsmiImport.Text = "�v���C���X�g���C���|�[�g (&I) ...";
			this.tsmiImport.Click += new System.EventHandler(this.tsmiImport_Click);
			// 
			// tsmiSerializePlayListNow
			// 
			this.tsmiSerializePlayListNow.Name = "tsmiSerializePlayListNow";
			this.tsmiSerializePlayListNow.Text = "�v���C���X�g���������V���A���C�Y (&S)";
			this.tsmiSerializePlayListNow.Click += new System.EventHandler(this.tsmiSerializePlayListNow_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			// 
			// tsmiExportAsAsx
			// 
			this.tsmiExportAsAsx.Name = "tsmiExportAsAsx";
			this.tsmiExportAsAsx.Text = "�v���C���X�g��ASX�Ƃ��ăC�N�X�|�[�g (&A) ...";
			this.tsmiExportAsAsx.Click += new System.EventHandler(this.tsmiExportAsAsx_Click);
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			// 
			// tsmiClearPlayList
			// 
			this.tsmiClearPlayList.Name = "tsmiClearPlayList";
			this.tsmiClearPlayList.Text = "�v���C���X�g�̑S���ڂ��폜 (&C)";
			this.tsmiClearPlayList.Click += new System.EventHandler(this.tsmiClearPlayList_Click);
			// 
			// tsddbSettings
			// 
			this.tsddbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMultiSelectEnabled});
			this.tsddbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbSettings.Name = "tsddbSettings";
			this.tsddbSettings.Text = "�ݒ� (&L)";
			// 
			// tsmiMultiSelectEnabled
			// 
			this.tsmiMultiSelectEnabled.Checked = true;
			this.tsmiMultiSelectEnabled.CheckOnClick = true;
			this.tsmiMultiSelectEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiMultiSelectEnabled.Name = "tsmiMultiSelectEnabled";
			this.tsmiMultiSelectEnabled.Text = "�����I����L�� (&M)";
			this.tsmiMultiSelectEnabled.Click += new System.EventHandler(this.tsmiMultiSelectEnabled_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			// 
			// tslMessage
			// 
			this.tslMessage.Name = "tslMessage";
			this.tslMessage.Text = "tslMessage";
			// 
			// sfdAsx
			// 
			this.sfdAsx.DefaultExt = "asx";
			this.sfdAsx.Filter = "ASX files (*.asx)|*.asx|All files (*.*)|*.*";
			this.sfdAsx.RestoreDirectory = true;
			// 
			// ofdXml
			// 
			this.ofdXml.DefaultExt = "xml";
			this.ofdXml.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
			this.ofdXml.RestoreDirectory = true;
			// 
			// sfdXml
			// 
			this.sfdXml.DefaultExt = "xml";
			this.sfdXml.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
			this.sfdXml.RestoreDirectory = true;
			// 
			// tsmiSetDeadlines
			// 
			this.tsmiSetDeadlines.Name = "tsmiSetDeadlines";
			this.tsmiSetDeadlines.Text = "�S���ڂ̔z�M������ݒ肵�Ȃ��� (&D)";
			this.tsmiSetDeadlines.Click += new System.EventHandler(this.tsmiSetDeadlines_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			// 
			// tsmiRefleshView
			// 
			this.tsmiRefleshView.Name = "tsmiRefleshView";
			this.tsmiRefleshView.Text = "�\�����e�������I�Ƀ��t���b�V�� (&R)";
			this.tsmiRefleshView.Click += new System.EventHandler(this.tsmiRefleshView_Click);
			// 
			// PlayListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "PlayListView";
			this.Size = new System.Drawing.Size(544, 161);
			this.Load += new System.EventHandler(this.PlayListView_Load);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.cmsPlayListItem.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader chId;
		private System.Windows.Forms.ColumnHeader chName;
		private System.Windows.Forms.ColumnHeader chDuration;
		private System.Windows.Forms.ContextMenuStrip cmsPlayListItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiMove;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveToTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveUp;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveDown;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveToBottom;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayWithWmp;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayWithBrowser;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseDetail;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyNameAndUri;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiCommands;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel tslTitle;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripDropDownButton tsddbOperation;
		private System.Windows.Forms.ToolStripMenuItem tsmiExportAsAsx;
		private System.Windows.Forms.ToolStripMenuItem tsmiSerializePlayListNow;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiClearPlayList;
		private System.Windows.Forms.ToolStripDropDownButton tsddbSettings;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripLabel tslMessage;
		private System.Windows.Forms.SaveFileDialog sfdAsx;
		private System.Windows.Forms.ToolStripMenuItem tsmiMultiSelectEnabled;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddById;
		private System.Windows.Forms.ToolStripMenuItem tsmiExport;
		private System.Windows.Forms.ToolStripMenuItem tsmiImport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.OpenFileDialog ofdXml;
		private System.Windows.Forms.SaveFileDialog sfdXml;
		private System.Windows.Forms.ColumnHeader chDeadLine;
		private System.Windows.Forms.ColumnHeader chComment;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetComment;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetDeadlines;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem tsmiRefleshView;
	}
}
