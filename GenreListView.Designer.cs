namespace Yusen.GExplorer {
	partial class GenreListView {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenreListView));
			this.lviewGenre = new System.Windows.Forms.ListView();
			this.chId = new System.Windows.Forms.ColumnHeader();
			this.chLimit = new System.Windows.Forms.ColumnHeader();
			this.chEpisode = new System.Windows.Forms.ColumnHeader();
			this.chLead = new System.Windows.Forms.ColumnHeader();
			this.cmsListView = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiWMP = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiPackage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSpecial = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiGenre = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiMultipulSelect = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiProperty = new System.Windows.Forms.ToolStripMenuItem();
			this.ilLarge = new System.Windows.Forms.ImageList(this.components);
			this.ilSmall = new System.Windows.Forms.ImageList(this.components);
			this.cmsListView.SuspendLayout();
			this.SuspendLayout();
			// 
			// lviewGenre
			// 
			this.lviewGenre.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chLimit,
            this.chEpisode,
            this.chLead});
			this.lviewGenre.ContextMenuStrip = this.cmsListView;
			this.lviewGenre.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lviewGenre.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lviewGenre.LargeImageList = this.ilLarge;
			this.lviewGenre.Location = new System.Drawing.Point(0, 0);
			this.lviewGenre.MultiSelect = false;
			this.lviewGenre.Name = "lviewGenre";
			this.lviewGenre.ShowItemToolTips = true;
			this.lviewGenre.Size = new System.Drawing.Size(584, 258);
			this.lviewGenre.SmallImageList = this.ilSmall;
			this.lviewGenre.TabIndex = 0;
			this.lviewGenre.View = System.Windows.Forms.View.Details;
			// 
			// chId
			// 
			this.chId.Text = "contents_id";
			this.chId.Width = 90;
			// 
			// chLimit
			// 
			this.chLimit.Text = "配信終了日";
			this.chLimit.Width = 80;
			// 
			// chEpisode
			// 
			this.chEpisode.Text = "話";
			this.chEpisode.Width = 70;
			// 
			// chLead
			// 
			this.chLead.Text = "リード";
			this.chLead.Width = 320;
			// 
			// cmsListView
			// 
			this.cmsListView.Enabled = true;
			this.cmsListView.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPlay,
            this.tsmiWMP,
            this.tsmiDetail,
            this.toolStripSeparator1,
            this.tsmiPackage,
            this.tsmiSpecial,
            this.toolStripSeparator2,
            this.tsmiGenre,
            this.toolStripSeparator3,
            this.tsmiCommands,
            this.toolStripSeparator4,
            this.tsmiMultipulSelect,
            this.tsmiView,
            this.toolStripSeparator5,
            this.tsmiProperty});
			this.cmsListView.Location = new System.Drawing.Point(21, 36);
			this.cmsListView.Name = "contextMenuStrip1";
			this.cmsListView.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsListView.Size = new System.Drawing.Size(247, 254);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.Text = "専用プレーヤで再生 (&O)";
			// 
			// tsmiWMP
			// 
			this.tsmiWMP.Name = "tsmiWMP";
			this.tsmiWMP.Text = "WMPで再生 (&W)";
			// 
			// tsmiDetail
			// 
			this.tsmiDetail.Name = "tsmiDetail";
			this.tsmiDetail.Text = "解説ページをIEで開く (&D)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiPackage
			// 
			this.tsmiPackage.Name = "tsmiPackage";
			this.tsmiPackage.Text = "パッケージページをIEで開く (&P)";
			// 
			// tsmiSpecial
			// 
			this.tsmiSpecial.Name = "tsmiSpecial";
			this.tsmiSpecial.Text = "パッケージの特集ページをIEで開く (&S)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiGenre
			// 
			this.tsmiGenre.Name = "tsmiGenre";
			this.tsmiGenre.Text = "ジャンルページをIEで開く (&G)";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			// 
			// tsmiCommands
			// 
			this.tsmiCommands.Enabled = false;
			this.tsmiCommands.Name = "tsmiCommands";
			this.tsmiCommands.Text = "外部コマンドの予定 (&C)";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			// 
			// tsmiMultipulSelect
			// 
			this.tsmiMultipulSelect.CheckOnClick = true;
			this.tsmiMultipulSelect.Name = "tsmiMultipulSelect";
			this.tsmiMultipulSelect.Text = "複数選択を有効 (&M)";
			// 
			// tsmiView
			// 
			this.tsmiView.Name = "tsmiView";
			this.tsmiView.Text = "表示形式 (&V)";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			// 
			// tsmiProperty
			// 
			this.tsmiProperty.Name = "tsmiProperty";
			this.tsmiProperty.Text = "プロパティ (&R)";
			// 
			// ilLarge
			// 
			this.ilLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLarge.ImageStream")));
			this.ilLarge.Images.SetKeyName(0, "normal32.ico");
			this.ilLarge.Images.SetKeyName(1, "normal32new.ico");
			this.ilLarge.Images.SetKeyName(2, "special32.ico");
			this.ilLarge.Images.SetKeyName(3, "special32new.ico");
			// 
			// ilSmall
			// 
			this.ilSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmall.ImageStream")));
			this.ilSmall.Images.SetKeyName(0, "normal16.ico");
			this.ilSmall.Images.SetKeyName(1, "normal16new.ico");
			this.ilSmall.Images.SetKeyName(2, "special16.ico");
			this.ilSmall.Images.SetKeyName(3, "special16new.ico");
			// 
			// GenreListView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lviewGenre);
			this.Name = "GenreListView";
			this.Size = new System.Drawing.Size(584, 258);
			this.cmsListView.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lviewGenre;
		private System.Windows.Forms.ColumnHeader chId;
		private System.Windows.Forms.ColumnHeader chLimit;
		private System.Windows.Forms.ColumnHeader chEpisode;
		private System.Windows.Forms.ColumnHeader chLead;
		private System.Windows.Forms.ContextMenuStrip cmsListView;
		private System.Windows.Forms.ToolStripMenuItem tsmiPlay;
		private System.Windows.Forms.ToolStripMenuItem tsmiCommands;
		private System.Windows.Forms.ToolStripMenuItem tsmiView;
		private System.Windows.Forms.ToolStripMenuItem tsmiPackage;
		private System.Windows.Forms.ToolStripMenuItem tsmiGenre;
		private System.Windows.Forms.ToolStripMenuItem tsmiSpecial;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmiDetail;
		private System.Windows.Forms.ImageList ilSmall;
		private System.Windows.Forms.ImageList ilLarge;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiWMP;
		private System.Windows.Forms.ToolStripMenuItem tsmiMultipulSelect;
		private System.Windows.Forms.ToolStripMenuItem tsmiProperty;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
	}
}
