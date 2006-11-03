using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Windows.Forms;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class CrawlResultView : UserControl, ICrawlResultViewBindingContract, INotifyPropertyChanged {
		#region ノードの内部クラス
		private abstract class NodeItem : IDisposable{
			private static readonly Size ImgSize4 = new Size(80, 60);
			private static readonly Size ImgSize2 = new Size(40, 30);
			private static readonly Size ImgSize1 = new Size(20, 15);

			public event EventHandler LoadImageAsyncCompleted;
			
			private readonly BackgroundImageLoader bgImgLoader;
			private readonly Uri uri = null;
			private Image image = null;
			private Color genColor;
			private bool requestedAlready = false;
			private bool disposed = false;
			
			protected NodeItem(BackgroundImageLoader bgImgLoader, Uri uri, Color genColor) {
				this.bgImgLoader = bgImgLoader;
				this.uri = uri;
				this.genColor = genColor;
			}
			private Image GetImage() {
				if (null != this.image) return this.image;
				if (this.requestedAlready) return null;

				this.requestedAlready = true;
				if (null == this.uri) return null;
				HttpWebRequest req = WebRequest.Create(this.uri) as HttpWebRequest;
				req.CachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheOnly);
				try {
					HttpWebResponse res = req.GetResponse() as HttpWebResponse;
					using (Stream stream = res.GetResponseStream()) {
						this.image = Image.FromStream(stream);
					}
					return this.image;
				} catch (WebException) {
					this.bgImgLoader.EnqueueTask(new BackgroundImageLoadTask(this.uri, this.BackgroundImageLoadCallback));
				}
				return null;
			}
			private bool BackgroundImageLoadCallback(Image img) {
				if (this.IsDisposed) {
					return false;
				}
				this.image = img;
				this.OnLoadImageAsyncCompleted();
				return true;
			}
			private void OnLoadImageAsyncCompleted() {
				EventHandler handler = this.LoadImageAsyncCompleted;
				if (null != handler) {
					handler(this, EventArgs.Empty);
				}
			}
			private void HandleDrawItemInternal(ListBox lbox, DrawItemEventArgs e, bool showImage, Size imageSize, int imageIndent, int textIndent, bool drawSecond, bool drawSummary) {
				Image img = showImage ? this.GetImage() : null;
				Color bgColor = this.GetBackgroundColor(lbox, e);
				Color fgColor = this.GetForegraondColor(lbox, e);
				Font font;
				bool hasCustomFont = this.TryGetCustomFont(lbox, e, out font);
				if (!hasCustomFont) font = e.Font;

				Rectangle genRect = new Rectangle(e.Bounds.Location, new Size(textIndent-1, e.Bounds.Height));
				using (LinearGradientBrush genBrush = new LinearGradientBrush(genRect, this.genColor, Color.White, LinearGradientMode.Vertical))
				using (SolidBrush bgBrush = new SolidBrush(bgColor))
				using (SolidBrush fgBrush = new SolidBrush(fgColor)) {
					e.Graphics.FillRectangle(genBrush, genRect);
					if (null != img) {
						e.Graphics.DrawImage(img, new Rectangle(new Point(imageIndent + e.Bounds.Left, 1 + e.Bounds.Top), imageSize));
					}
					e.Graphics.FillRectangle(bgBrush, new Rectangle(e.Bounds.Left+textIndent-1, e.Bounds.Top, e.Bounds.Width - textIndent+1, e.Bounds.Height));
					StringFormat sfText = new StringFormat(StringFormatFlags.FitBlackBox | StringFormatFlags.LineLimit);
					sfText.Trimming = StringTrimming.EllipsisCharacter;
					e.Graphics.DrawString(this.FirstLine, font, fgBrush, new RectangleF(textIndent + e.Bounds.Left, 1 + e.Bounds.Top, e.Bounds.Width - textIndent -1, 15), sfText);
					if (drawSecond) e.Graphics.DrawString(this.SeccondLine, font, fgBrush, new RectangleF(textIndent + e.Bounds.Left, 16 + e.Bounds.Top, e.Bounds.Width - textIndent - 1, 15), sfText);
					if (drawSummary) e.Graphics.DrawString(this.Summary, font, fgBrush, new RectangleF(textIndent + e.Bounds.Left, 31 + e.Bounds.Top, e.Bounds.Width - textIndent- 1, 30), sfText);
				}
				if ((e.State & DrawItemState.Focus) != DrawItemState.None) {
					using (Pen penFocus = new Pen(SystemColors.ControlDarkDark)) {
						penFocus.DashStyle = DashStyle.Dot;
						e.Graphics.DrawRectangle(penFocus, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1));
					}
				}
				if (hasCustomFont) font.Dispose();
			}
			
			public void HandleDrawItem4WithImage(ListBox lbox, DrawItemEventArgs e) {
				this.HandleDrawItemInternal(lbox, e, true, NodeItem.ImgSize4, 8, 90, true, true);
			}
			public void HandleDrawItem2WithImage(ListBox lbox, DrawItemEventArgs e) {
				this.HandleDrawItemInternal(lbox, e, true, NodeItem.ImgSize2, 4, 46, true, false);
			}
			public void HandleDrawItem1WithImage(ListBox lbox, DrawItemEventArgs e) {
				this.HandleDrawItemInternal(lbox, e, true, NodeItem.ImgSize1, 2, 24, false, false);
			}
			public void HandleDrawItem4WithoutImage(ListBox lbox, DrawItemEventArgs e) {
				this.HandleDrawItemInternal(lbox, e, false, NodeItem.ImgSize4, 8, 10, true, true);
			}
			public void HandleDrawItem2WithoutImage(ListBox lbox, DrawItemEventArgs e) {
				this.HandleDrawItemInternal(lbox, e, false, NodeItem.ImgSize2, 4, 6, true, false);
			}
			public void HandleDrawItem1WithoutImage(ListBox lbox, DrawItemEventArgs e) {
				this.HandleDrawItemInternal(lbox, e, false, NodeItem.ImgSize1, 2, 4, false, false);
			}
			
			protected abstract string FirstLine { get;}
			protected abstract string SeccondLine { get;}
			protected abstract string Summary { get;}
			protected abstract int ImageIndent { get;}
			protected abstract Color GetForegraondColor(ListBox lbox, DrawItemEventArgs e);
			protected abstract Color GetBackgroundColor(ListBox lbox, DrawItemEventArgs e);
			protected abstract bool TryGetCustomFont(ListBox lbox, DrawItemEventArgs e, out Font font);
			
			public bool IsDisposed {
				get { return this.disposed; }
			}
			protected virtual void Dispose(bool disposing) {
				if (disposing) {
					this.disposed = true;
					if (null != this.image) this.image.Dispose();
				}
			}
			public void Dispose() {
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
			~NodeItem() {
				this.Dispose(false);
			}
		}
		private class GroupNodeItem : NodeItem {
			public GroupNodeItem(BackgroundImageLoader bgImgLoader)
				: base(bgImgLoader, null, Color.Black) {
			}
			protected GroupNodeItem(BackgroundImageLoader bgImgLoader, Uri imageUri, Color genColor)
				: base(bgImgLoader, imageUri, genColor) {
			}
			
			protected override string FirstLine {
				get { return "不明なパッケージ"; }
			}
			protected override string SeccondLine {
				get { return string.Empty; }
			}
			protected override string Summary {
				get { return string.Empty; }
			}
			protected sealed override int ImageIndent {
				get { return 1; }
			}
			protected sealed override Color GetForegraondColor(ListBox lbox, DrawItemEventArgs e) {
				return SystemColors.ControlText;
			}
			protected sealed override Color GetBackgroundColor(ListBox lbox, DrawItemEventArgs e) {
				return SystemColors.ControlDark;
			}
			protected sealed override bool TryGetCustomFont(ListBox lbox, DrawItemEventArgs e, out Font font) {
				font = null;
				return false;
			}
		}
		private sealed class PackageNodeItem : GroupNodeItem {
			private readonly GPackageClass package;
			private readonly Color genColor;
			private string firstLine = null;
			public PackageNodeItem(BackgroundImageLoader bgImgLoader, GPackageClass package)
				: base(bgImgLoader, package.ImageMiddleUri, (null != package.ParentGenre)?package.ParentGenre.GenreColor : Color.Black) {
				this.package = package;
				if (null != package.ParentGenre) {
					this.genColor = package.ParentGenre.GenreColor;
				}
			}
			public GPackageClass Package {
				get { return this.package; }
			}
			
			protected override string FirstLine {
				get {
					if (null == this.firstLine) {
						this.firstLine = string.Format("<{0}> {1}", this.package.PackageId, this.package.PackageTitle);
					}
					return this.firstLine;
				}
			}
			protected override string SeccondLine {
				get {
					return this.Package.PackageCatch;
				}
			}
			protected override string Summary {
				get { return this.Package.PackageText; }
			}
		}
		private sealed class ContentNodeItem : NodeItem {
			private readonly GContentClass content;
			private readonly Color genColor;
			private readonly bool isNew;
			private readonly bool isModified;
			private string strFirstLine = null;
			private string strSecondLine = null;
			
			public ContentNodeItem(BackgroundImageLoader bgImgLoader, GContentClass content, bool newFlag, bool modifiedFlag)
				: base(bgImgLoader, content.ImageSmallUri, (null != content.GrandparentGenre) ? content.GrandparentGenre.GenreColor : Color.Black) {
				this.content = content;
				this.isNew = newFlag;
				this.isModified = modifiedFlag;
				if (null != this.content.GrandparentGenre) {
					this.genColor = this.content.GrandparentGenre.GenreColor;
				}
			}
			public GContentClass Content {
				get { return this.content; }
			}
			
			protected override string FirstLine {
				get {
					if (null == this.strFirstLine) {
						this.strFirstLine = string.Format("<{0}> {1} | {2} | {3}", content.ContentId, content.Title, content.SeriesNumber, content.Subtitle);
					}
					return this.strFirstLine;
				}
			}
			protected override string SeccondLine {
				get {
					if (null == this.strSecondLine) {
						this.strSecondLine = string.Format("{0} {1}", content.DurationValue, content.DeadlineValue, content.Created, content.LastModified);
					}
					return this.strSecondLine;
				}
			}
			protected override string Summary {
				get { return this.Content.SummaryText; }
			}
			protected override int ImageIndent {
				get { return 8; }
			}
			protected override Color GetForegraondColor(ListBox lbox, DrawItemEventArgs e) {
				if ((e.State & DrawItemState.Selected) != DrawItemState.None) {
					if (lbox.Focused) {
						return SystemColors.HighlightText;
					} else {
						return SystemColors.ControlText;
					}
				} else {
					return SystemColors.WindowText;
				}
			}
			protected override Color GetBackgroundColor(ListBox lbox, DrawItemEventArgs e) {
				if ((e.State & DrawItemState.Selected) != DrawItemState.None) {
					if (lbox.Focused) {
						return SystemColors.Highlight;
					} else {
						return SystemColors.Control;
					}
				} else {
					return SystemColors.Window;
				}
			}
			protected override bool TryGetCustomFont(ListBox lbox, DrawItemEventArgs e, out Font font) {
				if (this.isNew) {
					font = new Font(e.Font, FontStyle.Bold);
					return true;
				} else if (this.isModified) {
					font = new Font(e.Font, FontStyle.Italic);
					return true;
				}
				font = null;
				return false;
			}
		}
		#endregion

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler LastSelectedContentChanged;
		
		private GContentClass lastSelectedContent;
		private CrawlResultViewItemHeight itemHeight = CrawlResultViewItemHeight.FourLines;
		private bool showThumbnail = true;
		
		private readonly BackgroundImageLoader bgImgLoader = new BackgroundImageLoader(0);
		
		public CrawlResultView() {
			InitializeComponent();

			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
			this.tsmiCmsAddToThePlaylist.Font = new Font(this.tsmiCmsAddToThePlaylist.Font, FontStyle.Bold);
			
			this.lboxCrv.Font = new Font(this.Font.FontFamily, 12, GraphicsUnit.Pixel);
		}
		
		private void CrawlResultView_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			this.ChangeEnabilityOfMenuItems();
			
			this.bgImgLoader.StartWorking();
			this.Disposed += delegate {
				this.bgImgLoader.Dispose();
			};
		}
		private void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (null != handler) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		private void OnLastSelectedContentChanged() {
			EventHandler handler = this.LastSelectedContentChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		
		public void ViewCrawlResult(CrawlResult result) {
			this.bgImgLoader.ClearTasks();
			
			if (null == result) {
				this.ClearCrawlResult();
				return;
			}
			this.lboxCrv.BeginUpdate();
			this.lboxCrv.Items.Clear();
			int lastPacKey = -2;
			foreach (GContentClass cont in result.Contents) {
				if (null != cont.ParentPackage) {
					if (lastPacKey != cont.ParentPackage.PackageKey) {
						lastPacKey = cont.ParentPackage.PackageKey;
						this.AddNodeInternal(new PackageNodeItem(this.bgImgLoader, cont.ParentPackage));
					}
				} else if (lastPacKey != -1) {
					lastPacKey = -1;
					this.AddNodeInternal(new GroupNodeItem(this.bgImgLoader));
				}
				bool isNew = (result.SortedCKeysNew.BinarySearch(cont.ContentKey) >= 0);
				bool isModified = (result.SortedCKeysModified.BinarySearch(cont.ContentKey) >= 0);
				this.AddNodeInternal(new ContentNodeItem(this.bgImgLoader, cont, isNew, isModified));
			}
			this.lboxCrv.EndUpdate();
			
			this.ChangeEnabilityOfMenuItems();
			this.tslTime.Text = result.Time.ToString();
		}
		private void AddNodeInternal(NodeItem nodeItem) {
			nodeItem.LoadImageAsyncCompleted += new EventHandler(nodeItem_LoadImageAsyncCompleted);
			this.lboxCrv.Items.Add(nodeItem);
		}
		private void nodeItem_LoadImageAsyncCompleted(object sender, EventArgs e) {
			int idx = this.lboxCrv.Items.IndexOf(sender);
			if(idx >= 0){
				this.lboxCrv.Invalidate(this.lboxCrv.GetItemRectangle(idx));
			}
		}
		private void ClearCrawlResult() {
			this.lboxCrv.Items.Clear();
			this.ChangeEnabilityOfMenuItems();
		}
		
		private void tscbDestPlaylistName_DropDown(object sender, EventArgs e) {
			this.tscbDestPlaylistName.BeginUpdate();
			this.tscbDestPlaylistName.Items.Clear();
			foreach (Playlist pl in Program.PlaylistsManager) {
				this.tscbDestPlaylistName.Items.Add(pl.Name);
			}
			this.tscbDestPlaylistName.EndUpdate();
		}

		#region リストボックスのイベントハンドラ
		private void lboxCrv_SelectedIndexChanged(object sender, EventArgs e) {
			this.ChangeEnabilityOfMenuItems();
			//使い物にならないから DrawItem で
			/*if (this.lboxCrv.SelectedIndex >= 0) {
				ContentNodeItem cni = this.lboxCrv.Items[this.lboxCrv.SelectedIndex] as ContentNodeItem;
				if (cni != null) {
					this.LastSelectedContent = cni.Content;
				}
			}*/
		}
		private void lboxCrv_DrawItem(object sender, DrawItemEventArgs e) {
			if (e.Index < 0) return;
			NodeItem nodeItem = this.lboxCrv.Items[e.Index] as NodeItem;
			if (null != nodeItem) {
				if (this.ShowThumbnail) {
					switch (this.ItemHeight) {
						case CrawlResultViewItemHeight.FourLines:
							nodeItem.HandleDrawItem4WithImage(this.lboxCrv, e);
							break;
						case CrawlResultViewItemHeight.TwoLines:
							nodeItem.HandleDrawItem2WithImage(this.lboxCrv, e);
							break;
						case CrawlResultViewItemHeight.OneLine:
							nodeItem.HandleDrawItem1WithImage(this.lboxCrv, e);
							break;
					}
				} else {
					switch (this.ItemHeight) {
						case CrawlResultViewItemHeight.FourLines:
							nodeItem.HandleDrawItem4WithoutImage(this.lboxCrv, e);
							break;
						case CrawlResultViewItemHeight.TwoLines:
							nodeItem.HandleDrawItem2WithoutImage(this.lboxCrv, e);
							break;
						case CrawlResultViewItemHeight.OneLine:
							nodeItem.HandleDrawItem1WithoutImage(this.lboxCrv, e);
							break;
					}
				}
				if ((e.State & DrawItemState.Focus) != DrawItemState.None) {//仕方ないからこっちで
					ContentNodeItem cni = nodeItem as ContentNodeItem;
					if (cni != null) {
						this.LastSelectedContent = cni.Content;
					}
				}
				return;
			}
		}
		private void lboxCrv_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					this.tsmiCmsAddToThePlaylist.PerformClick();
					break;
			}
		}
		private void lboxCrv_DoubleClick(object sender, EventArgs e) {
			this.tsmiCmsAddToThePlaylist.PerformClick();
		}
		private void lboxCrv_Enter(object sender, EventArgs e) {
			if (this.lboxCrv.SelectedIndices.Count >= 2) {
				//フォーカスがなくて選択されている項目が再描画されないことへの対策
				this.lboxCrv.Invalidate();
			}
		}
		private void lboxCrv_Leave(object sender, EventArgs e) {
			if (this.lboxCrv.SelectedIndices.Count >= 2) {
				//フォーカスがなくて選択されている項目が再描画されないことへの対策
				this.lboxCrv.Invalidate();
			}
		}
		#endregion
		
		public ToolStripDropDown GetToolStripDropDown() {
			return this.tsddbDropDown.DropDown;
		}
		public void BindToOptions(CrawlResultViewOptions options) {
			BindingContractUtility.BindAllProperties<CrawlResultView, ICrawlResultViewBindingContract>(this, options);
		}
		#region メインメニュー
		private void tsmiAddToThePlaylist_Click(object sender, EventArgs e) {
			Playlist pl = Program.PlaylistsManager.GetOrCreatePlaylistNamedAs(this.tscbDestPlaylistName.Text);
			pl.BeginUpdate();
			foreach (GContentClass cont in this.GetSelectedContents()) {
				pl.AddContent(cont);
			}
			pl.EndUpdate();
		}
		private void tspmiAddToAnotherPlaylist_PlaylistSelected(object sender, EventArgs e) {
			Playlist pl = (sender as ToolStripPlaylistMenuItem).LastSelectedPlaylist;
			pl.BeginUpdate();
			foreach (GContentClass cont in this.GetSelectedContents()) {
				pl.AddContent(cont);
			}
			pl.EndUpdate();
		}
		private void tsmiPlay_Click(object sender, EventArgs e) {
			GContentClass[] conts = this.GetSelectedContents();
			Program.PlayContent(conts[0], null);
		}
		private void tsmiCopyName_Click(object sender, EventArgs e) {
			CPClipboardUtility.CopyNames(this.GetSelectedPackagesAndContents());
		}
		private void tsmiCopyUri_Click(object sender, EventArgs e) {
			CPClipboardUtility.CopyUris(this.GetSelectedPackagesAndContents());
		}
		private void tsmiCopyNameAndUri_Click(object sender, EventArgs e) {
			CPClipboardUtility.CopyNamesAndUris(this.GetSelectedPackagesAndContents());
		}
		private void tscpmiCopyOtherProperties_PropertySelected(object sender, EventArgs e) {
			CPClipboardUtility.CopyContentProperties(this.GetSelectedContents(), (sender as ToolStripContentPropertyMenuItem).LastSelectedPropertyInfo);
		}
		private void tsecmiCommand_ExternalCommandSelected(object sender, EventArgs e) {
			ExternalCommand ec = (sender as ToolStripExternalCommandMenuItem).LastSelectedExternalCommand;
			ec.Execute(this.GetSelectedContents());
		}
		#endregion
		#region コンテキストメニュー
		private void tsmiCmsAddToThePlaylist_Click(object sender, EventArgs e) {
			this.tsmiAddToThePlaylist.PerformClick();
		}
		private void tspmiCmsAddToAnotherPlaylist_PlaylistSelected(object sender, EventArgs e) {
			this.tspmiAddToAnotherPlaylist_PlaylistSelected(sender, e);
		}
		private void tsmiCmsPlay_Click(object sender, EventArgs e) {
			this.tsmiPlay.PerformClick();
		}
		private void tsmiCmsCopyName_Click(object sender, EventArgs e) {
			this.tsmiCopyName.PerformClick();
		}
		private void tsmiCmsCopyUri_Click(object sender, EventArgs e) {
			this.tsmiCopyUri.PerformClick();
		}
		private void tsmiCmsCopyNameAndUri_Click(object sender, EventArgs e) {
			this.tsmiCopyNameAndUri.PerformClick();
		}
		private void tscpmiCmsCopyOtherProperties_PropertySelected(object sender, EventArgs e) {
			this.tscpmiCopyOtherProperties_PropertySelected(sender, e);
		}
		private void tsecmiCmsCommand_ExternalCommandSelected(object sender, EventArgs e) {
			this.tsecmiCommand_ExternalCommandSelected(sender, e);
		}
		#endregion
		
		private GContentClass[] GetSelectedContents() {
			List<GContentClass> conts = new List<GContentClass>();
			foreach (NodeItem node in this.lboxCrv.SelectedItems) {
				ContentNodeItem cnode = node as ContentNodeItem;
				if (null != cnode) {
					conts.Add(cnode.Content);
				}
			}
			return conts.ToArray();
		}
		private GPackageClass[] GetSelectedPackages() {
			List<GPackageClass> pacs = new List<GPackageClass>();
			foreach (NodeItem node in this.lboxCrv.SelectedItems) {
				PackageNodeItem pnode = node as PackageNodeItem;
				if (null != pnode) {
					pacs.Add(pnode.Package);
				}
			}
			return pacs.ToArray();
		}
		private object[] GetSelectedPackagesAndContents() {
			List<object> objs = new List<object>();
			foreach (NodeItem node in this.lboxCrv.SelectedItems) {
				ContentNodeItem cnode = node as ContentNodeItem;
				PackageNodeItem pnode = node as PackageNodeItem;
				if (cnode != null) {
					objs.Add(cnode.Content);
				}else if(pnode != null){
					objs.Add(pnode.Package);
				}
			}
			return objs.ToArray();
		}
		
		private void ChangeEnabilityOfMenuItems() {
			bool hasContent = (this.GetSelectedContents().Length > 0);
			bool hasPackage = (this.GetSelectedPackages().Length > 0);
			
			this.tsmiAddToThePlaylist.Enabled = hasContent;
			this.tspmiAddToAnotherPlaylist.Enabled = hasContent;
			this.tsmiPlay.Enabled = hasContent;
			this.tsmiCopyName.Enabled = hasContent | hasPackage;
			this.tsmiCopyUri.Enabled = hasContent | hasPackage;
			this.tsmiCopyNameAndUri.Enabled = hasContent | hasPackage;
			this.tscpmiCopyOtherProperties.Enabled = hasContent;
			this.tsecmiCommand.Enabled = hasContent;
			
			this.tsmiCmsAddToThePlaylist.Enabled = hasContent;
			this.tspmiCmsAddToAnotherPlaylist.Enabled = hasContent;
			this.tsmiCmsPlay.Enabled = hasContent;
			this.tsmiCmsCopyName.Enabled = hasContent | hasPackage;
			this.tsmiCmsCopyUri.Enabled = hasContent | hasPackage;
			this.tsmiCmsCopyNameAndUri.Enabled = hasContent | hasPackage;
			this.tscpmiCmsCopyOtherProperties.Enabled = hasContent;
			this.tsecmiCmsCommand.Enabled = hasContent;
		}

		public GContentClass LastSelectedContent {
			get { return this.lastSelectedContent; }
			private set {
				if (this.lastSelectedContent != value) {
					this.lastSelectedContent = value;
					this.OnLastSelectedContentChanged();
				}
			}
		}
		#region ICrawlResultViewBindingContract Members
		[DefaultValue(CrawlResultViewItemHeight.FourLines)]
		public CrawlResultViewItemHeight ItemHeight {
			get { return this.itemHeight; }
			set {
				if (this.itemHeight != value) {
					this.itemHeight = value;
					this.tsbLine1.Checked = false;
					this.tsbLine2.Checked = false;
					this.tsbLine4.Checked = false;
					switch (value) {
						case CrawlResultViewItemHeight.OneLine:
							this.tsbLine1.Checked = true;
							break;
						case CrawlResultViewItemHeight.TwoLines:
							this.tsbLine2.Checked = true;
							break;
						case CrawlResultViewItemHeight.FourLines:
							this.tsbLine4.Checked = true;
							break;
					}
					this.OnPropertyChanged("ItemHeight");
					if (base.DesignMode) return;
					this.lboxCrv.ItemHeight = (int)value;
				}
			}
		}
		[DefaultValue(true)]
		public bool ShowThumbnail {
			get { return this.showThumbnail; }
			set {
				if (this.showThumbnail != value) {
					this.showThumbnail = value;
					this.OnPropertyChanged("ShowThumbnail");
					if (base.DesignMode) return;
					this.lboxCrv.Invalidate();
				}
			}
		}
		#endregion

		private void tsbLine1_Click(object sender, EventArgs e) {
			this.ItemHeight = CrawlResultViewItemHeight.OneLine;
		}
		private void tsbLine2_Click(object sender, EventArgs e) {
			this.ItemHeight = CrawlResultViewItemHeight.TwoLines;
		}
		private void tsbLine4_Click(object sender, EventArgs e) {
			this.ItemHeight = CrawlResultViewItemHeight.FourLines;
		}
	}

	public enum CrawlResultViewItemHeight {
		OneLine = 17,
		TwoLines = 32,
		FourLines = 62,
	}

	interface ICrawlResultViewBindingContract : IBindingContract {
		CrawlResultViewItemHeight ItemHeight { get;set;}
		bool ShowThumbnail { get;set;}
	}
	public sealed class CrawlResultViewOptions : ICrawlResultViewBindingContract {
		public CrawlResultViewOptions() {
		}

		#region ICrawlResultViewBindingContract Members
		private CrawlResultViewItemHeight itemHeight = CrawlResultViewItemHeight.FourLines;
		[Category("表示")]
		[DisplayName("行数")]
		[Description("ひとつのアイテムに割く行数を指定します．")]
		[DefaultValue(CrawlResultViewItemHeight.FourLines)]
		public CrawlResultViewItemHeight ItemHeight {
			get { return this.itemHeight; }
			set { this.itemHeight = value; }
		}
		private bool showThumbnail = true;
		[Category("表示")]
		[DisplayName("サムネイル")]
		[Description("サムネイルを表示します．")]
		[DefaultValue(true)]
		public bool ShowThumbnail {
			get { return this.showThumbnail; }
			set { this.showThumbnail = value; }
		}
		#endregion
	}
}
