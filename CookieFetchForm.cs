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
			this.Text = "www.gyao.jp ����N�b�L�[���擾���Ă��܂��D�D�D";
			this.Icon = Utility.GetGExplorerIcon();
			this.DialogResult = DialogResult.None;
			
			this.webBrowser1.Navigated += delegate{
				//���o�^����cookie
				//GYAOSID=XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
				//�o�^���cookie
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
			};
			
			this.webBrowser1.Navigate("http://www.gyao.jp/");
		}
	}
}
