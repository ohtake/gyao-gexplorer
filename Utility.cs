using System;
using System.Windows.Forms;
using System.IO;
using StringBuilder = System.Text.StringBuilder;
using Icon = System.Drawing.Icon;
using ComponentResourceManager = System.ComponentModel.ComponentResourceManager;
using Process = System.Diagnostics.Process;
using Resources = Yusen.GExplorer.Properties.Resources;

namespace Yusen.GExplorer {
	static class Utility{
		public static string GetPathForIE() {
			StringBuilder sb = new StringBuilder();
			sb.Append(Environment.GetEnvironmentVariable("ProgramFiles"));
			sb.Append(Path.DirectorySeparatorChar);
			sb.Append("Internet Explorer");
			sb.Append(Path.DirectorySeparatorChar);
			sb.Append("iexplore.exe");
			return sb.ToString();
		}
		
		public static string GetPathForWMP() {
			StringBuilder sb = new StringBuilder();
			sb.Append(Environment.GetEnvironmentVariable("ProgramFiles"));
			sb.Append(Path.DirectorySeparatorChar);
			sb.Append("Windows Media Player");
			sb.Append(Path.DirectorySeparatorChar);
			sb.Append("wmplayer.exe");
			return sb.ToString();
		}
		
		public static void DisplayException(Exception e) {
			MessageBox.Show(e.StackTrace, e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		
		public static Icon GetGExplorerIcon() {
			return Resources.Icon_3_973;
			//return Resources.Icon_4_13;
		}

		public static string ReadNextLineTextBeforeTag(TextReader reader) {
			string line = ReadNextLineHtml(reader);
			int indexOfLt = line.IndexOf('<');
			if(-1 != indexOfLt) {
				line = line.Substring(0, indexOfLt);
			}
			line = line.Replace("&nbsp;", " ");
			return line.Trim();
		}
		public static string ReadNextLineHtml(TextReader reader){
			string line = reader.ReadLine();
			if(null == line) return "";
			return line.Trim();
		}

		public static void AddHelpMenu(MenuStrip menuStrip) {
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
				"バージョン情報 (&A)", null,
				new EventHandler(delegate(object sender, EventArgs e) {
					new AboutBox().ShowDialog();
				}));
			ToolStripMenuItem help = new ToolStripMenuItem(
				"ヘルプ (&H)", null,
				read, change, new ToolStripSeparator(), about);
			menuStrip.Items.Add(help);
		}

		public static void BrowseByIE(Uri uri) {
			Process.Start(Utility.GetPathForIE(), uri.AbsoluteUri);
		}
	}
}

