using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	sealed class InputBoxDialog : CommonDialog{
		private string input = null;
		private string title = null;
		private string message = null;

		private InputBoxForm ibf = null;

		public InputBoxDialog() {
		}

		public string Input {
			get { return this.input; }
			set { this.input = value; }
		}
		public string Title {
			get { return this.title; }
			set { this.title = value; }
		}
		public string Message {
			get { return this.message; }
			set { this.message = value; }
		}

		public override void Reset() {
			this.Input = null;
			this.Title = null;
			this.Message = null;
		}

		protected override bool RunDialog(IntPtr hwndOwner) {
			if (null == this.ibf) {
				this.ibf = new InputBoxForm();
				this.ibf.StartPosition = FormStartPosition.CenterParent;
			}

			this.ibf.Input = (null == this.Input) ? string.Empty : this.Input;
			this.ibf.Text = (null == this.Title) ? string.Empty : this.Title;
			this.ibf.Message = (null == this.Message) ? string.Empty : this.Message;

			switch (this.ibf.ShowDialog(new Win32WindowImpl(hwndOwner))) {
				case DialogResult.OK:
					this.Input = this.ibf.Input;
					this.Title = this.ibf.Text;
					this.Message = this.ibf.Message;
					return true;
				default:
					return false;
			}
		}

		protected override void Dispose(bool disposing) {
			if (disposing && null != this.ibf) {
				this.ibf.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
