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
			this.tsmiDetail = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiPackage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSpecial = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiGenre = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.ilLarge = new System.Windows.Forms.ImageList(this.components);
			this.ilSmall = new System.Windows.Forms.ImageList(this.components);
			this.cmsListView.SuspendLayout();
			this.SuspendLayout();
			// 
			// lviewGenre
			// 
			this.lviewGenre.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lviewGenre.AllowColumnReorder = true;
			this.lviewGenre.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chId,
            this.chLimit,
            this.chEpisode,
            this.chLead});
			this.lviewGenre.ContextMenuStrip = this.cmsListView;
			this.lviewGenre.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lviewGenre.LargeImageList = this.ilLarge;
			this.lviewGenre.Location = new System.Drawing.Point(0, 0);
			this.lviewGenre.MultiSelect = false;
			this.lviewGenre.Name = "lviewGenre";
			this.lviewGenre.ShowItemToolTips = true;
			this.lviewGenre.Size = new System.Drawing.Size(572, 258);
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
			this.chLead.Width = 306;
			// 
			// cmsListView
			// 
			this.cmsListView.Enabled = true;
			this.cmsListView.GripMargin = new System.Windows.Forms.Padding(2);
			this.cmsListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPlay,
            this.tsmiDetail,
            this.tsmiPackage,
            this.tsmiSpecial,
            this.tsmiGenre,
            this.toolStripSeparator1,
            this.tsmiView,
            this.toolStripSeparator2,
            this.tsmiCommands});
			this.cmsListView.Location = new System.Drawing.Point(21, 36);
			this.cmsListView.Name = "contextMenuStrip1";
			this.cmsListView.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmsListView.Size = new System.Drawing.Size(218, 170);
			// 
			// tsmiPlay
			// 
			this.tsmiPlay.Name = "tsmiPlay";
			this.tsmiPlay.Text = "専用プレーヤで再生 (&O)";
			// 
			// tsmiDetail
			// 
			this.tsmiDetail.Name = "tsmiDetail";
			this.tsmiDetail.Text = "解説ページをIEで開く (&D)";
			// 
			// tsmiPackage
			// 
			this.tsmiPackage.Name = "tsmiPackage";
			this.tsmiPackage.Text = "パッケージページをIEで開く (&P)";
			// 
			// tsmiSpecial
			// 
			this.tsmiSpecial.Enabled = false;
			this.tsmiSpecial.Name = "tsmiSpecial";
			this.tsmiSpecial.Text = "特集ページをIEで開く (&S)";
			// 
			// tsmiGenre
			// 
			this.tsmiGenre.Name = "tsmiGenre";
			this.tsmiGenre.Text = "ジャンルページをIEで開く (&G)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// tsmiView
			// 
			this.tsmiView.Name = "tsmiView";
			this.tsmiView.Text = "表示形式 (&V)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			// 
			// tsmiCommands
			// 
			this.tsmiCommands.Enabled = false;
			this.tsmiCommands.Name = "tsmiCommands";
			this.tsmiCommands.Text = "外部コマンドみたなものの予定？";
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
			this.Size = new System.Drawing.Size(572, 258);
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
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsmiDetail;
		private System.Windows.Forms.ImageList ilSmall;
		private System.Windows.Forms.ImageList ilLarge;
	}
}
