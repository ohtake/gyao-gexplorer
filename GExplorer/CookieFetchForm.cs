using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer {
	/// <summary>
	/// GyaO�ł̃��[�U�����擾���邽�߂����ɑ��݂���t�H�[���D
	/// </summary>
	sealed partial class CookieFetchForm : FormBase {
		private static readonly Regex regexUserNo = new Regex(@"Cookie_UserId=([0-9]+)", RegexOptions.Singleline);
		
		private int? userNo = null;
		
		public CookieFetchForm() {
			InitializeComponent();
			
			this.Text = "www.gyao.jp ����N�b�L�[���擾���Ă��܂��D�D�D";
			this.DialogResult = DialogResult.None;
		}
		private void CookieFetchForm_Load(object sender, EventArgs e) {
			this.webBrowser1.Navigate("http://www.gyao.jp/about/");
		}
		private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
			// ���[�g�f�B���N�g���ɃA�N�Z�X�����ꍇ
			//���o�^����cookie
			//GYAOSID=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
			//�o�^���cookie
			//Cookie_UserId=000000; Cookie_CookieId=0000000000; GYAOSID=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
			//���[�g�ȊO���ƕʂ̂ɂȂ���ۂ�
			string cookie = this.webBrowser1.Document.Cookie;
			Match matchUserNo = CookieFetchForm.regexUserNo.Match(cookie);
			if (matchUserNo.Success) {
				this.userNo = int.Parse(matchUserNo.Groups[1].Value);
				this.DialogResult = DialogResult.OK;
			} else {
				this.DialogResult = DialogResult.Abort;
			}
			MessageBox.Show("�N�b�L�[�̓��e:\n\n" + cookie,
				Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			this.Close();
		}
		public int? UserNo {
			get { return this.userNo; }
		}
	}
}
