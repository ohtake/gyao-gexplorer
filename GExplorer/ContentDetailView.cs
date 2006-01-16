using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	sealed partial class ContentDetailView : UserControl , IHasSettings<ContentDetailViewSettings>{
		public event EventHandler<ImageLoadErrorEventArgs> ImageLoadError;

		private ContentAdapter contAd;
		private ContentImageSize imgSize = ContentImageSize.Large;
		
		public ContentDetailView() {
			InitializeComponent();
		}
		
		public ContentAdapter Content {
			get {
				return this.contAd;
			}
			set {
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
		[DefaultValue(ContentImageSize.Large)]
		public ContentImageSize ImageSize {
			get { return this.imgSize; }
			set {
				this.imgSize = value;
				foreach (ToolStripMenuItem subitem in this.tsmiImageSize.DropDownItems) {
					subitem.Checked = (value == (ContentImageSize)subitem.Tag);
				}
				this.LoadImage();
			}
		}
		[DefaultValue(PictureBoxSizeMode.Zoom)]
		public PictureBoxSizeMode ResizeMode {
			get { return this.picboxImage.SizeMode; }
			set {
				this.picboxImage.SizeMode = value;
				foreach (ToolStripMenuItem subitem in this.tsmiSizeMode.DropDownItems) {
					subitem.Checked = (value == (PictureBoxSizeMode)subitem.Tag);
				}
			}
		}
		[DefaultValue(true)]
		public bool SyncronizeToCurrentContentEnabled {
			get { return this.tsmiSyncronizeToCurrentContent.Checked; }
			set { this.tsmiSyncronizeToCurrentContent.Checked = value; }
		}

		public void FillSettings(ContentDetailViewSettings settings) {
			settings.ContentImageSize = this.ImageSize;
			settings.ResizeMode = this.ResizeMode;
			settings.ImageHeight = this.splitContainer1.SplitterDistance;
			settings.SelectedTabIndex = this.tabControl1.SelectedIndex;
			settings.SyncronizeToCurrentContentEnabled = this.SyncronizeToCurrentContentEnabled;
		}

		public void ApplySettings(ContentDetailViewSettings settings) {
			this.ImageSize = settings.ContentImageSize ?? this.ImageSize;
			this.ResizeMode = settings.ResizeMode ?? this.ResizeMode;
			this.splitContainer1.SplitterDistance = settings.ImageHeight ?? this.splitContainer1.SplitterDistance;
			this.tabControl1.SelectedIndex = settings.SelectedTabIndex ?? this.tabControl1.SelectedIndex;
			this.SyncronizeToCurrentContentEnabled = settings.SyncronizeToCurrentContentEnabled ?? this.SyncronizeToCurrentContentEnabled;
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
					if(null != this.ImageLoadError) {
						this.ImageLoadError(this, new ImageLoadErrorEventArgs(e));
					}
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
		private void ChangeEnabilityOfCmsItems() {
			bool hasContent = (null != this.Content);
			bool showImage = (null != this.picboxImage.Image);
			this.tsmiCopyImageUri.Enabled = hasContent & showImage;
			this.tsmiCopyNameAndImageUri.Enabled = hasContent & showImage;
			this.tsmiCopyNameDetailImageUri.Enabled = hasContent  & showImage;
			this.tsmiCopyImage.Enabled = hasContent & showImage;
		}
		private void ContentDetailView_Load(object sender, EventArgs e) {
			this.tsmiImageSize.DropDownItems.Clear();
			foreach (ContentImageSize cis in Enum.GetValues(typeof(ContentImageSize))) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(cis.ToString());
				tsmi.Tag = cis;
				tsmi.Click += new EventHandler(delegate(object sender2, EventArgs e2) {
					ToolStripMenuItem selMenu = sender2 as ToolStripMenuItem;
					ContentImageSize selSize = (ContentImageSize)selMenu.Tag;
					this.ImageSize = selSize;
					this.ChangeEnabilityOfCmsItems();
					
				});
				this.tsmiImageSize.DropDownItems.Add(tsmi);
			}
			this.tsmiImageSize.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;
			this.ImageSize = this.ImageSize;

			this.tsmiSizeMode.DropDownItems.Clear();
			foreach (PictureBoxSizeMode sizeMode in Enum.GetValues(typeof(PictureBoxSizeMode))) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(sizeMode.ToString());
				tsmi.Tag = sizeMode;
				tsmi.Click += new EventHandler(delegate(object sender2, EventArgs e2) {
					ToolStripMenuItem selMenu = sender2 as ToolStripMenuItem;
					PictureBoxSizeMode selSize = (PictureBoxSizeMode)selMenu.Tag;
					this.ResizeMode = selSize;
				});
				this.tsmiSizeMode.DropDownItems.Add(tsmi);
			}
			this.tsmiSizeMode.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;
			this.ResizeMode = this.ResizeMode;

			this.tsmiSettings.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;

			PlayList.Instance.CurrentContentChanged += new EventHandler(this.PlayList_CurrentContentChanged);
			this.Disposed += delegate {
				PlayList.Instance.CurrentContentChanged -= new EventHandler(this.PlayList_CurrentContentChanged);
			};
		}
		private void PlayList_CurrentContentChanged(object sender, EventArgs e) {
			if(this.SyncronizeToCurrentContentEnabled) {
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

		internal ToolStripDropDown SettingsDropDown{
			get { return this.tsmiSettings.DropDown; }
		}
		internal bool SettingsVisible{
			get { return this.tsmiSettings.Visible; }
			set{
				this.tsmiSettings.Visible = value;
				this.tssSettings.Visible = value;
			}
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
	}
	
	public enum ContentImageSize {
		None,
		Small,
		Large,
	}

	public class ContentDetailViewSettings {
		private int? imageHeight;
		private ContentImageSize? contentImageSize;
		private PictureBoxSizeMode? resizeMode;
		private int? selectedTabIndex;
		private bool? syncronizeToCurrentContentEnabled;
		
		public int? ImageHeight {
			get { return this.imageHeight; }
			set { this.imageHeight = value; }
		}
		public ContentImageSize? ContentImageSize {
			get { return this.contentImageSize; }
			set { this.contentImageSize = value; }
		}
		public PictureBoxSizeMode? ResizeMode {
			get { return this.resizeMode; }
			set { this.resizeMode = value; }
		}
		public int? SelectedTabIndex {
			get { return this.selectedTabIndex; }
			set { this.selectedTabIndex = value; }
		}
		public bool? SyncronizeToCurrentContentEnabled {
			get { return this.syncronizeToCurrentContentEnabled; }
			set { this.syncronizeToCurrentContentEnabled = value; }
		}
	}

	class ImageLoadErrorEventArgs : EventArgs {
		private Exception exception;
		public ImageLoadErrorEventArgs(Exception ex) {
			this.exception = ex;
		}
		public Exception Exception {
			get { return this.exception; }
		}
	}
}
