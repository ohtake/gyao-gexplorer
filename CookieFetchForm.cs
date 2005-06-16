using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer {
	partial class CookieFetchForm : Form {
		private static readonly Regex regexUserNo = new Regex(@"Cookie_UserId *= *([0-9]+)", RegexOptions.Singleline);
		
		public CookieFetchForm() {
			InitializeComponent();
			this.Text = "www.gyao.jp からクッキーを取得しています．．．";
			this.Icon = Utility.GetGExplorerIcon();
			this.DialogResult = DialogResult.None;
			
			this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(
				delegate(object sender, WebBrowserNavigatedEventArgs e) {
					//未登録時のcookie
					//GYAOSID = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
					//登録後のcookie
					//Cookie_UserId = 000000; Cookie_CookieId = 0000000000; GYAOSID = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
					string cookie = this.webBrowser1.Document.Cookie;
					Match match = CookieFetchForm.regexUserNo.Match(cookie);
					if(match.Success) {
						UserSettings.Instance.GyaoUserNo = int.Parse(match.Groups[1].Value);
						MessageBox.Show(
							"ユーザ情報の取得に成功しました．\n"
							+ "このウィンドウを閉じてメインフォームを表示します．\n\n"
							+ "クッキーの内容\n"
							+ cookie,
							Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.DialogResult = DialogResult.OK;
					} else {
						MessageBox.Show(
							"ユーザ情報を取得できなかったためプログラムを終了します．\n\n"
							+ "考えられる原因\n"
							+ "    - ユーザ登録をまだ行っていない．\n"
							+ "    - GExplorer のバグ．\n"
							+ "    - ネットワーク関連の不具合．\n"
							+ "    - GyaO の仕様が変わった．\n\n"
							+ "クッキーの内容\n"
							+ cookie,
							Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						this.DialogResult = DialogResult.Abort;
					}
					this.Close();
				});
			
			this.webBrowser1.Navigate("http://www.gyao.jp/");
		}
	}
}
