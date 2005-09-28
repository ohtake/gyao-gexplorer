using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Yusen.GExplorer {
	class InputBoxDialog : CommonDialog{
		private class Win32WindowImpl : IWin32Window {
			private IntPtr handle;
			public Win32WindowImpl(IntPtr handle) {
				this.handle = handle;
			}
			public IntPtr Handle {
				get { return this.handle; }
			}
		}
		
		private string input = null;
		private string title = null;
		private string message = null;

		public InputBoxDialog() {
		}
		public override void Reset() {
			this.input = null;
			this.title = null;
			this.message = null;
		}

		protected override bool RunDialog(IntPtr hwndOwner) {
			InputBoxForm ibf = new InputBoxForm();
			if (null != this.input) ibf.Input = this.input;
			if (null != this.title) ibf.Text = this.title;
			if (null != this.message) ibf.Message = this.message;

			switch (ibf.ShowDialog(new Win32WindowImpl(hwndOwner))) {
				case DialogResult.OK:
					this.input = ibf.Input;
					this.title = ibf.Text;
					this.message = ibf.Message;
					return true;
				default:
					return false;
			}
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
	}
}
