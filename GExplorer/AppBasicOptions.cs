using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xml.Serialization;
using Yusen.GExplorer.GyaoModel;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	[DefaultProperty("Bitrate")]
	public sealed class AppBasicOptions {
		public AppBasicOptions() {
		}
		
		private GBitrate bitrate = GBitrate.SuperFine;
		[Category("ビットレート")]
		[DisplayName("ビットレート")]
		[Description("ビットレートを指定します．")]
		[DefaultValue(GBitrate.SuperFine)]
		public GBitrate Bitrate {
			get { return this.bitrate; }
			set { this.bitrate = value; }
		}
		private bool promptBitrateOnStartup = true;
		[Category("ビットレート")]
		[DisplayName("起動時に確認")]
		[Description("起動時にビットレートを確認します．")]
		[DefaultValue(true)]
		public bool PromptBitrateOnStartup {
			get { return this.promptBitrateOnStartup; }
			set { this.promptBitrateOnStartup = value; }
		}
		
		[Category("GUI")]
		[DisplayName("ビジュアルスタイル")]
		[Description("ビジュアルスタイルの適用領域の指定．切り替えに時間がかかる場合があります．")]
		[DefaultValue(VisualStyleState.ClientAndNonClientAreasEnabled)]
		public VisualStyleState VisualStyle {
			get { return Application.VisualStyleState; }
			set {
				if (value != Application.VisualStyleState) {
					Application.VisualStyleState = value;
				}
			}
		}
		private string iconFile = string.Empty;
		[Category("GUI")]
		[DisplayName("アイコンのファイル名")]
		[Description("アイコンのファイル名を指定．無指定の場合は実行ファイル名と同名のアイコンファイルが指定されているものとする．変更は再起動後に有効になります．")]
		[DefaultValue("")]
		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string IconFile {
			get { return this.iconFile; }
			set { this.iconFile = value; }
		}

		private bool useDefaultGenres = true;
		[Category("GyaOのジャンル")]
		[DisplayName("規定のジャンル一覧を利用")]
		[Description("True にするとアプリケーションに埋め込まれたジャンル一覧を使います．基本的には True で使ってください．False にすると Cache\\DataTable_21_Genre.xml のジャンル一覧を利用します．変更は再起動後に有効になります．")]
		[DefaultValue(true)]
		public bool UseDefaultGenres {
			get { return this.useDefaultGenres; }
			set { this.useDefaultGenres = value; }
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
		
		public Uri GetPlayerUriOf(GContentClass content) {
			switch (this.Bitrate) {
				case GBitrate.Standard:
					return content.PlayerSmallUri;
				case GBitrate.SuperFine:
					return content.PlayerLargeUri;
				default:
					throw new InvalidOperationException();
			}
		}
		public Uri GetRecomendationUriOf(GContentClass content) {
			switch (this.Bitrate) {
				case GBitrate.Standard:
					return content.RecommendationSmallUri;
				case GBitrate.SuperFine:
					return content.RecommendationLargeUri;
				default:
					throw new InvalidOperationException();
			}
		}
	}
}
