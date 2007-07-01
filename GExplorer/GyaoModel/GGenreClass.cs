using System;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Yusen.GExplorer.GyaoModel {
	[Serializable]
	[DefaultProperty("GenreKey")]
	public sealed class GGenreClass{
		private int genreKey;
		private string genreName;
		private string rootDirectory;
		private string imageDirectory;
		private Color genreColor;
		
		[NonSerialized]
		private string genreId;
		
		private GGenreClass() {
		}
		public GGenreClass(int genreKey, string genreName, string rootDir, string imageDir, Color genreColor)
			: this() {
			this.genreKey = genreKey;
			this.genreName = genreName;
			this.rootDirectory = rootDir;
			this.imageDirectory = imageDir;
			this.genreColor = genreColor;
		}
		public GGenreClass(GDataSet.GGenreRow genreRow)
			: this(
				genreRow.GenreKey,
				genreRow.GenreName,
				genreRow.RootDirectory,
				genreRow.ImageDirectory,
				Color.FromArgb(genreRow.GenreColorRed, genreRow.GenreColorGreen, genreRow.GenreColorBlue)) {
		}
		
		[Category("ジャンル定義")]
		[Description("ジャンルキー．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public int GenreKey {
			get { return this.genreKey; }
			set { this.genreKey = value; }
		}
		[Category("ジャンル定義")]
		[Description("ジャンル名．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public string GenreName {
			get { return this.genreName; }
			set { this.genreName = value; }
		}
		[Category("ジャンル定義")]
		[Description("ジャンルトップのディレクトリ名．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public string RootDirectory {
			get { return this.rootDirectory; }
			set { this.rootDirectory = value; }
		}
		[Category("ジャンル定義")]
		[Description("画像ファイルのディレクトリ名．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public string ImageDirectory {
			get { return this.imageDirectory; }
			set { this.imageDirectory = value; }
		}
		[Category("ジャンル定義")]
		[Description("ジャンルの色．")]
		[ReadOnly(true)]
		[XmlIgnore]
		public Color GenreColor {
			get { return this.genreColor; }
			set { this.genreColor = value; }
		}
		
		[Category("ID")]
		[Description("ジャンルID．")]
		[XmlIgnore]
		public string GenreId {
			get {
				if (null == this.genreId) {
					this.genreId = GConvert.ToGenreId(this.GenreKey);
				}
				return this.genreId;
			}
		}
		[Category("URI")]
		[Description("ジャンルトップページのURI．")]
		[XmlIgnore]
		public Uri GenreTopPageUri {
			get {return GUriBuilder.CreateGenreToppageUri(this.RootDirectory);}
		}

		[Browsable(false)]
		[XmlAttribute]
		public int GenreColorArgbValue {
			get { return this.GenreColor.ToArgb(); }
			set { this.GenreColor = Color.FromArgb(value); }
		}
	}
}
