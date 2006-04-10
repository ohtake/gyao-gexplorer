using System;
using System.Collections.Generic;
using Color = System.Drawing.Color;

namespace Yusen.GCrawler {
	[Serializable]
	public abstract class GGenre : IEquatable<GGenre> {
		private static readonly GGenre[] allGenres;
		private static readonly SortedDictionary<int, GGenre> dicGenre;
		static GGenre() {
			GGenre.allGenres = new GGenre[]{
				new GGenre200509NewsBiz(22, "news", "ニュース・ビジネス", Color.FromArgb(0x00, 0x33, 0x99)),
				new GGenre200509( 1, "cinema", "映画", Color.FromArgb(0xCC, 0x00, 0x00)),
				new GGenre200509( 3, "music", "音楽", Color.FromArgb(0x99, 0x33, 0xCC)),
				new GGenre200509Drama( 2, "drama", "ドラマ", Color.FromArgb(0xFF, 0x99, 0x66)),
				new GGenre200509( 9, "sports", "スポーツ", Color.FromArgb(0x00, 0x66, 0xFF)),
				new GGenre200509(10, "documentary", "ドキュメンタリー", Color.FromArgb(0x00, 0x99, 0x33)),
				new GGenre200509(21, "beauty", "ビューティー＆ヘルス", Color.FromArgb(0xFF, 0x99, 0xCC)),
				new GGenre200509(20, "life", "ライフ＆カルチャー", Color.FromArgb(0x99, 0x66, 0x33)),
				new GGenre200509( 6, "anime", "アニメ", Color.FromArgb(0x00, 0x99, 0xFF)),
				new GGenre200509( 5, "variety", "バラエティ", Color.FromArgb(0x66, 0xCC, 0x33)),
				new GGenre200509( 4, "idol", "アイドル・グラビア", Color.FromArgb(0xFF, 0x66, 0xCC)),
				new GGenre200509VideoBlog(12, "blog", "映像ブログ", Color.FromArgb(0xFF, 0x99, 0x99)),
				new GGenre200509(24, "shopping", "ショッピング", Color.FromArgb(0xFF, 0x66, 0x00)),
				new GGenre200509(25, "game", "ゲーム", Color.FromArgb(0x00, 0x99, 0x66)),
				new GGenre200505(14, "station", "ステーションコール", Color.Black),
			};
			GGenre.dicGenre = new SortedDictionary<int, GGenre>();
			foreach (GGenre genre in GGenre.AllGenres) {
				GGenre.dicGenre.Add(genre.GenreKey, genre);
			}
		}
		public static IEnumerable<GGenre> AllGenres {
			get {
				return GGenre.allGenres;
			}
		}
		public static bool TryGetGerneByKey(int key, out GGenre genre) {
			return GGenre.dicGenre.TryGetValue(key, out genre);
		}
		public static string ConvertToIdFromKey(int key) {
			return string.Format("gen{0:d7}", key);
		}
		public static int ConvertToKeyFromId(string id) {
			return int.Parse(id.Substring(3)); // 3 == "gen".Length
		}
		private static bool EqualsHelper(GGenre obj1, GGenre obj2) {
			return obj1.GenreKey.Equals(obj2.GenreKey);
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

		public int GenreKey {
			get { return this.keyNo; }
		}
		public string GenreId {
			get { return "gen" + this.keyNo.ToString("0000000"); }
		}
		public string DirectoryName {
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
				return new Uri("http://www.gyao.jp/" + this.DirectoryName + "/");
			}
		}
		public Uri TimetableRecentlyUpdatedFirstUri {
			get {
				return new Uri("http://www.gyao.jp/timetable/index.php?"
					+"genre_id=" + this.GenreId
					/* + "&sort_flag1=" + ((int)TimetableSortType.RecentlyUpdatedFirst).ToString()*/);
			}
		}
		public Uri TimetableDeadlineNearFirstUri {
			get {
				//GETでは取れないっぽい
				return null;
				//return new Uri("http://www.gyao.jp/timetable/index.php?genre_id=" + this.GenreId + "&sort_flag1=" + ((int)TimetableSortType.DeadlineNearFirst).ToString());
			}
		}
		public override bool Equals(object obj) {
			GGenre gObj = obj as GGenre;
			if (null == gObj) {
				return false;
			}
			return GGenre.EqualsHelper(this, gObj);
		}
		public bool Equals(GGenre other) {
			if(null == other) {
				return false;
			}
			return GGenre.EqualsHelper(this, other);
		}
		public override int GetHashCode() {
			return this.GenreKey.GetHashCode();
		}
		public override string ToString() {
			return "<" + this.GenreId + "> " + this.GenreName;
		}
		[Serializable]
		private class GGenre200505 : GGenre {
			public GGenre200505(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override Uri TopPageUri {
				get {
					return new Uri("http://www.gyao.jp/sityou/catetop/genre_id/" + this.GenreId + "/");
				}
			}
		}
		[Serializable]
		private class GGenre200509 : GGenre {
			public GGenre200509(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override Uri TopPageUri {
				get {
					return new Uri("http://www.gyao.jp/" + this.DirectoryName + "/");
				}
			}
		}
		[Serializable]
		private sealed class GGenre200509NewsBiz : GGenre200509 {
			public GGenre200509NewsBiz(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override string ImageDirName {
				get { return "newsbiz"; }
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
