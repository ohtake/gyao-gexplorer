using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class FormBase : Form {
		public FormBase() {
			InitializeComponent();
			if (null != GlobalVariables.ApplicationIcon) {
				this.Icon = GlobalVariables.ApplicationIcon;
			}
		}
	}
}
