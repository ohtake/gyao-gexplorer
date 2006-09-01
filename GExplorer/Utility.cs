using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using System.Web;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	static class Utility{
		private static readonly string UserSettingsDir = Path.Combine(Application.StartupPath, "UserSettings");
		
		private static string GetPathForIE() {
			return Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
				"Internet Explorer" + Path.DirectorySeparatorChar + "iexplore.exe");
		}

		private static string GetPathForWMP() {
			return Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
				"Windows Media Player" + Path.DirectorySeparatorChar + "wmplayer.exe");
		}

		public static void Browse(Uri uri) {
			if (GlobalSettings.Instance.UseGBrowser) {
				BrowserForm.Browse(uri);
			} else if (!string.IsNullOrEmpty(GlobalSettings.Instance.BrowserPath)) {
				Process.Start(GlobalSettings.Instance.BrowserPath, uri.AbsoluteUri);
			} else {
				Process.Start(uri.AbsoluteUri);
			}
		}
		private static void BrowseWithIE(Uri uri) {
			Process.Start(Utility.GetPathForIE(), uri.AbsoluteUri);
		}

		public static Uri CreateLivedoorVideoGyaoSearchUri(string query) {
			//Encoding encoding = Encoding.GetEncoding("EUC-JP");
			Uri uri = new Uri(string.Format(
				"http://stream.search.livedoor.com/search/?svc=gyao&num=100&q={0}",
				/*HttpUtility.UrlEncode(query, encoding)*/ query)/*, false*/);
			return uri;
		}
		
		public static void SerializeSettings<T>(string filename, T settings) {
			XmlSerializer xs = new XmlSerializer(typeof(T));
			TextWriter tw = TextWriter.Null;
			try {
				DirectoryInfo di = new DirectoryInfo(Utility.UserSettingsDir);
				if (!di.Exists) di.Create();
				tw = new StreamWriter(Path.Combine(Utility.UserSettingsDir, filename));
				xs.Serialize(tw, settings);
			}catch(Exception e){
				Program.DisplayException(string.Format("設定ファイル '{0}' の書き出し失敗", filename), e);
			} finally {
				tw.Close();
			}
		}
		public static bool TryDeserializeSettings<T>(string filename, out T settings) {
			TextReader tr = TextReader.Null;
			try {
				DirectoryInfo di = new DirectoryInfo(Utility.UserSettingsDir);
				if (!di.Exists) di.Create();
				FileInfo fi = new FileInfo(Path.Combine(Utility.UserSettingsDir, filename));
				if (!fi.Exists) {
					goto failed;
				}
				XmlSerializer xs = new XmlSerializer(typeof(T));
				tr = new StreamReader(fi.FullName);
				settings = (T)xs.Deserialize(tr);
				return true;
			}catch(Exception e){
				Program.DisplayException(string.Format("設定ファイル '{0}' の読み取り失敗", filename), e);
			} finally {
				tr.Close();
			}
		failed:
			settings = default(T);
			return false;
		}
		
		public static void LoadSettingsAndEnableSaveOnClosedNew<TSettings>(IFormWithNewSettings<TSettings> form) where TSettings : INewSettings<TSettings>, new() {
			form.FormClosed += delegate {
				TSettings settingsSave = form.Settings;
				Utility.SerializeSettings(form.FilenameForSettings, settingsSave);
			};
			TSettings settingsLoad;
			if (!Utility.TryDeserializeSettings(form.FilenameForSettings, out settingsLoad)) {
				settingsLoad = new TSettings();
			}
			form.Settings.ApplySettings(settingsLoad);
		}
		public static void SubstituteAllPublicProperties<TNewSettings>(TNewSettings left, TNewSettings right) where TNewSettings : INewSettings<TNewSettings>, new() {
			Type type = typeof(TNewSettings);
			foreach (PropertyInfo pi in typeof(TNewSettings).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty)) {
				pi.SetValue(left, pi.GetValue(right, null), null);
			}
		}

		public static bool IsSorted<T>(IList<T> list, IComparer<T> comp) {
			if (list.Count <= 1) return true;
			for (int i=0; i<list.Count -1; i++) {
				if (comp.Compare(list[i], list[i+1]) > 0) return false;
			}
			return true;
		}

		public static void ToolStripDropDown_CancelClosingOnClick(object sender, ToolStripDropDownClosingEventArgs e) {
			switch (e.CloseReason) {
				case ToolStripDropDownCloseReason.ItemClicked:
					e.Cancel = true;
					break;
			}
		}
	}

	interface INewSettings<TNewSettings> where TNewSettings : new() {
		void ApplySettings(TNewSettings newSettings);
	}
	interface IHasNewSettings<TNewSettings> where TNewSettings : new() {
		TNewSettings Settings { get;}
	}
	interface IFormWithNewSettings<TNewSettings> : IHasNewSettings<TNewSettings> where TNewSettings : new() {
		string FilenameForSettings { get;}
		event FormClosedEventHandler FormClosed;
	}
}
