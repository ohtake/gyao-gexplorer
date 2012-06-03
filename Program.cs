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
				MessageBox.Show("IE �� www.gyao.jp �ɃA�N�Z�X���邱�ƂŃ��[�U�����擾���܂��D\n��낵���ł����H",
					Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)){
				Application.Run(new GCookieFetchForm());
				Application.Run(new MainForm());
			}
		}
	}
}
