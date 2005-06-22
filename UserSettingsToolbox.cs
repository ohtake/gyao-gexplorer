using System;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class UserSettingsToolbox : Form, IUsesUserSettings{
		private static UserSettingsToolbox instance = null;
		public static UserSettingsToolbox Instance {
			get {
				if(null == UserSettingsToolbox.instance || !UserSettingsToolbox.instance.CanFocus) {
					UserSettingsToolbox.instance = new UserSettingsToolbox();
				}
				return UserSettingsToolbox.instance;
			}
		}
		
		private UserSettingsToolbox() {
			InitializeComponent();
			this.LoadSettings();
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = "ユーザ設定ツールボックス";
			
			this.propertyGrid1.SelectedObject = UserSettings.Instance;
			
			//常に手前に表示
			this.chkTopMost.CheckedChanged += new EventHandler(delegate(object sender, EventArgs e) {
				this.TopMost = this.chkTopMost.Checked;
				this.SaveSettings();
			});
			
			//ユーザ設定
			this.LocationChanged += delegate {
				this.SaveSettings();
			};
			this.SizeChanged += delegate {
				this.SaveSettings();
			};
			this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(
				delegate(object s, PropertyValueChangedEventArgs e) {
					UserSettings.Instance.OnChangeCompleted(true);
				});
			UserSettings.Instance.ChangeCompleted +=
				new UserSettingsChangeCompletedEventHandler(this.ListeningUserSettings);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					UserSettings.Instance.ChangeCompleted -= this.ListeningUserSettings;
				});
		}
		
		private void ListeningUserSettings() {
			this.LoadSettings();
			this.propertyGrid1.Refresh();
		}
		public void LoadSettings(){
			UserSettings.Instance.UserSettingsToolbox.ApplySettings(this);
			this.chkTopMost.Checked = this.TopMost;
		}
		public void SaveSettings() {
			UserSettings.Instance.UserSettingsToolbox.StoreSettings(this);
			UserSettings.Instance.UserSettingsToolbox.OnChangeCompleted();
		}
	}
}
