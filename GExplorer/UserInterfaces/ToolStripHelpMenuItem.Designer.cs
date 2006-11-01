namespace Yusen.GExplorer.UserInterfaces {
	partial class ToolStripHelpMenuItem {
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
			this.tsmiReadMe = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiChangeLog = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
			// 
			// tsmiReadMe
			// 
			this.tsmiReadMe.Image = global::Yusen.GExplorer.Properties.Resources.help;
			this.tsmiReadMe.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsmiReadMe.Name = "tsmiReadMe";
			this.tsmiReadMe.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.tsmiReadMe.Size = new System.Drawing.Size(190, 22);
			this.tsmiReadMe.Text = "&ReadMe.txt";
			// 
			// tsmiChangeLog
			// 
			this.tsmiChangeLog.Name = "tsmiChangeLog";
			this.tsmiChangeLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F1)));
			this.tsmiChangeLog.Size = new System.Drawing.Size(190, 22);
			this.tsmiChangeLog.Text = "&ChangeLog.txt";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(187, 6);
			// 
			// tsmiAbout
			// 
			this.tsmiAbout.Name = "tsmiAbout";
			this.tsmiAbout.Size = new System.Drawing.Size(190, 22);
			this.tsmiAbout.Text = "バージョン情報(&A)...";
			// 
			// ToolStripHelpMenuItem
			// 
			this.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiReadMe,
            this.tsmiChangeLog,
            this.toolStripSeparator1,
            this.tsmiAbout});

		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem tsmiReadMe;
		private System.Windows.Forms.ToolStripMenuItem tsmiChangeLog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
	}
}
