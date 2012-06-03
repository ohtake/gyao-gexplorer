using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class GCookieFetchForm : Form {
		public GCookieFetchForm() {
			InitializeComponent();
			this.Text = "www.gyao.jp ����N�b�L�[���擾���Ă��܂��D�D�D";
			this.Icon = Utility.GetGExplorerIcon();
			
			this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(
				delegate(object sender, WebBrowserNavigatedEventArgs e) {
					//���o�^����cookie
					//GYAOSID = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
					//�o�^���cookie
					//Cookie_UserId = 000000; Cookie_CookieId = 0000000000; GYAOSID = XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
					
					GCookie.Cookie = this.webBrowser1.Document.Cookie;
					if(GCookie.IsRegistered) {
						MessageBox.Show(
							"���[�U���̎擾�ɐ������܂����D\n"
							+ "���̃E�B���h�E����ă��C���t�H�[����\�����܂��D\n\n"
							+ "�N�b�L�[�̓��e\n"
							+ GCookie.Cookie,
							Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
						this.Close();
					} else {
						MessageBox.Show(
							"���[�U�����擾�ł��Ȃ��������߃v���O�������I�����܂��D\n\n"
							+ "�l�����錴��\n"
							+ "    - ���[�U�o�^���܂��s���Ă��Ȃ��D\n"
							+ "    - GExplorer �̃o�O�D\n"
							+ "    - �l�b�g���[�N�֘A�̕s��D\n"
							+ "    - GyaO �̎d�l���ς�����D\n\n"
							+ "�N�b�L�[�̓��e\n"
							+ GCookie.Cookie,
							Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						Application.Exit();
					}
				});
			
			this.webBrowser1.Navigate("http://www.gyao.jp/");
		}
	}
}
