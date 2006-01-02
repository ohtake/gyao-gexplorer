using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Media;
using System.Diagnostics;

namespace Yusen.GExplorer {
	static class Program {
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main() {
			ApplicationContext context = Program.InitializeProgram();
			Application.Run(context);
			Program.QuitProgram();
		}

		private static ApplicationContext InitializeProgram() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			//カレントディレクトリをスタートアップパスにあわせる
			Environment.CurrentDirectory = Application.StartupPath;

			SplashForm sf = new SplashForm();
			sf.Initialize("起動中です．．．", 8+1);

			//グローバル設定
			sf.StepProgress("グローバル設定の読み込み");
			GlobalSettings.TryDeserialize();
			//ユーザID
			sf.StepProgress("ユーザIDの読み取り");
			if(!GlobalSettings.Instance.TryGetUserNumber()) {
				MessageBox.Show(
					"ユーザIDが取得できませんでした．グローバル設定でIDの設定をしてください．",
					Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			//アイコンの読み込み
			sf.StepProgress("アイコンの読み込み");
			try {
				string iconFileName = GlobalSettings.Instance.IconFile;
				if(string.IsNullOrEmpty(iconFileName)) {
					iconFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ico";
				}
				FormBase.CustomIcon = new Icon(iconFileName);
			} catch {
			}

			sf.StepProgress("キャッシュの初期化");
			Cache.Initialize();
			sf.StepProgress("外部コマンドの読み取り");
			UserCommandsManager.Instance.DeserializeItems();
			sf.StepProgress("NGコンテンツの読み取り");
			NgContentsManager.Instance.DeserializeItems();
			sf.StepProgress("プレイリストの読み取り");
			PlayList.Instance.DeserializeItems();

			sf.StepProgress("メインフォームの作成");
			ApplicationContext context = new ApplicationContext(new MainForm());

			sf.EndProgress();

			return context;
		}
		private static void QuitProgram() {
			SplashForm sf = new SplashForm();
			sf.Initialize("終了中です．．．", 5+1);
			sf.StepProgress("プレイリストの保存");
			PlayList.Instance.SerializeItems();
			sf.StepProgress("NGコンテンツの保存");
			NgContentsManager.Instance.SerializeItems();
			sf.StepProgress("外部コマンドの保存");
			UserCommandsManager.Instance.SerializeItems();
			sf.StepProgress("キャッシュの保存");
			Cache.Serialize();
			sf.StepProgress("グローバル設定の保存");
			GlobalSettings.Serialize();
			sf.EndProgress();
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
