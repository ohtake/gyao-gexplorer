using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Yusen.GCrawler;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public class ContentAdapter {
		private GContent innerCont;
		private string genreName;

		public ContentAdapter() {
		}
		public ContentAdapter(GContent innerCont, GGenre genre) {
			this.innerCont = innerCont;
			this.genreName = genre.GenreName;
		}
		
		[Browsable(false)]
		public GContent InnerContent {
			get { return this.innerCont; }
			set { this.innerCont = value; }
		}
		[Browsable(false)]
		public string GenreName {
			get { return this.genreName; }
			set { this.genreName = value; }
		}
		
		[XmlIgnore]
		[Category("キー")]
		[Description("コンテンツのID．")]
		public string ContentId {
			get { return this.innerCont.ContentId; }
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
				sb.Append("[" + this.genreName + "]");
				sb.Append(" " + this.Title);
				if ("" != this.EpisodeNumber) {
					sb.Append(" / " + this.EpisodeNumber);
				}
				if ("" != this.SubTitle && this.Title != this.SubTitle) {
					sb.Append(" / " + this.SubTitle);
				}
				return sb.ToString();
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
		public Uri RecomendPageUri {
			get {
				return GContent.CreateRecomendPageUri(this.ContentId, GlobalSettings.Instance.BitRate);
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
}
