using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Yusen.GExplorer {
	partial class NgContentsEditor : FormSettingsBase, IFormWithSettings<NgContentsEditorSettings> {
		private static NgContentsEditor instance = null;
		public static NgContentsEditor Instance {
			get {
				if(null == NgContentsEditor.instance || NgContentsEditor.instance.IsDisposed) {
					NgContentsEditor.instance = new NgContentsEditor();
				}
				return NgContentsEditor.instance;
			}
		}
		
		private NgContentsEditor() {
			InitializeComponent();
			this.Text = "NGコンテンツエディタ";
			
			foreach(PropertyInfo pi in typeof(ContentAdapter).GetProperties()) {
				object[] attribs = pi.GetCustomAttributes(typeof(BrowsableAttribute), false);
				if(attribs.Length > 0 && !(attribs[0] as BrowsableAttribute).Browsable) {
					continue;
				}
				this.comboProperty.Items.Add(pi.Name);
			}
			foreach(TwoStringsPredicateMethod m in Enum.GetValues(typeof(TwoStringsPredicateMethod))) {
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
			this.UpdateView();
		}

		private void NgContentsManager_NgContentsChanged(object sender, EventArgs e) {
			this.UpdateView();
		}
		private void NgContentsManager_LastAboneChanged(object sender, EventArgs e) {
			this.UpdateView();
		}

		private void lvNgContents_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Delete:
					foreach(ListViewItem selItem in (sender as ListView).SelectedItems) {
						NgContentsManager.Instance.Remove(selItem.Tag as NgContent);
					}
					break;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e) {
			NgContentsManager.Instance.Add(new NgContent(
				this.txtComment.Text,
				this.comboProperty.Text,
				(TwoStringsPredicateMethod)Enum.Parse(
					typeof(TwoStringsPredicateMethod), this.comboMethod.Text),
				this.txtWord.Text));
		}
	}

	public class NgContentsEditorSettings : FormSettingsBaseSettings {
	}
}
