using System;
using System.Collections.Generic;
using System.Text;
using Color = System.Drawing.Color;
using System.Collections.ObjectModel;

namespace Yusen.GCrawler {
	[Serializable]
	public abstract class GGenre {
		private static readonly GGenre[] allGenres;
		static GGenre() {
			GGenre.allGenres = new GGenre[]{
				new GGenre200507( 1, "cinema", "�f��", Color.FromArgb(0x93, 0x2e, 0x2e)),
				new GGenre200507( 3, "music", "���y", Color.FromArgb(0xf2, 0xa7, 0xc8)),
				new GGenre200505Drama( 2, "drama", "�h���}", Color.FromArgb(0xff, 0x66, 0x00)),
				new GGenre200507( 6, "anime", "�A�j��", Color.FromArgb(0xe7, 0x39, 0x8e)),
				new GGenre200505( 4, "idol", "�A�C�h���E�O���r�A", Color.FromArgb(0xf5, 0xa3, 0x00)),
				new GGenre200505( 5, "variety", "�o���G�e�B", Color.FromArgb(0x9c, 0x60, 0xa4)),
				new GGenre200505(10, "documentary", "�h�L�������^���[", Color.FromArgb(0xfa, 0xef, 0x45)),
				new GGenre200505(15, "life", "�t�@�b�V�����E�J���`���[", Color.FromArgb(0x53, 0x47, 0x9a)),
				new GGenre200505( 9, "sports", "�X�|�[�c", Color.FromArgb(0xab, 0xce, 0x30)),
				new GGenre200505(16, "business", "�r�W�l�X", Color.FromArgb(0x38, 0x72, 0xb9)),
				new GGenre200507( 7, "news", "�j���[�X", Color.FromArgb(0x00, 0xa5, 0x3c)),
				new GGenre200507VideoBlog(12, "videoblog", "�f���u���O", Color.FromArgb(0xb6, 0x24, 0xd4)),
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
		public Uri RootUri {
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
		[Serializable]
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
		private sealed class GGenre200505Drama : GGenre200505 {
			public GGenre200505Drama(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override string ImageDirName {
				get { return "dorama"; }
			}
		}
		[Serializable]
		private class GGenre200507 : GGenre{
			public GGenre200507(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override Uri TopPageUri {
				get {
					return new Uri("http://www.gyao.jp/" + base.DirectryName + "/");
				}
			}
		}
		[Serializable]
		private class GGenre200507VideoBlog : GGenre200507 {
			public GGenre200507VideoBlog(int keyNo, string dir, string name, Color color)
				: base(keyNo, dir, name, color) { }
			public override bool IsCrawlable {
				get { return false; }
			}
		}
	}
}
