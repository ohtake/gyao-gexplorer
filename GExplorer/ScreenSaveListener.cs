//#define SSL_DEBUG_PRINT

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NativeWindow=System.Windows.Forms.NativeWindow;
using Message=System.Windows.Forms.Message;
using Timer=System.Timers.Timer;

namespace Yusen.GExplorer {
	/// <summary>
	/// �t�H�A�O���E���h�E�B���h�E�����v���Z�X�̃E�B���h�E�Ȃ��
	/// ���̃E�B���h�E�ւ̃��b�Z�[�W�𕷂���
	/// �X�N���[���Z�[�o�������オ�肻���ɂȂ�����C�x���g�𔭐�������N���X�D
	/// </summary>
	sealed class ScreenSaveListener : NativeWindow, IDisposable {
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
		
		private const double DefaultInterval = 1 * 1000;
		
		public event CancelEventHandler ScreenSaverRaising;
		
		private int myPid;
		private IntPtr myHwnd = IntPtr.Zero;
		private Timer timer;
		private bool enabled = false;

		public ScreenSaveListener() {
			this.myPid = Process.GetCurrentProcess().Id;
			this.timer = new Timer(ScreenSaveListener.DefaultInterval);
			this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
#if SSL_DEBUG_PRINT
			Console.WriteLine("{0} ctor {1}", DateTime.Now.ToLongTimeString(), this.myPid);
#endif
		}
		
		void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
			IntPtr fgw = ScreenSaveListener.GetForegroundWindow();
			int pid;
			ScreenSaveListener.GetWindowThreadProcessId(fgw, out pid);
#if SSL_DEBUG_PRINT
			Console.WriteLine("{0} elapsed {1} {2}", DateTime.Now.ToLongTimeString(), fgw, pid);
#endif
			if (this.myPid == pid) {
				if (fgw != this.myHwnd) {
					base.ReleaseHandle();
					this.myHwnd = fgw;
					base.AssignHandle(this.myHwnd);
				}
			} else {
				base.ReleaseHandle();
				this.myHwnd = IntPtr.Zero;
			}
		}
		
		protected override void WndProc(ref Message m) {
			if (!this.enabled) {
				base.WndProc(ref m);
				return;
			}
			switch ((WM)m.Msg) {
				case WM.SYSCOMMAND:
#if SSL_DEBUG_PRINT
					Console.WriteLine("{0} syscmd {1} {2}", DateTime.Now.ToLongTimeString(), base.Handle, (SC)m.WParam);
#endif
					switch ((SC)m.WParam) {
						case SC.SCREENSAVE:
							if (null != this.ScreenSaverRaising) {
								CancelEventArgs cea = new CancelEventArgs();
								this.ScreenSaverRaising(this, cea);
								if (cea.Cancel) {
									return;
								}
							}
							break;
					}
					break;
			}
			base.WndProc(ref m);
		}
		
		public void Dispose() {
#if SSL_DEBUG_PRINT
			Console.WriteLine("{0} dispose", DateTime.Now.ToLongTimeString());
#endif
			this.timer.Stop();
			this.timer.Dispose();
			base.ReleaseHandle();
		}
		
		[DefaultValue(false)]
		public bool Enabled {
			get { return this.enabled; }
			set {
				this.enabled = value;
				if (value) {
					if (IntPtr.Zero != this.myHwnd) {
						this.AssignHandle(this.myHwnd);
					}
					this.timer.Start();
				} else {
					this.ReleaseHandle();
					this.timer.Stop();
				}
			}
		}
	}
}