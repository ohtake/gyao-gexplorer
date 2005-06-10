namespace Yusen.GExplorer {
	partial class CategorySelector {
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
			this.comboCategory = new System.Windows.Forms.ComboBox();
			this.lblCategory = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// comboCategory
			// 
			this.comboCategory.FormattingEnabled = true;
			this.comboCategory.Location = new System.Drawing.Point(62, 3);
			this.comboCategory.MaxDropDownItems = 10;
			this.comboCategory.Name = "comboCategory";
			this.comboCategory.Size = new System.Drawing.Size(101, 20);
			this.comboCategory.TabIndex = 2;
			this.comboCategory.Text = "(カテゴリの選択)";
			// 
			// lblCategory
			// 
			this.lblCategory.AutoSize = true;
			this.lblCategory.Location = new System.Drawing.Point(3, 6);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(53, 12);
			this.lblCategory.TabIndex = 1;
			this.lblCategory.Text = "カテゴリ(&C)";
			// 
			// CategorySelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.comboCategory);
			this.Name = "CategorySelector";
			this.Size = new System.Drawing.Size(170, 27);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.ComboBox comboCategory;

	}
}
