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
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Collections;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class CrawlResultView : UserControl, ICrawlResultViewBindingContract, INotifyPropertyChanged {
		internal const string DefaultDestinationPlaylistName = "My Playlist 1";
		
		private sealed class FilterFlagComparer : IComparer<ContentListViewItem>, IComparer {
			private IComparer<ContentListViewItem> anotherComparer;
			public FilterFlagComparer(IComparer<ContentListViewItem> anotherComparer) {
				this.anotherComparer = anotherComparer;
			}
			public int Compare(ContentListViewItem x, ContentListViewItem y) {
				if (x.FilterFlag == y.FilterFlag) {
					return this.anotherComparer.Compare(x, y);
				} else if (x.FilterFlag) {
					return -1;
				} else {
					return 1;
				}
			}
			public int Compare(object x, object y) {
				return this.Compare(x as ContentListViewItem, y as ContentListViewItem);
			}
		}
		private sealed class ContentListViewItem : ListViewItem {
			private readonly GContentClass cont;
			private readonly ContentStyle style;
			private readonly ListViewGroup packageGroup;
			private bool imageRequestedAlready = false;
			private bool newFlag = false;
			private bool modifiedFlag = false;
			private bool filterFlag = false;
			private bool isNg = false;
			private bool isFav = false;

			public ContentListViewItem(GContentClass cont, ContentStyle style, ListViewGroup group)
				: base(new string[] { cont.ContentId , cont.Title, cont.SeriesNumber, cont.Subtitle, cont.DurationValue.ToString(), cont.DeadlineValue.ToString(), cont.SummaryText}) {
				this.cont = cont;
				this.style = style;
				this.Group = group;
				this.packageGroup = group;
			}
			public GContentClass Content {
				get { return this.cont; }
			}
			public bool ImageRequestedAlready {
				get { return this.imageRequestedAlready; }
				set { this.imageRequestedAlready = value; }
			}
			public bool NewFlag {
				get { return this.newFlag; }
				set { this.newFlag = value; }
			}
			public bool ModifiedFlag {
				get { return this.modifiedFlag; }
				set { this.modifiedFlag = value; }
			}
			public bool FilterFlag {
				get { return this.filterFlag; }
				set { this.filterFlag = value; }
			}
			public void ApplyStyle() {
				if (this.NewFlag) {
					this.ForeColor = this.style.ColorNew;
				} else if (this.modifiedFlag) {
					this.ForeColor = this.style.ColorModified;
				} else {
					this.ForeColor = SystemColors.WindowText;
				}
				if (this.filterFlag) {
					this.BackColor = this.style.ColorFilter;
				} else {
					this.BackColor = SystemColors.Window;
				}
			}
			public bool IsNg {
				get { return this.isNg; }
			}
			public bool IsFav {
				get { return this.isFav; }
			}
			public void SetNgFavFlag(bool isNg, bool isFav) {
				if (this.isNg == isNg && this.isFav == isFav) return;
				this.isNg = isNg;
				this.isFav = isFav;
				FontStyle fs = FontStyle.Regular;
				if(this.isNg) fs |= FontStyle.Strikeout;
				if(this.isFav) fs |= FontStyle.Underline;
				base.Font = new Font(base.Font, fs);
			}
			public ListViewGroup PackageGroup {
				get { return this.packageGroup; }
			}
		}
		private sealed class ContentStyle {
			private Color colorNew = Color.Red;
			private Color colorModified = Color.DarkOrange;
			private Color colorFilter = Color.LemonChiffon;

			public Color ColorNew {
				get { return this.colorNew; }
				set { this.colorNew = value; }
			}
			public Color ColorModified {
				get { return this.colorModified; }
				set { this.colorModified = value; }
			}
			public Color ColorFilter {
				get { return this.colorFilter; }
				set { this.colorFilter = value; }
			}
		}
		
		#region カラムとソート関連
		private static readonly string[] ColWidthPropertyNames = new string[] {
			"ColWidthId", "ColWidthTitle", "ColWidthSeriesNumber", "ColWidthSubtitle", "ColWidthDuration", "ColWidthDeadline", "ColWidthSummary"
		};
		private static readonly Comparison<ContentListViewItem>[] ContentComparisons = new Comparison<ContentListViewItem>[]{
			new Comparison<ContentListViewItem>(delegate(ContentListViewItem x, ContentListViewItem y){
				return x.SubItems[0].Text.CompareTo(y.SubItems[0].Text);
			}),
			new Comparison<ContentListViewItem>(delegate(ContentListViewItem x, ContentListViewItem y){
				return x.SubItems[1].Text.CompareTo(y.SubItems[1].Text);
			}),
			new Comparison<ContentListViewItem>(delegate(ContentListViewItem x, ContentListViewItem y){
				return x.SubItems[2].Text.CompareTo(y.SubItems[2].Text);
			}),
			new Comparison<ContentListViewItem>(delegate(ContentListViewItem x, ContentListViewItem y){
				return x.SubItems[3].Text.CompareTo(y.SubItems[3].Text);
			}),
			new Comparison<ContentListViewItem>(delegate(ContentListViewItem x, ContentListViewItem y){
				return x.SubItems[4].Text.CompareTo(y.SubItems[4].Text);
			}),
			new Comparison<ContentListViewItem>(delegate(ContentListViewItem x, ContentListViewItem y){
				return x.SubItems[5].Text.CompareTo(y.SubItems[5].Text);
			}),
			new Comparison<ContentListViewItem>(delegate(ContentListViewItem x, ContentListViewItem y){
				return x.SubItems[6].Text.CompareTo(y.SubItems[6].Text);
			}),
		};
		#endregion

		private static readonly KeyValuePair<string, Comparison<ContentListViewItem>>[] AdditionalComparisons = new KeyValuePair<string, Comparison<ContentListViewItem>>[]{
			new KeyValuePair<string, Comparison<ContentListViewItem>>("データロウ作成日時", new Comparison<ContentListViewItem>(delegate(ContentListViewItem x, ContentListViewItem y){
				return x.Content.Created.CompareTo(y.Content.Created);
			})),
			new KeyValuePair<string, Comparison<ContentListViewItem>>("データロウ最終更新日時", new Comparison<ContentListViewItem>(delegate(ContentListViewItem x, ContentListViewItem y){
				return x.Content.LastModified.CompareTo(y.Content.LastModified);
			})),
		};
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler LastSelectedContentChanged;
		public event EventHandler LastSelectingPlaylistChanged;
		
		private CrawlResult result;
		private List<ListViewGroup> allGroups = new List<ListViewGroup>();
		private List<ContentListViewItem> allClvi = new List<ContentListViewItem>();
		
		private GContentClass lastSelectedContent;
		private Playlist lastSelectingPlaylist;
		private FilterFlagComparer comparerF;
		private StackableComparisonsComparer<ContentListViewItem> comparerS;
		
		private ContentVisibilities contentVisibilities = ContentVisibilities.PresetToumei;
		private CrawlResultViewView crvView = CrawlResultViewView.Details;
		private ContentStyle style = new ContentStyle();
		private bool groupingAtTheBegining = true;
		private Migemo migemo;
		private CrawlResultViewFilterType filterType = CrawlResultViewFilterType.Normal;
		private bool incrementalFilterEnabled = true;
		private bool caseInsensitiveFilter = true;
		private readonly BackgroundImageLoader bgImgLoader = new BackgroundImageLoader(0);
		private bool addingRules = false;
		
		public CrawlResultView() {
			InitializeComponent();

			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
			this.tsmiCmsAddToThePlaylist.Font = new Font(this.tsmiCmsAddToThePlaylist.Font, FontStyle.Bold);

			this.FilterType = CrawlResultViewFilterType.Regex;
			this.FilterType = CrawlResultViewFilterType.Normal;
		}

		private void CrawlResultView_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;

			List<ToolStripItem> sortMenuItems = new List<ToolStripItem>(this.lvResult.Columns.Count);
			foreach (ColumnHeader ch in this.lvResult.Columns) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(ch.Text);
				tsmi.Tag = ch.Index;
				tsmi.Click += delegate(object sender2, EventArgs e2) {
					this.PushComparison((int)(sender2 as ToolStripMenuItem).Tag);
				};
				sortMenuItems.Add(tsmi);
			}
			sortMenuItems.Add(new ToolStripSeparator());
			foreach (KeyValuePair<string, Comparison<ContentListViewItem>> pair in CrawlResultView.AdditionalComparisons) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(pair.Key);
				tsmi.Tag = pair.Value;
				tsmi.Click += delegate(object sender2, EventArgs e2) {
					this.PushComparison((sender2 as ToolStripMenuItem).Tag as Comparison<ContentListViewItem>);
				};
				sortMenuItems.Add(tsmi);
			}
			this.tsmiSort.DropDownItems.AddRange(sortMenuItems.ToArray());

			this.comparerS = new StackableComparisonsComparer<ContentListViewItem>();
			this.comparerF = new FilterFlagComparer(this.comparerS);
			this.lvResult.ListViewItemSorter = this.comparerF;
			this.ClearCrawlResult();
			
			this.bgImgLoader.StartWorking();
			this.Disposed += delegate {
				this.bgImgLoader.Dispose();
			};

			//フィルタの対象のメニューを作成
			List<ToolStripItem> filterObjects = new List<ToolStripItem>();
			foreach (ColumnHeader ch in this.lvResult.Columns) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(ch.Text);
				tsmi.Checked = true;
				tsmi.CheckOnClick = true;
				tsmi.Click += delegate {
					this.ExecuteFilter();
				};
				filterObjects.Add(tsmi);
			}
			this.tsddbFilterObjects.DropDownItems.AddRange(filterObjects.ToArray());

			//フィルタの対象のメニューは閉じない
			this.tsddbFilterObjects.DropDown.Closing += delegate(object sender2, ToolStripDropDownClosingEventArgs e2) {
				switch (e2.CloseReason) {
					case ToolStripDropDownCloseReason.ItemClicked:
						e2.Cancel = true;
						break;
				}
			};

			//Migemo初期化
			try {
				this.migemo = new Migemo(Program.RootOptions.AppBasicOptions.MigemoDicFile);
				this.Disposed += delegate {
					this.migemo.Dispose();
				};
			} catch (MigemoException ex) {
				this.migemo = null;
				this.tstbAnswer.Text = ex.Message;
				this.tsmiFilterTypeMigemo.Enabled = false;
				this.tsbFilterTypeMigemo.Enabled = false;
			}

			if (Program.ContentClassificationRulesManager == null) return;
			Program.ContentClassificationRulesManager.ContentCllasificationRulesManagerChanged += new EventHandler(ContentClassificationRulesManager_ContentCllasificationRulesManagerChanged);
			this.Disposed += delegate {
				Program.ContentClassificationRulesManager.ContentCllasificationRulesManagerChanged -= new EventHandler(ContentClassificationRulesManager_ContentCllasificationRulesManagerChanged);
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
		private void OnLastSelectingPlaylistChanged() {
			EventHandler handler = this.LastSelectingPlaylistChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		
		#region 表示を行う箇所
		public void ViewCrawlResult(CrawlResult result) {
			if (null == result) {
				this.ClearCrawlResult();
				return;
			}
			
			this.lvResult.BeginUpdate();
			this.ClearCrawlResult();
			this.result = result;
			
			this.lvResult.ShowGroups = true;
			
			this.MakeupToolbar();
			this.MakeupItems();
			this.ApplyContentStyle();
			this.ApplyNgFavFlags();
			if(this.GroupingAtTheBegining) this.AddGroups();
			this.DisplayItems();
			
			this.lvResult.EndUpdate();
			
			this.ChangeEnabilityOfMenuItems();
			this.lvResult.Focus();
		}
		private void ClearCrawlResult() {
			this.result = null;
			this.allGroups.Clear();
			this.allClvi.Clear();
			
			this.bgImgLoader.ClearTasks();
			
			this.lvResult.BeginUpdate();
			this.lvResult.Groups.Clear();
			this.lvResult.Items.Clear();
			this.ilLarge.Images.Clear();
			this.comparerS.ClearComparisons();
			this.lvResult.EndUpdate();

			this.tslCount.Text = string.Empty;
			this.tslTime.Text = string.Empty;

			this.tsddbPages.DropDownItems.Clear();
			this.tsddbErrors.DropDownItems.Clear();
			this.tsddbOffline.DropDownItems.Clear();
			this.tsddbPages.Text = "0";
			this.tsddbErrors.Text = "0";
			this.tsddbOffline.Text = "0";
			this.tsddbPages.Enabled = false;
			this.tsddbErrors.Enabled = false;
			this.tsddbOffline.Enabled = false;
			
			this.tstbFilter.Text = string.Empty;
			
			this.ChangeEnabilityOfMenuItems();
		}
		private void MakeupToolbar() {
			this.tslTime.Text = result.Time.ToString("MM/dd ddd HH:mm");

			if (0 < (result.VisitedPages.Count + result.NotVisitedPages.Count)) {
				this.tsddbPages.Enabled = true;
				this.tsddbPages.Text = (result.VisitedPages.Count + result.NotVisitedPages.Count).ToString();
			}
			if (0 < result.Exceptions.Count) {
				this.tsddbErrors.Enabled = true;
				this.tsddbErrors.Text = result.Exceptions.Count.ToString();
			}
			if (0 < result.DroppedContents.Count) {
				this.tsddbOffline.Enabled = true;
				this.tsddbOffline.Text = result.DroppedContents.Count.ToString();
			}
		}
		private void MakeupItems() {
			bool ignoreNewFlag = (0 == this.result.SortedCKeysNew.Count)
				|| (this.result.Contents.Count == this.result.SortedCKeysNew.Count);
			bool ignoreModifiedFlag = (0 == this.result.SortedCKeysModified.Count);
			SortedDictionary<int, ListViewGroup> dicGroups = new SortedDictionary<int, ListViewGroup>();
			foreach (GContentClass cont in this.result.Contents) {
				ListViewGroup group = null;
				GPackageClass pac = cont.ParentPackage;
				if (pac != null) {
					if (dicGroups.TryGetValue(pac.PackageKey, out group)) {
					} else {
						group = new ListViewGroup(string.Format("<{0}> {1} ({2})", pac.PackageId, pac.PackageTitle, pac.PackageCatch));
						dicGroups.Add(pac.PackageKey, group);
						this.allGroups.Add(group);
					}
				}
				ContentListViewItem clvi = new ContentListViewItem(cont, this.style, group);
				if (!ignoreNewFlag && result.SortedCKeysNew.BinarySearch(cont.ContentKey) >= 0) {
					clvi.NewFlag = true;
				} else if (!ignoreModifiedFlag && result.SortedCKeysModified.BinarySearch(cont.ContentKey) >= 0) {
					clvi.ModifiedFlag = true;
				}
				this.allClvi.Add(clvi);
			}
		}
		private void ApplyContentStyle() {
			this.lvResult.BeginUpdate();
			foreach (ContentListViewItem clvi in this.allClvi) {
				clvi.ApplyStyle();
			}
			this.lvResult.EndUpdate();
		}
		private void ApplyNgFavFlags() {
			this.lvResult.BeginUpdate();
			foreach (ContentListViewItem clvi in this.allClvi) {
				string[] dests = Program.ContentClassificationRulesManager.GetDestinationsFor(clvi.Content);
				bool isNg = false;
				bool isFav = false;
				foreach (string dest in dests) {
					if (string.IsNullOrEmpty(dest)) {
						isNg = true;
					} else {
						isFav = true;
					}
				}
				clvi.SetNgFavFlag(isNg, isFav);
			}
			this.lvResult.EndUpdate();
		}
		private void AddGroups() {
			this.lvResult.Groups.AddRange(this.allGroups.ToArray());
		}
		private void DisplayItems() {
			this.lvResult.BeginUpdate();
			this.lvResult.Items.Clear();
			
			ContentVisibilities vis = this.ContentVisibilities;
			bool showNgTrue = (vis & ContentVisibilities.NgTrue) != ContentVisibilities.None;
			bool showNgFalse = (vis & ContentVisibilities.NgFalse) != ContentVisibilities.None;
			bool showFavTrue = (vis & ContentVisibilities.FavTrue) != ContentVisibilities.None;
			bool showFavFalse = (vis & ContentVisibilities.FavFalse) != ContentVisibilities.None;
			bool showNewTrue = (vis & ContentVisibilities.NewTrue) != ContentVisibilities.None;
			bool showNewFalse = (vis & ContentVisibilities.NewFalse) != ContentVisibilities.None;
			List<ListViewItem> lvis = new List<ListViewItem>();
			foreach (ContentListViewItem clvi in this.allClvi) {
				if ((clvi.IsNg && !showNgTrue) || (!clvi.IsNg && !showNgFalse)) continue;
				if ((clvi.IsFav && !showFavTrue) || (!clvi.IsFav && !showFavFalse)) continue;
				if (((clvi.NewFlag || clvi.ModifiedFlag) && !showNewTrue) || (!(clvi.NewFlag || clvi.ModifiedFlag) && !showNewFalse)) continue;

				clvi.Group = clvi.PackageGroup;
				lvis.Add(clvi);
			}
			this.lvResult.Items.AddRange(lvis.ToArray());
			this.lvResult.EndUpdate();
			
			this.tslCount.Text = string.Format("{0}/{1}", lvis.Count, this.allClvi.Count);
		}
		private void ContentClassificationRulesManager_ContentCllasificationRulesManagerChanged(object sender, EventArgs e) {
			this.lvResult.BeginUpdate();
			this.ApplyNgFavFlags();
			if (!this.addingRules) this.DisplayItems();
			this.lvResult.EndUpdate();
			this.ChangeEnabilityOfMenuItems();
		}
		#endregion
		
		#region メインメニュー
		private void tsmiVisibilitiesToumei_Click(object sender, EventArgs e) {
			this.ContentVisibilities = ContentVisibilities.PresetToumei;
		}
		private void tsmiVisibilitiesSabori_Click(object sender, EventArgs e) {
			this.ContentVisibilities = ContentVisibilities.PresetSabori;
		}
		private void tsmiVisibilitiesHakidame_Click(object sender, EventArgs e) {
			this.ContentVisibilities = ContentVisibilities.PresetHakidame;
		}
		private void tsmiVisibilitiesYorigonomi_Click(object sender, EventArgs e) {
			this.ContentVisibilities = ContentVisibilities.PresetYorigonomi;
		}
		private void tsmiVisibilitiesShinchaku_Click(object sender, EventArgs e) {
			this.ContentVisibilities = ContentVisibilities.PresetShinchaku;
		}
		private void tsmiViewDetails_Click(object sender, EventArgs e) {
			this.CrvView = CrawlResultViewView.Details;
		}
		private void tsmiViewTile_Click(object sender, EventArgs e) {
			this.CrvView = CrawlResultViewView.Tile;
		}
		private void tsmiViewIcon_Click(object sender, EventArgs e) {
			this.CrvView = CrawlResultViewView.Icon;
		}
		private void tsmiFilter_Click(object sender, EventArgs e) {
			this.FilterBarVisible = !this.FilterBarVisible;
		}
		private void tsmiFilterTypeNormal_Click(object sender, EventArgs e) {
			this.FilterType = CrawlResultViewFilterType.Normal;
		}
		private void tsmiFilterTypeMigemo_Click(object sender, EventArgs e) {
			this.FilterType = CrawlResultViewFilterType.Migemo;
		}
		private void tsmiFilterTypeRegex_Click(object sender, EventArgs e) {
			this.FilterType = CrawlResultViewFilterType.Regex;
		}
		private void tsmiChangeDestinationPlaylist_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "追加先のプレイリスト名を指定";
			this.inputBoxDialog1.Message = "追加先のプレイリス名を入力してください．";
			this.inputBoxDialog1.Input = this.tscbDestPlaylistName.Text;
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					this.tscbDestPlaylistName.Text = this.inputBoxDialog1.Input;
					break;
			}
		}
		private void tsmiRedisplay_Click(object sender, EventArgs e) {
			this.ViewCrawlResult(this.result);
		}
		private void tsmiAddToThePlaylist_Click(object sender, EventArgs e) {
			Playlist pl = Program.PlaylistsManager.GetOrCreatePlaylistNamedAs(this.DestinationPlaylistName);
			this.AddToPlaylistHelper(pl, this.GetSelectedContents());
		}
		private void tspmiAddToAnotherPlaylist_PlaylistSelected(object sender, EventArgs e) {
			Playlist pl = (sender as ToolStripPlaylistMenuItem).LastSelectedPlaylist;
			this.AddToPlaylistHelper(pl, this.GetSelectedContents());
		}
		private void tsmiPlay_Click(object sender, EventArgs e) {
			GContentClass[] conts = this.GetSelectedContents();
			Program.PlayContent(conts[0], null);
		}
		private void tsmiCopyName_Click(object sender, EventArgs e) {
			ContentClipboardUtility.CopyNames(this.GetSelectedContents());
		}
		private void tsmiCopyUri_Click(object sender, EventArgs e) {
			ContentClipboardUtility.CopyUris(this.GetSelectedContents());
		}
		private void tsmiCopyNameAndUri_Click(object sender, EventArgs e) {
			ContentClipboardUtility.CopyNamesAndUris(this.GetSelectedContents());
		}
		private void tscpmiCopyOtherProperties_PropertySelected(object sender, EventArgs e) {
			ContentClipboardUtility.CopyContentProperties(this.GetSelectedContents(), (sender as ToolStripContentPropertyMenuItem).LastSelectedPropertyInfo);
		}
		private void tscrmiRules_SubmenuSelected(object sender, EventArgs e) {
			ToolStripClassificationRuleMenuItem tscrmi = sender as ToolStripClassificationRuleMenuItem;
			this.addingRules = true;
			tscrmi.LastSelectedAction(this.GetSelectedContents());
			this.addingRules = false;
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
		private void tscrmiCmsRules_SubmenuSelected(object sender, EventArgs e) {
			this.tscrmiRules_SubmenuSelected(sender, e);
		}
		private void tsecmiCmsCommand_ExternalCommandSelected(object sender, EventArgs e) {
			this.tsecmiCommand_ExternalCommandSelected(sender, e);
		}
		#endregion
		#region ツールバーのイベントハンドラ
		private void tsddbPages_DropDownOpening(object sender, EventArgs e) {
			if (!this.tsddbPages.HasDropDownItems) {
				List<ToolStripItem> items = new List<ToolStripItem>();
				foreach (Uri uri in this.result.VisitedPages) {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(uri.PathAndQuery);
					tsmi.Tag = uri;
					tsmi.Click += delegate(object sender2, EventArgs e2) {
						Program.BrowsePage((sender2 as ToolStripMenuItem).Tag as Uri);
					};
					items.Add(tsmi);
				}
				if (this.result.NotVisitedPages.Count > 0) {
					items.Add(new ToolStripSeparator());
					foreach (Uri uri in this.result.NotVisitedPages) {
						ToolStripMenuItem tsmi = new ToolStripMenuItem(uri.PathAndQuery);
						tsmi.Tag = uri;
						tsmi.Click += delegate(object sender2, EventArgs e2) {
							Program.BrowsePage((sender2 as ToolStripMenuItem).Tag as Uri);
						};
						items.Add(tsmi);
					}
				}
				this.tsddbPages.DropDownItems.AddRange(items.ToArray());
			}
		}
		private void tsddbErrors_DropDownOpening(object sender, EventArgs e) {
			if (!this.tsddbErrors.HasDropDownItems) {
				List<ToolStripItem> items = new List<ToolStripItem>();
				foreach (Exception ex in this.result.Exceptions) {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(ex.Message);
					tsmi.Tag = ex;
					tsmi.Click += delegate(object sender2, EventArgs e2) {
						this.exceptionDialog1.Exception = (sender2 as ToolStripMenuItem).Tag as Exception;
						this.exceptionDialog1.ShowDialog();
					};
					items.Add(tsmi);
				}
				this.tsddbErrors.DropDownItems.AddRange(items.ToArray());
			}
		}
		private void tsddbOffline_DropDownOpening(object sender, EventArgs e) {
			if (!this.tsddbOffline.HasDropDownItems) {
				List<ToolStripItem> items = new List<ToolStripItem>();
				foreach (GContentClass cont in this.result.DroppedContents) {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(string.Format("<{0}> {1} | {2} | {3}", cont.ContentId, cont.Title, cont.SeriesNumber, cont.Subtitle));
					tsmi.Tag = cont;
					tsmi.Click += delegate(object sender2, EventArgs e2) {
						this.LastSelectedContent = (sender2 as ToolStripMenuItem).Tag as GContentClass;
					};
					items.Add(tsmi);
				}
				this.tsddbOffline.DropDownItems.AddRange(items.ToArray());
			}
		}
		private void tscvsTVisibilities_ContentVisibilitiesChanged(object sender, EventArgs e) {
			this.ContentVisibilities = this.tscvsTVisibilities.ContentVisibilities;
		}
		private void tssbView_ButtonClick(object sender, EventArgs e) {
			switch (this.CrvView) {
				case CrawlResultViewView.Details:
					this.CrvView = CrawlResultViewView.Tile;
					break;
				case CrawlResultViewView.Tile:
					this.CrvView = CrawlResultViewView.Icon;
					break;
				case CrawlResultViewView.Icon:
					this.CrvView = CrawlResultViewView.Details;
					break;
			}
		}
		private void tsmiTViewDetails_Click(object sender, EventArgs e) {
			this.CrvView = CrawlResultViewView.Details;
		}
		private void tsmiTViewTile_Click(object sender, EventArgs e) {
			this.CrvView = CrawlResultViewView.Tile;
		}
		private void tsmiTViewIcon_Click(object sender, EventArgs e) {
			this.CrvView = CrawlResultViewView.Icon;
		}
		private void tscbDestPlaylistName_TextChanged(object sender, EventArgs e) {
			this.OnPropertyChanged("DestinationPlaylistName");
		}
		private void tscbDestPlaylistName_DropDown(object sender, EventArgs e) {
			this.tscbDestPlaylistName.BeginUpdate();
			this.tscbDestPlaylistName.Items.Clear();
			foreach (Playlist pl in Program.PlaylistsManager) {
				this.tscbDestPlaylistName.Items.Add(pl.Name);
			}
			this.tscbDestPlaylistName.EndUpdate();
		}
		#endregion
		#region 絞込みツールバー
		private void tsbFilterTypeNormal_Click(object sender, EventArgs e) {
			this.FilterType = CrawlResultViewFilterType.Normal;
		}
		private void tsbFilterTypeMigemo_Click(object sender, EventArgs e) {
			this.FilterType = CrawlResultViewFilterType.Migemo;
		}
		private void tsbFilterTypeRegex_Click(object sender, EventArgs e) {
			this.FilterType = CrawlResultViewFilterType.Regex;
		}
		private void tstbFilter_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					this.ExecuteFilter();
					break;
			}
		}
		private void tstbFilter_TextChanged(object sender, EventArgs e) {
			this.timerFilterDelay.Stop();
			if (this.IncrementalFilterEnabled) {
				this.timerFilterDelay.Start();
			}
		}
		private void tsmiFilterObjectsAll_Click(object sender, EventArgs e) {
			this.ChangeAllFilterObjectsHelper(new Action<ToolStripMenuItem>(delegate(ToolStripMenuItem tsmi) {
				tsmi.Checked = true;
			}));
			this.ExecuteFilter();
		}
		private void tsmiFilterObjectsNone_Click(object sender, EventArgs e) {
			this.ChangeAllFilterObjectsHelper(new Action<ToolStripMenuItem>(delegate(ToolStripMenuItem tsmi) {
				tsmi.Checked = false;
			}));
			this.ExecuteFilter();
		}
		private void tsmiFilterObjectsToggle_Click(object sender, EventArgs e) {
			this.ChangeAllFilterObjectsHelper(new Action<ToolStripMenuItem>(delegate(ToolStripMenuItem tsmi) {
				tsmi.Checked = !tsmi.Checked;
			}));
			this.ExecuteFilter();
		}
		#endregion
		#region リストビューのイベントハンドラ
		private void lvResult_MouseDoubleClick(object sender, MouseEventArgs e) {
			this.tsmiCmsAddToThePlaylist.PerformClick();
		}
		private void lvResult_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					this.tsmiCmsAddToThePlaylist.PerformClick();
					break;
			}
		}
		private void lvResult_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if (e.IsSelected) {
				this.LastSelectedContent = (e.Item as ContentListViewItem).Content;
			}
		}
		private void lvResult_SelectedIndexChanged(object sender, EventArgs e) {
			this.timerSelectionDelay.Start();
		}
		private void lvResult_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.PushComparison(e.Column);
		}
		private void lvResult_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e) {
			string propName = CrawlResultView.ColWidthPropertyNames[e.ColumnIndex];
			this.OnPropertyChanged(propName);
		}
		private void lvResult_DrawItem(object sender, DrawListViewItemEventArgs e) {
			ContentListViewItem clvi = e.Item as ContentListViewItem;
			if (clvi == null) goto defaultDraw;
			if (this.lvResult.View == View.Details) goto defaultDraw;
			if (clvi.ImageRequestedAlready) goto defaultDraw;

			clvi.ImageRequestedAlready = true;
			Uri uri = clvi.Content.ImageSmallUri;
			//キャッシュがあっても非同期の方が体感的には速いっぽいので
			/*try {
				HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
				req.CachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheOnly);
				HttpWebResponse res = req.GetResponse() as HttpWebResponse;
				using (Stream stream = res.GetResponseStream()) {
					using (Image img = Image.FromStream(stream)) {
						int idx = this.ilLarge.Images.Add(img, this.ilLarge.TransparentColor);
						if (idx >= 0) {
							clvi.ImageIndex = idx;
						}
					}
				}
			} catch (WebException) {*/
			this.bgImgLoader.PushTask(new BackgroundImageLoadTask(uri, this.BackgroundImageLoadCompletedCallback, clvi));
		//}
		defaultDraw:
			e.DrawDefault = true;
		}
		private void lvResult_DrawSubItem(object sender, DrawListViewSubItemEventArgs e) {
			e.DrawDefault = true;
		}
		private void lvResult_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e) {
			e.DrawDefault = true;
		}
		#endregion

		private void AddToPlaylistHelper(Playlist playlist, GContentClass[] conts) {
			this.LastSelectingPlaylist = playlist;
			playlist.BeginUpdate();
			foreach (GContentClass cont in conts) {
				playlist.AddContent(cont);
			}
			playlist.EndUpdate();
		}
		private bool BackgroundImageLoadCompletedCallback(Image image, object userState) {
			if (this.InvokeRequired) {
				return (bool)this.Invoke(new BackgroundImageLoadCompletedCallback(this.BackgroundImageLoadCompletedCallback), image, userState);
			} else {
				ContentListViewItem clvi = userState as ContentListViewItem;
				if (null != clvi) {
					int idx = this.ilLarge.Images.Add(image, this.ilLarge.TransparentColor);
					if (idx >= 0) {
						clvi.ImageIndex = idx;
					}
				}
				return false;
			}
		}
		private GContentClass[] GetSelectedContents() {
			List<GContentClass> conts = new List<GContentClass>();
			foreach (ContentListViewItem clvi in this.lvResult.SelectedItems) {
				conts.Add(clvi.Content);
			}
			return conts.ToArray();
		}

		private void PushComparison(Comparison<ContentListViewItem> comparison) {
			this.comparerS.PushComparison(comparison);
			this.lvResult.ShowGroups = false;
			this.lvResult.Sort();
		}
		private void PushComparison(int idx) {
			this.PushComparison(CrawlResultView.ContentComparisons[idx]);
		}

		private void ChangeEnabilityOfMenuItems() {
			bool hasLvi = (this.lvResult.Items.Count > 0);
			bool hasContent = (this.lvResult.SelectedItems.Count > 0);

			this.tsmiSort.Enabled = hasLvi;
			this.tsmiAddToThePlaylist.Enabled = hasContent;
			this.tspmiAddToAnotherPlaylist.Enabled = hasContent;
			this.tsmiPlay.Enabled = hasContent;
			this.tsmiCopyName.Enabled = hasContent;
			this.tsmiCopyUri.Enabled = hasContent;
			this.tsmiCopyNameAndUri.Enabled = hasContent;
			this.tscpmiCopyOtherProperties.Enabled = hasContent;
			this.tscrmiRules.Enabled = hasContent;
			this.tsecmiCommand.Enabled = hasContent;

			this.tsmiCmsAddToThePlaylist.Enabled = hasContent;
			this.tspmiCmsAddToAnotherPlaylist.Enabled = hasContent;
			this.tsmiCmsPlay.Enabled = hasContent;
			this.tsmiCmsCopyName.Enabled = hasContent;
			this.tsmiCmsCopyUri.Enabled = hasContent;
			this.tsmiCmsCopyNameAndUri.Enabled = hasContent;
			this.tscpmiCmsCopyOtherProperties.Enabled = hasContent;
			this.tscrmiCmsRules.Enabled = hasContent;
			this.tsecmiCmsCommand.Enabled = hasContent;
		}
		
		public ToolStripDropDown GetToolStripDropDown() {
			return this.tsddbDropDown.DropDown;
		}
		public void BindToOptions(CrawlResultViewOptions options) {
			options.NeutralizeUnspecificValues(this);
			BindingContractUtility.BindAllProperties<CrawlResultView, ICrawlResultViewBindingContract>(this, options);
		}
		public GContentClass LastSelectedContent {
			get { return this.lastSelectedContent; }
			private set {
				//if (this.lastSelectedContent != value) {
					this.lastSelectedContent = value;
					this.OnLastSelectedContentChanged();
				//}
			}
		}
		public Playlist LastSelectingPlaylist {
			get { return this.lastSelectingPlaylist; }
			set {
				this.lastSelectingPlaylist = value;
				this.OnLastSelectingPlaylistChanged();
			}
		}
		private void timerSelectionDelay_Tick(object sender, EventArgs e) {
			this.timerSelectionDelay.Stop();
			this.ChangeEnabilityOfMenuItems();
		}
		private void timerFilterDelay_Tick(object sender, EventArgs e) {
			this.timerFilterDelay.Stop();
			this.ExecuteFilter();
		}
		#region 絞込み関係
		private Regex CreateFilterRegex() {
			Regex regex = null;
			string query = this.tstbFilter.Text;
			string regexCompileErrMessage = null;
			RegexOptions rOpts = RegexOptions.Singleline | RegexOptions.ExplicitCapture | (this.CaseInsensitiveFilter ? RegexOptions.IgnoreCase : RegexOptions.None);
			if (!string.IsNullOrEmpty(query)) {
				switch (this.FilterType) {
					case CrawlResultViewFilterType.Normal:
						try {
							regex = new Regex(Regex.Escape(query), rOpts);
						} catch (ArgumentException e) {
							regexCompileErrMessage = e.Message;
							goto failed;
						}
						break;
					case CrawlResultViewFilterType.Migemo:
						string ans = this.migemo.Query(query);
						try {
							regex = new Regex(ans, rOpts);
						} catch (ArgumentException e) {
							regexCompileErrMessage = e.Message;
							goto failed;
						}
						break;
					case CrawlResultViewFilterType.Regex:
						try {
							regex = new Regex(query, rOpts);
						} catch (ArgumentException e) {
							regexCompileErrMessage = e.Message;
							goto failed;
						}
						break;
				}
			}
			this.tstbFilter.BackColor = SystemColors.Window;
			this.tstbAnswer.Text = (null == regex) ? string.Empty : regex.ToString();
			return regex;
		failed:
			this.tstbFilter.BackColor = Color.Yellow;
			if (!string.IsNullOrEmpty(regexCompileErrMessage)) {
				this.tstbAnswer.Text = regexCompileErrMessage;
			}
			return null;
		}
		private void ExecuteFilter() {
			Regex regex = this.CreateFilterRegex();
			this.lvResult.BeginUpdate();
			if (null == regex) {
				foreach (ContentListViewItem clvi in this.allClvi) {
					clvi.FilterFlag = false;
				}
			} else {
				this.lvResult.ShowGroups = false;
				foreach (ContentListViewItem clvi in this.allClvi) {
					int[] filterObjects = this.GetFilterObjectsList();
					foreach (int oIdx in filterObjects) {
						if (regex.Match(clvi.SubItems[oIdx].Text).Success) {
							clvi.FilterFlag = true;
							goto nextClvi;
						}
					}
					clvi.FilterFlag = false;
				nextClvi:
					continue;
				}
			}
			this.lvResult.Sort();
			if (this.lvResult.View == View.Details && this.lvResult.Items.Count > 0) {
				this.lvResult.TopItem = this.lvResult.Items[0];
			}
			this.ApplyContentStyle();
			this.lvResult.EndUpdate();
		}
		private bool CanUseMigemo {
			get { return this.migemo != null; }
		}
		private void ChangeAllFilterObjectsHelper(Action<ToolStripMenuItem> action) {
			bool afterSeparator = false;
			foreach (ToolStripItem tsi in this.tsddbFilterObjects.DropDownItems) {
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
		private int[] GetFilterObjectsList() {
			List<int> targets = new List<int>(this.lvResult.Columns.Count);
			bool afterSeparator = false;
			int index = 0;
			foreach (ToolStripItem tsi in this.tsddbFilterObjects.DropDownItems) {
				if (!afterSeparator) {
					if (tsi is ToolStripSeparator) {
						afterSeparator = true;
					}
					continue;
				}
				ToolStripMenuItem tsmi = tsi as ToolStripMenuItem;
				if (tsmi.Checked) {
					targets.Add(index);
				}
				index++;
			}
			return targets.ToArray();
		}
		#endregion
		
		#region ICrawlResultViewBindingContract Members
		[Browsable(false)]
		public string DestinationPlaylistName {
			get { return this.tscbDestPlaylistName.Text; }
			set {
				this.tscbDestPlaylistName.Text = value;
				this.OnPropertyChanged("DestinationPlaylistName");
			}
		}
		[Browsable(false)]
		public ContentVisibilities ContentVisibilities {
			get { return this.contentVisibilities; }
			set {
				value = ContentVisibilitiesUtility.SanitizeValue(value);
				if (value != this.ContentVisibilities) {
					this.contentVisibilities = value;
					this.tscvsTVisibilities.ContentVisibilities = value;

					this.tsmiVisibilitiesToumei.Checked = (value == ContentVisibilities.PresetToumei);
					this.tsmiVisibilitiesSabori.Checked = (value == ContentVisibilities.PresetSabori);
					this.tsmiVisibilitiesHakidame.Checked = (value == ContentVisibilities.PresetHakidame);
					this.tsmiVisibilitiesYorigonomi.Checked = (value == ContentVisibilities.PresetYorigonomi);
					this.tsmiVisibilitiesShinchaku.Checked = (value == ContentVisibilities.PresetShinchaku);

					this.tsddbVisibilities.Text = ContentVisibilitiesUtility.ConvertToFlagsString(value);

					if (!base.DesignMode) {
						this.lvResult.BeginUpdate();
						this.DisplayItems();
						this.lvResult.EndUpdate();
						this.ChangeEnabilityOfMenuItems();
					}

					this.OnPropertyChanged("ContentVisibilities");
				}
			}
		}
		[Browsable(false)]
		public CrawlResultViewView CrvView {
			get { return this.crvView; }
			set {
				if (this.crvView != value) {
					this.crvView = value;
					this.tsmiViewDetails.Checked = false;
					this.tsmiViewTile.Checked = false;
					this.tsmiViewIcon.Checked = false;
					this.tsmiTViewDetails.Checked = false;
					this.tsmiTViewTile.Checked = false;
					this.tsmiTViewIcon.Checked = false;
					switch (value) {
						case CrawlResultViewView.Details:
							this.lvResult.View = View.Details;
							this.tsmiViewDetails.Checked = true;
							this.tsmiTViewDetails.Checked = true;
							this.tssbView.Text = "D";
							break;
						case CrawlResultViewView.Tile:
							this.lvResult.View = View.Tile;
							this.tsmiViewTile.Checked = true;
							this.tsmiTViewTile.Checked = true;
							this.tssbView.Text = "T";
							break;
						case CrawlResultViewView.Icon:
							this.lvResult.View = View.LargeIcon;
							this.tsmiViewIcon.Checked = true;
							this.tsmiTViewIcon.Checked = true;
							this.tssbView.Text = "I";
							break;
					}
					this.OnPropertyChanged("CrvView");
				}
			}
		}
		[Browsable(false)]
		public Color ColorNew {
			get { return this.style.ColorNew; }
			set {
				this.style.ColorNew = value;
				this.OnPropertyChanged("ColorNew");
				this.ApplyContentStyle();
			}
		}
		[Browsable(false)]
		public Color ColorModified {
			get { return this.style.ColorModified; }
			set {
				this.style.ColorModified = value;
				this.OnPropertyChanged("ColorModified");
				this.ApplyContentStyle();
			}
		}
		[Browsable(false)]
		public Color ColorFilter {
			get { return this.style.ColorFilter; }
			set {
				this.style.ColorFilter = value;
				this.OnPropertyChanged("ColorFilter");
				this.ApplyContentStyle();
			}
		}
		[Browsable(false)]
		public bool GroupingAtTheBegining {
			get { return this.groupingAtTheBegining; }
			set { this.groupingAtTheBegining = value; }
		}
		[Browsable(false)]
		public bool FilterBarVisible {
			get {
				return this.tsFilter.Visible;
			}
			set {
				if (this.FilterBarVisible != value) {
					this.tsFilter.Visible = value;
					this.tsmiFilter.Checked = value;
					if (value) {
						this.tstbFilter.Focus();
					} else {
						this.lvResult.Focus();
					}
					this.OnPropertyChanged("FilterBarVisible");
				}
			}
		}
		[Browsable(false)]
		public CrawlResultViewFilterType FilterType {
			get { return this.filterType; }
			set {
				if (!this.CanUseMigemo && value == CrawlResultViewFilterType.Migemo) {
					this.FilterType = CrawlResultViewFilterType.Normal;
					return;
				}
				if (this.filterType != value) {
					this.tsmiFilterTypeNormal.Checked = false;
					this.tsmiFilterTypeMigemo.Checked = false;
					this.tsmiFilterTypeRegex.Checked = false;
					this.tsbFilterTypeNormal.Checked = false;
					this.tsbFilterTypeMigemo.Checked = false;
					this.tsbFilterTypeRegex.Checked = false;
					switch (value) {
						case CrawlResultViewFilterType.Normal:
							this.tsmiFilterTypeNormal.Checked = true;
							this.tsbFilterTypeNormal.Checked = true;
							break;
						case CrawlResultViewFilterType.Migemo:
							this.tsmiFilterTypeMigemo.Checked = true;
							this.tsbFilterTypeMigemo.Checked = true;
							break;
						case CrawlResultViewFilterType.Regex:
							this.tsmiFilterTypeRegex.Checked = true;
							this.tsbFilterTypeRegex.Checked = true;
							break;
					}
					this.filterType = value;
					this.ExecuteFilter();
					this.OnPropertyChanged("FilterType");
				}
			}
		}
		[Browsable(false)]
		public bool IncrementalFilterEnabled {
			get { return this.incrementalFilterEnabled; }
			set {
				this.incrementalFilterEnabled = value;
				if (value) {
					this.ExecuteFilter();
				}
				this.OnPropertyChanged("IncrementalFilterEnabled");
			}
		}
		[Browsable(false)]
		public bool CaseInsensitiveFilter {
			get { return this.caseInsensitiveFilter; }
			set {
				this.caseInsensitiveFilter = value;
				this.ExecuteFilter();
				this.OnPropertyChanged("CaseInsensitiveFilter");
			}
		}
		[Browsable(false)]
		public int ColWidthId {
			get { return this.chId.Width; }
			set {
				this.chId.Width = value;
				this.OnPropertyChanged(CrawlResultView.ColWidthPropertyNames[0]);
			}
		}
		[Browsable(false)]
		public int ColWidthTitle {
			get { return this.chTitle.Width; }
			set {
				this.chTitle.Width = value;
				this.OnPropertyChanged(CrawlResultView.ColWidthPropertyNames[1]);
			}
		}
		[Browsable(false)]
		public int ColWidthSeriesNumber {
			get { return this.chSeriesNumber.Width; }
			set {
				this.chSeriesNumber.Width = value;
				this.OnPropertyChanged(CrawlResultView.ColWidthPropertyNames[2]);
			}
		}
		[Browsable(false)]
		public int ColWidthSubtitle {
			get { return this.chSubtitle.Width; }
			set {
				this.chSubtitle.Width = value;
				this.OnPropertyChanged(CrawlResultView.ColWidthPropertyNames[3]);
			}
		}
		[Browsable(false)]
		public int ColWidthDuration {
			get { return this.chDuration.Width; }
			set {
				this.chDuration.Width = value;
				this.OnPropertyChanged(CrawlResultView.ColWidthPropertyNames[4]);
			}
		}
		[Browsable(false)]
		public int ColWidthDeadline {
			get { return this.chDeadline.Width; }
			set {
				this.chDeadline.Width = value;
				this.OnPropertyChanged(CrawlResultView.ColWidthPropertyNames[5]);
			}
		}
		[Browsable(false)]
		public int ColWidthSummary {
			get { return this.chSummary.Width; }
			set {
				this.chSummary.Width = value;
				this.OnPropertyChanged(CrawlResultView.ColWidthPropertyNames[6]);
			}
		}
		#endregion
	}
	
	public enum CrawlResultViewFilterType {
		Normal,
		Migemo,
		Regex,
	}
	public enum CrawlResultViewView {
		Details,
		Tile,
		Icon,
	}

	interface ICrawlResultViewBindingContract : IBindingContract {
		string DestinationPlaylistName { get;set;}

		ContentVisibilities ContentVisibilities { get;set;}
		CrawlResultViewView CrvView { get;set;}
		
		Color ColorNew { get;set;}
		Color ColorModified { get;set;}
		Color ColorFilter { get;set;}

		bool GroupingAtTheBegining { get;set;}

		bool FilterBarVisible { get;set;}
		CrawlResultViewFilterType FilterType { get;set;}
		bool IncrementalFilterEnabled { get;set;}
		bool CaseInsensitiveFilter { get;set;}

		int ColWidthId { get;set;}
		int ColWidthTitle { get;set;}
		int ColWidthSeriesNumber { get;set;}
		int ColWidthSubtitle { get;set;}
		int ColWidthDuration { get;set;}
		int ColWidthDeadline { get;set;}
		int ColWidthSummary { get;set;}
	}
	public sealed class CrawlResultViewOptions : ICrawlResultViewBindingContract {
		public CrawlResultViewOptions() {
		}
		
		#region ICrawlResultViewBindingContract Members
		private string destinationPlaylistName = CrawlResultView.DefaultDestinationPlaylistName;
		[Category("動作")]
		[DisplayName("追加先のプレイリスト名")]
		[Description("規定の追加先のプレイリスト名を指定します．")]
		[DefaultValue(CrawlResultView.DefaultDestinationPlaylistName)]
		public string DestinationPlaylistName {
			get { return this.destinationPlaylistName; }
			set { this.destinationPlaylistName = value; }
		}

		private ContentVisibilities contentVisibilities = ContentVisibilities.PresetToumei;
		[Category("表示")]
		[DisplayName("表示条件")]
		[Description("コンテンツの表示条件を指定します．")]
		[DefaultValue(ContentVisibilities.PresetToumei)]
		public ContentVisibilities ContentVisibilities {
			get { return this.contentVisibilities; }
			set {
				this.contentVisibilities = ContentVisibilitiesUtility.SanitizeValue(value);
			}
		}
		private CrawlResultViewView crvView = CrawlResultViewView.Details;
		[Category("表示")]
		[DisplayName("表示形式")]
		[Description("コンテンツの表示形式を指定します．Tileを指定できるのはXP以降のみです．")]
		[DefaultValue(CrawlResultViewView.Details)]
		public CrawlResultViewView CrvView {
			get { return this.crvView; }
			set { this.crvView = value; }
		}
		private Color colorNew = Color.Red;
		[Category("表示")]
		[DisplayName("新着の色")]
		[Description("新着コンテンツの文字色を指定します．")]
		[DefaultValue(typeof(Color), "Red")]
		[XmlIgnore]
		public Color ColorNew {
			get { return this.colorNew; }
			set { this.colorNew = value; }
		}
		private Color colorModified = Color.DarkOrange;
		[Category("表示")]
		[DisplayName("変更の色")]
		[Description("属性値に変更があったコンテンツの文字色を指定します．")]
		[DefaultValue(typeof(Color), "DarkOrange")]
		[XmlIgnore]
		public Color ColorModified {
			get { return this.colorModified; }
			set { this.colorModified = value; }
		}
		private Color colorFilter = Color.LemonChiffon;
		[Category("表示")]
		[DisplayName("絞込みの色")]
		[Description("絞込みの対象となったコンテンツの背景色を指定します．")]
		[DefaultValue(typeof(Color), "LemonChiffon")]
		[XmlIgnore]
		public Color ColorFilter {
			get { return this.colorFilter; }
			set { this.colorFilter = value; }
		}
		private bool groupingAtTheBegining = true;
		[Category("表示")]
		[DisplayName("グループ化")]
		[Description("クロール結果を表示した直後にパッケージでグループ化します．グループ化ができるのはXP以降です．")]
		[DefaultValue(true)]
		public bool GroupingAtTheBegining {
			get { return this.groupingAtTheBegining; }
			set { this.groupingAtTheBegining = value; }
		}
		
		private bool filterBarVisible = false;
		[Category("絞込み")]
		[DisplayName("絞込みバーを表示")]
		[Description("絞込みツールバーを表示します．")]
		[DefaultValue(false)]
		public bool FilterBarVisible {
			get { return this.filterBarVisible; }
			set { this.filterBarVisible = value; }
		}
		private CrawlResultViewFilterType filterType = CrawlResultViewFilterType.Normal;
		[Category("絞込み")]
		[DisplayName("絞込みの方法")]
		[Description("絞込みの方法を指定します．")]
		[DefaultValue(CrawlResultViewFilterType.Normal)]
		public CrawlResultViewFilterType FilterType {
			get { return this.filterType; }
			set { this.filterType = value; }
		}
		private bool incrementalFilterEnabled = true;
		[Category("絞込み")]
		[DisplayName("インクリメンタル")]
		[Description("インクリメンタルに絞込みを実行します．Falseにした場合は絞込みを行うたびにEnterを押す必要があります．")]
		[DefaultValue(true)]
		public bool IncrementalFilterEnabled {
			get { return this.incrementalFilterEnabled; }
			set { this.incrementalFilterEnabled = value; }
		}
		private bool caseInsensitiveFilter = true;
		[Category("絞込み")]
		[DisplayName("ケースインセンシティブ")]
		[Description("Trueにすると絞込みで大文字と小文字の区別をしません．")]
		[DefaultValue(true)]
		public bool CaseInsensitiveFilter {
			get { return this.caseInsensitiveFilter; }
			set { this.caseInsensitiveFilter = value; }
		}
		
		private int colWidthId = -1;
		[Category("カラム幅")]
		[DisplayName("[0] contents_id")]
		[Description("カラム 'contents_id' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthId {
			get { return this.colWidthId; }
			set { this.colWidthId = value; }
		}
		private int colWidthTitle = -1;
		[Category("カラム幅")]
		[DisplayName("[1] タイトル")]
		[Description("カラム 'タイトル' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthTitle {
			get { return this.colWidthTitle; }
			set { this.colWidthTitle = value; }
		}
		private int colWidthSeriesNumber = -1;
		[Category("カラム幅")]
		[DisplayName("[2] シリーズ番号")]
		[Description("カラム 'シリーズ番号' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthSeriesNumber {
			get { return this.colWidthSeriesNumber; }
			set { this.colWidthSeriesNumber = value; }
		}
		private int colWidthSubtitle = -1;
		[Category("カラム幅")]
		[DisplayName("[3] サブタイトル")]
		[Description("カラム 'サブタイトル' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthSubtitle {
			get { return this.colWidthSubtitle; }
			set { this.colWidthSubtitle = value; }
		}
		private int colWidthDuration = -1;
		[Category("カラム幅")]
		[DisplayName("[4] 時間")]
		[Description("カラム '時間' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthDuration {
			get { return this.colWidthDuration; }
			set { this.colWidthDuration = value; }
		}
		private int colWidthDeadline = -1;
		[Category("カラム幅")]
		[DisplayName("[5] 期限")]
		[Description("カラム '期限' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthDeadline {
			get { return this.colWidthDeadline; }
			set { this.colWidthDeadline = value; }
		}
		private int colWidthSummary = -1;
		[Category("カラム幅")]
		[DisplayName("[6] サマリー")]
		[Description("カラム 'サマリー' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthSummary {
			get { return this.colWidthSummary; }
			set { this.colWidthSummary = value; }
		}
		#endregion

		[Browsable(false)]
		public XmlSerializableColor ColorNewXmlSerializable {
			get { return new XmlSerializableColor(this.ColorNew); }
			set { this.ColorNew = value.ToColor(); }
		}
		[Browsable(false)]
		public XmlSerializableColor ColorModifiedXmlSerializable {
			get { return new XmlSerializableColor(this.ColorModified); }
			set { this.ColorModified = value.ToColor(); }
		}
		[Browsable(false)]
		public XmlSerializableColor ColorFilterXmlSerializable {
			get { return new XmlSerializableColor(this.ColorFilter); }
			set { this.ColorFilter = value.ToColor(); }
		}
		
		internal void NeutralizeUnspecificValues(CrawlResultView crv) {
			if (this.ColWidthId < 0) this.ColWidthId = crv.ColWidthId;
			if (this.ColWidthTitle < 0) this.ColWidthTitle = crv.ColWidthTitle;
			if (this.ColWidthSeriesNumber < 0) this.ColWidthSeriesNumber = crv.ColWidthSeriesNumber;
			if (this.ColWidthSubtitle < 0) this.ColWidthSubtitle = crv.ColWidthSubtitle;
			if (this.ColWidthDuration < 0) this.ColWidthDuration = crv.ColWidthDuration;
			if (this.ColWidthDeadline < 0) this.ColWidthDeadline = crv.ColWidthDeadline;
			if (this.ColWidthSummary < 0) this.ColWidthSummary = crv.ColWidthSummary;
		}
	}
}
