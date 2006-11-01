namespace Yusen.GExplorer.UserInterfaces {
	partial class CacheViewerForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabpGenre = new System.Windows.Forms.TabPage();
			this.dgcGenre = new System.Windows.Forms.DataGridView();
			this.genreKeyDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.genreNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rootDirectoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.imageDirectoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.genreColorRedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.genreColorGreenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.genreColorBlueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gGenreBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.gDataSet = new Yusen.GExplorer.GyaoModel.GDataSet();
			this.tabpPackage = new System.Windows.Forms.TabPage();
			this.dgvPackage = new System.Windows.Forms.DataGridView();
			this.packageKeyDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.genreKeyDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.packageTitleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.packageCatchDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.packageTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.createdDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lastModifiedDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gPackageBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.tabpContents = new System.Windows.Forms.TabPage();
			this.dgvContent = new System.Windows.Forms.DataGridView();
			this.contentKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.packageKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.genreKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.seriesNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.subtitleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.summaryHtmlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.durationValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.deadlineTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lastModifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gContentBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.tabControl1.SuspendLayout();
			this.tabpGenre.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgcGenre)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gGenreBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gDataSet)).BeginInit();
			this.tabpPackage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvPackage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gPackageBindingSource)).BeginInit();
			this.tabpContents.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvContent)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gContentBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabpGenre);
			this.tabControl1.Controls.Add(this.tabpPackage);
			this.tabControl1.Controls.Add(this.tabpContents);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(792, 446);
			this.tabControl1.TabIndex = 0;
			// 
			// tabpGenre
			// 
			this.tabpGenre.Controls.Add(this.dgcGenre);
			this.tabpGenre.Location = new System.Drawing.Point(4, 21);
			this.tabpGenre.Name = "tabpGenre";
			this.tabpGenre.Padding = new System.Windows.Forms.Padding(3);
			this.tabpGenre.Size = new System.Drawing.Size(784, 421);
			this.tabpGenre.TabIndex = 0;
			this.tabpGenre.Text = "ジャンル";
			this.tabpGenre.UseVisualStyleBackColor = true;
			// 
			// dgcGenre
			// 
			this.dgcGenre.AutoGenerateColumns = false;
			this.dgcGenre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgcGenre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.genreKeyDataGridViewTextBoxColumn2,
            this.genreNameDataGridViewTextBoxColumn,
            this.rootDirectoryDataGridViewTextBoxColumn,
            this.imageDirectoryDataGridViewTextBoxColumn,
            this.genreColorRedDataGridViewTextBoxColumn,
            this.genreColorGreenDataGridViewTextBoxColumn,
            this.genreColorBlueDataGridViewTextBoxColumn});
			this.dgcGenre.DataSource = this.gGenreBindingSource;
			this.dgcGenre.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgcGenre.Location = new System.Drawing.Point(3, 3);
			this.dgcGenre.Name = "dgcGenre";
			this.dgcGenre.RowTemplate.Height = 21;
			this.dgcGenre.Size = new System.Drawing.Size(778, 415);
			this.dgcGenre.TabIndex = 0;
			// 
			// genreKeyDataGridViewTextBoxColumn2
			// 
			this.genreKeyDataGridViewTextBoxColumn2.DataPropertyName = "GenreKey";
			this.genreKeyDataGridViewTextBoxColumn2.HeaderText = "GenreKey";
			this.genreKeyDataGridViewTextBoxColumn2.Name = "genreKeyDataGridViewTextBoxColumn2";
			// 
			// genreNameDataGridViewTextBoxColumn
			// 
			this.genreNameDataGridViewTextBoxColumn.DataPropertyName = "GenreName";
			this.genreNameDataGridViewTextBoxColumn.HeaderText = "GenreName";
			this.genreNameDataGridViewTextBoxColumn.Name = "genreNameDataGridViewTextBoxColumn";
			// 
			// rootDirectoryDataGridViewTextBoxColumn
			// 
			this.rootDirectoryDataGridViewTextBoxColumn.DataPropertyName = "RootDirectory";
			this.rootDirectoryDataGridViewTextBoxColumn.HeaderText = "RootDirectory";
			this.rootDirectoryDataGridViewTextBoxColumn.Name = "rootDirectoryDataGridViewTextBoxColumn";
			// 
			// imageDirectoryDataGridViewTextBoxColumn
			// 
			this.imageDirectoryDataGridViewTextBoxColumn.DataPropertyName = "ImageDirectory";
			this.imageDirectoryDataGridViewTextBoxColumn.HeaderText = "ImageDirectory";
			this.imageDirectoryDataGridViewTextBoxColumn.Name = "imageDirectoryDataGridViewTextBoxColumn";
			// 
			// genreColorRedDataGridViewTextBoxColumn
			// 
			this.genreColorRedDataGridViewTextBoxColumn.DataPropertyName = "GenreColorRed";
			this.genreColorRedDataGridViewTextBoxColumn.HeaderText = "GenreColorRed";
			this.genreColorRedDataGridViewTextBoxColumn.Name = "genreColorRedDataGridViewTextBoxColumn";
			// 
			// genreColorGreenDataGridViewTextBoxColumn
			// 
			this.genreColorGreenDataGridViewTextBoxColumn.DataPropertyName = "GenreColorGreen";
			this.genreColorGreenDataGridViewTextBoxColumn.HeaderText = "GenreColorGreen";
			this.genreColorGreenDataGridViewTextBoxColumn.Name = "genreColorGreenDataGridViewTextBoxColumn";
			// 
			// genreColorBlueDataGridViewTextBoxColumn
			// 
			this.genreColorBlueDataGridViewTextBoxColumn.DataPropertyName = "GenreColorBlue";
			this.genreColorBlueDataGridViewTextBoxColumn.HeaderText = "GenreColorBlue";
			this.genreColorBlueDataGridViewTextBoxColumn.Name = "genreColorBlueDataGridViewTextBoxColumn";
			// 
			// gGenreBindingSource
			// 
			this.gGenreBindingSource.DataMember = "GGenre";
			this.gGenreBindingSource.DataSource = this.gDataSet;
			// 
			// gDataSet
			// 
			this.gDataSet.DataSetName = "GDataSet";
			this.gDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// tabpPackage
			// 
			this.tabpPackage.Controls.Add(this.dgvPackage);
			this.tabpPackage.Location = new System.Drawing.Point(4, 21);
			this.tabpPackage.Name = "tabpPackage";
			this.tabpPackage.Padding = new System.Windows.Forms.Padding(3);
			this.tabpPackage.Size = new System.Drawing.Size(528, 350);
			this.tabpPackage.TabIndex = 1;
			this.tabpPackage.Text = "パッケージ";
			this.tabpPackage.UseVisualStyleBackColor = true;
			// 
			// dgvPackage
			// 
			this.dgvPackage.AutoGenerateColumns = false;
			this.dgvPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvPackage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.packageKeyDataGridViewTextBoxColumn1,
            this.genreKeyDataGridViewTextBoxColumn1,
            this.packageTitleDataGridViewTextBoxColumn,
            this.packageCatchDataGridViewTextBoxColumn,
            this.packageTextDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn1,
            this.lastModifiedDataGridViewTextBoxColumn1});
			this.dgvPackage.DataSource = this.gPackageBindingSource;
			this.dgvPackage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvPackage.Location = new System.Drawing.Point(3, 3);
			this.dgvPackage.Name = "dgvPackage";
			this.dgvPackage.RowTemplate.Height = 21;
			this.dgvPackage.Size = new System.Drawing.Size(522, 344);
			this.dgvPackage.TabIndex = 0;
			// 
			// packageKeyDataGridViewTextBoxColumn1
			// 
			this.packageKeyDataGridViewTextBoxColumn1.DataPropertyName = "PackageKey";
			this.packageKeyDataGridViewTextBoxColumn1.HeaderText = "PackageKey";
			this.packageKeyDataGridViewTextBoxColumn1.Name = "packageKeyDataGridViewTextBoxColumn1";
			// 
			// genreKeyDataGridViewTextBoxColumn1
			// 
			this.genreKeyDataGridViewTextBoxColumn1.DataPropertyName = "GenreKey";
			this.genreKeyDataGridViewTextBoxColumn1.HeaderText = "GenreKey";
			this.genreKeyDataGridViewTextBoxColumn1.Name = "genreKeyDataGridViewTextBoxColumn1";
			// 
			// packageTitleDataGridViewTextBoxColumn
			// 
			this.packageTitleDataGridViewTextBoxColumn.DataPropertyName = "PackageTitle";
			this.packageTitleDataGridViewTextBoxColumn.HeaderText = "PackageTitle";
			this.packageTitleDataGridViewTextBoxColumn.Name = "packageTitleDataGridViewTextBoxColumn";
			// 
			// packageCatchDataGridViewTextBoxColumn
			// 
			this.packageCatchDataGridViewTextBoxColumn.DataPropertyName = "PackageCatch";
			this.packageCatchDataGridViewTextBoxColumn.HeaderText = "PackageCatch";
			this.packageCatchDataGridViewTextBoxColumn.Name = "packageCatchDataGridViewTextBoxColumn";
			// 
			// packageTextDataGridViewTextBoxColumn
			// 
			this.packageTextDataGridViewTextBoxColumn.DataPropertyName = "PackageText";
			this.packageTextDataGridViewTextBoxColumn.HeaderText = "PackageText";
			this.packageTextDataGridViewTextBoxColumn.Name = "packageTextDataGridViewTextBoxColumn";
			// 
			// createdDataGridViewTextBoxColumn1
			// 
			this.createdDataGridViewTextBoxColumn1.DataPropertyName = "Created";
			this.createdDataGridViewTextBoxColumn1.HeaderText = "Created";
			this.createdDataGridViewTextBoxColumn1.Name = "createdDataGridViewTextBoxColumn1";
			// 
			// lastModifiedDataGridViewTextBoxColumn1
			// 
			this.lastModifiedDataGridViewTextBoxColumn1.DataPropertyName = "LastModified";
			this.lastModifiedDataGridViewTextBoxColumn1.HeaderText = "LastModified";
			this.lastModifiedDataGridViewTextBoxColumn1.Name = "lastModifiedDataGridViewTextBoxColumn1";
			// 
			// gPackageBindingSource
			// 
			this.gPackageBindingSource.DataMember = "GPackage";
			this.gPackageBindingSource.DataSource = this.gDataSet;
			// 
			// tabpContents
			// 
			this.tabpContents.Controls.Add(this.dgvContent);
			this.tabpContents.Location = new System.Drawing.Point(4, 21);
			this.tabpContents.Name = "tabpContents";
			this.tabpContents.Padding = new System.Windows.Forms.Padding(3);
			this.tabpContents.Size = new System.Drawing.Size(528, 350);
			this.tabpContents.TabIndex = 2;
			this.tabpContents.Text = "コンテンツ";
			this.tabpContents.UseVisualStyleBackColor = true;
			// 
			// dgvContent
			// 
			this.dgvContent.AutoGenerateColumns = false;
			this.dgvContent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvContent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.contentKeyDataGridViewTextBoxColumn,
            this.packageKeyDataGridViewTextBoxColumn,
            this.genreKeyDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.seriesNumberDataGridViewTextBoxColumn,
            this.subtitleDataGridViewTextBoxColumn,
            this.summaryHtmlDataGridViewTextBoxColumn,
            this.durationValueDataGridViewTextBoxColumn,
            this.deadlineTextDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn,
            this.lastModifiedDataGridViewTextBoxColumn});
			this.dgvContent.DataSource = this.gContentBindingSource;
			this.dgvContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvContent.Location = new System.Drawing.Point(3, 3);
			this.dgvContent.Name = "dgvContent";
			this.dgvContent.RowTemplate.Height = 21;
			this.dgvContent.Size = new System.Drawing.Size(522, 344);
			this.dgvContent.TabIndex = 0;
			// 
			// contentKeyDataGridViewTextBoxColumn
			// 
			this.contentKeyDataGridViewTextBoxColumn.DataPropertyName = "ContentKey";
			this.contentKeyDataGridViewTextBoxColumn.HeaderText = "ContentKey";
			this.contentKeyDataGridViewTextBoxColumn.Name = "contentKeyDataGridViewTextBoxColumn";
			// 
			// packageKeyDataGridViewTextBoxColumn
			// 
			this.packageKeyDataGridViewTextBoxColumn.DataPropertyName = "PackageKey";
			this.packageKeyDataGridViewTextBoxColumn.HeaderText = "PackageKey";
			this.packageKeyDataGridViewTextBoxColumn.Name = "packageKeyDataGridViewTextBoxColumn";
			// 
			// genreKeyDataGridViewTextBoxColumn
			// 
			this.genreKeyDataGridViewTextBoxColumn.DataPropertyName = "GenreKey";
			this.genreKeyDataGridViewTextBoxColumn.HeaderText = "GenreKey";
			this.genreKeyDataGridViewTextBoxColumn.Name = "genreKeyDataGridViewTextBoxColumn";
			// 
			// titleDataGridViewTextBoxColumn
			// 
			this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
			this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
			this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
			// 
			// seriesNumberDataGridViewTextBoxColumn
			// 
			this.seriesNumberDataGridViewTextBoxColumn.DataPropertyName = "SeriesNumber";
			this.seriesNumberDataGridViewTextBoxColumn.HeaderText = "SeriesNumber";
			this.seriesNumberDataGridViewTextBoxColumn.Name = "seriesNumberDataGridViewTextBoxColumn";
			// 
			// subtitleDataGridViewTextBoxColumn
			// 
			this.subtitleDataGridViewTextBoxColumn.DataPropertyName = "Subtitle";
			this.subtitleDataGridViewTextBoxColumn.HeaderText = "Subtitle";
			this.subtitleDataGridViewTextBoxColumn.Name = "subtitleDataGridViewTextBoxColumn";
			// 
			// summaryHtmlDataGridViewTextBoxColumn
			// 
			this.summaryHtmlDataGridViewTextBoxColumn.DataPropertyName = "SummaryHtml";
			this.summaryHtmlDataGridViewTextBoxColumn.HeaderText = "SummaryHtml";
			this.summaryHtmlDataGridViewTextBoxColumn.Name = "summaryHtmlDataGridViewTextBoxColumn";
			// 
			// durationValueDataGridViewTextBoxColumn
			// 
			this.durationValueDataGridViewTextBoxColumn.DataPropertyName = "DurationValue";
			this.durationValueDataGridViewTextBoxColumn.HeaderText = "DurationValue";
			this.durationValueDataGridViewTextBoxColumn.Name = "durationValueDataGridViewTextBoxColumn";
			// 
			// deadlineTextDataGridViewTextBoxColumn
			// 
			this.deadlineTextDataGridViewTextBoxColumn.DataPropertyName = "DeadlineText";
			this.deadlineTextDataGridViewTextBoxColumn.HeaderText = "DeadlineText";
			this.deadlineTextDataGridViewTextBoxColumn.Name = "deadlineTextDataGridViewTextBoxColumn";
			// 
			// createdDataGridViewTextBoxColumn
			// 
			this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
			this.createdDataGridViewTextBoxColumn.HeaderText = "Created";
			this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
			// 
			// lastModifiedDataGridViewTextBoxColumn
			// 
			this.lastModifiedDataGridViewTextBoxColumn.DataPropertyName = "LastModified";
			this.lastModifiedDataGridViewTextBoxColumn.HeaderText = "LastModified";
			this.lastModifiedDataGridViewTextBoxColumn.Name = "lastModifiedDataGridViewTextBoxColumn";
			// 
			// gContentBindingSource
			// 
			this.gContentBindingSource.DataMember = "GContent";
			this.gContentBindingSource.DataSource = this.gDataSet;
			// 
			// CacheViewerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 446);
			this.Controls.Add(this.tabControl1);
			this.Name = "CacheViewerForm";
			this.Text = "キャッシュビューア";
			this.Load += new System.EventHandler(this.CacheViewerForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabpGenre.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgcGenre)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gGenreBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gDataSet)).EndInit();
			this.tabpPackage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvPackage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gPackageBindingSource)).EndInit();
			this.tabpContents.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvContent)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gContentBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabpGenre;
		private System.Windows.Forms.TabPage tabpPackage;
		private System.Windows.Forms.TabPage tabpContents;
		private System.Windows.Forms.DataGridView dgvPackage;
		private System.Windows.Forms.DataGridViewTextBoxColumn packageKeyDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn genreKeyDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn packageTitleDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn packageCatchDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn packageTextDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn lastModifiedDataGridViewTextBoxColumn1;
		private System.Windows.Forms.BindingSource gPackageBindingSource;
		private Yusen.GExplorer.GyaoModel.GDataSet gDataSet;
		private System.Windows.Forms.DataGridView dgvContent;
		private System.Windows.Forms.DataGridViewTextBoxColumn contentKeyDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn packageKeyDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn genreKeyDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn seriesNumberDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn subtitleDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn summaryHtmlDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn durationValueDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn deadlineTextDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn lastModifiedDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource gContentBindingSource;
		private System.Windows.Forms.DataGridView dgcGenre;
		private System.Windows.Forms.DataGridViewTextBoxColumn genreKeyDataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn genreNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn rootDirectoryDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn imageDirectoryDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn genreColorRedDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn genreColorGreenDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn genreColorBlueDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource gGenreBindingSource;
	}
}