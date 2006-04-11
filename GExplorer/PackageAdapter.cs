using System;
using System.Collections.Generic;
using System.Text;
using Yusen.GCrawler;
using System.Drawing;
using System.Collections.ObjectModel;

namespace Yusen.GExplorer {
	public class PackageAdapter {
		private sealed class UnknownGenre : GGenre {
			public static GGenre Default = new UnknownGenre();
			private UnknownGenre()
				: base(0, "unknown", "(不明なジャンル)", Color.Black) {
			}
			public override Uri TopPageUri {
				get { return new Uri("about:blank"); }
			}
			public override Uri RootUri {
				get { return new Uri("about:blank"); }
			}
			public override bool IsCrawlable {
				get { return false; }
			}
		}
		
		private readonly GPackage innerPackage;
		private GGenre genre = null;
		private ReadOnlyCollection<ContentAdapter> cas = null;

		public PackageAdapter(GPackage innerPackage) {
			this.innerPackage = innerPackage;
		}

		private GGenre Genre {
			get {
				if (null == this.genre) {
					GGenre gen;
					if (GGenre.TryGetGerneByKey(this.innerPackage.GenreKey, out gen)) {
						this.genre = gen;
					} else {
						this.genre = UnknownGenre.Default;
					}
				}
				return this.genre;
			}
		}

		public int PackageKey {
			get { return this.innerPackage.PackageKey; }
		}
		public string PackageId {
			get { return this.innerPackage.PackageId; }
		}
		public int GenreKey {
			get { return this.Genre.GenreKey; }
		}
		public string GenreId {
			get { return this.Genre.GenreId; }
		}

		public string PackageName {
			get { return this.innerPackage.PackageName; }
		}
		public string CatchCopy {
			get { return this.innerPackage.CatchCopy; }
		}
		public string PackageText1 {
			get { return this.innerPackage.PackageText1; }
		}

		public ReadOnlyCollection<GContent> Contents {
			get { return this.innerPackage.Contents; }
		}
		public ReadOnlyCollection<ContentAdapter> ContentAdapters {
			get {
				if (null == this.cas) {
					List<ContentAdapter> list = new List<ContentAdapter>(this.Contents.Count);
					foreach (GContent content in this.Contents) {
						list.Add(new ContentAdapter(content));
					}
					this.cas = new ReadOnlyCollection<ContentAdapter>(list);
				}
				return this.cas;
			}
		}
		
		public Uri PackagePageUri {
			get { return this.innerPackage.PackagePageUri; }
		}
		public Uri ImageMiddleUri {
			get { return GPackage.CreateImageUri(this.PackageKey, this.Genre.ImageDirName, 'm'); }
		}
	}
}
