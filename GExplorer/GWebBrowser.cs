using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yusen.GCrawler;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Yusen.GExplorer {
	public partial class GWebBrowser : WebBrowser {
		private const string stylePackage = "border-top: 2px dashed blue !important; border-bottom: 2px dashed blue !important;";
		private const string styleContent = "border-top: 2px dashed red !important; border-bottom: 2px dashed red !important;";
		
		private Dictionary<HtmlElement, string> dicPackage = new Dictionary<HtmlElement, string>();
		private Dictionary<HtmlElement, string> dicContent = new Dictionary<HtmlElement, string>();
		
		public GWebBrowser() {
			InitializeComponent();
			
			//コンテキストメニュー
			this.tsmiPackageOpen.Click += new EventHandler(tsmiPackageOpen_Click);
			this.tsmiPackageCancel.Click += new EventHandler(tsmiPackageCancel_Click);
			this.tsmiContentOpenDetail.Click += new EventHandler(tsmiContentOpenDetail_Click);
			this.tsmiContentAddToPlayList.Click += new EventHandler(tsmiContentAddToPlayList_Click);
			this.tsmiContentAddToPlayListWithComment.Click += new EventHandler(tsmiContentAddToPlayListWithComment_Click);
			this.tsmiContentPlayWithoutAdding.Click += new EventHandler(tsmiContentPlayWithoutAdding_Click);
			this.tsmiContentPlayWmp.Click += new EventHandler(tsmiContentPlayWmp_Click);
			this.tsmiContentPlayBrowser.Click += new EventHandler(tsmiContentPlayBrowser_Click);
			this.tsmiContentCancel.Click += new EventHandler(tsmiContentCancel_Click);

			//外部コマンド
			this.UserCommandsManager_UserCommandsChanged(null, EventArgs.Empty);
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			this.Disposed += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			};
		}
		
		private void GWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			this.dicPackage.Clear();
			this.dicContent.Clear();

			//パッケージとコンテンツにメニューをくっつける
			foreach (HtmlElement elem in (sender as WebBrowser).Document.Body.All) {
				string strUri;
				//URI選択
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
				//パッケージ判定
				string packageId;
				if (GPackage.TryExtractPackageId(uri, out packageId)) {
					this.AddToPackages(elem, packageId);
					continue;
				}
				//コンテンツ判定
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
				//TagにIDを仕込んでおく
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
				//場所がうまく取れないので MousePosition で
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
				//TagにIDを仕込んでおく
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
					this.ttId.ToolTipTitle = id + " のキャッシュ";
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
				//場所がうまく取れないので MousePosition で
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
			MessageBox.Show("Altキーを押していれば何も出ません．", "ティップやメニューが邪魔", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
			this.inputBoxDialog1.Message = "コメントを入力してください．";
			this.inputBoxDialog1.Title = "コメントの入力";
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

#if false
		#region http://lab.msdn.microsoft.com/ProductFeedback/ViewWorkaround.aspx?FeedbackID=FDBK12057

		AxHost.ConnectionPointCookie cookie;
		WebBrowserExtendedEvents events;

		//This method will be called to give you a chance to create your own event sink
		protected override void CreateSink() {
			//MAKE SURE TO CALL THE BASE or the normal events won't fire
			base.CreateSink();
			events = new WebBrowserExtendedEvents(this);
			cookie = new AxHost.ConnectionPointCookie(this.ActiveXInstance, events, typeof(DWebBrowserEvents2));
		}

		protected override void DetachSink() {
			if(null != cookie) {
				cookie.Disconnect();
				cookie = null;
			}
			base.DetachSink();
		}
		
		//This new event will fire when the page is navigating
		public event EventHandler<WebBrowserNavigatingEventArgs> BeforeNavigate;
		public event EventHandler<WebBrowserNavigatingEventArgs> BeforeNewWindow;

		protected void OnBeforeNewWindow(Uri uri, out bool cancel) {
			EventHandler<WebBrowserNavigatingEventArgs> h = BeforeNewWindow;
			WebBrowserNavigatingEventArgs args = new WebBrowserNavigatingEventArgs(uri, null);
			if(null != h) {
				h(this, args);
			}
			cancel = args.Cancel;
		}

		protected void OnBeforeNavigate(Uri uri, string frame, out bool cancel) {
			EventHandler<WebBrowserNavigatingEventArgs> h = BeforeNavigate;
			WebBrowserNavigatingEventArgs args = new WebBrowserNavigatingEventArgs(uri, frame);
			if(null != h) {
				h(this, args);
			}
			//Pass the cancellation chosen back out to the events
			cancel = args.Cancel;
		}
		//This class will capture events from the WebBrowser
		private class WebBrowserExtendedEvents : StandardOleMarshalObject, DWebBrowserEvents2 {
			GWebBrowser _Browser;
			public WebBrowserExtendedEvents(GWebBrowser browser) { _Browser = browser; }

			//Implement whichever events you wish
			public void BeforeNavigate2(object pDisp, ref object URL, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel) {
				_Browser.OnBeforeNavigate(new Uri((string)URL), (string)targetFrameName, out cancel);
			}

			public void NewWindow3(object pDisp, ref bool cancel, ref object flags, ref object URLContext, ref object URL) {
				_Browser.OnBeforeNewWindow(new Uri((string)URL), out cancel);
			}
		}
		[ComImport(), Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),
		InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
		TypeLibType(TypeLibTypeFlags.FHidden)]
		private interface DWebBrowserEvents2 {
			[DispId(250)]
			void BeforeNavigate2(
				[In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
				[In] ref object URL,
				[In] ref object flags,
				[In] ref object targetFrameName,
				[In] ref object postData,
				[In] ref object headers,
				[In, Out] ref bool cancel);
			[DispId(273)]
			void NewWindow3(
				[In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
				[In, Out] ref bool cancel,
				[In] ref object flags,
				[In] ref object URLContext,
				[In] ref object URL);
		}
		#endregion
#endif
	}
}
