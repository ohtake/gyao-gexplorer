using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yusen.GCrawler;
using System.IO;

namespace Yusen.GExplorer {
	sealed partial class PlayListView : UserControl, IHasSettings<PlayListViewSettings> {
		public event EventHandler<ContentSelectionChangedEventArgs> ContentSelectionChanged;

		private bool dragging = false;
		private string[] dropIds = null;
		private ContentAdapter[] dropConts = null;

		public PlayListView() {
			InitializeComponent();
		}
		private void PlayListView_Load(object sender, EventArgs e) {
			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
			this.tsmiPlay.Font = new Font(this.tsmiPlay.Font, FontStyle.Bold);
			this.tsddbSettings.DropDown.Closing += Utility.ToolStripDropDown_CancelClosingOnClick;
			
			PlayList.Instance.PlayListChanged += new EventHandler(PlayList_PlayListChanged);
			PlayList.Instance.CurrentContentChanged += new EventHandler(PlayList_CurrentContentChanged);
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);

			this.Disposed += delegate {
				PlayList.Instance.PlayListChanged -= new EventHandler(PlayList_PlayListChanged);
				PlayList.Instance.CurrentContentChanged -= new EventHandler(PlayList_CurrentContentChanged);
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			};

			this.listView1.BeginUpdate();
			this.UpdateItems();
			this.UpdateBoldness();
			this.listView1.EndUpdate();

			this.UpdateStatusBarText();
			this.UpdateUserCommandsMenu();

			//リスト先頭を表示
			this.ScrollToTop();
		}

		public void FillSettings(PlayListViewSettings settings) {
			settings.MultiSelectEnabled = this.MultiSelectEnabled;
			settings.ColWidthId = this.chId.Width;
			settings.ColWidthName = this.chName.Width;
			settings.ColWidthDuration = this.chDuration.Width;
			settings.ColWidthDeadline = this.chDeadline.Width;
			settings.ColWidthComment = this.chComment.Width;
		}
		public void ApplySettings(PlayListViewSettings settings) {
			this.MultiSelectEnabled = settings.MultiSelectEnabled ?? this.MultiSelectEnabled;
			this.chId.Width = settings.ColWidthId ?? this.chId.Width;
			this.chName.Width = settings.ColWidthName ?? this.chName.Width;
			this.chDuration.Width = settings.ColWidthDuration ?? this.chDuration.Width;
			this.chDeadline.Width = settings.ColWidthDeadline ?? this.chDeadline.Width;
			this.chComment.Width = settings.ColWidthComment ?? this.chComment.Width;
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
		public ContentAdapter FocusedContent {
			get {
				ListViewItem lvi = this.listView1.FocusedItem;
				if (null != lvi) {
					return lvi.Tag as ContentAdapter;
				}
				return null;
			}
			set {
				if (null != value) {
					foreach (ListViewItem lvi in this.listView1.Items) {
						if (value.Equals(lvi.Tag)) {
							this.listView1.FocusedItem = lvi;
							return;
						}
					}
				}
				this.listView1.FocusedItem = null;
			}
		}
		public bool MultiSelectEnabled {
			get { return this.tsmiMultiSelectEnabled.Checked; }
			set {
				this.tsmiMultiSelectEnabled.Checked = value;
				this.listView1.MultiSelect = value;
			}
		}

		private bool CheckIfWholeUpdateItemsRequired() {
			int len = PlayList.Instance.Count;
			if (this.listView1.Items.Count != len) {
				return true;
			} else {
				return false;
			}
		}

		private void UpdateItems() {
			int oldCount = this.listView1.Items.Count;
			int newCount = PlayList.Instance.Count;
			int minCount = (oldCount < newCount) ? oldCount : newCount;

			//従来のアイテムの値を書き換える
			for(int i=0; i<minCount; i++) {
				ContentAdapter cont = PlayList.Instance[i];
				ListViewItem lvi = this.listView1.Items[i];
				lvi.SubItems[0].Text = cont.ContentId;
				lvi.SubItems[1].Text = cont.DisplayName;
				lvi.SubItems[2].Text = cont.GTimeSpan.ToString();
				lvi.SubItems[3].Text = cont.Deadline;
				lvi.SubItems[4].Text = cont.Comment;
				lvi.Tag = cont;
			}
			//増減をチェックしてから差分を埋める
			if(minCount == newCount) { //減ったか同じの場合
				for(int i=oldCount-1; i>=newCount; i--) {
					this.listView1.Items.RemoveAt(i);
				}
			} else if(minCount == oldCount){//増えた場合
				for(int i=minCount; i<newCount; i++) {
					ContentAdapter cont = PlayList.Instance[i];
					ListViewItem lvi = new ListViewItem(new string[] { cont.ContentId, cont.DisplayName, cont.GTimeSpan.ToString(), cont.Deadline, cont.Comment });
					lvi.Tag = cont;
					this.listView1.Items.Add(lvi);
				}
				//個数が増えたのならばおそらく末尾への追加だろう
				this.ScrollToBottom();
			}
		}
		private void UpdateBoldness() {
			foreach (ListViewItem lvi in this.listView1.Items) {
				FontStyle oldStyle = lvi.Font.Style;
				FontStyle newStyle = PlayList.Instance.IsCurrentContent(lvi.Tag as ContentAdapter) ? FontStyle.Bold : FontStyle.Regular;
				if(oldStyle != newStyle){
					lvi.Font = new Font(lvi.Font, newStyle);
				}
			}
		}
		private void UpdateStatusBarText() {
			int totalNum = PlayList.Instance.Count;
			TimeSpan totalTimeSpan;
			bool hasExactTotalTimeSpan;
			ContentAdapter.TotalTimeOf(PlayList.Instance, out totalTimeSpan, out hasExactTotalTimeSpan);

			int selectedNum = this.listView1.SelectedIndices.Count;
			TimeSpan selectedTimeSpan;
			bool hasExactSelectedTimeSpan;
			ContentAdapter.TotalTimeOf(this.SelectedContents, out selectedTimeSpan, out hasExactSelectedTimeSpan);

			string num = "数: " + selectedNum.ToString() + " / " + totalNum.ToString();
			string time = "時間: "
				+ selectedTimeSpan.ToString() + (hasExactSelectedTimeSpan ? "" : "+?")
				+ " / "
				+ totalTimeSpan.ToString() + (hasExactTotalTimeSpan ? "" : "+?");
			this.tslMessage.Text = num + "   " + time;
		}
		private void ScrollToTop() {
			if (0 < this.listView1.Items.Count) {
				this.listView1.TopItem = this.listView1.Items[0];
			}
		}
		private void ScrollToBottom() {
			if (0 < this.listView1.Items.Count) {
				this.listView1.TopItem = this.listView1.Items[this.listView1.Items.Count -1];
			}
		}

		private void UpdateUserCommandsMenu() {
			this.tsmiCommands.DropDownItems.Clear();
			foreach (UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(uc.Title);
				tsmi.Tag = uc;
				tsmi.Click += delegate(object sender, EventArgs e) {
					UserCommand command = (sender as ToolStripMenuItem).Tag as UserCommand;
					command.Execute(this.SelectedContents);
				};
				this.tsmiCommands.DropDownItems.Add(tsmi);
			}
			this.tsmiCommands.Enabled = this.tsmiCommands.HasDropDownItems;
		}

		private void PlayList_PlayListChanged(object sender, EventArgs e) {
			this.listView1.BeginUpdate();
			this.UpdateItems();
			this.UpdateBoldness();
			this.listView1.EndUpdate();
			this.UpdateStatusBarText();
		}
		private void PlayList_CurrentContentChanged(object sender, EventArgs e) {
			this.listView1.BeginUpdate();
			this.UpdateBoldness();
			this.listView1.EndUpdate();
		}
		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.UpdateUserCommandsMenu();
		}

		private void listView1_DoubleClick(object sender, EventArgs e) {
			this.tsmiPlay.PerformClick();
		}
		private void listView1_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					this.tsmiPlay.PerformClick();
					break;
			}
		}
		private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			this.timerSumSelected.Start();
			
			if (null != this.ContentSelectionChanged) {
				this.ContentSelectionChanged(this, new ContentSelectionChangedEventArgs(e.Item.Tag as ContentAdapter, e.IsSelected));
			}
		}
		private void listView1_ColumnClick(object sender, ColumnClickEventArgs e) {
			ListViewItemComparer comparer = new ListViewItemComparer(e.Column);
			List<ListViewItem> lvis = new List<ListViewItem>();
			foreach (ListViewItem lvi in this.listView1.Items) {
				lvis.Add(lvi);
			}

			if (Utility.IsSorted(lvis, comparer)) {
				lvis.Reverse();
			} else {
				lvis.Sort(comparer);
			}
			PlayList.Instance.SetAll(
				lvis.ConvertAll<ContentAdapter>(delegate(ListViewItem lvi){
					return lvi.Tag as ContentAdapter;
				}));
		}
		private void listView1_ItemDrag(object sender, ItemDragEventArgs e) {
			this.dragging = true;
			DataObject dobj = new DataObject();
			dobj.SetText(ContentAdapter.GetNamesAndUris(this.SelectedContents));
			dobj.SetData(typeof(ContentAdapter[]), this.SelectedContents);
			this.listView1.DoDragDrop(dobj, DragDropEffects.Copy);
			this.dragging = false;
		}
		private void listView1_DragEnter(object sender, DragEventArgs e) {
			if (this.dragging) {
				return;
			}

			ContentAdapter[] conts = null;
			string text = null;
			DragDropEffects effect = DragDropEffects.None;
			if((e.AllowedEffect & DragDropEffects.Copy) != DragDropEffects.None){
				//ContentAdapter[] から
				conts = e.Data.GetData(typeof(ContentAdapter[])) as ContentAdapter[];
				//文字列から
				text = e.Data.GetData(typeof(string)) as string;
				effect = DragDropEffects.Copy;
			} else if ((e.AllowedEffect & DragDropEffects.Link) != DragDropEffects.None) {
				//UniformResourceLocator から
				Stream linkStream = e.Data.GetData("UniformResourceLocator") as MemoryStream;
				if (null != linkStream) {
					byte[] linkBytes = new byte[linkStream.Length];
					linkStream.Read(linkBytes, 0, linkBytes.Length);
					text = Encoding.Default.GetString(linkBytes);
					effect = DragDropEffects.Link;
				}
			}

			this.dropConts = null;
			this.dropIds = null;

			if (null != conts) {
				this.dropConts = conts;
				e.Effect = effect;
			}else if (! string.IsNullOrEmpty(text)) {
				//ID取り出し
				string[] ids = GContent.ExtractContentIds(text);
				this.dropIds = ids;
				e.Effect = effect;
			}
		}
		private void listView1_DragDrop(object sender, DragEventArgs e) {
			if (null != this.dropConts) {
				PlayList.Instance.BeginUpdate();
				foreach (ContentAdapter cont in this.dropConts) {
					PlayList.Instance.AddIfNotExists(cont);
				}
				PlayList.Instance.EndUpdate();
			} else if (null != this.dropIds) {
				try {
					PlayList.Instance.BeginUpdate();
					foreach (string id in this.dropIds) {
					retry:
						GContent cont = null;
						try {
							cont = GContent.DoDownload(id);
						} catch (Exception ex) {
							switch (MessageBox.Show(ex.Message, "ドロップによる追加", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3)) {
								case DialogResult.Abort:
									return;
								case DialogResult.Retry:
									goto retry;
								case DialogResult.Ignore:
									continue;
							}
						}
						ContentAdapter ca = new ContentAdapter(cont);
						PlayList.Instance.AddIfNotExists(ca);
					}
				} finally {
					PlayList.Instance.EndUpdate();
				}
			}
		}
		
		#region メニューの項目
		private void tsmiAddById_Click(object sender, EventArgs e) {
			string title = "コンテンツIDを指定してプレイリストに追加";
			this.inputBoxDialog1.Title = title;
			this.inputBoxDialog1.Message = "追加するコンテンツのIDを入力してください．";
			this.inputBoxDialog1.Input = "cnt0000000";
			if (DialogResult.OK == this.inputBoxDialog1.ShowDialog()) {
				GContent cont = GContent.DoDownload(this.inputBoxDialog1.Input);
				ContentAdapter ca = new ContentAdapter(cont);
				if (PlayList.Instance.Contains(ca)) {
					MessageBox.Show("指定したIDはすでにプレイリストに存在します．",
						title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				} else {
					PlayList.Instance.AddIfNotExists(ca);
				}
			}
		}
		private void tsmiSetDeadlines_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in PlayList.Instance) {
				cont.TryResetDeadline();
			}
			this.tsmiRefleshView.PerformClick();
		}
		private void tsmiRefleshView_Click(object sender, EventArgs e) {
			this.listView1.BeginUpdate();
			this.UpdateItems();
			this.UpdateBoldness();
			this.listView1.EndUpdate();
		}
		private void tsmiExport_Click(object sender, EventArgs e) {
			if (DialogResult.OK == this.sfdXml.ShowDialog()) {
				PlayList.Instance.SerializeItems(this.sfdXml.FileName);
			}
		}
		private void tsmiImportAppend_Click(object sender, EventArgs e) {
			if (DialogResult.OK == this.ofdXml.ShowDialog()) {
				List<ContentAdapter> oldList = new List<ContentAdapter>(PlayList.Instance.Count);
				foreach (ContentAdapter cont in PlayList.Instance) {
					oldList.Add(cont);
				}
				PlayList.Instance.DeserializeItems(this.ofdXml.FileName);
				List<ContentAdapter> newList = new List<ContentAdapter>(PlayList.Instance.Count);
				foreach (ContentAdapter cont in PlayList.Instance) {
					newList.Add(cont);
				}
				PlayList.Instance.BeginUpdate();
				PlayList.Instance.SetAll(oldList);
				foreach (ContentAdapter cont in newList) {
					PlayList.Instance.AddIfNotExists(cont);
				}
				PlayList.Instance.EndUpdate();
			}
		}
		private void tsmiImportOverwrite_Click(object sender, EventArgs e) {
			if (DialogResult.OK == this.ofdXml.ShowDialog()) {
				PlayList.Instance.DeserializeItems(this.ofdXml.FileName);
			}
		}
		private void tsmiSerializePlayListNow_Click(object sender, EventArgs e) {
			PlayList.Instance.SerializeItems();
		}
		private void tsmiRemoveUnreachables_Click(object sender, EventArgs e) {
			PlayList.Instance.BeginUpdate();
			List<string> reachable = Cache.Instance.GetSortedReachableContentIds();
			foreach (ListViewItem lvi in this.listView1.Items) {
				ContentAdapter cont = lvi.Tag as ContentAdapter;
				if (reachable.BinarySearch(cont.ContentId) < 0) {
					PlayList.Instance.Remove(cont);
				}
			}
			PlayList.Instance.EndUpdate();
		}
		private void tsmiClearPlayList_Click(object sender, EventArgs e) {
			switch (MessageBox.Show("プレイリスト内の全項目を削除します．よろしいですか？", "プレイリスト内の全項目を削除", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)) {
				case DialogResult.Yes:
					PlayList.Instance.Clear();
					break;
			}
			
		}
		private void tsmiMultiSelectEnabled_Click(object sender, EventArgs e) {
			this.listView1.MultiSelect = this.tsmiMultiSelectEnabled.Checked;
		}
		#endregion
		
		#region コンテキストメニューの項目
		private void tsmiPlay_Click(object sender, EventArgs e) {
			ContentAdapter[] conts = this.SelectedContents;
			if (conts.Length > 0) {
				PlayerForm.Play(conts[0]);
			}
		}
		private void tsmiSetComment_Click(object sender, EventArgs e) {
			ContentAdapter[] conts = this.SelectedContents;
			if (conts.Length > 0) {
				this.inputBoxDialog1.Title = "コメントを入力．";
				this.inputBoxDialog1.Message = "コメントを入力してください．";
				this.inputBoxDialog1.Input = conts[0].Comment;
				switch (this.inputBoxDialog1.ShowDialog()) {
					case DialogResult.OK:
						string comment = this.inputBoxDialog1.Input;
						foreach (ContentAdapter cont in conts) {
							cont.Comment = comment;
						}
						this.tsmiRefleshView.PerformClick();
						this.SelectedContents = conts;
						break;
				}
			}
		}
		private void tsmiMoveToTop_Click(object sender, EventArgs e) {
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			conts.Reverse();
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.MoveToTop);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = conts.ToArray();
			this.ScrollToTop();
		}
		private void tsmiMoveUp_Click(object sender, EventArgs e) {
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.MoveUp);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = conts.ToArray();
		}
		private void tsmiMoveDown_Click(object sender, EventArgs e) {
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			conts.Reverse();
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.MoveDown);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = conts.ToArray();
		}
		private void tsmiMoveToBottom_Click(object sender, EventArgs e) {
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.MoveToBottom);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = conts.ToArray();
			this.ScrollToBottom();
		}
		private void tsmiRemoveItem_Click(object sender, EventArgs e) {
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.Remove);
			PlayList.Instance.EndUpdate();
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
		private void tsmiBrowseDetail_Click(object sender, EventArgs e) {
			foreach (ContentAdapter cont in this.SelectedContents) {
				Utility.Browse(cont.DetailPageUri);
			}
		}
		private void tsmiCopyName_Click(object sender, EventArgs e) {
			ContentAdapter.CopyNames(this.SelectedContents);
		}
		private void tsmiCopyUri_Click(object sender, EventArgs e) {
			ContentAdapter.CopyUris(this.SelectedContents);
		}
		private void tsmiCopyNameAndUri_Click(object sender, EventArgs e) {
			ContentAdapter.CopyNamesAndUris(this.SelectedContents);
		}
		#endregion

		private void cmsPlayListItem_Opening(object sender, CancelEventArgs e) {
			if (0 == this.SelectedContents.Length) {
				e.Cancel = true;
			}
		}

		private void timerSumSelected_Tick(object sender, EventArgs e) {
			this.timerSumSelected.Stop();
			this.UpdateStatusBarText();
		}

		internal ToolStripDropDown SettingsDropDown {
			get { return this.tsddbSettings.DropDown; }
		}
		internal bool SettingsVisible {
			get { return this.tsddbSettings.Visible; }
			set { this.tsddbSettings.Visible = value; }
		}
	}
	
	public class PlayListViewSettings {
		private bool? multiSelectEnabled;
		private int? colWidthId;
		private int? colWidthName;
		private int? colWidthDuration;
		private int? colWidthDeadline;
		private int? colWidthComment;

		public bool? MultiSelectEnabled {
			get { return this.multiSelectEnabled; }
			set { this.multiSelectEnabled = value; }
		}
		public int? ColWidthId {
			get { return this.colWidthId; }
			set { this.colWidthId = value; }
		}
		public int? ColWidthName {
			get { return this.colWidthName; }
			set { this.colWidthName = value; }
		}
		public int? ColWidthDuration {
			get { return this.colWidthDuration; }
			set { this.colWidthDuration = value; }
		}
		public int? ColWidthDeadline {
			get { return this.colWidthDeadline; }
			set { this.colWidthDeadline = value; }
		}
		public int? ColWidthComment {
			get { return this.colWidthComment; }
			set { this.colWidthComment = value; }
		}
	}
}
