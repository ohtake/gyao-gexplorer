using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace Yusen.GExplorer.Cinnamoroll {
	sealed partial class MovieAdForm : Form{
		public MovieAdForm() {
			InitializeComponent();
		}
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start((sender as LinkLabel).Text);
		}
	}
}
