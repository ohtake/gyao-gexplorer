using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using Yusen.GExplorer.OldCrawler;

namespace Yusen.GExplorer.OldApp {
	public class ContentAdapter{
		public sealed class UnknownGenre : GGenre {
			public static GGenre Default = new UnknownGenre();
			private UnknownGenre()
				: base(0, "(不明なジャンル)") {
			}
		}

		public const string PropertyNameContentId = "ContentId";
		public const string PropertyNamePackageId = "PackageId";
		public const string PropertyNameTitle = "Title";
		
		private static bool EqualsHelper(ContentAdapter cont1, ContentAdapter cont2) {
			return cont1.ContentKey.Equals(cont2.ContentKey);
		}

		private GContent innerCont;
		private GGenre genre = null;
		
		public ContentAdapter() {
		}
		public ContentAdapter(GContent innerCont) {
			this.InnerContent = innerCont;
		}

		[Browsable(false)]
		public GContent InnerContent {
			get {
				return this.innerCont;
			}
			set {
				this.innerCont = value;
			}
		}
		[XmlIgnore]
		[Browsable(false)]
		internal GGenre Genre {
			get {
				if (null == this.genre) {
					GGenre genre;
					if (GGenre.TryGetGerneByKey(this.GenreKey, out genre)) {
						this.genre = genre;
					} else {
						this.genre = UnknownGenre.Default;
					}
				}
				return this.genre;
			}
		}
		
		[XmlIgnore]
		[Category("キー")]
		[Description("コンテンツのキー．")]
		public int ContentKey {
			get { return this.innerCont.ContentKey; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("コンテンツのID．")]
		public string ContentId {
			get { return this.innerCont.ContentId; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("パッケージのキー．")]
		public int PackageKey {
			get { return this.innerCont.PackageKey; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("パッケージのID．")]
		public string PackageId {
			get { return this.innerCont.PackageId; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("ジャンルのキー．")]
		public int GenreKey {
			get { return this.innerCont.GenreKey; }
		}
		
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("タイトル．")]
		public string Title {
			get { return this.innerCont.Title; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("シリーズ番号．")]
		public string SeriesNumber {
			get { return this.innerCont.SeriesNumber; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("サブタイトル．")]
		public string Subtitle {
			get { return this.innerCont.Subtitle; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("番組時間 (CM時間を除く)．")]
		public string Duration {
			get { return this.innerCont.Duration; }
		}
		[XmlIgnore]
		[Category("付随情報P")]
		[Description("サマリー．")]
		public string Summary {
			get { return this.innerCont.Summary; }
		}
		[XmlIgnore]
		[Category("付随情報P")]
		[Description("配信期限．")]
		public string Deadline {
			get { return this.innerCont.Deadline; }
		}

		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("クロール前にはキャッシュになく，尚且つダミーでないコンテンツは新着と判断される．")]
		public bool IsNew {
			get { return !this.innerCont.FromCache; }
		}
	}
}
