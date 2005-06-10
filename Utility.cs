using System;
using System.Windows.Forms;
using StringBuilder = System.Text.StringBuilder;
using Path = System.IO.Path;

namespace Yusen.GExplorer {
	public abstract class Utility{
		public static string GetPathForIE() {
			StringBuilder sb = new StringBuilder();
			sb.Append(Environment.GetEnvironmentVariable("ProgramFiles"));
			sb.Append(Path.DirectorySeparatorChar);
			sb.Append("Internet Exploer");
			sb.Append(Path.DirectorySeparatorChar);
			sb.Append("iexplorer.exe");
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
	}
}
