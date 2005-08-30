using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class InputBoxDialog : FormBase {
		public InputBoxDialog() {
			InitializeComponent();
		}
		public InputBoxDialog(string text, string message, string input):this() {
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
	}
}