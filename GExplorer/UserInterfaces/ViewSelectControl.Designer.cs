namespace Yusen.GExplorer.UserInterfaces {
	partial class ViewSelectControl {
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
			this.lboxVsc = new System.Windows.Forms.ListBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.btnStopCrawl = new System.Windows.Forms.Button();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lboxVsc
			// 
			this.lboxVsc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lboxVsc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lboxVsc.Location = new System.Drawing.Point(0, 0);
			this.lboxVsc.Name = "lboxVsc";
			this.lboxVsc.Size = new System.Drawing.Size(165, 238);
			this.lboxVsc.TabIndex = 0;
			this.lboxVsc.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lboxVsv_DrawItem);
			this.lboxVsc.DoubleClick += new System.EventHandler(this.lboxVsv_DoubleClick);
			this.lboxVsc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lboxVsv_KeyDown);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.IsSplitterFixed = true;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lboxVsc);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.btnStopCrawl);
			this.splitContainer1.Size = new System.Drawing.Size(165, 274);
			this.splitContainer1.SplitterDistance = 245;
			this.splitContainer1.TabIndex = 1;
			// 
			// btnStopCrawl
			// 
			this.btnStopCrawl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnStopCrawl.Location = new System.Drawing.Point(0, 0);
			this.btnStopCrawl.Name = "btnStopCrawl";
			this.btnStopCrawl.Size = new System.Drawing.Size(165, 25);
			this.btnStopCrawl.TabIndex = 0;
			this.btnStopCrawl.Text = "クロール中止(&A)";
			this.btnStopCrawl.UseVisualStyleBackColor = true;
			// 
			// ViewSelectControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ViewSelectControl";
			this.Size = new System.Drawing.Size(165, 274);
			this.Load += new System.EventHandler(this.ViewSelectView_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lboxVsc;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button btnStopCrawl;

	}
}
