namespace Yusen.GExplorer.UserInterfaces {
	partial class ToolStripClassificationRuleMenuItem {
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
			this.tsmiNgTitle = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTestRules = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tspmiFavTitle = new Yusen.GExplorer.UserInterfaces.ToolStripPlaylistMenuItem();
			// 
			// tsmiNgTitle
			// 
			this.tsmiNgTitle.Name = "tsmiNgTitle";
			this.tsmiNgTitle.Size = new System.Drawing.Size(152, 22);
			this.tsmiNgTitle.Text = "タイトルでNG(&N)";
			// 
			// tsmiTestRules
			// 
			this.tsmiTestRules.Name = "tsmiTestRules";
			this.tsmiTestRules.Size = new System.Drawing.Size(152, 22);
			this.tsmiTestRules.Text = "仕分けテスト(&T)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// tspmiFavTitle
			// 
			this.tspmiFavTitle.Name = "tspmiFavTitle";
			this.tspmiFavTitle.Size = new System.Drawing.Size(152, 22);
			this.tspmiFavTitle.Text = "タイトルでFAV(&F)";
			// 
			// ToolStripClassificationRuleMenuItem
			// 
			this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNgTitle,
            this.tspmiFavTitle,
            this.toolStripSeparator1,
            this.tsmiTestRules});

		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem tsmiNgTitle;
		private System.Windows.Forms.ToolStripMenuItem tsmiTestRules;
		private ToolStripPlaylistMenuItem tspmiFavTitle;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

	}
}
