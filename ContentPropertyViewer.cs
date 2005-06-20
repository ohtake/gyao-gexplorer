using System;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class ContentPropertyViewer : Form, IUsesUserSettings {
		private static ContentPropertyViewer instance = null;
		public static ContentPropertyViewer Instance {
			get {
				if(null == ContentPropertyViewer.instance
					|| !ContentPropertyViewer.instance.CanFocus) {
					ContentPropertyViewer.instance = new ContentPropertyViewer();
				}
				return ContentPropertyViewer.instance;
			}
		}
		
		private GContent content = null;
		
		private ContentPropertyViewer() {
			InitializeComponent();
			this.LoadSettings();
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = "リストビューでコンテンツを選択してください";
			
			this.chkTopMost.CheckedChanged += new EventHandler(delegate(object sender, EventArgs e) {
				this.TopMost = this.chkTopMost.Checked;
				this.SaveSettings();
			});
			this.LocationChanged += new EventHandler(this.SaveToUserSettings);
			this.SizeChanged += new EventHandler(this.SaveToUserSettings);
		}
		
		public GContent Content {
			get {
				if(null == this.content) throw new InvalidOperationException();
				return this.content;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				Utility.SetTitlebarText(this, "<" + value.ContentId + "> のプロパティ");
				this.pgContent.SelectedObject = value;
				this.pgPackage.SelectedObject = value.Package;
				this.pgGenre.SelectedObject = value.Genre;
				this.content = value;
			}
		}
		public void LoadSettings() {
			UserSettings settings = UserSettings.Instance;
			this.Size = settings.GcpvSize;
			this.Location = settings.GcpvLocation;
			this.StartPosition = settings.GcpvStartPosition;
			this.TopMost = settings.GcpvTopMost;

			this.chkTopMost.Checked = this.TopMost;
		}
		private void SaveToUserSettings(object sender, EventArgs e) {
			this.SaveSettings();
		}
		public void SaveSettings() {
			UserSettings settings = UserSettings.Instance;
			settings.GcpvSize = this.Size;
			settings.GcpvLocation = this.Location;
			settings.GcpvStartPosition = this.StartPosition;
			settings.GcpvTopMost = this.TopMost;
			
			settings.OnChangeCompleted();
		}
	}
}
