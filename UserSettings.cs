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
		/// 設定ファイルに userNo が保存されていたら再取得の必要なしなので false が返る．
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
		[Category("GyaOとの通信")]
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
		[Category("GyaOとの通信")]
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
		[Description("GyaOのウェブページの取得を並列化して高速化を図る．環境によっては逆に遅くなる場合もあるかも．")]
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
		private UscForm userCommandsEditor = new UscForm(new Size(360, 340), new Point(200, 50));
	}
}
