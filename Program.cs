using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Xml.Serialization;
using System.IO;

namespace Yusen.GExplorer {
	static class Program {
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			
			UserSettings.LoadSettingsFromFile();
			if(UserSettings.Instance.RequireCookie){
				if(DialogResult.Yes !=
					MessageBox.Show(
						"IE で www.gyao.jp にアクセスすることでユーザ情報を取得します．\n"
						+ "よろしいですか？",
						Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
					return;
				}
				if(DialogResult.OK != new CookieFetchForm().ShowDialog()) {
					return;
				}
			}
			
			UserCommandsManager.LoadCommandsFromFile();
			Application.Run(new MainForm());
			UserCommandsManager.SaveCommandsToFile();
			UserSettings.SaveSettingsToFile();
		}
	}
}

