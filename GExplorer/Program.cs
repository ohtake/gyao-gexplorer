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
		private const int InitializationSteps = 8;
		private const int SerializationSteps = 5;
		private static SplashForm splashInit;
		private static MainForm mainForm;
		internal static event EventHandler<ProgramSerializationProgressEventArgs> ProgramSerializationProgress;
		
		/// <summary>The main entry point for the application.</summary>
		[STAThread]
		private static void Main() {
			//���܂��Ȃ�
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//�L���b�`����Ȃ���O���L���b�`
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			//���d�N���`�F�b�N
			while(Program.CheckMultipleExecution()) {
				switch(MessageBox.Show("���d�N���Ǝv���܂��D�ǂ����܂����H", Application.ProductName + " " + Application.ProductVersion, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning)) {
					case DialogResult.Abort:
						return;
					case DialogResult.Retry:
						continue;
					case DialogResult.Ignore:
						break;
				}
				break;
			}
			//�J�����g�f�B���N�g�����X�^�[�g�A�b�v�p�X�ɂ��킹��
			Environment.CurrentDirectory = Application.StartupPath;
			
			Program.splashInit = new SplashForm();
			Program.splashInit.Initialize("�N�����ł��D�D�D", Program.InitializationSteps +1);
			Program.InitializeProgram();
			Program.mainForm.Load += delegate {
				Program.splashInit.EndProgress();
				Program.splashInit = null;
			};
			
			Application.Run(Program.mainForm);
		}

		private static bool CheckMultipleExecution() {
			Process curProc = Process.GetCurrentProcess();
			foreach(Process p in Process.GetProcesses()) {
				if(curProc.Id.Equals(p.Id)) {
					continue;
				}
				try {
					if(curProc.MainModule.FileName.Equals(p.MainModule.FileName)) {
						return true;
					}
				} catch {
				}
			}
			return false;
		}

		private static void InitializeProgram() {
			//�O���[�o���ݒ�
			Program.splashInit.StepProgress("�O���[�o���ݒ�̓ǂݍ���");
			GlobalSettings.TryDeserialize();
			//���[�UID
			Program.splashInit.StepProgress("���[�UID�̓ǂݎ��");
			if(!GlobalSettings.Instance.TryGetUserNumber()) {
				MessageBox.Show(
					"���[�UID���擾�ł��܂���ł����D�O���[�o���ݒ��ID�̐ݒ�����Ă��������D",
					Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			//�A�C�R���̓ǂݍ���
			Program.splashInit.StepProgress("�A�C�R���̓ǂݍ���");
			try {
				string iconFileName = GlobalSettings.Instance.IconFile;
				if(string.IsNullOrEmpty(iconFileName)) {
					iconFileName = Path.GetFileNameWithoutExtension(Application.ExecutablePath) + ".ico";
				}
				FormBase.CustomIcon = new Icon(iconFileName);
			} catch {
			}

			Program.splashInit.StepProgress("�L���b�V���̏�����");
			Cache.Initialize();
			Program.splashInit.StepProgress("�O���R�}���h�̓ǂݎ��");
			UserCommandsManager.Instance.DeserializeItems();
			Program.splashInit.StepProgress("NG�R���e���c�̓ǂݎ��");
			NgContentsManager.Instance.DeserializeItems();
			Program.splashInit.StepProgress("�v���C���X�g�̓ǂݎ��");
			PlayList.Instance.DeserializeItems();

			Program.splashInit.StepProgress("���C���t�H�[���̍쐬");
			Program.mainForm = new MainForm();
		}
		private static void OnProgramSerializationProgress(int current, string message) {
			if(null != Program.ProgramSerializationProgress) {
				Program.ProgramSerializationProgress(null, new ProgramSerializationProgressEventArgs(current, Program.SerializationSteps, message));
			}
		}
		internal static void SerializeSettings() {
			int step = 0;
			Program.OnProgramSerializationProgress(step++, "�v���C���X�g�̕ۑ�");
			PlayList.Instance.SerializeItems();
			Program.OnProgramSerializationProgress(step++, "NG�R���e���c�̕ۑ�");
			NgContentsManager.Instance.SerializeItems();
			Program.OnProgramSerializationProgress(step++, "�O���R�}���h�̕ۑ�");
			UserCommandsManager.Instance.SerializeItems();
			Program.OnProgramSerializationProgress(step++, "�L���b�V���̕ۑ�");
			Cache.Serialize();
			Program.OnProgramSerializationProgress(step++, "�O���[�o���ݒ�̕ۑ�");
			GlobalSettings.Serialize();
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
		
		private static void DisplayException(string title, Exception e) {
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

	sealed class ProgramSerializationProgressEventArgs : EventArgs{
		private int current;
		private int max;
		private string message;
		public ProgramSerializationProgressEventArgs(int current, int max, string message) {
			this.current = current;
			this.max = max;
			this.message = message;
		}
		public int Current {
			get { return this.current; }
		}
		public int Max {
			get { return this.max; }
		}
		public string Message {
			get { return this.message; }
		}
	}

}
