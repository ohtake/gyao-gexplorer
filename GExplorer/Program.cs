using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.UserInterfaces;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer {
	static class Program {
		internal static readonly string ApplicationName = Application.ProductName + " " + Application.ProductVersion;

		private static RootOptions rootOptions;
		private static CacheController cacheController;
		private static PlaylistsManager playlistsManager;
		private static ContentClassificationRulesManager contentClassificatinoRulesManager;
		private static ExternalCommandsManager externalCommandsManager;
		private static CookieContainer cookieContainer;

		private static PlayerForm playerForm = null;
		private static BrowserForm browserForm = null;
		private static CacheViewerForm cacheViewerForm = null;
		private static ContentClassificationRuleEditForm contentClassificationRuleEditForm = null;
		private static ExternalCommandsEditForm externalCommandsEditForm = null;
		private static OptionsForm optionsForm = null;

		internal static RootOptions RootOptions {
			get { return Program.rootOptions; }
		}
		internal static CacheController CacheController {
			get { return Program.cacheController; }
		}
		internal static PlaylistsManager PlaylistsManager {
			get { return Program.playlistsManager; }
		}
		internal static ContentClassificationRulesManager ContentClassificationRulesManager {
			get { return Program.contentClassificatinoRulesManager; }
		}
		internal static ExternalCommandsManager ExternalCommandsManager {
			get { return Program.externalCommandsManager; }
		}
		internal static CookieContainer CookieContainer {
			get { return Program.cookieContainer; }
		}
		internal static void AddVirtualGenre(IVirtualGenre vgenre) {
			Program.mainForm.AddVirtualGenre(vgenre);
			Program.mainForm.Focus();
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
		internal static void ShowCacheViewerForm() {
			if (null == Program.cacheViewerForm || Program.cacheViewerForm.IsDisposed) {
				Program.cacheViewerForm = new CacheViewerForm();
			}
			Program.cacheViewerForm.Show();
			Program.cacheViewerForm.Focus();
		}
		internal static void ShowContentClassificationRuleEditForm() {
			if (null == Program.contentClassificationRuleEditForm || Program.contentClassificationRuleEditForm.IsDisposed) {
				Program.contentClassificationRuleEditForm = new ContentClassificationRuleEditForm();
			}
			Program.contentClassificationRuleEditForm.Show();
			Program.contentClassificationRuleEditForm.Focus();
		}
		internal static void ShowExternalCommandsEditForm() {
			if (null == Program.externalCommandsEditForm || Program.externalCommandsEditForm.IsDisposed) {
				Program.externalCommandsEditForm = new ExternalCommandsEditForm();
			}
			Program.externalCommandsEditForm.Show();
			Program.externalCommandsEditForm.Focus();
		}
		internal static void ShowOptionsForm() {
			if (null == Program.optionsForm || Program.optionsForm.IsDisposed) {
				Program.optionsForm = new OptionsForm();
			}
			Program.optionsForm.Show();
			Program.optionsForm.Focus();
		}
		internal static string GetWorkingDirectory(WorkingDirectory wd) {
			string path = Path.Combine(Application.StartupPath, wd.ToString());
			DirectoryInfo di = new DirectoryInfo(path);
			if (!di.Exists) di.Create();
			return path;
		}

		private const int InitializationSteps = 9;
		private const int SerializationSteps = 5;
		private static SplashForm splashForm;
		private static MainForm mainForm;
		internal static event ProgressChangedEventHandler ProgramSerializationProgress;

		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		private static void Main() {
			//おまじない
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//キャッチされない例外をキャッチ
#if !DEBUG
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
#endif
			
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
			Program.InitializeProgram();
			Program.mainForm.Load += delegate {
				Program.splashForm.EndProgress();
				Program.splashForm.Dispose();
				Program.splashForm = null;
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
			Program.splashForm.StepProgress("ルートオプションの読み込み");
			if (!RootOptions.TryDeserialize(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "RootOptions.xml"), out Program.rootOptions)) {
				Program.rootOptions = new RootOptions();
			}
			
			Program.splashForm.StepProgress("アイコンの読み込み");
			try {
				string iconFileName = Program.RootOptions.AppBasicOptions.IconFile;
				if (string.IsNullOrEmpty(iconFileName)) {
					iconFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ico";
				}
				if (File.Exists(iconFileName)) {
					BaseForm.CustomIcon = new Icon(iconFileName);
				}
			} catch (Exception e) {
				Program.DisplayException("アイコンの読み込みエラー", e);
			}
			
			Program.splashForm.StepProgress("クッキーの設定");
			Program.cookieContainer = new CookieContainer();
			try {
			retry:
				int cookieSize = 0;
				if (!WindowsFunctions.InternetGetCookie(GUriBuilder.TopPageUri.AbsoluteUri, null, null, ref cookieSize)) {
					if (Marshal.GetLastWin32Error() != (int)WinError.ERROR_NO_MORE_ITEMS) {
						Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
					} else {
						switch (MessageBox.Show("クッキーの読み込みに失敗しました．GyaOのトップページから視聴登録を行ってください．\n\n・はい: 視聴登録用のウィンドウを開く\n・いいえ: この警告を無視してアプリケーションを起動する\n・キャンセル: アプリケーションを終了する", "クッキーの読み込み失敗", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)) {
							case DialogResult.Yes:
								RegistrationForm regForm = new RegistrationForm();
								regForm.ShowDialog();
								regForm.Dispose();
								goto retry;
							case DialogResult.No:
								break;
							case DialogResult.Cancel:
								Environment.Exit(0);
								return;
						}
					}
				} else {
					StringBuilder cookieSb = new StringBuilder(cookieSize);
					if (!WindowsFunctions.InternetGetCookie(GUriBuilder.TopPageUri.AbsoluteUri, null, cookieSb, ref cookieSize)) {
						Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
					}
					string cookieStr = cookieSb.ToString();
					if (!cookieStr.Contains("Cookie_UserId")) {
						switch (MessageBox.Show("クッキーに Cookie_UserId がありません．GyaOのトップページから視聴登録を行ってください．\n\n・はい: 視聴登録用のウィンドウを開く\n・いいえ: この警告を無視してアプリケーションを起動する\n・キャンセル: アプリケーションを終了する\n\nクッキーの中身:\n" + cookieStr, "Cookie_UserId がない", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)) {
							case DialogResult.Yes:
								RegistrationForm regForm = new RegistrationForm();
								regForm.ShowDialog();
								regForm.Dispose();
								goto retry;
							case DialogResult.No:
								break;
							case DialogResult.Cancel:
								Environment.Exit(0);
								return;
						}
					}
					Program.cookieContainer.SetCookies(GUriBuilder.TopPageUri, cookieSb.ToString().Replace(';', ','));
				}
			} catch (Exception e) {
				Program.DisplayException("クッキーの読み込み失敗", e);
			}
			
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
			Program.cacheController = new CacheController(Program.GetWorkingDirectory(WorkingDirectory.Cache), Program.CookieContainer);
			if (Program.RootOptions.AppBasicOptions.UseDefaultGenres) {
				Program.CacheController.ResetToDefaultGenres();
			} else {
				Program.CacheController.DeserializeGenreTable();
			}
			Program.cacheController.DeserializePackageAndContentTables();

			Program.splashForm.StepProgress("プレイリストコレクションの読み込み");
			Program.playlistsManager = new PlaylistsManager();
			Program.playlistsManager.DeserializePlaylists(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "PlaylistCollection.xml"));

			Program.splashForm.StepProgress("仕分けルールの読み込み");
			Program.contentClassificatinoRulesManager = new ContentClassificationRulesManager();
			Program.contentClassificatinoRulesManager.TryDeserialize(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "ContentClassificationRules.xml"));

			Program.splashForm.StepProgress("外部コマンドの読み込み");
			Program.externalCommandsManager = new ExternalCommandsManager();
			Program.externalCommandsManager.TryDeserialize(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "ExternalCommands.xml"));
			
			Program.splashForm.StepProgress("メインフォームの作成");
			Program.mainForm = new MainForm();
		}
		private static void OnProgramSerializationProgress(int current, string message) {
			ProgressChangedEventHandler handler = Program.ProgramSerializationProgress;
			if (null != handler) {
				handler(null, new ProgressChangedEventArgs(100*current/Program.SerializationSteps, message));
			}
		}
		internal static void SerializeSettings() {
			int step = 0;
			Program.OnProgramSerializationProgress(step++, "プレイリストコレクションの保存");
			Program.playlistsManager.SerializePlaylists(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "PlaylistCollection.xml"));
			Program.OnProgramSerializationProgress(step++, "仕分けルールの保存");
			Program.contentClassificatinoRulesManager.Serialize(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "ContentClassificationRules.xml"));
			Program.OnProgramSerializationProgress(step++, "外部コマンドの保存");
			Program.externalCommandsManager.Serialize(Path.Combine(Program.GetWorkingDirectory(WorkingDirectory.UserSettings), "ExternalCommands.xml"));
			Program.OnProgramSerializationProgress(step++, "キャッシュの保存");
			Program.cacheController.SerializeTables();
			Program.OnProgramSerializationProgress(step++, "ルートオプションの保存");
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
