using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class PlaylistsViewPControl : UserControl, INotifyPropertyChanged, IPlaylistsViewPControlBindingContract {
		private sealed class ContentListViewItem : ListViewItem {
			private GContentClass content;
			private bool isPlaying;
			public ContentListViewItem(GContentClass content)
				: base(new string[] { content.ContentId, content.Title, content.SeriesNumber, content.Subtitle, content.DurationValue.ToString(), content.DeadlineValue.ToString() }) {
				this.content = content;
				//base.UseItemStyleForSubItems = false;
			}
			public GContentClass Content {
				get { return this.content; }
				set {
					if (this.content == value) return;
					this.content = value;
					this.SubItems[0].Text = value.ContentId;
					this.SubItems[1].Text = value.Title;
					this.SubItems[2].Text = value.SeriesNumber;
					this.SubItems[3].Text = value.Subtitle;
					this.SubItems[4].Text = value.DurationValue.ToString();
					this.SubItems[5].Text = value.DeadlineValue.ToString();
				}
			}
			public void MarkPlayingIfEqualsTo(GContentClass cont) {
				if (this.content == cont) {
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
				if (this.isPlaying) fs |= FontStyle.Underline;
				base.Font = new Font(base.Font, fs);
			}
		}

		private static readonly string[] ColWidthPropertyNames = new string[] {
			"ColWidthId", "ColWidthTitle", "ColWidthSeriesNumber", "ColWidthSubtitle", "ColWidthDuration", "ColWidthDeadline"
		};
		private static readonly Comparison<GContentClass>[] ContentComparisons = new Comparison<GContentClass>[]{
			new Comparison<GContentClass>(delegate(GContentClass x, GContentClass y){
				return x.ContentKey.CompareTo(y.ContentKey);
			}),
			new Comparison<GContentClass>(delegate(GContentClass x, GContentClass y){
				return x.Title.CompareTo(y.Title);
			}),
			new Comparison<GContentClass>(delegate(GContentClass x, GContentClass y){
				return x.SeriesNumber.CompareTo(y.SeriesNumber);
			}),
			new Comparison<GContentClass>(delegate(GContentClass x, GContentClass y){
				return x.Subtitle.CompareTo(y.Subtitle);
			}),
			new Comparison<GContentClass>(delegate(GContentClass x, GContentClass y){
				if(x.DurationValue.HasValue){
					if(y.DurationValue.HasValue){
						return x.DurationValue.Value.CompareTo(y.DurationValue.Value);
					}else{
						return 1;
					}
				}else{
					if(y.DurationValue.HasValue){
						return -1;
					}else{
						return 0;
					}
				}
			}),
			new Comparison<GContentClass>(delegate(GContentClass x, GContentClass y){
				return x.DeadlineValue.CompareTo(y.DeadlineValue);
			}),
		};
		
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler StatusMessagesChanged;
		public event EventHandler LastSelectedContentChanged;

		private Playlist attachedPlaylist;

		private GContentClass lastSelectedContent;
		private string statusMessageName;
		private string statusMessageCount;
		private string statusMessageTime;

		private StackableComparisonsComparer<GContentClass> comparer = new StackableComparisonsComparer<GContentClass>();
		
		public PlaylistsViewPControl() {
			InitializeComponent();

			this.tsmiCmsPlay.Font = new Font(this.tsmiCmsPlay.Font, FontStyle.Bold);
		}
		private void PlaylistsViewPControl_Load(object sender, EventArgs e) {
			if(base.DesignMode) return;
			this.UpdateStatusMessages();
			this.ChangeEnabilityOfMenuItems();

			List<ToolStripItem> sortMenuItems = new List<ToolStripItem>(this.lvContents.Columns.Count);
			foreach (ColumnHeader ch in this.lvContents.Columns) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(ch.Text);
				tsmi.Tag = ch.Index;
				tsmi.Click += delegate(object sender2, EventArgs e2) {
					this.PushComparison((int)(sender2 as ToolStripMenuItem).Tag);
				};
				sortMenuItems.Add(tsmi);
			}
			this.tsmiSort.DropDownItems.AddRange(sortMenuItems.ToArray());
			
			if (null == Program.PlaylistsManager) return;
			Program.PlaylistsManager.CurrentContentAndPlaylistChanged += new EventHandler(PlaylistsManager_CurrentContentAndPlaylistChanged);
			this.Disposed += delegate {
				this.DetachFromPlaylist();
				Program.PlaylistsManager.CurrentContentAndPlaylistChanged -= new EventHandler(PlaylistsManager_CurrentContentAndPlaylistChanged);
			};
		}


		private void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (null != handler) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		private void OnStatusMessageChanged(string name, string count, string time) {
			this.statusMessageName = name;
			this.statusMessageCount = count;
			this.statusMessageTime = time;
			EventHandler handler = this.StatusMessagesChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnLastSelectedContentChanged() {
			EventHandler handler = this.LastSelectedContentChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}

		private void ScrollToTop() {
			if (0 < this.lvContents.Items.Count) {
				this.lvContents.TopItem = this.lvContents.Items[0];
			}
		}
		private void ScrollToBottom() {
			int count = this.lvContents.Items.Count;
			if (0 < count) {
				this.lvContents.TopItem = this.lvContents.Items[count - 1];
			}
		}
		
		public void AttachToPlaylist(Playlist playlist) {
			if (null != this.attachedPlaylist) {
				this.DetachFromPlaylist();
			}
			this.attachedPlaylist = playlist;
			
			this.attachedPlaylist.PlaylistChanged += new EventHandler(attachedPlaylist_PlaylistChanged);
			this.attachedPlaylist.PlaylistDestroyed += new EventHandler(attachedPlaylist_PlaylistDestroyed);

			this.lvContents.BeginUpdate();
			this.UpdateListItems();
			this.UpdatePlayingMark();
			this.UpdateStatusMessages();
			this.ScrollToTop();
			this.lvContents.EndUpdate();
			this.ChangeEnabilityOfMenuItems();
		}
		private void DetachFromPlaylist() {
			this.comparer.ClearComparisons();
			if (null != this.attachedPlaylist) {
				this.attachedPlaylist.PlaylistChanged -= new EventHandler(attachedPlaylist_PlaylistChanged);
				this.attachedPlaylist.PlaylistDestroyed -= new EventHandler(attachedPlaylist_PlaylistDestroyed);
				this.attachedPlaylist = null;
			}
		}

		private void attachedPlaylist_PlaylistChanged(object sender, EventArgs e) {
			this.lvContents.BeginUpdate();
			this.UpdateListItems();
			this.UpdatePlayingMark();
			this.lvContents.EndUpdate();
			this.UpdateStatusMessages();
			this.ChangeEnabilityOfMenuItems();
		}
		private void attachedPlaylist_PlaylistDestroyed(object sender, EventArgs e) {
			this.DetachFromPlaylist();
			this.lvContents.BeginUpdate();
			this.ClearListItems();
			this.lvContents.EndUpdate();
			this.UpdateStatusMessages();
			this.ChangeEnabilityOfMenuItems();
		}
		private void PlaylistsManager_CurrentContentAndPlaylistChanged(object sender, EventArgs e) {
			this.UpdatePlayingMark();
		}

		private void ClearListItems() {
			this.lvContents.Items.Clear();
		}
		private void UpdateListItems() {
			int oldCount = this.lvContents.Items.Count;
			int newCount = this.attachedPlaylist.ContentCount;
			int minCount = (oldCount < newCount) ? oldCount : newCount;

			this.lvContents.BeginUpdate();
			//従来のアイテムの値を書き換える
			for (int i = 0; i < minCount; i++) {
				GContentClass cont = this.attachedPlaylist[i];
				ContentListViewItem clvi = this.lvContents.Items[i] as ContentListViewItem;
				clvi.Content = cont;
			}
			//増減をチェックしてから差分を埋める
			if (minCount == newCount) { //減ったか同じの場合
				for (int i = oldCount - 1; i >= newCount; i--) {
					this.lvContents.Items.RemoveAt(i);
				}
			} else if (minCount == oldCount) {//増えた場合
				for (int i = minCount; i < newCount; i++) {
					GContentClass pl = this.attachedPlaylist[i];
					ContentListViewItem clvi = new ContentListViewItem(pl);
					this.lvContents.Items.Add(clvi);
				}
				//個数が増えたのならばおそらく末尾への追加だろう
				this.ScrollToBottom();
			}
			
			this.lvContents.EndUpdate();
		}
		private void UpdatePlayingMark() {
			GContentClass currentCont = Program.PlaylistsManager.CurrentContent;
			foreach (ContentListViewItem clvi in this.lvContents.Items) {
				clvi.MarkPlayingIfEqualsTo(currentCont);
			}
		}
		private void UpdateStatusMessages() {
			if (null == this.attachedPlaylist) {
				this.OnStatusMessageChanged(string.Empty, string.Empty, string.Empty);
				return;
			}
			GContentClass[] selectedConts = this.GetSelectedContents();
			TimeSpan time = TimeSpan.Zero;
			foreach (GContentClass cont in selectedConts) {
				if (cont.DurationValue.HasValue) {
					time += cont.DurationValue.Value;
				}
			}
			this.OnStatusMessageChanged(
				this.attachedPlaylist.Name,
				string.Format("{0}/{1}", selectedConts.Length, this.attachedPlaylist.ContentCount),
				string.Format("{0}/{1}", time ,this.attachedPlaylist.SubtotalTime));
		}
		private void ChangeEnabilityOfMenuItems() {
			bool isSelected = (this.lvContents.SelectedIndices.Count > 0);
			bool hasPlaylist = (null != this.attachedPlaylist);

			this.tsmiSort.Enabled = hasPlaylist;
			this.tsmiRenamePlaylist.Enabled = hasPlaylist;
			this.tsmiPlay.Enabled = isSelected;
			this.tsmiRearrange.Enabled = isSelected;
			this.tsmiRearrangeToTop.Enabled = isSelected;
			this.tsmiRearrangeUp.Enabled = isSelected;
			this.tsmiRearrangeDown.Enabled = isSelected;
			this.tsmiRearrangeToBottom.Enabled = isSelected;
			this.tspmiCopyToAnother.Enabled = isSelected;
			this.tspmiMoveToAnother.Enabled = isSelected;
			this.tsmiDelete.Enabled = isSelected;
			this.tsmiCopyName.Enabled = isSelected;
			this.tsmiCopyUri.Enabled = isSelected;
			this.tsmiCopyNameAndUri.Enabled = isSelected;
			this.tscpmiCopyOtherProperties.Enabled = isSelected;
			this.tsecmiCommand.Enabled = isSelected;

			this.tsmiCmsPlay.Enabled = isSelected;
			this.tsmiCmsRearrange.Enabled = isSelected;
			this.tsmiCmsRearrangeToTop.Enabled = isSelected;
			this.tsmiCmsRearrangeUp.Enabled = isSelected;
			this.tsmiCmsRearrangeDown.Enabled = isSelected;
			this.tsmiCmsRearrangeToBottom.Enabled = isSelected;
			this.tspmiCmsCopyToAnother.Enabled = isSelected;
			this.tspmiCmsMoveToAnother.Enabled = isSelected;
			this.tsmiCmsDelete.Enabled = isSelected;
			this.tsmiCmsCopyName.Enabled = isSelected;
			this.tsmiCmsCopyUri.Enabled = isSelected;
			this.tsmiCmsCopyNameAndUri.Enabled = isSelected;
			this.tscpmiCmsCopyOtherProperties.Enabled = isSelected;
			this.tsecmiCmsCommand.Enabled = isSelected;
		}

		private GContentClass[] GetSelectedContents() {
			List<GContentClass> conts = new List<GContentClass>();
			foreach (ContentListViewItem clvi in this.lvContents.SelectedItems) {
				conts.Add(clvi.Content);
			}
			return conts.ToArray();
		}
		private void SetSelectedContents(IEnumerable<GContentClass> conts) {
			List<GContentClass> list = new List<GContentClass>(conts);
			this.lvContents.BeginUpdate();
			foreach (ContentListViewItem clvi in this.lvContents.Items) {
				clvi.Selected = list.Contains(clvi.Content);
			}
			this.lvContents.EndUpdate();
		}

		private void CopyToAnotherPlaylist(Playlist destination) {
			GContentClass[] conts = this.GetSelectedContents();
			destination.BeginUpdate();
			foreach (GContentClass cont in conts) {
				destination.AddContent(cont);
			}
			destination.EndUpdate();
		}
		private void MoveToAnotherPlaylist(Playlist destination) {
			//if (destination == this.attachedPlaylist) return;
			this.attachedPlaylist.BeginUpdate();
			this.CopyToAnotherPlaylist(destination);
			GContentClass[] conts = this.GetSelectedContents();
			foreach (GContentClass cont in conts) {
				this.attachedPlaylist.RemoveContent(cont);
			}
			this.attachedPlaylist.EndUpdate();
		}

		#region リストビューのイベントハンドラ
		private void lvContents_SelectedIndexChanged(object sender, EventArgs e) {
			this.timerSelectionDelay.Start();
		}
		private void lvContents_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if (e.IsSelected) {
				ContentListViewItem clvi = e.Item as ContentListViewItem;
				if (null != clvi) {
					this.LastSelectedContent = clvi.Content;
				}
			}
		}
		private void lvContents_MouseDoubleClick(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.Left:
					this.tsmiCmsPlay.PerformClick();
					break;
			}
		}
		private void lvContents_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					this.tsmiCmsPlay.PerformClick();
					break;
			}
		}
		private void lvContents_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.PushComparison(e.Column);
		}
		private void lvContents_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e) {
			string propName = PlaylistsViewPControl.ColWidthPropertyNames[e.ColumnIndex];
			this.OnPropertyChanged(propName);
		}
		#endregion

		public string StatusMessagePlaylistName {
			get { return this.statusMessageName; }
		}
		public string StatusMessageContentCount {
			get { return this.statusMessageCount; }
		}
		public string StatusMessageDuration {
			get { return this.statusMessageTime; }
		}

		public ToolStripDropDown GetToolStripDropDown() {
			return this.tsmiPVP.DropDown;
		}

		#region コンテキストメニュー
		private void tsmiCmsPlay_Click(object sender, EventArgs e) {
			this.tsmiPlay.PerformClick();
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
		private void tspmiCmsCopyToAnother_PlaylistSelected(object sender, EventArgs e) {
			this.CopyToAnotherPlaylist(this.tspmiCmsCopyToAnother.LastSelectedPlaylist);
		}
		private void tspmiCmsMoveToAnother_PlaylistSelected(object sender, EventArgs e) {
			this.MoveToAnotherPlaylist(this.tspmiCmsMoveToAnother.LastSelectedPlaylist);
			this.lvContents.SelectedIndices.Clear();
		}
		private void tsmiCmsDelete_Click(object sender, EventArgs e) {
			this.tsmiDelete.PerformClick();
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
		private void tsecmiCmsCommand_ExternalCommandSelected(object sender, EventArgs e) {
			this.tsecmiCommand_ExternalCommandSelected(sender, e);
		}
		#endregion

		#region メインメニュー
		private void tsmiRenamePlaylist_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "プレイリスト名の変更";
			this.inputBoxDialog1.Message = "新しいプレイリスト名を入力してください．";
			this.inputBoxDialog1.Input = this.attachedPlaylist.Name;
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					this.attachedPlaylist.Name = this.inputBoxDialog1.Input;
					break;
			}
		}
		private void tsmiPlay_Click(object sender, EventArgs e) {
			Program.PlayContent(this.GetSelectedContents()[0], this.attachedPlaylist);
		}
		private void tsmiRearrangeToTop_Click(object sender, EventArgs e) {
			this.lvContents.BeginUpdate();
			this.attachedPlaylist.BeginUpdate();
			GContentClass[] conts = this.GetSelectedContents();
			for (int i = conts.Length - 1; i >= 0; i--) {
				this.attachedPlaylist.MoveToTop(conts[i]);
			}
			this.attachedPlaylist.EndUpdate();
			this.SetSelectedContents(conts);
			this.lvContents.EndUpdate();
			this.ScrollToTop();
		}
		private void tsmiRearrangeUp_Click(object sender, EventArgs e) {
			this.lvContents.BeginUpdate();
			this.attachedPlaylist.BeginUpdate();
			GContentClass[] conts = this.GetSelectedContents();
			foreach (GContentClass cont in conts) {
				this.attachedPlaylist.MoveUp(cont);
			}
			this.attachedPlaylist.EndUpdate();
			this.SetSelectedContents(conts);
			this.lvContents.EndUpdate();
		}
		private void tsmiRearrangeDown_Click(object sender, EventArgs e) {
			this.lvContents.BeginUpdate();
			this.attachedPlaylist.BeginUpdate();
			GContentClass[] conts = this.GetSelectedContents();
			for (int i = conts.Length - 1; i >= 0; i--) {
				this.attachedPlaylist.MoveDown(conts[i]);
			}
			this.attachedPlaylist.EndUpdate();
			this.SetSelectedContents(conts);
			this.lvContents.EndUpdate();
		}
		private void tsmiRearrangeToBottom_Click(object sender, EventArgs e) {
			this.lvContents.BeginUpdate();
			this.attachedPlaylist.BeginUpdate();
			GContentClass[] conts = this.GetSelectedContents();
			foreach (GContentClass cont in conts) {
				this.attachedPlaylist.MoveToBottom(cont);
			}
			this.attachedPlaylist.EndUpdate();
			this.SetSelectedContents(conts);
			this.lvContents.EndUpdate();
			this.ScrollToBottom();
		}
		private void tspmiCopyToAnother_PlaylistSelected(object sender, EventArgs e) {
			this.CopyToAnotherPlaylist(this.tspmiCopyToAnother.LastSelectedPlaylist);
		}
		private void tspmiMoveToAnother_PlaylistSelected(object sender, EventArgs e) {
			this.MoveToAnotherPlaylist(this.tspmiMoveToAnother.LastSelectedPlaylist);
			this.lvContents.SelectedIndices.Clear();
		}
		private void tsmiDelete_Click(object sender, EventArgs e) {
			this.attachedPlaylist.BeginUpdate();
			foreach (GContentClass cont in this.GetSelectedContents()) {
				this.attachedPlaylist.RemoveContent(cont);
			}
			this.attachedPlaylist.EndUpdate();
			this.lvContents.SelectedIndices.Clear();
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
		private void tsecmiCommand_ExternalCommandSelected(object sender, EventArgs e) {
			(sender as ToolStripExternalCommandMenuItem).LastSelectedExternalCommand.Execute(this.GetSelectedContents());
		}
		#endregion
		
		private void PushComparison(int idx) {
			if (null == this.attachedPlaylist) return;
			this.comparer.PushComparison(PlaylistsViewPControl.ContentComparisons[idx]);
			this.attachedPlaylist.Sort(this.comparer);
		}

		public void BindToOptions(PlaylistsViewPControlOptions options) {
			options.NeutralizeUnspecificValues(this);
			BindingContractUtility.BindAllProperties<PlaylistsViewPControl, IPlaylistsViewPControlBindingContract>(this, options);
		}

		#region IPlaylistsViewPControlBindingContract Members
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthId {
			get { return this.chId.Width;}
			set {
				this.chId.Width = value;
				this.OnPropertyChanged(PlaylistsViewPControl.ColWidthPropertyNames[0]);
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthTitle {
			get { return this.chTitle.Width; }
			set {
				this.chTitle.Width = value;
				this.OnPropertyChanged(PlaylistsViewPControl.ColWidthPropertyNames[1]);
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthSeriesNumber {
			get { return this.chSeriesNumber.Width; }
			set {
				this.chSeriesNumber.Width = value;
				this.OnPropertyChanged(PlaylistsViewPControl.ColWidthPropertyNames[2]);
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthSubtitle {
			get { return this.chSubtitle.Width; }
			set {
				this.chSubtitle.Width = value;
				this.OnPropertyChanged(PlaylistsViewPControl.ColWidthPropertyNames[3]);
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthDuration {
			get { return this.chDuration.Width; }
			set {
				this.chDuration.Width = value;
				this.OnPropertyChanged(PlaylistsViewPControl.ColWidthPropertyNames[4]);
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthDeadline {
			get { return this.chDeadline.Width; }
			set {
				this.chDeadline.Width = value;
				this.OnPropertyChanged(PlaylistsViewPControl.ColWidthPropertyNames[5]);
			}
		}
		#endregion

		public GContentClass LastSelectedContent {
			get { return this.lastSelectedContent; }
			private set {
				//if (this.lastSelectedContent != value) {
					this.lastSelectedContent = value;
					this.OnLastSelectedContentChanged();
				//}
			}
		}

		private void timerSelectionDelay_Tick(object sender, EventArgs e) {
			this.timerSelectionDelay.Stop();
			this.UpdateStatusMessages();
			this.ChangeEnabilityOfMenuItems();
		}
	}

	interface IPlaylistsViewPControlBindingContract : IBindingContract {
		int ColWidthId{get;set;}
		int ColWidthTitle{get;set;}
		int ColWidthSeriesNumber{get;set;}
		int ColWidthSubtitle{get;set;}
		int ColWidthDuration{get;set;}
		int ColWidthDeadline{get;set;}
	}
	public sealed class PlaylistsViewPControlOptions : IPlaylistsViewPControlBindingContract {
		public PlaylistsViewPControlOptions() {
		}

		#region IPlaylistsViewPControlBindingContract Members
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
		#endregion
		internal void NeutralizeUnspecificValues(PlaylistsViewPControl pControl) {
			if (this.ColWidthId < 0) this.ColWidthId = pControl.ColWidthId;
			if (this.ColWidthTitle < 0) this.ColWidthTitle = pControl.ColWidthTitle;
			if (this.ColWidthSeriesNumber < 0) this.ColWidthSeriesNumber = pControl.ColWidthSeriesNumber;
			if (this.ColWidthSubtitle < 0) this.ColWidthSubtitle = pControl.ColWidthSubtitle;
			if (this.ColWidthDuration < 0) this.ColWidthDuration = pControl.ColWidthDuration;
			if (this.ColWidthDeadline < 0) this.ColWidthDeadline = pControl.ColWidthDeadline;
		}

	}
}
