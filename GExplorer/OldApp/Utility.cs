using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.IO;

namespace Yusen.GExplorer.OldApp {
	static class Utility{
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
		public static void ToolStripDropDown_CancelClosingOnClick(object sender, ToolStripDropDownClosingEventArgs e) {
			switch (e.CloseReason) {
				case ToolStripDropDownCloseReason.ItemClicked:
					e.Cancel = true;
					break;
			}
		}
	}
}
