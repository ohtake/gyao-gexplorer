using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yusen.GCrawler;
using System.Drawing;

namespace Yusen.GExplorer {
	public partial class GWebBrowser : WebBrowser {
		private const string stylePackage = "border-top: 2px dashed blue !important; border-bottom: 2px dashed blue !important;";
		private const string styleContent = "border-top: 2px dashed red !important; border-bottom: 2px dashed red !important;";
		
		private Dictionary<HtmlElement, string> dicPackage = new Dictionary<HtmlElement, string>();
		private Dictionary<HtmlElement, string> dicContent = new Dictionary<HtmlElement, string>();
		
		public GWebBrowser() {
			InitializeComponent();
			
			//�R���e�L�X�g���j���[
			this.tsmiPackageOpen.Click += new EventHandler(tsmiPackageOpen_Click);
			this.tsmiPackageCancel.Click += new EventHandler(tsmiPackageCancel_Click);
			this.tsmiContentOpenDetail.Click += new EventHandler(tsmiContentOpenDetail_Click);
			this.tsmiContentAddToPlayList.Click += new EventHandler(tsmiContentAddToPlayList_Click);
			this.tsmiContentAddToPlayListWithComment.Click += new EventHandler(tsmiContentAddToPlayListWithComment_Click);
			this.tsmiContentPlayWithoutAdding.Click += new EventHandler(tsmiContentPlayWithoutAdding_Click);
			this.tsmiContentPlayWmp.Click += new EventHandler(tsmiContentPlayWmp_Click);
			this.tsmiContentPlayBrowser.Click += new EventHandler(tsmiContentPlayBrowser_Click);
			this.tsmiContentCancel.Click += new EventHandler(tsmiContentCancel_Click);

			//�O���R�}���h
			this.UserCommandsManager_UserCommandsChanged(null, EventArgs.Empty);
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			this.Disposed += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			};
		}
		
		private void GWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			this.dicPackage.Clear();
			this.dicContent.Clear();

			//�p�b�P�[�W�ƃR���e���c�Ƀ��j���[����������
			foreach (HtmlElement elem in (sender as WebBrowser).Document.Body.All) {
				string strUri;
				//URI�I��
				switch (elem.TagName) {
					case "IMG":
						strUri = elem.GetAttribute("src");
						break;
					case "A":
					case "AREA":
						strUri = elem.GetAttribute("href");
						break;
					default:
						continue;
				}
				Uri uri;
				try {
					uri = new Uri(strUri);
				} catch(UriFormatException){
					continue;
				}
				//�p�b�P�[�W����
				string packageId;
				if (GPackage.TryExtractPackageId(uri, out packageId)) {
					this.AddToPackages(elem, packageId);
					continue;
				}
				//�R���e���c����
				string contentId;
				if (GContent.TryExtractContentId(uri, out contentId)) {
					this.AddToContents(elem, contentId);
					continue;
				}
			}
		}
		private void GWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			this.ttId.Hide(this);
		}

		private void Package_Click(object sender, HtmlElementEventArgs e) {
			this.ttId.Hide(this);
			if (Keys.None == (Keys.Alt & Control.ModifierKeys)) {
				//Tag��ID���d����ł���
				this.cmsPackage.Tag = this.dicPackage[sender as HtmlElement];
				this.cmsPackage.Show(Control.MousePosition);
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void Package_MouseEnter(object sender, HtmlElementEventArgs e) {
			if (Keys.None == (Keys.Alt & Control.ModifierKeys)) {
				HtmlElement elem = sender as HtmlElement;
				this.ttId.ToolTipTitle = this.dicPackage[elem];
				//�ꏊ�����܂����Ȃ��̂� MousePosition ��
				this.ttId.Show(" ", this, this.PointToClient(Control.MousePosition));
			}
		}
		private void Package_MouseLeave(object sender, HtmlElementEventArgs e) {
			this.ttId.Hide(this);
		}
		private void AddToPackages(HtmlElement elem, string packageId) {
			this.dicPackage.Add(elem, packageId);
			elem.Style += GWebBrowser.stylePackage;
			elem.MouseEnter += new HtmlElementEventHandler(this.Package_MouseEnter);
			elem.MouseLeave += new HtmlElementEventHandler(this.Package_MouseLeave);
			elem.Click += new HtmlElementEventHandler(this.Package_Click);
		}
		
		private void Content_Click(object sender, HtmlElementEventArgs e) {
			this.ttId.Hide(this);
			if (Keys.None == (Keys.Alt & Control.ModifierKeys)) {
				//Tag��ID���d����ł���
				this.cmsContent.Tag = this.dicContent[sender as HtmlElement];
				this.cmsContent.Show(Control.MousePosition);
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void Content_MouseEnter(object sender, HtmlElementEventArgs e) {
			if (Keys.None == (Keys.Alt & Control.ModifierKeys)) {
				HtmlElement elem = sender as HtmlElement;
				string id = this.dicContent[elem];
				string tipText;
				ContentCache cache;
				if (Cache.Instance.ContentCacheController.TryGetCache(id, out cache)) {
					ContentAdapter ca = new ContentAdapter(cache.Content);
					this.ttId.ToolTipTitle = id + " �̃L���b�V��";
					tipText =
						cache.LastWriteTime.ToString() + Environment.NewLine
						+ ca.GenreName + Environment.NewLine
						+ ca.Title + Environment.NewLine
						+ ca.EpisodeNumber + Environment.NewLine
						+ ca.SubTitle + Environment.NewLine
						+ ca.Duration + Environment.NewLine
						+ ca.Deadline;
				} else {
					this.ttId.ToolTipTitle = id;
					tipText = " ";
				}
				//�ꏊ�����܂����Ȃ��̂� MousePosition ��
				this.ttId.Show(tipText, this, this.PointToClient(Control.MousePosition));
			}
		}
		private void Content_MouseLeave(object sender, HtmlElementEventArgs e) {
			this.ttId.Hide(this);
		}
		private void AddToContents(HtmlElement elem, string contentId) {
			this.dicContent.Add(elem, contentId);
			elem.Style += GWebBrowser.styleContent;
			elem.Click += new HtmlElementEventHandler(this.Content_Click);
			elem.MouseEnter += new HtmlElementEventHandler(this.Content_MouseEnter);
			elem.MouseLeave += new HtmlElementEventHandler(this.Content_MouseLeave);
		}
		private void ShowHelpOnHowToCancelMenu() {
			MessageBox.Show("Alt�L�[�������Ă���Ή����o�܂���D", "�e�B�b�v�⃁�j���[���ז�", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void tsmiPackageOpen_Click(object sender, EventArgs e) {
			this.Url = GPackage.CreatePackagePageUri(this.cmsPackage.Tag as string);
		}
		private void tsmiPackageCancel_Click(object sender, EventArgs e) {
			this.ShowHelpOnHowToCancelMenu();
		}

		private void tsmiContentOpenDetail_Click(object sender, EventArgs e) {
			this.Url = GContent.CreateDetailPageUri(this.cmsContent.Tag as string);
		}
		private void tsmiContentAddToPlayList_Click(object sender, EventArgs e) {
			string contId = this.cmsContent.Tag as string;
			GContent cont = GContent.DoDownload(contId);
			ContentAdapter ca = new ContentAdapter(cont);
			PlayList.Instance.AddIfNotExists(ca);
		}
		private void tsmiContentAddToPlayListWithComment_Click(object sender, EventArgs e) {
			string contId = this.cmsContent.Tag as string;
			GContent cont = GContent.DoDownload(contId);
			ContentAdapter ca = new ContentAdapter(cont);
			this.inputBoxDialog1.Input = string.Empty;
			this.inputBoxDialog1.Message = "�R�����g����͂��Ă��������D";
			this.inputBoxDialog1.Title = "�R�����g�̓���";
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					ca.Comment = this.inputBoxDialog1.Input;
					PlayList.Instance.AddIfNotExists(ca);
					break;
			}
		}
		private void tsmiContentPlayWithoutAdding_Click(object sender, EventArgs e) {
			string contId = this.cmsContent.Tag as string;
			GContent cont = GContent.DoDownload(contId);
			ContentAdapter ca = new ContentAdapter(cont);
			PlayerForm.Play(ca);
		}
		private void tsmiContentPlayWmp_Click(object sender, EventArgs e) {
			Utility.PlayWithWMP(GContent.CreatePlayListUri(this.cmsContent.Tag as string, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate));
		}
		private void tsmiContentPlayBrowser_Click(object sender, EventArgs e) {
			Utility.Browse(GContent.CreatePlayerPageUri(this.cmsContent.Tag as string, GlobalSettings.Instance.BitRate));
		}
		private void tsmiContentCancel_Click(object sender, EventArgs e) {
			this.ShowHelpOnHowToCancelMenu();
		}

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.tsmiContentCommands.DropDownItems.Clear();
			List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();
			foreach (UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem mi = new ToolStripMenuItem(
					uc.Title, null,
					new EventHandler(delegate(object sender2, EventArgs e2) {
					string contId = this.cmsContent.Tag as string;
					GContent cont = GContent.DoDownload(contId);
					ContentAdapter ca = new ContentAdapter(cont);
					((sender2 as ToolStripMenuItem).Tag as UserCommand).Execute(
						new ContentAdapter[] { ca });
				}));
				mi.Tag = uc;
				menuItems.Add(mi);
			}
			this.tsmiContentCommands.DropDownItems.AddRange(menuItems.ToArray());
			this.tsmiContentCommands.Enabled = this.tsmiContentCommands.HasDropDownItems;
		}

		public void GotoCampaign() {
			if(null != this.Document) {
				this.Document.InvokeScript("gotoCampaign");
			}
		}
	}
}
