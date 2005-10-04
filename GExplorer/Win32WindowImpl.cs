using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	sealed class Win32WindowImpl : IWin32Window {
		private IntPtr handle;
		public Win32WindowImpl(IntPtr handle) {
			this.handle = handle;
		}
		public IntPtr Handle {
			get { return this.handle; }
		}
	}
}
