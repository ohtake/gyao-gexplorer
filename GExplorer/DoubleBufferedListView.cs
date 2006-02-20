using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class DoubleBufferedListView : ListView {
		public DoubleBufferedListView() {
			InitializeComponent();
			base.DoubleBuffered = GlobalSettings.Instance.ListViewDoubleBufferEnabled;
		}
	}
}
