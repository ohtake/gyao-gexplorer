using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Yusen.GCrawler;
using System.Reflection;

namespace Yusen.GExplorer {
	public sealed partial class CrawlResultView : UserControl, IHasNewSettings<CrawlResultView.CrawlResultViewSettings> {
		public sealed class CrawlResultViewSettings : INewSettings<CrawlResultViewSettings> {
			private const int MaxMenuItems = 16;
			
			private readonly CrawlResultView owner;
			public CrawlResultViewSettings() : this(null) {
			}
			internal CrawlResultViewSettings(CrawlResultView owner) {
				this.owner = owner;
			}
			
			[XmlIgnore]
			[Browsable(false)]
			private bool HasOwner {
				get { return null != this.owner; }
			}
			
			[Category("カラム幅")]
			[DisplayName("[0] contents_id")]
			[Description("'contents_id'カラムの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ColWidthId {
				get {
					if (this.HasOwner) return this.owner.chId.Width;
					else return this.colWidthId;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chId.Width = value.Value;
					else this.colWidthId = value;
				}
			}
			private int? colWidthId;

			[Category("カラム幅")]
			[DisplayName("[1] タイトル")]
			[Description("'タイトル'カラムの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ColWidthTitle {
				get {
					if (this.HasOwner) return this.owner.chTitle.Width;
					else return this.colWidthTitle;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chTitle.Width = value.Value;
					else this.colWidthTitle = value;
				}
			}
			private int? colWidthTitle;

			[Category("カラム幅")]
			[DisplayName("[2] シリーズ番号")]
			[Description("'シリーズ番号'カラムの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ColWidthSeriesNumber {
				get {
					if (this.HasOwner) return this.owner.chSeriesNumber.Width;
					else return this.colWidthSeriesNumber;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chSeriesNumber.Width = value.Value;
					else this.colWidthSeriesNumber = value;
				}
			}
			private int? colWidthSeriesNumber;

			[Category("カラム幅")]
			[DisplayName("[3] サブタイトル")]
			[Description("'サブタイトル'カラムの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ColWidthSubtitle {
				get {
					if (this.HasOwner) return this.owner.chSubTitle.Width;
					else return this.colWidthSubtitle;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chSubTitle.Width = value.Value;
					else this.colWidthSubtitle = value;
				}
			}
			private int? colWidthSubtitle;

			[Category("カラム幅")]
			[DisplayName("[4] 番組時間")]
			[Description("'番組時間'カラムの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ColWidthDuration {
				get {
					if (this.HasOwner) return this.owner.chDuration.Width;
					else return this.colWidthDuration;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chDuration.Width = value.Value;
					else this.colWidthDuration = value;
				}
			}
			private int? colWidthDuration;

			[Category("カラム幅")]
			[DisplayName("[5] 配信期限")]
			[Description("'配信期限'カラムの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ColWidthDeadline {
				get {
					if (this.HasOwner) return this.owner.chDeadline.Width;
					else return this.colWidthDeadline;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chDeadline.Width = value.Value;
					else this.colWidthDeadline = value;
				}
			}
			private int? colWidthDeadline;

			[Category("カラム幅")]
			[DisplayName("[6] サマリー")]
			[Description("'サマリー'カラムの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ColWidthSummary {
				get {
					if (this.HasOwner) return this.owner.chSummary.Width;
					else return this.colWidthSummary;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chSummary.Width = value.Value;
					else this.colWidthSummary = value;
				}
			}
			private int? colWidthSummary;

			[Category("カラム幅")]
			[DisplayName("[7] 属性")]
			[Description("'属性'カラムの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ColWidthAttribs {
				get {
					if (this.HasOwner) return this.owner.chAttribs.Width;
					else return this.colWidthAttribs;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chAttribs.Width = value.Value;
					else this.colWidthAttribs = value;
				}
			}
			private int? colWidthAttribs;

			[Category("表示")]
			[DisplayName("パッケージでグループ化")]
			[Description("パッケージごとにコンテンツをグループ化します．Windows XP 以降でのみ有効です．")]
			[DefaultValue(true)]
			public bool ShowPackages {
				get {
					if (this.HasOwner) return this.owner.ShowPackages;
					else return this.showPackages;
				}
				set {
					if (this.HasOwner) this.owner.ShowPackages = value;
					else this.showPackages = value;
				}
			}
			private bool showPackages = true;
			
			[Category("動作")]
			[DisplayName("マウスホバーで選択")]
			[Description("マウスホバーでリストビューの項目を選択できるようにします．")]
			[DefaultValue(false)]
			public bool HoverSelect {
				get {
					if (this.HasOwner) return this.owner.HoverSelect;
					else return this.hoverSelect;
				}
				set {
					if (this.HasOwner) this.owner.HoverSelect = value;
					else this.hoverSelect = value;
				}
			}
			private bool hoverSelect = false;

			
			[Category("フィルタ")]
			[DisplayName("フィルタ")]
			[Description("フィルタツールバーを表示してフィルタをかけます．")]
			[DefaultValue(false)]
			public bool FilterEnabled {
				get {
					if (this.HasOwner) return this.owner.FilterEnabled;
					else return this.filterEnabled;
				}
				set {
					if (this.HasOwner) this.owner.FilterEnabled = value;
					else this.filterEnabled = value;
				}
			}
			private bool filterEnabled = false;

			[Category("フィルタ")]
			[DisplayName("フィルタ解除で文字列クリア")]
			[Description("フィルタを解除したときに自動的にフィルタ文字列を空にします．")]
			[DefaultValue(true)]
			public bool ClearFilterStringOnHideEnabled {
				get { return this.clearFilterStringOnHideEnabled; }
				set { this.clearFilterStringOnHideEnabled = value; }
			}
			private bool clearFilterStringOnHideEnabled = true;

			[Category("フィルタ")]
			[DisplayName("フィルタの種類")]
			[Description("フィルタの種類を指定します．「Normal」では文字列の単純な部分一致です．「Migemo」はC/Migemoによるフィルタで migemo.dll が必要です．「Regex」は任意の正規表現でフィルタをかけます．")]
			[DefaultValue(FilterType.Normal)]
			public FilterType FilterType {
				get {
					if (this.HasOwner) return this.owner.FilterType;
					else return this.filterType;
				}
				set {
					if (this.HasOwner) this.owner.FilterType = value;
					else this.filterType = value;
				}
			}
			private FilterType filterType = FilterType.Normal;

			[Category("表示")]
			[DisplayName("'ページ'メニューの最大値")]
			[Description("'ページ'メニューに表示する項目の最大値を指定します．負数にすると無制限になります．")]
			[DefaultValue(CrawlResultViewSettings.MaxMenuItems)]
			public int MaxPageMenuItems {
				get {
					if (this.HasOwner) return this.owner.MaxPageMenuItems;
					else return this.maxPageMenuItems;
				}
				set {
					if (this.HasOwner) this.owner.MaxPageMenuItems = value;
					else this.maxPageMenuItems = value;
				}
			}
			private int maxPageMenuItems = CrawlResultViewSettings.MaxMenuItems;
			
			[Category("表示")]
			[DisplayName("'例外'メニューの最大値")]
			[Description("'例外'メニューに表示する項目の最大値を指定します．負数にすると無制限になります．")]
			[DefaultValue(16)]
			public int MaxExceptionMenuItems {
				get {
					if (this.HasOwner) return this.owner.MaxExceptionMenuItems;
					else return this.maxExceptionMenuItems;
				}
				set {
					if (this.HasOwner) this.owner.MaxExceptionMenuItems = value;
					else this.maxExceptionMenuItems = value;
				}
			}
			private int maxExceptionMenuItems = CrawlResultViewSettings.MaxMenuItems;

			[XmlIgnore]//ColorはXMLシリアライズできない
			[Category("表示")]
			[DisplayName("新着の色")]
			[Description("新着コンテンツを指定された色で表示します．")]
			[DefaultValue(typeof(Color), "Red")]
			public Color NewColor {
				get {
					if (this.HasOwner) return this.owner.NewColor;
					else return this.newColor;
				}
				set {
					if (this.HasOwner) this.owner.NewColor = value;
					else this.newColor = value;
				}
			}
			private Color newColor = Color.Red;
			
			[Browsable(false)]
			[DefaultValue(typeof(XmlSerializableColor), "Red")]
			public XmlSerializableColor NewColorXmlSerializable {
				get { return new XmlSerializableColor(this.NewColor); }
				set { this.NewColor = value.ToColor(); }
			}

			[Category("表示")]
			[DisplayName("表示条件")]
			[Description("コンテンツを表示する条件を指定します．")]
			[DefaultValue(ContentVisibilities.PresetToumei)]
			public ContentVisibilities ContentVisibilities {
				get {
					if (this.HasOwner) return this.owner.ContentVisibilities;
					else return this.contentVisibilities;
				}
				set {
					if (this.HasOwner) this.owner.ContentVisibilities = value;
					else this.contentVisibilities = value;
				}
			}
			private ContentVisibilities contentVisibilities = ContentVisibilities.PresetToumei;
			
			
			#region INewSettings<CrawlResultViewSettings> Members
			public void ApplySettings(CrawlResultViewSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}

		private sealed class ContentListViewItem : ListViewItem {
			public ContentListViewItem(ContentAdapter ca, ListViewGroup packageGroup)
				: base(new string[] { ca.ContentId, ca.Title, ca.SeriesNumber, ca.Subtitle, ca.GTimeSpan.ToString(), ca.GDeadline.ToString(), ca.Summary, ca.Attributes }) {
				this.contentAdapter = ca;
				this.packageGroup = packageGroup;
			}
			private ContentAdapter contentAdapter;
			public ContentAdapter ContentAdapter {
				get { return this.contentAdapter; }
			}
			private bool isNg = false;
			public bool IsNg {
				get { return this.isNg; }
			}
			private bool isFav = false;
			public bool IsFav {
				get { return this.isFav; }
			}
			private ListViewGroup packageGroup;
			public ListViewGroup PackageGroup {
				get { return this.packageGroup; }
			}

			public void ResetNgFlag() {
				bool newVal = ContentPredicatesManager.NgManager.IsTrueFor(this.ContentAdapter);
				if (this.isNg != newVal) {
					this.isNg = newVal;
					if (newVal) {
						base.Font = new Font(base.Font, base.Font.Style | FontStyle.Strikeout);
					} else {
						base.Font = new Font(base.Font, base.Font.Style ^ FontStyle.Strikeout);
					}
				}
			}
			public void ResetFavFlag() {
				bool newVal = ContentPredicatesManager.FavManager.IsTrueFor(this.ContentAdapter);
				if (this.isFav != newVal) {
					this.isFav = newVal;
					if(newVal){
						base.Font = new Font(base.Font, base.Font.Style | FontStyle.Italic);
					}else{
						base.Font = new Font(base.Font, base.Font.Style ^ FontStyle.Italic);
					}
				}
			}
			public void SetForeColorIfNew(Color clr) {
				if (this.contentAdapter.IsNew) {
					base.ForeColor = clr;
				}
			}
		}
		
		public event EventHandler<ContentSelectionChangedEventArgs> ContentSelectionChanged;

		private CrawlResult crawlResult;
		private ContentVisibilities contentVisibilities = ContentVisibilities.None;
		private FilterType filterType = FilterType.Normal;
		private bool showPackages = true;
		private int maxPageMenuItems = 16;
		private int maxExceptionMenuItems = 16;
		private Color newColor = Color.Red;

		private List<ListViewGroup> allLvgs = new List<ListViewGroup>();
		private List<ContentListViewItem> allClvis = new List<ContentListViewItem>();
		
		private Migemo migemo = null;
		private Regex filterRegex = null;

		private CrawlResultViewSettings settings;

		public CrawlResultView() {
			InitializeComponent();
			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
			this.tsmiAdd.Font = new Font(this.tsmiAdd.Font, FontStyle.Bold);
			this.tsddbFilterTarget.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;

			//フィルタの対象のメニューを作成
			foreach (ColumnHeader ch in this.listView1.Columns) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(ch.Text);
				tsmi.Checked = true;
				tsmi.CheckOnClick = true;
				tsmi.Click += delegate {
					this.UpdateViewIfFilterEnabledAndHasFilterRegex();
				};
				this.tsddbFilterTarget.DropDownItems.Add(tsmi);
			}
		}
		
		private void CrawlResultView_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			//表示をクリア
			this.CrawlResult = null;
			
			//設定読み込み
			this.settings = new CrawlResultViewSettings(this);
			
			//Migemo初期化
			try {
				this.migemo = new Migemo(GlobalSettings.Instance.MigemoDictionaryFilename);
				this.Disposed += delegate {
					this.migemo.Dispose();
				};
			} catch (MigemoException ex) {
				this.migemo = null;
				this.tstbAnswer.Text = ex.Message;
				this.tsbOneFTypeMigemo.Enabled = false;
			}

			ContentPredicatesManager.NgManager.PredicatesChanged += new EventHandler(NgManager_PredicatesChanged);
			ContentPredicatesManager.FavManager.PredicatesChanged += new EventHandler(FavManager_PredicatesChanged);
			this.Disposed += delegate {
				ContentPredicatesManager.NgManager.PredicatesChanged -= new EventHandler(NgManager_PredicatesChanged);
				ContentPredicatesManager.FavManager.PredicatesChanged -= new EventHandler(FavManager_PredicatesChanged);
			};
		}
		
		internal CrawlResult CrawlResult {
			get {
				return this.crawlResult;
			}
			set {
				lock (this) {
					this.crawlResult = value;

					this.listView1.BeginUpdate();
					this.ClearAllItems();
					this.ShowPackages = this.ShowPackages;
					if (null != value) {
						this.CreateItems();
						this.SetNewColorAllIfNew();
						this.ResetAllNgFlags();
						this.ResetAllFavFlags();
						this.DisplayItems();
					}
					this.listView1.EndUpdate();

					this.CreateNormalPagesMenuItems(false);
					this.CreateExceptionsMenuItems(false);
				}
			}
		}
		private GGenre Genre {
			get { return this.CrawlResult.Genre; }
		}
		private ContentAdapter[] SelectedContents {
			get {
				List<ContentAdapter> conts = new List<ContentAdapter>();
				foreach (ContentListViewItem clvi in this.listView1.SelectedItems) {
					conts.Add(clvi.ContentAdapter);
				}
				return conts.ToArray();
			}
			set {
				List<ContentAdapter> conts = new List<ContentAdapter>(value);
				foreach(ContentListViewItem clvi in this.listView1.Items) {
					clvi.Selected = conts.Contains(clvi.ContentAdapter);
				}
			}
		}

		private bool CanUseMigemo {
			get {
				return null != this.migemo;
			}
		}
		
		private string FilterString {
			get {
				return this.tstbFilter.Text;
			}
			set {
				this.tstbFilter.Text = value;
				this.CreateAndSetFilterRegex();
			}
		}
		private FilterType FilterType {
			get { return this.filterType; }
			set {
				//Migemoチェック
				if (FilterType.Migemo == value && !this.CanUseMigemo) {
					value = FilterType.Normal;
				}
				//ボタンなどの更新
				this.tsbOneFTypeNormal.Checked = value == FilterType.Normal;
				this.tsbOneFTypeMigemo.Checked = value == FilterType.Migemo;
				this.tsbOneFTypeRegex.Checked = value == FilterType.Regex;
				//フィルタ適用
				if(value != this.filterType) {
					this.filterType = value;
					this.CreateAndSetFilterRegex();
				}
			}
		}
		private Regex FilterRegex {
			get {return this.filterRegex;}
			set {
				if (this.filterRegex != value) {
					this.filterRegex = value;
					this.DisplayItems();
				}
			}
		}
		private Color NewColor {
			get { return this.newColor; }
			set {
				if (this.newColor != value) {
					this.newColor = value;
					this.SetNewColorAllIfNew();
				}
			}
		}
		private ContentVisibilities ContentVisibilities {
			get { return this.contentVisibilities; }
			set {
				ContentVisibilities vis = ContentVisibilitiesUtility.EnsureVisible(value);
				if (vis == this.ContentVisibilities) return;
				
				this.contentVisibilities = vis;
				this.tscvsVisibilitiesSelector.ContentVisibilities = vis;
				this.tslVisibilities.Text = string.Format("[{0}]", ContentVisibilitiesUtility.ConvertToFlagsString(vis));
				this.DisplayItems();
			}
		}
		
		private bool ShowPackages {
			get { return this.showPackages; }
			set {
				this.showPackages = value;
				this.listView1.BeginUpdate();
				this.listView1.ShowGroups = value;
				this.DisplayItems();
				this.listView1.EndUpdate();
			}
		}
		private bool HoverSelect {
			get { return this.listView1.HoverSelection; }
			set {
				this.listView1.HotTracking = value;
				this.listView1.HoverSelection = value;
			}
		}
		private bool FilterEnabled {
			get {return this.tsbShowFilter.Checked; }
			set {
				this.tsbShowFilter.Checked = value;
				this.tsFilter.Visible = value;
				if (value) {
					this.tstbFilter.Focus();
				}
			}
		}
		private int MaxPageMenuItems {
			get { return this.maxPageMenuItems; }
			set {
				if(value != this.maxPageMenuItems) {
					this.maxPageMenuItems = value;
					this.CreateNormalPagesMenuItems(false);
				}
			}
		}
		private int MaxExceptionMenuItems {
			get { return this.maxExceptionMenuItems; }
			set {
				if(value != this.maxExceptionMenuItems) {
					this.maxExceptionMenuItems = value;
					this.CreateExceptionsMenuItems(false);
				}
			}
		}
		private void ClearAllItems() {
			this.allLvgs.Clear();
			this.allClvis.Clear();

			this.listView1.Items.Clear();
			this.listView1.Groups.Clear();
			this.listView1.ListViewItemSorter = null;
			
			this.tslNumber.Text = string.Empty;
			this.tslTime.Text = string.Empty;
		}
		private void CreateItems() {
			this.listView1.BeginUpdate();
			foreach (GPackage p in this.CrawlResult.Packages) {
				ListViewGroup group = new ListViewGroup(string.Format("<{0}> {1}", p.PackageId, p.PackageName));
				
				this.allLvgs.Add(group);
				foreach (GContent c in p.Contents) {
					ContentAdapter ca = new ContentAdapter(c);
					this.allClvis.Add(new ContentListViewItem(ca, group));
				}
			}
			this.listView1.EndUpdate();
		}
		private void SetNewColorAllIfNew() {
			this.listView1.BeginUpdate();
			foreach (ContentListViewItem clvi in this.allClvis) {
				clvi.SetForeColorIfNew(this.newColor);
			}
			this.listView1.EndUpdate();
		}
		private void ResetAllNgFlags() {
			this.listView1.BeginUpdate();
			foreach(ContentListViewItem clvi in this.allClvis) {
				clvi.ResetNgFlag();
			}
			this.listView1.EndUpdate();
		}
		private void ResetAllFavFlags() {
			this.listView1.BeginUpdate();
			foreach (ContentListViewItem clvi in this.allClvis) {
				clvi.ResetFavFlag();
			}
			this.listView1.EndUpdate();
		}

		private void DisplayItems(){
			if (null == this.CrawlResult) return;

			this.listView1.BeginUpdate();

			this.listView1.Items.Clear();
			this.listView1.Groups.Clear();
			
			this.listView1.Groups.AddRange(this.allLvgs.ToArray());
			
			int aboned = 0;
			int filtered = 0;
			Regex filter = this.FilterEnabled ? this.FilterRegex : null;
			List<bool> filterTarges = this.GetFilterTargetList();
			
			ContentVisibilities vis = this.ContentVisibilities;
			bool showNgTrue = (vis & ContentVisibilities.NgTrue) != ContentVisibilities.None;
			bool showNgFalse = (vis & ContentVisibilities.NgFalse) != ContentVisibilities.None;
			bool showFavTrue = (vis & ContentVisibilities.FavTrue) != ContentVisibilities.None;
			bool showFavFalse = (vis & ContentVisibilities.FavFalse) != ContentVisibilities.None;
			bool showNewTrue = (vis & ContentVisibilities.NewTrue) != ContentVisibilities.None;
			bool showNewFalse = (vis & ContentVisibilities.NewFalse) != ContentVisibilities.None;
			
			foreach (ContentListViewItem clvi in this.allClvis) {
				//あぼ～ん処理
				if (clvi.IsNg) {
					if (!showNgTrue) goto l_abone;
				} else {
					if (!showNgFalse) goto l_abone;
				}
				if (clvi.IsFav) {
					if (!showFavTrue) goto l_abone;
				} else {
					if (!showFavFalse) goto l_abone;
				}
				if (clvi.ContentAdapter.IsNew) {
					if (!showNewTrue) goto l_abone;
				} else {
					if (!showNewFalse) goto l_abone;
				}
				
				//フィルタ処理
				if (null != filter) {
					Match match;
					for(int i=0; i<filterTarges.Count; i++) {
						if(filterTarges[i]) {
							match = filter.Match(clvi.SubItems[i].Text);
							if (match.Success) goto l_add;
						}
					}
					goto l_filter;
				}
				goto l_add;
			l_abone:
				aboned++;
				continue;
			l_filter:
				filtered++;
				continue;
			l_add:
				clvi.Group = clvi.PackageGroup;
				clvi.Selected = false;
				this.listView1.Items.Add(clvi);
				continue;
			}
			
			this.listView1.EndUpdate();
			
			if (this.FilterEnabled) {
				this.tslNumber.Text = string.Format("{0}+{1}+{2}", this.listView1.Items.Count, filtered, aboned);
			} else {
				this.tslNumber.Text = string.Format("{0}+{1}", this.listView1.Items.Count, aboned);
			}
			this.tslTime.Text = string.Format("({0})", this.CrawlResult.Time.ToString("MM/dd ddd HH:mm"));
		}
		private void CreateNormalPagesMenuItems(bool createAll) {
			this.tsddbNormalPages.DropDownItems.Clear();
			List<ToolStripItem> menuItems = new List<ToolStripItem>();
			int pageCount = 0;
			if (null != this.CrawlResult) {
				pageCount = this.CrawlResult.VisitedPages.Count;
				foreach (Uri page in this.CrawlResult.VisitedPages) {
					if(!createAll && menuItems.Count == this.MaxPageMenuItems) {
						menuItems.Add(new ToolStripSeparator());
						menuItems.Add(new ToolStripMenuItem(
							string.Format("省略せずに全{0}項目を展開する(&E)", this.CrawlResult.VisitedPages.Count),
							null,
							new EventHandler(delegate {
							this.CreateNormalPagesMenuItems(true);
						})));
						break;
					}
					ToolStripMenuItem tsmi = new ToolStripMenuItem(page.PathAndQuery);
					tsmi.Tag = page;
					tsmi.Click += delegate(object sender, EventArgs e) {
						Utility.Browse((sender as ToolStripMenuItem).Tag as Uri);
					};
					menuItems.Add(tsmi);
				}
			}
			this.tsddbNormalPages.DropDownItems.AddRange(menuItems.ToArray());
			this.tsddbNormalPages.Text = pageCount.ToString();
			this.tsddbNormalPages.Enabled = this.tsddbNormalPages.HasDropDownItems;
		}
		private void CreateExceptionsMenuItems(bool createAll) {
			this.tsddbExceptions.DropDownItems.Clear();
			List<ToolStripItem> menuItems = new List<ToolStripItem>();
			int exceptionCount = 0;
			if(null != this.CrawlResult) {
				exceptionCount = this.CrawlResult.IgnoredExceptions.Count;
				foreach (Exception ex in this.CrawlResult.IgnoredExceptions) {
					if(!createAll && menuItems.Count == this.MaxPageMenuItems) {
						menuItems.Add(new ToolStripSeparator());
						menuItems.Add(new ToolStripMenuItem(
							string.Format("省略せずに全{0}項目を展開する(&E)", this.CrawlResult.IgnoredExceptions.Count),
							null,
							new EventHandler(delegate {
							this.CreateExceptionsMenuItems(true);
						})));
						break;
					}
					ToolStripMenuItem tsmi = new ToolStripMenuItem(ex.Message);
					tsmi.Tag = ex;
					tsmi.Click += delegate(object sender, EventArgs e) {
						this.exceptionDialog1.Exception =  (sender as ToolStripMenuItem).Tag as Exception;
						this.exceptionDialog1.ShowDialog();
					};
					menuItems.Add(tsmi);
				}
			}
			this.tsddbExceptions.DropDownItems.AddRange(menuItems.ToArray());
			this.tsddbExceptions.Text = exceptionCount.ToString();
			this.tsddbExceptions.Enabled = this.tsddbExceptions.HasDropDownItems;
		}
		private void CreateAndSetFilterRegex() {
			Regex regex = null;
			string regexCompileErrMessage = null;
			if (!string.IsNullOrEmpty(this.FilterString)) {
				switch (this.FilterType) {
					case FilterType.Normal:
						try {
							regex = new Regex(Regex.Escape(this.FilterString), RegexOptions.IgnoreCase);
						} catch (ArgumentException e) {
							regexCompileErrMessage = e.Message;
							goto failed;
						}
						break;
					case FilterType.Migemo:
						string ans = this.migemo.Query(this.FilterString);
						try {
							regex = new Regex(ans, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
						} catch (ArgumentException e) {
							regexCompileErrMessage = e.Message;
							goto failed;
						}
						break;
					case FilterType.Regex:
						try {
							regex = new Regex(this.FilterString, RegexOptions.IgnoreCase);
						} catch (ArgumentException e) {
							regexCompileErrMessage = e.Message;
							goto failed;
						}
						break;
				}
			}
			this.tstbFilter.BackColor = SystemColors.Window;
			this.tstbAnswer.Text = (null == regex) ? string.Empty : regex.ToString();
			this.FilterRegex = regex;
			return;
		failed:
			this.tstbFilter.BackColor= Color.Yellow;
			if(!string.IsNullOrEmpty(regexCompileErrMessage)){
				this.tstbAnswer.Text = regexCompileErrMessage;
			}
			this.FilterRegex = null;
			return;
		}
		private void UpdateViewIfFilterEnabledAndHasFilterRegex() {
			if(this.FilterEnabled && null != this.FilterRegex) {
				this.DisplayItems();
			}
		}

		private void NgManager_PredicatesChanged(object sender, EventArgs e) {
			this.ResetAllNgFlags();
			if ((this.ContentVisibilities & ContentVisibilities.NgDontCare) != ContentVisibilities.NgDontCare) {
				this.DisplayItems();
			}
		}
		private void FavManager_PredicatesChanged(object sender, EventArgs e) {
			this.ResetAllFavFlags();
			if ((this.ContentVisibilities & ContentVisibilities.FavDontCare) != ContentVisibilities.FavDontCare) {
				this.DisplayItems();
			}
		}

		private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if (null != this.ContentSelectionChanged) {
				this.ContentSelectionChanged(this, new ContentSelectionChangedEventArgs((e.Item as ContentListViewItem).ContentAdapter, e.IsSelected));
			}
		}
		private void listView1_DoubleClick(object sender, EventArgs e) {
			this.tsmiAdd.PerformClick();
		}
		private void listView1_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					this.tsmiAdd.PerformClick();
					break;
				case Keys.A:
					if (Keys.None != (Control.ModifierKeys & Keys.Control)) {
						this.listView1.BeginUpdate();
						foreach (ListViewItem lvi in this.listView1.Items) {
							lvi.Selected = true;
						}
						this.listView1.EndUpdate();
					}
					break;
				case Keys.Escape:
					this.listView1.BeginUpdate();
					foreach (ListViewItem lvi in this.listView1.Items) {
						lvi.Selected = false;
					}
					this.listView1.EndUpdate();
					break;
			}
		}
		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e) {
			if(this.ShowPackages) {
				this.listView1.ShowGroups = false;
			}

			ListViewItemStackableComparer comparer = this.listView1.ListViewItemSorter as ListViewItemStackableComparer;
			if(null == comparer){
				comparer = new ListViewItemStackableComparer();
			}else{
				comparer = comparer.Clone();
			}
			comparer.StackColumnIndex(e.Column);
			this.listView1.ListViewItemSorter = comparer;
		}
		private void listView1_ItemDrag(object sender, ItemDragEventArgs e) {
			DataObject dobj = new DataObject();
			dobj.SetText(ContentAdapter.GetNamesAndUris(this.SelectedContents));
			dobj.SetData(typeof(ContentAdapter[]), this.SelectedContents);
			this.listView1.DoDragDrop(dobj, DragDropEffects.Copy);
		}

		private void cmsContent_Opening(object sender, CancelEventArgs e) {
			if (0 == this.SelectedContents.Length) {
				e.Cancel = true;
			}
		}
		#region コンテキストメニューの項目
		private void tsmiAdd_Click(object sender, EventArgs e) {
			PlayList.Instance.BeginUpdate();
			foreach (ContentAdapter cont in this.SelectedContents) {
				PlayList.Instance.AddIfNotExists(cont);
			}
			PlayList.Instance.EndUpdate();
		}
		private void tsmiAddWithComment_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "コメントを付けてプレイリストに追加";
			this.inputBoxDialog1.Message = "付加するコメントを入力してください．";
			this.inputBoxDialog1.Input = string.Empty;
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					string comment = this.inputBoxDialog1.Input;
					PlayList.Instance.BeginUpdate();
					foreach (ContentAdapter cont in this.SelectedContents) {
						cont.Comment = comment;
						PlayList.Instance.AddIfNotExists(cont);
					}
					PlayList.Instance.EndUpdate();
					break;
			}
		}
		private void tsmiPlay_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				PlayerForm.Play(cont);
			}
		}
		private void tsmiPlayWithBrowser_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				Utility.Browse(cont.PlayerPageUri);
			}
		}
		private void tsmiBroseDetail_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				Utility.Browse(cont.DetailPageUri);
			}
		}
		private void tsmiCopyName_Click(object sender, EventArgs e) {
			string text = ContentAdapter.GetNames(this.SelectedContents);
			if (!string.IsNullOrEmpty(text)) {
				Clipboard.SetText(text);
			}
		}
		private void tsmiCopyDetailUri_Click(object sender, EventArgs e) {
			string text = ContentAdapter.GetUris(this.SelectedContents);
			if (!string.IsNullOrEmpty(text)) {
				Clipboard.SetText(text);
			}
		}
		private void tsmiCopyNameAndDetailUri_Click(object sender, EventArgs e) {
			string text = ContentAdapter.GetNamesAndUris(this.SelectedContents);
			if (!string.IsNullOrEmpty(text)) {
				Clipboard.SetText(text);
			}
		}
		private void tscapmiCopyProperty_PropertySelected(object sender, CAPropertySelectedEventArgs e) {
			string text = ContentAdapter.GetPropertyValueLines(this.SelectedContents, e.PropertyInfo);
			if (!string.IsNullOrEmpty(text)) {
				Clipboard.SetText(text);
			}
		}
		private void tsmiCatalogNormal_Click(object sender, EventArgs e) {
			BrowserForm.Browse(this.SelectedContents);
		}
		private void tsmiCatalogImageSmall_Click(object sender, EventArgs e) {
			Uri[] images = Array.ConvertAll<ContentAdapter, Uri>(this.SelectedContents, new Converter<ContentAdapter, Uri>(delegate(ContentAdapter input) {
				return input.ImageSmallUri;
			}));
			BrowserForm.Browse(images);
		}
		private void tsmiCatalogImageLarge_Click(object sender, EventArgs e) {
			Uri[] images = Array.ConvertAll<ContentAdapter, Uri>(this.SelectedContents, new Converter<ContentAdapter, Uri>(delegate(ContentAdapter input) {
				return input.ImageLargeUri;
			}));
			BrowserForm.Browse(images);
		}
		private void tsnfmiNgFav_SubmenuSelected(object sender, ContentSelectionRequiredEventArgs e) {
			e.Selection = this.SelectedContents;
		}
		private void tsucmiCommandRoot_UserCommandSelected(object sender, UserCommandSelectedEventArgs e) {
			e.UserCommand.Execute(this.SelectedContents);
		}
		private void tsmiRemoveCache_Click(object sender, EventArgs e) {
			string title = "キャッシュの削除";
			ContentAdapter[] selConts = this.SelectedContents;
			switch (MessageBox.Show(string.Format("選択された {0} 個のコンテンツのキャッシュを削除しますか？", selConts.Length), title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
				case DialogResult.Yes:
					break;
				default:
					return;
			}
			int succeeded = 0;
			int failed = 0;
			foreach (ContentAdapter cont in selConts) {
				if (Cache.Instance.ContentCacheController.RemoveCache(cont.ContentKey)) {
					succeeded++;
				} else {
					failed++;
				}
			}
			
			MessageBox.Show(string.Format("削除成功数: {0}\n削除失敗数: {1}", succeeded, failed), title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		#endregion
		#region フィルタ関連
		private void tsbShowFilter_Click(object sender, EventArgs e) {
			this.FilterEnabled = this.FilterEnabled;
			if (this.settings.ClearFilterStringOnHideEnabled && !this.FilterEnabled) {
				this.FilterString = string.Empty;
			}
			this.DisplayItems();
		}
		private void tstbFilter_TextChanged(object sender, EventArgs e) {
			this.timerFilter.Start();
		}
		private void timerFilter_Tick(object sender, EventArgs e) {
			this.timerFilter.Stop();
			this.CreateAndSetFilterRegex();
		}
		private void tsbOneFTypeNormal_Click(object sender, EventArgs e) {
			this.FilterType = FilterType.Normal;
		}
		private void tsbOneFTypeMigemo_Click(object sender, EventArgs e) {
			this.FilterType = FilterType.Migemo;
		}
		private void tsbOneFTypeRegex_Click(object sender, EventArgs e) {
			this.FilterType = FilterType.Regex;
		}
		private List<bool> GetFilterTargetList() {
			List<bool> targets = new List<bool>(this.listView1.Columns.Count);
			bool afterSeparator = false;
			foreach (ToolStripItem tsi in this.tsddbFilterTarget.DropDownItems) {
				if (!afterSeparator) {
					if (tsi is ToolStripSeparator) {
						afterSeparator = true;
					}
					continue;
				}
				ToolStripMenuItem tsmi = tsi as ToolStripMenuItem;
				targets.Add(tsmi.Checked);
			}
			return targets;
		}
		private void ChangeAllFilterTargetHelper(Action<ToolStripMenuItem> action) {
			bool afterSeparator = false;
			foreach (ToolStripItem tsi in this.tsddbFilterTarget.DropDownItems) {
				if (!afterSeparator) {
					if (tsi is ToolStripSeparator) {
						afterSeparator = true;
					}
					continue;
				}
				ToolStripMenuItem tsmi = tsi as ToolStripMenuItem;
				action(tsmi);
			}
		}
		private void tsmiFilterTargetCheckAll_Click(object sender, EventArgs e) {
			this.ChangeAllFilterTargetHelper(new Action<ToolStripMenuItem>(delegate(ToolStripMenuItem tsmi) {
				tsmi.Checked = true;
			}));
			this.UpdateViewIfFilterEnabledAndHasFilterRegex();
		}
		private void tsmiFilterTargetUncheckAll_Click(object sender, EventArgs e) {
			this.ChangeAllFilterTargetHelper(new Action<ToolStripMenuItem>(delegate(ToolStripMenuItem tsmi) {
				tsmi.Checked = false;
			}));
			this.UpdateViewIfFilterEnabledAndHasFilterRegex();
		}
		private void tsmiFilterTargetToggleAll_Click(object sender, EventArgs e) {
			this.ChangeAllFilterTargetHelper(new Action<ToolStripMenuItem>(delegate(ToolStripMenuItem tsmi) {
				tsmi.Checked = !tsmi.Checked;
			}));
			this.UpdateViewIfFilterEnabledAndHasFilterRegex();
		}
		#endregion
		#region 表示条件
		private void tscvsVisibilitiesSelector_ContentVisibilitiesChanged(object sender, EventArgs e) {
			this.ContentVisibilities = this.tscvsVisibilitiesSelector.ContentVisibilities;
		}
		private void tscvsVisibilitiesSelector_CloseClick(object sender, EventArgs e) {
			this.tsddbVisibilities.DropDown.Close();
		}
		#endregion
		
		#region IHasNewSettings<CrawlResultViewSettings> Members
		public CrawlResultView.CrawlResultViewSettings Settings {
			get { return this.settings; }
		}
		#endregion

	}

	public enum FilterType {
		Normal,
		Migemo,
		Regex,
	}
}
