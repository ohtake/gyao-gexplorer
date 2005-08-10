using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace Yusen.GExplorer {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			
			Application.EnableVisualStyles();
			GlobalSettings.TryDeserialize();
			if (GlobalSettings.Instance.IsCookieRequired) {
				if (DialogResult.Yes !=
					MessageBox.Show(
						"IE で www.gyao.jp にアクセスすることでユーザ情報を取得します．\n"
						+ "よろしいですか？",
						Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
					return;
				}
				if (DialogResult.OK != new CookieFetchForm().ShowDialog()) {
					return;
				}
			}
			
			GlobalSettings.Serialize();
			
			UserCommandsManager.Instance.DeserializeItems();
			NgContentsManager.Instance.DeserializeItems();
			Application.Run(new MainForm());
			NgContentsManager.Instance.SerializeItems();
			UserCommandsManager.Instance.SerializeItems();
			GlobalSettings.Serialize();
		}
		
		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
			Program.DisplayException(e.Exception);
		}
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			Exception ex = e.ExceptionObject as Exception;
			if (null != ex) {
				Program.DisplayException(ex);
			}
		}
		private static void DisplayException(Exception e) {
			MessageBox.Show(e.Message + "\n\n" + e.StackTrace, e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
