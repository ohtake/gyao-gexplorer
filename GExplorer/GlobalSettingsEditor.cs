using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class GlobalSettingsEditor : FormSettingsBase, IFormWithSettings<GlobalSettingsEditorSettings> {
		private static GlobalSettingsEditor instance = null;
		public static GlobalSettingsEditor Instance {
			get {
				if (null == GlobalSettingsEditor.instance || GlobalSettingsEditor.instance.IsDisposed) {
					GlobalSettingsEditor.instance = new GlobalSettingsEditor();
				}
				return GlobalSettingsEditor.instance;
			}
		}
		
		public GlobalSettingsEditor() {
			InitializeComponent();
			this.propertyGrid1.SelectedObject = GlobalSettings.Instance;
		}

		public void FillSettings(GlobalSettingsEditorSettings settings) {
			base.FillSettings(settings);
		}
		public void ApplySettings(GlobalSettingsEditorSettings settings) {
			base.ApplySettings(settings);

			this.chkTopMost.Checked = this.TopMost;
		}
		public string FilenameForSettings {
			get { return @"GlobalSettingsEditorSettings.xml"; }
		}
		private void GlobalSettingsEditor_Load(object sender, EventArgs e) {
			Utility.LoadSettingsAndEnableSaveOnClosed(this);
		}
		private void chkTopMost_CheckedChanged(object sender, EventArgs e) {
			this.TopMost = this.chkTopMost.Checked;
		}
	}

	public class GlobalSettingsEditorSettings : FormSettingsBaseSettings {
	}
}
