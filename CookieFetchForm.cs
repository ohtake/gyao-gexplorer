using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer {
	partial class CookieFetchForm : Form {
		private static readonly Regex regexUserNo = new Regex(@"Cookie_UserId *= *([0-9]+)", RegexOptions.Singleline);
		
		public CookieFetchForm() {
			InitializeComponent();
			this.Text = "www.gyao.jp ����N�b�L�[���擾���Ă��܂��D�D�D";
			this.Icon = Utility.GetGExplorerIcon();
			this.DialogResult = DialogResult.None;
			
			this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(
				delegate(object sender, WebBrowserNavigatedEventArgs e) {
					//���o�^����cookie
					//GYAOSID = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
					//�o�^���cookie
					//Cookie_UserId = 000000; Cookie_CookieId = 0000000000; GYAOSID = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
					string cookie = this.webBrowser1.Document.Cookie;
					Match match = CookieFetchForm.regexUserNo.Match(cookie);
					if(match.Success) {
						UserSettings.Instance.GyaoUserNo = int.Parse(match.Groups[1].Value);
						MessageBox.Show(
							"���[�U���̎擾�ɐ������܂����D\n"
							+ "���̃E�B���h�E����ă��C���t�H�[����\�����܂��D\n\n"
							+ "�N�b�L�[�̓��e\n"
							+ cookie,
							Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.DialogResult = DialogResult.OK;
					} else {
						MessageBox.Show(
							"���[�U�����擾�ł��Ȃ��������߃v���O�������I�����܂��D\n\n"
							+ "�l�����錴��\n"
							+ "    - ���[�U�o�^���܂��s���Ă��Ȃ��D\n"
							+ "    - GExplorer �̃o�O�D\n"
							+ "    - �l�b�g���[�N�֘A�̕s��D\n"
							+ "    - GyaO �̎d�l���ς�����D\n\n"
							+ "�N�b�L�[�̓��e\n"
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
