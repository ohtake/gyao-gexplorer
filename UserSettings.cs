using System;
using System.ComponentModel;
using System.IO;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;
using CultureInfo = System.Globalization.CultureInfo;

using System.Windows.Forms;
using System.Drawing;

namespace Yusen.GExplorer {
	interface IUsesUserSettings {
		void LoadFromUserSettings();
		void SaveToUserSettings();
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
		
		internal event UserSettingsChangeCompletedEventHandler ChangeCompleted;
		public void OnChangeCompleted() {
			if(null != this.ChangeCompleted) {
				this.ChangeCompleted();
			}
		}
		private UserSettingsToolbox toolbox = null;
		public void ShowUserSettingsForm() {
			if(null == this.toolbox || !this.toolbox.CanFocus) {
				this.toolbox = new UserSettingsToolbox();
				this.toolbox.Show();
			} else {
				this.toolbox.Focus();
			}
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
		
		#region GyaO
		private int gyaoUserId = 0;
		private GBitRate gyaoBitRate = GBitRate.High;
		[Category("GyaO")]
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
		[Category("GyaO")]
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

		[Browsable(false)]
		[Category("GyaO")]
		[DisplayName("rateId")]
		[Description("GyaO�ɑ��M����r�b�g���[�g��ID�̒l�D")]
		[DefaultValue("bit0000002")]
		public string GyaoBitRateId {
			get {
				return "bit" + ((int)this.gyaoBitRate).ToString("0000000");
			}
		}
		#endregion GyaO
		
		#region ���[�U�ݒ�c�[���{�b�N�X
		private FormStartPosition ustStartPosition = FormStartPosition.Manual;
		private Size ustSize = new Size(240, 400);
		private Point ustLocation = new Point(150, 150);
		private bool ustTopMost = false;

		[Category("���[�U�ݒ�c�[���{�b�N�X")]
		[DisplayName("�����z�u")]
		[Description("�N�������Ƃ��ɕ\�������ʒu�̌�����@�D")]
		[DefaultValue(FormStartPosition.Manual)]
		public FormStartPosition UstStartPosition {
			get {
				return this.ustStartPosition;
			}
			set {
				this.ustStartPosition = value;
			}
		}
		[Category("���[�U�ݒ�c�[���{�b�N�X")]
		[DisplayName("�T�C�Y")]
		[Description("�E�B���h�E�̃T�C�Y�D")]
		public Size UstSize {
			get {
				return this.ustSize;
			}
			set {
				this.ustSize = value;
			}
		}
		[Category("���[�U�ݒ�c�[���{�b�N�X")]
		[DisplayName("�ʒu")]
		[Description("�ʒu�D�����z�u��Manual�ɂ���K�v����D")]
		public Point UstLocation {
			get {
				return this.ustLocation;
			}
			set {
				this.ustLocation = value;
			}
		}
		[Category("���[�U�ݒ�c�[���{�b�N�X")]
		[DisplayName("��Ɏ�O�ɕ\��")]
		[Description("��Ɏ�O�ɕ\�����܂��D")]
		[DefaultValue(false)]
		public bool UstTopMost {
			get {
				return this.ustTopMost;
			}
			set {
				this.ustTopMost = value;
			}
		}
		#endregion
		
		#region �v���[��
		private bool playerAutoVolume = true;
		private bool playerAlwaysOnTop = false;
		private Size playerSize = new Size(670, 640);

		[Category("��p�v���[���̏����ݒ�")]
		[DisplayName("�������ʒ���")]
		[Description("CM�Ǝv�킵������̉��ʂ�10�ɂ��C�����ł͂Ȃ��̂�100�ɂ��܂��D")]
		[DefaultValue(true)]
		public bool PlayerAutoVolume {
			get {
				return this.playerAutoVolume;
			}
			set {
				this.playerAutoVolume = value;
			}
		}
		[Category("��p�v���[���̏����ݒ�")]
		[DisplayName("��Ɏ�O�ɕ\��")]
		[Description("�őO�ʂŕ\������悤�ɂ��܂��D")]
		[DefaultValue(false)]
		public bool PlayerAlwaysOnTop {
			get {
				return this.playerAlwaysOnTop;
			}
			set {
				this.playerAlwaysOnTop = value;
			}
		}
		[Category("��p�v���[���̏����ݒ�")]
		[DisplayName("�T�C�Y")]
		[Description("�E�B���h�E�̃T�C�Y�D")]
		public Size PlayerSize {
			get {
				return this.playerSize;
			}
			set {
				this.playerSize = value;
			}
		}
		#endregion
		
		#region ���C���t�H�[��
		private FormStartPosition mfStartPosition = FormStartPosition.Manual;
		private FormWindowState mfWindowState = FormWindowState.Normal;
		private Size mfSize = new Size(600, 450);
		private Point mfLocation = new Point(100, 100);
		[Category("���C���t�H�[��")]
		[DisplayName("�����z�u")]
		[Description("�N�������Ƃ��ɕ\�������ʒu�̌�����@�D")]
		[DefaultValue(FormStartPosition.Manual)]
		public FormStartPosition MfStartPosition {
			get {
				return this.mfStartPosition;
			}
			set {
				this.mfStartPosition = value;
			}
		}
		[Category("���C���t�H�[��")]
		[DisplayName("���")]
		[Description("�ŏ����C�ʏ�C�ő剻�D")]
		[DefaultValue(FormWindowState.Normal)]
		public FormWindowState MfWindowState {
			get {
				return this.mfWindowState;
			}
			set {
				this.mfWindowState = value;
			}
		}
		[Category("���C���t�H�[��")]
		[DisplayName("�T�C�Y")]
		[Description("�E�B���h�E�̃T�C�Y�D")]
		public Size MfSize {
			get {
				return this.mfSize;
			}
			set {
				this.mfSize = value;
			}
		}
		[Category("���C���t�H�[��")]
		[DisplayName("�ʒu")]
		[Description("�ʒu�D�����z�u��Manual�ɂ���K�v����D")]
		public Point MfLocation {
			get {
				return this.mfLocation;
			}
			set {
				this.mfLocation = value;
			}
		}
		#endregion
		
		#region ���X�g�r���[
		private bool lvMultiSelect = false;
		private View lvView = View.Details;
		private int lvColWidthId = 90;
		private int lvColWidthLimit = 80;
		private int lvColWidthEpisode = 70;
		private int lvColWidthLead = 320;

		[Category("���X�g�r���[")]
		[DisplayName("�����I��")]
		[Description("�����I����L���ɂ��܂��D�둀��h�~�̂��� False �����D")]
		[DefaultValue(false)]
		public bool LvMultiSelect {
			get {
				return this.lvMultiSelect;
			}
			set {
				this.lvMultiSelect = value;
			}
		}
		[Category("���X�g�r���[")]
		[DisplayName("�\���`��")]
		[Description("���X�g�r���[�̕\���`����ύX���܂��D")]
		[DefaultValue(View.Details)]
		public View LvView {
			get {
				return this.lvView;
			}
			set {
				this.lvView = value;
			}
		}
		[Category("���X�g�r���[")]
		[DisplayName("�J������ (1. contents_id)")]
		[Description("���X�g�r���[�̃J�����̕��D(1. contents_id)")]
		[DefaultValue(90)]
		public int LvColWidthId {
			get {
				return this.lvColWidthId;
			}
			set {
				this.lvColWidthId = value;
			}
		}
		[Category("���X�g�r���[")]
		[DisplayName("�J������ (2. �z�M�I����)")]
		[Description("���X�g�r���[�̃J�����̕��D(2. �z�M�I����)")]
		[DefaultValue(80)]
		public int LvColWidthLimit {
			get {
				return this.lvColWidthLimit;
			}
			set {
				this.lvColWidthLimit = value;
			}
		}
		[Category("���X�g�r���[")]
		[DisplayName("�J������ (3. �b)")]
		[Description("���X�g�r���[�̃J�����̕��D(3. �b)")]
		[DefaultValue(70)]
		public int LvColWidthEpisode {
			get {
				return this.lvColWidthEpisode;
			}
			set {
				this.lvColWidthEpisode = value;
			}
		}
		[Category("���X�g�r���[")]
		[DisplayName("�J������ (4. ���[�h)")]
		[Description("���X�g�r���[�̃J�����̕��D(4. ���[�h)")]
		[DefaultValue(320)]
		public int LvColWidthLead {
			get {
				return this.lvColWidthLead;
			}
			set {
				this.lvColWidthLead = value;
			}
		}
		#endregion
		
		#region �R���e���g�v���p�e�B�r���[�A
		private Size gcpvSize = new Size(260, 310);
		[Category("�R���e���g�v���p�e�B�r���[�A�̏����ݒ�")]
		[DisplayName("�T�C�Y")]
		[Description("�E�B���h�E�̃T�C�Y�D")]
		public Size GcpvSize {
			get {
				return this.gcpvSize;
			}
			set {
				this.gcpvSize = value;
			}
		}
		#endregion
	}

#if false
	// ���[�U�ݒ�̍��ڂ������ĊǗ��Ɏ肪�������Ȃ��Ȃ�����
	// �O���[�v���ƂɃN���X������ ExpandableObjectConverter ���g��
	
	interface IUserSettingsMember {
	}
	
	class UserSettingsMemberConverter : ExpandableObjectConverter {
		public override bool CanConvertTo(
				ITypeDescriptorContext context, Type destinationType) {
			if(typeof(IUserSettingsMember) == destinationType) {
				return true;
			} else {
				return base.CanConvertTo(context, destinationType);
			}
		}
		public override object ConvertTo(
				ITypeDescriptorContext context, CultureInfo culture,
				object value, Type destinationType) {
			if(typeof(System.String) == destinationType
					&& value is IUserSettingsMember) {
				return "";
			} else {
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
#endif
}
