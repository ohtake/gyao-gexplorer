using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WebClient = System.Net.WebClient;
using Encoding = System.Text.Encoding;

/* GyaO�̃R���e���cDB�͈ȉ��̖؍\���ɂȂ��Ă���͂��H
 * �W������
 *   �����p�b�P�[�W
 *         �����R���e���c
 */

namespace Yusen.GExplorer{
	delegate void LoadingPackagesEventHandler(GGenre sender, int nume, int denom);

	/// <summary>GyaO�ɂ����� genre�D�f��C�h���}�ȂǁD</summary>
	abstract class GGenre{
		/// <summary>2005-05�`���̃W�������y�[�W</summary>
		class GGenre200505 : GGenre{
			private static readonly Regex regexPacId =
				new Regex(@"^<img src=""/img/info/[^/]+/pac([0-9]+)_m\.jpg""", RegexOptions.Compiled | RegexOptions.Singleline);
			private static readonly Regex regexSpecial =
				new Regex(@"<a href=""([^""]+)"".+?alt=""���W�y�[�W""", RegexOptions.Compiled | RegexOptions.Singleline);
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
					if(!line.StartsWith("<!--")) continue;//���L��switch����M��Ƃ��ɗv����
					Match match;
					switch(line) {
						case "<!--�T�u�W�������� ��-->":
							sgName.Enqueue(Utility.ReadNextLineTextBeforeTag(reader));
							break;
						case "<!--�T�u�W�������L���b�`�R�s�[ ��-->":
							sgCatch.Enqueue(Utility.ReadNextLineTextBeforeTag(reader));
							break;
						case "<!--�p�b�N�� ��-->":
							packName = Utility.ReadNextLineTextBeforeTag(reader);
							break;
						case "<!--�p�b�N�摜�i���j ��-->":
							match = GGenre200505.regexPacId.Match(Utility.ReadNextLineHtml(reader));
							if(match.Success) {
								packages.Add(new GPackage(
									this,
									int.Parse(match.Groups[1].Value),
									packName,
									0 == sgName.Count ? "(�o�O��������)" : sgName.Dequeue(),
									0 == sgCatch.Count ? "(�o�O��������)" : sgCatch.Dequeue()));
							}
							break;
						case "<!-- �{�^�� �� -->":
							line = Utility.ReadNextLineHtml(reader);
							match = GGenre200505.regexSpecial.Match(line);
							if(match.Success) {
								specialPages.Add(match.Groups[1].Value);
							} else {
								specialPages.Add(null);
							}
							comings.Add(line.Contains("���������o��"));
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
		/// <summary>2005-07�ȍ~�ł̉f��C���y�C�A�j���̃W�������D</summary>
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
		/// <summary>2005-07�ł͂��邪�A�j���̓p�b�P�[�W��ID���擾�ł�����ۂ�</summary>
		sealed class GGenre200507Anime : GGenre200507 {
			private static readonly Regex regexTtl =
				new Regex(@"<img src=""images/ttl_([a-z]+(?:_on)?)\.gif"" alt=""(.+?)""", RegexOptions.Compiled | RegexOptions.Singleline);
			private static readonly Regex regexWeeklyPackage = 
				new Regex(@"<img src=""http://www.gyao.jp/img/info/anime/pac([0-9]+)_m\.jpg"" alt=""(.+?)""", RegexOptions.Compiled | RegexOptions.Singleline);
			private static readonly Regex regexRePackName =
				new Regex(@"<td colspan=""2"" bgcolor=""#757575"" class=""font13white"">�@<a name=""[^""]+"">(.+?)</a>", RegexOptions.Compiled | RegexOptions.Singleline);
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
				new GGenre200507( 1, "�f��", "cinema"),
				new GGenre200507( 3, "���y", "music"),
				new GGenre200505( 2, "�h���}", "dorama"),
				new GGenre200507Anime( 6, "�A�j��", "anime"),
				new GGenre200505( 4, "�A�C�h���E�O���r�A", "idol"),
				new GGenre200505( 5, "�o���G�e�B", "variety"),
				new GGenre200505(10, "�h�L�������^���[", "documentary"),
				new GGenre200505(15, "���C�t", "life"),
				new GGenre200505( 9, "�X�|�[�c", "sports"),
				new GGenre200505(16, "�r�W�l�X", "business"),
				new GGenre200507( 7, "�j���[�X", "news"),
				new GGenre200507(12, "�f���u���O", "videoblog"),
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
		
		[Category("�t�����")]
		[Description("�W���������D")]
		public string GenreName {
			get {
				return this.name;
			}
		}
		[Category("�L�[")]
		[Description("�W�������ɑΉ�����f�B���N�g�����D")]
		public string DirectoryName {
			get {
				return this.dir;
			}
		}
		[Category("�L�[")]
		[Description("genre_id")]
		public string GenreId {
			get {
				return "gen" + this.keyNo.ToString("0000000");
			}
		}
		[Category("URI")]
		[Description("�W�������g�b�v�y�[�W��URI�D")]
		abstract public Uri GenreTopPageUri {
			get;
		}
		[Category("��u�����t���������")]
		[Description("�ǂݍ��ݍς݂ł��邩�ۂ��D")]
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
		[Category("��u�����t���������")]
		[Description("�J�e�S���g�b�v�y�[�W���u�����Ō�ɓǂݍ��񂾓����D")]
		public DateTime LastFetchTime{
			get{
				return this.lastFetch;
			}
		}
		[Category("��u�����t���������")]
		[Description("GExplorer�œǂ߂邩�ۂ��D")]
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
						"�y�[�W�̕��s�擾�ŃG���[���N���܂����D\"�����I\"�ȃG���[���p������悤�Ȃ�Ώ]���̒����擾�ɐ؂�ւ���Ɖ��P���邩������܂���D\n\n"
						+ "�����擾�ɐ؂�ւ��܂����H(�蓮�Ő؂�ւ���ɂ̓��[�U�ݒ�ŕύX�o���܂��D)",
						Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
					UserSettings.Instance.GyaoEnableConcurrentFetch = false;
				}
			}
		}
	}
	
	/// <summary>
	/// GyaO�ɂ����� pac�D�u�S�V���[�Y������v�̃{�^�����������Ƃ��̔�ѐ�D
	/// �u�S�V���[�Y������v�̃{�^�����Ȃ��Ă��C�e�R���e���c�͉��炩�̃p�b�N�ɏ������Ă���͗l�D
	/// </summary>
	class GPackage {
		private static readonly Regex regexPCatch =
			new Regex("^\t\t" + @"<td class=""text12b""><b>(.*?)<!-- �p�b�N�L���b�`�R�s�[ -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexPText =
			new Regex("^\t\t" + @"<td>(.*?)<!-- �p�b�N�e�L�X�g�P -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexSnum =
			new Regex("^\t\t\t\t" + @"<td align=""left"" class=""title12"">(.*?)<!-- �X�g�[���[�ԍ� -->", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexLimit =
			new Regex("^\t\t\t\t\t\t\t\t\t\t\t\t" + "(?:<[^>]*>)*(.*���߂܂�)", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexStory =
			new Regex("^\t\t\t\t\t\t\t" + @"<td valign=""top"" class=""text10"">(.*?)<!-- �X�g�[���[ -->", RegexOptions.Compiled | RegexOptions.Singleline);
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
		[Category("�t�����")]
		[Description("�T�u�W���������D")]
		public string SubgenreName {
			get {
				return this.subgenreName;
			}
		}
		[Category("�t�����")]
		[Description("�T�u�W�������̃L���b�`�R�s�[")]
		public string SubgenreCatch {
			get {
				return this.subgenreCatch;
			}
		}
		[Category("�t�����")]
		[Description("�p�b�P�[�W���D")]
		public string PackageName {
			get {
				return this.name;
			}
		}
		[Category("URI")]
		[Description("�p�b�P�[�W���̃R���e���c�ꗗ��������y�[�W��URI�D")]
		public Uri PackagePageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catelist/pac_id/" + this.PackageId + "/");
			}
		}
		[Category("�L�[")]
		[Description("pac_id")]
		public string PackageId {
			get {
				return "pac" + this.keyNo.ToString("0000000");
			}
		}
		[ReadOnly(true)]
		[Category("�t�����")]
		[Description("�p�b�P�[�W�̃L���b�`�R�s�[�D")]
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
		[Category("�t�����")]
		[Description("�p�b�P�[�W�̐������D")]
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
		[Description("���W�y�[�W��URI�D��u���ł͓��W�y�[�W�̉�͍͂s���Ă��Ȃ��̂Œʏ�̃E�F�u�u���E�U�Œ��g���m�F����K�v����D")]
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
		[Category("��u�����t���������")]
		[Description("���W�y�[�W�̗L���D")]
		public bool HasSpecialPage {
			get {
				return null != this.specialPage;
			}
		}
		[Category("�t�����")]
		[Description("�u���������o��v�̐^�U�D")]
		[ReadOnly(true)]
		public bool IsComingSoon {
			get {
				return this.comingSoon;
			}
			set {
				this.comingSoon = value;
			}
		}
		[Category("��u�����t���������")]
		[Description("�ǂݍ��ݍς݂ł��邩�ۂ��D")]
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
		[Category("��u�����t���������")]
		[Description("�p�b�P�[�W�y�[�W���Ō�ɓǂݍ��񂾓����D")]
		public DateTime LastFetchTime {
			get {
				return this.lastFetch;
			}
		}
		[Category("URI")]
		[Description("�摜 (��) ��URI�D")]
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
			if(this.IsComingSoon) {//�u���������o��v�̎��͓ǂݍ��܂Ȃ��ǂ� (��)
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
			if(null == line) throw new Exception("�����炭�p�b�P�[�W<" + this.PackageId + ">�̒��g����D");
			
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
	
	/// <summary>GyaO�ɂ����� cont�D</summary>
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
		[Category("�t�����")]
		[Description("�R���e���c�� (�b)�D")]
		public string ContentName {
			get {
				return this.name;
			}
		}
		[Category("�L�[")]
		[Description("contents_id")]
		public string ContentId {
			get {
				return "cnt" + this.keyNo.ToString("0000000");
			}
		}
		[Category("�t�����")]
		[Description("New�}�[�N�����Ă��邩�ۂ��D")]
		public bool IsNew {
			get {
				return this.isNew;
			}
		}
		[Category("�t�����")]
		[Description("���[�h�D�����ƒ�����������ǂ݂�����Ώڍ׃y�[�W��ǂނׂ��D")]
		public string Lead {
			get {
				return this.lead;
			}
		}
		[Category("�t�����")]
		[Description("�z�M�I�����D")]
		public string Limit {
			get {
				return this.limit;
			}
		}
		[Category("URI")]
		[Description("�ڍ׃y�[�W��URI�D")]
		public Uri DetailPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catedetail/contents_id/" + this.ContentId + "/");
			}
		}
		[Category("URI")]
		[Description("���K��IE�ōĐ�����ꍇ�̃y�[�W��URI�D")]
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
		[Description("�v���C���X�g��URI�D")]
		public Uri PlayListUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/asx.php?"
					+ "contentsId=" + this.ContentId
					+ "&userNo=" + UserSettings.Instance.GyaoUserNo.ToString()
					+ "&rateId=" + UserSettings.Instance.GyaoBitRateId);
			}
		}
		[Category("URI")]
		[Description("���f�B�A�t�@�C����URI�D")]
		public Uri MediaFileUri {
			get {
				return new Uri("rtsp://wms.cd.gyao.jp/gyaovod01?QueryString="
					+ "contentsId=" + this.ContentId
					+ ":userNo=" + UserSettings.Instance.GyaoUserNo.ToString()
					+ ":rateId=" + UserSettings.Instance.GyaoBitRateId);
			}
		}
		[Category("URI")]
		[Description("�摜 (��) ��URI�D")]
		public Uri ImageLargeUri {
			get {
				return new Uri("http://www.gyao.jp/img/info/"
					+ this.Genre.DirectoryName + "/"
					+ this.ContentId + "_l.jpg");
			}
		}
		[Category("URI")]
		[Description("�摜 (��) ��URI�D")]
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

