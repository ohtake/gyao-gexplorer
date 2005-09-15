using System;
using System.Collections.Generic;
using Color = System.Drawing.Color;

namespace Yusen.GCrawler {
	[Serializable]
	public abstract class GGenre {
		private static readonly GGenre[] allGenres;
		static GGenre() {
			GGenre.allGenres = new GGenre[]{
				new GGenre200509(22, "news", "ニュース・ビジネス", Color.FromArgb(0xCC, 0x00, 0xFF)),
				new GGenre200509( 1, "cinema", "映画", Color.FromArgb(0x99, 0x00, 0xFF)),
				new GGenre200509( 3, "music", "音楽", Color.FromArgb(0x66, 0x00, 0xFF)),
				new GGenre200509Drama( 2, "drama", "ドラマ", Color.FromArgb(0x33, 0x00, 0xFF)),
				new GGenre200509( 9, "sports", "スポーツ", Color.FromArgb(0x00, 0x66, 0xFF)),
				new GGenre200509(10, "documentary", "ドキュメンタリー", Color.FromArgb(0x00, 0xCC, 0xFF)),
				new GGenre200509(21, "beauty", "ビューティー＆ヘルス", Color.FromArgb(0x00, 0xFF, 0xFF)),
				new GGenre200509(20, "life", "ライフ＆カルチャー", Color.FromArgb(0x00, 0xFF, 0x99)),
				new GGenre200509( 6, "anime", "アニメ", Color.FromArgb(0x00, 0xFF, 0x33)),
				new GGenre200509( 5, "variety", "バラエティ", Color.FromArgb(0x99, 0xFF, 0x00)),
				new GGenre200509( 4, "idol", "アイドル・グラビア", Color.FromArgb(0xFF, 0xFF, 0x00)),
				new GGenre200509VideoBlog(12, "blog", "映像ブログ", Color.FromArgb(0xFF, 0x99, 0x00)),
			};
		}
		public static IEnumerable<GGenre> AllGenres {
			get {
				return GGenre.allGenres;
			}
		}

		private readonly int keyNo;
		private readonly string dir;
		private readonly string name;
		private readonly Color color;
		
		protected GGenre(int keyNo, string dir, string name, Color color) {
			this.keyNo = keyNo;
			this.dir = dir;
			this.name = name;
			this.color = color;
		}
		
		public string GenreId {
			get { return "gen" + this.keyNo.ToString("0000000"); }
		}
		public string DirectryName {
			get { return this.dir; }
		}
		public virtual string ImageDirName {
			get { return this.dir; }
		}
		public string GenreName {
			get { return this.name; }
		}
		public Color GenreColor {
			get { return this.color; }
		}
		public virtual bool IsCrawlable {
			get { return true; }
		}
		public abstract Uri TopPageUri {
			get;
		}
		public virtual Uri RootUri {
			get {
				return new Uri("http://www.gyao.jp/" + this.DirectryName + "/");
			}
		}
		public Uri TimetableUri {
			get {
				return new Uri("http://www.gyao.jp/timetable/index.php?genre_id=" + this.GenreId);
			}
		}
		public override bool Equals(object obj) {
			if (null == obj) {
				return false;
			}
			if (!(obj is GGenre)) {
				return base.Equals(obj);
			}
			return this.GenreId.Equals((obj as GGenre).GenreId);
		}
		public override int GetHashCode() {
			return this.GenreId.GetHashCode();
		}
		public override string ToString() {
			return "<" + this.GenreId + "> " + this.GenreName;
		}
		#region 2005-05/2005-07
		[Serializable]
		[Obsolete]
		private class GGenre200505 : GGenre {
			public GGenre200505(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			
			public override Uri TopPageUri {
				get {
					return new Uri("http://www.gyao.jp/sityou/catetop/genre_id/" + base.GenreId + "/");
				}
			}
		}
		[Serializable]
		[Obsolete]
		private sealed class GGenre200505Drama : GGenre200505 {
			public GGenre200505Drama(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override string ImageDirName {
				get { return "dorama"; }
			}
		}
		[Serializable]
		[Obsolete]
		private class GGenre200507 : GGenre{
			public GGenre200507(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override Uri TopPageUri {
				get {
					return new Uri("http://www.gyao.jp/" + base.DirectryName + "/");
				}
			}
		}
		[Obsolete("ビデオブログは番組表からパッケージIDが取得可能")]
		[Serializable]
		private class GGenre200507VideoBlog : GGenre200507 {
			public GGenre200507VideoBlog(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override bool IsCrawlable {
				get { return false; }
			}
		}
		#endregion
		[Serializable]
		private class GGenre200509 : GGenre {
			public GGenre200509(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override Uri TopPageUri {
				get {
					return new Uri("http://www.gyao.jp/" + this.DirectryName + "/");
				}
			}
		}
		[Serializable]
		private sealed class GGenre200509Drama : GGenre200509 {
			public GGenre200509Drama(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			// GyaO 側のスペルミスへの対応
			public override string ImageDirName {
				get { return "dorama"; }
			}
		}
		[Serializable]
		private sealed class GGenre200509VideoBlog : GGenre200509 {
			public GGenre200509VideoBlog(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override Uri RootUri {
				// blog の動的なページによりトラップにはまるので
				// トップと番組表のみしか読まないようにする
				get { return new Uri("http://www.gyao.jp/dummy/"); }
			}
		}
	}
}
