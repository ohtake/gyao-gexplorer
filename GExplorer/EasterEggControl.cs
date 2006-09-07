using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	public partial class EasterEggControl : UserControl {
		public EasterEggControl() {
			InitializeComponent();
		}

		private void btnCopyAA_Click(object sender, EventArgs e) {
			Clipboard.SetText(this.lblCinnamon.Text);
		}
	}
}
