using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Serialization;
using Yusen.GExplorer.OldApp;

namespace Yusen.GExplorer {
	public sealed partial class NgFavContentsEditor : FormSettingsBase, IFormWithNewSettings<NgFavContentsEditor.NgFavContentsEditorSettings> {
		public sealed class NgFavContentsEditorSettings : INewSettings<NgFavContentsEditorSettings>{
			private readonly NgFavContentsEditor owner;

			public NgFavContentsEditorSettings() : this(null) {
			}
			internal NgFavContentsEditorSettings(NgFavContentsEditor owner) {
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

			#region INewSettings<NgFavContentsEditorSettings> Members
			public void ApplySettings(NgFavContentsEditorSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}

		private static NgFavContentsEditor instance = null;
		public static bool HasInstance {
			get { return null != NgFavContentsEditor.instance && !NgFavContentsEditor.instance.IsDisposed; }
		}
		public static NgFavContentsEditor Instance {
			get {
				if (!NgFavContentsEditor.HasInstance) {
					NgFavContentsEditor.instance = new NgFavContentsEditor();
				}
				return NgFavContentsEditor.instance;
			}
		}

		private NgFavContentsEditorSettings settings;
		
		private NgFavContentsEditor() {
			InitializeComponent();
		}
		public string FilenameForSettings {
			get { return "NgFavContentsEditorSettings.xml"; }
		}

		private void NgContentsEditor_Load(object sender, EventArgs e) {
			this.settings = new NgFavContentsEditorSettings(this);
			Utility.LoadSettingsAndEnableSaveOnClosedNew(this);

			this.editorNg.SetManager(ContentPredicatesManager.NgManager);
			this.editorFav.SetManager(ContentPredicatesManager.FavManager);
		}


		#region IHasNewSettings<NgFavContentsEditor> Members
		public NgFavContentsEditor.NgFavContentsEditorSettings Settings {
			get { return this.settings; }
		}
		#endregion
	}
}
