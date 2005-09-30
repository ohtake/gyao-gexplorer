using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Drawing;

namespace Yusen.GExplorer {
	static class Program {
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			
			GlobalSettings.TryDeserialize();
			//レジストリからユーザIDの取得
			if (GlobalSettings.Instance.IsCookieRequired) {
				Program.ReadUserIdFromRegistry();
			}
			//ファイルシステム上のクッキーからユーザIDの取得
			if (GlobalSettings.Instance.IsCookieRequired) {
				Program.ReadUserIdFromCookieOnFileSystem();
			}
			//IE経由でgyaoにアクセスしてクッキーを取得
			if (GlobalSettings.Instance.IsCookieRequired) {
				using (CookieFetchForm cff =  new CookieFetchForm()) {
					cff.ShowDialog();
				}
			}
			
			GlobalSettings.Serialize();
			if (GlobalSettings.Instance.IsCookieRequired) {
				MessageBox.Show(
					"ユーザIDの取得に失敗したため " + Application.ProductName + " を終了します．",
					Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			
			//アイコンの読み込み
			try {
				string iconFileName = GlobalSettings.Instance.IconFile;
				if (string.IsNullOrEmpty(iconFileName)) {
					iconFileName = Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]) + ".ico";
				}
				GlobalVariables.ApplicationIcon = new Icon(iconFileName);
			} catch {
			}
			
			UserCommandsManager.Instance.DeserializeItems();
			NgContentsManager.Instance.DeserializeItems();
			PlayList.Instance.DeserializeItems();
			
			Application.Run(new MainForm());
			
			PlayList.Instance.SerializeItems();
			NgContentsManager.Instance.SerializeItems();
			UserCommandsManager.Instance.SerializeItems();
			
			GlobalSettings.Serialize();
		}

		private static void ReadUserIdFromRegistry() {
			try {
				using (RegistryKey cu = Registry.CurrentUser)
				using (RegistryKey software = cu.OpenSubKey("SOFTWARE"))
				using (RegistryKey usen = software.OpenSubKey("USEN"))
				using (RegistryKey gyaoTool = usen.OpenSubKey("GyaOTool")) {
					GlobalSettings.Instance.UserNo = int.Parse(gyaoTool.GetValue("Cookie_UserId") as string);
				}
			} catch {
			}
		}
		private static void ReadUserIdFromCookieOnFileSystem() {
			try {
				DirectoryInfo cookieDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
				if (cookieDir.Exists) {
					FileInfo[] fis = cookieDir.GetFiles("*@gyao*.txt");
					foreach (FileInfo fi in fis) {
						using (TextReader reader = new StreamReader(fi.FullName)) {
							string line;
							while (null != (line=reader.ReadLine())) {
								if ("Cookie_UserId" == line) {
									GlobalSettings.Instance.UserNo = int.Parse(reader.ReadLine());
									break;
								}
							}
							if (!GlobalSettings.Instance.IsCookieRequired) {
								break;
							}
						}
					}
				}
			} catch {
			}
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
