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
		[Description("クッキーに保存されている Cookie_UserId の値．いちおう変更不可．")]
		[DefaultValue(0)]
		[ReadOnly(true)]
		public int UserNo {
			get { return this.userNo; }
			set { this.userNo = value; }
		}
		
		private GBitRate bitRate = GBitRate.SuperFine;
		[Category("GyaO")]
		[DisplayName("ビットレート")]
		[Description("再生する動画のビットレート．専用プレーヤだけでなくWMPでの再生もこの設定の影響を受けます．")]
		[DefaultValue(GBitRate.SuperFine)]
		public GBitRate BitRate {
			get {return this.bitRate;}
			set {this.bitRate = value;}
		}

		/// <summary>
		/// 設定ファイルに userNo が保存されてなかったら取得の必要があり true が返る．
		/// </summary>
		[Browsable(false)]
		internal bool IsCookieRequired {
			get {
				return 0 == this.UserNo;
			}
		}
		
		[Category("GUI")]
		[DisplayName("ビジュアルスタイル")]
		[Description("ビジュアルスタイルの適用領域の指定．")]
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
		[DisplayName("辞書ファイル")]
		[Description("migemoの辞書ファイル．再起動後に有効．")]
		[DefaultValue(@"dict\migemo-dict")]
		public string MigemoDictionaryFilename {
			get { return this.migemoDictionaryFilename; }
			set { this.migemoDictionaryFilename = value; }
		}
		
		private bool useGBrowser = true;
		[Category("ウェブブラウザ")]
		[DisplayName("内蔵ブラウザを使う")]
		[Description("GExplorer内蔵のブラウザを使用する場合には True にする．")]
		[DefaultValue(true)]
		public bool UseGBrowser {
			get { return this.useGBrowser; }
			set { this.useGBrowser = value; }
		}
		
		private string browserPath = "";
		[Category("ウェブブラウザ")]
		[DisplayName("パス")]
		[Description("内蔵のブラウザを用いない場合に使用するウェブブラウザのパスを指定する．未指定の場合はデフォルトのウェブブラウザを用いる．")]
		[DefaultValue("")]
		public string BrowserPath {
			get { return this.browserPath; }
			set { this.browserPath = value; }
		}

		private bool playlistLoop = true;
		[Category("プレイリスト")]
		[DisplayName("ループ")]
		[Description("プレイリストの末尾の項目を再生し終わったら先頭に移動する．")]
		[DefaultValue(true)]
		public bool PlaylistLoop {
			get { return this.playlistLoop; }
			set { this.playlistLoop = value; }
		}
	}
}
