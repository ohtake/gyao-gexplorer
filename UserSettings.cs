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
	
	/// <summary>ビットレート</summary>
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
		
		// シングルトンだけどXMLデシリアライズに必要だからしゃあない
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
		/// 設定ファイルに userNo が保存されていたら再取得の必要なしなので false が返る．
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
		[Description("クッキーに保存されている userNo の値．いちおう変更不可．")]
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
		[DisplayName("ビットレート")]
		[Description("再生する動画のビットレート．専用プレーヤだけでなくWMPでの再生もこの設定の影響を受けます．")]
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
		[Description("GyaOに送信するビットレートのIDの値．")]
		[DefaultValue("bit0000002")]
		public string GyaoBitRateId {
			get {
				return "bit" + ((int)this.gyaoBitRate).ToString("0000000");
			}
		}
		#endregion GyaO
		
		#region ユーザ設定ツールボックス
		private FormStartPosition ustStartPosition = FormStartPosition.Manual;
		private Size ustSize = new Size(240, 400);
		private Point ustLocation = new Point(150, 150);
		private bool ustTopMost = false;

		[Category("ユーザ設定ツールボックス")]
		[DisplayName("初期配置")]
		[Description("起動したときに表示される位置の決定方法．")]
		[DefaultValue(FormStartPosition.Manual)]
		public FormStartPosition UstStartPosition {
			get {
				return this.ustStartPosition;
			}
			set {
				this.ustStartPosition = value;
			}
		}
		[Category("ユーザ設定ツールボックス")]
		[DisplayName("サイズ")]
		[Description("ウィンドウのサイズ．")]
		public Size UstSize {
			get {
				return this.ustSize;
			}
			set {
				this.ustSize = value;
			}
		}
		[Category("ユーザ設定ツールボックス")]
		[DisplayName("位置")]
		[Description("位置．初期配置をManualにする必要あり．")]
		public Point UstLocation {
			get {
				return this.ustLocation;
			}
			set {
				this.ustLocation = value;
			}
		}
		[Category("ユーザ設定ツールボックス")]
		[DisplayName("常に手前に表示")]
		[Description("常に手前に表示します．")]
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
		
		#region プレーヤ
		private bool playerAutoVolume = true;
		private bool playerAlwaysOnTop = false;
		private Size playerSize = new Size(670, 640);

		[Category("専用プレーヤの初期設定")]
		[DisplayName("自動音量調整")]
		[Description("CMと思わしき動画の音量を10にし，そうではないのを100にします．")]
		[DefaultValue(true)]
		public bool PlayerAutoVolume {
			get {
				return this.playerAutoVolume;
			}
			set {
				this.playerAutoVolume = value;
			}
		}
		[Category("専用プレーヤの初期設定")]
		[DisplayName("常に手前に表示")]
		[Description("最前面で表示するようにします．")]
		[DefaultValue(false)]
		public bool PlayerAlwaysOnTop {
			get {
				return this.playerAlwaysOnTop;
			}
			set {
				this.playerAlwaysOnTop = value;
			}
		}
		[Category("専用プレーヤの初期設定")]
		[DisplayName("サイズ")]
		[Description("ウィンドウのサイズ．")]
		public Size PlayerSize {
			get {
				return this.playerSize;
			}
			set {
				this.playerSize = value;
			}
		}
		#endregion
		
		#region メインフォーム
		private FormStartPosition mfStartPosition = FormStartPosition.Manual;
		private FormWindowState mfWindowState = FormWindowState.Normal;
		private Size mfSize = new Size(600, 450);
		private Point mfLocation = new Point(100, 100);
		[Category("メインフォーム")]
		[DisplayName("初期配置")]
		[Description("起動したときに表示される位置の決定方法．")]
		[DefaultValue(FormStartPosition.Manual)]
		public FormStartPosition MfStartPosition {
			get {
				return this.mfStartPosition;
			}
			set {
				this.mfStartPosition = value;
			}
		}
		[Category("メインフォーム")]
		[DisplayName("状態")]
		[Description("最小化，通常，最大化．")]
		[DefaultValue(FormWindowState.Normal)]
		public FormWindowState MfWindowState {
			get {
				return this.mfWindowState;
			}
			set {
				this.mfWindowState = value;
			}
		}
		[Category("メインフォーム")]
		[DisplayName("サイズ")]
		[Description("ウィンドウのサイズ．")]
		public Size MfSize {
			get {
				return this.mfSize;
			}
			set {
				this.mfSize = value;
			}
		}
		[Category("メインフォーム")]
		[DisplayName("位置")]
		[Description("位置．初期配置をManualにする必要あり．")]
		public Point MfLocation {
			get {
				return this.mfLocation;
			}
			set {
				this.mfLocation = value;
			}
		}
		#endregion
		
		#region リストビュー
		private bool lvMultiSelect = false;
		private View lvView = View.Details;
		private int lvColWidthId = 90;
		private int lvColWidthLimit = 80;
		private int lvColWidthEpisode = 70;
		private int lvColWidthLead = 320;

		[Category("リストビュー")]
		[DisplayName("複数選択")]
		[Description("複数選択を有効にします．誤操作防止のため False 推奨．")]
		[DefaultValue(false)]
		public bool LvMultiSelect {
			get {
				return this.lvMultiSelect;
			}
			set {
				this.lvMultiSelect = value;
			}
		}
		[Category("リストビュー")]
		[DisplayName("表示形式")]
		[Description("リストビューの表示形式を変更します．")]
		[DefaultValue(View.Details)]
		public View LvView {
			get {
				return this.lvView;
			}
			set {
				this.lvView = value;
			}
		}
		[Category("リストビュー")]
		[DisplayName("カラム幅 (1. contents_id)")]
		[Description("リストビューのカラムの幅．(1. contents_id)")]
		[DefaultValue(90)]
		public int LvColWidthId {
			get {
				return this.lvColWidthId;
			}
			set {
				this.lvColWidthId = value;
			}
		}
		[Category("リストビュー")]
		[DisplayName("カラム幅 (2. 配信終了日)")]
		[Description("リストビューのカラムの幅．(2. 配信終了日)")]
		[DefaultValue(80)]
		public int LvColWidthLimit {
			get {
				return this.lvColWidthLimit;
			}
			set {
				this.lvColWidthLimit = value;
			}
		}
		[Category("リストビュー")]
		[DisplayName("カラム幅 (3. 話)")]
		[Description("リストビューのカラムの幅．(3. 話)")]
		[DefaultValue(70)]
		public int LvColWidthEpisode {
			get {
				return this.lvColWidthEpisode;
			}
			set {
				this.lvColWidthEpisode = value;
			}
		}
		[Category("リストビュー")]
		[DisplayName("カラム幅 (4. リード)")]
		[Description("リストビューのカラムの幅．(4. リード)")]
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
		
		#region コンテントプロパティビューア
		private Size gcpvSize = new Size(260, 310);
		[Category("コンテントプロパティビューアの初期設定")]
		[DisplayName("サイズ")]
		[Description("ウィンドウのサイズ．")]
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
	// ユーザ設定の項目が増えて管理に手がおえられなくなったら
	// グループごとにクラス化して ExpandableObjectConverter を使う
	
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
