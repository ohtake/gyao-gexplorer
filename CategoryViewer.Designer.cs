namespace Yusen.GExplorer {
	partial class CategoryViewer {
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
			this.dgviewTop = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabctrlRoot = new System.Windows.Forms.TabControl();
			this.tabNormal = new System.Windows.Forms.TabPage();
			this.tabSeries = new System.Windows.Forms.TabPage();
			this.txtSeries = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabSpecial = new System.Windows.Forms.TabPage();
			this.txtSpecial = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tabMisc = new System.Windows.Forms.TabPage();
			this.btnBrowse = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgviewTop)).BeginInit();
			this.tabctrlRoot.SuspendLayout();
			this.tabNormal.SuspendLayout();
			this.tabSeries.SuspendLayout();
			this.tabSpecial.SuspendLayout();
			this.tabMisc.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgviewTop
			// 
			this.dgviewTop.AllowUserToAddRows = false;
			this.dgviewTop.AllowUserToDeleteRows = false;
			this.dgviewTop.AllowUserToOrderColumns = true;
			this.dgviewTop.Columns.Add(this.dataGridViewTextBoxColumn1);
			this.dgviewTop.Columns.Add(this.dataGridViewCheckBoxColumn1);
			this.dgviewTop.Columns.Add(this.dataGridViewTextBoxColumn2);
			this.dgviewTop.Columns.Add(this.dataGridViewTextBoxColumn3);
			this.dgviewTop.Columns.Add(this.dataGridViewTextBoxColumn4);
			this.dgviewTop.Columns.Add(this.dataGridViewTextBoxColumn5);
			this.dgviewTop.Columns.Add(this.dataGridViewTextBoxColumn6);
			this.dgviewTop.Columns.Add(this.dataGridViewTextBoxColumn7);
			this.dgviewTop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgviewTop.Location = new System.Drawing.Point(3, 3);
			this.dgviewTop.Name = "dgviewTop";
			this.dgviewTop.ReadOnly = true;
			this.dgviewTop.RowTemplate.Height = 21;
			this.dgviewTop.Size = new System.Drawing.Size(606, 279);
			this.dgviewTop.TabIndex = 0;
			this.dgviewTop.Text = "dataGridView1";
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "VideoItem";
			this.dataGridViewTextBoxColumn1.Name = "VideoItem";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Visible = false;
			// 
			// dataGridViewCheckBoxColumn1
			// 
			this.dataGridViewCheckBoxColumn1.HeaderText = "New";
			this.dataGridViewCheckBoxColumn1.Name = "New";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			this.dataGridViewCheckBoxColumn1.Width = 30;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.HeaderText = "ID";
			this.dataGridViewTextBoxColumn2.Name = "ID";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.Width = 40;
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.HeaderText = "配信終了日";
			this.dataGridViewTextBoxColumn3.Name = "配信終了日";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = 70;
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.HeaderText = "サブジャンル";
			this.dataGridViewTextBoxColumn4.Name = "サブジャンル";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Width = 80;
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.HeaderText = "パック";
			this.dataGridViewTextBoxColumn5.Name = "パック";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Width = 80;
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.HeaderText = "話";
			this.dataGridViewTextBoxColumn6.Name = "話";
			this.dataGridViewTextBoxColumn6.ReadOnly = true;
			this.dataGridViewTextBoxColumn6.Width = 50;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.HeaderText = "リード";
			this.dataGridViewTextBoxColumn7.Name = "リード";
			this.dataGridViewTextBoxColumn7.ReadOnly = true;
			this.dataGridViewTextBoxColumn7.Width = 200;
			// 
			// tabctrlRoot
			// 
			this.tabctrlRoot.Controls.Add(this.tabNormal);
			this.tabctrlRoot.Controls.Add(this.tabSeries);
			this.tabctrlRoot.Controls.Add(this.tabSpecial);
			this.tabctrlRoot.Controls.Add(this.tabMisc);
			this.tabctrlRoot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabctrlRoot.Location = new System.Drawing.Point(0, 0);
			this.tabctrlRoot.Name = "tabctrlRoot";
			this.tabctrlRoot.SelectedIndex = 0;
			this.tabctrlRoot.Size = new System.Drawing.Size(620, 310);
			this.tabctrlRoot.TabIndex = 1;
			// 
			// tabNormal
			// 
			this.tabNormal.Controls.Add(this.dgviewTop);
			this.tabNormal.Location = new System.Drawing.Point(4, 21);
			this.tabNormal.Name = "tabNormal";
			this.tabNormal.Padding = new System.Windows.Forms.Padding(3);
			this.tabNormal.Size = new System.Drawing.Size(612, 285);
			this.tabNormal.TabIndex = 0;
			this.tabNormal.Text = "見る";
			// 
			// tabSeries
			// 
			this.tabSeries.Controls.Add(this.txtSeries);
			this.tabSeries.Controls.Add(this.label1);
			this.tabSeries.Location = new System.Drawing.Point(4, 21);
			this.tabSeries.Name = "tabSeries";
			this.tabSeries.Padding = new System.Windows.Forms.Padding(3);
			this.tabSeries.Size = new System.Drawing.Size(612, 285);
			this.tabSeries.TabIndex = 1;
			this.tabSeries.Text = "全シリーズを見る";
			// 
			// txtSeries
			// 
			this.txtSeries.Location = new System.Drawing.Point(21, 28);
			this.txtSeries.Multiline = true;
			this.txtSeries.Name = "txtSeries";
			this.txtSeries.ReadOnly = true;
			this.txtSeries.Size = new System.Drawing.Size(475, 157);
			this.txtSeries.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(20, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(170, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "「見る」のとうまく統合できればいいな";
			// 
			// tabSpecial
			// 
			this.tabSpecial.Controls.Add(this.txtSpecial);
			this.tabSpecial.Controls.Add(this.label2);
			this.tabSpecial.Location = new System.Drawing.Point(4, 21);
			this.tabSpecial.Name = "tabSpecial";
			this.tabSpecial.Size = new System.Drawing.Size(612, 285);
			this.tabSpecial.TabIndex = 2;
			this.tabSpecial.Text = "特集ページ";
			// 
			// txtSpecial
			// 
			this.txtSpecial.Location = new System.Drawing.Point(18, 72);
			this.txtSpecial.Multiline = true;
			this.txtSpecial.Name = "txtSpecial";
			this.txtSpecial.ReadOnly = true;
			this.txtSpecial.Size = new System.Drawing.Size(520, 159);
			this.txtSpecial.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(170, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "「見る」のとうまく統合できればいいな";
			// 
			// tabMisc
			// 
			this.tabMisc.Controls.Add(this.btnBrowse);
			this.tabMisc.Location = new System.Drawing.Point(4, 21);
			this.tabMisc.Name = "tabMisc";
			this.tabMisc.Size = new System.Drawing.Size(612, 285);
			this.tabMisc.TabIndex = 3;
			this.tabMisc.Text = "その他";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(27, 22);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(150, 23);
			this.btnBrowse.TabIndex = 0;
			this.btnBrowse.Text = "標準のブラウザで表示する";
			// 
			// CategoryViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabctrlRoot);
			this.Name = "CategoryViewer";
			this.Size = new System.Drawing.Size(620, 310);
			((System.ComponentModel.ISupportInitialize)(this.dgviewTop)).EndInit();
			this.tabctrlRoot.ResumeLayout(false);
			this.tabNormal.ResumeLayout(false);
			this.tabSeries.ResumeLayout(false);
			this.tabSeries.PerformLayout();
			this.tabSpecial.ResumeLayout(false);
			this.tabSpecial.PerformLayout();
			this.tabMisc.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgviewTop;
		private System.Windows.Forms.TabControl tabctrlRoot;
		private System.Windows.Forms.TabPage tabNormal;
		private System.Windows.Forms.TabPage tabSeries;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabSpecial;
		private System.Windows.Forms.TabPage tabMisc;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtSpecial;
		private System.Windows.Forms.TextBox txtSeries;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
	}
}
