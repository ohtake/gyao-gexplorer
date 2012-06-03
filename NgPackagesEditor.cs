using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Yusen.GExplorer {
	partial class NgPackagesEditor : Form, IUsesUserSettings {
		private static NgPackagesEditor instance = null;
		public static NgPackagesEditor Instance {
			get {
				if(null == NgPackagesEditor.instance || NgPackagesEditor.instance.IsDisposed) {
					NgPackagesEditor.instance = new NgPackagesEditor();
				}
				return NgPackagesEditor.instance;
			}
		}
		
		private NgPackagesEditor() {
			InitializeComponent();
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = "NG�p�b�P�[�W�G�f�B�^";
			this.LoadSettings();
			
			this.LocationChanged += delegate {
				this.SaveSettings();
			};
			this.SizeChanged += delegate {
				this.SaveSettings();
			};
			UserSettings.Instance.NgPackagesEditor.ChangeCompleted += this.LoadSettings;
			this.FormClosing += delegate {
				if(FormWindowState.Minimized == this.WindowState) {
					//�ŏ��������܂܏I�������ƃE�B���h�E�ʒu���ςɂȂ�̂Ō��ɖ߂�
					this.WindowState = FormWindowState.Normal;
				}
				UserSettings.Instance.NgPackagesEditor.ChangeCompleted -= this.LoadSettings;
			};
			
			foreach(PropertyInfo pi in typeof(GPackage).GetProperties()) {
				object[] attribs = pi.GetCustomAttributes(typeof(BrowsableAttribute), false);
				if(attribs.Length > 0 && !(attribs[0] as BrowsableAttribute).Browsable) {
					continue;
				}
				this.comboProperty.Items.Add(pi.Name);
			}
			foreach(TwoStringsPredicateMethod m in Enum.GetValues(typeof(TwoStringsPredicateMethod))) {
				this.comboMethod.Items.Add(m);
			}

			NgPackagesManager.Instance.NgPackagesChanged +=
				new EventHandler(this.NgPackagesManager_NgPackagesChanged);
			NgPackagesManager.Instance.LastAboneChanged +=
				new EventHandler(this.NgPackagesManager_LastAboneChanged);
			this.FormClosing += delegate {
				NgPackagesManager.Instance.NgPackagesChanged -=
					new EventHandler(this.NgPackagesManager_NgPackagesChanged);
				NgPackagesManager.Instance.LastAboneChanged -=
					new EventHandler(this.NgPackagesManager_LastAboneChanged);
			};
			
			this.RefleshView();
		}
		
		private void RefleshView() {
			this.lvNgPackages.BeginUpdate();
			this.lvNgPackages.Items.Clear();
			foreach(NgPackage np in NgPackagesManager.Instance) {
				ListViewItem lvi = new ListViewItem(
					new string[]{
					np.Comment,
					np.PropertyName, np.Method.ToString(), np.Word,
					np.Created.ToString(), np.LastAbone.ToString()});
				lvi.Tag = np;
				this.lvNgPackages.Items.Add(lvi);
			}
			this.lvNgPackages.EndUpdate();
		}
		private void NgPackagesManager_NgPackagesChanged(object sender, EventArgs e) {
			this.RefleshView();
		}
		private void NgPackagesManager_LastAboneChanged(object sender, EventArgs e) {
			this.RefleshView();
		}
		public void LoadSettings() {
			UserSettings.Instance.NgPackagesEditor.ApplySettings(this);
		}
		public void SaveSettings() {
			UserSettings.Instance.NgPackagesEditor.StoreSettings(this);
			UserSettings.Instance.NgPackagesEditor.OnChangeCompleted();
		}

		private void lvNgPackages_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Delete:
					foreach(ListViewItem selItem in (sender as ListView).SelectedItems) {
						NgPackagesManager.Instance.Remove(selItem.Tag as NgPackage);
					}
					break;
			}
		}

		private void btnAdd_Click(object sender, EventArgs e) {
			try {
				NgPackagesManager.Instance.Add(new NgPackage(
					this.txtComment.Text,
					this.comboProperty.Text,
					(TwoStringsPredicateMethod)Enum.Parse(
						typeof(TwoStringsPredicateMethod), this.comboMethod.Text),
					this.txtWord.Text));
			} catch(Exception ex) {
				Utility.DisplayException(ex);
			}
		}
		
	}
}
