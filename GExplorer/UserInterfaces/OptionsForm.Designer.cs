namespace Yusen.GExplorer.UserInterfaces {
	partial class OptionsForm {
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tvOptions = new System.Windows.Forms.TreeView();
			this.pgOptions = new System.Windows.Forms.PropertyGrid();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tvOptions);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.pgOptions);
			this.splitContainer1.Size = new System.Drawing.Size(354, 336);
			this.splitContainer1.SplitterDistance = 117;
			this.splitContainer1.TabIndex = 0;
			this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
			// 
			// tvOptions
			// 
			this.tvOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvOptions.FullRowSelect = true;
			this.tvOptions.HideSelection = false;
			this.tvOptions.HotTracking = true;
			this.tvOptions.Location = new System.Drawing.Point(0, 0);
			this.tvOptions.Name = "tvOptions";
			this.tvOptions.ShowNodeToolTips = true;
			this.tvOptions.Size = new System.Drawing.Size(117, 336);
			this.tvOptions.TabIndex = 0;
			this.tvOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvOptions_AfterSelect);
			// 
			// pgOptions
			// 
			this.pgOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgOptions.Location = new System.Drawing.Point(0, 0);
			this.pgOptions.Name = "pgOptions";
			this.pgOptions.Size = new System.Drawing.Size(233, 336);
			this.pgOptions.TabIndex = 0;
			this.pgOptions.ToolbarVisible = false;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(354, 336);
			this.Controls.Add(this.splitContainer1);
			this.Name = "OptionsForm";
			this.Text = "オプション";
			this.Load += new System.EventHandler(this.OptionsForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView tvOptions;
		private System.Windows.Forms.PropertyGrid pgOptions;
	}
}