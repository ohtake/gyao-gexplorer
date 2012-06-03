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
					new Category("�f��", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000001/")),
					new Category("�h���}", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000002/")),
					new Category("�A�C�h���E�O���r�A", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000004/")),
					new Category("�h�L�������^���[", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000010/")),
					new Category("�X�|�[�c", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000009/")),
					new Category("���y", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000003/")),
					new Category("�A�j��", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000006/")),
					new Category("�o���G�e�B", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000005/")),
					new Category("���C�t", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000015/")),
					new Category("�r�W�l�X", new Uri("http://www.gyao.jp/sityou/catetop/genre_id/gen0000016/")),
				};
		}
	}
}
