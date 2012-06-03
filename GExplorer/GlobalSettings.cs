using System;
using System.Collections.Generic;
using System.ComponentModel;
using GBitRate = Yusen.GCrawler.GBitRate;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Yusen.GExplorer {
	public class GlobalSettings {
		/// <summary>�N�b�L�[�̎���</summary>
		private const double CookieExpiresInDays = 7;
		private const string SettingsFilename = "GlobalSettings.xml";

		private static GlobalSettings instance = new GlobalSettings();
		internal static GlobalSettings Instance {
			get { return GlobalSettings.instance; }
		}
		internal static bool TryDeserialize() {
			GlobalSettings settings;
			if (Utility.TryDeserializeSettings(GlobalSettings.SettingsFilename, out settings)) {
				GlobalSettings.instance = settings;
				return true;
			}
			return false;
		}
		internal static void Serialize() {
			Utility.SerializeSettings(GlobalSettings.SettingsFilename, GlobalSettings.Instance);
		}
		
		private int userNo = 0;
		[Category("GyaO")]
		[DisplayName("Cookie_UserId")]
		[Description("�N�b�L�[�ɕۑ�����Ă��� Cookie_UserId �̒l�D���������ύX�s�D")]
		[DefaultValue(0)]
		[ReadOnly(true)]
		public int UserNo {
			get { return this.userNo; }
			set { this.userNo = value; }
		}
		
		private DateTime cookieTime = DateTime.MinValue;
		[Category("GyaO")]
		[DisplayName("�N�b�L�[�̎擾����")]
		[Description("�N�b�L�[���擾���������D���������ύX�s�D")]
		[ReadOnly(true)]
		public DateTime CookieTime {
			get { return this.cookieTime; }
			set { this.cookieTime = value; }
		}

		private GBitRate bitRate = GBitRate.SuperFine;
		[Category("GyaO")]
		[DisplayName("�r�b�g���[�g")]
		[Description("�Đ����铮��̃r�b�g���[�g�D��p�v���[�������łȂ�WMP�ł̍Đ������̐ݒ�̉e�����󂯂܂��D")]
		[DefaultValue(GBitRate.SuperFine)]
		public GBitRate BitRate {
			get {return this.bitRate;}
			set {this.bitRate = value;}
		}

		/// <summary>
		/// �ݒ�t�@�C���� userNo ���ۑ�����ĂȂ�������N�b�L�[���Â�������Ď擾�̕K�v������
		/// true ���Ԃ�D
		/// </summary>
		[Browsable(false)]
		internal bool IsCookieRequired {
			get {
				return
					0 == this.UserNo
					|| this.cookieTime.AddDays(GlobalSettings.CookieExpiresInDays) < DateTime.Now;
			}
		}
		
		[Category("GUI")]
		[DisplayName("�r�W���A���X�^�C��")]
		[Description("�r�W���A���X�^�C���̓K�p�̈�̎w��D")]
		[DefaultValue(VisualStyleState.ClientAndNonClientAreasEnabled)]
		public VisualStyleState VisualStyle {
			get {return Application.VisualStyleState;}
			set {
				if (value != Application.VisualStyleState) {
					Application.VisualStyleState = value;
				}
			}
		}
		
		private string migemoDictionaryFilename = @"dict\migemo-dict";
		[Category("migemo")]
		[DisplayName("�����t�@�C��")]
		[Description("migemo�̎����t�@�C���D�ċN����ɗL���D")]
		[DefaultValue(@"dict\migemo-dict")]
		public string MigemoDictionaryFilename {
			get { return this.migemoDictionaryFilename; }
			set { this.migemoDictionaryFilename = value; }
		}
	}
}
