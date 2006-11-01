using System;
using System.Collections.Generic;
using Color = System.Drawing.Color;
using System.Runtime.Serialization;

namespace Yusen.GExplorer.OldCrawler {
	[Serializable]
	public abstract class GGenre : IEquatable<GGenre> {
		private static readonly GGenre[] allGenres;
		private static readonly SortedDictionary<int, GGenre> dicGenre;
		static GGenre() {
			GGenre.allGenres = new GGenre[]{
				new GGenre200509(22, "newsbiz", "news", "ニュース・ビジネス", Color.FromArgb(0x00, 0x33, 0x99)),
				new GGenre200509( 1, "cinema", "映画", Color.FromArgb(0xCC, 0x00, 0x00)),
				new GGenre200509( 3, "music", "音楽", Color.FromArgb(0x99, 0x33, 0xCC)),
				new GGenre200509( 2, "dorama", "drama", "ドラマ", Color.FromArgb(0xFF, 0x99, 0x66)),
				new GGenre200509( 9, "sports", "スポーツ", Color.FromArgb(0x00, 0x66, 0xFF)),
				new GGenre200509(10, "documentary", "ドキュメンタリー", Color.FromArgb(0x00, 0x99, 0x33)),
				new GGenre200509(21, "health", "beauty", "ビューティー＆ファッション", Color.FromArgb(0xFF, 0x99, 0xCC)),
				new GGenre200509(20, "culture", "life", "ライフ＆カルチャー", Color.FromArgb(0x99, 0x66, 0x33)),
				new GGenre200509( 6, "anime", "アニメ", Color.FromArgb(0x00, 0x99, 0xFF)),
				new GGenre200509( 5, "variety", "バラエティ", Color.FromArgb(0x66, 0xCC, 0x33)),
				new GGenre200509( 4, "idol", "アイドル・グラビア", Color.FromArgb(0xFF, 0x66, 0xCC)),
				new GGenre200509VideoBlog(12, "blog", "映像ブログ", Color.FromArgb(0xFF, 0x99, 0x99)),
				new GGenre200509(24, "shopping", "ショッピング", Color.FromArgb(0xFF, 0x66, 0x00)),
				new GGenre200509(25, "game", "ゲーム", Color.FromArgb(0x00, 0x99, 0x66)),
				new GGenre200509(28, "comics", "コミックス", Color.FromArgb(0xA7, 0x0B, 0x85)),
				new GGenre200509(27, "mansion", "special", "マンション情報", Color.FromArgb(0xFF, 0xC1, 0x00)),
				/*
				 *  7	--> news
				 *  8	ラジオ
				 * 11	教育・学習
				 * 13	世の中のイベント・動き
				 * 14	ステーションコール (pac0000062)
				 * 15	--> beauty
				 * 16	ビジネス
				 * 17	オメコメ
				 * 18	--> c.gyao.jp
				 * 19	--> election
				 * 23	アンケート
				 * 26	テスト (pac0001397)
				 */
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
		private readonly string imageDir;
		private readonly string name;
		private readonly Color color;

		protected GGenre(int keyNo, string imageDir, string name, Color color) {
			this.keyNo = keyNo;
			this.imageDir = imageDir;
			this.name = name;
			this.color = color;
		}

		public int GenreKey {
			get { return this.keyNo; }
		}
		public string GenreId {
			get { return string.Format("gen{0:d7}", this.keyNo); }
		}
		public string ImageDirName {
			get { return this.imageDir; }
		}
		public string GenreName {
			get { return this.name; }
		}
		public Color GenreForeColor {
			get { return this.color; }
		}
		public abstract bool IsCrawlable {get;}
		public abstract Uri TopPageUri {get;}
		public abstract Uri RootUri { get;}
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
			return string.Format("<{0}> {1}", this.GenreId, this.GenreName);
		}
		[Serializable]
		[Obsolete("200505形式のジャンルは多分もうない (2006-07-07)", true)]
		private class GGenre200505 : GGenre {
			public GGenre200505(int keyNo, string imageDir, string name, Color color)
				: base(keyNo, imageDir, name, color) { }
			public override Uri TopPageUri {
				get {
					return new Uri("http://www.gyao.jp/sityou/catetop/genre_id/" + this.GenreId + "/");
				}
			}
			public override Uri RootUri {
				get { return this.TopPageUri; }
			}
			public override bool IsCrawlable {
				get { return true; }
			}
		}
		[Serializable]
		private class GGenre200509 : GGenre {
			private readonly string rootDir;
			public GGenre200509(int keyNo, string sameDir, string name, Color color)
				: this(keyNo, sameDir, sameDir, name, color) {
			}
			public GGenre200509(int keyNo, string imageDir, string rootDir, string name, Color color)
				: base(keyNo, imageDir, name, color) {
				this.rootDir = rootDir;
			}
			public override Uri TopPageUri {
				get {
					return new Uri("http://www.gyao.jp/" + this.rootDir + "/");
				}
			}
			public override Uri RootUri {
				get { return this.TopPageUri; }
			}
			public override bool IsCrawlable {
				get { return true; }
			}
		}
		[Serializable]
		private sealed class GGenre200509VideoBlog : GGenre200509 {
			public GGenre200509VideoBlog(int keyNo, string sameDir, string name, Color color)
				: base(keyNo, sameDir, name, color) { }
			public override Uri RootUri {
				// blog の動的なページによりトラップにはまるので
				// トップと番組表のみしか読まないようにする
				get { return new Uri("http://www.gyao.jp/dummy/"); }
			}
		}
	}
}
