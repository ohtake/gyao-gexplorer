using System;
using System.ComponentModel;
using System.IO;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;
using System.Drawing;

namespace Yusen.GExplorer {
	interface IUsesUserSettings {
		void LoadSettings();
		void SaveSettings();
	}
	
	delegate void UserSettingsChangeCompletedEventHandler();
	
	/// <summary>�r�b�g���[�g</summary>
	public enum GBitRate {
		High = 2,
		Low = 1,
	}
	
	public class UserSettings {
		private static UserSettings instance = new UserSettings();
		private const string filename = "UserSettings.xml";
		
		public static UserSettings Instance {
			get {
				return UserSettings.instance;
			}
		}
		
		public static void LoadSettingsFromFile() {
			TextReader tr = null;
			try {
				XmlSerializer xs = new XmlSerializer(typeof(UserSettings));
				tr = new StreamReader(UserSettings.filename);
				UserSettings.instance = (UserSettings)xs.Deserialize(tr);
			} catch(FileNotFoundException) {
				return;
			} catch(Exception e) {
				Utility.DisplayException(e);
			} finally {
				if(null != tr) {
					tr.Close();
				}
				UserSettings.Instance.OnChangeCompleted();
			}
		}
		
		public static void SaveSettingsToFile() {
			TextWriter tw = null;
			try {
				XmlSerializer xs = new XmlSerializer(typeof(UserSettings));
				tw = new StreamWriter(UserSettings.filename);
				xs.Serialize(tw, UserSettings.Instance);
			} catch(Exception e) {
				Utility.DisplayException(e);
			} finally {
				if(null != tw) {
					tw.Close();
				}
			}
		}
		
		// �V���O���g��������XML�f�V���A���C�Y�ɕK�v�����炵�Ⴀ�Ȃ�
		public UserSettings() {
		}

		private bool enableChangeCompletedEvent = true;
		internal event UserSettingsChangeCompletedEventHandler ChangeCompleted;
		internal void OnChangeCompleted() {
			if(null != this.ChangeCompleted && this.enableChangeCompletedEvent) {
				this.ChangeCompleted();
			}
		}
		internal void OnChangeCompleted(bool notifyAll) {
			if(notifyAll) {
				this.enableChangeCompletedEvent = false;
				this.MainForm.OnChangeCompleted();
				this.PlayerForm.OnChangeCompleted();
				this.ContentPropertyViewer.OnChangeCompleted();
				this.UserCommandsEditor.OnChangeCompleted();
				this.UserSettingsToolbox.OnChangeCompleted();
				this.enableChangeCompletedEvent = true;
			}
			this.OnChangeCompleted();
		}
		
		/// <summary>
		/// �ݒ�t�@�C���� userNo ���ۑ�����Ă�����Ď擾�̕K�v�Ȃ��Ȃ̂� false ���Ԃ�D
		/// </summary>
		[Browsable(false)]
		public bool RequireCookie {
			get {
				return 0 == this.gyaoUserId;
			}
		}
		
		private int gyaoUserId = 0;
		private GBitRate gyaoBitRate = GBitRate.High;
		private bool gyaoEnableConcurrentFetch = true;
		[Category("GyaO�Ƃ̒ʐM")]
		[DisplayName("userNo")]
		[Description("�N�b�L�[�ɕۑ�����Ă��� userNo �̒l�D���������ύX�s�D")]
		[DefaultValue(0)]
		[ReadOnly(true)]
		public int GyaoUserNo {
			get {
				return this.gyaoUserId;
			}
			set {
				this.gyaoUserId = value;
			}
		}
		[Category("GyaO�Ƃ̒ʐM")]
		[DisplayName("�r�b�g���[�g")]
		[Description("�Đ����铮��̃r�b�g���[�g�D��p�v���[�������łȂ�WMP�ł̍Đ������̐ݒ�̉e�����󂯂܂��D")]
		[DefaultValue(GBitRate.High)]
		public GBitRate GyaoBitRate {
			get {
				return this.gyaoBitRate;
			}
			set {
				this.gyaoBitRate = value;
			}
		}
		[Category("GyaO�Ƃ̒ʐM")]
		[DisplayName("rateId")]
		[Description("GyaO�ɑ��M����r�b�g���[�g��ID�̒l�D")]
		[DefaultValue("bit0000002")]
		public string GyaoBitRateId {
			get {
				return "bit" + ((int)this.gyaoBitRate).ToString("0000000");
			}
		}
		[Category("GyaO�Ƃ̒ʐM")]
		[DisplayName("����ǂݍ���")]
		[Description("GyaO�̃E�F�u�y�[�W�̎擾����񉻂��č�������}��D���ɂ���Ă͋t�ɒx���Ȃ�ꍇ�����邩���D")]
		[DefaultValue(true)]
		public bool GyaoEnableConcurrentFetch {
			get {
				return this.gyaoEnableConcurrentFetch;
			}
			set {
				this.gyaoEnableConcurrentFetch = value;
			}
		}

		[Category("�e�E�B���h�E���Ƃ̐ݒ�")]
		[DisplayName("���C���t�H�[��")]
		public UscMainForm MainForm {
			get { return this.mainForm; }
			set { this.mainForm = value; }
		}
		private UscMainForm mainForm = new UscMainForm(new Size(600, 450), new Point(100, 100));

		[Category("�e�E�B���h�E���Ƃ̐ݒ�")]
		[DisplayName("�v���[��")]
		public UscPlayerForm PlayerForm {
			get { return this.playerForm; }
			set { this.playerForm = value; }
		}
		private UscPlayerForm playerForm = new UscPlayerForm(new Size(670, 640), new Point(0, 0));
		
		[Category("�e�E�B���h�E���Ƃ̐ݒ�")]
		[DisplayName("���[�U�ݒ�c�[���{�b�N�X")]
		public UscFormTopmostable UserSettingsToolbox {
			get {return this.userSettingsToolbox;}
			set {this.userSettingsToolbox = value;}
		}
		private UscFormTopmostable userSettingsToolbox = new UscFormTopmostable(new Size(240, 400), new Point(50, 150));

		[Category("�e�E�B���h�E���Ƃ̐ݒ�")]
		[DisplayName("�R���e���c�v���p�e�B�r���[�A")]
		public UscFormTopmostable ContentPropertyViewer {
			get { return this.contentPropertyViewer; }
			set { this.contentPropertyViewer = value; }
		}
		private UscFormTopmostable contentPropertyViewer = new UscFormTopmostable(new Size(260, 310), new Point(500, 50));

		[Category("�e�E�B���h�E���Ƃ̐ݒ�")]
		[DisplayName("�O���R�}���h�G�f�B�^")]
		public UscForm UserCommandsEditor {
			get { return this.userCommandsEditor; }
			set { this.userCommandsEditor = value; }
		}
		private UscForm userCommandsEditor = new UscForm(new Size(360, 340), new Point(200, 50));
	}
}
