using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Yusen.GExplorer.GyaoModel {
	[Serializable]
	[DefaultProperty("PackageKey")]
	public sealed class GPackageClass {
		private int packageKey;
		private int? genreKey;
		private string packageTitle;
		private string packageCatch;
		private string packageText;
		private DateTime created;
		private DateTime lastModified;
		
		private GGenreClass parent;
		
		[NonSerialized]
		private string packageId;
		[NonSerialized]
		private string genreId;
		
		private GPackageClass() {
			GDataSet ds = new GDataSet();
			
		}

		public GPackageClass(GDataSet.GPackageRow row, GGenreClass parent)
			: this() {
			this.packageKey = row.PackageKey;
			this.genreKey = row.IsGenreKeyNull() ? (int?)null : row.GenreKey;
			this.packageTitle = row.IsPackageTitleNull() ? null : row.PackageTitle;
			this.packageCatch = row.IsPackageCatchNull() ? null : row.PackageCatch;
			this.packageText = row.IsPackageTextNull() ? null : row.PackageText;
			this.created = row.Created;
			this.lastModified = row.LastModified;
			
			this.parent = parent;
		}

		#region GPackageRow にあるプロパティ
		[Category("キー")]
		[Description("パッケージキー．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public int PackageKey {
			get { return this.packageKey; }
			set { this.packageKey = value; }
		}
		[Category("キー")]
		[Description("ジャンルキー．")]
		[ReadOnly(true)]
		public int? GenreKey {
			get { return this.genreKey; }
			set { this.genreKey = value; }
		}
		[Category("パッケージ情報")]
		[Description("パッケージタイトル．")]
		[ReadOnly(true)]
		public string PackageTitle {
			get { return this.packageTitle; }
			set { this.packageTitle = value; }
		}
		[Category("パッケージ情報")]
		[Description("パッケージキャッチ．")]
		[ReadOnly(true)]
		public string PackageCatch {
			get { return this.packageCatch; }
			set { this.packageCatch = value; }
		}
		[Category("パッケージ情報")]
		[Description("パッケージテキスト．")]
		[ReadOnly(true)]
		public string PackageText {
			get { return this.packageText; }
			set { this.packageText = value; }
		}
		[Category("キャッシュ情報")]
		[Description("データロウ作成日時．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public DateTime Created {
			get { return this.created; }
			set { this.created = value; }
		}
		[Category("キャッシュ情報")]
		[Description("データロウ最終更新日時．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public DateTime LastModified {
			get { return this.lastModified; }
			set { this.lastModified = value; }
		}
		#endregion

		#region GPackageRow にあるプロパティから作成できるプロパティ
		[Category("キー")]
		[Description("パッケージID．")]
		[XmlIgnore]
		public string PackageId {
			get {
				if (null == this.packageId) {
					this.packageId = GConvert.ToPackageId(this.PackageKey);
				}
				return this.packageId;
			}
		}
		[Category("キー")]
		[Description("ジャンルID．不明な場合は gen0000000 とする．")]
		[XmlIgnore]
		public string GenreId {
			get {
				if (null == this.genreId) {
					if (this.GenreKey.HasValue) {
						this.genreId = GConvert.ToGenreId(this.GenreKey.Value);
					} else {
						this.genreId = GConvert.ToGenreId(0);
					}
				}
				return this.genreId;
			}
		}
		[Category("URI")]
		[Description("シリーズ一覧ページのURI．")]
		[XmlIgnore]
		public Uri PackagePageUri {
			get {
				return GUriBuilder.CreatePackagePageUri(this.PackageId);
			}
		}
		[Category("URI")]
		[Description("画像(中)のURI．")]
		[XmlIgnore]
		public Uri ImageMiddleUri {
			get {
				return GUriBuilder.CreatePackageImageMiddleUri(this.PackageId, this.ImageDirectory);
			}
		}
		#endregion

		[Browsable(false)]
		public GGenreClass ParentGenre {
			get { return this.parent; }
			set { this.parent = value; }
		}

		[Browsable(false)]
		[XmlIgnore]
		private string ImageDirectory {
			get {
				if (null != this.ParentGenre) {
					return this.ParentGenre.ImageDirectory;
				} else {
					return "dummy";
				}
			}
		}
	}
}
