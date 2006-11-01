using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class InputBoxForm : BaseForm {
		public InputBoxForm() {
			InitializeComponent();
		}
		public InputBoxForm(string text, string message, string input):this() {
			this.Text = text;
			this.Message = message;
			this.Input = input;
		}

		public string Message {
			get { return this.lblMessage.Text; }
			set { this.lblMessage.Text = value; }
		}
		public string Input {
			get { return this.txtInput.Text; }
			set { this.txtInput.Text = value; }
		}

		private void InputBoxForm_Shown(object sender, EventArgs e) {
			this.txtInput.Focus();
			this.txtInput.SelectAll();
		}
	}
}
