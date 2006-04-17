namespace Yusen.GExplorer {
	partial class NgFavContentsEditor {
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lblMessage = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.editorNg = new Yusen.GExplorer.ContentPredicatesEditControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.editorFav = new Yusen.GExplorer.ContentPredicatesEditControl();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.lblMessage, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(622, 274);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(3, 0);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(442, 36);
			this.lblMessage.TabIndex = 201;
			this.lblMessage.Text = "一覧で条件を選択してからDeleteキーを押すと削除できます．\r\nアプリケーションの動作速度の悪化を防ぐため，最終True日時の古い条件は削除してください．\r\n条" +
				"件の個数に食い違いがある場合は，高速化対象の条件に重複があることが考えられます．";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 45);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(616, 226);
			this.tabControl1.TabIndex = 202;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.editorNg);
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(608, 201);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "NGコンテンツ";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// editorNg
			// 
			this.editorNg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorNg.Location = new System.Drawing.Point(3, 3);
			this.editorNg.Name = "editorNg";
			this.editorNg.Size = new System.Drawing.Size(602, 195);
			this.editorNg.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.editorFav);
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(608, 201);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "FAVコンテンツ";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// editorFav
			// 
			this.editorFav.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorFav.Location = new System.Drawing.Point(3, 3);
			this.editorFav.Name = "editorFav";
			this.editorFav.Size = new System.Drawing.Size(602, 195);
			this.editorFav.TabIndex = 0;
			// 
			// NgFavContentsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(622, 274);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(550, 270);
			this.Name = "NgFavContentsEditor";
			this.ShowInTaskbar = false;
			this.Text = "NG/FAVコンテンツエディタ";
			this.Load += new System.EventHandler(this.NgContentsEditor_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private ContentPredicatesEditControl editorNg;
		private ContentPredicatesEditControl editorFav;
	}
}