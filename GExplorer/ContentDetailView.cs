using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Net;
using System.IO;
using System.Net.Cache;

namespace Yusen.GExplorer {
	public sealed partial class ContentDetailView : UserControl, IHasNewSettings<ContentDetailView.ContentDetailViewSettings> {
		public sealed class ContentDetailViewSettings : INewSettings<ContentDetailViewSettings>{
			private readonly ContentDetailView owner;

			public ContentDetailViewSettings() : this(null){
			}
			internal ContentDetailViewSettings(ContentDetailView owner) {
				this.owner = owner;
			}
			
			[XmlIgnore]
			[Browsable(false)]
			private bool HasOwner {
				get { return null != this.owner; }
			}

			[Category("位置とサイズ")]
			[DisplayName("画像の高さ")]
			[Description("画像の高さをピクセルで指定します")]
			[DefaultValue(null)]
			public int? ImageHeight {
				get {
					if (this.HasOwner) return this.owner.splitContainer1.SplitterDistance;
					return this.imageHeight;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.splitContainer1.SplitterDistance = value.Value;
					else this.imageHeight = value;
				}
			}
			private int? imageHeight;

			[Category("画像")]
			[DisplayName("画像の大きさ")]
			[Description("表示する画像の大きさを指定します．表示しない設定も可能です．")]
			[DefaultValue(ContentImageSize.Large)]
			public ContentImageSize ContentImageSize {
				get {
					if (this.HasOwner) return this.owner.ImageSize;
					else return this.contentImageSize;
				}
				set {
					if (this.HasOwner) this.owner.ImageSize = value;
					else this.contentImageSize = value; }
			}
			private ContentImageSize contentImageSize = ContentImageSize.Large;

			[Category("画像")]
			[DisplayName("リサイズ方法")]
			[Description("画像を表示する際のリサイズ方法を指定します．")]
			[DefaultValue(PictureBoxSizeMode.Zoom)]
			public PictureBoxSizeMode ResizeMode {
				get {
					if (this.HasOwner) return this.owner.ResizeMode;
					else return this.resizeMode;
				}
				set {
					if (this.HasOwner) this.owner.ResizeMode = value;
					else this.resizeMode = value;
				}
			}
			private PictureBoxSizeMode resizeMode = PictureBoxSizeMode.Zoom;

			[Category("画像")]
			[DisplayName("キャッシュレベル")]
			[Description("画像を読み込む際のキャッシュレベルを指定します．")]
			[DefaultValue(RequestCacheLevel.Default)]
			public RequestCacheLevel ImageRequestCacheLevel {
				get {
					if (this.HasOwner) return this.owner.wcImage.CachePolicy.Level;
					else return this.imageRequestCacheLevel;
				}
				set {
					if (this.HasOwner) this.owner.wcImage.CachePolicy = new RequestCachePolicy(value);
					else this.imageRequestCacheLevel = value;
				}
			}
			private RequestCacheLevel imageRequestCacheLevel = RequestCacheLevel.Default;

			[ReadOnly(true)]
			[Category("タブ")]
			[DisplayName("インデックス")]
			[Description("選択されているタブのインデックス番号を指定します．")]
			[DefaultValue(null)]
			public int? SelectedTabIndex {
				get {
					if (this.HasOwner) return this.owner.tabControl1.SelectedIndex;
					else return this.selectedTabIndex;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.tabControl1.SelectedIndex = value.Value;
					else this.selectedTabIndex = value;
				}
			}
			private int? selectedTabIndex = null;
			
			[Category("動作")]
			[DisplayName("再生中の項目にも同期")]
			[Description("プレーヤで再生しているコンテンツが変化したら，詳細ビューで表示するコンテンツを追従させます．")]
			[DefaultValue(true)]
			public bool SyncronizeToCurrentContentEnabled {
				get { return this.syncronizeToCurrentContentEnabled; }
				set { this.syncronizeToCurrentContentEnabled = value; }
			}
			private bool syncronizeToCurrentContentEnabled = true;

			#region INewSettings<ContentDetailViewSettings> Members
			public void ApplySettings(ContentDetailViewSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}
		
		public event EventHandler<ImageLoadErrorEventArgs> ImageLoadError;

		private ContentAdapter contAd;
		private ContentImageSize imgSize = ContentImageSize.Large;

		private object objSetContentAdapter = new object();
		private object objWcImage = new object();
		private WebClient wcImage = new WebClient();

		private ContentDetailViewSettings settings;

		public ContentDetailView() {
			InitializeComponent();

			if (base.DesignMode) return;
			
			this.settings = new ContentDetailViewSettings(this);
			this.wcImage.OpenReadCompleted += new OpenReadCompletedEventHandler(wcImage_OpenReadCompleted);
		}

		public ContentAdapter Content {
			get {
				return this.contAd;
			}
			set {
				lock (this.objSetContentAdapter) {
					if (this.contAd == value) {
						return;
					}
					this.contAd = value;
					if (null == value) {
						this.ClearDisplayedInfo();
						return;
					}
					this.txtId.Text = value.ContentId;
					this.txtTitle.Text = value.Title;
					this.txtEpisode.Text = value.SeriesNumber;
					this.txtSubtitle.Text = value.Subtitle;
					this.txtDuration.Text = value.Duration;
					this.txtDeadline.Text = value.Deadline;
					this.txtSummary.Text = value.Summary;
					this.txtDescription.Text = value.MergedDescription.Replace("\n", "\r\n");
					this.propgDetail.SelectedObject = value;
					
					this.LoadImageAsyncWithWebClient();
				}
			}
		}

		
		private ContentImageSize ImageSize {
			get { return this.imgSize; }
			set {
				this.imgSize = value;
				this.LoadImageAsyncWithWebClient();
			}
		}
		private PictureBoxSizeMode ResizeMode {
			get { return this.picboxImage.SizeMode; }
			set { this.picboxImage.SizeMode = value; }
		}

		private Uri ImageUri {
			get {
				if (null == this.Content) {
					return null;
				}
				switch (this.ImageSize) {
					case ContentImageSize.Large:
						return this.Content.ImageLargeUri;
					case ContentImageSize.Small:
						return this.Content.ImageSmallUri;
					default:
						return null;
				}
			}
		}

		private void ClearDisplayedInfo() {
			this.txtId.Clear();
			this.txtTitle.Clear();
			this.txtEpisode.Clear();
			this.txtSubtitle.Clear();
			this.txtDuration.Clear();
			this.txtSummary.Clear();
			this.txtDescription.Clear();
			this.propgDetail.SelectedObject = null;
			this.picboxImage.Image = null;
		}
		
		private void LoadImageAsyncWithWebClient() {
			lock (this.objWcImage) {
				Uri uri = this.ImageUri;
				if (null == uri) {
					this.picboxImage.Image = null;
				} else {
					this.wcImage.CancelAsync();
					this.wcImage.OpenReadAsync(uri);
				}
			}
		}
		private void wcImage_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e) {
			lock (this.objWcImage) {
				if (e.Cancelled) return;
				if (null != e.Error) {
					this.picboxImage.Image = this.picboxImage.ErrorImage;
					this.OnImageLoadError(new ImageLoadErrorEventArgs(e.Error));
					return;
				}
				try {
					this.picboxImage.Image = Image.FromStream(e.Result);
				} catch (WebException e2) {
					this.picboxImage.Image = this.picboxImage.ErrorImage;
					this.OnImageLoadError(new ImageLoadErrorEventArgs(e2));
				}
			}
		}

		private void OnImageLoadError(ImageLoadErrorEventArgs e) {
			EventHandler<ImageLoadErrorEventArgs> handler = this.ImageLoadError;
			if(null != handler) {
				handler(this, e);
			}
		}
		private void ChangeEnabilityOfCmsItems() {
			bool hasContent = (null != this.Content);
			bool showImage = (null != this.picboxImage.Image);
			
			this.tsmiCopyImageUri.Enabled = hasContent & showImage;
			this.tsmiCopyNameAndImageUri.Enabled = hasContent & showImage;
			this.tsmiCopyNameDetailImageUri.Enabled = hasContent  & showImage;
			this.tsmiCopyImage.Enabled = hasContent & showImage;
			
			this.tsucmiCommand.Enabled = hasContent;
		}
		private void ContentDetailView_Load(object sender, EventArgs e) {
			
			if (base.DesignMode) return;

			PlayList.Instance.CurrentContentChanged += new EventHandler(this.PlayList_CurrentContentChanged);
			this.Disposed += delegate {
				PlayList.Instance.CurrentContentChanged -= new EventHandler(this.PlayList_CurrentContentChanged);
			};
		}
		private void PlayList_CurrentContentChanged(object sender, EventArgs e) {
			if(this.settings.SyncronizeToCurrentContentEnabled) {
				if(PlayList.Instance.HasCurrentContent) {
					this.Content = PlayList.Instance.CurrentContent;
				}
			}
		}
		private void tsmiCopyImageUri_Click(object sender, EventArgs e) {
			Clipboard.SetText(this.ImageUri.AbsoluteUri);
		}
		private void tsmiCopyNameAndImageUri_Click(object sender, EventArgs e) {
			string name = this.Content.DisplayName;
			string uri = this.ImageUri.AbsoluteUri;
			Clipboard.SetText(name + Environment.NewLine + uri);
		}
		private void tsmiCopyNameDetailImageUri_Click(object sender, EventArgs e) {
			string name = this.Content.DisplayName;
			string detail = this.Content.DetailPageUri.AbsoluteUri;
			string uri = this.ImageUri.AbsoluteUri;
			Clipboard.SetText(name + Environment.NewLine + detail + Environment.NewLine + uri);
		}
		private void tsmiCopyImage_Click(object sender, EventArgs e) {
			if (null != this.picboxImage.Image) {
				Clipboard.SetImage(this.picboxImage.Image);
			}
		}
		private void tsucmiCommand_UserCommandSelected(object sender, UserCommandSelectedEventArgs e) {
			e.UserCommand.Execute(new ContentAdapter[] { this.contAd });
		}

		private void cmsImage_Opening(object sender, CancelEventArgs e) {
			this.ChangeEnabilityOfCmsItems();
		}
		
		#region IHasNewSettings<ContentDetailViewSettings> Members
		public ContentDetailView.ContentDetailViewSettings Settings {
			get { return this.settings; }
		}
		#endregion

	}
	
	public enum ContentImageSize {
		None,
		Small,
		Large,
	}

	public sealed class ImageLoadErrorEventArgs : EventArgs {
		private Exception exception;
		public ImageLoadErrorEventArgs(Exception ex) {
			this.exception = ex;
		}
		public Exception Exception {
			get { return this.exception; }
		}
	}
}
