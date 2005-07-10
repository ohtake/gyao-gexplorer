using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	static class Program {
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			
			UserSettings.LoadSettingsFromFile();
			if(UserSettings.Instance.IsCookieRequired) {
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
			
			UserCommandsManager.Instance.DeserializeItems();
			NgPackagesManager.Instance.DeserializeItems();
			
			Application.Run(MainForm.Instance);
			
			NgPackagesManager.Instance.SerializeItems();
			UserCommandsManager.Instance.SerializeItems();
			UserSettings.SaveSettingsToFile();
		}
	}
}

