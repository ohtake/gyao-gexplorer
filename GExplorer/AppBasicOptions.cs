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
	public sealed class AppBasicOptions {
		public AppBasicOptions() {
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
	}
}
