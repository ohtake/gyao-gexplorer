using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using Yusen.GCrawler;
using Clipboard=System.Windows.Forms.Clipboard;

namespace Yusen.GExplorer {
	public class ContentAdapter : IEquatable<ContentAdapter>{
		internal static void CopyNames(IEnumerable<ContentAdapter> conts) {
			StringBuilder sb = new StringBuilder();
			foreach (ContentAdapter cont in conts) {
				if (sb.Length > 0) {
					sb.Append(Environment.NewLine);
				}
				sb.Append(cont.DisplayName);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		internal static void CopyUris(IEnumerable<ContentAdapter> conts) {
			StringBuilder sb = new StringBuilder();
			foreach (ContentAdapter cont in conts) {
				if (sb.Length > 0) {
					sb.Append(Environment.NewLine);
				}
				sb.Append(cont.DetailPageUri.AbsoluteUri);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		internal static void CopyNamesAndUris(IEnumerable<ContentAdapter> conts) {
			string text = ContentAdapter.GetNamesAndUris(conts);
			if (!string.IsNullOrEmpty(text)) {
				Clipboard.SetText(text);
			}
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
		internal static void TotalTimeOf(IEnumerable<ContentAdapter> conts, out TimeSpan total, out bool isExact) {
			total = TimeSpan.Zero;
			isExact = true;
			foreach (ContentAdapter cont in conts) {
				if (cont.GTimeSpan.CanParse) {
					total += cont.GTimeSpan.TimeSpan;
				} else {
					isExact = false;
				}
			}
		}
		private static bool EqualsHelper(ContentAdapter cont1, ContentAdapter cont2) {
			return cont1.ContentId.Equals(cont2.ContentId);
		}

		private GContent innerCont;
		private GTimeSpan gTimeSpan;
		private string deadline = string.Empty;
		private string comment = string.Empty;

		public ContentAdapter() {
		}
		public ContentAdapter(GContent innerCont) {
			this.InnerContent = innerCont;
			this.TryResetDeadline();
		}
		
		[Browsable(false)]
		public GContent InnerContent {
			get {
				return this.innerCont;
			}
			set {
				this.innerCont = value;
				this.gTimeSpan = new GTimeSpan(this.innerCont.Duration);
			}
		}
		[ReadOnly(true)]
		[Category("専ブラが付加した情報")]
		[Description("配信期限．かなり適当なので必ずしも信用できるわけじゃない．")]
		public string Deadline {
			get { return this.deadline; }
			set { this.deadline = value; }
		}
		[Category("ユーザが入力する情報")]
		[Description("コメント．ユーザが自由に入力できる．ただしプレイリストに入っているものに対して入力しないとほとんど意味ない．")]
		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		
		
		[XmlIgnore]
		[Category("キー")]
		[Description("コンテンツのID．")]
		public string ContentId {
			get { return this.innerCont.ContentId; }
		}
		[XmlIgnore]
		[Category("付随情報")]
		[Description("ジャンル名．")]
		public string GenreName {
			get { return this.innerCont.GenreName; }
		}
		[XmlIgnore]
		[Category("付随情報")]
		[Description("タイトル．")]
		public string Title {
			get { return this.innerCont.Title; }
		}
		[XmlIgnore]
		[Category("付随情報")]
		[Description("サブタイトル．")]
		public string SubTitle {
			get { return this.innerCont.SubTitle; }
		}
		[XmlIgnore]
		[Category("付随情報")]
		[Description("話数．")]
		public string EpisodeNumber {
			get { return this.innerCont.EpisodeNumber; }
		}
		[XmlIgnore]
		[Category("付随情報")]
		[Description("番組時間 (CM時間を除く)．")]
		public string Duration {
			get { return this.innerCont.Duration; }
		}
		[XmlIgnore]
		[Category("付随情報")]
		[Description("詳細記述(長)．")]
		public string LongDescription {
			get { return this.innerCont.LongDescription; }
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("Trueの場合はキャッシュから読まれたことを示す．")]
		public bool FromCache {
			get { return this.innerCont.FromCache; }
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("ジャンル名やタイトルを適当に組み合わせた表示名．")]
		public string DisplayName{
			get {
				StringBuilder sb = new StringBuilder();
				sb.Append("[" + this.GenreName + "]");
				sb.Append(" " + this.Title);
				if (! string.IsNullOrEmpty(this.EpisodeNumber)) {
					sb.Append(" / " + this.EpisodeNumber);
				}
				if (! string.IsNullOrEmpty(this.SubTitle) && this.Title != this.SubTitle && this.EpisodeNumber != this.SubTitle) {
					sb.Append(" / " + this.SubTitle);
				}
				return sb.ToString();
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
		[Description("ダミーかどうかのフラグ．")]
		public bool IsDummy {
			get { return this.InnerContent.IsDummy; }
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("属性文字列．")]
		public string Attributes{
			get {
				if(this.IsDummy) {
					return this.FromCache ? "D" : "DN";
				} else {
					return this.FromCache ? string.Empty : "N";
				}
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
				return GContent.CreatePlayerPageUri(this.ContentId, GlobalSettings.Instance.BitRate);
			}
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("プレイリストのURI．(グローバル設定のビットレートの影響を受ける．ユーザIDを含む．)")]
		public Uri PlayListUri {
			get {
				return GContent.CreatePlayListUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate);
			}
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("コンテンツの画像(大)のURI．")]
		public Uri ImageLargeUri {
			get {return this.innerCont.ImageLargeUri;}
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("コンテンツの画像(小)のURI．")]
		public Uri ImageSmallUri {
			get { return this.innerCont.ImageSmallUri; }
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("お勧め番組の案内ページのURI． (グローバル設定のビットレートの影響を受ける．)")]
		public Uri RecommendPageUri {
			get {
				return GContent.CreateRecommendPageUri(this.ContentId, GlobalSettings.Instance.BitRate);
			}
		}
		
		
		public Uri ChapterPlayListUriOf(int chapterNo) {
			return GContent.CreatePlayListUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate, chapterNo);
		}
		public bool TryResetDeadline() {
			if (Cache.Instance.DeadlineTableReadOnly.TryGetDeadline(this.ContentId, out this.deadline)) {
				return true;
			} else {
				this.deadline = string.Empty;
				return false;
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
			return this.ContentId.GetHashCode();
		}
		public override string ToString() {
			return "<" + this.ContentId + "> " + this.DisplayName;
		}
	}

	class ContentSelectionChangedEventArgs : EventArgs {
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
}
