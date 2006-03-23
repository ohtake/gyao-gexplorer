using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	sealed partial class PropertyGridContainer : UserControl {
		public PropertyGridContainer() {
			InitializeComponent();
		}
		public object SelectedObject {
			get { return this.propertyGrid1.SelectedObject; }
			set { this.propertyGrid1.SelectedObject = value; }
		}
	}
}
