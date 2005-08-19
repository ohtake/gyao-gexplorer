using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Yusen.GCrawler;
using System.Xml.Serialization;
using System.Text;

namespace Yusen.GExplorer {
	partial class CrawlResultView : UserControl, IHasSettings<GenreListViewSettings>{
		public event EventHandler<SelectedContentsChangedEventArgs> SelectedContentsChanged;
		public event EventHandler<ContentSelectionChangedEventArgs> ContentSelectionChanged;
		
		private CrawlResult crawlResult;
		private AboneType aboneType = AboneType.Sabori;
		private Color newColor = Color.Red;

		public CrawlResultView() {
			InitializeComponent();
			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
			this.tsmiAdd.Font = new Font(this.tsmiAdd.Font, FontStyle.Bold);
			
			this.tsmiAboneType.DropDownItems.Clear();
			foreach (AboneType atype in Enum.GetValues(typeof(AboneType))) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(atype.ToString());
				tsmi.Tag = atype;
				tsmi.Click += delegate(object sender, EventArgs e) {
					this.AboneType = (AboneType)(sender as ToolStripMenuItem).Tag;
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
		}
		
		public CrawlResult CrawlResult {
			get {
				return this.crawlResult;
			}
			set {
				lock (this) {
					this.crawlResult = value;
					
					this.listView1.BeginUpdate();
					this.ClearContents();
					if (null != value) {
						this.DisplayContents();
					}
					this.listView1.EndUpdate();
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
		
		public AboneType AboneType {
			get { return this.aboneType; }
			set {
				if (value != this.aboneType) {
					this.aboneType = value;
					this.RefleshView();
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
		public Color NewColor {
			get { return this.newColor; }
			set {
				if (value != this.newColor) {
					this.newColor = value;
					this.RefleshView();
				}
			}
		}
		public void FillSettings(GenreListViewSettings settings) {
			settings.ColWidthId = this.chId.Width;
			settings.ColWidthTitle = this.chTitle.Width;
			settings.ColWidthEpisode = this.chEpisode.Width;
			settings.ColWidthSubtitle = this.chSubTitle.Width;
			settings.ColWidthDuration = this.chDuration.Width;
			settings.ColWidthLongDesc = this.chDescription.Width;
			settings.AboneType = this.AboneType;
			settings.ShowPackages = this.ShowPackages;
			settings.HoverSelect = this.HoverSelect;
			settings.MultiSelect = this.MultiSelect;
			settings.NewColor = this.NewColor;
		}
		public void ApplySettings(GenreListViewSettings settings) {
			this.chId.Width = settings.ColWidthId ?? this.chId.Width;
			this.chTitle.Width = settings.ColWidthTitle ?? this.chTitle.Width;
			this.chEpisode.Width = settings.ColWidthEpisode ?? this.chEpisode.Width;
			this.chSubTitle.Width = settings.ColWidthSubtitle ?? this.chSubTitle.Width;
			this.chDuration.Width = settings.ColWidthDuration ?? this.chDuration.Width;
			this.chDescription.Width = settings.ColWidthLongDesc ?? this.chDescription.Width;
			this.AboneType = settings.AboneType ?? this.AboneType;
			this.ShowPackages = settings.ShowPackages ?? this.ShowPackages;
			this.HoverSelect = settings.HoverSelect ?? this.HoverSelect;
			this.MultiSelect = settings.MultiSelect ?? this.MultiSelect;
			this.NewColor = settings.NewColor ?? this.NewColor;
		}
		private void ClearContents() {
			if (null != this.SelectedContentsChanged) {
				this.SelectedContentsChanged(this, new SelectedContentsChangedEventArgs());
			}
			this.listView1.Items.Clear();
			this.listView1.Groups.Clear();
			this.tslGenre.Text = "";
			this.tslMessage.Text = "";
		}
		private void DisplayContents(){
			bool showPackages = this.ShowPackages;
			this.ShowPackages = true;
			int abones = 0;
			foreach (GPackage p in this.Genre.Packages) {
				ListViewGroup group = new ListViewGroup(p.ToString());
				this.listView1.Groups.Add(group);
				foreach (GContent c in p.Contents) {
					ContentAdapter ca = new ContentAdapter(c, this.Genre);
					bool isNg = NgContentsManager.Instance.IsNgContent(ca);
					switch (this.AboneType) {
						case AboneType.Hakidame:
							isNg = !isNg;
							goto case AboneType.Toumei;
						case AboneType.Toumei:
							if (isNg) {
								abones++;
								continue;
							}
							break;
					}
					
					ListViewItem item = new ListViewItem(
						new string[]{
							ca.ContentId, ca.Title, ca.EpisodeNumber, ca.SubTitle, ca.Duration, ca.LongDescription},
						group);
					item.Tag = ca;
					if (! ca.FromCache) {
						item.ForeColor = this.NewColor;
					}
					if (isNg && AboneType.Sabori == this.AboneType) {
						item.ForeColor = SystemColors.GrayText;
					}
					
					this.listView1.Items.Add(item);
				}
			}
			this.ShowPackages = showPackages;
			
			this.tslGenre.ForeColor = this.Genre.GenreColor;
			this.tslGenre.Text = "[" + this.Genre.GenreName + "]";
			this.tslMessage.Text =
							this.listView1.Items.Count.ToString() + " + " + abones.ToString() + " 個"
							+ " (" + this.Genre.LastCrawlTime.ToShortTimeString() + ")";
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
		private void RefleshView() {
			this.CrawlResult = this.CrawlResult;
		}

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateUserCommandsMenuItems();
		}
		private void NgContentsManager_NgContentsChanged(object sender, EventArgs e) {
			this.RefleshView();
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
		private void tsmiPlayWithIe_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				Utility.BrowseWithIE(cont.PlayerPageUri);
			}
		}
		private void tsmiBroseDetailWithIe_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				Utility.BrowseWithIE(cont.DetailPageUri);
			}
		}
		private void tsmiCopyName_Click(object sender, EventArgs e) {
			StringBuilder sb = new StringBuilder();
			foreach (ContentAdapter cont in this.SelectedContents) {
				if (sb.Length > 0) {
					sb.Append("\r\n");
				}
				sb.Append(cont.DisplayName);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		private void tsmiCopyDetailUri_Click(object sender, EventArgs e) {
			StringBuilder sb = new StringBuilder();
			foreach (ContentAdapter cont in this.SelectedContents) {
				if (sb.Length > 0) {
					sb.Append("\r\n");
				}
				sb.Append(cont.DetailPageUri.AbsoluteUri);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		private void tsmiCopyNameAndDetailUri_Click(object sender, EventArgs e) {
			StringBuilder sb = new StringBuilder();
			foreach (ContentAdapter cont in this.SelectedContents) {
				if (sb.Length > 0) {
					sb.Append("\r\n");
				}
				sb.Append(cont.DisplayName);
				sb.Append("\r\n");
				sb.Append(cont.DetailPageUri.AbsoluteUri);
			}
			if (sb.Length > 0) {
				Clipboard.SetText(sb.ToString());
			}
		}
		private void tsmiAddNgWithId_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				string contId = cont.ContentId;
				NgContentsManager.Instance.Add(new NgContent("簡易追加(ID)", "ContentId", TwoStringsPredicateMethod.Equals, contId));
			}
		}
		private void tsmiAddNgWithTitle_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				string title = cont.Title;
				NgContentsManager.Instance.Add(new NgContent("簡易追加(タイトル)", "Title", TwoStringsPredicateMethod.Equals, title));
			}
		}
		#endregion
		#region メニューの項目
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
		private int? colWidthLongDesc;
		private AboneType? aboneType;
		private bool? showPackages;
		private bool? hoverSelect;
		private bool? multiSelect;
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
	}
}
