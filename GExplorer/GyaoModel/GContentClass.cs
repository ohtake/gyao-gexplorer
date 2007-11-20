using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer.GyaoModel {
	[Serializable]
	[DefaultProperty("ContentKey")]
	public sealed class GContentClass : ICloneable{
		private int contentKey;
		private int? packageKey;
		private int? genreKey;
		private string title;
		private string seriesNumber;
		private string subtitle;
		private string summaryHtml;
		private TimeSpan? durationValue;
		private string deadlineText;
		private DateTime created;
		private DateTime lastModified;

		private GPackageClass parentPackage;
		private GGenreClass grandparentGenre;
		
		[NonSerialized]
		private string contentId;
		[NonSerialized]
		private string packageId;
		[NonSerialized]
		private string genreId;
		[NonSerialized]
		private bool hasDeadlineValue = false;
		[NonSerialized]
		private GDeadline deadlineValue;
		[NonSerialized]
		private string summaryText;
		
		public GContentClass() {
		}
		public GContentClass(GDataSet.GContentRow row, GPackageClass parentPackage, GGenreClass grandparentGenre)
			: this() {
			this.contentKey = row.ContentKey;
			this.packageKey = row.IsPackageKeyNull() ? (int?)null : row.PackageKey;
			this.genreKey = row.IsGenreKeyNull() ? (int?)null : row.GenreKey;
			this.title = row.IsTitleNull() ? null : row.Title;
			this.seriesNumber = row.IsSeriesNumberNull() ? null : row.SeriesNumber;
			this.subtitle = row.IsSubtitleNull() ? null : row.Subtitle;
			this.summaryHtml = row.IsSummaryHtmlNull() ? null : row.SummaryHtml;
			this.durationValue = row.IsDurationValueNull() ? (TimeSpan?)null : row.DurationValue;
			this.deadlineText = row.IsDeadlineTextNull() ? null : row.DeadlineText;
			this.created = row.Created;
			this.lastModified = row.LastModified;

			this.parentPackage = parentPackage;
			this.grandparentGenre = grandparentGenre;
		}

		#region GContentRow にあるプロパティ
		[Category("キー")]
		[Description("コンテンツキー．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public int ContentKey {
			get { return this.contentKey; }
			set { this.contentKey = value; }
		}
		[Category("キー")]
		[Description("パッケージキー．")]
		[ReadOnly(true)]
		public int? PackgeKey {
			get { return this.packageKey; }
			set { this.packageKey = value; }
		}
		[Category("キー")]
		[Description("ジャンルキー．")]
		[ReadOnly(true)]
		public int? GenreKey {
			get { return this.genreKey; }
			set { this.genreKey = value; }
		}
		[Category("コンテンツ情報")]
		[Description("タイトル．")]
		[ReadOnly(true)]
		public string Title {
			get { return this.title; }
			set { this.title = value; }
		}
		[Category("コンテンツ情報")]
		[Description("シリーズ番号．")]
		[ReadOnly(true)]
		public string SeriesNumber {
			get { return this.seriesNumber; }
			set { this.seriesNumber = value; }
		}
		[Category("コンテンツ情報")]
		[Description("サブタイトル．")]
		[ReadOnly(true)]
		public string Subtitle {
			get { return this.subtitle; }
			set { this.subtitle = value; }
		}
		[Category("コンテンツ情報")]
		[Description("サマリー(HTML)．")]
		[ReadOnly(true)]
		public string SummaryHtml {
			get { return this.summaryHtml; }
			set { this.summaryHtml = value; }
		}
		[Category("コンテンツ情報")]
		[Description("番組時間．")]
		[ReadOnly(true)]
		[XmlIgnore]
		public TimeSpan? DurationValue {
			get { return this.durationValue; }
			set { this.durationValue = value; }
		}
		[Category("コンテンツ情報")]
		[Description("配信期限(テキスト)．")]
		[ReadOnly(true)]
		public string DeadlineText {
			get { return this.deadlineText; }
			set { this.deadlineText = value; }
		}
		[Category("キャッシュ情報")]
		[Description("データロウ作成日時．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public DateTime Created {
			get { return this.created; }
			set { this.created = value; }
		}
		[Category("キャッシュ情報")]
		[Description("データロウ最終更新日時．")]
		[ReadOnly(true)]
		[XmlAttribute]
		public DateTime LastModified {
			get { return this.lastModified; }
			set { this.lastModified = value; }
		}
		#endregion

		#region GContentRow にあるプロパティから作成できるプロパティ
		[Category("キー")]
		[Description("コンテンツID")]
		[XmlIgnore]
		public string ContentId {
			get {
				if (null == this.contentId) {
					this.contentId = GConvert.ToContentId(this.ContentKey);
				}
				return this.contentId;
			}
		}
		[Category("キー")]
		[Description("パッケージ．不明な場合は pac0000000 とする．")]
		[XmlIgnore]
		public string PackageId {
			get {
				if (null == this.packageId) {
					if (this.PackgeKey.HasValue) {
						this.packageId = GConvert.ToPackageId(this.PackgeKey.Value);
					} else {
						this.packageId = GConvert.ToPackageId(0);
					}
				}
				return this.packageId;
			}
		}
		[Category("キー")]
		[Description("ジャンルID．不明な場合は gen0000000 とする．")]
		[XmlIgnore]
		public string GenreId {
			get {
				if (null == this.genreId) {
					if (this.GenreKey.HasValue) {
						this.genreId = GConvert.ToGenreId(this.GenreKey.Value);
					} else {
						this.genreId = GConvert.ToGenreId(0);
					}
				}
				return this.genreId;
			}
		}
		[Category("URI")]
		[Description("詳細ページのURI．")]
		[XmlIgnore]
		public Uri ContentDetailUri {
			get { return GUriBuilder.CreateContentDetailUri(this.ContentId); }
		}
		[Category("URI")]
		[Description("再生ページのURI．")]
		[XmlIgnore]
		public Uri PlayerLargeUri {
			get { return GUriBuilder.CreatePlayerUri(this.ContentId, GBitrate.SuperFine); }
		}
		[Category("URI")]
		[Description("コンテンツ画像(大)のURI．")]
		[XmlIgnore]
		public Uri ImageLargeUri {
			get { return GUriBuilder.CreateContentImageUri(this.ContentId, this.ImageDirectory, 'l'); }
		}
		[Category("URI")]
		[Description("コンテンツ画像(小)のURI．")]
		[XmlIgnore]
		public Uri ImageSmallUri {
			get { return GUriBuilder.CreateContentImageUri(this.ContentId, this.ImageDirectory, 's'); }
		}
		[Category("URI")]
		[Description("おすすめページのURI．")]
		[XmlIgnore]
		public Uri RecommendationLargeUri {
			get { return GUriBuilder.CreateRecommendationUri(this.ContentId, GBitrate.SuperFine); }
		}
		[Category("URI")]
		[Description("レビュー一覧ページのURI．")]
		[XmlIgnore]
		public Uri ReviewListUri {
			get { return GUriBuilder.CreateReviewListUri(this.ContentId, this.PackageId); }
		}
		[Category("URI")]
		[Description("レビュー投稿ページのURI．")]
		[XmlIgnore]
		public Uri ReviewInputUri {
			get { return GUriBuilder.CreateReviewInputUri(this.ContentId, this.PackageId); }
		}

		[Category("パーズ結果")]
		[Description("配信期限のパーズ結果．")]
		[XmlIgnore]
		public GDeadline DeadlineValue {
			get {
				if (!this.hasDeadlineValue) {
					this.deadlineValue = new GDeadline(this.DeadlineText);
					this.hasDeadlineValue = true;
				}
				return this.deadlineValue;
			}
		}
		[Category("パーズ結果")]
		[Description("サマリー(テキスト)．")]
		[XmlIgnore]
		public string SummaryText {
			get {
				if (null == this.summaryText) {
					this.summaryText = HtmlUtility.HtmlToText(this.SummaryHtml);
				}
				return this.summaryText;
			}
		}
		#endregion
		
		[Browsable(false)]
		public GGenreClass GrandparentGenre {
			get { return this.grandparentGenre; }
			set { this.grandparentGenre = value; }
		}
		[Browsable(false)]
		public GPackageClass ParentPackage {
			get { return this.parentPackage; }
			set { this.parentPackage = value; }
		}
		
		[Browsable(false)]
		[XmlIgnore]
		private string ImageDirectory {
			get {
				if (null != this.GrandparentGenre) {
					return this.GrandparentGenre.ImageDirectory;
				} else {
					return "dummy";
				}
			}
		}

		[Browsable(false)]
		public long? DurationValueTick {
			get {
				return this.DurationValue.HasValue ? this.DurationValue.Value.Ticks : (long?)null;
			}
			set {
				this.DurationValue = value.HasValue ? new TimeSpan(value.Value) : (TimeSpan?)null;
			}
		}
		
		public GContentClass Clone() {
			return base.MemberwiseClone() as GContentClass;
		}
		object ICloneable.Clone() {
			return this.Clone();
		}
	}
}
