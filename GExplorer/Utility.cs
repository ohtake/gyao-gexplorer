using System;
using System.Windows.Forms;
using System.IO;
using Process = System.Diagnostics.Process;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Yusen.GExplorer {
	static class Utility{
		private delegate void SetTitlebarTextDelegate(Form form, string title);

		private const string UserSettingsDir = "UserSettings";
		
		public static string GetPathForIE() {
			return Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
				"Internet Explorer" + Path.DirectorySeparatorChar + "iexplore.exe");
		}
		
		public static string GetPathForWMP() {
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
		
		public static void SetTitlebarText(Form form, string title) {
			if(form.InvokeRequired) {
				form.Invoke(new SetTitlebarTextDelegate(Utility.SetTitlebarText),
					new object[] { form, title });
			} else {
				form.Text = title;
			}
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
				"バージョン情報 (&A) ...", null,
				new EventHandler(delegate(object sender, EventArgs e) {
					using (AboutBox abox = new AboutBox()) {
						abox.ShowDialog((sender as ToolStripMenuItem).Tag as Form);
					}
				}));
			about.Tag = menuStrip.FindForm();
			
			ToolStripMenuItem help = new ToolStripMenuItem(
				"ヘルプ (&H)", null,
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
			} finally {
				if (null != tw) tw.Close();
			}
		}
		public static bool TryDeserializeSettings<T>(string filename, out T settings) {
			XmlSerializer xs = new XmlSerializer(typeof(T));
			TextReader tr = null;
			try {
				DirectoryInfo di = new DirectoryInfo(Utility.UserSettingsDir);
				if (!di.Exists) di.Create();
				tr = new StreamReader(Path.Combine(Utility.UserSettingsDir, filename));
				settings = (T)xs.Deserialize(tr);
				return true;
			} catch (FileNotFoundException) {
				settings = default(T);
				return false;
			} finally {
				if (null != tr) tr.Close();
			}
		}
		
		public static void LoadSettingsAndEnableSaveOnClosed<TSettings>(IFormWithSettings<TSettings> form) where TSettings : new(){
			form.FormClosed += delegate {
				TSettings settings2 = new TSettings();
				form.FillSettings(settings2);
				Utility.SerializeSettings(form.FilenameForSettings, settings2);
			};
			TSettings settings;
			if (Utility.TryDeserializeSettings(form.FilenameForSettings, out settings)) {
				form.ApplySettings(settings);
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
	
	interface IHasSettings<TSettings> where TSettings : new() {
		void FillSettings(TSettings settings);
		void ApplySettings(TSettings settings);
	}
	interface IFormWithSettings<TSettings> : IHasSettings<TSettings> where TSettings : new() {
		string FilenameForSettings { get;}
		event FormClosedEventHandler FormClosed;
	}
}
