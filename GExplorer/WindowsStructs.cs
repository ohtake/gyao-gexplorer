using System;
using System.Runtime.InteropServices;

namespace Yusen.GExplorer {
	/// <summary>WM_WINDOWPOSCHANGING/CHANGED struct pointed to by lParam</summary>
	[StructLayout(LayoutKind.Sequential)]
	struct WINDOWPOS {
		// WinUser32.h
		public IntPtr hwnd;
		public IntPtr hwndInsertAfter;
		public int x;
		public int y;
		public int cx;
		public int cy;
		public SetWindowsPosFlags flags;
	}
}
