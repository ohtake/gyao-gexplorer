namespace Yusen.GExplorer.OldApp {
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.editorNg = new Yusen.GExplorer.OldApp.ContentPredicatesEditControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.editorFav = new Yusen.GExplorer.OldApp.ContentPredicatesEditControl();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(622, 274);
			this.tabControl1.TabIndex = 202;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.editorNg);
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(614, 249);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "NGコンテンツ";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// editorNg
			// 
			this.editorNg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorNg.Location = new System.Drawing.Point(3, 3);
			this.editorNg.Name = "editorNg";
			this.editorNg.Size = new System.Drawing.Size(608, 243);
			this.editorNg.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.editorFav);
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(614, 249);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "FAVコンテンツ";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// editorFav
			// 
			this.editorFav.Dock = System.Windows.Forms.DockStyle.Fill;
			this.editorFav.Location = new System.Drawing.Point(3, 3);
			this.editorFav.Name = "editorFav";
			this.editorFav.Size = new System.Drawing.Size(608, 243);
			this.editorFav.TabIndex = 0;
			// 
			// NgFavContentsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(622, 274);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(580, 250);
			this.Name = "NgFavContentsEditor";
			this.ShowInTaskbar = false;
			this.Text = "NG/FAVコンテンツエディタ";
			this.Load += new System.EventHandler(this.NgContentsEditor_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private ContentPredicatesEditControl editorNg;
		private ContentPredicatesEditControl editorFav;
	}
}