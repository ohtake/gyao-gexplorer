using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Yusen.GExplorer.Cinnamoroll {
	public partial class HappyControl : UserControl {
		public HappyControl() {
			this.InitializeComponent();
		}
		
		private void btnCafe_Click(object sender, EventArgs e) {
			Process.Start("http://sanriobb.com/freeisp/common_cn.html");
		}
	}
}
