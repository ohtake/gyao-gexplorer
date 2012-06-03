namespace Yusen.GExplorer {
	partial class CrawlResultView {
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
			this.listView1 = new System.Windows.Forms.ListView();
			this.chId = new System.Windows.Forms.ColumnHeader();
			this.chTitle = new System.Windows.Forms.ColumnHeader();
			this.chEpisode = new System.Windows.Forms.ColumnHeader();
			this.chSubTitle = new System.Windows.Forms.ColumnHeader();
			this.chDuration = new System.Windows.Forms.ColumnHeader();
			this.chDeadline = new System.Windows.Forms.ColumnHeader();
			this.chDescription = new System.Windows.Forms.ColumnHeader();
			this.cmsContent = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAddWithComment = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPlayWithWmp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlayWithBrowser = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiBroseDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyDetailUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyNameAndDetailUri = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRemoveCache = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAddNg = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAddNgWithTitle = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAddNgWithId = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiUserCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tslTitle = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbShowFilter = new System.Windows.Forms.ToolStripButton();
			this.tsddbNormalPages = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsddbExceptions = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsddbSettings = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiAboneType = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiShowPackages = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiHoverSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMultiSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiClearFilterStringOnHide = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiNewColor = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tslGenre = new System.Windows.Forms.ToolStripLabel();
			this.tslNumber = new System.Windows.Forms.ToolStripLabel();
			this.tslTime = new System.Windows.Forms.ToolStripLabel();
			this.tsFilter = new System.Windows.Forms.ToolStrip();
			this.tslFilter = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.tsddbFilterType = new System.Windows.Forms.ToolStripDropDownButton();
			this.tstbFilter = new System.Windows.Forms.ToolStripTextBox();
			this.tstbAnswer = new System.Windows.Forms.ToolStripTextBox();
			this.inputBoxDialog1 = new Yusen.GExplorer.InputBoxDialog();
			this.exceptionDialog1 = new Yusen.GExplorer.ExceptionDialog();
			this.timerFilter = new System.Windows.Forms.Timer(this.components);
			this.cmsContent.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.tsMain.SuspendLayout();
			this.tsFilter.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.AllowDrop = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chTitle,
            this.chEpisode,
            this.chSubTitle,
            this.chDuration,
            this.chDeadline,
            this.chDescription});
			this.listView1.ContextMenuStrip = this.cmsContent;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new System.Drawing.Size(550, 158);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
			this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView1_ItemDrag);
			// 
			// chId
			// 
			this.chId.Text = "contents_id";
			this.chId.Width = 74;
			// 
			// chTitle
			// 
			this.chTitle.Text = "�^�C�g��";
			this.chTitle.Width = 74;
			// 
			// chEpisode
			// 
			this.chEpisode.Text = "�b��";
			this.chEpisode.Width = 53;
			// 
			// chSubTitle
			// 
			this.chSubTitle.Text = "�T�u�^�C�g��";
			this.chSubTitle.Width = 55;
			// 
			// chDuration
			// 
			this.chDuration.Text = "�ԑg����";
			this.chDuration.Width = 56;
			// 
			// chDeadline
			// 
			this.chDeadline.Text = "�z�M����";
			this.chDeadline.Width = 70;
			// 
			// chDescription
			// 
			this.chDescription.Text = "����";
			this.chDescription.Width = 134;
			// 
			// cmsContent
			// 
			this.cmsContent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiAddWithComment,
            this.tsmiPlay,
            this.toolStripSeparator6,
            this.tsmiPlayWithWmp,
            this.tsmiPlayWithBrowser,
            this.tsmiBroseDetail,
            this.toolStripSeparator1,
            this.tsmiCopyName,
            this.tsmiCopyDetailUri,
            this.tsmiCopyNameAndDetailUri,
            this.toolStripSeparator2,
            this.tsmiRemoveCache,
            this.tsmiAddNg,
            this.toolStripSeparator3,
            this.tsmiUserCommands});
			this.cmsContent.Name = "contextMenuStrip1";
			this.cmsContent.Size = new System.Drawing.Size(215, 292);
			this.cmsContent.Opening += new System.ComponentModel.CancelEventHandler(this.cmsContent_Opening);
			// 
			// tsmiAdd
			// 
			this.tsmiAdd.Name = "tsmiAdd";
			this.tsmiAdd.Size = new System.Drawing.Size(214, 22);
			this.tsmiAdd.Text = "�v���C���X�g�ɒǉ�(&A)";
			this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
			// 
			// tsmiAddWithComment
			// 
			this.tsmiAddWithComment.Name = "tsmiAddWithComment";
			this.tsmiAddWithComment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.tsmiAddWithComment.Size = new System.Drawing.Size(214, 22);
			this.tsmiAddWithComment.Text = "�R�����g�t���Œǉ�(&L)...";
			this.tsmiAddWithComment.Click += new System.EventHandler(this.tsmiAddWithComment_Click);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.tsmiPlay.Size = new System.Drawing.Size(214, 22);
			this.tsmiPlay.Text = "�ǉ������ɍĐ�(&P)";
			this.tsmiPlay.Click += new System.EventHandler(this.tsmiPlay_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(211, 6);
			// 
			// tsmiPlayWithWmp
			// 
			this.tsmiPlayWithWmp.Name = "tsmiPlayWithWmp";
			this.tsmiPlayWithWmp.Size = new System.Drawing.Size(214, 22);
			this.tsmiPlayWithWmp.Text = "WMP�ōĐ�(&W)";
			this.tsmiPlayWithWmp.Click += new System.EventHandler(this.tsmiPlayWithWmp_Click);
			// 
			// tsmiPlayWithBrowser
			// 
			this.tsmiPlayWithBrowser.Name = "tsmiPlayWithBrowser";
			this.tsmiPlayWithBrowser.Size = new System.Drawing.Size(214, 22);
			this.tsmiPlayWithBrowser.Text = "�E�F�u�u���E�U�ōĐ��y�[�W(&I)";
			this.tsmiPlayWithBrowser.Click += new System.EventHandler(this.tsmiPlayWithBrowser_Click);
			// 
			// tsmiBroseDetail
			// 
			this.tsmiBroseDetail.Name = "tsmiBroseDetail";
			this.tsmiBroseDetail.Size = new System.Drawing.Size(214, 22);
			this.tsmiBroseDetail.Text = "�E�F�u�u���E�U�ŏڍ׃y�[�W(&E)";
			this.tsmiBroseDetail.Click += new System.EventHandler(this.tsmiBroseDetail_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(211, 6);
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.Size = new System.Drawing.Size(214, 22);
			this.tsmiCopyName.Text = "���O���R�s�[(&N)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyDetailUri
			// 
			this.tsmiCopyDetailUri.Name = "tsmiCopyDetailUri";
			this.tsmiCopyDetailUri.Size = new System.Drawing.Size(214, 22);
			this.tsmiCopyDetailUri.Text = "URI���R�s�[(&D)";
			this.tsmiCopyDetailUri.Click += new System.EventHandler(this.tsmiCopyDetailUri_Click);
			// 
			// tsmiCopyNameAndDetailUri
			// 
			this.tsmiCopyNameAndDetailUri.Name = "tsmiCopyNameAndDetailUri";
			this.tsmiCopyNameAndDetailUri.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsmiCopyNameAndDetailUri.Size = new System.Drawing.Size(214, 22);
			this.tsmiCopyNameAndDetailUri.Text = "���O��URI���R�s�[(&B)";
			this.tsmiCopyNameAndDetailUri.Click += new System.EventHandler(this.tsmiCopyNameAndDetailUri_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(211, 6);
			// 
			// tsmiRemoveCache
			// 
			this.tsmiRemoveCache.Name = "tsmiRemoveCache";
			this.tsmiRemoveCache.Size = new System.Drawing.Size(214, 22);
			this.tsmiRemoveCache.Text = "�L���b�V�����폜(&R)";
			this.tsmiRemoveCache.Click += new System.EventHandler(this.tsmiRemoveCache_Click);
			// 
			// tsmiAddNg
			// 
			this.tsmiAddNg.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddNgWithTitle,
            this.tsmiAddNgWithId});
			this.tsmiAddNg.Name = "tsmiAddNg";
			this.tsmiAddNg.Size = new System.Drawing.Size(214, 22);
			this.tsmiAddNg.Text = "NG�R���e���c�ɊȈՒǉ�(&G)";
			// 
			// tsmiAddNgWithTitle
			// 
			this.tsmiAddNgWithTitle.Name = "tsmiAddNgWithTitle";
			this.tsmiAddNgWithTitle.Size = new System.Drawing.Size(164, 22);
			this.tsmiAddNgWithTitle.Text = "�^�C�g����NG(&T)";
			this.tsmiAddNgWithTitle.Click += new System.EventHandler(this.tsmiAddNgWithTitle_Click);
			// 
			// tsmiAddNgWithId
			// 
			this.tsmiAddNgWithId.Name = "tsmiAddNgWithId";
			this.tsmiAddNgWithId.Size = new System.Drawing.Size(164, 22);
			this.tsmiAddNgWithId.Text = "contents_id��NG(&I)";
			this.tsmiAddNgWithId.Click += new System.EventHandler(this.tsmiAddNgWithId_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(211, 6);
			// 
			// tsmiUserCommands
			// 
			this.tsmiUserCommands.Name = "tsmiUserCommands";
			this.tsmiUserCommands.Size = new System.Drawing.Size(214, 22);
			this.tsmiUserCommands.Text = "�O���R�}���h(&C)";
			// 
			// colorDialog1
			// 
			this.colorDialog1.AnyColor = true;
			this.colorDialog1.FullOpen = true;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.listView1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(550, 158);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(550, 208);
			this.toolStripContainer1.TabIndex = 2;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsMain);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsFilter);
			// 
			// tsMain
			// 
			this.tsMain.AllowItemReorder = true;
			this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
			this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslTitle,
            this.toolStripSeparator4,
            this.tsbShowFilter,
            this.tsddbNormalPages,
            this.tsddbExceptions,
            this.tsddbSettings,
            this.toolStripSeparator5,
            this.tslGenre,
            this.tslNumber,
            this.tslTime});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(550, 25);
			this.tsMain.Stretch = true;
			this.tsMain.TabIndex = 2;
			this.tsMain.Text = "toolStrip1";
			// 
			// tslTitle
			// 
			this.tslTitle.Name = "tslTitle";
			this.tslTitle.Size = new System.Drawing.Size(66, 22);
			this.tslTitle.Text = "�N���[������";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbShowFilter
			// 
			this.tsbShowFilter.CheckOnClick = true;
			this.tsbShowFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsbShowFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbShowFilter.Name = "tsbShowFilter";
			this.tsbShowFilter.Size = new System.Drawing.Size(53, 22);
			this.tsbShowFilter.Text = "�t�B���^(&I)";
			this.tsbShowFilter.Click += new System.EventHandler(this.tsbShowFilter_Click);
			// 
			// tsddbNormalPages
			// 
			this.tsddbNormalPages.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbNormalPages.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbNormalPages.Name = "tsddbNormalPages";
			this.tsddbNormalPages.Size = new System.Drawing.Size(63, 22);
			this.tsddbNormalPages.Text = "�y�[�W(&P)";
			// 
			// tsddbExceptions
			// 
			this.tsddbExceptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbExceptions.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbExceptions.Name = "tsddbExceptions";
			this.tsddbExceptions.Size = new System.Drawing.Size(57, 22);
			this.tsddbExceptions.Text = "��O(&E)";
			// 
			// tsddbSettings
			// 
			this.tsddbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAboneType,
            this.toolStripSeparator8,
            this.tsmiShowPackages,
            this.tsmiHoverSelect,
            this.tsmiMultiSelect,
            this.toolStripSeparator7,
            this.tsmiClearFilterStringOnHide,
            this.toolStripSeparator10,
            this.tsmiNewColor});
			this.tsddbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbSettings.Name = "tsddbSettings";
			this.tsddbSettings.Size = new System.Drawing.Size(58, 22);
			this.tsddbSettings.Text = "�ݒ�(&C)";
			// 
			// tsmiAboneType
			// 
			this.tsmiAboneType.Name = "tsmiAboneType";
			this.tsmiAboneType.Size = new System.Drawing.Size(246, 22);
			this.tsmiAboneType.Text = "���ځ`����@(&A)";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(243, 6);
			// 
			// tsmiShowPackages
			// 
			this.tsmiShowPackages.Checked = true;
			this.tsmiShowPackages.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiShowPackages.Name = "tsmiShowPackages";
			this.tsmiShowPackages.Size = new System.Drawing.Size(246, 22);
			this.tsmiShowPackages.Text = "�p�b�P�[�W���Ƃɕ���(&G)";
			this.tsmiShowPackages.Click += new System.EventHandler(this.tsmiShowPackages_Click);
			// 
			// tsmiHoverSelect
			// 
			this.tsmiHoverSelect.CheckOnClick = true;
			this.tsmiHoverSelect.Name = "tsmiHoverSelect";
			this.tsmiHoverSelect.Size = new System.Drawing.Size(246, 22);
			this.tsmiHoverSelect.Text = "�}�E�X�z�o�[�őI��(&H)";
			this.tsmiHoverSelect.Click += new System.EventHandler(this.tsmiHoverSelect_Click);
			// 
			// tsmiMultiSelect
			// 
			this.tsmiMultiSelect.Checked = true;
			this.tsmiMultiSelect.CheckOnClick = true;
			this.tsmiMultiSelect.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiMultiSelect.Name = "tsmiMultiSelect";
			this.tsmiMultiSelect.Size = new System.Drawing.Size(246, 22);
			this.tsmiMultiSelect.Text = "�����I����L��(&M)";
			this.tsmiMultiSelect.Click += new System.EventHandler(this.tsmiMultiSelect_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(243, 6);
			// 
			// tsmiClearFilterStringOnHide
			// 
			this.tsmiClearFilterStringOnHide.Checked = true;
			this.tsmiClearFilterStringOnHide.CheckOnClick = true;
			this.tsmiClearFilterStringOnHide.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiClearFilterStringOnHide.Name = "tsmiClearFilterStringOnHide";
			this.tsmiClearFilterStringOnHide.Size = new System.Drawing.Size(246, 22);
			this.tsmiClearFilterStringOnHide.Text = "�t�B���^�����Ńt�B���^������N���A(&C)";
			// 
			// toolStripSeparator10
			// 
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(243, 6);
			// 
			// tsmiNewColor
			// 
			this.tsmiNewColor.Name = "tsmiNewColor";
			this.tsmiNewColor.Size = new System.Drawing.Size(246, 22);
			this.tsmiNewColor.Text = "�V���̐F(&N)...";
			this.tsmiNewColor.Click += new System.EventHandler(this.tsmiNewColor_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// tslGenre
			// 
			this.tslGenre.Name = "tslGenre";
			this.tslGenre.Size = new System.Drawing.Size(48, 22);
			this.tslGenre.Text = "tslGenre";
			// 
			// tslNumber
			// 
			this.tslNumber.Name = "tslNumber";
			this.tslNumber.Size = new System.Drawing.Size(57, 22);
			this.tslNumber.Text = "tslNumber";
			// 
			// tslTime
			// 
			this.tslTime.Name = "tslTime";
			this.tslTime.Size = new System.Drawing.Size(43, 22);
			this.tslTime.Text = "tslTime";
			// 
			// tsFilter
			// 
			this.tsFilter.Dock = System.Windows.Forms.DockStyle.None;
			this.tsFilter.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslFilter,
            this.toolStripSeparator9,
            this.tsddbFilterType,
            this.tstbFilter,
            this.tstbAnswer});
			this.tsFilter.Location = new System.Drawing.Point(0, 25);
			this.tsFilter.Name = "tsFilter";
			this.tsFilter.Size = new System.Drawing.Size(550, 25);
			this.tsFilter.Stretch = true;
			this.tsFilter.TabIndex = 3;
			this.tsFilter.Text = "tsFilter";
			// 
			// tslFilter
			// 
			this.tslFilter.Name = "tslFilter";
			this.tslFilter.Size = new System.Drawing.Size(38, 22);
			this.tslFilter.Text = "�t�B���^";
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
			// 
			// tsddbFilterType
			// 
			this.tsddbFilterType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbFilterType.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbFilterType.Name = "tsddbFilterType";
			this.tsddbFilterType.Size = new System.Drawing.Size(70, 22);
			this.tsddbFilterType.Text = "FilterType";
			// 
			// tstbFilter
			// 
			this.tstbFilter.Name = "tstbFilter";
			this.tstbFilter.Size = new System.Drawing.Size(150, 25);
			this.tstbFilter.TextChanged += new System.EventHandler(this.tstbFilter_TextChanged);
			// 
			// tstbAnswer
			// 
			this.tstbAnswer.Name = "tstbAnswer";
			this.tstbAnswer.ReadOnly = true;
			this.tstbAnswer.Size = new System.Drawing.Size(200, 25);
			// 
			// inputBoxDialog1
			// 
			this.inputBoxDialog1.Input = null;
			this.inputBoxDialog1.Message = null;
			this.inputBoxDialog1.Title = null;
			// 
			// exceptionDialog1
			// 
			this.exceptionDialog1.Exception = null;
			this.exceptionDialog1.Title = "�N���[�����ɖ���������O";
			// 
			// timerFilter
			// 
			this.timerFilter.Tick += new System.EventHandler(this.timerFilter_Tick);
			// 
			// CrawlResultView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "CrawlResultView";
			this.Size = new System.Drawing.Size(550, 208);
			this.Load += new System.EventHandler(this.CrawlResultView_Load);
			this.cmsContent.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.tsFilter.ResumeLayout(false);
			this.tsFilter.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader chId;
		private System.Windows.Forms.ColumnHeader chTitle;
		private System.Windows.Forms.ColumnHeader chEpisode;
		private System.Windows.Forms.ColumnHeader chSubTitle;
		private System.Windows.Forms.ColumnHeader chDuration;
		private System.Windows.Forms.ColumnHeader chDescription;
		private System.Windows.Forms.ContextMenuStrip cmsContent;
		private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayWithWmp;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayWithBrowser;
		private System.Windows.Forms.ToolStripMenuItem tsmiBroseDetail;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyDetailUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyNameAndDetailUri;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiUserCommands;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNg;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNgWithId;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddNgWithTitle;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ToolStripLabel tslTitle;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripDropDownButton tsddbSettings;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiAboneType;
		private System.Windows.Forms.ToolStripMenuItem tsmiNewColor;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem tsmiShowPackages;
		private System.Windows.Forms.ToolStripMenuItem tsmiMultiSelect;
		private System.Windows.Forms.ToolStripMenuItem tsmiHoverSelect;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
		private System.Windows.Forms.ToolStripLabel tslGenre;
		private System.Windows.Forms.ToolStrip tsFilter;
		private System.Windows.Forms.ToolStripTextBox tstbFilter;
		private System.Windows.Forms.ToolStripButton tsbShowFilter;
		private System.Windows.Forms.ToolStripTextBox tstbAnswer;
		private System.Windows.Forms.ToolStripDropDownButton tsddbNormalPages;
		private System.Windows.Forms.ToolStripLabel tslNumber;
		private System.Windows.Forms.ToolStripLabel tslTime;
		private System.Windows.Forms.ToolStripMenuItem tsmiClearFilterStringOnHide;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
		private System.Windows.Forms.ColumnHeader chDeadline;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddWithComment;
		private InputBoxDialog inputBoxDialog1;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveCache;
		private System.Windows.Forms.ToolStripDropDownButton tsddbExceptions;
		private ExceptionDialog exceptionDialog1;
		private System.Windows.Forms.ToolStripDropDownButton tsddbFilterType;
		private System.Windows.Forms.ToolStripLabel tslFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.Timer timerFilter;
	}
}
