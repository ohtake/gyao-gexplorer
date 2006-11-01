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
	sealed partial class CrawlResultView2 : UserControl, ICrawlResultViewBindingContract, INotifyPropertyChanged {
		#region ノードの内部クラス
		private abstract class NodeItem : IDisposable{
			private static readonly Size ImgSize4 = new Size(80, 60);
			private static readonly Size ImgSize2 = new Size(40, 30);
			private static readonly Size ImgSize1 = new Size(20, 15);
			
			private readonly ListBox lbox;
			private readonly BackgroundImageLoader bgImgLoader;
			private readonly Uri uri = null;
			private Image image = null;
			private Color genColor;
			private bool requestedAlready = false;
			private bool disposed = false;
			
			protected NodeItem(ListBox lbox, BackgroundImageLoader bgImgLoader, Uri uri, Color genColor) {
				this.lbox = lbox;
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
				this.lbox.Invalidate();
				return true;
			}
			
			public void HandleDrawItem4WithImage(ListBox lbox, DrawItemEventArgs e) {
				Image img = this.GetImage();
				Color bgColor = this.GetBackgroundColor(lbox, e);
				Color fgColor = this.GetForegraondColor(lbox, e);
				Font font;
				bool hasCustomFont = this.TryGetCustomFont(lbox, e, out font);
				if (!hasCustomFont) font = e.Font;

				Rectangle genRect = new Rectangle(e.Bounds.Location, new Size(89, e.Bounds.Height));
				using (LinearGradientBrush genBrush = new LinearGradientBrush(genRect, this.genColor, Color.White, LinearGradientMode.Vertical))
				using (SolidBrush bgBrush = new SolidBrush(bgColor))
				using (SolidBrush fgBrush = new SolidBrush(fgColor)) {
					e.Graphics.FillRectangle(bgBrush, e.Bounds);
					e.Graphics.FillRectangle(genBrush, genRect);
					if (null != img) {
						e.Graphics.DrawImage(img, new Rectangle(new Point(8 + e.Bounds.Left, 1 + e.Bounds.Top), NodeItem.ImgSize4));
					}
					StringFormat sfText = new StringFormat(StringFormatFlags.FitBlackBox | StringFormatFlags.LineLimit);
					sfText.Trimming = StringTrimming.EllipsisCharacter;
					e.Graphics.DrawString(this.FirstLine, font, fgBrush, new RectangleF(90 + e.Bounds.Left, 1 + e.Bounds.Top, e.Bounds.Width -91, 15), sfText);
					e.Graphics.DrawString(this.SeccondLine, font, fgBrush, new RectangleF(90 + e.Bounds.Left, 16 + e.Bounds.Top, e.Bounds.Width - 91, 15), sfText);
					e.Graphics.DrawString(this.Summary, font, fgBrush, new RectangleF(90 + e.Bounds.Left, 31 + e.Bounds.Top, e.Bounds.Width - 91, 30), sfText);
				}
				if ((e.State & DrawItemState.Focus) != DrawItemState.None) {
					using (Pen penFocus = new Pen(SystemColors.ControlDarkDark)) {
						penFocus.DashStyle = DashStyle.Dot;
						e.Graphics.DrawRectangle(penFocus, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1));
					}
				}
				if (hasCustomFont) font.Dispose();
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
			public GroupNodeItem(ListBox lbox, BackgroundImageLoader bgImgLoader)
				: base(lbox, bgImgLoader, null, Color.Black) {
			}
			protected GroupNodeItem(ListBox lbox, BackgroundImageLoader bgImgLoader, Uri imageUri, Color genColor)
				: base(lbox, bgImgLoader, imageUri, genColor) {
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
			public PackageNodeItem(ListBox lbox, BackgroundImageLoader bgImgLoader, GPackageClass package)
				: base(lbox, bgImgLoader, package.ImageMiddleUri, (null != package.ParentGenre)?package.ParentGenre.GenreColor : Color.Black) {
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
			
			public ContentNodeItem(ListBox lbox, BackgroundImageLoader bgImgLoader, GContentClass content, bool newFlag, bool modifiedFlag)
				: base(lbox, bgImgLoader, content.ImageSmallUri, (null != content.GrandparentGenre) ? content.GrandparentGenre.GenreColor : Color.Black) {
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
		
		private readonly BackgroundImageLoader bgImgLoader = new BackgroundImageLoader(0);
		
		public CrawlResultView2() {
			InitializeComponent();

			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
			this.tsmiCmsAddToThePlaylist.Font = new Font(this.tsmiCmsAddToThePlaylist.Font, FontStyle.Bold);
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
						this.lboxCrv.Items.Add(new PackageNodeItem(this.lboxCrv, this.bgImgLoader, cont.ParentPackage));
					}
				} else if(lastPacKey != -1){
					lastPacKey = -1;
					this.lboxCrv.Items.Add(new GroupNodeItem(this.lboxCrv, this.bgImgLoader));
				}
				bool isNew = (result.SortedCKeysNew.BinarySearch(cont.ContentKey) >= 0);
				bool isModified = (result.SortedCKeysModified.BinarySearch(cont.ContentKey) >= 0);
				this.lboxCrv.Items.Add(new ContentNodeItem(this.lboxCrv, this.bgImgLoader, cont, isNew, isModified));
			}
			this.lboxCrv.EndUpdate();
			
			this.ChangeEnabilityOfMenuItems();
			this.tslTime.Text = result.Time.ToString();
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
				nodeItem.HandleDrawItem4WithImage(lboxCrv, e);
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
			BindingContractUtility.BindAllProperties<CrawlResultView2, ICrawlResultViewBindingContract>(this, options);
		}
		#region メインメニュー
		private void tsmiAddToThePlaylist_Click(object sender, EventArgs e) {
			Playlist pl = Program.PlaylistsManager.GetOrCreatePlaylistNamedAs(this.tscbDestPlaylistName.Text);
			this.AddSelectedContentsToPlaylist(pl);
		}
		private void tspmiAddToAnotherPlaylist_PlaylistSelected(object sender, EventArgs e) {
			this.AddSelectedContentsToPlaylist(this.tspmiAddToAnotherPlaylist.LastSelectedPlaylist);
		}
		private void tsmiPlay_Click(object sender, EventArgs e) {
			
		}
		#endregion
		#region コンテキストメニュー
		private void tsmiCmsAddToThePlaylist_Click(object sender, EventArgs e) {
			this.tsmiAddToThePlaylist.PerformClick();
		}
		private void tspmiCmsAddToAnotherPlaylist_PlaylistSelected(object sender, EventArgs e) {
			this.AddSelectedContentsToPlaylist(this.tspmiCmsAddToAnotherPlaylist.LastSelectedPlaylist);
		}
		private void tsmiCmsPlay_Click(object sender, EventArgs e) {
			this.tsmiPlay.PerformClick();
		}
		#endregion
		
		private void AddSelectedContentsToPlaylist(Playlist pl) {
			pl.BeginUpdate();
			foreach (GContentClass cont in this.GetSelectedContents()) {
				pl.AddContent(cont);
			}
			pl.EndUpdate();
		}
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

		private void ChangeEnabilityOfMenuItems() {
			bool hasContent = (this.GetSelectedContents().Length > 0);
			
			this.tsmiAddToThePlaylist.Enabled = hasContent;
			this.tspmiAddToAnotherPlaylist.Enabled = hasContent;
			this.tsmiPlay.Enabled = hasContent;
			
			this.tsmiCmsAddToThePlaylist.Enabled = hasContent;
			this.tspmiCmsAddToAnotherPlaylist.Enabled = hasContent;
			this.tsmiCmsPlay.Enabled = hasContent;
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
	}
	
	interface ICrawlResultViewBindingContract : IBindingContract {
	}
	public sealed class CrawlResultViewOptions : ICrawlResultViewBindingContract {
		public CrawlResultViewOptions() {
		}
	}
}
