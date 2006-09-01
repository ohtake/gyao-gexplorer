﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Yusen.GExplorer {
	static class WindowsFunctions {
		#region WinUser.h
#if false
		[DllImport("user32.dll", CallingConvention=CallingConvention.Winapi)]
		public static extern bool SetWindowPos(
			//WinUser.h
			[In] IntPtr hWnd,
			[In] IntPtr hWndInsertAfter,
			[In] int X,
			[In] int Y,
			[In] int cx,
			[In] int cy,
			[In] SetWindowsPosFlags uFlags);
#endif

		[DllImport("user32.dll", CallingConvention = CallingConvention.Winapi)]
		public static extern IntPtr GetFocus();

		[DllImport("user32.dll", CallingConvention = CallingConvention.Winapi)]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", CallingConvention = CallingConvention.Winapi)]
		public static extern uint GetWindowThreadProcessId(
			[In] IntPtr hWnd,
			[Out] out int lpdwProcessId);
		#endregion
		
		#region WinInet.h
		[DllImport("wininet.dll", CharSet = CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
		public static extern bool InternetGetCookie(
			[In] string lpszUrl,
			[In] string lpszCookieName,
			[Out] StringBuilder lpszCookieData,
			[In,Out] ref int lpdwSize);
#if false
		[DllImport("wininet.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
		public static extern bool InternetSetCookie(
			[In] string lpszUrl,
			[In] string lpszCookieName,
			[In] string lpszCookieData);
#endif
		#endregion
	}
}
