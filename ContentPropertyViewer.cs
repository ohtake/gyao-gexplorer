using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Yusen.GExplorer {
	partial class ContentPropertyViewer : Form, IUsesUserSettings {
		private static ContentPropertyViewer instance = null;
		public static ContentPropertyViewer Instance {
			get {
				if(null == ContentPropertyViewer.instance
					|| ContentPropertyViewer.instance.IsDisposed) {
					ContentPropertyViewer.instance = new ContentPropertyViewer();
				}
				return ContentPropertyViewer.instance;
			}
		}
		public static void View(GContent content) {
			ContentPropertyViewer.Instance.Show();
			ContentPropertyViewer.Instance.Content = content;
			ContentPropertyViewer.Instance.Focus();
		}
		
		private GContent content = null;
		
		private ContentPropertyViewer() {
			InitializeComponent();
			this.LoadSettings();
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = "リストビューでコンテンツを選択してください";
			
			this.chkTopMost.CheckedChanged += delegate{
				this.TopMost = this.chkTopMost.Checked;
				this.SaveSettings();
			};
			this.LocationChanged += delegate {
				this.SaveSettings();
			};
			this.SizeChanged += delegate {
				this.SaveSettings();
			};
			UserSettings.Instance.ContentPropertyViewer.ChangeCompleted += this.LoadSettings;
			this.FormClosing += delegate {
				if(FormWindowState.Minimized == this.WindowState) {
					//最小化したまま終了されるとウィンドウ位置が変になるので元に戻す
					this.WindowState = FormWindowState.Normal;
				}
				UserSettings.Instance.ContentPropertyViewer.ChangeCompleted -= this.LoadSettings;
			};
			
			MainForm.Instance.SelectedContentsChanged += this.ListeningToContentsSelection;
			this.FormClosing += delegate {
				MainForm.Instance.SelectedContentsChanged -= this.ListeningToContentsSelection;
			};
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
		private void ListeningToContentsSelection(GenreListView sender, IEnumerable<GContent> contents) {
			foreach(GContent content in contents) {
				this.Content = content;
				break;
			}
		}
		public void LoadSettings() {
			UserSettings.Instance.ContentPropertyViewer.ApplySettings(this);
			this.chkTopMost.Checked = this.TopMost;
		}
		public void SaveSettings() {
			UserSettings.Instance.ContentPropertyViewer.StoreSettings(this);
			UserSettings.Instance.ContentPropertyViewer.OnChangeCompleted();
		}
	}
}
