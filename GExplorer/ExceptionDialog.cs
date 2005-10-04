using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	sealed class ExceptionDialog : CommonDialog {
		private Exception exception = null;
		private string title = null;
		private ExceptionForm ef = null;
		
		public ExceptionDialog() {
		}
		
		public Exception Exception {
			get { return this.exception; }
			set { this.exception = value; }
		}
		public string Title {
			get { return this.title; }
			set { this.title = value; }
		}

		public override void Reset() {
			this.exception = null;
			this.title = null;
		}
		protected override bool RunDialog(IntPtr hwndOwner) {
			if (null == this.ef) {
				this.ef = new ExceptionForm();
				this.ef.StartPosition = FormStartPosition.CenterParent;
			}
			this.ef.Exception = this.exception;
			if (! string.IsNullOrEmpty(this.title)) {
				this.ef.Text = this.title;
			}
			
			switch (this.ef.ShowDialog(new Win32WindowImpl(hwndOwner))) {
				case DialogResult.Abort:
					return false;
				case DialogResult.Ignore:
				default:
					return true;
			}
		}

		protected override void Dispose(bool disposing) {
			if (disposing && null != this.ef) {
				this.ef.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
