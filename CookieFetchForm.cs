using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer {
	partial class CookieFetchForm : Form {
		private static readonly Regex regexUserNo = new Regex(@"Cookie_UserId=([0-9]+)", RegexOptions.Singleline);
#if COOKIE
		private static readonly Regex regexCookieId = new Regex(@"Cookie_CookieId=([0-9]+)", RegexOptions.Singleline);
		private static readonly Regex regexSid = new Regex(@"GYAOSID=([0-9a-f]{32})", RegexOptions.Singleline);
#endif
		public CookieFetchForm() {
			InitializeComponent();
			this.Text = "www.gyao.jp からクッキーを取得しています．．．";
			this.Icon = Utility.GetGExplorerIcon();
			this.DialogResult = DialogResult.None;
			
			this.webBrowser1.Navigated += delegate{
				//未登録時のcookie
				//GYAOSID=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
				//登録後のcookie
				//Cookie_UserId=000000; Cookie_CookieId=0000000000; GYAOSID=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
				string cookie = this.webBrowser1.Document.Cookie;
				Match matchUserNo = CookieFetchForm.regexUserNo.Match(cookie);
#if COOKIE
				Match matchCookieId = CookieFetchForm.regexCookieId.Match(cookie);
				Match matchSid = CookieFetchForm.regexSid.Match(cookie);
#endif
#if COOKIE
				if(matchUserNo.Success && matchCookieId.Success && matchSid.Success) {
#else
				if(matchUserNo.Success) {
#endif
					UserSettings.Instance.GyaoUserNo = int.Parse(matchUserNo.Groups[1].Value);
#if COOKIE
					UserSettings.Instance.GyaoCookieId = int.Parse(matchCookieId.Groups[1].Value);
					UserSettings.Instance.GyaoSessionId = matchSid.Groups[1].Value;
#endif
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
			};
			
			this.webBrowser1.Navigate("http://www.gyao.jp/");
		}
	}
}
