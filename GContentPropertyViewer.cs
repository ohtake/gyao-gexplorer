using System;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class GContentPropertyViewer : Form, IUsesUserSettings {
		private GContentPropertyViewer() {
			InitializeComponent();
			this.LoadFromUserSettings();
			this.Icon = Utility.GetGExplorerIcon();
		}
		
		public GContentPropertyViewer(GContent content) :this() {
			this.Text = "<" + content.ContentId + "> のプロパティ";
			this.pgContent.SelectedObject = content;
			this.pgPackage.SelectedObject = content.Package;
			this.pgGenre.SelectedObject = content.Genre;
		}
		
		public void LoadFromUserSettings() {
			UserSettings settings = UserSettings.Instance;
			this.Size = settings.GcpvSize;
		}
		
		public void SaveToUserSettings() {
		}
	}
}
