using System;

namespace Yusen.GExplorer {
	public struct Category {
		private readonly string name;
		private readonly Uri uri;
		
		public string Name {
			get {
				return this.name;
			}
		}
		public Uri Uri {
			get {
				return this.uri;
			}
		}
		
		private Category(string name, Uri uri) {
			this.name = name;
			this.uri = uri;
		}
		public override string ToString() {
			return this.name;
		}
		
		public static Category[] ListCategories() {
			return new Category[]{
					new Category("映画", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000001/")),
					new Category("ドラマ", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000002/")),
					new Category("アイドル・グラビア", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000004/")),
					new Category("ドキュメンタリー", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000010/")),
					new Category("スポーツ", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000009/")),
					new Category("音楽", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000003/")),
					new Category("アニメ", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000006/")),
					new Category("バラエティ", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000005/")),
					new Category("ライフ", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000015/")),
					new Category("ビジネス", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000016/")),
				};
		}
	}
}
