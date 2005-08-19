using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class PlayListView : UserControl, IHasSettings<PlayListViewSettings> {
		public event EventHandler<SelectedContentsChangedEventArgs> SelectedContentsChanged;
		public event EventHandler<ContentSelectionChangedEventArgs> ContentSelectionChanged;
		
		public PlayListView() {
			InitializeComponent();
			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
			this.tsmiPlay.Font = new Font(this.tsmiPlay.Font, FontStyle.Bold);
			
			PlayList.Instance.PlayListChanged += new EventHandler(PlayList_PlayListChanged);
			PlayList.Instance.CurrentContentChanged += new EventHandler(PlayList_CurrentContentChanged);
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
			
			this.Disposed += delegate {
				PlayList.Instance.PlayListChanged -= new EventHandler(PlayList_PlayListChanged);
				PlayList.Instance.CurrentContentChanged -= new EventHandler(PlayList_CurrentContentChanged);
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			};
			
			this.UpdatePlayListView();
			this.UpdateStatusBarText();
			this.UpdateUserCommandsMenu();
		}

		public void FillSettings(PlayListViewSettings settings) {
			settings.MultiSelectEnabled = this.MultiSelectEnabled;
			settings.ColWidthId = this.chId.Width;
			settings.ColWidthName = this.chName.Width;
			settings.ColWidthDuration = this.chDuration.Width;
		}
		public void ApplySettings(PlayListViewSettings settings) {
			this.MultiSelectEnabled = settings.MultiSelectEnabled ?? this.MultiSelectEnabled;
			this.chId.Width = settings.ColWidthId ?? this.chId.Width;
			this.chName.Width = settings.ColWidthName ?? this.chName.Width;
			this.chDuration.Width = settings.ColWidthDuration ?? this.chDuration.Width;
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
		public bool MultiSelectEnabled {
			get { return this.tsmiMultiSelectEnabled.Checked; }
			set {
				this.tsmiMultiSelectEnabled.Checked = value;
				this.listView1.MultiSelect = value;
			}
		}
		
		private void UpdatePlayListView() {
			this.listView1.BeginUpdate();
			this.listView1.Items.Clear();
			foreach (ContentAdapter cont in PlayList.Instance) {
				ListViewItem lvi = new ListViewItem(new string[] { cont.ContentId, cont.DisplayName, cont.Duration });
				lvi.Tag = cont;
				if (PlayList.Instance.IsCurrentContent(cont)) {
					lvi.Font = new Font(lvi.Font, FontStyle.Bold);
				}
				this.listView1.Items.Add(lvi);
			}
			this.listView1.EndUpdate();
		}
		private void UpdateStatusBarText() {
			if (this.SelectedContents.Length <= 0) {
				this.tslMessage.Text = "全 " + this.listView1.Items.Count.ToString() + " 個";
			} else {
				this.tslMessage.Text = this.SelectedContents.Length.ToString() + " 個を選択中";
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
			this.UpdatePlayListView();
			this.UpdateStatusBarText();
		}
		private void PlayList_CurrentContentChanged(object sender, EventArgs e) {
			this.UpdatePlayListView();
			this.UpdateStatusBarText();
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
		private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
			this.UpdateStatusBarText();
			if (null != this.SelectedContentsChanged) {
				this.SelectedContentsChanged(this, new SelectedContentsChangedEventArgs(this.SelectedContents));
			}
		}
		private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if (null != this.ContentSelectionChanged) {
				this.ContentSelectionChanged(this, new ContentSelectionChangedEventArgs(e.Item.Tag as ContentAdapter, e.IsSelected));
			}
		}

		#region メニューの項目
		private void tsmiExportAsAsx_Click(object sender, EventArgs e) {
			if (DialogResult.OK == this.saveFileDialog1.ShowDialog()) {
				PlayList.Instance.ExportAsAsx(this.saveFileDialog1.FileName);
			}
		}
		private void tsmiSerializePlayListNow_Click(object sender, EventArgs e) {
			PlayList.Instance.SerializeItems();
		}
		private void tsmiSortId_Click(object sender, EventArgs e) {
			PlayList.Instance.Sort(new Comparison<ContentAdapter>(delegate(ContentAdapter a, ContentAdapter b) {
				return a.ContentId.CompareTo(b.ContentId);
			}));
		}
		private void tsmiSortNameDescending_Click(object sender, EventArgs e) {
			PlayList.Instance.Sort(new Comparison<ContentAdapter>(delegate(ContentAdapter a, ContentAdapter b) {
				return a.DisplayName.CompareTo(b.DisplayName);
			}));
		}
		private void tsmiSortNameAscending_Click(object sender, EventArgs e) {
			PlayList.Instance.Sort(new Comparison<ContentAdapter>(delegate(ContentAdapter a, ContentAdapter b) {
				return b.DisplayName.CompareTo(a.DisplayName);
			}));
		}
		private void tsmiClearPlayList_Click(object sender, EventArgs e) {
			PlayList.Instance.Clear();
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
		private void tsmiMoveToTop_Click(object sender, EventArgs e) {
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			conts.Reverse();
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.MoveToTop);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = conts.ToArray();
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
		}
		private void tsmiRemoveItem_Click(object sender, EventArgs e) {
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.Remove);
			PlayList.Instance.EndUpdate();
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
		private void tsmiBrowseWithIe_Click(object sender, EventArgs e) {
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
		private void tsmiCopyUri_Click(object sender, EventArgs e) {
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
		private void tsmiCopyNameAndUri_Click(object sender, EventArgs e) {
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
		#endregion

		private void cmsPlayListItem_Opening(object sender, CancelEventArgs e) {
			if (0 == this.SelectedContents.Length) {
				e.Cancel = true;
			}
		}
	}
	
	public class PlayListViewSettings {
		private bool? multiSelectEnabled;
		private int? colWidthId;
		private int? colWidthName;
		private int? colWidthDuration;

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
	}
}
