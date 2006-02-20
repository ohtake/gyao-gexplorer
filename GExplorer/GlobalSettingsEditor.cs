using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	sealed partial class GlobalSettingsEditor : FormSettingsBase, IFormWithSettings<GlobalSettingsEditorSettings> {
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
		}

		public void FillSettings(GlobalSettingsEditorSettings settings) {
			base.FillSettings(settings);
		}
		public void ApplySettings(GlobalSettingsEditorSettings settings) {
			base.ApplySettings(settings);
		}
		public string FilenameForSettings {
			get { return @"GlobalSettingsEditorSettings.xml"; }
		}
		private void GlobalSettingsEditor_Load(object sender, EventArgs e) {
			Utility.LoadSettingsAndEnableSaveOnClosed(this);
			this.propertyGrid1.SelectedObject = GlobalSettings.Instance;
		}
	}

	public class GlobalSettingsEditorSettings : FormSettingsBaseSettings {
	}
}
