using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class ContentDetailView : UserControl , IHasSettings<ContentDetailViewSettings>{
		private ContentAdapter contAd;
		private ContentImageSize imgSize = ContentImageSize.Large;
		
		public ContentDetailView() {
			InitializeComponent();

			this.tsmiImageSize.DropDownItems.Clear();
			foreach (ContentImageSize cis in Enum.GetValues(typeof(ContentImageSize))) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(cis.ToString());
				tsmi.Tag = cis;
				tsmi.Click += new EventHandler(delegate(object sender, EventArgs e){
					ToolStripMenuItem selMenu = sender as ToolStripMenuItem;
					ContentImageSize selSize = (ContentImageSize)selMenu.Tag;
					this.ImageSize = selSize;
				});
				this.tsmiImageSize.DropDownItems.Add(tsmi);
			}
			this.ImageSize = this.ImageSize;
			
			this.tsmiSizeMode.DropDownItems.Clear();
			foreach (PictureBoxSizeMode sizeMode in Enum.GetValues(typeof(PictureBoxSizeMode))) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(sizeMode.ToString());
				tsmi.Tag = sizeMode;
				tsmi.Click += new EventHandler(delegate(object sender, EventArgs e) {
					ToolStripMenuItem selMenu = sender as ToolStripMenuItem;
					PictureBoxSizeMode selSize = (PictureBoxSizeMode)selMenu.Tag;
					this.ResizeMode = selSize;
				});
				this.tsmiSizeMode.DropDownItems.Add(tsmi);
			}
			this.ResizeMode = this.ResizeMode;
		}
		
		public ContentAdapter Content {
			get {
				return this.contAd;
			}
			set {
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
				this.txtDescription.Text = value.LongDescription.Replace("\n", "\r\n");
				this.propgDetail.SelectedObject = value;
				this.LoadImageAsync();
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
				this.LoadImageAsync();
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
		public void FillSettings(ContentDetailViewSettings settings) {
			settings.ContentImageSize = this.ImageSize;
			settings.ResizeMode = this.ResizeMode;
			settings.ImageHeight = this.splitContainer1.SplitterDistance;
			settings.SelectedTabIndex = this.tabControl1.SelectedIndex;
		}

		public void ApplySettings(ContentDetailViewSettings settings) {
			this.ImageSize = settings.ContentImageSize ?? this.ImageSize;
			this.ResizeMode = settings.ResizeMode ?? this.ResizeMode;
			this.splitContainer1.SplitterDistance = settings.ImageHeight ?? this.splitContainer1.SplitterDistance;
			this.tabControl1.SelectedIndex = settings.SelectedTabIndex ?? this.tabControl1.SelectedIndex;
		}
		
		public Uri ImageUri {
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
		private void LoadImageAsync() {
			Uri uri = this.ImageUri;
			if(null == uri){
				this.picboxImage.Image = null;
			}else{
				this.picboxImage.LoadAsync(uri.AbsoluteUri);
			}
		}
		private void tsmiCopyImageUri_Click(object sender, EventArgs e) {
			Clipboard.SetText(this.ImageUri.AbsoluteUri);
		}
		private void tsmiCopyNameAndImageUri_Click(object sender, EventArgs e) {
			string name = this.Content.DisplayName;
			string uri = this.ImageUri.AbsoluteUri;
			Clipboard.SetText(name + "\r\n"+ uri);
		}
		private void tsmiCopyNameDetailImageUri_Click(object sender, EventArgs e) {
			string name = this.Content.DisplayName;
			string detail = this.Content.DetailPageUri.AbsoluteUri;
			string uri = this.ImageUri.AbsoluteUri;
			Clipboard.SetText(name + "\r\n" + detail + "\r\n"+ uri);
		}
		private void tsmiCopyImage_Click(object sender, EventArgs e) {
			if (null != this.picboxImage.Image) {
				Clipboard.SetImage(this.picboxImage.Image);
			}
		}
		private void cmsImage_Opening(object sender, CancelEventArgs e) {
			bool hasContent = (null != this.Content);
			this.tsmiCopyImageUri.Enabled = hasContent;
			this.tsmiCopyNameAndImageUri.Enabled = hasContent;
			this.tsmiCopyNameDetailImageUri.Enabled = hasContent;
			this.tsmiCopyImage.Enabled = hasContent;
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
	}
}
