using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

/* GyaOのコンテンツDBは以下の木構造になっているはず？
 * ジャンル
 *   └─パッケージ
 *         └─コンテント
 */

namespace Yusen.GExplorer{
	delegate void LoadingPackagesEventHandler(GGenre sender, GPackage package, int nume, int denom);

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
		
		public string GenreName {
			get {
				return this.name;
			}
		}
		public string DirectoryName {
			get {
				return this.dir;
			}
		}
		public Uri GenreTopPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catetop/genre_id/" + this.GenreId + "/");
			}
		}
		public string GenreId {
			get {
				return "gen" + this.keyNo.ToString("0000000");
			}
		}
		public bool IsLoaded {
			get {
				return null != this.children;
			}
		}
		public IEnumerable<GPackage> Packages {
			get {
				if(! this.IsLoaded) throw new InvalidOperationException();
				return this.children;
			}
		}
		public DateTime LastFetchTime{
			get{
				return this.lastFetch;
			}
		}
		public override string ToString() {
			return this.GenreName;
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
				if(!line.StartsWith("<!--")) continue;
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
		public void FetchAll() {
			IEnumerable<GPackage> ps = this.FetchPackages();
			int denom = 0;
			foreach(GPackage p in ps) denom++;
			int nume = 0;
			foreach(GPackage p in this.FetchPackages()) {
				p.FetchContents();
				GGenre.LoadingPackages(this, p, ++nume, denom);
			}
		}
	}
	
	/// <summary>
	/// GyaOにおける pac．「全シリーズを見る」のボタンを押したときの飛び先．
	/// 「全シリーズを見る」のボタンがなくても，各コンテンツは何らかのパックに所属している模様．
	/// </summary>
	class GPackage{
		private static readonly Regex regexPCatch =
			new Regex("^\t\t" + @"<td class=""text12b""><b>(.*?)<!-- パックキャッチコピー -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexPText =
			new Regex("^\t\t" + @"<td>(.*?)<!-- パックテキスト１ -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexSnum =
			new Regex("^\t\t\t\t" + @"<td align=""left"" class=""title12"">(.*?)<!-- ストーリー番号 -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexLimit =
			new Regex("^\t\t\t\t\t\t\t\t\t\t\t\t" + "(.*正午まで)", RegexOptions.Compiled | RegexOptions.Singleline);
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
		
		public GGenre Genre {
			get {
				return this.parent;
			}
		}
		public string PackageName {
			get {
				return this.name;
			}
		}
		public Uri PackagePageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catelist/pac_id/" + this.PackageId + "/");
			}
		}
		public string PackageId {
			get {
				return "pac" + this.keyNo.ToString("0000000");
			}
		}
		public string SubgenreName {
			get {
				return this.subgenreName;
			}
		}
		public string SubgenreCatch {
			get {
				return this.subgenreCatch;
			}
		}
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
		public Uri SpecialPageUri {
			get {
				if(null == this.specialPage) throw new InvalidOperationException();
				return this.specialPage;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				if(null != this.specialPage) throw new InvalidOperationException();
				this.specialPage = value;
			}
		}
		public bool HasSpecialPage {
			get {
				return null != this.specialPage;
			}
		}
		public bool IsLoaded {
			get {
				return null != this.children;
			}
		}
		public IEnumerable<GContent> Contents {
			get {
				if(! this.IsLoaded) throw new InvalidOperationException();
				return this.children;
			}
		}
		public DateTime LastFetchTime {
			get {
				return this.lastFetch;
			}
		}
		public override string ToString() {
			return "<" + this.PackageId + "> " + this.PackageName + " [" + this.SubgenreName + "]";
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
		
		public GPackage Package {
			get {
				return this.parent;
			}
		}
		public GGenre Genre {
			get {
				return this.Package.Genre;
			}
		}
		public string ContentName {
			get {
				return this.name;
			}
		}
		public string ContentId {
			get {
				return "cnt" + this.keyNo.ToString("0000000");
			}
		}
		public bool IsNew {
			get {
				return this.isNew;
			}
		}
		public string Lead {
			get {
				return this.lead;
			}
		}
		public string Limit {
			get {
				return this.limit;
			}
		}
		public Uri DetailPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catedetail/contents_id/" + this.ContentId + "/");
			}
		}
		public Uri PlayerPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/movie/"
					+ "contentsId/" + this.ContentId + "/"
					+ "rateId/" + GBitRate.Default.RateId + "/"
					+ "login_from/shityou/");
			}
		}
		public Uri PlayListUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/asx.php?"
					+ "contentsId=" + this.ContentId
					+ "&userNo=" + GCookie.UserNo.ToString()
					+ "rateId=" + GBitRate.Default.RateId);
			}
		}
		public Uri MediaFileUri {
			get {
				return new Uri("rtsp://wms.cd.gyao.jp/gyaovod01?QueryString="
					+ "contentsId=" + this.ContentId
					+ ":userNo=" + GCookie.UserNo.ToString()
					+ ":rateId=" + GBitRate.Default.RateId);
			}
		}
		public override string ToString() {
			return "<" + this.ContentId + "> " + this.ContentName
				+ " <" + this.Package.PackageId + ">" + this.Package.PackageName;
		}
	}
	
	/// <summary>ビットレートのタイプセイフイニューム．</summary>
	class GBitRate {
		private readonly static GBitRate high;
		private readonly static GBitRate low;
		private static GBitRate defa;
		
		static GBitRate() {
			GBitRate.high = new GBitRate(2);
			GBitRate.low = new GBitRate(1);
			GBitRate.defa = GBitRate.high;
		}
		public static GBitRate Default {
			get {
				return GBitRate.defa;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				GBitRate.defa = value;
			}
		}
		public static GBitRate High {
			get {
				return GBitRate.high;
			}
		}
		public static GBitRate Low {
			get {
				return GBitRate.low;
			}
		}
		
		private int keyNo;
		
		private GBitRate(int keyNo) {
			this.keyNo = keyNo;
		}
		
		public string RateId {
			get {
				return "bit" + this.keyNo.ToString("0000000");
			}
		}
	}
}
