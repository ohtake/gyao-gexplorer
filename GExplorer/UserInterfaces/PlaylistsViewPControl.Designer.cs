namespace Yusen.GExplorer.UserInterfaces {
	partial class PlaylistsViewPControl {
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
			this.cmsContents = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiCmsPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCmsRearrange = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsRearrangeToTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsRearrangeUp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsRearrangeDown = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsRearrangeToBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.tspmiCmsCopyToAnother = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			this.tspmiCmsMoveToAnother = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			this.tsmiCmsDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCmsCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCmsCopyNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tscpmiCmsCopyOtherProperties = new Yusen.GExplorer.UserInterfaces.ToolStripContentPropertyMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsecmiCmsCommand = new Yusen.GExplorer.UserInterfaces.ToolStripExternalCommandMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tsmiPVP = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSort = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRenamePlaylist = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiRearrange = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRearrangeToTop = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRearrangeUp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRearrangeDown = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiRearrangeToBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.tspmiCopyToAnother = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			this.tspmiMoveToAnother = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCopyNameAndUri = new System.Windows.Forms.ToolStripMenuItem();
			this.tscpmiCopyOtherProperties = new Yusen.GExplorer.UserInterfaces.ToolStripContentPropertyMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsecmiCommand = new Yusen.GExplorer.UserInterfaces.ToolStripExternalCommandMenuItem();
			this.lvContents = new Yusen.GExplorer.UserInterfaces.DoubleBufferedListView();
			this.chId = new System.Windows.Forms.ColumnHeader();
			this.chTitle = new System.Windows.Forms.ColumnHeader();
			this.chSeriesNumber = new System.Windows.Forms.ColumnHeader();
			this.chSubtitle = new System.Windows.Forms.ColumnHeader();
			this.chDuration = new System.Windows.Forms.ColumnHeader();
			this.chDeadline = new System.Windows.Forms.ColumnHeader();
			this.inputBoxDialog1 = new Yusen.GExplorer.UserInterfaces.InputBoxDialog();
			this.timerSelectionDelay = new System.Windows.Forms.Timer(this.components);
			this.cmsContents.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmsContents
			// 
			this.cmsContents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCmsPlay,
            this.toolStripMenuItem3,
            this.tsmiCmsRearrange,
            this.tspmiCmsCopyToAnother,
            this.tspmiCmsMoveToAnother,
            this.tsmiCmsDelete,
            this.toolStripMenuItem4,
            this.tsmiCmsCopyName,
            this.tsmiCmsCopyUri,
            this.tsmiCmsCopyNameAndUri,
            this.tscpmiCmsCopyOtherProperties,
            this.toolStripMenuItem7,
            this.tsecmiCmsCommand});
			this.cmsContents.Name = "contextMenuStrip1";
			this.cmsContents.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.cmsContents.Size = new System.Drawing.Size(248, 242);
			// 
			// tsmiCmsPlay
			// 
			this.tsmiCmsPlay.Image = global::Yusen.GExplorer.Properties.Resources.Play;
			this.tsmiCmsPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsPlay.Name = "tsmiCmsPlay";
			this.tsmiCmsPlay.Size = new System.Drawing.Size(247, 22);
			this.tsmiCmsPlay.Text = "再生(&P)";
			this.tsmiCmsPlay.Click += new System.EventHandler(this.tsmiCmsPlay_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(244, 6);
			// 
			// tsmiCmsRearrange
			// 
			this.tsmiCmsRearrange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCmsRearrangeToTop,
            this.tsmiCmsRearrangeUp,
            this.tsmiCmsRearrangeDown,
            this.tsmiCmsRearrangeToBottom});
			this.tsmiCmsRearrange.Name = "tsmiCmsRearrange";
			this.tsmiCmsRearrange.Size = new System.Drawing.Size(247, 22);
			this.tsmiCmsRearrange.Text = "リスト内を移動(&R)";
			// 
			// tsmiCmsRearrangeToTop
			// 
			this.tsmiCmsRearrangeToTop.Name = "tsmiCmsRearrangeToTop";
			this.tsmiCmsRearrangeToTop.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
						| System.Windows.Forms.Keys.Up)));
			this.tsmiCmsRearrangeToTop.Size = new System.Drawing.Size(240, 22);
			this.tsmiCmsRearrangeToTop.Text = "最上部へ(&T)";
			this.tsmiCmsRearrangeToTop.Click += new System.EventHandler(this.tsmiCmsRearrangeToTop_Click);
			// 
			// tsmiCmsRearrangeUp
			// 
			this.tsmiCmsRearrangeUp.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_moveup;
			this.tsmiCmsRearrangeUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsRearrangeUp.Name = "tsmiCmsRearrangeUp";
			this.tsmiCmsRearrangeUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Up)));
			this.tsmiCmsRearrangeUp.Size = new System.Drawing.Size(240, 22);
			this.tsmiCmsRearrangeUp.Text = "上へ(&U)";
			this.tsmiCmsRearrangeUp.Click += new System.EventHandler(this.tsmiCmsRearrangeUp_Click);
			// 
			// tsmiCmsRearrangeDown
			// 
			this.tsmiCmsRearrangeDown.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_movedown;
			this.tsmiCmsRearrangeDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsRearrangeDown.Name = "tsmiCmsRearrangeDown";
			this.tsmiCmsRearrangeDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)));
			this.tsmiCmsRearrangeDown.Size = new System.Drawing.Size(240, 22);
			this.tsmiCmsRearrangeDown.Text = "下へ(&D)";
			this.tsmiCmsRearrangeDown.Click += new System.EventHandler(this.tsmiCmsRearrangeDown_Click);
			// 
			// tsmiCmsRearrangeToBottom
			// 
			this.tsmiCmsRearrangeToBottom.Name = "tsmiCmsRearrangeToBottom";
			this.tsmiCmsRearrangeToBottom.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
						| System.Windows.Forms.Keys.Down)));
			this.tsmiCmsRearrangeToBottom.Size = new System.Drawing.Size(240, 22);
			this.tsmiCmsRearrangeToBottom.Text = "最下部へ(&B)";
			this.tsmiCmsRearrangeToBottom.Click += new System.EventHandler(this.tsmiCmsRearrangeToBottom_Click);
			// 
			// tspmiCmsCopyToAnother
			// 
			this.tspmiCmsCopyToAnother.Name = "tspmiCmsCopyToAnother";
			this.tspmiCmsCopyToAnother.Size = new System.Drawing.Size(247, 22);
			this.tspmiCmsCopyToAnother.Text = "別のリストに複製(&C)";
			this.tspmiCmsCopyToAnother.PlaylistSelected += new System.EventHandler(this.tspmiCmsCopyToAnother_PlaylistSelected);
			// 
			// tspmiCmsMoveToAnother
			// 
			this.tspmiCmsMoveToAnother.Name = "tspmiCmsMoveToAnother";
			this.tspmiCmsMoveToAnother.Size = new System.Drawing.Size(247, 22);
			this.tspmiCmsMoveToAnother.Text = "別のリストに移動(&M)";
			this.tspmiCmsMoveToAnother.PlaylistSelected += new System.EventHandler(this.tspmiCmsMoveToAnother_PlaylistSelected);
			// 
			// tsmiCmsDelete
			// 
			this.tsmiCmsDelete.Image = global::Yusen.GExplorer.Properties.Resources.Delete;
			this.tsmiCmsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsDelete.Name = "tsmiCmsDelete";
			this.tsmiCmsDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiCmsDelete.Size = new System.Drawing.Size(247, 22);
			this.tsmiCmsDelete.Text = "削除(&D)";
			this.tsmiCmsDelete.Click += new System.EventHandler(this.tsmiCmsDelete_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(244, 6);
			// 
			// tsmiCmsCopyName
			// 
			this.tsmiCmsCopyName.Name = "tsmiCmsCopyName";
			this.tsmiCmsCopyName.Size = new System.Drawing.Size(247, 22);
			this.tsmiCmsCopyName.Text = "名前をコピー(&N)";
			this.tsmiCmsCopyName.Click += new System.EventHandler(this.tsmiCmsCopyName_Click);
			// 
			// tsmiCmsCopyUri
			// 
			this.tsmiCmsCopyUri.Name = "tsmiCmsCopyUri";
			this.tsmiCmsCopyUri.Size = new System.Drawing.Size(247, 22);
			this.tsmiCmsCopyUri.Text = "URIをコピー(&U)";
			this.tsmiCmsCopyUri.Click += new System.EventHandler(this.tsmiCmsCopyUri_Click);
			// 
			// tsmiCmsCopyNameAndUri
			// 
			this.tsmiCmsCopyNameAndUri.Image = global::Yusen.GExplorer.Properties.Resources.Copy;
			this.tsmiCmsCopyNameAndUri.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCmsCopyNameAndUri.Name = "tsmiCmsCopyNameAndUri";
			this.tsmiCmsCopyNameAndUri.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsmiCmsCopyNameAndUri.Size = new System.Drawing.Size(247, 22);
			this.tsmiCmsCopyNameAndUri.Text = "名前とURIをコピー(&B)";
			this.tsmiCmsCopyNameAndUri.Click += new System.EventHandler(this.tsmiCmsCopyNameAndUri_Click);
			// 
			// tscpmiCmsCopyOtherProperties
			// 
			this.tscpmiCmsCopyOtherProperties.Name = "tscpmiCmsCopyOtherProperties";
			this.tscpmiCmsCopyOtherProperties.Size = new System.Drawing.Size(247, 22);
			this.tscpmiCmsCopyOtherProperties.Text = "その他のコピー(&O)";
			this.tscpmiCmsCopyOtherProperties.PropertySelected += new System.EventHandler(this.tscpmiCmsCopyOtherProperties_PropertySelected);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(244, 6);
			// 
			// tsecmiCmsCommand
			// 
			this.tsecmiCmsCommand.Name = "tsecmiCmsCommand";
			this.tsecmiCmsCommand.Size = new System.Drawing.Size(247, 22);
			this.tsecmiCmsCommand.Text = "外部コマンド(&X)";
			this.tsecmiCmsCommand.ExternalCommandSelected += new System.EventHandler(this.tsecmiCmsCommand_ExternalCommandSelected);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPVP});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.menuStrip1.Size = new System.Drawing.Size(393, 24);
			this.menuStrip1.Stretch = false;
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			this.menuStrip1.Visible = false;
			// 
			// tsmiPVP
			// 
			this.tsmiPVP.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSort,
            this.tsmiRenamePlaylist,
            this.toolStripMenuItem2,
            this.tsmiPlay,
            this.toolStripMenuItem1,
            this.tsmiRearrange,
            this.tspmiCopyToAnother,
            this.tspmiMoveToAnother,
            this.tsmiDelete,
            this.toolStripMenuItem5,
            this.tsmiCopyName,
            this.tsmiCopyUri,
            this.tsmiCopyNameAndUri,
            this.tscpmiCopyOtherProperties,
            this.toolStripMenuItem6,
            this.tsecmiCommand});
			this.tsmiPVP.Name = "tsmiPVP";
			this.tsmiPVP.Size = new System.Drawing.Size(42, 20);
			this.tsmiPVP.Text = "PVP";
			// 
			// tsmiSort
			// 
			this.tsmiSort.Name = "tsmiSort";
			this.tsmiSort.Size = new System.Drawing.Size(225, 22);
			this.tsmiSort.Text = "ソート(&S)";
			// 
			// tsmiRenamePlaylist
			// 
			this.tsmiRenamePlaylist.Name = "tsmiRenamePlaylist";
			this.tsmiRenamePlaylist.Size = new System.Drawing.Size(225, 22);
			this.tsmiRenamePlaylist.Text = "プレイリスト名の変更(&L)...";
			this.tsmiRenamePlaylist.Click += new System.EventHandler(this.tsmiRenamePlaylist_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(222, 6);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Image = global::Yusen.GExplorer.Properties.Resources.Play;
			this.tsmiPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.Size = new System.Drawing.Size(225, 22);
			this.tsmiPlay.Text = "再生(&P)";
			this.tsmiPlay.Click += new System.EventHandler(this.tsmiPlay_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(222, 6);
			// 
			// tsmiRearrange
			// 
			this.tsmiRearrange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRearrangeToTop,
            this.tsmiRearrangeUp,
            this.tsmiRearrangeDown,
            this.tsmiRearrangeToBottom});
			this.tsmiRearrange.Name = "tsmiRearrange";
			this.tsmiRearrange.Size = new System.Drawing.Size(225, 22);
			this.tsmiRearrange.Text = "リスト内を移動(&R)";
			// 
			// tsmiRearrangeToTop
			// 
			this.tsmiRearrangeToTop.Name = "tsmiRearrangeToTop";
			this.tsmiRearrangeToTop.Size = new System.Drawing.Size(142, 22);
			this.tsmiRearrangeToTop.Text = "最上部へ(&T)";
			this.tsmiRearrangeToTop.Click += new System.EventHandler(this.tsmiRearrangeToTop_Click);
			// 
			// tsmiRearrangeUp
			// 
			this.tsmiRearrangeUp.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_moveup;
			this.tsmiRearrangeUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiRearrangeUp.Name = "tsmiRearrangeUp";
			this.tsmiRearrangeUp.Size = new System.Drawing.Size(142, 22);
			this.tsmiRearrangeUp.Text = "上へ(&U)";
			this.tsmiRearrangeUp.Click += new System.EventHandler(this.tsmiRearrangeUp_Click);
			// 
			// tsmiRearrangeDown
			// 
			this.tsmiRearrangeDown.Image = global::Yusen.GExplorer.Properties.Resources.BuilderDialog_movedown;
			this.tsmiRearrangeDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiRearrangeDown.Name = "tsmiRearrangeDown";
			this.tsmiRearrangeDown.Size = new System.Drawing.Size(142, 22);
			this.tsmiRearrangeDown.Text = "下へ(&D)";
			this.tsmiRearrangeDown.Click += new System.EventHandler(this.tsmiRearrangeDown_Click);
			// 
			// tsmiRearrangeToBottom
			// 
			this.tsmiRearrangeToBottom.Name = "tsmiRearrangeToBottom";
			this.tsmiRearrangeToBottom.Size = new System.Drawing.Size(142, 22);
			this.tsmiRearrangeToBottom.Text = "最下部へ(&B)";
			this.tsmiRearrangeToBottom.Click += new System.EventHandler(this.tsmiRearrangeToBottom_Click);
			// 
			// tspmiCopyToAnother
			// 
			this.tspmiCopyToAnother.Name = "tspmiCopyToAnother";
			this.tspmiCopyToAnother.Size = new System.Drawing.Size(225, 22);
			this.tspmiCopyToAnother.Text = "別のリストに複製(&C)";
			this.tspmiCopyToAnother.PlaylistSelected += new System.EventHandler(this.tspmiCopyToAnother_PlaylistSelected);
			// 
			// tspmiMoveToAnother
			// 
			this.tspmiMoveToAnother.Name = "tspmiMoveToAnother";
			this.tspmiMoveToAnother.Size = new System.Drawing.Size(225, 22);
			this.tspmiMoveToAnother.Text = "別のリストに移動(&M)";
			this.tspmiMoveToAnother.PlaylistSelected += new System.EventHandler(this.tspmiMoveToAnother_PlaylistSelected);
			// 
			// tsmiDelete
			// 
			this.tsmiDelete.Image = global::Yusen.GExplorer.Properties.Resources.Delete;
			this.tsmiDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiDelete.Name = "tsmiDelete";
			this.tsmiDelete.Size = new System.Drawing.Size(225, 22);
			this.tsmiDelete.Text = "削除(&D)";
			this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(222, 6);
			// 
			// tsmiCopyName
			// 
			this.tsmiCopyName.Name = "tsmiCopyName";
			this.tsmiCopyName.Size = new System.Drawing.Size(225, 22);
			this.tsmiCopyName.Text = "名前をコピー(&N)";
			this.tsmiCopyName.Click += new System.EventHandler(this.tsmiCopyName_Click);
			// 
			// tsmiCopyUri
			// 
			this.tsmiCopyUri.Name = "tsmiCopyUri";
			this.tsmiCopyUri.Size = new System.Drawing.Size(225, 22);
			this.tsmiCopyUri.Text = "URIをコピー(&U)";
			this.tsmiCopyUri.Click += new System.EventHandler(this.tsmiCopyUri_Click);
			// 
			// tsmiCopyNameAndUri
			// 
			this.tsmiCopyNameAndUri.Image = global::Yusen.GExplorer.Properties.Resources.Copy;
			this.tsmiCopyNameAndUri.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiCopyNameAndUri.Name = "tsmiCopyNameAndUri";
			this.tsmiCopyNameAndUri.Size = new System.Drawing.Size(225, 22);
			this.tsmiCopyNameAndUri.Text = "名前とURIをコピー(&B)";
			this.tsmiCopyNameAndUri.Click += new System.EventHandler(this.tsmiCopyNameAndUri_Click);
			// 
			// tscpmiCopyOtherProperties
			// 
			this.tscpmiCopyOtherProperties.Name = "tscpmiCopyOtherProperties";
			this.tscpmiCopyOtherProperties.Size = new System.Drawing.Size(225, 22);
			this.tscpmiCopyOtherProperties.Text = "その他のコピー(&O)";
			this.tscpmiCopyOtherProperties.PropertySelected += new System.EventHandler(this.tscpmiCopyOtherProperties_PropertySelected);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(222, 6);
			// 
			// tsecmiCommand
			// 
			this.tsecmiCommand.Name = "tsecmiCommand";
			this.tsecmiCommand.Size = new System.Drawing.Size(225, 22);
			this.tsecmiCommand.Text = "外部コマンド(&X)";
			this.tsecmiCommand.ExternalCommandSelected += new System.EventHandler(this.tsecmiCommand_ExternalCommandSelected);
			// 
			// lvContents
			// 
			this.lvContents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chTitle,
            this.chSeriesNumber,
            this.chSubtitle,
            this.chDuration,
            this.chDeadline});
			this.lvContents.ContextMenuStrip = this.cmsContents;
			this.lvContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvContents.FullRowSelect = true;
			this.lvContents.HideSelection = false;
			this.lvContents.Location = new System.Drawing.Point(0, 0);
			this.lvContents.Name = "lvContents";
			this.lvContents.ShowGroups = false;
			this.lvContents.ShowItemToolTips = true;
			this.lvContents.Size = new System.Drawing.Size(456, 150);
			this.lvContents.TabIndex = 0;
			this.lvContents.UseCompatibleStateImageBehavior = false;
			this.lvContents.View = System.Windows.Forms.View.Details;
			this.lvContents.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvContents_ColumnWidthChanged);
			this.lvContents.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvContents_MouseDoubleClick);
			this.lvContents.SelectedIndexChanged += new System.EventHandler(this.lvContents_SelectedIndexChanged);
			this.lvContents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvContents_KeyDown);
			this.lvContents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvContents_ColumnClick);
			this.lvContents.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvContents_ItemSelectionChanged);
			// 
			// chId
			// 
			this.chId.Text = "contents_id";
			this.chId.Width = 66;
			// 
			// chTitle
			// 
			this.chTitle.Text = "タイトル";
			this.chTitle.Width = 80;
			// 
			// chSeriesNumber
			// 
			this.chSeriesNumber.Text = "シリーズ番号";
			this.chSeriesNumber.Width = 65;
			// 
			// chSubtitle
			// 
			this.chSubtitle.Text = "サブタイトル";
			this.chSubtitle.Width = 70;
			// 
			// chDuration
			// 
			this.chDuration.Text = "時間";
			this.chDuration.Width = 52;
			// 
			// chDeadline
			// 
			this.chDeadline.Text = "期限";
			this.chDeadline.Width = 80;
			// 
			// inputBoxDialog1
			// 
			this.inputBoxDialog1.Input = null;
			this.inputBoxDialog1.Message = null;
			this.inputBoxDialog1.Title = null;
			// 
			// timerSelectionDelay
			// 
			this.timerSelectionDelay.Interval = 10;
			this.timerSelectionDelay.Tick += new System.EventHandler(this.timerSelectionDelay_Tick);
			// 
			// PlaylistsViewPControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.lvContents);
			this.Name = "PlaylistsViewPControl";
			this.Size = new System.Drawing.Size(456, 150);
			this.Load += new System.EventHandler(this.PlaylistsViewPControl_Load);
			this.cmsContents.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DoubleBufferedListView lvContents;
		private System.Windows.Forms.ColumnHeader chId;
		private System.Windows.Forms.ColumnHeader chTitle;
		private System.Windows.Forms.ColumnHeader chSeriesNumber;
		private System.Windows.Forms.ColumnHeader chSubtitle;
		private System.Windows.Forms.ColumnHeader chDuration;
		private System.Windows.Forms.ColumnHeader chDeadline;
		private System.Windows.Forms.ContextMenuStrip cmsContents;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem tsmiPVP;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrange;
		private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsPlay;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrange;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsDelete;
		private ToolStripPlaylistMenuItem tspmiCmsCopyToAnother;
		private ToolStripPlaylistMenuItem tspmiCmsMoveToAnother;
		private ToolStripPlaylistMenuItem tspmiCopyToAnother;
		private ToolStripPlaylistMenuItem tspmiMoveToAnother;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrangeToTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrangeUp;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrangeDown;
		private System.Windows.Forms.ToolStripMenuItem tsmiRearrangeToBottom;
		private System.Windows.Forms.ToolStripMenuItem tsmiSort;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrangeToTop;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrangeUp;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrangeDown;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsRearrangeToBottom;
		private System.Windows.Forms.ToolStripMenuItem tsmiRenamePlaylist;
		private InputBoxDialog inputBoxDialog1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyNameAndUri;
		private ToolStripContentPropertyMenuItem tscpmiCopyOtherProperties;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private ToolStripExternalCommandMenuItem tsecmiCommand;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCopyName;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCopyUri;
		private System.Windows.Forms.ToolStripMenuItem tsmiCmsCopyNameAndUri;
		private ToolStripContentPropertyMenuItem tscpmiCmsCopyOtherProperties;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
		private ToolStripExternalCommandMenuItem tsecmiCmsCommand;
		private System.Windows.Forms.Timer timerSelectionDelay;
	}
}
