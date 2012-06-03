﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	sealed partial class CrawlResultView : UserControl, IHasSettings<GenreListViewSettings>{
		private sealed class ContentListViewItem : ListViewItem {
			public ContentListViewItem(ContentAdapter ca, ListViewGroup packageGroup)
				: base(new string[] { ca.ContentId, ca.Title, ca.EpisodeNumber, ca.SubTitle, ca.GTimeSpan.ToString(), ca.Deadline, ca.LongDescription, ca.Attributes }) {
				this.contentAdapter = ca;
				this.packageGroup = packageGroup;
				this.RefreshNgCached();
			}
			private ContentAdapter contentAdapter;
			public ContentAdapter ContentAdapter {
				get { return this.contentAdapter; }
			}
			private bool isNgCached;
			public bool IsNgCached {
				get { return this.isNgCached; }
			}
			private ListViewGroup packageGroup;
			public ListViewGroup PackageGroup {
				get { return this.packageGroup; }
			}

			public void RefreshNgCached() {
				this.isNgCached = NgContentsManager.Instance.IsNgContent(this.ContentAdapter);
			}
		}
		
		public event EventHandler<ContentSelectionChangedEventArgs> ContentSelectionChanged;
		public event EventHandler<ManuallyCacheDeletedEventArgs> ManuallyCacheDeleted;
		
		private CrawlResult crawlResult;
		private AboneType aboneType = AboneType.Sabori;
		private FilterType filterType = FilterType.Migemo;
		private Color newColor = Color.Red;
		private int maxPageMenuItems = 16;
		private int maxExceptionMenuItems = 16;
		
		private List<ListViewGroup> allLvgs = new List<ListViewGroup>();
		private List<ContentListViewItem> allClvis = new List<ContentListViewItem>();
		
		private Migemo migemo = null;
		private Regex filterRegex = null;

		public CrawlResultView() {
			InitializeComponent();
		}
		
		private void CrawlResultView_Load(object sender, EventArgs e) {
			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
			this.tsmiAdd.Font = new Font(this.tsmiAdd.Font, FontStyle.Bold);
			this.tsddbSettings.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;
			this.tsmiAboneType.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;
			this.tsmiFilterType.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;
			this.tsddbFilterTarget.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;
			
			//Migemo初期化
			try {
				this.migemo = new Migemo(GlobalSettings.Instance.MigemoDictionaryFilename);
				this.Disposed += delegate {
					this.migemo.Dispose();
				};
			} catch (MigemoException ex) {
				this.migemo = null;
				this.tstbAnswer.Text = ex.Message;
			}
			
			//あぼ～ん方法のメニュー作成
			this.tsmiAboneType.DropDownItems.Clear();
			foreach (AboneType atype in Enum.GetValues(typeof(AboneType))) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(atype.ToString());
				tsmi.Tag = atype;
				tsmi.Click += delegate(object sender2, EventArgs e2) {
					this.AboneType = (AboneType)(sender2 as ToolStripMenuItem).Tag;
				};
				this.tsmiAboneType.DropDownItems.Add(tsmi);
			}
			this.AboneType = this.AboneType;
			
			//フィルタ用のメニュー作成
			this.tsmiFilterType.DropDownItems.Clear();
			foreach (FilterType ftype in Enum.GetValues(typeof(FilterType))) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(ftype.ToString());
				tsmi.Tag = ftype;
				tsmi.Click += delegate(object sender2, EventArgs e2) {
					this.FilterType = (FilterType)(sender2 as ToolStripMenuItem).Tag;
				};
				//Migemoが使用不可の場合は選択不可に
				if (FilterType.Migemo == ftype && !this.CanUseMigemo) {
					tsmi.Enabled = false;
					this.tsbOneFTypeMigemo.Enabled = false;
				}
				this.tsmiFilterType.DropDownItems.Add(tsmi);
			}
			this.FilterType = this.FilterType;
			
			//フィルタの対象のメニューを作成
			foreach(ColumnHeader ch in this.listView1.Columns) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(ch.Text);
				tsmi.Checked = true;
				tsmi.CheckOnClick = true;
				tsmi.Click += delegate {
					this.UpdateViewIfFilterEnabledAndHasFilterRegex();
				};
				this.tsddbFilterTarget.DropDownItems.Add(tsmi);
			}

			this.CreateUserCommandsMenuItems();
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
			this.Disposed += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			};
			
			NgContentsManager.Instance.NgContentsChanged += new EventHandler(NgContentsManager_NgContentsChanged);
			this.Disposed += delegate {
				NgContentsManager.Instance.NgContentsChanged -= new EventHandler(NgContentsManager_NgContentsChanged);
			};
			
		}
		
		public CrawlResult CrawlResult {
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
						this.DisplayItems();
					}
					this.listView1.EndUpdate();

					this.CreateNormalPagesMenuItems(false);
					this.CreateExceptionsMenuItems(false);
				}
			}
		}
		public GGenre Genre {
			get { return this.CrawlResult.Genre; }
		}
		public ContentAdapter[] SelectedContents {
			get {
				List<ContentAdapter> conts = new List<ContentAdapter>();
				foreach (ContentListViewItem clvi in this.listView1.SelectedItems) {
					conts.Add(clvi.ContentAdapter);
				}
				return conts.ToArray();
			}
			private set {
				List<ContentAdapter> conts = new List<ContentAdapter>(value);
				foreach(ContentListViewItem clvi in this.listView1.Items) {
					clvi.Selected = conts.Contains(clvi.ContentAdapter);
				}
			}
		}

		public bool CanUseMigemo {
			get {
				return null != this.migemo;
			}
		}
		
		[DefaultValue("")]
		public string FilterString {
			get {
				return this.tstbFilter.Text;
			}
			set {
				this.tstbFilter.Text = value;
				this.CreateAndSetFilterRegex();
			}
		}
		public FilterType FilterType {
			get { return this.filterType; }
			set {
				//Migemoチェック
				if (FilterType.Migemo == value && !this.CanUseMigemo) {
					value = FilterType.Normal;
				}
				//ボタンなどの更新
				foreach(ToolStripMenuItem tsmi in this.tsmiFilterType.DropDownItems) {
					bool active = value == (FilterType)tsmi.Tag;
					tsmi.Checked = active;
				}
				this.tsbOneFTypeNormal.Checked = false;
				this.tsbOneFTypeMigemo.Checked = false;
				this.tsbOneFTypeRegex.Checked = false;
				switch(value) {
					case FilterType.Normal:
						this.tsbOneFTypeNormal.Checked = true;
						break;
					case FilterType.Migemo:
						this.tsbOneFTypeMigemo.Checked = true;
						break;
					case FilterType.Regex:
						this.tsbOneFTypeRegex.Checked = true;
						break;
				}
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
					this.UpdateView();
				}
			}
		}
		
		public AboneType AboneType {
			get { return this.aboneType; }
			set {
				foreach(ToolStripMenuItem tsmi in this.tsmiAboneType.DropDownItems) {
					tsmi.Checked = value == (AboneType)tsmi.Tag;
				}
				this.tsbOneAboneToumei.Checked = false;
				this.tsbOneAboneSabori.Checked = false;
				this.tsbOneAboneHakidame.Checked = false;
				switch(value) {
					case AboneType.Toumei:
						this.tsbOneAboneToumei.Checked = true;
						break;
					case AboneType.Sabori:
						this.tsbOneAboneSabori.Checked = true;
						break;
					case AboneType.Hakidame:
						this.tsbOneAboneHakidame.Checked = true;
						break;
				}
				if(value != this.aboneType) {
					this.aboneType = value;
					this.UpdateView();
				}
			}
		}
		public bool ShowPackages {
			get { return this.tsmiShowPackages.Checked; }
			set {
				this.tsmiShowPackages.CheckState = value ? CheckState.Checked : CheckState.Unchecked;
				this.listView1.ShowGroups = value;
			}
		}
		public bool HoverSelect {
			get { return this.tsmiHoverSelect.Checked; }
			set {
				this.tsmiHoverSelect.Checked = value;
				this.listView1.HotTracking = value;
				this.listView1.HoverSelection = value;
				this.listView1.Activation = value ? ItemActivation.OneClick : ItemActivation.Standard;
			}
		}
		public bool MultiSelect {
			get { return this.tsmiMultiSelect.Checked; }
			set {
				this.tsmiMultiSelect.Checked = value;
				this.listView1.MultiSelect = value;
			}
		}
		public bool ClearFilterStringOnHideEnabled {
			get { return this.tsmiClearFilterStringOnHide.Checked; }
			set { this.tsmiClearFilterStringOnHide.Checked = value; }
		}
		public bool FilterEnabled {
			get {return this.tsbShowFilter.Checked; }
			set {
				this.tsbShowFilter.Checked = value;
				this.tsFilter.Visible = value;
			}
		}
		public Color NewColor {
			get { return this.newColor; }
			set {
				if (value != this.newColor) {
					this.newColor = value;
					this.UpdateView();
				}
			}
		}
		public int MaxPageMenuItems {
			get { return this.maxPageMenuItems; }
			set {
				if(value != this.maxPageMenuItems) {
					this.maxPageMenuItems = value;
					this.CreateNormalPagesMenuItems(false);
				}
			}
		}
		public int MaxExceptionMenuItems {
			get { return this.maxExceptionMenuItems; }
			set {
				if(value != this.maxExceptionMenuItems) {
					this.maxExceptionMenuItems = value;
					this.CreateExceptionsMenuItems(false);
				}
			}
		}
		public void FillSettings(GenreListViewSettings settings) {
			settings.ColWidthId = this.chId.Width;
			settings.ColWidthTitle = this.chTitle.Width;
			settings.ColWidthEpisode = this.chEpisode.Width;
			settings.ColWidthSubtitle = this.chSubTitle.Width;
			settings.ColWidthDuration = this.chDuration.Width;
			settings.ColWidthDeadline = this.chDeadline.Width;
			settings.ColWidthLongDesc = this.chDescription.Width;
			settings.ColWidthAttribs = this.chAttribs.Width;
			settings.AboneType = this.AboneType;
			settings.ShowPackages = this.ShowPackages;
			settings.HoverSelect = this.HoverSelect;
			settings.MultiSelect = this.MultiSelect;
			settings.ClearFilterStringOnHideEnabled = this.ClearFilterStringOnHideEnabled;
			settings.FilterEnabled = this.FilterEnabled;
			settings.FilterType = this.FilterType;
			settings.NewColor = this.NewColor;
			settings.MaxPageMenuItems = this.MaxPageMenuItems;
			settings.MaxExceptionMenuItems = this.MaxExceptionMenuItems;
		}
		public void ApplySettings(GenreListViewSettings settings) {
			this.chId.Width = settings.ColWidthId ?? this.chId.Width;
			this.chTitle.Width = settings.ColWidthTitle ?? this.chTitle.Width;
			this.chEpisode.Width = settings.ColWidthEpisode ?? this.chEpisode.Width;
			this.chSubTitle.Width = settings.ColWidthSubtitle ?? this.chSubTitle.Width;
			this.chDuration.Width = settings.ColWidthDuration ?? this.chDuration.Width;
			this.chDeadline.Width = settings.ColWidthDeadline ?? this.chDeadline.Width;
			this.chDescription.Width = settings.ColWidthLongDesc ?? this.chDescription.Width;
			this.chAttribs.Width = settings.ColWidthAttribs ?? this.chAttribs.Width;
			this.AboneType = settings.AboneType ?? this.AboneType;
			this.ShowPackages = settings.ShowPackages ?? this.ShowPackages;
			this.HoverSelect = settings.HoverSelect ?? this.HoverSelect;
			this.MultiSelect = settings.MultiSelect ?? this.MultiSelect;
			this.ClearFilterStringOnHideEnabled = settings.ClearFilterStringOnHideEnabled ?? this.ClearFilterStringOnHideEnabled;
			this.FilterEnabled = settings.FilterEnabled ?? this.FilterEnabled;
			this.FilterType = settings.FilterType ?? this.FilterType;
			this.NewColor = settings.NewColor ?? this.NewColor;
			this.MaxPageMenuItems = settings.MaxPageMenuItems ?? this.MaxPageMenuItems;
			this.MaxExceptionMenuItems = settings.MaxExceptionMenuItems ?? this.MaxExceptionMenuItems;
		}
		private void ClearAllItems() {
			this.allLvgs.Clear();
			this.allClvis.Clear();

			this.listView1.Items.Clear();
			this.listView1.Groups.Clear();
			this.listView1.ListViewItemSorter = null;
			
			this.tslGenre.Text = string.Empty;
			this.tslNumber.Text = string.Empty;
			this.tslTime.Text = string.Empty;
		}
		private void CreateItems() {
			foreach (GPackage p in this.CrawlResult.Packages) {
				ListViewGroup group = new ListViewGroup(p.ToString());
				this.allLvgs.Add(group);
				foreach (GContent c in p.Contents) {
					ContentAdapter ca = new ContentAdapter(c);
					this.allClvis.Add(new ContentListViewItem(ca, group));
				}
			}
			
			this.tslGenre.ForeColor = this.Genre.GenreColor;
			this.tslGenre.Text = "[" + this.Genre.GenreName + "]";
		}
		private void RefreshAllNgCached() {
			foreach(ContentListViewItem clvi in this.allClvis) {
				clvi.RefreshNgCached();
			}
		}
		private void DisplayItems(){
			if (null == this.CrawlResult) return;
			this.listView1.Groups.Clear();
			this.listView1.Items.Clear();
			
			this.listView1.Groups.AddRange(this.allLvgs.ToArray());
			
			int aboned = 0;
			int filtered = 0;
			Regex filter = this.FilterEnabled ? this.FilterRegex : null;
			List<bool> filterTarges = this.GetFilterTargetList();
			
			foreach (ContentListViewItem clvi in this.allClvis) {
				//NG処理
				bool isNg = clvi.IsNgCached;
				switch (this.AboneType) {
					case AboneType.Hakidame:
						isNg = !isNg;
						goto case AboneType.Toumei;
					case AboneType.Toumei:
						if (isNg) {
							aboned++;
							continue;
						}
						break;
				}
				//フィルタ処理
				if (null != filter) {
					Match match;
					for(int i=0; i<filterTarges.Count; i++) {
						if(filterTarges[i]) {
							match = filter.Match(clvi.SubItems[i].Text);
							if(match.Success) goto unfiltered;
						}
					}
					filtered++;
					continue;
				}
			unfiltered:

				//色づけ
				clvi.ForeColor = SystemColors.WindowText;
				if (! clvi.ContentAdapter.FromCache) {
					clvi.ForeColor = this.NewColor;
				}
				if (isNg && AboneType.Sabori == this.AboneType) {
					clvi.ForeColor = SystemColors.GrayText;
				}

				clvi.Group = clvi.PackageGroup;
				this.listView1.Items.Add(clvi);
			}

			this.tslNumber.Text = this.listView1.Items.Count.ToString() + "+" + filtered.ToString() + "+" + aboned.ToString();
			this.tslTime.Text = string.Format("({0})", this.CrawlResult.Time.ToString("MM/dd ddd HH:mm"));
		}
		private void CreateNormalPagesMenuItems(bool createAll) {
			this.tsddbNormalPages.DropDownItems.Clear();
			List<ToolStripItem> menuItems = new List<ToolStripItem>();
			if (null != this.CrawlResult) {
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
			this.tsddbNormalPages.Enabled = this.tsddbNormalPages.HasDropDownItems;
		}
		private void CreateExceptionsMenuItems(bool createAll) {
			this.tsddbExceptions.DropDownItems.Clear();
			List<ToolStripItem> menuItems = new List<ToolStripItem>();
			if(null != this.CrawlResult) {
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

		private void CreateUserCommandsMenuItems() {
			this.tsmiUserCommands.DropDownItems.Clear();
			List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();
			foreach (UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(uc.Title);
				tsmi.Tag = uc;
				tsmi.Click += delegate(object sender, EventArgs e) {
					ToolStripMenuItem tsmi2 = sender as ToolStripMenuItem;
					if (null != tsmi2) {
						UserCommand uc2 = tsmi2.Tag as UserCommand;
						if (null != uc2) {
							uc2.Execute(this.SelectedContents);
						}
					}
				};
				menuItems.Add(tsmi);
			}
			this.tsmiUserCommands.DropDownItems.AddRange(menuItems.ToArray());
			this.tsmiUserCommands.Enabled = this.tsmiUserCommands.HasDropDownItems;
		}
		private void UpdateView() {
			this.listView1.BeginUpdate();
			this.DisplayItems();
			this.listView1.EndUpdate();
		}
		private void UpdateViewIfFilterEnabledAndHasFilterRegex() {
			if(this.FilterEnabled && null != this.FilterRegex) {
				this.UpdateView();
			}
		}

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateUserCommandsMenuItems();
		}
		private void NgContentsManager_NgContentsChanged(object sender, EventArgs e) {
			this.RefreshAllNgCached();
			this.UpdateView();
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
			}
		}
		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e) {
			if(this.ShowPackages) {
				this.listView1.ShowGroups = false;
				this.tsmiShowPackages.CheckState = CheckState.Indeterminate;
			}

			ListViewItemComparer comparer = new ListViewItemComparer(e.Column);
			if (comparer.SameIndexAs(this.listView1.ListViewItemSorter as ListViewItemComparer)) {
				comparer = (this.listView1.ListViewItemSorter as ListViewItemComparer).Clone() as ListViewItemComparer;
				comparer.Toggle();
				this.listView1.ListViewItemSorter = comparer;
			} else {
				this.listView1.ListViewItemSorter = comparer;
			}
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
		private void tsmiPlayWithWmp_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				Utility.PlayWithWMP(cont.PlayListUri);
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
			ContentAdapter.CopyNames(this.SelectedContents);
		}
		private void tsmiCopyDetailUri_Click(object sender, EventArgs e) {
			ContentAdapter.CopyUris(this.SelectedContents);
		}
		private void tsmiCopyNameAndDetailUri_Click(object sender, EventArgs e) {
			ContentAdapter.CopyNamesAndUris(this.SelectedContents);
		}
		private void tsmiViewImagesSmall_Click(object sender, EventArgs e) {
			Uri[] images = Array.ConvertAll<ContentAdapter, Uri>(this.SelectedContents, new Converter<ContentAdapter, Uri>(delegate(ContentAdapter input) {
				return input.ImageSmallUri;
			}));
			BrowserForm.Browse(images);
		}
		private void tsmiViewImagesLarge_Click(object sender, EventArgs e) {
			Uri[] images = Array.ConvertAll<ContentAdapter, Uri>(this.SelectedContents, new Converter<ContentAdapter, Uri>(delegate(ContentAdapter input) {
				return input.ImageLargeUri;
			}));
			BrowserForm.Browse(images);
		}
		private void tsmiAddNgWithTitle_Click(object sender, EventArgs e) {
			List<string> titles = new List<string>();
			foreach(ContentAdapter cont in this.SelectedContents) {
				string title = cont.Title;
				if(!titles.Contains(title)) {
					titles.Add(title);
				}
			}
			List<NgContent> ngs = titles.ConvertAll<NgContent>(new Converter<string, NgContent>(delegate(string input) {
				return new NgContent("簡易追加", "Title", TwoStringsPredicateMethod.Equals, input);
			}));
			if(ngs.Count > 0) {
				NgContentsManager.Instance.AddRange(ngs);
			}
		}
		private void tsmiAddNgWithId_Click(object sender, EventArgs e) {
			List<NgContent> ngs = new List<NgContent>();
			foreach(ContentAdapter cont in this.SelectedContents) {
				ngs.Add(new NgContent("簡易追加", "ContentId", TwoStringsPredicateMethod.Equals, cont.ContentId));
			}
			if(ngs.Count > 0) {
				NgContentsManager.Instance.AddRange(ngs);
			}
		}
		private void tsmiNgTest_Click(object sender, EventArgs e) {
			List<NgContent> ngs = new List<NgContent>();
			NgContentsManager manager = NgContentsManager.Instance;
			foreach (ContentAdapter cont in this.SelectedContents) {
				foreach (NgContent ng in manager.EnumerateNgsTo(cont)) {
					if (!ngs.Contains(ng)) {
						ngs.Add(ng);
					}
				}
			}
			if (0 == ngs.Count) {
				MessageBox.Show("該当するNGコンテンツはありません．", "NGテスト", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} else {
				string separator = "-------------------------------------------------";
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(separator);
				sb.AppendLine("コメント\t主語\t述語\t目的語\t作成日時");
				sb.AppendLine(separator);
				foreach (NgContent ng in ngs) {
					sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", ng.Comment, ng.PropertyName, ng.Method.ToString(), ng.Word, ng.Created.ToString()));
				}
				sb.AppendLine(separator);
				switch (MessageBox.Show("以下のNGコンテンツに該当します．\n\n"+ sb.ToString() + "\n該当するNGコンテンツをすべて削除しますか？", "NGテスト", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) {
					case DialogResult.Yes:
						manager.RemoveAll(ngs.Contains);
						break;
				}
			}
		}
		private void tsmiRemoveCache_Click(object sender, EventArgs e) {
			int succeeded = 0;
			int failed = 0;
			foreach (ContentAdapter cont in this.SelectedContents) {
				if (Cache.Instance.ContentCacheController.RemoveCache(cont.ContentId)) {
					succeeded++;
				} else {
					failed++;
				}
			}
			if (null != this.ManuallyCacheDeleted) {
				this.ManuallyCacheDeleted(this, new ManuallyCacheDeletedEventArgs(succeeded, failed));
			}
		}
		#endregion
		#region メニューの項目
		private void tsmiShowPackages_Click(object sender, EventArgs e) {
			switch(this.tsmiShowPackages.CheckState) {
				case CheckState.Checked:
					this.ShowPackages = false;
					break;
				case CheckState.Indeterminate:
				case CheckState.Unchecked:
					this.ShowPackages = true;
					this.listView1.BeginUpdate();
					this.ClearAllItems();
					if(null != this.CrawlResult) {
						this.CreateItems();
						this.DisplayItems();
					}
					this.listView1.EndUpdate();
					break;
			}
		}
		private void tsmiHoverSelect_Click(object sender, EventArgs e) {
			this.HoverSelect = this.HoverSelect;
		}
		private void tsmiMultiSelect_Click(object sender, EventArgs e) {
			this.MultiSelect = this.MultiSelect;
		}
		private void tsmiNewColor_Click(object sender, EventArgs e) {
			this.tsddbSettings.DropDown.Close();
			this.colorDialog1.Color = this.NewColor;
			if (DialogResult.OK == this.colorDialog1.ShowDialog()) {
				this.NewColor = this.colorDialog1.Color;
			}
		}
		private void tsmiMaxPageMenuItems_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "「ページ」メニューの項目数の最大値";
			this.inputBoxDialog1.Message = "項目数の最大値を入力してください．負数で無制限．";
			this.inputBoxDialog1.Input = this.MaxPageMenuItems.ToString();
			switch(this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					string s = this.inputBoxDialog1.Input;
					try {
						this.MaxPageMenuItems = int.Parse(s);
					} catch(Exception ex) {
						MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					break;
			}
		}
		private void tsmiMaxExceptionMenuItems_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "「例外」メニューの項目数の最大値";
			this.inputBoxDialog1.Message = "項目数の最大値を入力してください．負数で無制限．";
			this.inputBoxDialog1.Input = this.MaxPageMenuItems.ToString();
			switch(this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					string s = this.inputBoxDialog1.Input;
					try {
						this.MaxExceptionMenuItems = int.Parse(s);
					} catch(Exception ex) {
						MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					break;
			}
		}
		#endregion
		#region フィルタ関連
		private void tsbShowFilter_Click(object sender, EventArgs e) {
			this.FilterEnabled = this.FilterEnabled;
			if (this.ClearFilterStringOnHideEnabled && !this.FilterEnabled) {
				this.FilterString = string.Empty;
			}
			this.UpdateView();
		}
		private void tstbFilter_TextChanged(object sender, EventArgs e) {
			this.timerFilter.Start();
		}
		private void timerFilter_Tick(object sender, EventArgs e) {
			this.timerFilter.Stop();
			this.CreateAndSetFilterRegex();
		}
		#endregion
		#region ワンクリックでの切り替え
		private void tsbOneAboneToumei_Click(object sender, EventArgs e) {
			this.AboneType = AboneType.Toumei;
		}
		private void tsbOneAboneSabori_Click(object sender, EventArgs e) {
			this.AboneType = AboneType.Sabori;
		}
		private void tsbOneAboneHakidame_Click(object sender, EventArgs e) {
			this.AboneType = AboneType.Hakidame;
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
		#endregion
		#region フィルタ対象
		private List<bool> GetFilterTargetList() {
			List<bool> targets = new List<bool>(this.listView1.Columns.Count);
			bool afterSeparator = false;
			foreach(ToolStripItem tsi in this.tsddbFilterTarget.DropDownItems) {
				if(!afterSeparator) {
					if(tsi is ToolStripSeparator) {
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
			foreach(ToolStripItem tsi in this.tsddbFilterTarget.DropDownItems) {
				if(!afterSeparator) {
					if(tsi is ToolStripSeparator) {
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

		internal ToolStripDropDown SettingsDropDown {
			get { return this.tsddbSettings.DropDown; }
		}
		internal bool SettingsVisible {
			get { return this.tsddbSettings.Visible; }
			set { this.tsddbSettings.Visible = value; }
		}

	}

	public enum AboneType {
		Toumei,
		Sabori,
		Hakidame,
	}
	public enum FilterType {
		Normal,
		Migemo,
		Regex,
	}
	internal class ManuallyCacheDeletedEventArgs : EventArgs {
		private int succeeded;
		private int failed;
		internal ManuallyCacheDeletedEventArgs(int succeeded, int failed) {
			this.succeeded = succeeded;
			this.failed = failed;
		}
		public int Succeeded {
			get { return this.succeeded; }
		}
		public int Failed {
			get { return this.failed; }
		}
	}
	public class GenreListViewSettings {
		private int? colWidthId;
		private int? colWidthTitle;
		private int? colWidthEpisode;
		private int? colWidthSubtitle;
		private int? colWidthDuration;
		private int? colWidthDeadline;
		private int? colWidthLongDesc;
		private int? colWidthAttribs;
		private AboneType? aboneType;
		private bool? showPackages;
		private bool? hoverSelect;
		private bool? multiSelect;
		private bool? clearFilterStringOnHideEnabled;
		private bool? filterEnabled;
		private FilterType? filterType;
		private Color? newColor;
		private int? maxPageMenuItems;
		private int? maxExceptionMenuItems;
		
		public int? ColWidthId {
			get { return this.colWidthId; }
			set { this.colWidthId = value; }
		}
		public int? ColWidthTitle {
			get { return this.colWidthTitle; }
			set { this.colWidthTitle = value; }
		}
		public int? ColWidthEpisode {
			get { return this.colWidthEpisode; }
			set { this.colWidthEpisode = value; }
		}
		public int? ColWidthSubtitle {
			get { return this.colWidthSubtitle; }
			set { this.colWidthSubtitle = value; }
		}
		public int? ColWidthDuration {
			get { return this.colWidthDuration; }
			set { this.colWidthDuration = value; }
		}
		public int? ColWidthDeadline {
			get { return this.colWidthDeadline; }
			set { this.colWidthDeadline = value; }
		}
		public int? ColWidthLongDesc {
			get { return this.colWidthLongDesc; }
			set { this.colWidthLongDesc = value; }
		}
		public int? ColWidthAttribs {
			get { return this.colWidthAttribs; }
			set { this.colWidthAttribs = value; }
		}
		public AboneType? AboneType {
			get { return this.aboneType; }
			set { this.aboneType = value; }
		}
		public bool? ShowPackages {
			get { return this.showPackages; }
			set { this.showPackages = value; }
		}
		public bool? HoverSelect {
			get { return this.hoverSelect; }
			set { this.hoverSelect = value; }
		}
		public bool? MultiSelect {
			get { return this.multiSelect; }
			set { this.multiSelect = value; }
		}
		public bool? ClearFilterStringOnHideEnabled {
			get { return this.clearFilterStringOnHideEnabled; }
			set { this.clearFilterStringOnHideEnabled = value; }
		}
		public bool? FilterEnabled {
			get { return this.filterEnabled; }
			set { this.filterEnabled = value; }
		}
		public FilterType? FilterType {
			get { return this.filterType; }
			set { this.filterType = value; }
		}
		
		[XmlIgnore] //ColorはXMLシリアライズできない？
		public Color? NewColor {
			get { return this.newColor; }
			set { this.newColor = value; }
		}
		public int? NewColorArgb {
			get {
				if (this.NewColor.HasValue) {
					return this.NewColor.Value.ToArgb();
				} else {
					return null;
				}
			}
			set {
				if (value.HasValue) {
					this.NewColor = Color.FromArgb(value.Value);
				} else {
					this.NewColor = null;
				}
			}
		}

		public int? MaxPageMenuItems {
			get { return this.maxPageMenuItems; }
			set { this.maxPageMenuItems = value; }
		}
		public int? MaxExceptionMenuItems {
			get { return this.maxExceptionMenuItems; }
			set { this.maxExceptionMenuItems = value; }
		}
	}
}
