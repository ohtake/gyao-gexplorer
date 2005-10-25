using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Media;

namespace Yusen.GExplorer {
	static class Program {
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			
			//カレントディレクトリをスタートアップパスにあわせる
			Environment.CurrentDirectory = Application.StartupPath;
			
			//グローバル設定
			GlobalSettings.TryDeserialize();
			if (!GlobalSettings.Instance.TryGetUserNumber()) {
				MessageBox.Show(
					"ユーザIDの取得に失敗したため " + Application.ProductName + " を終了します．",
					Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			
			//アイコンの読み込み
			try {
				string iconFileName = GlobalSettings.Instance.IconFile;
				if (string.IsNullOrEmpty(iconFileName)) {
					iconFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ico";
				}
				FormBase.CustomIcon = new Icon(iconFileName);
			} catch {
			}
			
			Cache.Initialize();
			UserCommandsManager.Instance.DeserializeItems();
			NgContentsManager.Instance.DeserializeItems();
			PlayList.Instance.DeserializeItems();
			
			Application.Run(new MainForm());
			
			PlayList.Instance.SerializeItems();
			NgContentsManager.Instance.SerializeItems();
			UserCommandsManager.Instance.SerializeItems();
			Cache.Serialize();
			
			GlobalSettings.Serialize();
		}
		
		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
			Program.DisplayException("Application.ThreadException", e.Exception);
		}
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			Exception ex = e.ExceptionObject as Exception;
			if (null != ex) {
				Program.DisplayException("CurrentDomain.UnhandledException", ex);
			}
		}
		
		public static void DisplayException(string title, Exception e) {
			SystemSounds.Exclamation.Play();
			using (ExceptionDialog ed = new ExceptionDialog()) {
				ed.AllowAbort = true;
				ed.Exception = e;
				ed.Title = title;
				switch (ed.ShowDialog()) {
					case DialogResult.OK:
						break;
					case DialogResult.Cancel:
						Environment.Exit(1);
						break;
				}
			}
		}
	}
}
