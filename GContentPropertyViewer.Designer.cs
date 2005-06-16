namespace Yusen.GExplorer {
	partial class GContentPropertyViewer {
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
			this.tabContent = new System.Windows.Forms.TabPage();
			this.pgContent = new System.Windows.Forms.PropertyGrid();
			this.tabPackage = new System.Windows.Forms.TabPage();
			this.pgPackage = new System.Windows.Forms.PropertyGrid();
			this.tabGenre = new System.Windows.Forms.TabPage();
			this.pgGenre = new System.Windows.Forms.PropertyGrid();
			this.tabControl1.SuspendLayout();
			this.tabContent.SuspendLayout();
			this.tabPackage.SuspendLayout();
			this.tabGenre.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabContent);
			this.tabControl1.Controls.Add(this.tabPackage);
			this.tabControl1.Controls.Add(this.tabGenre);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(252, 284);
			this.tabControl1.TabIndex = 0;
			// 
			// tabContent
			// 
			this.tabContent.Controls.Add(this.pgContent);
			this.tabContent.Location = new System.Drawing.Point(4, 21);
			this.tabContent.Name = "tabContent";
			this.tabContent.Padding = new System.Windows.Forms.Padding(3);
			this.tabContent.Size = new System.Drawing.Size(244, 259);
			this.tabContent.TabIndex = 0;
			this.tabContent.Text = "コンテント";
			// 
			// pgContent
			// 
			this.pgContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgContent.Location = new System.Drawing.Point(3, 3);
			this.pgContent.Name = "pgContent";
			this.pgContent.Size = new System.Drawing.Size(238, 253);
			this.pgContent.TabIndex = 0;
			this.pgContent.ToolbarVisible = false;
			// 
			// tabPackage
			// 
			this.tabPackage.Controls.Add(this.pgPackage);
			this.tabPackage.Location = new System.Drawing.Point(4, 21);
			this.tabPackage.Name = "tabPackage";
			this.tabPackage.Padding = new System.Windows.Forms.Padding(3);
			this.tabPackage.Size = new System.Drawing.Size(264, 259);
			this.tabPackage.TabIndex = 1;
			this.tabPackage.Text = "パッケージ";
			// 
			// pgPackage
			// 
			this.pgPackage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgPackage.Location = new System.Drawing.Point(3, 3);
			this.pgPackage.Name = "pgPackage";
			this.pgPackage.Size = new System.Drawing.Size(258, 253);
			this.pgPackage.TabIndex = 0;
			this.pgPackage.ToolbarVisible = false;
			// 
			// tabGenre
			// 
			this.tabGenre.Controls.Add(this.pgGenre);
			this.tabGenre.Location = new System.Drawing.Point(4, 21);
			this.tabGenre.Name = "tabGenre";
			this.tabGenre.Padding = new System.Windows.Forms.Padding(3);
			this.tabGenre.Size = new System.Drawing.Size(264, 259);
			this.tabGenre.TabIndex = 2;
			this.tabGenre.Text = "ジャンル";
			// 
			// pgGenre
			// 
			this.pgGenre.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgGenre.Location = new System.Drawing.Point(3, 3);
			this.pgGenre.Name = "pgGenre";
			this.pgGenre.Size = new System.Drawing.Size(258, 253);
			this.pgGenre.TabIndex = 0;
			this.pgGenre.ToolbarVisible = false;
			// 
			// GContentPropertyViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(252, 284);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "GContentPropertyViewer";
			this.Text = "GContentPropertyViewer";
			this.tabControl1.ResumeLayout(false);
			this.tabContent.ResumeLayout(false);
			this.tabPackage.ResumeLayout(false);
			this.tabGenre.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabContent;
		private System.Windows.Forms.TabPage tabPackage;
		private System.Windows.Forms.TabPage tabGenre;
		private System.Windows.Forms.PropertyGrid pgContent;
		private System.Windows.Forms.PropertyGrid pgPackage;
		private System.Windows.Forms.PropertyGrid pgGenre;
	}
}