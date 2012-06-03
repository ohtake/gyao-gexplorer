using System;
using System.Collections.Generic;
using System.ComponentModel;
using GBitRate = Yusen.GCrawler.GBitRate;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;
using System.IO;
using System.Xml.Serialization;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	public class GlobalSettings {
		private const string SettingsFilename = "GlobalSettings.xml";
		private const int InvalidUserNo = 0;
		
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
		
		private int userNo = GlobalSettings.InvalidUserNo;
		[XmlIgnore]
		[Category("GyaO")]
		[DisplayName("Cookie_UserId")]
		[Description("�N�b�L�[�ɕۑ�����Ă��� Cookie_UserId �̒l�D���������ύX�s�D")]
		[DefaultValue(GlobalSettings.InvalidUserNo)]
		[ReadOnly(true)]
		public int UserNo {
			get { return this.userNo; }
			private set { this.userNo = value; }
		}
		
		private GBitRate bitRate = GBitRate.SuperFine;
		[Category("GyaO")]
		[DisplayName("�r�b�g���[�g")]
		[Description("�Đ����铮��̃r�b�g���[�g�D��p�v���[�������łȂ�WMP�ł̍Đ������̐ݒ�̉e�����󂯂�D")]
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
				return GlobalSettings.InvalidUserNo == this.UserNo;
			}
		}
		
		private CrawlOrder crawlOrder = CrawlOrder.TimetableFirst;
		[Category("�N���[��")]
		[DisplayName("�N���[���̏���")]
		[Description("�g�b�v�y�[�W�Ɣԑg�\�̂ǂ�����ɃN���[�����邩�w�肵�܂��D��ɃN���[�������y�[�W�ł̃p�b�P�[�W�̏o���������N���[�����ʂɋ����e������܂��D")]
		[DefaultValue(CrawlOrder.TimetableFirst)]
		public CrawlOrder CrawlOrder {
			get { return this.crawlOrder; }
			set { this.crawlOrder = value; }
		}
		private TimetableSortType timetableSortType = TimetableSortType.RecentlyUpdatedFirst;
		[XmlIgnore]
		[Category("�N���[��")]
		[DisplayName("�ԑg�\�̃\�[�g�Ώ�")]
		[Description("�N���[�����ɗ��p����ԑg�\���X�V���D��ɂ��邩�c������D��ɂ��邩���w�肵�܂��D�u�N���[���̏����v�Ŕԑg�\���ɓǂސݒ�ɂ��Ă����Ȃ��ƂقƂ�ǈӖ�������܂���D")]
		[DefaultValue(TimetableSortType.RecentlyUpdatedFirst)]
		public TimetableSortType TimetableSortType {
			get { return this.timetableSortType; }
			//set { this.timetableSortType = value; }
		}
		private int maxCrawlPages = 256;
		[Category("�N���[��")]
		[DisplayName("��ʃy�[�W�̍ő吔")]
		[Description("�N���[�������ʃy�[�W�̍ő吔���w�肵�܂��D�w�肳�ꂽ�l�ȏ�ɂȂ�����N���[����ł��؂�܂��D�l�����������Ă��N���[�����ʂ̐��x�ɂ͑傫�Ȉ��e�����y�ڂ��Ȃ��̂ŁC�l�����������ē��쑬�x�ƃT�[�o�ւ̕��ׂ����P����̂��ꋻ�ł��D")]
		[DefaultValue(256)]
		public int MaxCrawlPages {
			get { return this.maxCrawlPages; }
			set { this.maxCrawlPages = value; }
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
		private string iconFile = "";
		[Category("GUI")]
		[DisplayName("�A�C�R���̃t�@�C����")]
		[Description("�A�C�R���̃t�@�C�������w��D���w��̏ꍇ�͎��s�t�@�C�����Ɠ����̃A�C�R���t�@�C�����w�肳��Ă�����̂Ƃ���D�ċN����ɗL���D")]
		[DefaultValue("")]
		public string IconFile {
			get { return this.iconFile; }
			set { this.iconFile = value; }
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

		internal bool TryGetUserNumber() {
			this.UserNo = GlobalSettings.InvalidUserNo;
			//���W�X�g������̎擾�����݂�
			try {
				using (RegistryKey cu = Registry.CurrentUser)
				using (RegistryKey software = cu.OpenSubKey("SOFTWARE"))
				using (RegistryKey usen = software.OpenSubKey("USEN"))
				using (RegistryKey gyaoTool = usen.OpenSubKey("GyaOTool")) {
					GlobalSettings.Instance.UserNo = int.Parse(gyaoTool.GetValue("Cookie_UserId") as string);
				}
			} catch {
			}
			if (!this.IsCookieRequired) {
				return true;
			}
			//�t�@�C���V�X�e����̃N�b�L�[����擾�����݂�
			try {
				DirectoryInfo cookieDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
				if (cookieDir.Exists) {
					FileInfo[] fis = cookieDir.GetFiles("*@gyao*.txt");
					foreach (FileInfo fi in fis) {
						using (TextReader reader = new StreamReader(fi.FullName)) {
							string line;
							while (null != (line=reader.ReadLine())) {
								if ("Cookie_UserId" == line) {
									GlobalSettings.Instance.UserNo = int.Parse(reader.ReadLine());
									break;
								}
							}
							if (!GlobalSettings.Instance.IsCookieRequired) {
								break;
							}
						}
					}
				}
			} catch {
			}
			if (!this.IsCookieRequired) {
				return true;
			}
			//�u���E�U�ォ��N�b�L�[���擾����
			using (CookieFetchForm cff =  new CookieFetchForm()) {
				switch (cff.ShowDialog()) {
					case DialogResult.OK:
						if (cff.UserNo.HasValue) {
							this.UserNo = cff.UserNo.Value;
						}
						break;
				}
			}
			if (!this.IsCookieRequired) {
				return true;
			}
			return false;
		}

		internal CrawlSettings GetCrawlSettings() {
			return new CrawlSettings(this.maxCrawlPages, this.crawlOrder, this.timetableSortType);
		}
	}
}
