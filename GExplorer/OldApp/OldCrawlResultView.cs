using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Yusen.GExplorer.OldCrawler;
using System.Reflection;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer.OldApp {
	public sealed partial class OldCrawlResultView : UserControl{
		private sealed class ContentListViewItem : ListViewItem {
			public ContentListViewItem(ContentAdapter ca, ListViewGroup packageGroup)
				: base(new string[] { ca.ContentId, ca.Title, ca.SeriesNumber, ca.Subtitle, ca.Duration, ca.Deadline, ca.Summary, "A" }) {
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
				}
			}
			public void SetBackColorIfFav(Color clr) {
				if (this.IsFav) {
					base.BackColor = clr;
				} else {
					base.BackColor = SystemColors.Window;
				}
			}
			public void SetForeColorIfNew(Color clr) {
				if (this.contentAdapter.IsNew) {
					base.ForeColor = clr;
				} else {
					base.ForeColor = SystemColors.WindowText;
				}
			}
		}
		
		private CrawlResult crawlResult;
		private ContentVisibilities contentVisibilities = ContentVisibilities.None;
		private FilterType filterType = FilterType.Normal;
		private int maxPageMenuItems = 16;
		private int maxExceptionMenuItems = 16;
		private Color newColor = Color.Red;
		private Color favColor = Color.LightYellow;

		private List<ListViewGroup> allLvgs = new List<ListViewGroup>();
		private List<ContentListViewItem> allClvis = new List<ContentListViewItem>();
		
		private Migemo migemo = null;
		private Regex filterRegex = null;

		public OldCrawlResultView() {
			InitializeComponent();
			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
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
			
			//Migemo初期化
			try {
				this.migemo = new Migemo(@"dict\migemo-dict");
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
		private Color FavColor {
			get { return this.favColor; }
			set {
				if (this.favColor != value) {
					this.favColor = value;
					this.SetFavColorAllIfFav();
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
		private void SetFavColorAllIfFav() {
			this.listView1.BeginUpdate();
			foreach (ContentListViewItem clvi in this.allClvis) {
				clvi.SetBackColorIfFav(this.favColor);
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
				clvi.SetBackColorIfFav(this.FavColor);
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
						//Utility.Browse((sender as ToolStripMenuItem).Tag as Uri);
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

		#region フィルタ関連
		private void tsbShowFilter_Click(object sender, EventArgs e) {
			this.FilterEnabled = this.FilterEnabled;
			if (!this.FilterEnabled) {
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
		
	}

	public enum FilterType {
		Normal,
		Migemo,
		Regex,
	}
}
