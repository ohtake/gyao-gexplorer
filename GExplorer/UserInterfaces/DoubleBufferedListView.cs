using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yusen.GExplorer.UserInterfaces {
	public partial class DoubleBufferedListView : ListView {
		public DoubleBufferedListView() {
			InitializeComponent();
			base.DoubleBuffered = true;
		}
	}
}
