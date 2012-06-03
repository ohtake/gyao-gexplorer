using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AxWMPLib;
using WMPLib;

namespace Yusen.GExplorer {
	partial class PlayerForm : Form, IUsesUserSettings{
		private static PlayerForm instance = null;
		public static PlayerForm Instance {
			get {
				if(null == PlayerForm.instance || ! PlayerForm.instance.CanFocus) {
					PlayerForm.instance = new PlayerForm();
				}
				return PlayerForm.instance;
			}
		}
		
		private GContent content = null;
		private PlayerForm() {
			InitializeComponent();
			this.Icon = Utility.GetGExplorerIcon();
			Utility.AppendHelpMenu(this.menuStrip1);
			//���[�U�ݒ�̓ǂݍ���
			this.LoadSettings();
			//���j���[�I�����̓���
			this.tsmiOpenGenre.Click += delegate{
				MainForm.Instance.Genre = this.Content.Genre;
				MainForm.Instance.Focus();
			};
			this.tsmiProperty.Click +=delegate {
				ContentPropertyViewer cpv = ContentPropertyViewer.Instance;
				cpv.Show();
				cpv.Content = this.Content;
				cpv.Focus();
			};
			this.tsmiReload.Click += delegate {
				this.wmpMain.URL = this.content.MediaFileUri.AbsoluteUri;
			};
			this.tsmiClose.Click += delegate {
				this.Close();
			};
			this.tsmiAlwaysOnTop.Click += delegate {
				this.TopMost = this.tsmiAlwaysOnTop.Checked;
				this.SaveSettings();
			};
			this.tsmiFullscreen.Click += delegate{
				switch(this.wmpMain.playState) {
					case WMPPlayState.wmppsPlaying:
					case WMPPlayState.wmppsPaused:
						this.wmpMain.fullScreen = !this.wmpMain.fullScreen;
						break;
				}
			};
			this.tsmiAutoVolume.Click +=delegate {
				this.SaveSettings();
			};
			this.tsmiFocusOnWmp.Click += delegate {
				this.tabControl1.SelectedTab = this.tabPlayer;
				this.wmpMain.Focus();
			};
			//�O���R�}���h
			this.LoadCommands();
			UserCommandsManager.Instance.UserCommandsChanged += new UserCommandsChangedEventHandler(this.LoadCommands);
			this.FormClosing += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new UserCommandsChangedEventHandler(this.LoadCommands);
			};
			//���ʂ̎�������
			this.wmpMain.OpenStateChange += delegate{
				// THANKSTO: http://pc8.2ch.net/test/read.cgi/esite/1116115226/81 �̐_
				if(this.tsmiAutoVolume.Checked && WMPOpenState.wmposMediaOpen == this.wmpMain.openState) {
					bool isCf = this.wmpMain.currentMedia.getItemInfo("WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL").StartsWith("Adv:");
					this.wmpMain.settings.volume = isCf ? 10 : 100;
					//��̑Ή�
					this.wmpMain.settings.mute = true;
					this.wmpMain.settings.mute = false;
				}
			};
			//���[�U�ݒ�
			this.SizeChanged += delegate {
				this.SaveSettings();
			};
			this.LocationChanged += delegate {
				this.SaveSettings();
			};
			UserSettings.Instance.PlayerForm.ChangeCompleted += this.LoadSettings;
			this.FormClosing += delegate{
				if(FormWindowState.Minimized == this.WindowState) {
					//�ŏ��������܂܏I�������ƃE�B���h�E�ʒu���ςɂȂ�̂Ō��ɖ߂�
					this.WindowState = FormWindowState.Normal;
				}
				UserSettings.Instance.PlayerForm.ChangeCompleted -= this.LoadSettings;
			};
		}
		public GContent Content {
			get {
				if(null == this.content) throw new InvalidOperationException();
				return this.content;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				Utility.SetTitlebarText(this, value.Package.PackageName + (("" == value.ContentName) ? "" : " / " + value.ContentName));
				this.wmpMain.URL = value.MediaFileUri.AbsoluteUri;
				this.ieMain.Navigate(value.DetailPageUri);
				this.content = value;
			}
		}
		public bool AutoVolumeEnabled {
			get {
				return this.tsmiAutoVolume.Checked;
			}
			set {
				this.tsmiAutoVolume.Checked = value;
			}
		}

		public void LoadSettings() {
			UserSettings.Instance.PlayerForm.ApplySettings(this);
			this.tsmiAlwaysOnTop.Checked = this.TopMost;
		}
		public void SaveSettings() {
			UserSettings.Instance.PlayerForm.StoreSettings(this);
			UserSettings.Instance.PlayerForm.OnChangeCompleted();
		}
		
		private void LoadCommands() {
			this.tsmiCommands.DropDownItems.Clear();
			foreach(UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem mi = new ToolStripMenuItem(
					uc.Title, null,
					new EventHandler(delegate(object sender, EventArgs e) {
					((sender as ToolStripMenuItem).Tag as UserCommand).Execute(
						new GContent[] { this.Content });
				}));
				mi.Tag = uc;
				this.tsmiCommands.DropDownItems.Add(mi);
			}
			this.tsmiCommands.Enabled = 0 != this.tsmiCommands.DropDownItems.Count;
		}
	}
}
