using System;
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
		public event EventHandler<SelectedContentsChangedEventArgs> SelectedContentsChanged;
		public event EventHandler<ContentSelectionChangedEventArgs> ContentSelectionChanged;
		
		private CrawlResult crawlResult;
		private AboneType aboneType = AboneType.Sabori;
		private Color newColor = Color.Red;

		private List<ListViewItem> allLvis = new List<ListViewItem>();
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

			this.CreateUserCommandsMenuItems();
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
			this.Disposed += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			};

			NgContentsManager.Instance.NgContentsChanged += new EventHandler(NgContentsManager_NgContentsChanged);
			this.Disposed += delegate {
				NgContentsManager.Instance.NgContentsChanged -= new EventHandler(NgContentsManager_NgContentsChanged);
			};
			
			try {
				this.migemo = new Migemo(GlobalSettings.Instance.MigemoDictionaryFilename);
				this.Disposed += delegate {
					this.migemo.Dispose();
				};
			} catch (MigemoException ex) {
				this.migemo = null;
				this.tstbFilter.ReadOnly = true;
				this.tstbFilter.BackColor = Color.Pink;
				this.tstbFilter.Text = ex.Message;
			}
		}
		
		public CrawlResult CrawlResult {
			get {
				return this.crawlResult;
			}
			set {
				lock (this) {
					this.crawlResult = value;
					
					this.listView1.BeginUpdate();
					this.listView1.ListViewItemSorter = null;
					this.ClearAllItems();
					if (null != value) {
						this.CreateItems();
						this.DisplayItems();
					}
					this.listView1.EndUpdate();

					this.CreateNormalPagesMenuItems();
				}
			}
		}
		public GGenre Genre {
			get { return this.CrawlResult.Genre; }
		}
		public ContentAdapter[] SelectedContents {
			get {
				List<ContentAdapter> conts = new List<ContentAdapter>();
				foreach (ListViewItem lvi in this.listView1.SelectedItems) {
					conts.Add(lvi.Tag as ContentAdapter);
				}
				return conts.ToArray();
			}
			private set {
				List<ContentAdapter> conts = new List<ContentAdapter>(value);
				foreach (ListViewItem lvi in this.listView1.Items) {
					lvi.Selected = conts.Contains(lvi.Tag as ContentAdapter);
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
				if (this.CanUseMigemo) {
					return this.tstbFilter.Text;
				} else {
					return string.Empty;
				}
			}
			set {
				this.tstbFilter.Text = value;
				this.CreateFilterRegex();
			}
		}
		private Regex FilterRegex {
			get {return this.filterRegex;}
			set {
				this.filterRegex = value;
				this.tstbMigemoAnswer.Text = (null==value)? string.Empty : this.filterRegex.ToString();
			}
		}
		
		public AboneType AboneType {
			get { return this.aboneType; }
			set {
				if (value != this.aboneType) {
					this.aboneType = value;
					this.UpdateView();
				}
				foreach (ToolStripMenuItem tsmi in this.tsmiAboneType.DropDownItems) {
					tsmi.Checked = value == (AboneType)tsmi.Tag;
				}
			}
		}
		public bool ShowPackages {
			get { return this.tsmiShowPackages.Checked; }
			set {
				this.tsmiShowPackages.Checked = value;
				this.listView1.ShowGroups = value;
				if (value) {
					this.UpdateView();
				}
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
		public void FillSettings(GenreListViewSettings settings) {
			settings.ColWidthId = this.chId.Width;
			settings.ColWidthTitle = this.chTitle.Width;
			settings.ColWidthEpisode = this.chEpisode.Width;
			settings.ColWidthSubtitle = this.chSubTitle.Width;
			settings.ColWidthDuration = this.chDuration.Width;
			settings.ColWidthDeadline = this.chDeadline.Width;
			settings.ColWidthLongDesc = this.chDescription.Width;
			settings.AboneType = this.AboneType;
			settings.ShowPackages = this.ShowPackages;
			settings.HoverSelect = this.HoverSelect;
			settings.MultiSelect = this.MultiSelect;
			settings.ClearFilterStringOnHideEnabled = this.ClearFilterStringOnHideEnabled;
			settings.FilterEnabled = this.FilterEnabled;
			settings.NewColor = this.NewColor;
		}
		public void ApplySettings(GenreListViewSettings settings) {
			this.chId.Width = settings.ColWidthId ?? this.chId.Width;
			this.chTitle.Width = settings.ColWidthTitle ?? this.chTitle.Width;
			this.chEpisode.Width = settings.ColWidthEpisode ?? this.chEpisode.Width;
			this.chSubTitle.Width = settings.ColWidthSubtitle ?? this.chSubTitle.Width;
			this.chDuration.Width = settings.ColWidthDuration ?? this.chDuration.Width;
			this.chDeadline.Width = settings.ColWidthDeadline ?? this.chDeadline.Width;
			this.chDescription.Width = settings.ColWidthLongDesc ?? this.chDescription.Width;
			this.AboneType = settings.AboneType ?? this.AboneType;
			this.ShowPackages = settings.ShowPackages ?? this.ShowPackages;
			this.HoverSelect = settings.HoverSelect ?? this.HoverSelect;
			this.MultiSelect = settings.MultiSelect ?? this.MultiSelect;
			this.ClearFilterStringOnHideEnabled = settings.ClearFilterStringOnHideEnabled ?? this.ClearFilterStringOnHideEnabled;
			this.FilterEnabled = settings.FilterEnabled ?? this.FilterEnabled;
			this.NewColor = settings.NewColor ?? this.NewColor;
		}
		private void ClearAllItems() {
			if (null != this.SelectedContentsChanged) {
				this.SelectedContentsChanged(this, new SelectedContentsChangedEventArgs());
			}
			this.allLvis.Clear();
			this.listView1.Items.Clear();
			this.listView1.Groups.Clear();
			this.tslGenre.Text = string.Empty;
			this.tslNumber.Text = string.Empty;
			this.tslTime.Text = string.Empty;
		}
		private void CreateItems() {
			bool showg = this.listView1.ShowGroups;
			if (!showg) {
				this.listView1.ShowGroups = true;
			}
			foreach (GPackage p in this.CrawlResult.Packages) {
				ListViewGroup group = new ListViewGroup(p.ToString());
				this.listView1.Groups.Add(group);
				foreach (GContent c in p.Contents) {
					ContentAdapter ca = new ContentAdapter(c);
					ListViewItem item = new ListViewItem(
						new string[]{
							ca.ContentId, ca.Title, ca.EpisodeNumber, ca.SubTitle, ca.GTimeSpan.ToString(), ca.DeadLine, ca.LongDescription},
						group);
					item.Tag = ca;
					this.allLvis.Add(item);
				}
			}

			if (!showg) {
				this.listView1.ShowGroups = false;
			}
			
			this.tslGenre.ForeColor = this.Genre.GenreColor;
			this.tslGenre.Text = "[" + this.Genre.GenreName + "]";
		}
		private void DisplayItems(){
			if (null == this.CrawlResult) return;
			this.listView1.Items.Clear();

			bool showg = this.listView1.ShowGroups;
			if (!showg) {
				this.listView1.ShowGroups = true;
			}

			int aboned = 0;
			int filtered = 0;
			Regex filter = this.FilterEnabled ? this.FilterRegex : null;
			foreach (ListViewItem lvi in this.allLvis) {
				ContentAdapter cont = lvi.Tag as ContentAdapter;
				bool isNg = NgContentsManager.Instance.IsNgContent(cont);
				//NG����
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
				//�t�B���^����
				if (null != filter) {
					Match match;
					match = filter.Match(lvi.Text);
					if (match.Success) goto unfiltered;
					foreach (ListViewItem.ListViewSubItem si in lvi.SubItems) {
						match = filter.Match(si.Text);
						if (match.Success) goto unfiltered;
					}
					filtered++;
					continue;
				}
			unfiltered:

				//�F�Â�
				lvi.ForeColor = SystemColors.WindowText;
				if (! cont.FromCache) {
					lvi.ForeColor = this.NewColor;
				}
				if (isNg && AboneType.Sabori == this.AboneType) {
					lvi.ForeColor = SystemColors.GrayText;
				}

				this.listView1.Items.Add(lvi);
			}

			if (!showg) {
				this.listView1.ShowGroups = false;
			}
			
			this.tslNumber.Text = this.listView1.Items.Count.ToString() + "+" + filtered.ToString() + "+" + aboned.ToString();
			this.tslTime.Text = "(" + this.CrawlResult.Time.ToShortDateString() + " "+ this.CrawlResult.Time.ToShortTimeString() + ")";
		}
		private void CreateNormalPagesMenuItems() {
			this.tsddbNormalPages.DropDownItems.Clear();
			if (null != this.CrawlResult) {
				foreach (Uri page in this.CrawlResult.VisitedPages) {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(page.PathAndQuery);
					tsmi.Tag = page;
					tsmi.Click += delegate(object sender, EventArgs e) {
						Utility.Browse((sender as ToolStripMenuItem).Tag as Uri);
					};
					this.tsddbNormalPages.DropDownItems.Add(tsmi);
				}
			}
			this.tsddbNormalPages.Enabled = this.tsddbNormalPages.HasDropDownItems;
		}
		private void CreateFilterRegex() {
			if (!this.CanUseMigemo) {
				return;
			}
			string query = this.FilterString;
			Regex regex = null;
			if (string.IsNullOrEmpty(query)) {
				goto success;
			}
			string ans = null;
			try {
				ans = this.migemo.Query(query);
			} catch {
				goto failed;
			}
			if (string.IsNullOrEmpty(ans)) {
				goto failed;
			}
			try {
				regex = new Regex(ans, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
			} catch {
				goto failed;
			}
			goto success;
		failed:
			this.tstbFilter.BackColor= Color.Yellow;
			this.FilterRegex = null;
			return;
		success:
			this.tstbFilter.BackColor = SystemColors.Window;
			this.FilterRegex = regex;
			return;
		}

		private void CreateUserCommandsMenuItems() {
			this.tsmiUserCommands.DropDownItems.Clear();
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
				this.tsmiUserCommands.DropDownItems.Add(tsmi);
			}
			this.tsmiUserCommands.Enabled = this.tsmiUserCommands.HasDropDownItems;
		}
		private void UpdateView() {
			this.listView1.BeginUpdate();
			this.DisplayItems();
			this.listView1.EndUpdate();
		}

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateUserCommandsMenuItems();
		}
		private void NgContentsManager_NgContentsChanged(object sender, EventArgs e) {
			this.UpdateView();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
			if (null != this.SelectedContentsChanged) {
				this.SelectedContentsChanged(this, new SelectedContentsChangedEventArgs(this.SelectedContents));
			}
		}
		private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if (null != this.ContentSelectionChanged) {
				this.ContentSelectionChanged(this, new ContentSelectionChangedEventArgs(e.Item.Tag as ContentAdapter, e.IsSelected));
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
			bool showpackages = this.ShowPackages;
			this.ShowPackages = false;

			ListViewItemComparer comparer = new ListViewItemComparer(e.Column);
			if (comparer.SameIndexAs(this.listView1.ListViewItemSorter as ListViewItemComparer)) {
				comparer = (this.listView1.ListViewItemSorter as ListViewItemComparer).Clone() as ListViewItemComparer;
				comparer.Toggle();
				this.listView1.ListViewItemSorter = comparer;
			} else {
				this.listView1.ListViewItemSorter = comparer;
			}
			
			this.ShowPackages = showpackages;
		}
		
		private void cmsContent_Opening(object sender, CancelEventArgs e) {
			if (0 == this.SelectedContents.Length) {
				e.Cancel = true;
			}
		}
		#region �R���e�L�X�g���j���[�̍���
		private void tsmiAdd_Click(object sender, EventArgs e) {
			PlayList.Instance.BeginUpdate();
			foreach (ContentAdapter cont in this.SelectedContents) {
				PlayList.Instance.AddIfNotExists(cont);
			}
			PlayList.Instance.EndUpdate();
		}
		private void tsmiAddWithComment_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "�R�����g��t���ăv���C���X�g�ɒǉ�";
			this.inputBoxDialog1.Message = "�t������R�����g����͂��Ă��������D";
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
				Utility.PlayWithWMP(cont.MediaFileUri);
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
		private void tsmiAddNgWithId_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				string contId = cont.ContentId;
				NgContentsManager.Instance.Add(new NgContent("�ȈՒǉ�", "ContentId", TwoStringsPredicateMethod.Equals, contId));
			}
		}
		private void tsmiAddNgWithTitle_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				string title = cont.Title;
				NgContentsManager.Instance.Add(new NgContent("�ȈՒǉ�", "Title", TwoStringsPredicateMethod.Equals, title));
			}
		}
		#endregion
		#region ���j���[�̍���
		private void tsmiShowPackages_Click(object sender, EventArgs e) {
			this.ShowPackages = this.ShowPackages;
		}
		private void tsmiHoverSelect_Click(object sender, EventArgs e) {
			this.HoverSelect = this.HoverSelect;
		}
		private void tsmiMultiSelect_Click(object sender, EventArgs e) {
			this.MultiSelect = this.MultiSelect;
		}
		private void tsmiNewColor_Click(object sender, EventArgs e) {
			this.colorDialog1.Color = this.NewColor;
			if (DialogResult.OK == this.colorDialog1.ShowDialog()) {
				this.NewColor = this.colorDialog1.Color;
			}
		}
		#endregion
		#region �t�B���^�֘A
		private void tsbShowFilter_Click(object sender, EventArgs e) {
			this.FilterEnabled = this.FilterEnabled;
			if (this.CanUseMigemo && this.ClearFilterStringOnHideEnabled && !this.FilterEnabled) {
				this.FilterString = string.Empty;
			}
			this.UpdateView();
		}
		private void tstbFilter_TextChanged(object sender, EventArgs e) {
			this.CreateFilterRegex();
			this.UpdateView();
		}
		#endregion
	}

	public enum AboneType {
		Toumei,
		Sabori,
		Hakidame,
	}
	public class GenreListViewSettings {
		private int? colWidthId;
		private int? colWidthTitle;
		private int? colWidthEpisode;
		private int? colWidthSubtitle;
		private int? colWidthDuration;
		private int? colWidthDeadline;
		private int? colWidthLongDesc;
		private AboneType? aboneType;
		private bool? showPackages;
		private bool? hoverSelect;
		private bool? multiSelect;
		private bool? clearFilterStringOnHideEnabled;
		private bool? filterEnabled;
		private Color? newColor;
		
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
		
		[XmlIgnore] //Color��XML�V���A���C�Y�ł��Ȃ��H
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
	}
}
