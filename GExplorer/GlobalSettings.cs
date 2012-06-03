using System;
using System.Collections.Generic;
using System.ComponentModel;
using GBitRate = Yusen.GCrawler.GBitRate;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Yusen.GExplorer {
	public class GlobalSettings {
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
		/// �ݒ�t�@�C���� userNo ���ۑ�����ĂȂ�������擾�̕K�v������ true ���Ԃ�D
		/// </summary>
		[Browsable(false)]
		internal bool IsCookieRequired {
			get {
				return 0 == this.UserNo;
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
		
		private bool useGBrowser = true;
		[Category("�E�F�u�u���E�U")]
		[DisplayName("�����u���E�U���g��")]
		[Description("GExplorer�����̃u���E�U���g�p����ꍇ�ɂ� True �ɂ���D")]
		[DefaultValue(true)]
		public bool UseGBrowser {
			get { return this.useGBrowser; }
			set { this.useGBrowser = value; }
		}
		
		private string browserPath = "";
		[Category("�E�F�u�u���E�U")]
		[DisplayName("�p�X")]
		[Description("�����̃u���E�U��p���Ȃ��ꍇ�Ɏg�p����E�F�u�u���E�U�̃p�X���w�肷��D���w��̏ꍇ�̓f�t�H���g�̃E�F�u�u���E�U��p����D")]
		[DefaultValue("")]
		public string BrowserPath {
			get { return this.browserPath; }
			set { this.browserPath = value; }
		}

		private bool playlistLoop = true;
		[Category("�v���C���X�g")]
		[DisplayName("���[�v")]
		[Description("�v���C���X�g�̖����̍��ڂ��Đ����I�������擪�Ɉړ�����D")]
		[DefaultValue(true)]
		public bool PlaylistLoop {
			get { return this.playlistLoop; }
			set { this.playlistLoop = value; }
		}
	}
}
