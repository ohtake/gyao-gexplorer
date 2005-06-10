namespace Yusen.GExplorer {
	partial class MainForm {
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.statstripMain = new System.Windows.Forms.StatusStrip();
			this.tsslCategoryStat = new System.Windows.Forms.ToolStripStatusLabel();
			this.numUserId = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblUserId = new System.Windows.Forms.Label();
			this.categoryViewer1 = new Yusen.GExplorer.CategoryViewer();
			this.categorySelector1 = new Yusen.GExplorer.CategorySelector();
			this.statstripMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numUserId)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statstripMain
			// 
			this.statstripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslCategoryStat});
			this.statstripMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
			this.statstripMain.Location = new System.Drawing.Point(0, 383);
			this.statstripMain.Name = "statstripMain";
			this.statstripMain.Size = new System.Drawing.Size(632, 23);
			this.statstripMain.TabIndex = 4;
			// 
			// tsslCategoryStat
			// 
			this.tsslCategoryStat.Name = "tsslCategoryStat";
			this.tsslCategoryStat.Text = "tsslCategoryStat";
			// 
			// numUserId
			// 
			this.numUserId.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.numUserId.Location = new System.Drawing.Point(246, 11);
			this.numUserId.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.numUserId.Name = "numUserId";
			this.numUserId.Size = new System.Drawing.Size(69, 19);
			this.numUserId.TabIndex = 3;
			this.numUserId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.categoryViewer1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(632, 383);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblUserId);
			this.panel1.Controls.Add(this.numUserId);
			this.panel1.Controls.Add(this.categorySelector1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(626, 44);
			this.panel1.TabIndex = 0;
			// 
			// lblUserId
			// 
			this.lblUserId.AutoSize = true;
			this.lblUserId.Location = new System.Drawing.Point(203, 14);
			this.lblUserId.Name = "lblUserId";
			this.lblUserId.Size = new System.Drawing.Size(39, 12);
			this.lblUserId.TabIndex = 2;
			this.lblUserId.Text = "&userNo";
			// 
			// categoryViewer1
			// 
			this.categoryViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.categoryViewer1.Location = new System.Drawing.Point(3, 53);
			this.categoryViewer1.Name = "categoryViewer1";
			this.categoryViewer1.Size = new System.Drawing.Size(626, 327);
			this.categoryViewer1.TabIndex = 4;
			// 
			// categorySelector1
			// 
			this.categorySelector1.Location = new System.Drawing.Point(9, 8);
			this.categorySelector1.Name = "categorySelector1";
			this.categorySelector1.Size = new System.Drawing.Size(170, 27);
			this.categorySelector1.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(632, 406);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.statstripMain);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.statstripMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numUserId)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statstripMain;
		private System.Windows.Forms.ToolStripStatusLabel tsslCategoryStat;
		private System.Windows.Forms.NumericUpDown numUserId;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblUserId;
		private CategorySelector categorySelector1;
		private CategoryViewer categoryViewer1;
	}
}

