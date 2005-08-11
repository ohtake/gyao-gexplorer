using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Yusen.GCrawler;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	partial class GenreListView : UserControl, IHasSettings<GenreListViewSettings>{
		public event EventHandler<SelectedContentChangedEventArgs> SelectedContentChanged;
		public event EventHandler<GenreListViewGenreShowedEventArgs> GenreShowed;
		
		private GGenre genre;
		private AboneType aboneType = AboneType.Sabori;
		private Color newColor = Color.Red;
		
		public GenreListView() {
			InitializeComponent();
			
			this.tsmiPlay.Font = new Font(this.tsmiPlay.Font, FontStyle.Bold);
			
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
		public GGenre Genre {
			get {
				return this.genre;
			}
			set {
				lock (this) {
					this.genre = value;
					if (null == value) {
						this.ClearContents();
					} else {
						this.ClearContents();
						this.DisplayContents();
						if (null != this.GenreShowed) {
							this.GenreShowed(this, new GenreListViewGenreShowedEventArgs(this.genre, this.listView1.Items.Count));
						}
					}
				}
			}
		}
		public ContentAdapter SelectedContent {
			get {
				if (1 == this.listView1.SelectedItems.Count) {
					return this.listView1.SelectedItems[0].Tag as ContentAdapter;
				} else {
					return null;
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
			settings.ColWidthId = this.listView1.Columns[0].Width;
			settings.ColWidthTitle = this.listView1.Columns[1].Width;
			settings.ColWidthEpisode = this.listView1.Columns[2].Width;
			settings.ColWidthSubtitle = this.listView1.Columns[3].Width;
			settings.ColWidthDuration = this.listView1.Columns[4].Width;
			settings.ColWidthLongDesc = this.listView1.Columns[5].Width;
			settings.AboneType = this.AboneType;
			settings.NewColor = this.NewColor;
		}
		public void ApplySettings(GenreListViewSettings settings) {
			this.listView1.Columns[0].Width = settings.ColWidthId ?? this.listView1.Columns[0].Width;
			this.listView1.Columns[1].Width = settings.ColWidthTitle ?? this.listView1.Columns[1].Width;
			this.listView1.Columns[2].Width = settings.ColWidthEpisode ?? this.listView1.Columns[2].Width;
			this.listView1.Columns[3].Width = settings.ColWidthSubtitle ?? this.listView1.Columns[3].Width;
			this.listView1.Columns[4].Width = settings.ColWidthDuration ?? this.listView1.Columns[4].Width;
			this.listView1.Columns[5].Width = settings.ColWidthLongDesc ?? this.listView1.Columns[5].Width;
			this.AboneType = settings.AboneType ?? this.AboneType;
			this.NewColor = settings.NewColor ?? this.NewColor;
		}
		private void ClearContents() {
			if (null != this.SelectedContentChanged) {
				this.SelectedContentChanged(this, new SelectedContentChangedEventArgs());
			}
			this.listView1.BeginUpdate();
			this.listView1.Items.Clear();
			this.listView1.Groups.Clear();
			this.listView1.EndUpdate();
		}
		private void DisplayContents(){
			this.listView1.BeginUpdate();
			foreach (GPackage p in this.Genre.Packages) {
				ListViewGroup group = new ListViewGroup(p.ToString());
				this.listView1.Groups.Add(group);
				foreach (GContent c in p.Contents) {
					ContentAdapter ca = new ContentAdapter(c, this.Genre);
					bool isNg = NgContentsManager.Instance.IsNgContent(ca);
					switch (this.AboneType) {
						case AboneType.Toumei:
							if (isNg) continue;
							break;
						case AboneType.Hakidame:
							if (!isNg) continue;
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
			this.listView1.EndUpdate();
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
							uc2.Execute(this.SelectedContent);
						}
					}
				};
				this.tsmiUserCommands.DropDownItems.Add(tsmi);
			}
			this.tsmiUserCommands.Enabled = this.tsmiUserCommands.HasDropDownItems;
		}
		private void RefleshView() {
			this.Genre = this.Genre;
		}

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateUserCommandsMenuItems();
		}
		void NgContentsManager_NgContentsChanged(object sender, EventArgs e) {
			this.RefleshView();
		}
		private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
			if (null != this.SelectedContentChanged) {
				this.SelectedContentChanged(this, new SelectedContentChangedEventArgs(this.SelectedContent));
			}
		}
		private void listView1_DoubleClick(object sender, EventArgs e) {
			ContentAdapter cont = this.SelectedContent;
			if (null != cont) {
				PlayerForm.AddToPlayList(cont);
			}
		}
		private void listView1_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					if (null != this.SelectedContent) {
						PlayerForm.AddToPlayList(this.SelectedContent);
					}
					break;
			}
		}
		
		private void cmsContent_Opening(object sender, CancelEventArgs e) {
			bool hasContent = (null != this.SelectedContent);
			foreach (ToolStripItem tsi in this.cmsContent.Items) {
				tsi.Enabled = hasContent;
			}
			this.tsmiUserCommands.Enabled = hasContent && this.tsmiUserCommands.DropDownItems.Count> 0;
			this.tsmiAboneType.Enabled = true;
			this.tsmiNewColor.Enabled = true;
		}
		
		private void tsmiAddToPlaylist_Click(object sender, EventArgs e) {
			PlayerForm.AddToPlayList(this.SelectedContent);
		}
		private void tsmiPlayWithWmp_Click(object sender, EventArgs e) {
			Utility.PlayWithWMP(this.SelectedContent.MediaFileUri);
		}
		private void tsmiPlayWithIe_Click(object sender, EventArgs e) {
			Utility.BrowseWithIE(this.SelectedContent.PlayerPageUri);
		}
		private void tsmiBroseDetailWithIe_Click(object sender, EventArgs e) {
			Utility.BrowseWithIE(this.SelectedContent.DetailPageUri);
		}
		private void tsmiCopyName_Click(object sender, EventArgs e) {
			Clipboard.SetText(this.SelectedContent.DisplayName);
		}
		private void tsmiCopyDetailUri_Click(object sender, EventArgs e) {
			Clipboard.SetText(this.SelectedContent.DetailPageUri.AbsoluteUri);
		}
		private void tsmiCopyNameAndDetailUri_Click(object sender, EventArgs e) {
			Clipboard.SetText(this.SelectedContent.DisplayName + "\r\n" + this.SelectedContent.DetailPageUri.AbsoluteUri);
		}
		private void tsmiAddNgWithId_Click(object sender, EventArgs e) {
			string contId = this.SelectedContent.ContentId;
			NgContentsManager.Instance.Add(new NgContent("簡易追加(ID): " + contId, "ContentId", TwoStringsPredicateMethod.Equals, contId));
		}
		private void tsmiAddNgWithTitle_Click(object sender, EventArgs e) {
			string title = this.SelectedContent.Title;
			NgContentsManager.Instance.Add(new NgContent("簡易追加(タイトル): " + title, "Title", TwoStringsPredicateMethod.Equals, title));
		}
		private void tsmiNewColor_Click(object sender, EventArgs e) {
			this.colorDialog1.Color = this.NewColor;
			if (DialogResult.OK == this.colorDialog1.ShowDialog()) {
				this.NewColor = this.colorDialog1.Color;
			}
		}
	}

	class SelectedContentChangedEventArgs : EventArgs {
		private ContentAdapter content;

		public SelectedContentChangedEventArgs() {
			this.content = null;
		}
		public SelectedContentChangedEventArgs(ContentAdapter content) {
			this.content = content;
		}
		public ContentAdapter Content {
			get { return this.content; }
		}
		public bool IsSelected {
			get {
				return null != this.content;
			}
		}
	}
	class GenreListViewGenreShowedEventArgs : EventArgs {
		private readonly GGenre genre;
		private readonly int numCont;
		public GenreListViewGenreShowedEventArgs(GGenre genre, int numCont) {
			this.genre = genre;
			this.numCont = numCont;
		}
		public GGenre Genre {
			get { return this.genre; }
		}
		public int NumberOfContents {
			get { return this.numCont; }
		}
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
