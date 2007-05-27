using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.UserInterfaces {
	public partial class RegistrationForm : BaseForm {
		public RegistrationForm() {
			InitializeComponent();
		}
		
		private void RegistrationForm_Load(object sender, EventArgs e) {
			this.Text = Program.ApplicationName;
			this.webBrowser1.DocumentText =
@"<html>
<body>
<p>
ウィンドウ下部の「HTTPSで視聴設定ページを開く」または「HTTPで視聴設定ページを開く」を選択してユーザ登録してください．
ボタンを2回クリックするとうまくいく場合もあるみたいです．
</p>
<p>
視聴設定が終わったら「視聴設定が終わったのでウィンドウを閉じる」を選択しアプリケーションの起動を続行してください．
</p>
</body>
</html>";
		}

		private void btnOpenHttps_Click(object sender, EventArgs e) {
			this.webBrowser1.Navigate("https://reg2.gyao.jp/register/");
		}
		private void btnOpenHttp_Click(object sender, EventArgs e) {
			this.webBrowser1.Navigate("http://reg2.gyao.jp/register/");
		}
		private void btnClose_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void btnQuit_Click(object sender, EventArgs e) {
			Environment.Exit(0);
		}
	}
}

