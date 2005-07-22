//#define PRINT_EXCEPTION_TO_STDERR

using System;
using System.Windows.Forms;
using System.IO;
using Icon = System.Drawing.Icon;
using ComponentResourceManager = System.ComponentModel.ComponentResourceManager;
using Process = System.Diagnostics.Process;
using Resources = Yusen.GExplorer.Properties.Resources;
using PropertyInfo = System.Reflection.PropertyInfo;
using System.Reflection;

namespace Yusen.GExplorer {
	static class Utility{
		private delegate void SetTitlebarTextDelegate(Form form, string title);
		
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
		
		public static void DisplayException(Exception e) {
#if !PRINT_EXCEPTION_TO_STDERR
			MessageBox.Show(e.Message + "\n\n" + e.StackTrace, e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
			Console.Error.WriteLine(e.GetType().ToString());
			Console.Error.WriteLine(e.Message);
			Console.Error.WriteLine(e.StackTrace);
			Console.Error.WriteLine();
#endif
		}
		
		public static Icon GetGExplorerIcon() {
			return Resources.GExplorer_16x16_16;
		}
		
		public static string ReadNextLineTextBeforeTag(TextReader reader) {
			string line = ReadNextLineHtml(reader);
			int indexOfLt = line.IndexOf('<');
			if(-1 != indexOfLt) {
				line = line.Substring(0, indexOfLt);
			}
			line = Utility.UnescapeHtmlEntity(line);
			return line.Trim();
		}
		public static string ReadNextLineHtml(TextReader reader){
			string line = reader.ReadLine();
			if(null == line) return "";
			return line.Trim();
		}
		public static string UnescapeHtmlEntity(string input) {
			input = input.Replace("&nbsp;", " ");
			input = input.Replace("&lt;", "<");
			input = input.Replace("&gt;", ">");
			input = input.Replace("&apos;", "'");
			input = input.Replace("&quot;", "\"");
			input = input.Replace("&amp;", "&");
			return input;
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
				"バージョン情報 (&A)...", null,
				new EventHandler(delegate(object sender, EventArgs e) {
					new AboutBox().ShowDialog();
				}));
			ToolStripMenuItem help = new ToolStripMenuItem(
				"ヘルプ (&H)", null,
				read, change, new ToolStripSeparator(), about);
			menuStrip.Items.Add(help);
		}
		
		public static void BrowseWithIE(Uri uri) {
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
	}
}
