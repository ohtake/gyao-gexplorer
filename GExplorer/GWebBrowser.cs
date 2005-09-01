using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	public partial class GWebBrowser : WebBrowser {
		private const string stylePackage = "border: 4px dotted blue !important;";
		private const string styleContent = "border: 4px dotted red !important;";
		
		private Dictionary<HtmlElement, string> dicPackage = new Dictionary<HtmlElement, string>();
		private Dictionary<HtmlElement, string> dicContent = new Dictionary<HtmlElement, string>();
		
		public GWebBrowser() {
			InitializeComponent();
			
			//�R���e�L�X�g���j���[
			this.tsmiPackageOpen.Click += new EventHandler(tsmiPackageOpen_Click);
			this.tsmiPackageCancel.Click += new EventHandler(tsmiPackageCancel_Click);
			this.tsmiContentOpenDetail.Click += new EventHandler(tsmiContentOpenDetail_Click);
			this.tsmiContentAddToPlayList.Click += new EventHandler(tsmiContentAddToPlayList_Click);
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
				} catch {
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

		private void Package_Click(object sender, HtmlElementEventArgs e) {
			if (Keys.Alt != Control.ModifierKeys) {
				//Tag��ID���d����ł���
				this.cmsPackage.Tag = this.dicPackage[sender as HtmlElement];
				this.cmsPackage.Location = Control.MousePosition;
				this.cmsPackage.Show();
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void AddToPackages(HtmlElement elem, string packageId) {
			this.dicPackage.Add(elem, packageId);
			elem.Style += GWebBrowser.stylePackage;
			elem.Click += new HtmlElementEventHandler(this.Package_Click);
		}
		private void Content_Click(object sender, HtmlElementEventArgs e) {
			if (Keys.Alt != Control.ModifierKeys) {
				//Tag��ID���d����ł���
				this.cmsContent.Tag = this.dicContent[sender as HtmlElement];
				this.cmsContent.Location = Control.MousePosition;
				this.cmsContent.Show();
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void AddToContents(HtmlElement elem, string contentId) {
			this.dicContent.Add(elem, contentId);
			elem.Style += GWebBrowser.styleContent;
			elem.Click += new HtmlElementEventHandler(this.Content_Click);
		}
		private void ShowHelpOnHowToCancelMenu() {
			MessageBox.Show("Alt�L�[�������Ă���Βʏ�̃N���b�N�Ƃ��Ĉ����܂��D", "�ςȃ��j���[���o�Ă��Ďז�����D", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
			GContent cont;
			if (GContent.TryDownload(contId, out cont)) {
				ContentAdapter ca = new ContentAdapter(cont);
				PlayList.Instance.AddIfNotExists(ca);
			} else {
				MessageBox.Show("�w�肳�ꂽ�R���e���cID�Ɋւ����񂪎擾�ł��܂���ł����D", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void tsmiContentPlayWithoutAdding_Click(object sender, EventArgs e) {
			string contId = this.cmsContent.Tag as string;
			GContent cont;
			if (GContent.TryDownload(contId, out cont)) {
				ContentAdapter ca = new ContentAdapter(cont);
				PlayerForm.Play(ca);
			} else {
				MessageBox.Show("�w�肳�ꂽ�R���e���cID�Ɋւ����񂪎擾�ł��܂���ł����D", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void tsmiContentPlayWmp_Click(object sender, EventArgs e) {
			Utility.PlayWithWMP(GContent.CreateMediaFileUri(this.cmsContent.Tag as string, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate));
		}
		private void tsmiContentPlayBrowser_Click(object sender, EventArgs e) {
			Utility.Browse(GContent.CreatePlayerPageUri(this.cmsContent.Tag as string, GlobalSettings.Instance.BitRate));
		}
		private void tsmiContentCancel_Click(object sender, EventArgs e) {
			this.ShowHelpOnHowToCancelMenu();
		}

		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.tsmiContentCommands.DropDownItems.Clear();
			foreach (UserCommand uc in UserCommandsManager.Instance) {
				ToolStripMenuItem mi = new ToolStripMenuItem(
					uc.Title, null,
					new EventHandler(delegate(object sender2, EventArgs e2) {
					string contId = this.cmsContent.Tag as string;
					GContent cont;
					if (GContent.TryDownload(contId, out cont)) {
						ContentAdapter ca = new ContentAdapter(cont);
						((sender2 as ToolStripMenuItem).Tag as UserCommand).Execute(
							new ContentAdapter[] { ca });
					} else {
						MessageBox.Show("�w�肳�ꂽ�R���e���cID�Ɋւ����񂪎擾�ł��܂���ł����D", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}));
				mi.Tag = uc;
				this.tsmiContentCommands.DropDownItems.Add(mi);
			}
			this.tsmiContentCommands.Enabled = this.tsmiContentCommands.HasDropDownItems;
		}
	}
}