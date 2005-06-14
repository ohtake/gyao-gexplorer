using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class GCookieFetchForm : Form {
		public GCookieFetchForm() {
			InitializeComponent();
			this.Text = "www.gyao.jp からクッキーを取得しています．．．";
			this.Icon = Utility.GetGExplorerIcon();
			
			this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(
				delegate(object sender, WebBrowserNavigatedEventArgs e) {
					//未登録時のcookie
					//GYAOSID = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
					//登録後のcookie
					//Cookie_UserId = 000000; Cookie_CookieId = 0000000000; GYAOSID = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
					
					GCookie.Cookie = this.webBrowser1.Document.Cookie;
					if(GCookie.IsRegistered) {
						MessageBox.Show(
							"ユーザ情報の取得に成功しました．\n"
							+ "このウィンドウを閉じてメインフォームを表示します．\n\n"
							+ "クッキーの内容\n"
							+ GCookie.Cookie,
							Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.Close();
					} else {
						MessageBox.Show(
							"ユーザ情報を取得できなかったためプログラムを終了します．\n\n"
							+ "考えられる原因\n"
							+ "    - ユーザ登録をまだ行っていない．\n"
							+ "    - GExplorer のバグ．\n"
							+ "    - ネットワーク関連の不具合．\n"
							+ "    - GyaO の仕様が変わった．\n\n"
							+ "クッキーの内容\n"
							+ GCookie.Cookie,
							Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						Application.Exit();
					}
				});
			
			this.webBrowser1.Navigate("http://www.gyao.jp/");
		}
	}
}
