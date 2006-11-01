using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.AppCore;
using System.Threading;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer.UserInterfaces {
	partial class GenreSelctControl : UserControl {
		#region タブページ派生クラス
		private abstract class GenreTabPage : TabPage {
			protected GenreTabPage(string text) : base(text){
			}
			protected GenreTabPage(string text, string toolTipText) : this(text){
				base.ToolTipText = toolTipText;
			}
			public abstract Color Color { get;}
		}
		private sealed class GGenreTabPage : GenreTabPage {
			private readonly GGenreClass ggenre;
			public GGenreTabPage(GGenreClass ggenre)
				: base(ggenre.GenreName, ggenre.GenreName + Environment.NewLine + ggenre.GenreTopPageUri.AbsoluteUri) {
				this.ggenre = ggenre;
			}
			public override Color Color {
				get { return this.ggenre.GenreColor; }
			}
			public GGenreClass Genre {
				get { return this.ggenre; }
			}
		}
		private sealed class VGenreTabPage : GenreTabPage {
			private readonly IVirtualGenre vgenre;
			public VGenreTabPage(IVirtualGenre vgenre)
				: base(vgenre.ShortName, vgenre.LongName) {
				this.vgenre = vgenre;
			}
			public override Color Color {
				get { return Color.Black; }
			}
			public IVirtualGenre VirtualGenre {
				get { return this.vgenre; }
			}
		}
		#endregion
		#region その他内部クラス
		private sealed class TabControlDoubleClickListener : NativeWindow{
			public event EventHandler DoubleClick;
			public TabControlDoubleClickListener(TabControl tabc) {
				tabc.HandleDestroyed += new EventHandler(tabc_HandleDestroyed);
				base.AssignHandle(tabc.Handle);
			}
			private void tabc_HandleDestroyed(object sender, EventArgs e) {
				base.ReleaseHandle();
			}
			private void OnDoubleClick() {
				EventHandler handler = this.DoubleClick;
				if (null != handler) {
					handler(this, EventArgs.Empty);
				}
			}
			protected override void WndProc(ref Message m) {
				switch ((WM)m.Msg) {
					case WM.LBUTTONDBLCLK:
						this.OnDoubleClick();
						break;
				}
				base.WndProc(ref m);
			}
		}
		private sealed class CrawlRequestObject {
			private readonly GGenreClass genre;
			private readonly CrawlResult prevResult;
			private readonly DateTime created;
			public CrawlRequestObject(GGenreClass genre, CrawlResult prevResult) {
				this.genre = genre;
				this.prevResult = prevResult;
				this.created = DateTime.Now;
			}
			public GGenreClass Genre {
				get { return this.genre; }
			}
			public CrawlResult PreviousCrawlResult {
				get { return this.prevResult; }
			}
			public DateTime Created {
				get { return this.created; }
			}
		}
		#endregion
		
		public event EventHandler CrawlResultSelected;
		public event ProgressChangedEventHandler CrawlProgressChanged;
		public event EventHandler CrawlStarted;
		public event EventHandler CrawlEnded;
		public event EventHandler StatusMessageChanged;
		public event EventHandler RequiredHeightChanged;
		
		private int updatingGenreTab = 0;
		private CrawlResult selectedCrawlResult = null;
		private CrawlRequestObject lastRequest = null;
		private GenreTabPage lastContextMenuSource = null;
		private string statusMessage = string.Empty;
		private int requiredHeight = -1;

		public GenreSelctControl() {
			InitializeComponent();
			this.tsmiGoToPrevTab.ShortcutKeys = Keys.Control | Keys.PageUp;
			this.tsmiGoToNextTab.ShortcutKeys = Keys.Control | Keys.PageDown;
			this.tsmiCmsStartCrawl.Font = new Font(this.tsmiCmsStartCrawl.Font, FontStyle.Bold);
			
			if (base.DesignMode) return;
			TabControlDoubleClickListener tcDoubleClickListener = new TabControlDoubleClickListener(this.tabcGsc);
			tcDoubleClickListener.DoubleClick += new EventHandler(tcDoubleClickListener_DoubleClick);
		}

		private void GenreSelctControl_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			this.RecalcurateRequiredHeight();
		}

		#region タブ選択
		private void BeginUpdateGenreTabs() {
			Interlocked.Increment(ref this.updatingGenreTab);
		}
		private void EndUpdateGenreTabs() {
			if (! this.IsUpdatingGenreTabs) {
				throw new InvalidOperationException();
			}
			if (0 == Interlocked.Decrement(ref this.updatingGenreTab)) {
				this.HandleTabChangeInternal();
				this.RecalcurateRequiredHeight();
			}
		}
		private bool IsUpdatingGenreTabs {
			get { return this.updatingGenreTab > 0; }
		}
		private void RecalcurateRequiredHeight() {
			if (this.IsUpdatingGenreTabs) return;
			int maxBottom = 0;
			for (int i = 0; i < this.tabcGsc.TabCount; i++) {
				Rectangle rect = this.tabcGsc.GetTabRect(i);
				int bot = rect.Bottom;
				if (maxBottom < bot) {
					maxBottom = bot;
				}
			}
			this.RequiredHeight = maxBottom;
		}
		private void HandleTabChangeInternal() {
			this.ChangeEnabilityOfMainMenuItems();
			
			GenreTabPage gtp = this.tabcGsc.SelectedTab as GenreTabPage;
			if (null == gtp) return;
			
			GGenreTabPage ggtp = gtp as GGenreTabPage;
			VGenreTabPage vgtp = gtp as VGenreTabPage;
			if (null != ggtp) {
				CrawlResult result;
				Program.CacheManager.TryDeserializeCrawlResult(ggtp.Genre, out result);
				this.SelectedCrawlResult = result;
				return;
			} else if (null != vgtp) {
				this.SelectedCrawlResult = vgtp.VirtualGenre.GetCrawlResult();
				return;
			}
			
			
		}
		private void ChangeEnabilityOfMainMenuItems() {
			if (this.IsUpdatingGenreTabs) return;
			
			bool isGGenre = this.tabcGsc.SelectedTab is GGenreTabPage;
			bool isVGenre = this.tabcGsc.SelectedTab is VGenreTabPage;
			bool isSelected = isGGenre || isVGenre;
			bool isFirstTab = (0 == this.tabcGsc.SelectedIndex);
			bool isLastTab = (this.tabcGsc.TabCount == 1 + this.tabcGsc.SelectedIndex);
			bool isAloneTab = (1 == this.tabcGsc.TabCount);
			bool hasAnyTab = (this.tabcGsc.TabCount > 0);

			this.tsmiGoToPrevTab.Enabled = hasAnyTab && !(isSelected && isAloneTab);
			this.tsmiGoToNextTab.Enabled = hasAnyTab && !(isSelected && isAloneTab);
			this.tsmiStartCrawl.Enabled = isGGenre;
			this.tsmiBrowseGenreTop.Enabled = isGGenre;
			this.tsmiBrowseTimetableUpdated.Enabled = isGGenre;
			this.tsmiCopyName.Enabled = isGGenre || isVGenre;
			this.tsmiCopyUri.Enabled = isGGenre;
			this.tsmiCopyNameAndUri.Enabled = isGGenre;
			this.tsmiCloseThis.Enabled = isSelected;
			this.tsmiCloseLeft.Enabled = isSelected && !isFirstTab;
			this.tsmiCloseRight.Enabled = isSelected && !isLastTab;
			this.tsmiCloseButThis.Enabled = isSelected && !isAloneTab;
			this.tsmiCloseAll.Enabled = hasAnyTab;
		}
		#endregion
		#region タブの追加・削除
		private void RemoveGenreTabInternal(GenreTabPage gtp) {
			if (gtp == this.tabcGsc.SelectedTab) {
				this.BeginUpdateGenreTabs();
				int idx = this.tabcGsc.TabPages.IndexOf(gtp);
				if (idx + 1 == this.tabcGsc.TabCount) {
					this.tabcGsc.SelectedIndex = idx - 1;
				} else {
					this.tabcGsc.SelectedIndex = idx + 1;
				}
				this.tabcGsc.TabPages.Remove(gtp);
				this.EndUpdateGenreTabs();
			} else {
				this.tabcGsc.TabPages.Remove(gtp);
			}
			if (this.tabcGsc.TabCount == 0) {
				this.SelectedCrawlResult = null;
			}
			this.RecalcurateRequiredHeight();
			this.ChangeEnabilityOfMainMenuItems();
		}
		private void AddGenreTabInternal(GenreTabPage gtp) {
			this.tabcGsc.TabPages.Add(gtp);
			this.RecalcurateRequiredHeight();
			this.ChangeEnabilityOfMainMenuItems();
		}
		private bool AddTabIfNotExists(GGenreClass genre) {
			foreach (GenreTabPage gtp in this.tabcGsc.TabPages) {
				GGenreTabPage ggtp = gtp as GGenreTabPage;
				if (null != ggtp) {
					if (ggtp.Genre == genre) return false;
				}
			}
			this.AddGenreTabInternal(new GGenreTabPage(genre));
			return true;
		}
		private bool AddTabIfNotExists(IVirtualGenre vgenre) {
			foreach (GenreTabPage gtp in this.tabcGsc.TabPages) {
				VGenreTabPage vgtp = gtp as VGenreTabPage;
				if (null != vgtp) {
					if (vgtp.VirtualGenre.ShortName == vgenre.ShortName && vgtp.VirtualGenre.LongName == vgenre.LongName) return false;
				}
			}
			this.AddGenreTabInternal(new VGenreTabPage(vgenre));
			return true;
		}
		public void AddAndSelectVirtualGenre(IVirtualGenre vgenre) {
			if (this.AddTabIfNotExists(vgenre)) {
				this.tabcGsc.SelectedIndex = this.tabcGsc.TabCount - 1;
				this.HandleTabChangeInternal();
			}
		}
		private void RemoveLeftTabs(GenreTabPage gtp) {
			this.BeginUpdateGenreTabs();
			TabPage tp;
			while ((tp = this.tabcGsc.TabPages[0]) != gtp) {
				this.RemoveGenreTabInternal(tp as GenreTabPage);
			}
			this.EndUpdateGenreTabs();
		}
		private void RemoveRightTabs(GenreTabPage gtp) {
			this.BeginUpdateGenreTabs();
			TabPage tp;
			while ((tp = this.tabcGsc.TabPages[this.tabcGsc.TabCount - 1]) != gtp) {
				this.RemoveGenreTabInternal(tp as GenreTabPage);
			}
			this.EndUpdateGenreTabs();
		}
		public void ClearGenreTabs() {
			this.BeginUpdateGenreTabs();
			for (int i = this.tabcGsc.TabCount - 1; i >= 0; i--) {
				this.RemoveGenreTabInternal(this.tabcGsc.TabPages[i] as GenreTabPage);
			}
			this.EndUpdateGenreTabs();
		}
		#endregion

		#region Onなんちゃら
		private void OnCrawlResultSelected() {
			EventHandler handler = this.CrawlResultSelected;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnCrawlProgressChanged(ProgressChangedEventArgs e) {
			ProgressChangedEventHandler handler = this.CrawlProgressChanged;
			if (null != handler) {
				handler(this, e);
			}
		}
		private void OnStatusMessageChanged() {
			EventHandler handler = this.StatusMessageChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnRequiredHeightChanged() {
			EventHandler handler = this.RequiredHeightChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnCrawlStarted() {
			EventHandler handler = this.CrawlStarted;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnCrawlEnded() {
			EventHandler handler = this.CrawlEnded;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		
		public CrawlResult SelectedCrawlResult {
			get {return this.selectedCrawlResult;}
			private set {
				if (this.selectedCrawlResult != value) {
					this.selectedCrawlResult = value;
					this.OnCrawlResultSelected();
				}
			}
		}
		#endregion
		#region OnなんちゃらをInvoke
		private void InvokeOnCrawlStarted() {
			if (this.InvokeRequired) {
				this.Invoke(new MethodInvoker(this.InvokeOnCrawlStarted));
			} else {
				this.OnCrawlStarted();
			}
		}
		private void InvokeOnCrawlEnded() {
			if (this.InvokeRequired) {
				this.Invoke(new MethodInvoker(this.InvokeOnCrawlEnded));
			} else {
				this.OnCrawlEnded();
			}
		}
		#endregion
		#region クロール
		private void StartCrawl(GGenreTabPage ggtp) {
			if (this.bwCrawl.IsBusy) {
				MessageBox.Show("多重クロール禁止", "多重クロールは禁止です．", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			CrawlResult prevResult;
			Program.CacheManager.TryDeserializeCrawlResult(ggtp.Genre, out prevResult);
			this.lastRequest = new CrawlRequestObject(ggtp.Genre, prevResult);
			this.bwCrawl.RunWorkerAsync();
		}
		private void bwCrawl_DoWork(object sender, DoWorkEventArgs e) {
			CrawlRequestObject req = this.lastRequest;
			if (null == req) throw new InvalidOperationException();

			this.InvokeOnCrawlStarted();
			e.Result = new GenreCrawler(req.Genre, req.PreviousCrawlResult, Program.RootOptions.CrawlOptions, Program.CacheManager, this.bwCrawl).GetResult();
			if (null == e.Result) {
				e.Cancel = true;
			}
		}
		private void bwCrawl_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			if (this.InvokeRequired) {
				this.Invoke(new ProgressChangedEventHandler(this.bwCrawl_ProgressChanged), new object[] { sender, e });
			} else {
				this.OnCrawlProgressChanged(e);
			}
		}
		private void bwCrawl_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			this.InvokeOnCrawlEnded();
			if (e.Cancelled) {
				this.StatusMessage = string.Format("クロールを中止しました．クロール時間: {0}", DateTime.Now - this.lastRequest.Created);
				return;
			}
			if (null != e.Error) {
				Program.DisplayException("クロール中にキャッチされなかった例外", e.Error);
				return;
			}
			
			CrawlResult result = e.Result as CrawlResult;
			Program.CacheManager.SerializeCrawlResult(this.lastRequest.Genre, result);
			
			this.BeginUpdateGenreTabs();
			if (this.AddTabIfNotExists(this.lastRequest.Genre)) {
				this.tabcGsc.SelectedIndex = this.tabcGsc.TabCount - 1;
			} else {
				foreach (GGenreTabPage ggtp in this.tabcGsc.TabPages) {
					if (this.lastRequest.Genre == ggtp.Genre) {
						this.tabcGsc.SelectedTab = ggtp;
						break;
					}
				}
			}
			this.EndUpdateGenreTabs();
			this.StatusMessage = string.Format("クロール完了．クロール時間: {0}", DateTime.Now - this.lastRequest.Created);
		}
		public void RequestCrawlCancellation() {
			this.bwCrawl.CancelAsync();
		}
		#endregion
		

		#region タブコントロールのイベントハンドラ
		private void tabcGsc_DrawItem(object sender, DrawItemEventArgs e) {
			GenreTabPage gtp = this.tabcGsc.TabPages[e.Index] as GenreTabPage;
			if (null == gtp) return;

			Color genreColor = gtp.Color;
			string tabText = gtp.Text;
			Rectangle textRect = new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + 2, e.Bounds.Width, e.Bounds.Height);
			StringFormat strFormat = new StringFormat();
			strFormat.FormatFlags = StringFormatFlags.NoWrap;

			if (e.State == DrawItemState.Selected) {
				using (SolidBrush brushGenreColor = new SolidBrush(genreColor))
				using (SolidBrush brushWhite = new SolidBrush(Color.White)) {
					e.Graphics.FillRectangle(brushGenreColor, e.Bounds);
					e.Graphics.DrawString(tabText, e.Font, brushWhite, textRect, strFormat);
				}
			} else {
				Rectangle markRect = new Rectangle(e.Bounds.Left, e.Bounds.Top, 3, e.Bounds.Height);
				using (SolidBrush brushText = new SolidBrush(SystemColors.WindowText))
				using (SolidBrush brushGenreColor = new SolidBrush(genreColor)) {
					e.Graphics.DrawString(tabText, e.Font, brushText, textRect, strFormat);
					e.Graphics.FillRectangle(brushGenreColor, markRect);
				}
			}
		}
		private void tabcGsc_SelectedIndexChanged(object sender, EventArgs e) {
			if (this.IsUpdatingGenreTabs) return;
			this.HandleTabChangeInternal();
		}
		private void tabcGsc_MouseDoubleClick(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.Left:
					// Appearance が Normal 以外だとダブルクリックの動作がおかしいので
					// ダブルクリックは WndProc で拾う
					break;
			}
		}
		private void tabcGsc_MouseClick(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.Right:
					for (int i = 0; i < this.tabcGsc.TabCount; i++) {
						Rectangle tabRect = this.tabcGsc.GetTabRect(i);
						if (tabRect.Contains(e.Location)) {
							this.OpenContextMenu(i, e.Location);
							break;
						}
					}
					break;
				case MouseButtons.Middle:
					for (int i = 0; i < this.tabcGsc.TabCount; i++) {
						Rectangle tabRect = this.tabcGsc.GetTabRect(i);
						if (tabRect.Contains(e.Location)) {
							this.RemoveGenreTabInternal(this.tabcGsc.TabPages[i] as GenreTabPage);
							break;
						}
					}
					break;
			}
		}
		private void tabcGsc_KeyUp(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Apps://Appsは離されたとき
					int tabIndex = this.tabcGsc.SelectedIndex;
					if (tabIndex >= 0) {
						Rectangle tabRect = this.tabcGsc.GetTabRect(tabIndex);
						this.OpenContextMenu(tabIndex, new Point(tabRect.Left, tabRect.Bottom));
					}
					break;
			}
		}
		private void tabcGsc_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.F10://Shift+F10は押されたとき
					if ((Control.ModifierKeys & Keys.Shift) != Keys.None) {
						int tabIndex = this.tabcGsc.SelectedIndex;
						if (tabIndex >= 0) {
							Rectangle tabRect = this.tabcGsc.GetTabRect(tabIndex);
							this.OpenContextMenu(tabIndex, new Point(tabRect.Left, tabRect.Bottom));
						}
					}
					break;
			}
		}
		private void tabcGsc_Resize(object sender, EventArgs e) {
			this.RecalcurateRequiredHeight();
		}
		#endregion

		private void tcDoubleClickListener_DoubleClick(object sender, EventArgs e) {
			this.tsmiStartCrawl.PerformClick();
		}
		private void OpenContextMenu(int tabIndex, Point location) {
			this.lastContextMenuSource = this.tabcGsc.TabPages[tabIndex] as GenreTabPage;
			bool isGGenre = this.lastContextMenuSource is GGenreTabPage;
			bool isVGenre = this.lastContextMenuSource is VGenreTabPage;
			bool isFirstTab = (tabIndex == 0);
			bool isLastTab = (this.tabcGsc.TabCount == tabIndex + 1);
			bool isAloneTab = (this.tabcGsc.TabCount == 1);

			this.tsmiCmsStartCrawl.Enabled = isGGenre;
			this.tsmiCmsBrowseGenreTop.Enabled = isGGenre;
			this.tsmiCmsBrowseTimetableUpdated.Enabled = isGGenre;
			this.tsmiCmsCopyName.Enabled = isGGenre || isVGenre;
			this.tsmiCmsCopyUri.Enabled = isGGenre;
			this.tsmiCmsCopyNameAndUri.Enabled = isGGenre;
			this.tsmiCmsCloseThis.Enabled = true;
			this.tsmiCmsCloseLeft.Enabled = !isFirstTab;
			this.tsmiCmsCloseRight.Enabled = !isLastTab;
			this.tsmiCmsCloseButThis.Enabled = !isAloneTab;
			
			this.cmsGenreTab.Show(this.tabcGsc, location);
		}

		public string StatusMessage {
			get { return this.statusMessage; }
			private set {
				if (this.statusMessage != value) {
					this.statusMessage = value;
					this.OnStatusMessageChanged();
				}
			}
		}
		public int RequiredHeight {
			get { return this.requiredHeight; }
			private set {
				if (value != this.requiredHeight || value != this.Height) {
					this.requiredHeight = value;
					this.OnRequiredHeightChanged();
				}
			}
		}

		public ToolStripDropDown GetToolStripDropDown() {
			return this.tsmiRoot.DropDown;
		}

		#region メインメニュー
		private void tsgmiAddAndSelectGenre_GenreSelected(object sender, EventArgs e) {
			this.BeginUpdateGenreTabs();
			if (this.AddTabIfNotExists(this.tsgmiAddAndSelectGenre.LastSelectedGenre)) {
				this.tabcGsc.SelectedIndex = this.tabcGsc.TabCount - 1;
			} else {
				foreach (GGenreTabPage ggtp in this.tabcGsc.TabPages) {
					if (this.tsgmiAddAndSelectGenre.LastSelectedGenre == ggtp.Genre) {
						this.tabcGsc.SelectedTab = ggtp;
						break;
					}
				}
			}
			this.EndUpdateGenreTabs();
		}
		private void tsmiAddAllGenres_Click(object sender, EventArgs e) {
			bool isSelected = this.tabcGsc.SelectedIndex >= 0;
			this.BeginUpdateGenreTabs();
			foreach (GGenreClass genre in Program.CacheManager.GetEnumerableOfAllGenres()) {
				this.AddTabIfNotExists(genre);
			}
			if (!isSelected) this.tabcGsc.SelectedIndex = -1;
			this.EndUpdateGenreTabs();
		}
		private void tsmiGoToPrevTab_Click(object sender, EventArgs e) {
			int tabIdx = this.tabcGsc.SelectedIndex;
			if (tabIdx < 0 || tabIdx == 0) {
				this.tabcGsc.SelectedIndex = this.tabcGsc.TabCount - 1;
			} else {
				this.tabcGsc.SelectedIndex = tabIdx - 1;
			}
		}
		private void tsmiGoToNextTab_Click(object sender, EventArgs e) {
			int tabIdx = this.tabcGsc.SelectedIndex;
			if (tabIdx < 0 || tabIdx + 1 == this.tabcGsc.TabCount) {
				this.tabcGsc.SelectedIndex = 0;
			} else {
				this.tabcGsc.SelectedIndex = tabIdx + 1;
			}
		}
		private void tsmiStartCrawl_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.tabcGsc.SelectedTab as GGenreTabPage;
			this.StartCrawl(ggtp);
		}
		private void tsmiBrowseGenreTop_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.tabcGsc.SelectedTab as GGenreTabPage;
			Program.BrowsePage(ggtp.Genre.GenreTopPageUri);
		}
		private void tsmiBrowseTimetableUpdated_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.tabcGsc.SelectedTab as GGenreTabPage;
			Program.BrowsePage(ggtp.Genre.TimetableRecentlyUpdatedFirstUri);
		}
		private void tsmiCopyName_Click(object sender, EventArgs e) {
			GenreTabPage gtp = this.tabcGsc.SelectedTab as GenreTabPage;
			Clipboard.SetText(gtp.Text);
		}
		private void tsmiCopyUri_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.tabcGsc.SelectedTab as GGenreTabPage;
			Clipboard.SetText(ggtp.Genre.GenreTopPageUri.AbsoluteUri);
		}
		private void tsmiCopyNameAndUri_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.tabcGsc.SelectedTab as GGenreTabPage;
			Clipboard.SetText(ggtp.Genre.GenreName + Environment.NewLine + ggtp.Genre.GenreTopPageUri.AbsoluteUri);
		}
		private void tsmiCloseThis_Click(object sender, EventArgs e) {
			GenreTabPage gtp = this.tabcGsc.SelectedTab as GenreTabPage;
			this.RemoveGenreTabInternal(gtp);
		}
		private void tsmiCloseLeft_Click(object sender, EventArgs e) {
			this.RemoveLeftTabs(this.tabcGsc.SelectedTab as GenreTabPage);
		}
		private void tsmiCloseRight_Click(object sender, EventArgs e) {
			this.RemoveRightTabs(this.tabcGsc.SelectedTab as GenreTabPage);
		}
		private void tsmiCloseButThis_Click(object sender, EventArgs e) {
			GenreTabPage gtp = this.tabcGsc.SelectedTab as GenreTabPage;
			this.BeginUpdateGenreTabs();
			this.RemoveRightTabs(gtp);
			this.RemoveLeftTabs(gtp);
			this.EndUpdateGenreTabs();
		}
		private void tsmiCloseAll_Click(object sender, EventArgs e) {
			this.ClearGenreTabs();
		}
		#endregion

		#region コンテキストメニュー
		private void tsmiCmsStartCrawl_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.lastContextMenuSource as GGenreTabPage;
			this.StartCrawl(ggtp);
		}
		private void tsmiCmsBrowseGenreTop_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.lastContextMenuSource as GGenreTabPage;
			Program.BrowsePage(ggtp.Genre.GenreTopPageUri);
		}
		private void tsmiCmsBrowseTimetableUpdated_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.lastContextMenuSource as GGenreTabPage;
			Program.BrowsePage(ggtp.Genre.TimetableRecentlyUpdatedFirstUri);
		}
		private void tsmiCmsCopyName_Click(object sender, EventArgs e) {
			Clipboard.SetText(this.lastContextMenuSource.Text);
		}
		private void tsmiCmsCopyUri_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.lastContextMenuSource as GGenreTabPage;
			Clipboard.SetText(ggtp.Genre.GenreTopPageUri.AbsoluteUri);
		}
		private void tsmiCmsCopyNameAndUri_Click(object sender, EventArgs e) {
			GGenreTabPage ggtp = this.lastContextMenuSource as GGenreTabPage;
			Clipboard.SetText(ggtp.Genre.GenreName + Environment.NewLine + ggtp.Genre.GenreTopPageUri.AbsoluteUri);
		}
		private void tsmiCmsCloseThis_Click(object sender, EventArgs e) {
			this.RemoveGenreTabInternal(this.lastContextMenuSource);
		}
		private void tsmiCmsCloseLeft_Click(object sender, EventArgs e) {
			this.RemoveLeftTabs(this.lastContextMenuSource);
		}
		private void tsmiCmsCloseRight_Click(object sender, EventArgs e) {
			this.RemoveRightTabs(this.lastContextMenuSource);
		}
		private void tsmiCmsCloseButThis_Click(object sender, EventArgs e) {
			this.BeginUpdateGenreTabs();
			this.RemoveRightTabs(this.lastContextMenuSource);
			this.RemoveLeftTabs(this.lastContextMenuSource);
			this.EndUpdateGenreTabs();
		}
		#endregion

		public void LoadOpenTabs(GenreSelectControlOptions options) {
			if (null == options.OpenTabs) {
				this.tsmiAddAllGenres.PerformClick();
				options.OpenTabs = new List<int>();
			} else {
				if (options.RestoreOpenTabs) {
					Dictionary<int, GGenreClass> genres = new Dictionary<int, GGenreClass>();
					foreach (GGenreClass genre in Program.CacheManager.GetEnumerableOfAllGenres()) {
						genres.Add(genre.GenreKey, genre);
					}
					this.BeginUpdateGenreTabs();
					foreach (int gKey in options.OpenTabs) {
						GGenreClass genre;
						if (genres.TryGetValue(gKey, out genre)) {
							this.AddTabIfNotExists(genre);
						}
					}
					this.tabcGsc.SelectedIndex = -1;
					this.EndUpdateGenreTabs();
				}
			}
		}
		public void StoreOpenTabs(GenreSelectControlOptions options) {
			List<int> openTabs = new List<int>();
			foreach (GenreTabPage gtp in this.tabcGsc.TabPages) {
				GGenreTabPage ggtp = gtp as GGenreTabPage;
				if (null != ggtp) {
					openTabs.Add(ggtp.Genre.GenreKey);
				}
			}
			options.OpenTabs = openTabs;
		}
	}

	public sealed class GenreSelectControlOptions {
		private bool restoreOpenTabs = true;
		[Category("起動時のタブ")]
		[DisplayName("開いていたタブを復元する")]
		[Description("前回終了したときに開いていたタブを起動時に復元します．")]
		[DefaultValue(true)]
		public bool RestoreOpenTabs {
			get { return this.restoreOpenTabs; }
			set { this.restoreOpenTabs = value; }
		}
		
		private List<int> openTabs = null;
		[Category("起動時のタブ")]
		[DisplayName("前回終了時のタブ")]
		[Description("前回終了したときに開いていたタブの一覧です．")]
		[ReadOnly(true)]
		public List<int> OpenTabs {
			get { return this.openTabs; }
			set { this.openTabs = value; }
		}
		
	}
}
