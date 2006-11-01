using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using System.Drawing;
using Yusen.GExplorer.OldCrawler;

namespace Yusen.GExplorer.OldApp {
	public class ContentAdapter : IEquatable<ContentAdapter>{
		public sealed class UnknownGenre : GGenre {
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
				get {return false;}
			}
		}

		public const string PropertyNameContentId = "ContentId";
		public const string PropertyNamePackageId = "PackageId";
		public const string PropertyNameTitle = "Title";
		
		internal static string GetNames(IEnumerable<ContentAdapter> conts) {
			StringBuilder sb = new StringBuilder();
			foreach (ContentAdapter cont in conts) {
				if (sb.Length > 0) {
					sb.Append(Environment.NewLine);
				}
				sb.Append(cont.DisplayName);
			}
			return sb.ToString();
		}
		internal static string GetUris(IEnumerable<ContentAdapter> conts) {
			StringBuilder sb = new StringBuilder();
			foreach (ContentAdapter cont in conts) {
				if (sb.Length > 0) {
					sb.Append(Environment.NewLine);
				}
				sb.Append(cont.DetailPageUri.AbsoluteUri);
			}
			return sb.ToString();
		}
		internal static string GetNamesAndUris(IEnumerable<ContentAdapter> conts) {
			StringBuilder sb = new StringBuilder();
			foreach (ContentAdapter cont in conts) {
				if (sb.Length > 0) {
					sb.Append(Environment.NewLine);
				}
				sb.Append(cont.DisplayName);
				sb.Append(Environment.NewLine);
				sb.Append(cont.DetailPageUri.AbsoluteUri);
			}
			return sb.ToString();
		}
		internal static string GetPropertyValueLines(IEnumerable<ContentAdapter> conts, PropertyInfo pi) {
			StringBuilder sb = new StringBuilder();
			foreach (ContentAdapter cont in conts) {
				if (sb.Length > 0) {
					sb.Append(Environment.NewLine);
				}
				sb.Append(pi.GetValue(cont, null).ToString());
			}
			return sb.ToString();
		}
		internal static void TotalTimeOf(IEnumerable<ContentAdapter> conts, out TimeSpan total, out bool isExact) {
			total = TimeSpan.Zero;
			isExact = true;
			foreach (ContentAdapter cont in conts) {
				if (cont.GTimeSpan.HasTimeSpan) {
					total += cont.GTimeSpan.TimeSpan;
				} else {
					isExact = false;
				}
			}
		}
		private static bool EqualsHelper(ContentAdapter cont1, ContentAdapter cont2) {
			return cont1.ContentKey.Equals(cont2.ContentKey);
		}

		private GContent innerCont;
		private GTimeSpan gTimeSpan;
		private GDeadline gDeadline;
		private GGenre genre = null;
		private string mergedDescription = null;
		private string comment = string.Empty;
		private string displayName = null;
		private string attributes = null;
		
		public ContentAdapter() {
		}
		public ContentAdapter(GContent innerCont) {
			this.InnerContent = innerCont;
		}

		private string CreateDisplayName() {
			StringBuilder sb = new StringBuilder();
			sb.Append("["+ this.GenreName + "]");
			sb.Append(" " + this.Title);
			if (!string.IsNullOrEmpty(this.SeriesNumber) && !this.SeriesNumber.Equals(this.Title)) {
				sb.Append(" / " + this.SeriesNumber);
			}
			if (!string.IsNullOrEmpty(this.Subtitle) && !this.Subtitle.Equals(this.Title) && !this.Subtitle.Equals(this.SeriesNumber)) {
				sb.Append(" / " + this.Subtitle);
			}
			return sb.ToString();
		}
		private string CreateAttributes() {
			if (this.IsDummy) {
				return "D";
			} else if (this.IsNew) {
				return "N";
			} else {
				return string.Empty;
			}
		}

		[Browsable(false)]
		public GContent InnerContent {
			get {
				return this.innerCont;
			}
			set {
				this.innerCont = value;
				this.gTimeSpan = new GTimeSpan(this.innerCont.Duration);
				this.gDeadline = new GDeadline(this.innerCont.Deadline);
			}
		}
		[XmlIgnore]
		[Browsable(false)]
		internal GGenre Genre {
			get {
				if (null == this.genre) {
					GGenre genre;
					if (GGenre.TryGetGerneByKey(this.GenreKey, out genre)) {
						this.genre = genre;
					} else {
						this.genre = UnknownGenre.Default;
					}
				}
				return this.genre;
			}
		}
		
		[Category("ユーザが入力する情報")]
		[Description("コメント．ユーザが自由に入力できる．ただしプレイリストに入っているものに対して入力しないとほとんど意味ない．")]
		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		
		[XmlIgnore]
		[Category("キー")]
		[Description("コンテンツのキー．")]
		public int ContentKey {
			get { return this.innerCont.ContentKey; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("コンテンツのID．")]
		public string ContentId {
			get { return this.innerCont.ContentId; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("パッケージのキー．")]
		public int PackageKey {
			get { return this.innerCont.PackageKey; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("パッケージのID．")]
		public string PackageId {
			get { return this.innerCont.PackageId; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("ジャンルのキー．")]
		public int GenreKey {
			get { return this.innerCont.GenreKey; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("ジャンルのID．")]
		public string GenreId {
			get { return this.innerCont.GenreId; }
		}
		
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("タイトル．")]
		public string Title {
			get { return this.innerCont.Title; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("シリーズ番号．")]
		public string SeriesNumber {
			get { return this.innerCont.SeriesNumber; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("サブタイトル．")]
		public string Subtitle {
			get { return this.innerCont.Subtitle; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("番組時間 (CM時間を除く)．")]
		public string Duration {
			get { return this.innerCont.Duration; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("説明記述1．")]
		public string Description1 {
			get { return this.innerCont.Description1; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("説明記述2．")]
		public string Description2{
			get { return this.innerCont.Description2; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("説明記述3．")]
		public string Description3 {
			get { return this.innerCont.Description3; }
		}
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("説明記述4．")]
		public string Description4 {
			get { return this.innerCont.Description4; }
		}
		[XmlIgnore]
		[Category("付随情報P")]
		[Description("サマリー．")]
		public string Summary {
			get { return this.innerCont.Summary; }
		}
		[XmlIgnore]
		[Category("付随情報P")]
		[Description("配信期限．")]
		public string Deadline {
			get { return this.innerCont.Deadline; }
		}

		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("ジャンル名．")]
		public string GenreName {
			get {
				return this.Genre.GenreName;
			}
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("結合した説明記述．")]
		public string MergedDescription {
			get {
				if (null == this.mergedDescription) {
					List<string> descs = new List<string>();
					descs.Add(this.Description1.Trim());
					descs.Add(this.Description2.Trim());
					descs.Add(this.Description3.Trim());
					descs.Add(this.Description4.Trim());
					List<string> desc2 = new List<string>();
					foreach (string desc in descs) {
						if (!string.IsNullOrEmpty(desc)) desc2.Add(desc);
					}
					StringBuilder sb = new StringBuilder();
					foreach (string desc in desc2) {
						if (sb.Length > 0) {
							sb.Append("\n\n");
						}
						sb.Append(desc);
					}
					this.mergedDescription = sb.ToString();
				}
				return this.mergedDescription;
			}
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("Trueの場合はキャッシュから読まれたことを示す．")]
		public bool FromCache {
			get { return this.innerCont.FromCache; }
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("クロール前にはキャッシュになく，尚且つダミーでないコンテンツは新着と判断される．")]
		public bool IsNew {
			get {return !this.innerCont.FromCache && !this.innerCont.IsDummy; }
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("ジャンル名やタイトルを適当に組み合わせた表示名．")]
		public string DisplayName {
			get {
				if (null == this.displayName) {
					this.displayName = this.CreateDisplayName();
				}
				return this.displayName;
			}
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("時間のパーズ結果．")]
		public GTimeSpan GTimeSpan {
			get {
				return this.gTimeSpan;
			}
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("配信期限のパーズ結果．")]
		public GDeadline GDeadline {
			get {
				return this.gDeadline;
			}
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("ダミーかどうかのフラグ．")]
		public bool IsDummy {
			get { return this.InnerContent.IsDummy; }
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("属性文字列．")]
		public string Attributes{
			get {
				if (null == this.attributes) {
					this.attributes = this.CreateAttributes();
				}
				return this.attributes;
			}
		}
		
		[XmlIgnore]
		[Category("URI")]
		[Description("詳細ページのURI．")]
		public Uri DetailPageUri {
			get { return this.innerCont.DetailPageUri; }
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("IEで正規に再生する場合のURI．(グローバル設定のビットレートの影響を受ける．)")]
		public Uri PlayerPageUri {
			get {
				return GContent.CreatePlayerPageUri(this.ContentKey, GlobalSettings.Instance.BitRate);
			}
		}

		[XmlIgnore]
		[Category("URI")]
		[Description("コンテンツの画像(大)のURI．")]
		public Uri ImageLargeUri {
			get {
				return GContent.CreateImageUri(this.ContentKey, this.Genre.ImageDirName, 'l');
			}
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("コンテンツの画像(小)のURI．")]
		public Uri ImageSmallUri {
			get {
				return GContent.CreateImageUri(this.ContentKey, this.Genre.ImageDirName, 's');
			}
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("お勧め番組の案内ページのURI． (グローバル設定のビットレートの影響を受ける．)")]
		public Uri RecommendPageUri {
			get {
				return GContent.CreateRecommendPageUri(this.ContentKey, GlobalSettings.Instance.BitRate);
			}
		}
		
		public override bool Equals(object obj) {
			ContentAdapter cObj = obj as ContentAdapter;
			if (null == cObj) {
				return false;
			}
			return ContentAdapter.EqualsHelper(this, cObj);
		}
		public bool Equals(ContentAdapter other) {
			if(null == other) {
				return false;
			}
			return ContentAdapter.EqualsHelper(this, other);
		}
		
		public override int GetHashCode() {
			return this.ContentKey.GetHashCode();
		}
		public override string ToString() {
			return "<" + this.ContentId + "> " + this.DisplayName;
		}
	}

	public sealed class ContentSelectionChangedEventArgs : EventArgs {
		private readonly ContentAdapter content;
		private readonly bool isSelected;
		public ContentSelectionChangedEventArgs(ContentAdapter content, bool isSelected) {
			this.content = content;
			this.isSelected = isSelected;
		}
		public ContentAdapter Content {
			get { return this.content; }
		}
		public bool IsSelected {
			get { return this.isSelected; }
		}
	}

	public sealed class ContentSelectionRequiredEventArgs : EventArgs {
		private IEnumerable<ContentAdapter> selection;
		public ContentSelectionRequiredEventArgs() {
		}
		public IEnumerable<ContentAdapter> Selection {
			get { return this.selection; }
			set { this.selection = value; }
		}
	}

	public sealed class CAPropertySelectedEventArgs : EventArgs {
		private PropertyInfo pi;
		public CAPropertySelectedEventArgs(PropertyInfo pi) {
			this.pi = pi;
		}
		public PropertyInfo PropertyInfo {
			get { return this.pi; }
		}
	}
}
