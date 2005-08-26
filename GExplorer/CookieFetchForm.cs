using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer {
	/// <summary>
	/// GyaOでのユーザ情報を取得するためだけに存在するフォーム．
	/// </summary>
	sealed partial class CookieFetchForm : FormBase {
		private static readonly Regex regexUserNo = new Regex(@"Cookie_UserId=([0-9]+)", RegexOptions.Singleline);
		public CookieFetchForm() {
			InitializeComponent();
			this.Text = "www.gyao.jp からクッキーを取得しています．．．";
			this.DialogResult = DialogResult.None;
			
			this.webBrowser1.Navigated += delegate{
				// ルートディレクトリにアクセスした場合
				//未登録時のcookie
				//GYAOSID=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
				//登録後のcookie
				//Cookie_UserId=000000; Cookie_CookieId=0000000000; GYAOSID=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
				//ルート以外だと別のになるっぽい
				string cookie = this.webBrowser1.Document.Cookie;
				Match matchUserNo = CookieFetchForm.regexUserNo.Match(cookie);
				if(matchUserNo.Success) {
					GlobalSettings.Instance.UserNo = int.Parse(matchUserNo.Groups[1].Value);
					this.DialogResult = DialogResult.OK;
				} else {
					this.DialogResult = DialogResult.Abort;
				}
				MessageBox.Show("クッキーの内容:\n\n" + cookie,
					Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.Close();
			};
			
			this.webBrowser1.Navigate("http://www.gyao.jp/about/");
		}
	}
}
