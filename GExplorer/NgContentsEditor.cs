using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Yusen.GExplorer {
	sealed partial class NgContentsEditor : FormSettingsBase, IFormWithSettings<NgContentsEditorSettings> {
		private static NgContentsEditor instance = null;
		public static NgContentsEditor Instance {
			get {
				if(null == NgContentsEditor.instance || NgContentsEditor.instance.IsDisposed) {
					NgContentsEditor.instance = new NgContentsEditor();
				}
				return NgContentsEditor.instance;
			}
		}

		private volatile NgContent selNg = null;

		private NgContentsEditor() {
			InitializeComponent();
		}
		public void FillSettings(NgContentsEditorSettings settings) {
			base.FillSettings(settings);
		}
		public void ApplySettings(NgContentsEditorSettings settings) {
			base.ApplySettings(settings);
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
		}
		
		private void NgContentsEditor_Load(object sender, EventArgs e) {
			Utility.LoadSettingsAndEnableSaveOnClosed(this);

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
				MessageBox.Show(ex.Message, "NGƒRƒ“ƒeƒ“ƒc‚Ì’Ç‰Á", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
	}

	public class NgContentsEditorSettings : FormSettingsBaseSettings {
	}
}
