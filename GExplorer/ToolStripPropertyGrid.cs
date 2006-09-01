using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Yusen.GExplorer {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	public sealed class ToolStripPropertyGrid : ToolStripControlHost{
		internal static void CancelDropDownClosingIfEditingPropertyGrid(object sender, ToolStripDropDownClosingEventArgs e) {
			switch (e.CloseReason) {
				case ToolStripDropDownCloseReason.AppFocusChange:
					Control control = Control.FromHandle(WindowsFunctions.GetFocus());
					while (null != control) {
						switch (control.GetType().ToString()) {
							case "System.Windows.Forms.PropertyGridInternal.PropertyGridView+DropDownHolder":
								e.Cancel = true;
								return;
						}
						control = control.Parent;
					}
					break;
			}
		}
		
		private PropertyGridContainer pgc;
		public ToolStripPropertyGrid() : this(new PropertyGridContainer()) {
		}
		private ToolStripPropertyGrid(PropertyGridContainer pgc) : base(pgc) {
			this.pgc = pgc;
		}
		
		public object SelectedObject {
			get { return this.pgc.SelectedObject; }
			set { this.pgc.SelectedObject = value; }
		}

		public void RefreshPropertyGrid() {
			this.pgc.SelectedObject = this.pgc.SelectedObject;
		}
	}
}
