using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

/* GyaO�̃R���e���cDB�͈ȉ��̖؍\���ɂȂ��Ă���͂��H
 * �W������
 *   �����p�b�P�[�W
 *         �����R���e���g
 */

namespace Yusen.GExplorer{
	delegate void LoadingPackagesEventHandler(GGenre sender, int nume, int denom);

	/// <summary>GyaO�ɂ����� genre�D�f��C�h���}�ȂǁD</summary>
	class GGenre{
		public static event LoadingPackagesEventHandler LoadingPackages;
		
		private static IEnumerable<GGenre> allGenres =
			new GGenre[]{
				new GGenre( 1, "�f��", "cinema"),
				new GGenre( 2, "�h���}", "dorama"),
				new GGenre( 4, "�A�C�h���E�O���r�A", "idol"),
				new GGenre(10, "�h�L�������^���[", "documentary"),
				new GGenre( 9, "�X�|�[�c", "sports"),
				new GGenre( 3, "���y", "music"),
				new GGenre( 6, "�A�j��", "anime"),
				new GGenre( 5, "�o���G�e�B", "variety"),
				new GGenre(15, "���C�t", "life"),
				new GGenre(16, "�r�W�l�X", "business"),
			};
		private static readonly Regex regexPacId = 
			new Regex(@"^<img src=""/img/info/[^/]+/pac([0-9]+)_m\.jpg""", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex regexSpecial =
			new Regex(@"<a href=""([^""]+)"".+?alt=""���W�y�[�W""", RegexOptions.Compiled | RegexOptions.Singleline);
		
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
		
		[Category("�t�����")]
		[DisplayName("�W��������")]
		[Description("")]
		public string GenreName {
			get {
				return this.name;
			}
		}
		[Category("�L�[")]
		[DisplayName("�f�B���N�g����")]
		[Description("�W�������ɑΉ�����f�B���N�g�����D")]
		public string DirectoryName {
			get {
				return this.dir;
			}
		}
		[Category("URI")]
		[DisplayName("�W�������g�b�v")]
		[Description("")]
		public Uri GenreTopPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catetop/genre_id/" + this.GenreId + "/");
			}
		}
		[Category("�L�[")]
		[DisplayName("genre_id")]
		[Description("")]
		public string GenreId {
			get {
				return "gen" + this.keyNo.ToString("0000000");
			}
		}
		[Category("��u�����t���������")]
		[DisplayName("�ǂݍ��ݍς�")]
		[Description("")]
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
		[Category("��u�����t���������")]
		[DisplayName("�ŏI�ǂݍ��ݓ���")]
		[Description("�J�e�S���g�b�v�y�[�W���u�����Ō�ɓǂݍ��񂾓���")]
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
					case "<!-- �{�^�� �� -->":
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
		[Category("�t�����")]
		[DisplayName("�p�b�P�[�W��")]
		[Description("")]
		public string PackageName {
			get {
				return this.name;
			}
		}
		[Category("URI")]
		[DisplayName("�p�b�P�[�W�y�[�W")]
		[Description("�p�b�P�[�W���̃R���e���c�ꗗ��������y�[�W��URI�D")]
		public Uri PackagePageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catelist/pac_id/" + this.PackageId + "/");
			}
		}
		[Category("�L�[")]
		[DisplayName("pac_id")]
		[Description("")]
		public string PackageId {
			get {
				return "pac" + this.keyNo.ToString("0000000");
			}
		}
		[Category("�t�����")]
		[DisplayName("�T�u�W��������")]
		[Description("")]
		public string SubgenreName {
			get {
				return this.subgenreName;
			}
		}
		[Category("�t�����")]
		[DisplayName("�T�u�W�������̃L���b�`�R�s�[")]
		[Description("")]
		public string SubgenreCatch {
			get {
				return this.subgenreCatch;
			}
		}
		[ReadOnly(true)]
		[Category("�t�����")]
		[DisplayName("�L���b�`�R�s�[")]
		[Description("")]
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
		[Category("�t�����")]
		[DisplayName("������")]
		[Description("")]
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
		[DisplayName("���W�y�[�W")]
		[Description("��u���ł͓��W�y�[�W�̉�͍͂s���Ă��Ȃ��̂Œʏ�̃E�F�u�u���E�U�Œ��g���m�F����K�v����D")]
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
		[Category("��u�����t���������")]
		[DisplayName("���W�y�[�W�̗L��")]
		[Description("")]
		public bool HasSpecialPage {
			get {
				return null != this.specialPage;
			}
		}
		[Category("��u�����t���������")]
		[DisplayName("�ǂݍ��ݍς�")]
		[Description("")]
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
		[Category("��u�����t���������")]
		[DisplayName("�ŏI�擾����")]
		[Description("�p�b�P�[�W�y�[�W���Ō�ɓǂݍ��񂾓���")]
		public DateTime LastFetchTime {
			get {
				return this.lastFetch;
			}
		}
		[Category("URI")]
		[DisplayName("�摜 (��)")]
		[Description("")]
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
	
	/// <summary>GyaO�ɂ����� cont�D</summary>
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
		[Category("�t�����")]
		[DisplayName("�b")]
		[Description("")]
		public string ContentName {
			get {
				return this.name;
			}
		}
		[Category("�L�[")]
		[DisplayName("contsnts_id")]
		[Description("")]
		public string ContentId {
			get {
				return "cnt" + this.keyNo.ToString("0000000");
			}
		}
		[Category("�t�����")]
		[DisplayName("New")]
		[Description("New�}�[�N�����Ă��邩�ۂ��D")]
		public bool IsNew {
			get {
				return this.isNew;
			}
		}
		[Category("�t�����")]
		[DisplayName("���[�h")]
		[Description("�����ƒ�����������ǂ݂�����Ώڍ׃y�[�W��ǂނׂ��D")]
		public string Lead {
			get {
				return this.lead;
			}
		}
		[Category("�t�����")]
		[DisplayName("�z�M�I����")]
		[Description("")]
		public string Limit {
			get {
				return this.limit;
			}
		}
		[Category("URI")]
		[DisplayName("�ڍ׃y�[�W")]
		[Description("catedetail �̃y�[�W�̂��ƁD")]
		public Uri DetailPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/catedetail/contents_id/" + this.ContentId + "/");
			}
		}
		[Category("URI")]
		[DisplayName("�{���̍Đ��y�[�W")]
		[Description("�E�N���b�N�ł��ˁ[�C�S��ʂɏo���ˁ[�C�����[�D�̃y�[�W�̂��ƁD")]
		public Uri PlayerPageUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/movie/"
					+ "contentsId/" + this.ContentId + "/"
					+ "rateId/" + UserSettings.Instance.GyaoBitRateId + "/"
					+ "login_from/shityou/");
			}
		}
		[Category("URI")]
		[DisplayName("�v���C���X�g")]
		[Description("")]
		public Uri PlayListUri {
			get {
				return new Uri("http://www.gyao.jp/sityou/asx.php?"
					+ "contentsId=" + this.ContentId
					+ "&userNo=" + UserSettings.Instance.GyaoUserNo.ToString()
					+ "rateId=" + UserSettings.Instance.GyaoBitRateId);
			}
		}
		[Category("URI")]
		[DisplayName("���f�B�A�t�@�C��")]
		[Description("�v���C���X�g��2���ږځH")]
		public Uri MediaFileUri {
			get {
				return new Uri("rtsp://wms.cd.gyao.jp/gyaovod01?QueryString="
					+ "contentsId=" + this.ContentId
					+ ":userNo=" + UserSettings.Instance.GyaoUserNo.ToString()
					+ ":rateId=" + UserSettings.Instance.GyaoBitRateId);
			}
		}
		[Category("URI")]
		[DisplayName("�摜 (��)")]
		[Description("")]
		public Uri ImageLargeUri {
			get {
				return new Uri("http://www.gyao.jp/img/info/"
					+ this.Genre.DirectoryName + "/"
					+ this.ContentId + "_l.jpg");
			}
		}
		[Category("URI")]
		[DisplayName("�摜 (��)")]
		[Description("")]
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

