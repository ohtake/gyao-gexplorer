using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Serialization;

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

		private ContentDetailViewSettings settings;

		public ContentDetailView() {
			InitializeComponent();
			this.settings = new ContentDetailViewSettings(this);
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
					this.txtEpisode.Text = value.EpisodeNumber;
					this.txtSubtitle.Text = value.SubTitle;
					this.txtDuration.Text = value.Duration;
					this.txtDeadline.Text = value.Deadline;
					this.txtDescription.Text = value.LongDescription.Replace("\n", "\r\n");
					this.propgDetail.SelectedObject = value;
#if false
					this.LoadImage();
#else
					this.LoadImageAsync();
#endif
				}
			}
		}
		private ContentImageSize ImageSize {
			get { return this.imgSize; }
			set {
				this.imgSize = value;
				this.LoadImage();
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
			this.txtDescription.Clear();
			this.propgDetail.SelectedObject = null;
			this.picboxImage.Image = null;
		}
		private void LoadImage() {
			Uri uri = this.ImageUri;
			if(null == uri){
				this.picboxImage.Image = null;
			}else{
				try {
					this.picboxImage.Load(uri.AbsoluteUri);
				} catch(Exception e) {
					this.picboxImage.Image = this.picboxImage.ErrorImage;
					this.OnImageLoadError(new ImageLoadErrorEventArgs(e));
				}
			}
		}
		private void LoadImageAsync() {
			Uri uri = this.ImageUri;
			if(null == uri) {
				this.picboxImage.Image = null;
			} else {
				this.picboxImage.LoadAsync(uri.AbsoluteUri);
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
		}
		private void ContentDetailView_Load(object sender, EventArgs e) {
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
		private void cmsImage_Opening(object sender, CancelEventArgs e) {
			this.ChangeEnabilityOfCmsItems();
		}

		private void tsmiTestLoad_Click(object sender, EventArgs e) {
			this.picboxImage.Load();
		}
		private void tsmiTestLoadAsync_Click(object sender, EventArgs e) {
			this.picboxImage.LoadAsync();
		}
		private void tsmiTestCancelAsync_Click(object sender, EventArgs e) {
			this.picboxImage.CancelAsync();
		}
		private void tsmiTestNewPictureBox_Click(object sender, EventArgs e) {
			PictureBoxSizeMode sizeMode = this.picboxImage.SizeMode;
			ContentImageSize imgSize = this.ImageSize;
			
			this.splitContainer1.Panel1.Controls.Remove(this.picboxImage);
			this.picboxImage.Dispose();
			
			this.picboxImage = new PictureBox();
			this.picboxImage.Dock = DockStyle.Fill;
			this.picboxImage.ContextMenuStrip = this.cmsImage;
			this.picboxImage.LoadCompleted += new AsyncCompletedEventHandler(this.picboxImage_LoadCompleted);
			this.splitContainer1.Panel1.Controls.Add(this.picboxImage);
			
			this.picboxImage.SizeMode = sizeMode;
			this.ImageSize = ContentImageSize.None;
			this.ImageSize = imgSize;
		}
		
		private void picboxImage_LoadCompleted(object sender, AsyncCompletedEventArgs e) {
			if(null != e.Error) {
				this.OnImageLoadError(new ImageLoadErrorEventArgs(e.Error));
			}
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
