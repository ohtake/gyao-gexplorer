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

		private string migemoDicFile = @"dict\migemo-dict";
		[Category("Migemo")]
		[DisplayName("Migemoの辞書ファイル")]
		[Description("Migemoの辞書ファイルを指定します．再起動後に変更が有効になります．")]
		[DefaultValue(@"dict\migemo-dict")]
		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string MigemoDicFile {
			get { return this.migemoDicFile; }
			set { this.migemoDicFile = value; }
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
