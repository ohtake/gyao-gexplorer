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
	/// ユーザ設定が変わった場合に呼び出される．
	/// <see cref="UserSettings"/>またはその直下ノードの<see cref="UserSettingsChild"/>から発生．
	/// </summary>
	delegate void UserSettingsChangeCompletedEventHandler();
	
	/// <summary>ビットレート</summary>
	public enum GBitRate {
		SuperFine = 2,
		Standard = 1,
	}
	/// <summary>あぼーんの方法．各メンバ名の由来はJaneViewより．</summary>
	public enum AboneType {
		/// <summary>とうめい</summary>
		Toumei,
		/// <summary>さぼり．現状ではNG対象であっても別色で表示したりはしない．</summary>
		Sabori,
		/// <summary>はきだめ</summary>
		Hakidame,
	}
	
	/// <summary>ユーザ設定のシングルトン．</summary>
	public class UserSettings {
		private static UserSettings instance = new UserSettings();
		private const string filename = "UserSettings.xml";
		/// <summary>クッキーの寿命</summary>
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
		
		// シングルトンだけどXMLデシリアライズに必要だからしゃあない
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
		/// 設定ファイルに userNo が保存されてなかったりクッキーが古すぎたら再取得の必要があり
		/// true が返る．
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
		[Category("GyaOとの通信")]
		[DisplayName("Cookie_UserId")]
		[Description("クッキーに保存されている Cookie_UserId の値．いちおう変更不可．")]
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
		[Category("GyaOとの通信")]
		[DisplayName("Cookie_CookieId")]
		[Description("クッキーに保存されている Cookie_CookieId の値．いちおう変更不可．")]
		[ReadOnly(true)]
		public int GyaoCookieId {
			get {
				return this.gyaoCookieId;
			}
			set {
				this.gyaoCookieId = value;
			}
		}
		[Category("GyaOとの通信")]
		[DisplayName("GYAOSID")]
		[Description("クッキーに保存されている GYAOSID の値．いちおう変更不可．")]
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
		[Category("GyaOとの通信")]
		[Browsable(false)]
		public long GyaoSessionIdHigh {
			get {
				return this.gyaoSessionIdHigh;
			}
			set {
				this.gyaoSessionIdHigh = value;
			}
		}
		[Category("GyaOとの通信")]
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
		[Category("GyaOとの通信")]
		[DisplayName("クッキーの取得日時")]
		[Description("クッキーを取得した日時．いちおう変更不可．")]
		[ReadOnly(true)]
		public DateTime GyaoCookieTime {
			get {
				return this.gyaoCookieTime;
			}
			set {
				this.gyaoCookieTime = value;
			}
		}
		[Category("GyaOとの通信")]
		[DisplayName("ビットレート")]
		[Description("再生する動画のビットレート．専用プレーヤだけでなくWMPでの再生もこの設定の影響を受けます．")]
		[DefaultValue(GBitRate.SuperFine)]
		public GBitRate GyaoBitRate {
			get {
				return this.gyaoBitRate;
			}
			set {
				this.gyaoBitRate = value;
			}
		}
		[Category("GyaOとの通信")]
		[DisplayName("rateId")]
		[Description("GyaOに送信するビットレートのIDの値．")]
		[DefaultValue("bit0000002")]
		public string GyaoBitRateId {
			get {
				return "bit" + ((int)this.gyaoBitRate).ToString("0000000");
			}
		}
		[Category("GyaOとの通信")]
		[DisplayName("並列読み込み")]
		[Description("GyaOのウェブページの取得を並列化して高速化を図る．うまく動作しない環境では無効にすること．")]
		[DefaultValue(true)]
		public bool GyaoEnableConcurrentFetch {
			get {
				return this.gyaoEnableConcurrentFetch;
			}
			set {
				this.gyaoEnableConcurrentFetch = value;
			}
		}

		[Category("各ウィンドウごとの設定")]
		[DisplayName("メインフォーム")]
		public UscMainForm MainForm {
			get { return this.mainForm; }
			set { this.mainForm = value; }
		}
		private UscMainForm mainForm = new UscMainForm(new Size(600, 450), new Point(100, 100));

		[Category("各ウィンドウごとの設定")]
		[DisplayName("プレーヤ")]
		public UscPlayerForm PlayerForm {
			get { return this.playerForm; }
			set { this.playerForm = value; }
		}
		private UscPlayerForm playerForm = new UscPlayerForm(new Size(670, 640), new Point(0, 0));

		[Category("各ウィンドウごとの設定")]
		[DisplayName("ブラウザフォーム")]
		public UscForm BrowserForm {
			get { return this.browserForm; }
			set { this.browserForm = value; }
		}
		private UscForm browserForm = new UscForm(new Size(850, 600), new Point(0, 0));

		[Category("各ウィンドウごとの設定")]
		[DisplayName("ユーザ設定ツールボックス")]
		public UscFormTopmostable UserSettingsToolbox {
			get {return this.userSettingsToolbox;}
			set {this.userSettingsToolbox = value;}
		}
		private UscFormTopmostable userSettingsToolbox = new UscFormTopmostable(new Size(240, 400), new Point(50, 150));

		[Category("各ウィンドウごとの設定")]
		[DisplayName("コンテンツプロパティビューア")]
		public UscFormTopmostable ContentPropertyViewer {
			get { return this.contentPropertyViewer; }
			set { this.contentPropertyViewer = value; }
		}
		private UscFormTopmostable contentPropertyViewer = new UscFormTopmostable(new Size(260, 310), new Point(500, 50));

		[Category("各ウィンドウごとの設定")]
		[DisplayName("外部コマンドエディタ")]
		public UscForm UserCommandsEditor {
			get { return this.userCommandsEditor; }
			set { this.userCommandsEditor = value; }
		}
		private UscForm userCommandsEditor = new UscForm(new Size(360, 340), new Point(350, 50));
		
		[Category("各ウィンドウごとの設定")]
		[DisplayName("NGパッケージエディタ")]
		public UscForm NgPackagesEditor {
			get { return this.ngPackagesEditor; }
			set { this.ngPackagesEditor = value; }
		}
		private UscForm ngPackagesEditor = new UscForm(new Size(600, 250), new Point(200, 50));
	}
}
