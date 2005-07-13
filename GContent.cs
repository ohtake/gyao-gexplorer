using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WebClient = System.Net.WebClient;
using Encoding = System.Text.Encoding;

/* GyaOのコンテンツDBは以下の木構造になっているはず？
 * ジャンル
 *   └─パッケージ
 *         └─コンテンツ
 */

namespace Yusen.GExplorer{
	delegate void LoadingPackagesEventHandler(GGenre sender, int nume, int denom);

	/// <summary>GyaOにおける genre．映画，ドラマなど．</summary>
	abstract class GGenre{
		/// <summary>2005-05形式のジャンルページ</summary>
		class GGenre200505 : GGenre{
			private static readonly Regex regexPacId =
				new Regex(@"^<img src=""/img/info/[^/]+/pac([0-9]+)_m\.jpg""", RegexOptions.Compiled | RegexOptions.Singleline);
			private static readonly Regex regexSpecial =
				new Regex(@"<a href=""([^""]+)"".+?alt=""特集ページ""", RegexOptions.Compiled | RegexOptions.Singleline);
			public GGenre200505(int keyNo, string name, string dir)
				: base(keyNo, name, dir) {
			}

			public override Uri GenreTopPageUri {
				get {
					return new Uri("http://www.gyao.jp/sityou/catetop/genre_id/" + this.GenreId + "/");
				}
			}
			public override bool CanBeExplorerable {
				get {
					return true;
				}
			}

			public override IEnumerable<GPackage> FetchPackages() {
				List<GPackage> packages = new List<GPackage>();
				TextReader reader = new StreamReader(new WebClient().OpenRead(this.GenreTopPageUri), Encoding.GetEncoding("Shift_JIS"));

				string line;
				Queue<string> sgName = new Queue<string>();
				Queue<string> sgCatch = new Queue<string>();
				string packName = "";
				List<string> specialPages = new List<string>();
				List<bool> comings = new List<bool>();
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
							match = GGenre200505.regexPacId.Match(Utility.ReadNextLineHtml(reader));
							if(match.Success) {
								packages.Add(new GPackage(
									this,
									int.Parse(match.Groups[1].Value),
									packName,
									0 == sgName.Count ? "(バグったかも)" : sgName.Dequeue(),
									0 == sgCatch.Count ? "(バグったかも)" : sgCatch.Dequeue()));
							}
							break;
						case "<!-- ボタン ↓ -->":
							line = Utility.ReadNextLineHtml(reader);
							match = GGenre200505.regexSpecial.Match(line);
							if(match.Success) {
								specialPages.Add(match.Groups[1].Value);
							} else {
								specialPages.Add(null);
							}
							comings.Add(line.Contains("もうすぐ登場"));
							break;
					}
				}
				for(int i = 0; i < specialPages.Count; i++) {
					string sp = specialPages[i];
					if(null != sp) {
						packages[i].SpecialPageUri = new Uri(this.GenreTopPageUri, sp);
					}
				}
				for(int i = 0; i < comings.Count; i++) {
					if(comings[i]) packages[i].IsComingSoon = true;
				}
				this.children = packages;
				this.lastFetch = DateTime.Now;
				return this.Packages;
			}
		}
		/// <summary>2005-07以降での映画，音楽，アニメのジャンル．</summary>
		class GGenre200507 : GGenre {
			public GGenre200507(int keyNo, string name, string dir)
				: base(keyNo, name, dir) {
			}
			public override Uri GenreTopPageUri {
				get {
					return new Uri("http://www.gyao.jp/" + base.DirectoryName + "/");
				}
			}
			public override bool CanBeExplorerable {
				get {
					return false;
				}
			}
			public override IEnumerable<GPackage> FetchPackages() {
				throw new Exception("The method or operation is not implemented.");
			}
		}
		/// <summary>2005-07ではあるがアニメはパッケージのIDが取得できるっぽい</summary>
		sealed class GGenre200507Anime : GGenre200507 {
			private static readonly Regex regexTtl =
				new Regex(@"<img src=""images/ttl_([a-z]+(?:_on)?)\.gif"" alt=""(.+?)""", RegexOptions.Compiled | RegexOptions.Singleline);
			private static readonly Regex regexWeeklyPackage = 
				new Regex(@"<img src=""http://www.gyao.jp/img/info/anime/pac([0-9]+)_m\.jpg"" alt=""(.+?)""", RegexOptions.Compiled | RegexOptions.Singleline);
			private static readonly Regex regexRePackName =
				new Regex(@"<td colspan=""2"" bgcolor=""#757575"" class=""font13white"">　<a name=""[^""]+"">(.+?)</a>", RegexOptions.Compiled | RegexOptions.Singleline);
			private static readonly Regex regexRePackId =
				new Regex(@"<a href=""http://www.gyao.jp/sityou/catelist/pac_id/pac([0-9]+)/""><img src=""images/btn_series.gif""", RegexOptions.Compiled | RegexOptions.Singleline);
			
			public GGenre200507Anime(int keyNo, string name, string dir)
				: base(keyNo, name, dir) {
			}
			public override bool CanBeExplorerable {
				get {
					return true;
				}
			}
			public override IEnumerable<GPackage> FetchPackages() {
				List<GPackage> packages = new List<GPackage>();
				TextReader reader = new StreamReader(new WebClient().OpenRead(this.GenreTopPageUri), Encoding.GetEncoding("Shift_JIS"));
				
				string line;
				string subgenreName = "";
				string subgenreCatch = "";
				string packName = "";
				string packIdStr = "";
				while(null != (line = reader.ReadLine())) {
					Match match;
					match = GGenre200507Anime.regexTtl.Match(line);
					if(match.Success) {
						subgenreName = match.Groups[2].Value;
						subgenreCatch = match.Groups[1].Value;
						continue;
					}
					match = GGenre200507Anime.regexWeeklyPackage.Match(line);
					if(match.Success) {
						packIdStr = match.Groups[1].Value;
						packName = match.Groups[2].Value;
						goto AddToPackages;
					}
					match = GGenre200507Anime.regexRePackName.Match(line);
					if(match.Success) {
						packName = match.Groups[1].Value;
						continue;
					}
					match = GGenre200507Anime.regexRePackId.Match(line);
					if(match.Success) {
						packIdStr = match.Groups[1].Value;
						goto AddToPackages;
					}
					continue;
				AddToPackages:
					packages.Add(new GPackage(
						this, int.Parse(packIdStr), packName, subgenreName, subgenreCatch));
					continue;
				}
				this.children = packages;
				this.lastFetch = DateTime.Now;
				return this.Packages;
			}
		}
		
		public static event LoadingPackagesEventHandler LoadingPackages;
		
		private static readonly GGenre[] allGenres =
			new GGenre[]{
				new GGenre200507( 1, "映画", "cinema"),
				new GGenre200507( 3, "音楽", "music"),
				new GGenre200505( 2, "ドラマ", "dorama"),
				new GGenre200507Anime( 6, "アニメ", "anime"),
				new GGenre200505( 4, "アイドル・グラビア", "idol"),
				new GGenre200505( 5, "バラエティ", "variety"),
				new GGenre200505(10, "ドキュメンタリー", "documentary"),
				new GGenre200505(15, "ライフ", "life"),
				new GGenre200505( 9, "スポーツ", "sports"),
				new GGenre200505(16, "ビジネス", "business"),
				new GGenre200507( 7, "ニュース", "news"),
				new GGenre200507(12, "映像ブログ", "videoblog"),
			};
		
		public static IEnumerable<GGenre> AllGenres {
			get {
				return GGenre.allGenres;
			}
		}

		protected readonly int keyNo;
		protected readonly string name;
		protected readonly string dir;
		protected IEnumerable<GPackage> children = null;
		protected DateTime lastFetch = default(DateTime);
		
		protected GGenre(int keyNo, string name, string dir) {
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
		abstract public Uri GenreTopPageUri {
			get;
		}
		[Category("専ブラが付加した情報")]
		[Description("読み込み済みであるか否か．")]
		public bool HasLoaded {
			get {
				return null != this.children;
			}
		}
		[Browsable(false)]
		public IEnumerable<GPackage> Packages {
			get {
				//if(! this.HasLoaded) throw new InvalidOperationException();
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
		[Category("専ブラが付加した情報")]
		[Description("GExplorerで読めるか否か．")]
		abstract public bool CanBeExplorerable {
			get;
		}
		public override string ToString() {
			return "<" + this.GenreId + "," + this.DirectoryName + "> " + this.GenreName;
		}
		
		abstract public IEnumerable<GPackage> FetchPackages();
		
		public void FetchAll(){
			if(UserSettings.Instance.GyaoEnableConcurrentFetch) {
				try {
					this.FetchAllConcurrently();
				} catch(Exception e) {
					Utility.DisplayException(e);
				}
			} else {
				try {
					this.FetchAllSequentially();
				} catch(Exception e) {
					Utility.DisplayException(e);
				}
			}
		}
		private void FetchAllSequentially() {
			IEnumerable<GPackage> ps = this.FetchPackages();
			int denom = 1;
			foreach(GPackage p in ps) denom++;
			int nume = 0;
			foreach(GPackage p in this.FetchPackages()) {
				nume++;
				GGenre.LoadingPackages(this, nume, denom);
				try {
					p.FetchContents();
				} catch(Exception e) {
					Utility.DisplayException(e);
				}
			}
			GGenre.LoadingPackages(this, denom, denom);
		}
		private void FetchAllConcurrently() {
			bool hadError = false;
			
			IEnumerable<GPackage> ps = this.FetchPackages();
			int denom = 1;
			Queue<Thread> tq = new Queue<Thread>();
			foreach(GPackage p in ps) {
				Thread t = new Thread(new ThreadStart(delegate() {
					try {
						p.FetchContents();
					} catch(Exception e) {
						lock(this) {
							hadError = true;
						}
						Utility.DisplayException(e);
					}
				}));
				t.Start();
				denom++;
				tq.Enqueue(t);
			}
			int nume = 0;
			foreach(Thread t in tq) {
				nume++;
				GGenre.LoadingPackages(this, nume, denom);
				t.Join();
			}
			GGenre.LoadingPackages(this, denom, denom);
			
			if(hadError) {
				if(DialogResult.Yes == MessageBox.Show(
						"ページの並行取得でエラーが起きました．\"偶発的\"なエラーが頻発するようならば従来の逐次取得に切り替えると改善するかもしれません．\n\n"
						+ "逐次取得に切り替えますか？(手動で切り替えるにはユーザ設定で変更出来ます．)",
						Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
					UserSettings.Instance.GyaoEnableConcurrentFetch = false;
				}
			}
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
		
		public static GPackage CreateDummyPackage(int pacId) {
			return new GPackage(null, pacId, "", "", "");
		}
		
		private readonly GGenre parent;
		private readonly int keyNo;
		private readonly string name;
		private readonly string subgenreName;
		private readonly string subgenreCatch;
		private string pacCatch = null;
		private string pacText = null;
		private Uri specialPage = null;
		private bool comingSoon = false;
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
				//if(null == this.pacCatch) throw new InvalidOperationException();
				return this.pacCatch;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				//if(null != this.pacCatch) throw new InvalidOperationException();
				this.pacCatch = value;
			}
		}
		[ReadOnly(true)]
		[Category("付随情報")]
		[Description("パッケージの説明文．")]
		public string PackageText {
			get {
				//if(null == this.pacText) throw new InvalidOperationException();
				return this.pacText;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				//if(null != this.pacText) throw new InvalidOperationException();
				this.pacText = value;
			}
		}
		[ReadOnly(true)]
		[Category("URI")]
		[Description("特集ページのURI．専ブラでは特集ページの解析は行っていないので通常のウェブブラウザで中身を確認する必要あり．")]
		public Uri SpecialPageUri {
			get {
				if(null == this.specialPage){
					//throw new InvalidOperationException();
					return null;
				}
				return this.specialPage;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				//if(null != this.specialPage) throw new InvalidOperationException();
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
		[Category("付随情報")]
		[Description("「もうすぐ登場」の真偽．")]
		[ReadOnly(true)]
		public bool IsComingSoon {
			get {
				return this.comingSoon;
			}
			set {
				this.comingSoon = value;
			}
		}
		[Category("専ブラが付加した情報")]
		[Description("読み込み済みであるか否か．")]
		public bool HasLoaded {
			get {
				return null != this.children;
			}
		}
		[Browsable(false)]
		public IEnumerable<GContent> Contents {
			get {
				//if(! this.HasLoaded) throw new InvalidOperationException();
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
			if(this.IsComingSoon) {//「もうすぐ登場」の時は読み込まないどく (仮)
				this.children = new List<GContent>();
				this.lastFetch = DateTime.MinValue;
				return this.Contents;
			}
			
			TextReader reader = new StreamReader(new WebClient().OpenRead(this.PackagePageUri), Encoding.GetEncoding("Shift_JIS"));
			
			string line;
			while(null != (line = reader.ReadLine())){
				Match match = GPackage.regexPCatch.Match(line);
				if(match.Success) {
					this.PackageCatch = Utility.UnescapeHtmlEntity(match.Groups[1].Value);
					break;
				}
			}
			while(null != (line = reader.ReadLine())) {
				Match match = GPackage.regexPText.Match(line);
				if(match.Success) {
					this.PackageText = Utility.UnescapeHtmlEntity(match.Groups[1].Value);
					break;
				}
			}
			if(null == line) throw new Exception("おそらくパッケージ<" + this.PackageId + ">の中身が空．");
			
			List<GContent> contents = new List<GContent>();
			while(null != line) {
				string snum = "";
				while(null != (line = reader.ReadLine())) {
					Match match = GPackage.regexSnum.Match(line);
					if(match.Success) {
						snum = Utility.UnescapeHtmlEntity(match.Groups[1].Value);
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
						limit = Utility.UnescapeHtmlEntity(match.Groups[1].Value);
						break;
					}
				}
				string story = "";
				while(null != (line = reader.ReadLine())) {
					Match match = GPackage.regexStory.Match(line);
					if(match.Success) {
						story = Utility.UnescapeHtmlEntity(match.Groups[1].Value);
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
		public static GContent CreateDummyContent(int contId) {
			return new GContent(null, contId, "", "", false, "");
		}
		
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
				return new Uri(
					//"http://www.gyao.jp/sityou/movie/"
					"http://www.gyao.jp/login/judge_cookie/"
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
					+ "&rateId=" + UserSettings.Instance.GyaoBitRateId);
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

