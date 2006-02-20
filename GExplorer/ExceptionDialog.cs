using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	sealed class ExceptionDialog : CommonDialog {
		private ExceptionForm ef = null;
		
		private Exception exception = null;
		private string title = null;
		private bool allowAbort = false;
		
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
		[DefaultValue(false)]
		public bool AllowAbort {
			get { return this.allowAbort; }
			set { this.allowAbort = value; }
		}

		public override void Reset() {
			this.exception = null;
			this.title = null;
			this.allowAbort = false;
		}
		protected override bool RunDialog(IntPtr hwndOwner) {
			if (null == this.ef) {
				this.ef = new ExceptionForm();
				this.ef.StartPosition = FormStartPosition.CenterParent;
			}
			this.ef.Exception = this.exception;
			this.ef.AllowAbort = this.allowAbort;
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
