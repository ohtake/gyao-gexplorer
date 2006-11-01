using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer.UserInterfaces {
	public partial class DoubleBufferedListBox : ListBox {
		public DoubleBufferedListBox() {
			InitializeComponent();
			
			base.DoubleBuffered = true;
		}
	}
}
