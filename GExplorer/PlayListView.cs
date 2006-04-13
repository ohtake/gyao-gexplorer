using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yusen.GCrawler;
using System.IO;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public sealed partial class PlayListView : UserControl, IHasNewSettings<PlayListView.PlayListViewSettings> {
		public class PlayListViewSettings : INewSettings<PlayListViewSettings>{
			private PlayListView owner;
			public PlayListViewSettings() : this(null) {
			}
			internal PlayListViewSettings(PlayListView owner) {
				this.owner = owner;
			}
			
			[XmlIgnore]
			[Browsable(false)]
			private bool HasOwner {
				get { return null != this.owner; }
			}
			
			[Category("カラム幅")]
			[DisplayName("[0] contents_id")]
			[Description("'contents_id'カラムの幅を指定します．")]
			[DefaultValue(null)]
			public int? ColWidthId {
				get {
					if (this.HasOwner) return this.owner.chId.Width;
					else return this.colWidthId;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chId.Width = value.Value;
					else this.colWidthId = value;
				}
			}
			private int? colWidthId;

			[Category("カラム幅")]
			[DisplayName("[1] コンテンツ名")]
			[Description("'コンテンツ名'カラムの幅を指定します．")]
			[DefaultValue(null)]
			public int? ColWidthName {
				get {
					if (this.HasOwner) return this.owner.chName.Width;
					else return this.colWidthName;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chName.Width = value.Value;
					else this.colWidthName = value;
				}
			}
			private int? colWidthName;

			[Category("カラム幅")]
			[DisplayName("[2] 番組時間")]
			[Description("'番組時間'カラムの幅を指定します．")]
			[DefaultValue(null)]
			public int? ColWidthDuration {
				get {
					if (this.HasOwner) return this.owner.chDuration.Width;
					else return this.colWidthDuration;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chDuration.Width = value.Value;
					else this.colWidthDuration = value;
				}
			}
			private int? colWidthDuration;

			[Category("カラム幅")]
			[DisplayName("[3] 配信期限")]
			[Description("'配信期限'カラムの幅を指定します．")]
			[DefaultValue(null)]
			public int? ColWidthDeadline {
				get {
					if (this.HasOwner) return this.owner.chDeadline.Width;
					else return this.colWidthDeadline;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chDeadline.Width = value.Value;
					else this.colWidthDeadline = value;
				}
			}
			private int? colWidthDeadline;

			[Category("カラム幅")]
			[DisplayName("[4] コメント")]
			[Description("'コメント'カラムの幅を指定します．")]
			[DefaultValue(null)]
			public int? ColWidthComment {
				get {
					if (this.HasOwner) return this.owner.chComment.Width;
					else return this.colWidthComment;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.chComment.Width = value.Value;
					else this.colWidthComment = value;
				}
			}
			private int? colWidthComment;

			#region INewSettings<PlayListViewSettings> Members
			public void ApplySettings(PlayListViewSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}


		public event EventHandler<ContentSelectionChangedEventArgs> ContentSelectionChanged;


		private bool dragging = false;
		private string[] dropIds = null;
		private ContentAdapter[] dropConts = null;

		private PlayListViewSettings settings;

		public PlayListView() {
			InitializeComponent();
			
			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
			this.tsmiPlay.Font = new Font(this.tsmiPlay.Font, FontStyle.Bold);
			
			this.settings = new PlayListViewSettings(this);
		}
		private void PlayListView_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			PlayList.Instance.PlayListChanged += new EventHandler(PlayList_PlayListChanged);
			PlayList.Instance.CurrentContentChanged += new EventHandler(PlayList_CurrentContentChanged);
			PlayList.Instance.AdditionRejected += new EventHandler(PlayList_AdditionRejected);
			this.Disposed += delegate {
				PlayList.Instance.PlayListChanged -= new EventHandler(PlayList_PlayListChanged);
				PlayList.Instance.CurrentContentChanged -= new EventHandler(PlayList_CurrentContentChanged);
				PlayList.Instance.AdditionRejected -= new EventHandler(PlayList_AdditionRejected);
			};

			this.listView1.BeginUpdate();
			this.UpdateItems();
			this.UpdateBoldness();
			this.listView1.EndUpdate();

			this.UpdateStatusBarText();

			//リスト先頭を表示
			this.ScrollToTop();
		}

		private ContentAdapter[] SelectedContents {
			get {
				List<ContentAdapter> conts = new List<ContentAdapter>();
				foreach (ListViewItem lvi in this.listView1.SelectedItems) {
					conts.Add(lvi.Tag as ContentAdapter);
				}
				return conts.ToArray();
			}
			set {
				List<ContentAdapter> conts = new List<ContentAdapter>(value);
				foreach (ListViewItem lvi in this.listView1.Items) {
					lvi.Selected = conts.Contains(lvi.Tag as ContentAdapter);
				}
			}
		}

		private void UpdateItems() {
			int oldCount = this.listView1.Items.Count;
			int newCount = PlayList.Instance.Count;
			int minCount = (oldCount < newCount) ? oldCount : newCount;

			this.listView1.BeginUpdate();
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
			this.listView1.EndUpdate();
		}
		private void UpdateBoldness() {
			this.listView1.BeginUpdate();
			foreach (ListViewItem lvi in this.listView1.Items) {
				FontStyle oldStyle = lvi.Font.Style;
				FontStyle newStyle = PlayList.Instance.IsCurrentContent(lvi.Tag as ContentAdapter) ? FontStyle.Bold : FontStyle.Regular;
				if(oldStyle != newStyle){
					lvi.Font = new Font(lvi.Font, newStyle);
				}
			}
			this.listView1.EndUpdate();
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
				+ selectedTimeSpan.ToString() + (hasExactSelectedTimeSpan ? string.Empty : "+?")
				+ " / "
				+ totalTimeSpan.ToString() + (hasExactTotalTimeSpan ? string.Empty : "+?");
			this.tslMessage.Text = num + "   " + time;
		}
		private void ScrollToTop() {
			if (0 < this.listView1.Items.Count) {
				this.listView1.TopItem = this.listView1.Items[0];
			}
		}
		private void ScrollToBottom() {
			if (0 < this.listView1.Items.Count) {
				this.listView1.TopItem = this.listView1.Items[this.listView1.Items.Count - 1];
			}
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
		private void PlayList_AdditionRejected(object sender, EventArgs e) {
			ContentAdapter[] conts = PlayList.Instance.LastAdditionRejectedContents;
			this.SelectedContents = conts;
			if (conts.Length > 0) {
				foreach (ListViewItem lvi in this.listView1.Items) {
					if (lvi.Tag.Equals(conts[0])) {
						this.listView1.TopItem = lvi;
						break;
					}
				}
			}
		}

		private void listView1_DoubleClick(object sender, EventArgs e) {
			this.tsmiPlay.PerformClick();
		}
		private void listView1_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					this.tsmiPlay.PerformClick();
					break;
				case Keys.A:
					if (Keys.None != (Control.ModifierKeys & Keys.Control)) {
						this.listView1.BeginUpdate();
						foreach (ListViewItem lvi in this.listView1.Items) {
							lvi.Selected = true;
						}
						this.listView1.EndUpdate();
					}
					break;
				case Keys.Escape:
					this.listView1.BeginUpdate();
					foreach (ListViewItem lvi in this.listView1.Items) {
						lvi.Selected = false;
					}
					this.listView1.EndUpdate();
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
						ContentAdapter ca = null;
						try {
							ca = Cache.Instance.GetCacheOrDownloadContent(GContent.ConvertToKeyFromId(id));
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
				ContentAdapter ca = Cache.Instance.GetCacheOrDownloadContent(GContent.ConvertToKeyFromId(this.inputBoxDialog1.Input));
				if (PlayList.Instance.Contains(ca)) {
					MessageBox.Show("指定したIDはすでにプレイリストに存在します．",
						title, MessageBoxButtons.OK, MessageBoxIcon.Error);
				} else {
					PlayList.Instance.AddIfNotExists(ca);
				}
			}
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
		private void tsmiRemoveUnreachables_Click(object sender, EventArgs e) {
			//削除予定のリスト作成
			List<int> reachables = Cache.Instance.GetSortedReachableContentKeys();
			List<ContentAdapter> contsToBeRemoved = new List<ContentAdapter>();
			foreach (ContentAdapter cont in PlayList.Instance) {
				if (reachables.BinarySearch(cont.ContentKey) < 0) {
					contsToBeRemoved.Add(cont);
				}
			}
			//確認と削除
			if (contsToBeRemoved.Count > 0) {
				string separator = "--------------------------------";
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(separator);
				sb.AppendLine("contents_id コンテンツ名");
				sb.AppendLine(separator);
				foreach (ContentAdapter cont in contsToBeRemoved) {
					sb.AppendLine(string.Format("{0} {1}", cont.ContentId, cont.DisplayName));
				}
				sb.Append(separator);
				switch (MessageBox.Show("以下のコンテンツが到達不可能だと判断されました．削除しますか？\n\n" + sb.ToString(), "到達不可コンテンツの削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
					case DialogResult.Yes:
						PlayList.Instance.RemoveAll(contsToBeRemoved.Contains);
						break;
				}
			} else {
				MessageBox.Show("到達不可と思われるコンテンツはプレイリスト内にありませんでした．", "到達不可コンテンツの削除", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void tsmiClearPlayList_Click(object sender, EventArgs e) {
			switch (MessageBox.Show("プレイリスト内の全項目を削除します．よろしいですか？", "プレイリスト内の全項目を削除", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)) {
				case DialogResult.Yes:
					PlayList.Instance.Clear();
					break;
			}
			
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
			this.listView1.BeginUpdate();
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.MoveToTop);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = conts.ToArray();
			if (conts.Count > 0) {
				int idx = PlayList.Instance.IndexOf(conts[0]);
				this.listView1.Items[idx].Focused = true;
				this.ScrollToTop();
			}
			this.listView1.EndUpdate();
		}
		private void tsmiMoveUp_Click(object sender, EventArgs e) {
			int? ensVis = null;
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			this.listView1.BeginUpdate();
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.MoveUp);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = conts.ToArray();
			if (conts.Count > 0) {
				int idx = PlayList.Instance.IndexOf(conts[0]);
				this.listView1.Items[idx].Focused = true;
				ensVis = (0 <= idx - 1) ? idx - 1 : 0;
			}
			this.listView1.EndUpdate();
			if (ensVis.HasValue) {
				this.listView1.EnsureVisible(ensVis.Value);
			}
		}
		private void tsmiMoveDown_Click(object sender, EventArgs e) {
			int? ensVis = null;
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			conts.Reverse();
			this.listView1.BeginUpdate();
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.MoveDown);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = conts.ToArray();
			if (conts.Count > 0) {
				int idx = PlayList.Instance.IndexOf(conts[0]);
				this.listView1.Items[idx].Focused = true;
				ensVis = (this.listView1.Items.Count > idx + 1) ? idx + 1 : idx;
			}
			this.listView1.EndUpdate();
			if (ensVis.HasValue) {
				this.listView1.EnsureVisible(ensVis.Value);
			}
		}
		private void tsmiMoveToBottom_Click(object sender, EventArgs e) {
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			this.listView1.BeginUpdate();
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.MoveToBottom);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = conts.ToArray();
			if (conts.Count > 0) {
				int idx = PlayList.Instance.IndexOf(conts[0]);
				this.listView1.Items[idx].Focused = true;
				this.ScrollToBottom();
			}
			this.listView1.EndUpdate();
		}
		private void tsmiRemoveItem_Click(object sender, EventArgs e) {
			List<ContentAdapter> conts = new List<ContentAdapter>(this.SelectedContents);
			this.listView1.BeginUpdate();
			PlayList.Instance.BeginUpdate();
			conts.ForEach(PlayList.Instance.Remove);
			PlayList.Instance.EndUpdate();
			this.SelectedContents = new ContentAdapter[] { };
			this.listView1.EndUpdate();
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
			string text = ContentAdapter.GetNames(this.SelectedContents);
			if (!string.IsNullOrEmpty(text)) {
				Clipboard.SetText(text);
			}
		}
		private void tsmiCopyUri_Click(object sender, EventArgs e) {
			string text = ContentAdapter.GetUris(this.SelectedContents);
			if (!string.IsNullOrEmpty(text)) {
				Clipboard.SetText(text);
			}
		}
		private void tsmiCopyNameAndUri_Click(object sender, EventArgs e) {
			string text = ContentAdapter.GetNamesAndUris(this.SelectedContents);
			if (!string.IsNullOrEmpty(text)) {
				Clipboard.SetText(text);
			}
		}
		private void tscapmiCopyProperty_PropertySelected(object sender, CAPropertySelectedEventArgs e) {
			string text = ContentAdapter.GetPropertyValueLines(this.SelectedContents, e.PropertyInfo);
			if (!string.IsNullOrEmpty(text)) {
				Clipboard.SetText(text);
			}
		}
		private void tsmiCatalogNormal_Click(object sender, EventArgs e) {
			BrowserForm.Browse(this.SelectedContents);
		}
		private void tsmiCatalogImageSmall_Click(object sender, EventArgs e) {
			Uri[] images = Array.ConvertAll<ContentAdapter, Uri>(this.SelectedContents, new Converter<ContentAdapter, Uri>(delegate(ContentAdapter input) {
				return input.ImageSmallUri;
			}));
			BrowserForm.Browse(images);
		}
		private void tsmiCatalogImageLarge_Click(object sender, EventArgs e) {
			Uri[] images = Array.ConvertAll<ContentAdapter, Uri>(this.SelectedContents, new Converter<ContentAdapter, Uri>(delegate(ContentAdapter input) {
				return input.ImageLargeUri;
			}));
			BrowserForm.Browse(images);
		}
		private void tsucmiCommand_UserCommandSelected(object sender, UserCommandSelectedEventArgs e) {
			e.UserCommand.Execute(this.SelectedContents);
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

		#region IHasNewSettings<PlayListViewSettings> Members
		public PlayListView.PlayListViewSettings Settings {
			get { return this.settings; }
		}
		#endregion
	}
}
