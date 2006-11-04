using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class PlaylistsViewCControl : UserControl, INotifyPropertyChanged, IPlaylistsViewCControlBindingContract {
		private sealed class PlaylistListViewItem : ListViewItem {
			private Playlist playlist;
			private bool isSelected;
			private bool isPlaying;
			public PlaylistListViewItem(Playlist pl) : base(new string[5]) {
				this.Playlist = pl;
				base.UseItemStyleForSubItems = false;
			}
			public Playlist Playlist {
				get { return this.playlist; }
				set {
					//if(value == this.playlist) return;
					this.playlist = value;
					base.SubItems[0].Text = value.Name;
					base.SubItems[1].Text = value.ContentCount.ToString();
					base.SubItems[2].Text = value.SubtotalTime.ToString();
					base.SubItems[3].Text = value.Created.ToString();
					base.SubItems[4].Text = value.LastModified.ToString();
				}
			}
			public void MarkSelectedIfEqualsTo(Playlist pl) {
				if (this.playlist == pl) {
					if (this.isSelected) return;
					this.isSelected = true;
				} else {
					if (!this.isSelected) return;
					this.isSelected = false;
				}
				this.RecreateFont();
			}
			public void MarkPlayingIfEqualsTo(Playlist pl) {
				if (this.playlist == pl) {
					if (this.isPlaying) return;
					this.isPlaying = true;
				} else {
					if (!this.isPlaying) return;
					this.isPlaying = false;
				}
				this.RecreateFont();
			}
			private void RecreateFont() {
				FontStyle fs = FontStyle.Regular;
				if (this.isSelected) fs |= FontStyle.Bold;
				if (this.isPlaying) fs |= FontStyle.Underline;
				base.Font = new Font(base.Font, fs);
			}
		}
		
		private static readonly string[] ColWidthPropertyNames = new string[] {
			"ColWidthName", "ColWidthCount", "ColWidthTime", "ColWidthCreated", "ColWidthModified"
		};
		private static readonly Comparison<Playlist>[] PlaylistComparisons = new Comparison<Playlist>[]{
			new Comparison<Playlist>(delegate(Playlist x, Playlist y){
				return x.Name.CompareTo(y.Name);
			}),
			new Comparison<Playlist>(delegate(Playlist x, Playlist y){
				return x.ContentCount.CompareTo(y.ContentCount);
			}),
			new Comparison<Playlist>(delegate(Playlist x, Playlist y){
				return x.SubtotalTime.CompareTo(y.SubtotalTime);
			}),
			new Comparison<Playlist>(delegate(Playlist x, Playlist y){
				return x.Created.CompareTo(y.Created);
			}),
			new Comparison<Playlist>(delegate(Playlist x, Playlist y){
				return x.LastModified.CompareTo(y.LastModified);
			}),
		};
		
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler ViewPlaylistRequested;
		public event EventHandler StatusMessagesChanged;
		
		private PlaylistsManager plManager;
		private Playlist lastRequestedPlaylist;
		private string statusPlaylistCount;
		private string statusContentCount;
		private string statusDurationSum;
		
		private StackableComparisonsComparer<Playlist> comparer = new StackableComparisonsComparer<Playlist>();
		
		public PlaylistsViewCControl() {
			InitializeComponent();

			this.tsmiCmsViewPlaylistContents.Font = new Font(this.tsmiCmsViewPlaylistContents.Font, FontStyle.Bold);
		}

		private void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (null != handler) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		public Playlist LastRequestedPlaylist {
			get { return this.lastRequestedPlaylist; }
		}
		public string StatusPlaylistCount {
			get { return this.statusPlaylistCount; }
		}
		public string StatusContentCount {
			get { return this.statusContentCount; }
		}
		public string StatusDurationSum {
			get { return this.statusDurationSum; }
		}

		private void OnViewPlaylistsRequested(Playlist pl) {
			this.lastRequestedPlaylist = pl;
			EventHandler handler = this.ViewPlaylistRequested;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnStatusMessagesChanged(string pCount, string cCount, string dSum) {
			this.statusPlaylistCount = pCount;
			this.statusContentCount = cCount;
			this.statusDurationSum = dSum;
			EventHandler handler = this.StatusMessagesChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}

		private void PlaylistsViewCControl_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;

			List<ToolStripItem> sortMenuItems = new List<ToolStripItem>(this.lvPlaylists.Columns.Count);
			foreach (ColumnHeader ch in this.lvPlaylists.Columns) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(ch.Text);
				tsmi.Tag = ch.Index;
				tsmi.Click += delegate(object sender2, EventArgs e2) {
					this.PushComparison((int)(sender2 as ToolStripMenuItem).Tag);
				};
				sortMenuItems.Add(tsmi);
			}
			this.tsmiSort.DropDownItems.AddRange(sortMenuItems.ToArray());

			if (null == Program.PlaylistsManager) return;
			
			this.plManager = Program.PlaylistsManager;
			this.plManager.PlaylistsManagerChanged += new EventHandler(this.plManager_PlaylistsManagerChanged);
			this.plManager.CurrentContentAndPlaylistChanged += new EventHandler(this.plManager_CurrentContentAndPlaylistChanged);
			this.Disposed += delegate {
				this.plManager.PlaylistsManagerChanged -= new EventHandler(this.plManager_PlaylistsManagerChanged);
				this.plManager.CurrentContentAndPlaylistChanged -= new EventHandler(this.plManager_CurrentContentAndPlaylistChanged);
			};
			this.lvPlaylists.BeginUpdate();
			this.UpdateListItems();
			this.UpdatePlayingMark();
			this.ScrollToTop();
			this.lvPlaylists.EndUpdate();
			this.UpdateStatusMessages();
			this.ChangeEnabilityOfMenuItems();
		}

		private void plManager_PlaylistsManagerChanged(object sender, EventArgs e) {
			this.lvPlaylists.BeginUpdate();
			this.UpdateListItems();
			this.UpdateSelectedMark();
			this.UpdatePlayingMark();
			this.lvPlaylists.EndUpdate();
			this.UpdateStatusMessages();
			this.ChangeEnabilityOfMenuItems();
		}
		private void plManager_CurrentContentAndPlaylistChanged(object sender, EventArgs e) {
			this.UpdatePlayingMark();
		}

		private void ScrollToTop() {
			if (0 < this.lvPlaylists.Items.Count) {
				this.lvPlaylists.TopItem = this.lvPlaylists.Items[0];
			}
		}
		private void ScrollToBottom() {
			int count = this.lvPlaylists.Items.Count;
			if (0 < count) {
				this.lvPlaylists.TopItem = this.lvPlaylists.Items[count - 1];
			}
		}
		private void UpdateListItems() {
			int oldCount = this.lvPlaylists.Items.Count;
			int newCount = this.plManager.PlaylistsCount;
			int minCount = (oldCount < newCount) ? oldCount : newCount;
			
			this.lvPlaylists.BeginUpdate();
			//従来のアイテムの値を書き換える
			for (int i = 0; i < minCount; i++) {
				Playlist pl = this.plManager[i];
				PlaylistListViewItem plvi = this.lvPlaylists.Items[i] as PlaylistListViewItem;
				plvi.Playlist = pl;
			}
			//増減をチェックしてから差分を埋める
			if (minCount == newCount) { //減ったか同じの場合
				for (int i = oldCount - 1; i >= newCount; i--) {
					this.lvPlaylists.Items.RemoveAt(i);
				}
			} else if (minCount == oldCount) {//増えた場合
				for (int i = minCount; i < newCount; i++) {
					Playlist pl = this.plManager[i];
					PlaylistListViewItem plvi = new PlaylistListViewItem(pl);
					this.lvPlaylists.Items.Add(plvi);
				}
				//個数が増えたのならばおそらく末尾への追加だろう
				this.ScrollToBottom();
			}
			
			this.lvPlaylists.EndUpdate();
		}
		private void UpdateSelectedMark() {
			foreach (PlaylistListViewItem plvi in this.lvPlaylists.Items) {
				plvi.MarkSelectedIfEqualsTo(this.lastRequestedPlaylist);
			}
		}
		private void UpdatePlayingMark() {
			Playlist currentPlaylist = Program.PlaylistsManager.CurrentPlaylist;
			foreach (PlaylistListViewItem plvi in this.lvPlaylists.Items) {
				plvi.MarkPlayingIfEqualsTo(currentPlaylist);
			}
		}
		private void UpdateStatusMessages() {
			string pCount;
			string cCount;
			string time;
			Playlist[] selectedLists = this.GetSelectedPlaylists();
			if (selectedLists.Length == 0) {
				pCount = this.plManager.PlaylistsCount.ToString();
				cCount = this.plManager.GrandTotalContentsCount.ToString();
				time = this.plManager.GrandTotalTime.ToString();
			} else {
				int contCount = 0;
				TimeSpan tspan = TimeSpan.Zero;
				foreach (Playlist pl in selectedLists) {
					contCount += pl.ContentCount;
					tspan += pl.SubtotalTime;
				}
				pCount = string.Format("{0}/{1}", selectedLists.Length, this.plManager.PlaylistsCount);
				cCount = string.Format("{0}/{1}", contCount, this.plManager.GrandTotalContentsCount);
				time = string.Format("{0}/{1}", tspan, this.plManager.GrandTotalTime);
			}
			this.OnStatusMessagesChanged(pCount, cCount, time);
		}

		private Playlist[] GetSelectedPlaylists() {
			List<Playlist> ls = new List<Playlist>();
			foreach (PlaylistListViewItem plvi in this.lvPlaylists.SelectedItems) {
				ls.Add(plvi.Playlist);
			}
			return ls.ToArray();
		}
		private void SetSelectedPlaylists(IEnumerable<Playlist> playlists) {
			List<Playlist> pls = new List<Playlist>(playlists);
			this.lvPlaylists.BeginUpdate();
			foreach (PlaylistListViewItem plvi in this.lvPlaylists.Items) {
				plvi.Selected = pls.Contains(plvi.Playlist);
			}
			this.lvPlaylists.EndUpdate();
		}
		
		#region メインメニュー
		private void tsmiViewContents_Click(object sender, EventArgs e) {
			this.OnViewPlaylistsRequested(this.GetSelectedPlaylists()[0]);
			this.UpdateSelectedMark();
		}
		private void tsmiPlay_Click(object sender, EventArgs e) {
			Playlist pl = this.GetSelectedPlaylists()[0];
			if (pl.ContentCount > 0) {
				Program.PlayContent(pl[0], pl);
			} else {
				Program.PlayContent(null, pl);
			}
		}
		private void tsmiRename_Click(object sender, EventArgs e) {
			ListViewItem lvi = this.lvPlaylists.FocusedItem;
			if (null != lvi) {
				lvi.BeginEdit();
			}
		}
		private void tsmiRearrangeToTop_Click(object sender, EventArgs e) {
			this.lvPlaylists.BeginUpdate();
			this.plManager.BeginUpdate();
			Playlist[] pls = this.GetSelectedPlaylists();
			for (int i = pls.Length - 1; i >= 0; i--) {
				this.plManager.MoveToTop(pls[i]);
			}
			this.plManager.EndUpdate();
			this.SetSelectedPlaylists(pls);
			this.lvPlaylists.EndUpdate();
			this.ScrollToTop();
		}
		private void tsmiRearrangeUp_Click(object sender, EventArgs e) {
			this.lvPlaylists.BeginUpdate();
			this.plManager.BeginUpdate();
			Playlist[] pls = this.GetSelectedPlaylists();
			foreach (Playlist pl in pls) {
				this.plManager.MoveUp(pl);
			}
			this.plManager.EndUpdate();
			this.SetSelectedPlaylists(pls);
			this.lvPlaylists.EndUpdate();
		}
		private void tsmiRearrangeDown_Click(object sender, EventArgs e) {
			this.lvPlaylists.BeginUpdate();
			this.plManager.BeginUpdate();
			Playlist[] pls = this.GetSelectedPlaylists();
			for (int i = pls.Length - 1; i >= 0; i--) {
				this.plManager.MoveDown(pls[i]);
			}
			this.plManager.EndUpdate();
			this.SetSelectedPlaylists(pls);
			this.lvPlaylists.EndUpdate();
		}
		private void tsmiRearrangeToBottom_Click(object sender, EventArgs e) {
			this.lvPlaylists.BeginUpdate();
			this.plManager.BeginUpdate();
			Playlist[] pls = this.GetSelectedPlaylists();
			foreach (Playlist pl in pls) {
				this.plManager.MoveToBottom(pl);
			}
			this.plManager.EndUpdate();
			this.SetSelectedPlaylists(pls);
			this.lvPlaylists.EndUpdate();
			this.ScrollToBottom();
		}
		private void tsmiDelete_Click(object sender, EventArgs e) {
			this.plManager.BeginUpdate();
			foreach (Playlist pl in this.GetSelectedPlaylists()) {
				this.plManager.RemovePlaylist(pl);
			}
			this.plManager.EndUpdate();
			this.lvPlaylists.SelectedIndices.Clear();
		}
		#endregion
		
		#region コンテキストメニュー
		private void tsmiCmsViewPlaylistContents_Click(object sender, EventArgs e) {
			this.tsmiViewContents.PerformClick();
		}
		private void tsmiCmsPlay_Click(object sender, EventArgs e) {
			this.tsmiPlay.PerformClick();
		}
		private void tsmiCmsRenamePlaylist_Click(object sender, EventArgs e) {
			this.tsmiRename.PerformClick();
		}
		private void tsmiCmsRearrangeToTop_Click(object sender, EventArgs e) {
			this.tsmiRearrangeToTop.PerformClick();
		}
		private void tsmiCmsRearrangeUp_Click(object sender, EventArgs e) {
			this.tsmiRearrangeUp.PerformClick();
		}
		private void tsmiCmsRearrangeDown_Click(object sender, EventArgs e) {
			this.tsmiRearrangeDown.PerformClick();
		}
		private void tsmiCmsRearrangeToBottom_Click(object sender, EventArgs e) {
			this.tsmiRearrangeToBottom.PerformClick();
		}
		private void tsmiCmsDelete_Click(object sender, EventArgs e) {
			this.tsmiDelete.PerformClick();
		}
		#endregion
		#region リストビューのイベントハンドラ
		private void lvPlaylists_SelectedIndexChanged(object sender, EventArgs e) {
			this.UpdateStatusMessages();
			this.ChangeEnabilityOfMenuItems();
		}
		private void lvPlaylists_BeforeLabelEdit(object sender, LabelEditEventArgs e) {
			this.cmsPlaylists.Enabled = false;
		}
		private void lvPlaylists_AfterLabelEdit(object sender, LabelEditEventArgs e) {
			this.cmsPlaylists.Enabled = true;
			if (null == e.Label) {
				e.CancelEdit = true;
				return;
			}
			PlaylistListViewItem plvi = this.lvPlaylists.Items[e.Item] as PlaylistListViewItem;
			if (plvi.Playlist.Name != e.Label) {
				plvi.Playlist.Name = e.Label;
			}
		}
		private void lvPlaylists_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					this.tsmiCmsViewPlaylistContents.PerformClick();
					break;
			}
		}
		private void lvPlaylists_MouseDoubleClick(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.Left:
					this.tsmiCmsViewPlaylistContents.PerformClick();
					break;
			}
		}
		private void lvPlaylists_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.PushComparison(e.Column);
		}
		private void lvPlaylists_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e) {
			string propName = PlaylistsViewCControl.ColWidthPropertyNames[e.ColumnIndex];
			this.OnPropertyChanged(propName);
		}
		#endregion
		
		public ToolStripDropDown GetToolStripDropDown() {
			return this.pVCToolStripMenuItem.DropDown;
		}
		private void ChangeEnabilityOfMenuItems() {
			bool isSelected = (this.lvPlaylists.SelectedIndices.Count > 0);
			bool hasFocusedItem = (null != this.lvPlaylists.FocusedItem);
			
			this.tsmiViewContents.Enabled = isSelected;
			this.tsmiPlay.Enabled = isSelected;
			this.tsmiRename.Enabled = hasFocusedItem;
			this.tsmiRearrange.Enabled = isSelected;
			this.tsmiRearrangeToTop.Enabled = isSelected;
			this.tsmiRearrangeUp.Enabled = isSelected;
			this.tsmiRearrangeDown.Enabled = isSelected;
			this.tsmiRearrangeToBottom.Enabled = isSelected;
			this.tsmiDelete.Enabled = isSelected;

			this.tsmiCmsViewPlaylistContents.Enabled = isSelected;
			this.tsmiCmsPlay.Enabled = isSelected;
			this.tsmiCmsRearrange.Enabled = hasFocusedItem;
			this.tsmiCmsRearrange.Enabled = isSelected;
			this.tsmiCmsRearrangeToTop.Enabled = isSelected;
			this.tsmiCmsRearrangeUp.Enabled = isSelected;
			this.tsmiCmsRearrangeDown.Enabled = isSelected;
			this.tsmiCmsRearrangeToBottom.Enabled = isSelected;
			this.tsmiCmsDelete.Enabled = isSelected;
		}
		
		
		#region IPlaylistsViewCControlBindingContract Members
		public int ColWidthName {
			get {
				return this.chName.Width;
			}
			set {
				this.chName.Width = value;
				this.OnPropertyChanged(PlaylistsViewCControl.ColWidthPropertyNames[0]);
			}
		}
		public int ColWidthCount {
			get {
				return this.chCount.Width;
			}
			set {
				this.chCount.Width = value;
				this.OnPropertyChanged(PlaylistsViewCControl.ColWidthPropertyNames[1]);
			}
		}
		public int ColWidthTime {
			get {
				return this.chTime.Width;
			}
			set {
				this.chTime.Width = value;
				this.OnPropertyChanged(PlaylistsViewCControl.ColWidthPropertyNames[2]);
			}
		}
		public int ColWidthCreated {
			get {
				return this.chCreated.Width;
			}
			set {
				this.chCreated.Width = value;
				this.OnPropertyChanged(PlaylistsViewCControl.ColWidthPropertyNames[3]);
			}
		}
		public int ColWidthModified {
			get {
				return this.chModified.Width;
			}
			set {
				this.chModified.Width = value;
				this.OnPropertyChanged(PlaylistsViewCControl.ColWidthPropertyNames[4]);
			}
		}
		#endregion

		private void PushComparison(int idx) {
			this.comparer.PushComparison(PlaylistsViewCControl.PlaylistComparisons[idx]);
			this.plManager.Sort(this.comparer);
		}
		
		public void BindToOptions(PlaylistsViewCControlOptions options) {
			options.NeutralizeUnspecificValues(this);
			BindingContractUtility.BindAllProperties<PlaylistsViewCControl, IPlaylistsViewCControlBindingContract>(this, options);
		}
		
	}

	interface IPlaylistsViewCControlBindingContract : IBindingContract {
		int ColWidthName { get;set;}
		int ColWidthCount { get;set;}
		int ColWidthTime { get;set;}
		int ColWidthCreated { get;set;}
		int ColWidthModified { get;set;}
	}

	public sealed class PlaylistsViewCControlOptions : IPlaylistsViewCControlBindingContract {
		public PlaylistsViewCControlOptions() {
		}

		#region IPlaylistsViewCControlBindingContract Members
		private int colWidthName = -1;
		[Category("カラム幅")]
		[DisplayName("[0] プレイリスト名")]
		[Description("カラム 'プレイリスト名' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthName {
			get { return this.colWidthName; }
			set { this.colWidthName = value; }
		}

		private int colWidthCount = -1;
		[Category("カラム幅")]
		[DisplayName("[1] 数")]
		[Description("カラム '数' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthCount {
			get { return this.colWidthCount; }
			set { this.colWidthCount = value; }
		}

		private int colWidthTime = -1;
		[Category("カラム幅")]
		[DisplayName("[2] 時間")]
		[Description("カラム '時間' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthTime {
			get { return this.colWidthTime; }
			set { this.colWidthTime = value; }
		}

		private int colWidthCreated = -1;
		[Category("カラム幅")]
		[DisplayName("[3] 作成日時")]
		[Description("カラム '作成日時' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthCreated {
			get { return this.colWidthCreated; }
			set { this.colWidthCreated = value; }
		}

		private int colWidthModified = -1;
		[Category("カラム幅")]
		[DisplayName("[4] 最終更新日時")]
		[Description("カラム '最終更新日時' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthModified {
			get { return this.colWidthModified; }
			set { this.colWidthModified = value; }
		}
		#endregion

		internal void NeutralizeUnspecificValues(PlaylistsViewCControl cControl) {
			if (this.ColWidthName < 0) this.ColWidthName = cControl.ColWidthName;
			if (this.ColWidthCount < 0) this.ColWidthCount = cControl.ColWidthCount;
			if (this.ColWidthTime < 0) this.ColWidthTime = cControl.ColWidthTime;
			if (this.ColWidthCreated < 0) this.ColWidthCreated = cControl.ColWidthCreated;
			if (this.ColWidthModified < 0) this.ColWidthModified = cControl.ColWidthModified;
		}
	}
}
