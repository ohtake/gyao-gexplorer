using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.UserInterfaces {
	public partial class RegistrationForm : BaseForm {
		public RegistrationForm() {
			InitializeComponent();
		}
		
		private void RegistrationForm_Load(object sender, EventArgs e) {
			this.Text = Program.ApplicationName;
		}
		
		private void btnOpenTop_Click(object sender, EventArgs e) {
			this.webBrowser1.Navigate(GUriBuilder.TopPageUri);
		}
		private void btnClose_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void btnQuit_Click(object sender, EventArgs e) {
			Environment.Exit(0);
		}
	}
}
