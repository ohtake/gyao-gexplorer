namespace Yusen.GExplorer {
	partial class GenreTabControl {
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
			this.SuspendLayout();
			// 
			// GenreTabControl
			// 
			this.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.HotTrack = true;
			this.ItemSize = new System.Drawing.Size(60, 20);
			this.Multiline = true;
			this.ShowToolTips = true;
			this.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.DoubleClick += new System.EventHandler(this.GenreTabControl_DoubleClick);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.GenreTabControl_Layout);
			this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GenreTabControl_DrawItem);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GenreTabControl_MouseClick);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GenreTabControl_KeyUp);
			this.SelectedIndexChanged += new System.EventHandler(this.GenreTabControl_SelectedIndexChanged);
			this.SizeChanged += new System.EventHandler(this.GenreTabControl_SizeChanged);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GenreTabControl_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion

	}
}
