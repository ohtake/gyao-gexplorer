﻿using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;

namespace Yusen.GExplorer {
	static class Utility{
		private const string UserSettingsDir = "UserSettings";
		
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
		public static void PlayWithWMP(Uri uri) {
			Process.Start(Utility.GetPathForWMP(), uri.AbsoluteUri);
		}
		
		public static void AppendHelpMenu(MenuStrip menuStrip) {
			ToolStripMenuItem read = new ToolStripMenuItem(
				"&ReadMe.txt", null,
				new EventHandler(delegate(object sender, EventArgs e) {
					Process.Start("ReadMe.txt");
				}),
				Keys.F1);
			ToolStripMenuItem change = new ToolStripMenuItem(
				"&ChangeLog.txt", null,
				new EventHandler(delegate(object sender, EventArgs e) {
					Process.Start("ChangeLog.txt");
				}),
				Keys.Shift | Keys.F1);
			ToolStripMenuItem about = new ToolStripMenuItem(
				"バージョン情報(&A)...", null,
				new EventHandler(delegate(object sender, EventArgs e) {
					using (AboutBox abox = new AboutBox()) {
						abox.ShowDialog((sender as ToolStripMenuItem).Tag as Form);
					}
				}));
			about.Tag = menuStrip.FindForm();
			
			ToolStripMenuItem help = new ToolStripMenuItem(
				"ヘルプ(&H)", null,
				read, change, new ToolStripSeparator(), about);
			
			menuStrip.Items.Add(help);
		}
		
		public static void SerializeSettings<T>(string filename, T settings) {
			XmlSerializer xs = new XmlSerializer(typeof(T));
			TextWriter tw = null;
			try {
				DirectoryInfo di = new DirectoryInfo(Utility.UserSettingsDir);
				if (!di.Exists) di.Create();
				tw = new StreamWriter(Path.Combine(Utility.UserSettingsDir, filename));
				xs.Serialize(tw, settings);
			}catch(Exception e){
				Program.DisplayException(string.Format("設定ファイル '{0}' の書き出し", filename), e);
			} finally {
				if (null != tw) tw.Close();
			}
		}
		public static bool TryDeserializeSettings<T>(string filename, out T settings) {
			TextReader tr = null;
			try {
				DirectoryInfo di = new DirectoryInfo(Utility.UserSettingsDir);
				if (!di.Exists) di.Create();
				tr = new StreamReader(Path.Combine(Utility.UserSettingsDir, filename));
				XmlSerializer xs = new XmlSerializer(typeof(T));
				settings = (T)xs.Deserialize(tr);
				return true;
			} catch(FileNotFoundException) {
			}catch(Exception e){
				Program.DisplayException(string.Format("設定ファイル '{0}'  の読み取り", filename), e);
			} finally {
				if (null != tr) tr.Close();
			}
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

		public static bool TryGetUserProfileOf(int userNo, out string profile) {
			try {
				HttpWebRequest req = WebRequest.Create("http://www.gyao.jp/sityou/movie/contentsId/cnt0000000/rateId/bit0000002/login_from/shityou/") as HttpWebRequest;
				CookieContainer cc = new CookieContainer();
				cc.Add(new Cookie("Cookie_UserId", userNo.ToString(), "/", "www.gyao.jp"));
				cc.Add(new Cookie("Cookie_CookieId", "0", "/", "www.gyao.jp"));
				req.CookieContainer = cc;
				WebResponse res = req.GetResponse();
				using(TextReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("Shift_JIS"))) {
					Regex regex = new Regex(@"(sex=\w+;(\w+=\w+;)*)sz=");
					string line;
					while(null != (line = reader.ReadLine())) {
						Match match = regex.Match(line);
						if(match.Success) {
							profile = match.Groups[1].Value;
							return true;
						}
					}
					profile = null;
					return false;
				}
			} catch(Exception e) {
				Program.DisplayException("Utility.TryGetUserProfileOf", e);
				profile = null;
				return false;
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
