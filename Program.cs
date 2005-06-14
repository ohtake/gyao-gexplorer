using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Net;

namespace Yusen.GExplorer {
	static class Program {
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			if(DialogResult.Yes ==
				MessageBox.Show("IE で www.gyao.jp にアクセスすることでユーザ情報を取得します．\nよろしいですか？",
					Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)){
				Application.Run(new GCookieFetchForm());
				Application.Run(new MainForm());
			}
		}
	}
}
