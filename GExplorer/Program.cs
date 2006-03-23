using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Media;
using System.Diagnostics;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	static class Program {
		internal static readonly string ApplicationName = Application.ProductName + " " + Application.ProductVersion;
		
		private const int InitializationSteps = 9;
		private const int SerializationSteps = 5;
		private static SplashForm splashInit;
		private static MainForm mainForm;
		internal static event EventHandler<ProgramSerializationProgressEventArgs> ProgramSerializationProgress;
		
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		private static void Main() {
			//おまじない
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//キャッチされない例外をキャッチ
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			//多重起動チェック
			while(Program.CheckMultipleExecution()) {
				switch(MessageBox.Show("多重起動と思われます．どうしますか？", Program.ApplicationName, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning)) {
					case DialogResult.Abort:
						return;
					case DialogResult.Retry:
						continue;
					case DialogResult.Ignore:
						break;
				}
				break;
			}
			//カレントディレクトリをスタートアップパスにあわせる
			Environment.CurrentDirectory = Application.StartupPath;
			
			Program.splashInit = new SplashForm();
			Program.splashInit.Initialize("起動中です．．．", Program.InitializationSteps +1);
			Program.InitializeProgram();
			Program.mainForm.Load += delegate {
				Program.splashInit.EndProgress();
				Program.splashInit.Dispose();
				Program.splashInit = null;
			};
			
			Application.Run(Program.mainForm);
		}

		private static bool CheckMultipleExecution() {
			Process curProc = Process.GetCurrentProcess();
			foreach(Process p in Process.GetProcesses()) {
				if(curProc.Id.Equals(p.Id)) {
					continue;
				}
				try {
					if(curProc.MainModule.FileName.Equals(p.MainModule.FileName)) {
						return true;
					}
				} catch {
				}
			}
			return false;
		}

		private static void InitializeProgram() {
			//グローバル設定
			Program.splashInit.StepProgress("グローバル設定の読み込み");
			GlobalSettings.TryDeserialize();

			//アイコンの読み込み
			Program.splashInit.StepProgress("アイコンの読み込み");
			try {
				string iconFileName = GlobalSettings.Instance.IconFile;
				if (string.IsNullOrEmpty(iconFileName)) {
					iconFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ico";
				}
				FormBase.CustomIcon = new Icon(iconFileName);
			} catch {
			}

			//ユーザID
			Program.splashInit.StepProgress("ユーザIDの読み取り");
			if(!GlobalSettings.Instance.TryGetUserNumber()) {
				MessageBox.Show(
					"ユーザIDが取得できませんでした．グローバル設定でIDの設定をしてください．",
					Program.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			//ビットレートの設定
			Program.splashInit.StepProgress("ビットレートの設定");
			if (GlobalSettings.Instance.PromptBitrateOnStartup) {
				using (BitRateForm brf = new BitRateForm()) {
					brf.BitRate = GlobalSettings.Instance.BitRate;
					switch (brf.ShowDialog()) {
						case DialogResult.OK:
							GlobalSettings.Instance.BitRate = brf.BitRate;
							GlobalSettings.Instance.PromptBitrateOnStartup = !brf.SkipNextTimeEnabled;
							break;
					}
				}
			}
			
			Program.splashInit.StepProgress("キャッシュの読み込み");
			Cache.Initialize();
			Program.splashInit.StepProgress("外部コマンドの読み込み");
			UserCommandsManager.Instance.DeserializeItems();
			Program.splashInit.StepProgress("NGコンテンツの読み込み");
			NgContentsManager.Instance.DeserializeItems();
			Program.splashInit.StepProgress("プレイリストの読み込み");
			PlayList.Instance.DeserializeItems();

			Program.splashInit.StepProgress("メインフォームの作成");
			Program.mainForm = new MainForm();
		}
		private static void OnProgramSerializationProgress(int current, string message) {
			if(null != Program.ProgramSerializationProgress) {
				Program.ProgramSerializationProgress(null, new ProgramSerializationProgressEventArgs(current, Program.SerializationSteps, message));
			}
		}
		internal static void SerializeSettings() {
			int step = 0;
			Program.OnProgramSerializationProgress(step++, "プレイリストの保存");
			PlayList.Instance.SerializeItems();
			Program.OnProgramSerializationProgress(step++, "NGコンテンツの保存");
			NgContentsManager.Instance.SerializeItems();
			Program.OnProgramSerializationProgress(step++, "外部コマンドの保存");
			UserCommandsManager.Instance.SerializeItems();
			Program.OnProgramSerializationProgress(step++, "キャッシュの保存");
			Cache.Serialize();
			Program.OnProgramSerializationProgress(step++, "グローバル設定の保存");
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
		
		internal static void DisplayException(string title, Exception e) {
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

	sealed class ProgramSerializationProgressEventArgs : EventArgs{
		private int current;
		private int max;
		private string message;
		public ProgramSerializationProgressEventArgs(int current, int max, string message) {
			this.current = current;
			this.max = max;
			this.message = message;
		}
		public int Current {
			get { return this.current; }
		}
		public int Max {
			get { return this.max; }
		}
		public string Message {
			get { return this.message; }
		}
	}
}
