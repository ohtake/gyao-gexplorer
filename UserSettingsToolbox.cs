using System;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class UserSettingsToolbox : Form, IUsesUserSettings{
		public UserSettingsToolbox() {
			InitializeComponent();
			this.LoadFromUserSettings();
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = "ユーザ設定ツールボックス";
			
			this.propertyGrid1.SelectedObject = UserSettings.Instance;
			
			//常に手前に表示
			this.chkTopMost.CheckedChanged += new EventHandler(delegate(object sender, EventArgs e) {
				this.TopMost = this.chkTopMost.Checked;
				this.SaveToUserSettings();
			});
			
			//ユーザ設定
			this.LocationChanged += new EventHandler(this.SaveToUserSettings);
			this.Resize += new EventHandler(this.SaveToUserSettings);
			this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(
				delegate(object s, PropertyValueChangedEventArgs e) {
					UserSettings.Instance.OnChangeCompleted();
				});
			UserSettings.Instance.ChangeCompleted +=
				new UserSettingsChangeCompletedEventHandler(this.ListeningUserSettings);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					UserSettings.Instance.ChangeCompleted -= this.ListeningUserSettings;
				});
		}
		
		private void ListeningUserSettings() {
			this.LoadFromUserSettings();
			this.propertyGrid1.Refresh();
		}
		public void LoadFromUserSettings(){
			UserSettings settings = UserSettings.Instance;
			this.StartPosition = settings.UstStartPosition;
			this.Location = settings.UstLocation;
			this.Size = settings.UstSize;
			this.TopMost = settings.UstTopMost;
			
			this.chkTopMost.Checked = this.TopMost;
		}
		private void SaveToUserSettings(object sender, EventArgs e) {
			this.SaveToUserSettings();
		}
		public void SaveToUserSettings() {
			UserSettings settings = UserSettings.Instance;
			settings.UstStartPosition = this.StartPosition;
			settings.UstLocation = this.Location;
			settings.UstSize = this.Size;
			settings.UstTopMost = this.TopMost;
			settings.OnChangeCompleted();
		}
	}
}
