using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

/* GyaOのコンテンツDBは以下の木構造になっているはず？
 * ジャンル
 *   └─パッケージ
 *         └─コンテンツ
 */

namespace Yusen.GExplorer{
	delegate void LoadingPackagesEventHandler(GGenre sender, int nume, int denom);

	/// <summary>GyaOにおける genre．映画，ドラマなど．</summary>
	class GGenre{
		public static event LoadingPackagesEventHandler LoadingPackages;
		
		private static IEnumerable<GGenre> allGenres =
			new GGenre[]{
				new GGenre( 1, "映画", "cinema"),
				new GGenre( 2, "ドラマ", "dorama"),
				new GGenre( 4, "アイドル・グラビア", "idol"),
				new GGenre(10, "ドキュメンタリー", "documentary"),
				new GGenre( 9, "スポーツ", "sports"),
				new GGenre( 3, "音楽", "music"),
				new GGenre( 6, "アニメ", "anime"),
				new GGenre( 5, "バラエティ", "variety"),
				new GGenre(15, "ライフ", "life"),
				new GGenre(16, "ビジネス", "business"),
			};
		private static readonly Regex regexPacId = 
			new Regex(@"^<img src=""/img/info/[^/]+/pac([0-9]+)_m\.jpg""", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexSpecial =
			new Regex(@"<a href=""([^""]+)"".+?alt=""特集ページ""", RegexOptions.Compiled | RegexOptions.Singleline);
		
		public static IEnumerable<GGenre> AllGenres {
			get {
				return GGenre.allGenres;
			}
		}
		
		private readonly int keyNo;
		private readonly string name;
		private readonly string dir;
		private IEnumerable<GPackage> children = null;
		private DateTime lastFetch = default(DateTime);
		
		private GGenre(int keyNo, string name, string dir) {
			this.keyNo = keyNo;
			this.name = name;
			this.dir = dir;
		}
		
		[Category("付随情報")]
		[Description("ジャンル名．")]
		public string GenreName {
			get {
				return this.name;
			}
		}
		[Category("キー")]
		[Description("ジャンルに対応するディレクトリ名．")]
		public string DirectoryName {
			get {
				return this.dir;
			}
		}
		[Category("キー")]
		[Description("genre_id")]
		public string GenreId {
			get {
				return "gen" + this.keyNo.ToString("0000000");
			}
		}
		[Category("URI")]
		[Description("ジャンルトップページのURI．")]
		public Uri GenreTopPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catetop/genre_id/" + this.GenreId + "/");
			}
		}
		[Category("専ブラが付加した情報")]
		[Description("読み込み済みであるか否か．")]
		public bool IsLoaded {
			get {
				return null != this.children;
			}
		}
		[Browsable(false)]
		public IEnumerable<GPackage> Packages {
			get {
				if(! this.IsLoaded) throw new InvalidOperationException();
				return this.children;
			}
		}
		[Category("専ブラが付加した情報")]
		[Description("カテゴリトップページを専ブラが最後に読み込んだ日時．")]
		public DateTime LastFetchTime{
			get{
				return this.lastFetch;
			}
		}
		public override string ToString() {
			return "<" + this.GenreId + "> " + this.GenreName;
		}
		public IEnumerable<GPackage> FetchPackages() {
			List<GPackage> packages = new List<GPackage>();
			TextReader reader = new StreamReader(new WebClient().OpenRead(this.GenreTopPageUri), Encoding.GetEncoding("Shift_JIS"));
			
			string line;
			Queue<string> sgName = new Queue<string>();
			Queue<string> sgCatch = new Queue<string>();
			string packName = "";
			List<string> specialPages = new List<string>();
			while(null != (line = reader.ReadLine())) {
				if(!line.StartsWith("<!--")) continue;//下記のswitch文を弄るときに要注意
				Match match;
				switch(line) {
					case "<!--サブジャンル名 ↓-->":
						sgName.Enqueue(Utility.ReadNextLineTextBeforeTag(reader));
						break;
					case "<!--サブジャンルキャッチコピー ↓-->":
						sgCatch.Enqueue(Utility.ReadNextLineTextBeforeTag(reader));
						break;
					case "<!--パック名 ↓-->":
						packName = Utility.ReadNextLineTextBeforeTag(reader);
						break;
					case "<!--パック画像（中） ↓-->":
						match = GGenre.regexPacId.Match(Utility.ReadNextLineHtml(reader));
						if(match.Success) {
							packages.Add(new GPackage(
								this,
								int.Parse(match.Groups[1].Value),
								packName,
								0 == sgName.Count ? "" : sgName.Dequeue(),
								0 == sgCatch.Count ? "" : sgCatch.Dequeue()));
						} else {
							throw new Exception();
						}
						break;
					case "<!-- ボタン ↓ -->":
						match = GGenre.regexSpecial.Match(Utility.ReadNextLineHtml(reader));
						if(match.Success) {
							specialPages.Add(match.Groups[1].Value);
						} else {
							specialPages.Add(null);
						}
						break;
				}
			}
			for(int i = 0; i < specialPages.Count; i++) {
				string sp = specialPages[i];
				if(null != sp) {
					packages[i].SpecialPageUri = new Uri(this.GenreTopPageUri, sp);
				}
			}
			this.children = packages;
			this.lastFetch = DateTime.Now;
			return this.Packages;
		}
		public void FetchAll(){
			IEnumerable<GPackage> ps = this.FetchPackages();
			int denom = 1;
			foreach(GPackage p in ps) denom++;
			int nume = 0;
			foreach(GPackage p in this.FetchPackages()) {
				GGenre.LoadingPackages(this, ++nume, denom);
				p.FetchContents();
			}
			GGenre.LoadingPackages(this, denom, denom);
		}
	}
	
	/// <summary>
	/// GyaOにおける pac．「全シリーズを見る」のボタンを押したときの飛び先．
	/// 「全シリーズを見る」のボタンがなくても，各コンテンツは何らかのパックに所属している模様．
	/// </summary>
	class GPackage {
		private static readonly Regex regexPCatch =
			new Regex("^\t\t" + @"<td class=""text12b""><b>(.*?)<!-- パックキャッチコピー -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexPText =
			new Regex("^\t\t" + @"<td>(.*?)<!-- パックテキスト１ -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexSnum =
			new Regex("^\t\t\t\t" + @"<td align=""left"" class=""title12"">(.*?)<!-- ストーリー番号 -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexLimit =
			new Regex("^\t\t\t\t\t\t\t\t\t\t\t\t" + "(?:<[^>]*>)*(.*正午まで)", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexStory =
			new Regex("^\t\t\t\t\t\t\t" + @"<td valign=""top"" class=""text10"">(.*?)<!-- ストーリー -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexCnt =
			new Regex("^\t\t\t\t\t\t" + @"<td><a href=""javascript:gotoDetail\( 'cnt([0-9]+)' \)""", RegexOptions.Compiled | RegexOptions.Singleline);
		
		private readonly GGenre parent;
		private readonly int keyNo;
		private readonly string name;
		private readonly string subgenreName;
		private readonly string subgenreCatch;
		private string pacCatch = null;
		private string pacText = null;
		private Uri specialPage = null;
		private IEnumerable<GContent> children = null;
		private DateTime lastFetch = default(DateTime);

		public GPackage(GGenre parent, int keyNo, string name,
				string subgenreName, string subgenreCatch) {
			this.parent = parent;
			this.keyNo = keyNo;
			this.name = name;
			this.subgenreName = subgenreName;
			this.subgenreCatch = subgenreCatch;
		}
		[Browsable(false)]
		public GGenre Genre {
			get {
				return this.parent;
			}
		}
		[Category("付随情報")]
		[Description("サブジャンル名．")]
		public string SubgenreName {
			get {
				return this.subgenreName;
			}
		}
		[Category("付随情報")]
		[Description("サブジャンルのキャッチコピー")]
		public string SubgenreCatch {
			get {
				return this.subgenreCatch;
			}
		}
		[Category("付随情報")]
		[Description("パッケージ名．")]
		public string PackageName {
			get {
				return this.name;
			}
		}
		[Category("URI")]
		[Description("パッケージ内のコンテンツ一覧を見られるページのURI．")]
		public Uri PackagePageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catelist/pac_id/" + this.PackageId + "/");
			}
		}
		[Category("キー")]
		[Description("pac_id")]
		public string PackageId {
			get {
				return "pac" + this.keyNo.ToString("0000000");
			}
		}
		[ReadOnly(true)]
		[Category("付随情報")]
		[Description("パッケージのキャッチコピー．")]
		public string PackageCatch {
			get {
				if(null == this.pacCatch) throw new InvalidOperationException();
				return this.pacCatch;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				if(null != this.pacCatch) throw new InvalidOperationException();
				this.pacCatch = value;
			}
		}
		[ReadOnly(true)]
		[Category("付随情報")]
		[Description("パッケージの説明文．")]
		public string PackageText {
			get {
				if(null == this.pacText) throw new InvalidOperationException();
				return this.pacText;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				if(null != this.pacText) throw new InvalidOperationException();
				this.pacText = value;
			}
		}
		[ReadOnly(true)]
		[Category("URI")]
		[Description("特集ページのURI．専ブラでは特集ページの解析は行っていないので通常のウェブブラウザで中身を確認する必要あり．")]
		public Uri SpecialPageUri {
			get {
				if(null == this.specialPage) {
					//throw new InvalidOperationException();
					return null;
				}
				return this.specialPage;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				if(null != this.specialPage) throw new InvalidOperationException();
				this.specialPage = value;
			}
		}
		[Category("専ブラが付加した情報")]
		[Description("特集ページの有無．")]
		public bool HasSpecialPage {
			get {
				return null != this.specialPage;
			}
		}
		[Category("専ブラが付加した情報")]
		[Description("読み込み済みであるか否か．")]
		public bool IsLoaded {
			get {
				return null != this.children;
			}
		}
		[Browsable(false)]
		public IEnumerable<GContent> Contents {
			get {
				if(! this.IsLoaded) throw new InvalidOperationException();
				return this.children;
			}
		}
		[Category("専ブラが付加した情報")]
		[Description("パッケージページを最後に読み込んだ日時．")]
		public DateTime LastFetchTime {
			get {
				return this.lastFetch;
			}
		}
		[Category("URI")]
		[Description("画像 (中) のURI．")]
		public Uri ImageMiddleUri {
			get {
				return new Uri("http://www.gyao.jp/img/info/"
					+ this.Genre.DirectoryName + "/"
					+ this.PackageId + "_m.jpg");
			}
		}
		public override string ToString() {
			return "<" + this.PackageId + "> " + this.PackageName;
		}
		
		public IEnumerable<GContent> FetchContents() {
			TextReader reader = new StreamReader(new WebClient().OpenRead(this.PackagePageUri), Encoding.GetEncoding("Shift_JIS"));
			
			string line;
			while(null != (line = reader.ReadLine())){
				Match match = GPackage.regexPCatch.Match(line);
				if(match.Success) {
					this.PackageCatch = match.Groups[1].Value;
					break;
				}
			}
			while(null != (line = reader.ReadLine())) {
				Match match = GPackage.regexPText.Match(line);
				if(match.Success) {
					this.PackageText = match.Groups[1].Value;
					break;
				}
			}
			if(null == line) throw new Exception();
			
			List<GContent> contents = new List<GContent>();
			while(null != line) {
				string snum = "";
				while(null != (line = reader.ReadLine())) {
					Match match = GPackage.regexSnum.Match(line);
					if(match.Success) {
						snum = match.Groups[1].Value;
						break;
					}
				}
				bool isNew = false;
				while(null != (line = reader.ReadLine())) {
					switch(line) {
						case "\t\t\t\t\t\t\t\t\t&nbsp;":
							isNew = false;
							break;
						case "\t\t\t\t\t\t\t\t\t<img src=\"http://www.gyao.jp/sityou/common/images/icon_new.gif\" alt=\"NEW\" width=\"32\" height=\"14\" border=\"0\">":
							isNew = true;
							break;
						default:
							continue;
					}
					break;
				}
				string limit = "";
				while(null != (line = reader.ReadLine())) {
					Match match = GPackage.regexLimit.Match(line);
					if(match.Success) {
						limit = match.Groups[1].Value;
						break;
					}
				}
				string story = "";
				while(null != (line = reader.ReadLine())) {
					Match match = GPackage.regexStory.Match(line);
					if(match.Success) {
						story = match.Groups[1].Value;
						break;
					}
				}
				int contId = 0;
				while(null != (line = reader.ReadLine())) {
					Match match = GPackage.regexCnt.Match(line);
					if(match.Success) {
						contId = int.Parse(match.Groups[1].Value);
						break;
					}
				}
				if(null == line) break;
				contents.Add(new GContent(this, contId, snum, story, isNew, limit));
			}

			this.children = contents;
			this.lastFetch = DateTime.Now;
			return this.Contents;
		}
	}
	
	/// <summary>GyaOにおける cont．</summary>
	class GContent {
		private readonly GPackage parent;
		private readonly int keyNo;
		private readonly string name;
		private readonly string lead;
		private readonly bool isNew;
		private readonly string limit;
		
		public GContent(GPackage parent, int keyNo, string name, string lead, bool isNew, string limit) {
			this.parent = parent;
			this.keyNo = keyNo;
			this.name = name;
			this.lead = lead;
			this.isNew = isNew;
			this.limit = limit;
		}
		[Browsable(false)]
		public GPackage Package {
			get {
				return this.parent;
			}
		}
		[Browsable(false)]
		public GGenre Genre {
			get {
				return this.Package.Genre;
			}
		}
		[Category("付随情報")]
		[Description("コンテンツ名 (話)．")]
		public string ContentName {
			get {
				return this.name;
			}
		}
		[Category("キー")]
		[Description("contents_id")]
		public string ContentId {
			get {
				return "cnt" + this.keyNo.ToString("0000000");
			}
		}
		[Category("付随情報")]
		[Description("Newマークがついているか否か．")]
		public bool IsNew {
			get {
				return this.isNew;
			}
		}
		[Category("付随情報")]
		[Description("リード．もっと長い説明文を読みたければ詳細ページを読むべし．")]
		public string Lead {
			get {
				return this.lead;
			}
		}
		[Category("付随情報")]
		[Description("配信終了日．")]
		public string Limit {
			get {
				return this.limit;
			}
		}
		[Category("URI")]
		[Description("詳細ページのURI．")]
		public Uri DetailPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catedetail/contents_id/" + this.ContentId + "/");
			}
		}
		[Category("URI")]
		[Description("正規にIEで再生する場合のページのURI．")]
		public Uri PlayerPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/movie/"
					+ "contentsId/" + this.ContentId + "/"
					+ "rateId/" + UserSettings.Instance.GyaoBitRateId + "/"
					+ "login_from/shityou/");
			}
		}
		[Category("URI")]
		[Description("プレイリストのURI．")]
		public Uri PlayListUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/asx.php?"
					+ "contentsId=" + this.ContentId
					+ "&userNo=" + UserSettings.Instance.GyaoUserNo.ToString()
					+ "rateId=" + UserSettings.Instance.GyaoBitRateId);
			}
		}
		[Category("URI")]
		[Description("メディアファイルのURI．")]
		public Uri MediaFileUri {
			get {
				return new Uri("rtsp://wms.cd.gyao.jp/gyaovod01?QueryString="
					+ "contentsId=" + this.ContentId
					+ ":userNo=" + UserSettings.Instance.GyaoUserNo.ToString()
					+ ":rateId=" + UserSettings.Instance.GyaoBitRateId);
			}
		}
		[Category("URI")]
		[Description("画像 (大) のURI．")]
		public Uri ImageLargeUri {
			get {
				return new Uri("http://www.gyao.jp/img/info/"
					+ this.Genre.DirectoryName + "/"
					+ this.ContentId + "_l.jpg");
			}
		}
		[Category("URI")]
		[Description("画像 (小) のURI．")]
		public Uri ImageSmallUri {
			get {
				return new Uri("http://www.gyao.jp/img/info/"
					+ this.Genre.DirectoryName + "/"
					+ this.ContentId + "_s.jpg");
			}
		}
		public override string ToString() {
			return "<" + this.ContentId + "> " + this.ContentName
				+ " [" + this.Package.ToString() + "]";
		}
	}
}

