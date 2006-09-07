using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using GGenre = Yusen.GCrawler.GGenre;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public sealed partial class GenreTabControl : TabControl, IHasNewSettings<GenreTabControl.GenreTabControlSettings> {
		private const int MaxNameLengthDefaultValue = -1;
		
		public sealed class GenreTabControlSettings : INewSettings<GenreTabControlSettings> {
			private readonly GenreTabControl owner;
			public GenreTabControlSettings() : this(null){
			}
			internal GenreTabControlSettings(GenreTabControl owner) {
				this.owner = owner;
			}
			
			[XmlIgnore]
			[Browsable(false)]
			[ReadOnly(true)]
			private bool HasOwner {
				get { return null != this.owner; }
			}

			[Category("見た目")]
			[DisplayName("外観")]
			[Description("タブの外観を指定します．")]
			[DefaultValue(TabAppearance.FlatButtons)]
			public TabAppearance TabAppearance {
				get {
					if (this.HasOwner) return this.owner.Appearance;
					else return this.tabAppearance;
				}
				set {
					if (this.HasOwner) this.owner.Appearance = value;
					else this.tabAppearance = value;
				}
			}
			private TabAppearance tabAppearance = TabAppearance.FlatButtons;

			[Category("見た目")]
			[DisplayName("ドローモード")]
			[Description("タブのドローモードを指定します．OwnerDrawFixedにするとオーナードローで独自の描画を行います．")]
			[DefaultValue(TabDrawMode.OwnerDrawFixed)]
			public TabDrawMode TabDrawMode {
				get {
					if (this.HasOwner) return this.owner.DrawMode;
					else return this.tabDrawMode;
				}
				set {
					if (this.HasOwner) this.owner.DrawMode = value;
					else this.tabDrawMode = value;
				}
			}
			private TabDrawMode tabDrawMode = TabDrawMode.OwnerDrawFixed;

			[Category("レイアウト")]
			[DisplayName("多段タブ")]
			[Description("タブを多段で表示します．")]
			[DefaultValue(true)]
			public bool MultiLineTab {
				get {
					if (this.HasOwner) return this.owner.Multiline;
					else return this.multiLineTab;
				}
				set {
					if (this.HasOwner) this.owner.Multiline = value;
					else this.multiLineTab = value;
				}
			}
			private bool multiLineTab = true;

			[Category("レイアウト")]
			[DisplayName("タブ名の最大長")]
			[Description("タブ名の長さが指定値よりも大きい場合に省略示します．負数の場合は無制限．サイズモードが Fixed のときは負数にしてください．")]
			[DefaultValue(GenreTabControl.MaxNameLengthDefaultValue)]
			public int MaxNameLength {
				get {
					if (this.HasOwner) return this.owner.MaxNameLength;
					else return this.maxNameLength;
				}
				set {
					if (this.HasOwner) this.owner.MaxNameLength = value;
					else this.maxNameLength = value;
				}
			}
			private int maxNameLength = GenreTabControl.MaxNameLengthDefaultValue;

			[Category("レイアウト")]
			[DisplayName("サイズモード")]
			[Description("各々のタブのサイズの決定方法を指定します．")]
			[DefaultValue(TabSizeMode.Fixed)]
			public TabSizeMode TabSizeMode {
				get {
					if (this.HasOwner) return this.owner.SizeMode;
					else return this.tabSizeMode;
				}
				set {
					if (this.HasOwner) this.owner.SizeMode = value;
					else this.tabSizeMode = value;
				}
			}
			private TabSizeMode tabSizeMode = TabSizeMode.Fixed;

			[Category("レイアウト")]
			[DisplayName("タブサイズ")]
			[Description("サイズモードが Fixed の場合のタブのサイズを指定します．")]
			[DefaultValue(typeof(Size), "60, 20")]
			public Size TabItemSize {
				get {
					if (this.HasOwner) return this.owner.ItemSize;
					else return this.tabItemSize;
				}
				set {
					if (this.HasOwner) this.owner.ItemSize = value;
					else this.tabItemSize = value;
				}
			}
			private Size tabItemSize = new Size(60, 20);

			#region INewSettings<GenreTabControlSettings> Members
			public void ApplySettings(GenreTabControlSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}

		public event EventHandler<GenreTabSelectedEventArgs> GenreSelected;
		public event EventHandler RequiredHeightChanged;

		private int requiredHeight = 0;

		private GenreTabControlSettings settings;

		private int maxNameLength = GenreTabControl.MaxNameLengthDefaultValue;
		private int MaxNameLength {
			get { return this.maxNameLength; }
			set {
				if (value != this.maxNameLength) {
					this.maxNameLength = value;
					this.OnMaxNameLengthChanged();
				}
			}
		}

		public GenreTabControl() {
			InitializeComponent();

			if (base.DesignMode) return;
			this.settings = new GenreTabControlSettings(this);
		}

		public void AddGenre(GGenre genre) {
			GenreTabPage gtp;
			string name = genre.GenreName;
			gtp = new GenreTabPage(genre);
			gtp.CrawlRequested += new EventHandler(gtp_ReloadRequested);
			gtp.ResultRemoved += new EventHandler(gtp_ResultRemoved);
			base.TabPages.Add(gtp);
		}
		
		public GGenre SelectedGenre {
			get {
				GenreTabPage selGtp = base.SelectedTab as GenreTabPage;
				if (null == selGtp) return null;
				return selGtp.Genre;
			}
			set {
				foreach (TabPage tp in base.TabPages) {
					GenreTabPage gtp = tp as GenreTabPage;
					if (null == gtp) continue;
					if(gtp.Genre.Equals(value)){
						base.SelectedTab = tp;
						return;
					}
				}
				this.SelectedTab = null;
			}
		}

		private DateTime lastClickedTime = DateTime.MinValue;
		private Point lastClickedPoint = Point.Empty;

		private void GenreTabControl_MouseClick(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.Left:
					// Appearance が Normal 以外だとダブルクリックの動作がおかしいので
					// ダブルクリックは WndProc で拾う
					break;
				case MouseButtons.Right:
					for (int i = 0; i < base.TabCount; i++) {
						Rectangle tabRect = base.GetTabRect(i);
						if (tabRect.Contains(e.Location)) {
							GenreTabPage gtp = base.TabPages[i] as GenreTabPage;
							if (null != gtp) {
								gtp.ShowContextMenu(this.PointToScreen(e.Location));
							}
						}
					}
					break;
				//タブを閉じる隠し機能はバグいっぽいから廃止
				/*case MouseButtons.Middle:
					for (int i = 0; i < base.TabCount; i++) {
						Rectangle tabRect = base.GetTabRect(i);
						if (tabRect.Contains(e.Location)) {
							this.TabPages.RemoveAt(i);
						}
					}
					break;*/
			}
		}

		private void GenreTabControl_DoubleClick(object sender, EventArgs e) {
			// Appearance が Normal 以外だとダブルクリックの動作がおかしいので
			// ダブルクリックは WndProc で拾う
			
			//this.OnGenreSelected(true);
		}
		private void GenreTabControl_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.F10://Shift+F10は押されたとき
					if ((Control.ModifierKeys & Keys.Shift) != Keys.None) {
						this.ShowContextMenuOnSelectedTag();
					}
					break;
			}
		}

		private void GenreTabControl_KeyUp(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Apps://Appsは離されたとき
					this.ShowContextMenuOnSelectedTag();
					break;
			}
		}

		private void GenreTabControl_SelectedIndexChanged(object sender, EventArgs e) {
			this.OnGenreSelected(false);
		}

		private void gtp_ReloadRequested(object sender, EventArgs e) {
			this.SelectedGenre = (sender as GenreTabPage).Genre;
			this.OnGenreSelected(true);
		}
		private void gtp_ResultRemoved(object sender, EventArgs e) {
			this.SelectedGenre = (sender as GenreTabPage).Genre;
			this.OnGenreSelected(false);
		}

		private void OnGenreSelected(bool forceReload) {
			EventHandler<GenreTabSelectedEventArgs> handler = this.GenreSelected;
			if (null != handler) {
				GGenre genre = this.SelectedGenre;
				if (null != genre) {
					handler(this, new GenreTabSelectedEventArgs(genre, forceReload));
				}
			}
		}
		private void OnMaxNameLengthChanged() {
			foreach (TabPage tp in base.TabPages) {
				GenreTabPage gtp = tp as GenreTabPage;
				if (null == gtp) continue;
				string name = gtp.Genre.GenreName;
				if (this.MaxNameLength >= 0 && name.Length > this.MaxNameLength) {
					gtp.Text = name.Substring(0, this.MaxNameLength) + "...";
				} else {
					gtp.Text = name;
				}
			}
		}

		private void GenreTabControl_DrawItem(object sender, DrawItemEventArgs e) {
			if (TabAppearance.Normal == base.Appearance && TabRenderer.IsSupported) {
				TabItemState tabItemState = TabItemState.Normal;
				switch (e.State) {
					case DrawItemState.Selected: tabItemState = TabItemState.Selected; break;
					case DrawItemState.HotLight: tabItemState = TabItemState.Hot; break;
				}
				TabRenderer.DrawTabItem(e.Graphics, e.Bounds, tabItemState);
			}

			GenreTabPage gtp = base.TabPages[e.Index] as GenreTabPage;
			if(null == gtp) return;
			
			Color genreColor = gtp.Genre.GenreForeColor;
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

		private void ShowContextMenuOnSelectedTag() {
			int selIndex = base.SelectedIndex;
			GenreTabPage gtp = base.TabPages[selIndex] as GenreTabPage;
			if (null == gtp) {
				return;
			}

			Rectangle tabRect = base.GetTabRect(selIndex);
			gtp.ShowContextMenu(this.PointToScreen(new Point(tabRect.Left, tabRect.Bottom)));
		}

		protected override void WndProc(ref Message m) {
			switch ((WM)m.Msg) {
				case WM.LBUTTONDBLCLK://ダブルクリック
					this.OnGenreSelected(true);
					break;
			}
			base.WndProc(ref m);
		}
		
		#region IHasNewSettings<GenreTabControlSettings> Members
		public GenreTabControl.GenreTabControlSettings Settings {
			get { return this.settings; }
		}
		#endregion

		private void GenreTabControl_SizeChanged(object sender, EventArgs e) {
			this.RecalcurateRequiredHeight();
		}
		private void RecalcurateRequiredHeight() {
			int maxBottom = 0;
			for (int i = 0; i < base.TabCount; i++) {
				Rectangle rect = base.GetTabRect(i);
				int bot = rect.Bottom;
				if (maxBottom < bot) {
					maxBottom = bot;
				}
			}
			if (this.requiredHeight != maxBottom) {
				this.requiredHeight = maxBottom;
				EventHandler handler = this.RequiredHeightChanged;
				if (null != handler) {
					handler(this, EventArgs.Empty);
				}
			}
		}
		public int RequiredHeight {
			get { return this.requiredHeight; }
		}

		private void GenreTabControl_Layout(object sender, LayoutEventArgs e) {
			this.RecalcurateRequiredHeight();
		}
	}

	public sealed class GenreTabSelectedEventArgs : EventArgs {
		private readonly GGenre genre;
		private readonly bool forceReload;

		public GenreTabSelectedEventArgs(GGenre genre, bool forceReload) {
			this.genre = genre;
			this.forceReload = forceReload;
		}
		public GGenre Genre {
			get { return this.genre; }
		}
		public bool ForceReload {
			get { return this.forceReload; }
		}
	}
}
