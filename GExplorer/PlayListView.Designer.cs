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
			this.listView1 = new Yusen.GExplorer.DoubleBufferedListView();
			this.chId = new System.Windows.Forms.ColumnHeader();
			this.chName = new System.Windows.Forms.ColumnHeader();
			this.chDuration = new System.Windows.Forms.ColumnHeader();
			this.chDeadline = new System.Windows.Forms.ColumnHeader();
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
			this.tsmiBrowseDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPlayWithBrowser = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tscapmiCopyProperty = new Yusen.GExplorer.ToolStripCAPropertyMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCatalog = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCatalogNormal = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCatalogImageSmall = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCatalogImageLarge = new System.Windows.Forms.ToolStripMenuItem();
			this.tsnfmiNgFav = new Yusen.GExplorer.ToolStripNgFavMenuItem();
			this.tsucmiCommand = new Yusen.GExplorer.ToolStripUserCommandMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tslTitle = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsddbOperation = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiAddById = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRefleshView = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiExport = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiImportAppend = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiImportOverwrite = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRemoveUnreachables = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiClearPlayList = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.tslCount = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			this.tslTime = new System.Windows.Forms.ToolStripLabel();
			this.sfdAsx = new System.Windows.Forms.SaveFileDialog();
			this.ofdXml = new System.Windows.Forms.OpenFileDialog();
			this.sfdXml = new System.Windows.Forms.SaveFileDialog();
			this.timerSumSelected = new System.Windows.Forms.Timer(this.components);
			this.inputBoxDialog1 = new Yusen.GExplorer.InputBoxDialog();
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
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(600, 155);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(600, 180);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// listView1
			// 
			this.listView1.AllowDrop = true;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chName,
            this.chDuration,
            this.chDeadline,
            this.chComment});
			this.listView1.ContextMenuStrip = this.cmsPlayListItem;
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.ShowGroups = false;
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new System.Drawing.Size(600, 155);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
			this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
			this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView1_ItemDrag);
			// 
			// chId
			// 
			this.chId.Text = "contents_id";
			this.chId.Width = 80;
			// 
			// chName
			// 
			this.chName.Text = "コンテンツ名";
			this.chName.Width = 255;
			// 
			// chDuration
			// 
			this.chDuration.Text = "番組時間";
			this.chDuration.Width = 62;
			// 
			// chDeadline
			// 
			this.chDeadline.Text = "配信期限";
			this.chDeadline.Width = 80;
			// 
			// chComment
			// 
			this.chComment.Text = "コメント";
			this.chComment.Width = 90;
			// 
			// cmsPlayListItem
			// 
			this.cmsPlayListItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPlay,
            this.toolStripSeparator1,
            this.tsmiSetComment,
            this.tsmiMove,
            this.tsmiRemoveItem,
            this.toolStripSeparator2,
            this.tsmiBrowseDetail,
            this.tsmiPlayWithBrowser,
            this.toolStripSeparator3,
            this.tsmiCopyName,
            this.tsmiCopyUri,
            this.tsmiCopyNameAndUri,
            this.tscapmiCopyProperty,
            this.toolStripSeparator4,
            this.tsmiCatalog,
            this.tsnfmiNgFav,
            this.tsucmiCommand});
			this.cmsPlayListItem.Name = "cmsPlayListItem";
			this.cmsPlayListItem.Size = new System.Drawing.Size(212, 314);
			this.cmsPlayListItem.Opening += new System.ComponentModel.CancelEventHandler(this.cmsPlayListItem_Opening);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Image = global::Yusen.GExplorer.Properties.Resources.Play;
			this.tsmiPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.Size = new System.Drawing.Size(211, 22);
			this.tsmiPlay.Text = "再生(&P)";
			this.tsmiPlay.Click += new System.EventHandler(this.tsmiPlay_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
			// 
			// tsmiSetComment
			// 
			this.tsmiSetComment.Image = global::Yusen.GExplorer.Properties.Resources.Comment;
			this.tsmiSetComment.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiSetComment.Name = "tsmiSetComment";
			this.tsmiSetComment.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.tsmiSetComment.Size = new System.Drawing.Size(211, 22);
			this.tsmiSetComment.Text = "コメントを入力(&L)...";
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
			this.tsmiMove.Size = new System.Drawing.Size(211, 22);
			this.tsmiMove.Text = "リスト内を移動(&M)";
			// 
			// tsmiMoveToTop
			// 
			this.tsmiMoveToTop.Name = "tsmiMoveToTop";
			this.tsmiMoveToTop.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
						| System.Windows.Forms.Keys.Up)));
			this.tsmiMoveToTop.Size = new System.Drawing.Size(211, 22);
			this.tsmiMoveToTop.Text = "最上部へ(&T)";
			this.tsmiMoveToTop.Click += new System.EventHandler(this.tsmiMoveToTop_Click);
			// 
			// tsmiMoveUp
			// 
			this.tsmiMoveUp.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_moveup;
			this.tsmiMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiMoveUp.Name = "tsmiMoveUp";
			this.tsmiMoveUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
			this.tsmiMoveUp.Size = new System.Drawing.Size(211, 22);
			this.tsmiMoveUp.Text = "上へ(&U)";
			this.tsmiMoveUp.Click += new System.EventHandler(this.tsmiMoveUp_Click);
			// 
			// tsmiMoveDown
			// 
			this.tsmiMoveDown.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_movedown;
			this.tsmiMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiMoveDown.Name = "tsmiMoveDown";
			this.tsmiMoveDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
			this.tsmiMoveDown.Size = new System.Drawing.Size(211, 22);
			this.tsmiMoveDown.Text = "下へ(&D)";
			this.tsmiMoveDown.Click += new System.EventHandler(this.tsmiMoveDown_Click);
			// 
			// tsmiMoveToBottom
			// 
			this.tsmiMoveToBottom.Name = "tsmiMoveToBottom";
			this.tsmiMoveToBottom.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
						| System.Windows.Forms.Keys.Down)));
			this.tsmiMoveToBottom.Size = new System.Drawing.Size(211, 22);
			this.tsmiMoveToBottom.Text = "最下部へ(&B)";
			this.tsmiMoveToBottom.Click += new System.EventHandler(this.tsmiMoveToBottom_Click);
			// 
			// tsmiRemoveItem
			// 
			this.tsmiRemoveItem.Image = global::Yusen.GExplorer.Properties.Resources.Delete;
			this.tsmiRemoveItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiRemoveItem.Name = "tsmiRemoveItem";
			this.tsmiRemoveItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiRemoveItem.Size = new System.Drawing.Size(211, 22);
			this.tsmiRemoveItem.Text = "リストから削除(&R)";
			this.tsmiRemoveItem.Click += new System.EventHandler(this.tsmiRemoveItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(208, 6);
			// 
			// tsmiBrowseDetail
			// 
			this.tsmiBrowseDetail.Name = "tsmiBrowseDetail";
			this.tsmiBrowseDetail.Size = new System.Drawing.Size(211, 22);
			this.tsmiBrowseDetail.Text = "ウェブブラウザで詳細ページ(&D)";
			this.tsmiBrowseDetail.Click += new System.EventHandler(this.tsmiBrowseDetail_Click);
			// 
			// tsmiPlayWithBrowser
			// 
			this.tsmiPlayWithBrowser.Name = "tsmiPlayWithBrowser";
			this.tsmiPlayWithBrowser.Size = new System.Drawing.Size(211, 22);
			this.tsmiPlayWithBrowser.Text = "ウェブブラウザで再生ページ(&V)";
			this.tsmiPlayWithBrowser.Click += new System.EventHandler(this.tsmiPlayWithBrowser_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(208, 6);
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.Size = new System.Drawing.Size(211, 22);
			this.tsmiCopyName.Text = "名前をコピー(&N)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyUri
			// 
			this.tsmiCopyUri.Name = "tsmiCopyUri";
			this.tsmiCopyUri.Size = new System.Drawing.Size(211, 22);
			this.tsmiCopyUri.Text = "URIをコピー(&U)";
			this.tsmiCopyUri.Click += new System.EventHandler(this.tsmiCopyUri_Click);
			// 
			// tsmiCopyNameAndUri
			// 
			this.tsmiCopyNameAndUri.Image = global::Yusen.GExplorer.Properties.Resources.Copy;
			this.tsmiCopyNameAndUri.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCopyNameAndUri.Name = "tsmiCopyNameAndUri";
			this.tsmiCopyNameAndUri.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsmiCopyNameAndUri.Size = new System.Drawing.Size(211, 22);
			this.tsmiCopyNameAndUri.Text = "名前とURIをコピー(&B)";
			this.tsmiCopyNameAndUri.Click += new System.EventHandler(this.tsmiCopyNameAndUri_Click);
			// 
			// tscapmiCopyProperty
			// 
			this.tscapmiCopyProperty.Name = "tscapmiCopyProperty";
			this.tscapmiCopyProperty.Size = new System.Drawing.Size(211, 22);
			this.tscapmiCopyProperty.Text = "その他のコピー(&O)";
			this.tscapmiCopyProperty.PropertySelected += new System.EventHandler<Yusen.GExplorer.CAPropertySelectedEventArgs>(this.tscapmiCopyProperty_PropertySelected);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(208, 6);
			// 
			// tsmiCatalog
			// 
			this.tsmiCatalog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCatalogNormal,
            this.tsmiCatalogImageSmall,
            this.tsmiCatalogImageLarge});
			this.tsmiCatalog.Name = "tsmiCatalog";
			this.tsmiCatalog.Size = new System.Drawing.Size(211, 22);
			this.tsmiCatalog.Text = "カタログ表示(&C)";
			// 
			// tsmiCatalogNormal
			// 
			this.tsmiCatalogNormal.Name = "tsmiCatalogNormal";
			this.tsmiCatalogNormal.Size = new System.Drawing.Size(179, 22);
			this.tsmiCatalogNormal.Text = "サムネイルとテキスト(&N)";
			this.tsmiCatalogNormal.Click += new System.EventHandler(this.tsmiCatalogNormal_Click);
			// 
			// tsmiCatalogImageSmall
			// 
			this.tsmiCatalogImageSmall.Name = "tsmiCatalogImageSmall";
			this.tsmiCatalogImageSmall.Size = new System.Drawing.Size(179, 22);
			this.tsmiCatalogImageSmall.Text = "画像小のみ(&S)";
			this.tsmiCatalogImageSmall.Click += new System.EventHandler(this.tsmiCatalogImageSmall_Click);
			// 
			// tsmiCatalogImageLarge
			// 
			this.tsmiCatalogImageLarge.Name = "tsmiCatalogImageLarge";
			this.tsmiCatalogImageLarge.Size = new System.Drawing.Size(179, 22);
			this.tsmiCatalogImageLarge.Text = "画像大のみ(&L)";
			this.tsmiCatalogImageLarge.Click += new System.EventHandler(this.tsmiCatalogImageLarge_Click);
			// 
			// tsnfmiNgFav
			// 
			this.tsnfmiNgFav.Name = "tsnfmiNgFav";
			this.tsnfmiNgFav.Size = new System.Drawing.Size(211, 22);
			this.tsnfmiNgFav.Text = "NG/FAV関連(&G)";
			this.tsnfmiNgFav.SubmenuSelected += new System.EventHandler<Yusen.GExplorer.ContentSelectionRequiredEventArgs>(this.tsnfmiNgFav_SubmenuSelected);
			// 
			// tsucmiCommand
			// 
			this.tsucmiCommand.Name = "tsucmiCommand";
			this.tsucmiCommand.Size = new System.Drawing.Size(211, 22);
			this.tsucmiCommand.Text = "外部コマンド(&E)";
			this.tsucmiCommand.UserCommandSelected += new System.EventHandler<Yusen.GExplorer.UserCommandSelectedEventArgs>(this.tsucmiCommand_UserCommandSelected);
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
            this.toolStripSeparator7,
            this.tslCount,
            this.toolStripSeparator8,
            this.tslTime});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(600, 25);
			this.toolStrip1.Stretch = true;
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tslTitle
			// 
			this.tslTitle.Name = "tslTitle";
			this.tslTitle.Size = new System.Drawing.Size(56, 22);
			this.tslTitle.Text = "プレイリスト";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// tsddbOperation
			// 
			this.tsddbOperation.AutoToolTip = false;
			this.tsddbOperation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsddbOperation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddById,
            this.tsmiRefleshView,
            this.toolStripSeparator9,
            this.tsmiExport,
            this.tsmiImportAppend,
            this.tsmiImportOverwrite,
            this.toolStripSeparator5,
            this.tsmiRemoveUnreachables,
            this.tsmiClearPlayList});
			this.tsddbOperation.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbOperation.Name = "tsddbOperation";
			this.tsddbOperation.Size = new System.Drawing.Size(58, 22);
			this.tsddbOperation.Text = "操作(&O)";
			// 
			// tsmiAddById
			// 
			this.tsmiAddById.Name = "tsmiAddById";
			this.tsmiAddById.Size = new System.Drawing.Size(254, 22);
			this.tsmiAddById.Text = "ID指定で手動追加(&O)...";
			this.tsmiAddById.Click += new System.EventHandler(this.tsmiAddById_Click);
			// 
			// tsmiRefleshView
			// 
			this.tsmiRefleshView.Name = "tsmiRefleshView";
			this.tsmiRefleshView.Size = new System.Drawing.Size(254, 22);
			this.tsmiRefleshView.Text = "表示内容を強制的にリフレッシュ(&R)";
			this.tsmiRefleshView.Click += new System.EventHandler(this.tsmiRefleshView_Click);
			// 
			// toolStripSeparator9
			// 
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(251, 6);
			// 
			// tsmiExport
			// 
			this.tsmiExport.Name = "tsmiExport";
			this.tsmiExport.Size = new System.Drawing.Size(254, 22);
			this.tsmiExport.Text = "プレイリストをイクスポート(&E)...";
			this.tsmiExport.Click += new System.EventHandler(this.tsmiExport_Click);
			// 
			// tsmiImportAppend
			// 
			this.tsmiImportAppend.Name = "tsmiImportAppend";
			this.tsmiImportAppend.Size = new System.Drawing.Size(254, 22);
			this.tsmiImportAppend.Text = "プレイリストを追加インポート(&I)...";
			this.tsmiImportAppend.Click += new System.EventHandler(this.tsmiImportAppend_Click);
			// 
			// tsmiImportOverwrite
			// 
			this.tsmiImportOverwrite.Name = "tsmiImportOverwrite";
			this.tsmiImportOverwrite.Size = new System.Drawing.Size(254, 22);
			this.tsmiImportOverwrite.Text = "プレイリストを上書きインポート(&M)...";
			this.tsmiImportOverwrite.Click += new System.EventHandler(this.tsmiImportOverwrite_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(251, 6);
			// 
			// tsmiRemoveUnreachables
			// 
			this.tsmiRemoveUnreachables.Name = "tsmiRemoveUnreachables";
			this.tsmiRemoveUnreachables.Size = new System.Drawing.Size(254, 22);
			this.tsmiRemoveUnreachables.Text = "クロール結果で到達不可のを削除(&U)...";
			this.tsmiRemoveUnreachables.Click += new System.EventHandler(this.tsmiRemoveUnreachables_Click);
			// 
			// tsmiClearPlayList
			// 
			this.tsmiClearPlayList.Name = "tsmiClearPlayList";
			this.tsmiClearPlayList.Size = new System.Drawing.Size(254, 22);
			this.tsmiClearPlayList.Text = "プレイリストの全項目を削除(&C)...";
			this.tsmiClearPlayList.Click += new System.EventHandler(this.tsmiClearPlayList_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			// 
			// tslCount
			// 
			this.tslCount.Name = "tslCount";
			this.tslCount.Size = new System.Drawing.Size(48, 22);
			this.tslCount.Text = "tslCount";
			// 
			// toolStripSeparator8
			// 
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
			// 
			// tslTime
			// 
			this.tslTime.Name = "tslTime";
			this.tslTime.Size = new System.Drawing.Size(43, 22);
			this.tslTime.Text = "tslTime";
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
			// timerSumSelected
			// 
			this.timerSumSelected.Interval = 30;
			this.timerSumSelected.Tick += new System.EventHandler(this.timerSumSelected_Tick);
			// 
			// inputBoxDialog1
			// 
			this.inputBoxDialog1.Input = null;
			this.inputBoxDialog1.Message = null;
			this.inputBoxDialog1.Title = null;
			// 
			// PlayListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer1);
			this.Name = "PlayListView";
			this.Size = new System.Drawing.Size(600, 180);
			this.Load += new System.EventHandler(this.PlayListView_Load);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.cmsPlayListItem.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private Yusen.GExplorer.DoubleBufferedListView listView1;
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
		private System.Windows.Forms.ToolStripMenuItem tsmiPlayWithBrowser;
		private System.Windows.Forms.ToolStripMenuItem tsmiBrowseDetail;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyNameAndUri;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripLabel tslTitle;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripDropDownButton tsddbOperation;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiClearPlayList;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripLabel tslCount;
		private System.Windows.Forms.SaveFileDialog sfdAsx;
		private System.Windows.Forms.ToolStripMenuItem tsmiExport;
		private System.Windows.Forms.ToolStripMenuItem tsmiImportOverwrite;
		private System.Windows.Forms.OpenFileDialog ofdXml;
		private System.Windows.Forms.SaveFileDialog sfdXml;
		private System.Windows.Forms.ColumnHeader chDeadline;
		private System.Windows.Forms.ColumnHeader chComment;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetComment;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
		private System.Windows.Forms.ToolStripMenuItem tsmiRefleshView;
		private System.Windows.Forms.ToolStripMenuItem tsmiImportAppend;
		private System.Windows.Forms.Timer timerSumSelected;
		private System.Windows.Forms.ToolStripMenuItem tsmiRemoveUnreachables;
		private InputBoxDialog inputBoxDialog1;
		private ToolStripCAPropertyMenuItem tscapmiCopyProperty;
		private ToolStripUserCommandMenuItem tsucmiCommand;
		private System.Windows.Forms.ToolStripMenuItem tsmiCatalog;
		private System.Windows.Forms.ToolStripMenuItem tsmiCatalogNormal;
		private System.Windows.Forms.ToolStripMenuItem tsmiCatalogImageSmall;
		private System.Windows.Forms.ToolStripMenuItem tsmiCatalogImageLarge;
		private System.Windows.Forms.ToolStripMenuItem tsmiAddById;
		private ToolStripNgFavMenuItem tsnfmiNgFav;
		private System.Windows.Forms.ToolStripLabel tslTime;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
	}
}
