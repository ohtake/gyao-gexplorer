using System;
using System.Collections.Generic;
using Color = System.Drawing.Color;
using System.Runtime.Serialization;

namespace Yusen.GExplorer.OldCrawler {
	[Serializable]
	public abstract class GGenre {
		private static readonly GGenre[] allGenres;
		private static readonly SortedDictionary<int, GGenre> dicGenre;
		static GGenre() {
			GGenre.allGenres = new GGenre[]{
				new GGenre200509(22, "ニュース・ビジネス"),
				new GGenre200509( 1, "映画"),
				new GGenre200509( 3, "音楽"),
				new GGenre200509( 2, "ドラマ"),
				new GGenre200509( 9, "スポーツ"),
				new GGenre200509(10, "ドキュメンタリー"),
				new GGenre200509(21, "ビューティー＆ファッション"),
				new GGenre200509(20, "ライフ＆カルチャー"),
				new GGenre200509( 6, "アニメ"),
				new GGenre200509( 5, "バラエティ"),
				new GGenre200509( 4, "アイドル・グラビア"),
				new GGenre200509(12, "映像ブログ"),
				new GGenre200509(24, "ショッピング"),
				new GGenre200509(25, "ゲーム"),
				new GGenre200509(28, "コミックス"),
				new GGenre200509(27, "マンション情報"),
			};
			GGenre.dicGenre = new SortedDictionary<int, GGenre>();
			foreach (GGenre genre in GGenre.allGenres) {
				GGenre.dicGenre.Add(genre.GenreKey, genre);
			}
		}
		public static bool TryGetGerneByKey(int key, out GGenre genre) {
			return GGenre.dicGenre.TryGetValue(key, out genre);
		}

		private readonly int keyNo;
		private readonly string name;

		protected GGenre(int keyNo, string name) {
			this.keyNo = keyNo;
			this.name = name;
		}

		public int GenreKey {
			get { return this.keyNo; }
		}
		public string GenreName {
			get { return this.name; }
		}
		[Serializable]
		private class GGenre200509 : GGenre {
			public GGenre200509(int keyNo, string name)
				: base(keyNo, name) {
			}
		}
	}
}
