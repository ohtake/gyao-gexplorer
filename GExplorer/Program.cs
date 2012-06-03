using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Media;
using System.Diagnostics;

namespace Yusen.GExplorer {
	static class Program {
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		static void Main() {
			ApplicationContext context = Program.InitializeProgram();
			Application.Run(context);
			Program.QuitProgram();
		}

		private static ApplicationContext InitializeProgram() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			//�J�����g�f�B���N�g�����X�^�[�g�A�b�v�p�X�ɂ��킹��
			Environment.CurrentDirectory = Application.StartupPath;

			SplashForm sf = new SplashForm();
			sf.Initialize("�N�����ł��D�D�D", 8+1);

			//�O���[�o���ݒ�
			sf.StepProgress("�O���[�o���ݒ�̓ǂݍ���");
			GlobalSettings.TryDeserialize();
			//���[�UID
			sf.StepProgress("���[�UID�̓ǂݎ��");
			if(!GlobalSettings.Instance.TryGetUserNumber()) {
				MessageBox.Show(
					"���[�UID���擾�ł��܂���ł����D�O���[�o���ݒ��ID�̐ݒ�����Ă��������D",
					Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			//�A�C�R���̓ǂݍ���
			sf.StepProgress("�A�C�R���̓ǂݍ���");
			try {
				string iconFileName = GlobalSettings.Instance.IconFile;
				if(string.IsNullOrEmpty(iconFileName)) {
					iconFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ico";
				}
				FormBase.CustomIcon = new Icon(iconFileName);
			} catch {
			}

			sf.StepProgress("�L���b�V���̏�����");
			Cache.Initialize();
			sf.StepProgress("�O���R�}���h�̓ǂݎ��");
			UserCommandsManager.Instance.DeserializeItems();
			sf.StepProgress("NG�R���e���c�̓ǂݎ��");
			NgContentsManager.Instance.DeserializeItems();
			sf.StepProgress("�v���C���X�g�̓ǂݎ��");
			PlayList.Instance.DeserializeItems();

			sf.StepProgress("���C���t�H�[���̍쐬");
			ApplicationContext context = new ApplicationContext(new MainForm());

			sf.EndProgress();

			return context;
		}
		private static void QuitProgram() {
			SplashForm sf = new SplashForm();
			sf.Initialize("�I�����ł��D�D�D", 5+1);
			sf.StepProgress("�v���C���X�g�̕ۑ�");
			PlayList.Instance.SerializeItems();
			sf.StepProgress("NG�R���e���c�̕ۑ�");
			NgContentsManager.Instance.SerializeItems();
			sf.StepProgress("�O���R�}���h�̕ۑ�");
			UserCommandsManager.Instance.SerializeItems();
			sf.StepProgress("�L���b�V���̕ۑ�");
			Cache.Serialize();
			sf.StepProgress("�O���[�o���ݒ�̕ۑ�");
			GlobalSettings.Serialize();
			sf.EndProgress();
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
			Program.DisplayException("Application.ThreadException", e.Exception);
		}
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
			Exception ex = e.ExceptionObject as Exception;
			if (null != ex) {
				Program.DisplayException("CurrentDomain.UnhandledException", ex);
			}
		}
		
		public static void DisplayException(string title, Exception e) {
			SystemSounds.Exclamation.Play();
			using (ExceptionDialog ed = new ExceptionDialog()) {
				ed.AllowAbort = true;
				ed.Exception = e;
				ed.Title = title;
				switch (ed.ShowDialog()) {
					case DialogResult.OK:
						break;
					case DialogResult.Cancel:
						Environment.Exit(1);
						break;
				}
			}
		}
	}
}
