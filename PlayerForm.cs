using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AxWMPLib;
using WMPLib;

namespace Yusen.GExplorer {
	partial class PlayerForm : Form, IUsesUserSettings{
		private PlayerForm() {
			InitializeComponent();
			this.Icon = Utility.GetGExplorerIcon();
			Utility.AddHelpMenu(this.menuStrip1);
			//ユーザ設定の読み込み
			this.LoadFromUserSettings();
			//メニュー選択時の動作
			this.tsmiClose.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.Close();
			});
			this.tsmiAlwaysOnTop.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.TopMost = this.tsmiAlwaysOnTop.Checked;
			});
			this.tsmiFullscreen.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.wmpMain.fullScreen = !this.wmpMain.fullScreen;
			});
			//音量の自動調整
			this.wmpMain.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(
				delegate(object sender, _WMPOCXEvents_OpenStateChangeEvent e) {
					// THANKSTO: http://pc8.2ch.net/test/read.cgi/esite/1116115226/81 の神
					if(this.tsmiAutoVolume.Checked && WMPOpenState.wmposMediaOpen == this.wmpMain.openState) {
						this.wmpMain.settings.volume =
							this.wmpMain.currentMedia.getItemInfo("WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL").StartsWith("Adv:") ? 10 : 100;
					}
				});
		}
		
		public PlayerForm(GContent content) :this(){
			this.Text = content.Package.PackageName + (("" == content.ContentName) ? "" : " / " + content.ContentName);
			this.wmpMain.URL = content.MediaFileUri.AbsoluteUri;
			this.ieMain.Navigate(content.DetailPageUri);
		}
		
		public void LoadFromUserSettings() {
			UserSettings settings = UserSettings.Instance;
			this.tsmiAutoVolume.Checked = settings.PlayerAutoVolume;
			this.tsmiAlwaysOnTop.Checked = settings.PlayerAlwaysOnTop;
			this.Size = settings.PlayerSize;
			
			this.TopMost = this.tsmiAlwaysOnTop.Checked;
		}
		public void SaveToUserSettings() {
		}
	}
}
