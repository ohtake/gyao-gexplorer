using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Yusen.GCrawler;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public class ContentAdapter {
		private GContent innerCont;
		private GTimeSpan gTimeSpan;
		private string deadLine = string.Empty;
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
		public string DeadLine {
			get { return this.deadLine; }
			set { this.deadLine = value; }
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
		[Description("正味時間．")]
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
				if (! string.IsNullOrEmpty(this.SubTitle) && this.Title != this.SubTitle) {
					sb.Append(" / " + this.SubTitle);
				}
				return sb.ToString();
			}
		}
		[XmlIgnore]
		[Category("専ブラが付加した情報")]
		[Description("正味時間のパーズ結果．")]
		public GTimeSpan GTimeSpan {
			get {
				return this.gTimeSpan;
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
		[Description("動画ファイルのURI．(グローバル設定のビットレートの影響を受ける．ユーザIDを含む．)")]
		public Uri MediaFileUri {
			get {
				return GContent.CreateMediaFileUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate);
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
		
		
		public Uri ChapterMediaFileUriOf(int chapterNo) {
			return GContent.CreateMediaFileUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate, chapterNo);
		}
		public bool TryResetDeadline() {
			if (GlobalVariables.DeadLineDictionaryReadonly.TryGetDeadLine(this.ContentId, out this.deadLine)) {
				return true;
			} else {
				this.deadLine = string.Empty;
				return false;
			}
		}

		public override bool Equals(object obj) {
			if (null == obj) {
				return false;
			}
			if (!(obj is ContentAdapter)) {
				return base.Equals(obj);
			}
			return this.ContentId.Equals((obj as ContentAdapter).ContentId);
		}
		public override int GetHashCode() {
			return this.ContentId.GetHashCode();
		}
		public override string ToString() {
			return "<" + this.ContentId + "> " + this.DisplayName;
		}
	}

	class SelectedContentsChangedEventArgs : EventArgs {
		private ReadOnlyCollection<ContentAdapter> contents;

		public SelectedContentsChangedEventArgs() {
			this.contents = new ReadOnlyCollection<ContentAdapter>(new List<ContentAdapter>());
		}
		public SelectedContentsChangedEventArgs(IEnumerable<ContentAdapter> contents) {
			this.contents = new ReadOnlyCollection<ContentAdapter>(new List<ContentAdapter>(contents));
		}
		public ReadOnlyCollection<ContentAdapter> Contents {
			get { return this.contents; }
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
