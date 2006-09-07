﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;
using Yusen.GCrawler;
using System.Windows.Forms.Design;
using System.Drawing.Design;

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
		
		private string nameFamily = string.Empty;
		[Category("ユーザ情報")]
		[DisplayName("氏名(姓)")]
		[Description("応募フォームのフィルに使います．注意: この設定は設定ファイルに保存されます．")]
		[DefaultValue("")]
		public string NameFamily {
			get { return this.nameFamily; }
			set { this.nameFamily = value; }
		}
		private string nameFirst = string.Empty;
		[Category("ユーザ情報")]
		[DisplayName("氏名(名)")]
		[Description("応募フォームのフィルに使います．注意: この設定は設定ファイルに保存されます．")]
		[DefaultValue("")]
		public string NameFirst {
			get { return this.nameFirst; }
			set { this.nameFirst = value; }
		}
		private string emailAddress = string.Empty;
		[Category("ユーザ情報")]
		[DisplayName("Eメールアドレス")]
		[Description("応募フォームのフィルに使います．注意: この設定は設定ファイルに保存されます．")]
		[DefaultValue("")]
		public string EmailAddress {
			get { return this.emailAddress; }
			set { this.emailAddress = value; }
		}
		[XmlIgnore]
		[Browsable(false)]
		public bool IsValidFormData {
			get {
				return !string.IsNullOrEmpty(this.NameFamily) && !string.IsNullOrEmpty(this.NameFirst) && !string.IsNullOrEmpty(this.EmailAddress);
			}
		}
		
		private GBitRate bitRate = GBitRate.SuperFine;
		[Category("ビットレート")]
		[DisplayName("ビットレート")]
		[Description("再生する動画のビットレート．専用プレーヤだけでなくWMPでの再生もこの設定の影響を受ける．")]
		[DefaultValue(GBitRate.SuperFine)]
		public GBitRate BitRate {
			get {return this.bitRate;}
			set {this.bitRate = value;}
		}
		private bool promptBitrateOnStartup = true;
		[Category("ビットレート")]
		[DisplayName("起動時にビットレートを確認")]
		[Description("起動時にビットレートを確認します．")]
		[DefaultValue(true)]
		public bool PromptBitrateOnStartup {
			get { return this.promptBitrateOnStartup; }
			set { this.promptBitrateOnStartup = value; }
		}
		
		
		private CrawlOrder crawlOrder = CrawlOrder.TimetableFirst;
		[Category("クローラ")]
		[DisplayName("クロールの順序")]
		[Description("トップページと番組表のどちらを先にクロールするか指定します．先にクロールしたページでのパッケージの出現順序がクロール結果に強く影響されます．")]
		[DefaultValue(CrawlOrder.TimetableFirst)]
		public CrawlOrder CrawlOrder {
			get { return this.crawlOrder; }
			set { this.crawlOrder = value; }
		}
		private TimetableSortType timetableSortType = TimetableSortType.RecentlyUpdatedFirst;
		[XmlIgnore]
		[ReadOnly(true)]
		[Category("クローラ")]
		[DisplayName("番組表のソート対象")]
		[Description("クロール時に利用する番組表を更新日優先にするか残り日数優先にするかを指定します．「クロールの順序」で番組表を先に読む設定にしておかないとほとんど意味がありません．")]
		[DefaultValue(TimetableSortType.RecentlyUpdatedFirst)]
		public TimetableSortType TimetableSortType {
			get { return this.timetableSortType; }
			//set { this.timetableSortType = value; }
		}
		private int maxCrawlPages = 32;
		[Category("クローラ")]
		[DisplayName("一般ページの最大数")]
		[Description("クロールする一般ページの最大数を指定します．指定された値以上になったらクロールを打ち切ります．値を小さくしてもクロール結果の精度には大きな悪影響を及ぼさないので，値を小さくして動作速度とサーバへの負荷を改善するのも一興です．")]
		[DefaultValue(32)]
		public int MaxCrawlPages {
			get { return this.maxCrawlPages; }
			set { this.maxCrawlPages = value; }
		}
		private bool ignoreAddMyListFunction = true;
		[Category("クローラ")]
		[DisplayName("addMyList関数を無視する")]
		[Description("addMyList関数は放送予定コンテンツでも使われるため，無視することでダミーのコンテンツが減るようになります．")]
		[DefaultValue(true)]
		public bool IgnoreAddMyListFunction {
			get { return this.ignoreAddMyListFunction; }
			set { this.ignoreAddMyListFunction = value; }
		}

		[Category("GUI")]
		[DisplayName("ビジュアルスタイル")]
		[Description("ビジュアルスタイルの適用領域の指定．切り替えに時間がかかる場合があります．")]
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
		[DisplayName("アイコンのファイル名")]
		[Description("アイコンのファイル名を指定．無指定の場合は実行ファイル名と同名のアイコンファイルが指定されているものとする．再起動後に有効．")]
		[DefaultValue("")]
		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string IconFile {
			get { return this.iconFile; }
			set { this.iconFile = value; }
		}
		private bool listViewDoubleBufferEnabled = true;
		[Category("GUI")]
		[DisplayName("リストビューのダブルバッファ")]
		[Description("リストビューのダブルバッファを有効にして表示のちらつきを抑えます．リストビューの再生成後に有効．")]
		[DefaultValue(true)]
		public bool ListViewDoubleBufferEnabled {
			get { return this.listViewDoubleBufferEnabled; }
			set { this.listViewDoubleBufferEnabled = value; }
		}
		
		private string migemoDictionaryFilename = @"dict\migemo-dict";
		[Category("migemo")]
		[DisplayName("辞書ファイルのディレクトリ")]
		[Description("migemoの辞書ファイル．再起動後に有効．")]
		[DefaultValue(@"dict\migemo-dict")]
		[Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
		public string MigemoDictionaryFilename {
			get { return this.migemoDictionaryFilename; }
			set { this.migemoDictionaryFilename = value; }
		}
		
		private bool useGBrowser = true;
		[Category("ウェブブラウザ")]
		[DisplayName("内蔵ブラウザを使う")]
		[Description("GExplorer内蔵のブラウザを使用する場合には True にする．ただしカタログ表示の時には設定によらず内臓ウェブブラウザが使われる．")]
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
		
		private bool automaticallyAddFavAndNewContents = true;
		[Category("FAVコンテンツ")]
		[DisplayName("新着FAVコンテンツの自動追加")]
		[Description("新着であり尚且つFAVコンテンツの条件を満たすコンテンツをクロール終了時に自動的にプレイリストに追加します．")]
		[DefaultValue(true)]
		public bool AutomaticallyAddFavAndNewContents {
			get { return this.automaticallyAddFavAndNewContents; }
			set { this.automaticallyAddFavAndNewContents = value; }
		}
		
		internal CrawlSettings CreateCrawlSettings() {
			return new CrawlSettings(this.maxCrawlPages, this.crawlOrder, this.timetableSortType, this.ignoreAddMyListFunction);
		}
	}
}
