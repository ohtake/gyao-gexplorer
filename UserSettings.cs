//#define COOKIE

using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using System.Drawing;

#if COOKIE
using System.Net;
using NumberStyles = System.Globalization.NumberStyles;
#endif

namespace Yusen.GExplorer {
	interface IUsesUserSettings {
		void LoadSettings();
		void SaveSettings();
	}
	
	/// <summary>
	/// ���[�U�ݒ肪�ς�����ꍇ�ɌĂяo�����D
	/// <see cref="UserSettings"/>�܂��͂��̒����m�[�h��<see cref="UserSettingsChild"/>���甭���D
	/// </summary>
	delegate void UserSettingsChangeCompletedEventHandler();
	
	/// <summary>�r�b�g���[�g</summary>
	public enum GBitRate {
		SuperFine = 2,
		Standard = 1,
	}
	/// <summary>���ځ[��̕��@�D�e�����o���̗R����JaneView���D</summary>
	public enum AboneType {
		/// <summary>�Ƃ��߂�</summary>
		Toumei,
		/// <summary>���ڂ�D����ł�NG�Ώۂł����Ă��ʐF�ŕ\��������͂��Ȃ��D</summary>
		Sabori,
		/// <summary>�͂�����</summary>
		Hakidame,
	}
	
	/// <summary>���[�U�ݒ�̃V���O���g���D</summary>
	public class UserSettings {
		private static UserSettings instance = new UserSettings();
		private const string filename = "UserSettings.xml";
		/// <summary>�N�b�L�[�̎���</summary>
		private const double CookieExpiresInDays = 7;

		internal static UserSettings Instance {
			get {
				return UserSettings.instance;
			}
		}
		
		internal static void LoadSettingsFromFile() {
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
				UserSettings.Instance.OnChangeCompleted(true);
			}
		}

		internal static void SaveSettingsToFile() {
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
				this.BrowserForm.OnChangeCompleted();
				this.PlayerForm.OnChangeCompleted();
				this.ContentPropertyViewer.OnChangeCompleted();
				this.UserCommandsEditor.OnChangeCompleted();
				this.UserSettingsToolbox.OnChangeCompleted();
				this.NgPackagesEditor.OnChangeCompleted();
				this.enableChangeCompletedEvent = true;
			}
			this.OnChangeCompleted();
		}
		
		/// <summary>
		/// �ݒ�t�@�C���� userNo ���ۑ�����ĂȂ�������N�b�L�[���Â�������Ď擾�̕K�v������
		/// true ���Ԃ�D
		/// </summary>
		[Browsable(false)]
		internal bool IsCookieRequired {
			get {
				return
					0 == this.gyaoUserId
					|| this.gyaoCookieTime.AddDays(UserSettings.CookieExpiresInDays) < DateTime.Now;
			}
		}
#if COOKIE
		private CookieCollection gyaoCookie = null;
		public void CreateCookieCollection() {
			this.gyaoCookie = new CookieCollection();
			this.gyaoCookie.Add(new Cookie("Cookie_UserId", this.GyaoUserNo.ToString(), "/", "www.gyao.jp"));
			this.gyaoCookie.Add(new Cookie("Cookie_CookieId", this.GyaoCookieId.ToString(), "/", "www.gyao.jp"));
			this.gyaoCookie.Add(new Cookie("GYAOSID", this.GyaoSessionId, "/", "www.gyao.jp"));
		}
		[XmlIgnore]
		[Browsable(false)]
		public CookieCollection GyaoCookie {
			get {
				return this.gyaoCookie;
			}
		}
#endif

		private int gyaoUserId = 0;
		private DateTime gyaoCookieTime = DateTime.MinValue;
#if COOKIE
		private int gyaoCookieId = 0;
		private long gyaoSessionIdHigh = 0;
		private long gyaoSessionIdLow = 0;
#endif
		private GBitRate gyaoBitRate = GBitRate.SuperFine;
		private bool gyaoEnableConcurrentFetch = true;
		[Category("GyaO�Ƃ̒ʐM")]
		[DisplayName("Cookie_UserId")]
		[Description("�N�b�L�[�ɕۑ�����Ă��� Cookie_UserId �̒l�D���������ύX�s�D")]
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
#if COOKIE
		[Category("GyaO�Ƃ̒ʐM")]
		[DisplayName("Cookie_CookieId")]
		[Description("�N�b�L�[�ɕۑ�����Ă��� Cookie_CookieId �̒l�D���������ύX�s�D")]
		[ReadOnly(true)]
		public int GyaoCookieId {
			get {
				return this.gyaoCookieId;
			}
			set {
				this.gyaoCookieId = value;
			}
		}
		[Category("GyaO�Ƃ̒ʐM")]
		[DisplayName("GYAOSID")]
		[Description("�N�b�L�[�ɕۑ�����Ă��� GYAOSID �̒l�D���������ύX�s�D")]
		[ReadOnly(true)]
		public string GyaoSessionId {
			get {
				return this.gyaoSessionIdHigh.ToString("X").PadLeft(16, '0')
					+ this.gyaoSessionIdLow.ToString("X").PadLeft(16, '0');
			}
			set {
				if(32 != value.Length) throw new ArgumentException();
				this.GyaoSessionIdHigh = long.Parse(value.Substring(0, 16), NumberStyles.HexNumber);
				this.GyaoSessionIdLow = long.Parse(value.Substring(16, 16), NumberStyles.HexNumber);
			}
		}
		[Category("GyaO�Ƃ̒ʐM")]
		[Browsable(false)]
		public long GyaoSessionIdHigh {
			get {
				return this.gyaoSessionIdHigh;
			}
			set {
				this.gyaoSessionIdHigh = value;
			}
		}
		[Category("GyaO�Ƃ̒ʐM")]
		[Browsable(false)]
		public long GyaoSessionIdLow {
			get {
				return this.gyaoSessionIdLow;
			}
			set {
				this.gyaoSessionIdLow = value;
			}
		}
#endif
		[Category("GyaO�Ƃ̒ʐM")]
		[DisplayName("�N�b�L�[�̎擾����")]
		[Description("�N�b�L�[���擾���������D���������ύX�s�D")]
		[ReadOnly(true)]
		public DateTime GyaoCookieTime {
			get {
				return this.gyaoCookieTime;
			}
			set {
				this.gyaoCookieTime = value;
			}
		}
		[Category("GyaO�Ƃ̒ʐM")]
		[DisplayName("�r�b�g���[�g")]
		[Description("�Đ����铮��̃r�b�g���[�g�D��p�v���[�������łȂ�WMP�ł̍Đ������̐ݒ�̉e�����󂯂܂��D")]
		[DefaultValue(GBitRate.SuperFine)]
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
		[Description("GyaO�̃E�F�u�y�[�W�̎擾����񉻂��č�������}��D���܂����삵�Ȃ����ł͖����ɂ��邱�ƁD")]
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
		[DisplayName("�u���E�U�t�H�[��")]
		public UscForm BrowserForm {
			get { return this.browserForm; }
			set { this.browserForm = value; }
		}
		private UscForm browserForm = new UscForm(new Size(850, 600), new Point(0, 0));

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
		private UscForm userCommandsEditor = new UscForm(new Size(360, 340), new Point(350, 50));
		
		[Category("�e�E�B���h�E���Ƃ̐ݒ�")]
		[DisplayName("NG�p�b�P�[�W�G�f�B�^")]
		public UscForm NgPackagesEditor {
			get { return this.ngPackagesEditor; }
			set { this.ngPackagesEditor = value; }
		}
		private UscForm ngPackagesEditor = new UscForm(new Size(600, 250), new Point(200, 50));
	}
}
