using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.UserInterfaces;
using Yusen.GExplorer.GyaoModel;
using System.ComponentModel;

namespace Yusen.GExplorer {
	static class Program {
		internal static readonly string ApplicationName = Application.ProductName + " " + Application.ProductVersion;
		private static CacheManager cacheManager;
		private static PlaylistsManager playlistsManager;
		private static RootOptions rootOptions;
		private static ExternalCommandsManager externalCommandsManager;
		private static PlayerForm playerForm = null;
		private static OptionsForm optionsForm = null;
		private static CacheViewerForm cacheViewerForm = null;
		private static BrowserForm browserForm = null;
		private static ExternalCommandsEditor externalCommandsEditor = null;

		internal static CacheManager CacheManager {
			get { return Program.cacheManager; }
		}
		internal static PlaylistsManager PlaylistsManager {
			get { return Program.playlistsManager; }
		}
		internal static ExternalCommandsManager ExternalCommandsManager {
			get { return Program.externalCommandsManager; }
		}
		internal static RootOptions RootOptions {
			get { return Program.rootOptions; }
		}
		internal static void AddVirtualGenre(IVirtualGenre vgenre) {
			Program.mainForm2.AddVirtualGenre(vgenre);
			Program.mainForm2.Focus();
		}
		internal static void BrowsePage(Uri uri) {
			if (null == Program.browserForm || Program.browserForm.IsDisposed) {
				Program.browserForm = new BrowserForm();
			}
			Program.browserForm.Show();
			Program.browserForm.Focus();
			Program.browserForm.DocumentUri = uri;
		}
		internal static void PlayContent(GContentClass content, Playlist playlist) {
			if (null == Program.playerForm || Program.playerForm.IsDisposed) {
				Program.playerForm = new PlayerForm();
			}
			Program.playerForm.Show();
			Program.playerForm.Focus();
			Program.playerForm.PlayContent(content, playlist);
		}
		internal static void ShowOptionsForm() {
			if (null == Program.optionsForm || Program.optionsForm.IsDisposed) {
				Program.optionsForm = new OptionsForm();
			}
			Program.optionsForm.Show();
			Program.optionsForm.Focus();
		}
		internal static void ShowCacheViewerForm() {
			if (null == Program.cacheViewerForm || Program.cacheViewerForm.IsDisposed) {
				Program.cacheViewerForm = new CacheViewerForm();
			}
			Program.cacheViewerForm.Show();
			Program.cacheViewerForm.Focus();
		}
		internal static void ShowExternalCommandsEditor() {
			if (null == Program.externalCommandsEditor || Program.externalCommandsEditor.IsDisposed) {
				Program.externalCommandsEditor = new ExternalCommandsEditor();
			}
			Program.externalCommandsEditor.Show();
			Program.externalCommandsEditor.Focus();
		}
		internal static string GetWorkingDirectory(WorkingDirectory wd) {
			string path = Path.Combine(Application.StartupPath, wd.ToString());
			DirectoryInfo di = new DirectoryInfo(path);
			if (!di.Exists) di.Create();
			return path;
		}

		private const int InitializationSteps = 7;
		private const int SerializationSteps = 4;
		private static SplashForm splashForm;
		private static MainForm mainForm2;
		internal static event ProgressChangedEventHandler ProgramSerializationProgress2;

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
			while (Program.CheckMultipleExecution()) {
				switch (MessageBox.Show("多重起動と思われます．どうしますか？", Program.ApplicationName, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning)) {
					case DialogResult.Abort:
						return;
					case DialogResult.Retry:
						continue;
					case DialogResult.Ignore:
						break;
				}
				break;
			}

			Program.splashForm = new SplashForm();
			Program.splashForm.Initialize("起動中です．．．", Program.InitializationSteps + 1);
			Program.InitializeProgram2();
			Program.mainForm2.Load += delegate {
				Program.splashForm.EndProgress();

				Program.splashForm.Dispose();
				Program.splashForm = null;
			};
			
			Application.Run(Program.mainForm2);
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
		private static void InitializeProgram2() {
			Program.splashForm.StepProgress("ルートオプションの読み込み");
			if (!RootOptions.TryDeserialize(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "RootOptions.xml"), out Program.rootOptions)) {
				Program.rootOptions = new RootOptions();
			}
			
			//アイコンの読み込み
			Program.splashForm.StepProgress("アイコンの読み込み");
			try {
				//string iconFileName = GlobalSettings.Instance.IconFile;
				string iconFileName = string.Empty;
				if (string.IsNullOrEmpty(iconFileName)) {
					iconFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ico";
				}
				BaseForm.CustomIcon = new Icon(iconFileName);
			} catch {
			}

			//ビットレートの設定
			Program.splashForm.StepProgress("ビットレートの設定");
			if (Program.RootOptions.AppBasicOptions.PromptBitrateOnStartup) {
				using (BitrateForm brf = new BitrateForm()) {
					brf.Bitrate = Program.RootOptions.AppBasicOptions.Bitrate;
					switch (brf.ShowDialog()) {
						case DialogResult.OK:
							Program.RootOptions.AppBasicOptions.Bitrate = brf.Bitrate;
							Program.RootOptions.AppBasicOptions.PromptBitrateOnStartup = !brf.SkipNextTimeEnabled;
							break;
					}
				}
			}

			Program.splashForm.StepProgress("キャッシュの初期化と読み込み");
			Program.cacheManager = new CacheManager(Program.GetWorkingDirectory(WorkingDirectory.Cache));
			if (Program.RootOptions.AppBasicOptions.UseDefaultGenres) {
				Program.CacheManager.ResetToDefaultGenres();
			} else {
				Program.CacheManager.DeserializeGenreTable();
			}
			Program.cacheManager.DeserializePackageAndContentTables();

			Program.splashForm.StepProgress("プレイリストコレクションの読み込み");
			Program.playlistsManager = new PlaylistsManager();
			Program.playlistsManager.DeserializePlaylists(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "PlaylistCollection.xml"));

			Program.splashForm.StepProgress("外部コマンドの読み込み");
			Program.externalCommandsManager = new ExternalCommandsManager();
			Program.externalCommandsManager.TryDeserialize(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "ExternalCommands.xml"));

			Program.splashForm.StepProgress("メインフォームの作成");
			Program.mainForm2 = new MainForm();
		}
		private static void OnProgramSerializationProgress2(int current, string message) {
			ProgressChangedEventHandler handler = Program.ProgramSerializationProgress2;
			if (null != handler) {
				handler(null, new ProgressChangedEventArgs(100*current/Program.SerializationSteps, message));
			}
		}
		internal static void SerializeSettings2() {
			int step = 0;
			Program.OnProgramSerializationProgress2(step++, "プレイリストコレクションの保存");
			Program.playlistsManager.SerializePlaylists(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "PlaylistCollection.xml"));
			Program.OnProgramSerializationProgress2(step++, "プレイリストコレクションの保存");
			Program.externalCommandsManager.Serialize(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "ExternalCommands.xml"));
			Program.OnProgramSerializationProgress2(step++, "キャッシュの保存");
			Program.cacheManager.SerializeTables();
			Program.OnProgramSerializationProgress2(step++, "ルートオプションの保存");
			Program.rootOptions.Serialize(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "RootOptions.xml"));
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
	
	enum WorkingDirectory {
		Cache,
		UserSettings,
		Temp,
	}
}
