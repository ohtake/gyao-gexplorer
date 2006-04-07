using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public sealed partial class NgContentsEditor : FormSettingsBase, IFormWithNewSettings<NgContentsEditor.NgContentsEditorSettings> {
		public sealed class NgContentsEditorSettings : INewSettings<NgContentsEditorSettings>{
			private readonly NgContentsEditor owner;

			public NgContentsEditorSettings() : this(null) {
			}
			internal NgContentsEditorSettings(NgContentsEditor owner) {
				this.owner = owner;
				this.formSettingsBaseSettings = new FormSettingsBaseSettings(owner);
			}

			[XmlIgnore]
			[Browsable(false)]
			private bool HasOwner {
				get { return null != this.owner; }
			}

			[ReadOnly(true)]
			[Category("位置とサイズ")]
			[DisplayName("フォームの基本設定")]
			[Description("フォームの基本的な設定です．")]
			public FormSettingsBaseSettings FormSettingsBaseSettings {
				get { return this.formSettingsBaseSettings; }
				set { this.FormSettingsBaseSettings.ApplySettings(value); }
			}
			private FormSettingsBaseSettings formSettingsBaseSettings;

			#region INewSettings<NgContentsEditorSettings> Members
			public void ApplySettings(NgContentsEditorSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}

		private static NgContentsEditor instance = null;
		public static NgContentsEditor Instance {
			get {
				if(!NgContentsEditor.HasInstance) {
					NgContentsEditor.instance = new NgContentsEditor();
				}
				return NgContentsEditor.instance;
			}
		}
		public static bool HasInstance {
			get {
				return null != NgContentsEditor.instance && !NgContentsEditor.instance.IsDisposed;
			}
		}

		private volatile NgContent selNg = null;
		private NgContentsEditorSettings settings;

		private NgContentsEditor() {
			InitializeComponent();
			
			foreach (PropertyInfo pi in typeof(ContentAdapter).GetProperties()) {
				object[] attribs = pi.GetCustomAttributes(typeof(BrowsableAttribute), false);
				if (attribs.Length > 0 && !(attribs[0] as BrowsableAttribute).Browsable) {
					continue;
				}
				this.comboProperty.Items.Add(pi.Name);
			}
			foreach (TwoStringsPredicateMethod m in Enum.GetValues(typeof(TwoStringsPredicateMethod))) {
				this.comboMethod.Items.Add(m);
			}
		}
		public string FilenameForSettings {
			get { return @"NgContentsEditorSettings.xml"; }
		}

		private void UpdateView() {
			this.lvNgContents.BeginUpdate();
			this.lvNgContents.Items.Clear();
			foreach(NgContent nc in NgContentsManager.Instance) {
				ListViewItem lvi = new ListViewItem(
					new string[]{
					nc.Comment,
					nc.PropertyName, nc.Method.ToString(), nc.Word,
					nc.Created.ToString(), nc.LastAbone.ToString()});
				lvi.Tag = nc;
				this.lvNgContents.Items.Add(lvi);
			}
			this.lvNgContents.EndUpdate();
			
			int cntAll = NgContentsManager.Instance.Count;
			int cntAccId = NgContentsManager.Instance.AcceralatedNgIdsCount;
			int cntAccTitle = NgContentsManager.Instance.AcceralatedNgTitlesCount;
			int cntNoneAcc = NgContentsManager.Instance.NonAcceralatedNgContentsCount;
			int cntDiff = cntAll - cntAccId - cntAccTitle - cntNoneAcc;
			this.lblCount.Text = string.Format("総数[{0}] = 高速化済ID[{1}] + 高速化済タイトル[{2}] + 非高速化[{3}] + 食い違い数[{4}]", cntAll, cntAccId, cntAccTitle, cntNoneAcc, cntDiff);
		}
		
		private void NgContentsEditor_Load(object sender, EventArgs e) {
			this.settings = new NgContentsEditorSettings(this);
			Utility.LoadSettingsAndEnableSaveOnClosedNew(this);

			NgContentsManager.Instance.NgContentsChanged +=
				new EventHandler(this.NgContentsManager_NgContentsChanged);
			NgContentsManager.Instance.LastAboneChanged +=
				new EventHandler(this.NgContentsManager_LastAboneChanged);
			this.FormClosing += delegate {
				NgContentsManager.Instance.NgContentsChanged -=
					new EventHandler(this.NgContentsManager_NgContentsChanged);
				NgContentsManager.Instance.LastAboneChanged -=
					new EventHandler(this.NgContentsManager_LastAboneChanged);
			};
			
			this.UpdateView();
		}

		private void NgContentsManager_NgContentsChanged(object sender, EventArgs e) {
			this.UpdateView();
		}
		private void NgContentsManager_LastAboneChanged(object sender, EventArgs e) {
			this.timerLastAbone.Start();
		}
		private void timerLastAbone_Tick(object sender, EventArgs e) {
			this.timerLastAbone.Stop();
			this.UpdateView();
		}

		private void lvNgContents_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Delete:
					List<NgContent> selectedContents = new List<NgContent>();
					foreach(ListViewItem selItem in (sender as ListView).SelectedItems){
						selectedContents.Add(selItem.Tag as NgContent);
					}
					NgContentsManager.Instance.RemoveAll(new Predicate<NgContent>(
						delegate(NgContent nc) {
							return selectedContents.Contains(nc);
						}
					));
					break;
			}
		}
		private void lvNgContents_ColumnClick(object sender, ColumnClickEventArgs e) {
			ListViewItemComparer comparer = new ListViewItemComparer(e.Column);
			List<ListViewItem> lvis = new List<ListViewItem>();
			foreach (ListViewItem lvi in this.lvNgContents.Items) {
				lvis.Add(lvi);
			}

			if (Utility.IsSorted(lvis, comparer)) {
				lvis.Reverse();
			} else {
				lvis.Sort(comparer);
			}
			NgContentsManager.Instance.SetAll(
				lvis.ConvertAll<NgContent>(delegate(ListViewItem lvi) {
					return lvi.Tag as NgContent;
			}));
		}

		private void btnAdd_Click(object sender, EventArgs e) {
			NgContent ng = null;
			try{
				ng = new NgContent(
					this.txtComment.Text,
					this.comboProperty.Text,
					(TwoStringsPredicateMethod)Enum.Parse(
						typeof(TwoStringsPredicateMethod), this.comboMethod.Text),
					this.txtWord.Text);
			}catch(Exception ex){
				MessageBox.Show(ex.Message, "NGコンテンツの追加", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			NgContentsManager.Instance.Add(ng);
		}

		private void lvNgContents_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if(e.IsSelected) {
				this.selNg = e.Item.Tag as NgContent;
				this.timerItemSelect.Start();
			}
		}

		private void timerItemSelect_Tick(object sender, EventArgs e) {
			this.timerItemSelect.Stop();
			NgContent ng = this.selNg;
			if(null != ng) {
				this.txtComment.Text = ng.Comment;
				this.comboProperty.Text = ng.PropertyName;
				this.comboMethod.Text = ng.Method.ToString();
				this.txtWord.Text = ng.Word;
			}
		}

		#region IHasNewSettings<NgContentsEditorSettings> Members
		public NgContentsEditor.NgContentsEditorSettings Settings {
			get { return this.settings; }
		}
		#endregion
	}
}
