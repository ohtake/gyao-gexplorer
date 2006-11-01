using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yusen.GExplorer.Utilities;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.UserInterfaces {
	public partial class GWebBrowser : WebBrowser {
		private sealed class WebBrowserXButtonDownListener : NativeWindow {
			private static short GetHighWordOf(int x) {
				return (short)(x >> 16);
			}
			private static short GetLowWordOf(int x) {
				return (short)(x & 0xFFFF);
			}

			public event EventHandler XButton1Down;
			public event EventHandler XButton2Down;

			public WebBrowserXButtonDownListener(WebBrowser wb) {
				wb.HandleCreated += new EventHandler(wb_HandleCreated);
				wb.HandleDestroyed += new EventHandler(wb_HandleDestroyed);
			}
			
			private void wb_HandleCreated(object sender, EventArgs e) {
				base.AssignHandle((sender as WebBrowser).Handle);
			}
			private void wb_HandleDestroyed(object sender, EventArgs e) {
				base.ReleaseHandle();
			}

			protected override void WndProc(ref Message m) {
				switch ((WM)m.Msg) {
					case WM.PARENTNOTIFY:
						switch ((WM)WebBrowserXButtonDownListener.GetLowWordOf(m.WParam.ToInt32())) {
							case WM.XBUTTONDOWN:
								switch ((XBUTTON)WebBrowserXButtonDownListener.GetHighWordOf(m.WParam.ToInt32())) {
									case XBUTTON.XBUTTON1:
										this.OnXButton1Down();
										break;
									case XBUTTON.XBUTTON2:
										this.OnXButton2Down();
										break;
								}
								break;
						}
						break;
				}
				base.WndProc(ref m);
			}

			private void OnXButton1Down() {
				EventHandler handler = this.XButton1Down;
				if (null != handler) {
					handler(this, EventArgs.Empty);
				}
			}
			private void OnXButton2Down() {
				EventHandler handler = this.XButton2Down;
				if (null != handler) {
					handler(this, EventArgs.Empty);
				}
			}
		}
		
		private Dictionary<HtmlElement, int> dicPackage = new Dictionary<HtmlElement, int>();
		private Dictionary<HtmlElement, int> dicContent = new Dictionary<HtmlElement, int>();

		private HtmlElement clickedPackage;
		private HtmlElement clickedContent;
		
		private bool ignoreMenuFlag = false;

		public GWebBrowser() {
			InitializeComponent();
			
			//コンテキストメニュー
			this.tsmiPackagePerformClick.Click += new EventHandler(tsmiPackagePerformClick_Click);
			this.tsmiPackageOpen.Click += new EventHandler(tsmiPackageOpen_Click);
			this.tsmiPackageCancel.Click += new EventHandler(tsmiPackageCancel_Click);
			this.tsmiContentPerformClick.Click += new EventHandler(tsmiContentPerformClick_Click);
			this.tsmiContentOpenDetail.Click += new EventHandler(tsmiContentOpenDetail_Click);
			this.tspmiAddToPlaylist.PlaylistSelected += new EventHandler(tspmiAddToPlaylist_PlaylistSelected);
			this.tsmiContentPlayWithoutAdding.Click += new EventHandler(tsmiContentPlayWithoutAdding_Click);
			this.tsmiContentCancel.Click += new EventHandler(tsmiContentCancel_Click);

			//拡張ボタン
			WebBrowserXButtonDownListener wbxbdListener = new WebBrowserXButtonDownListener(this);
			wbxbdListener.XButton1Down += new EventHandler(this.wbxbdListener_XButton1Down);
			wbxbdListener.XButton2Down += new EventHandler(this.wbxbdListener_XButton2Down);
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
				//コンテンツ判定
				string contentId;
				if (GIdExtractor.TryExtractContentId(uri, out contentId)) {
					this.AddToContents(elem, GConvert.ToContentKey(contentId));
					continue;
				}
				//パッケージ判定
				string packageId;
				if (GIdExtractor.TryExtractPackageId(uri, out packageId)) {
					this.AddToPackages(elem, GConvert.ToPackageKey(packageId));
					continue;
				}
			}
		}
		private void GWebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e) {
			this.ttId.Hide(this);
		}
		private void GWebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e) {
			this.ttId.Hide(this);
		}

		private void Package_Click(object sender, HtmlElementEventArgs e) {
			this.ttId.Hide(this);
			if (this.ignoreMenuFlag) {
				return;
			}
			if (Keys.None == (Keys.Alt & Control.ModifierKeys)) {
				this.clickedPackage = sender as HtmlElement;
				this.cmsPackage.Show(Control.MousePosition);
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void Package_MouseEnter(object sender, HtmlElementEventArgs e) {
			if (Keys.None == (Keys.Alt & Control.ModifierKeys)) {
				HtmlElement elem = sender as HtmlElement;
				this.ttId.ToolTipTitle = this.dicPackage[elem].ToString();
				//場所がうまく取れないので MousePosition で
				this.ttId.Show(" ", this, this.PointToClient(Control.MousePosition));
			}
		}
		private void Package_MouseLeave(object sender, HtmlElementEventArgs e) {
			this.ttId.Hide(this);
		}
		private void AddToPackages(HtmlElement elem, int packageKey) {
			this.dicPackage.Add(elem, packageKey);
			elem.MouseEnter += new HtmlElementEventHandler(this.Package_MouseEnter);
			elem.MouseLeave += new HtmlElementEventHandler(this.Package_MouseLeave);
			elem.Click += new HtmlElementEventHandler(this.Package_Click);
		}
		
		private void Content_Click(object sender, HtmlElementEventArgs e) {
			this.ttId.Hide(this);
			if (this.ignoreMenuFlag) {
				return;
			}
			if (Keys.None == (Keys.Alt & Control.ModifierKeys)) {
				this.clickedContent = sender as HtmlElement;
				this.cmsContent.Show(Control.MousePosition);
				e.BubbleEvent = false;
				e.ReturnValue = false;
			}
		}
		private void Content_MouseEnter(object sender, HtmlElementEventArgs e) {
			if (Keys.None == (Keys.Alt & Control.ModifierKeys)) {
				HtmlElement elem = sender as HtmlElement;
				int key = this.dicContent[elem];
				string id = GConvert.ToContentId(key);
				string tipText;
				GContentClass cont;
				if (Program.CacheManager.TryFindContent(key, out cont)) {
					this.ttId.ToolTipTitle = id + " のキャッシュ";
					tipText =
						cont.Title + Environment.NewLine
						+ cont.SeriesNumber + Environment.NewLine
						+ cont.Subtitle + Environment.NewLine
						+ cont.DurationValue.ToString() + Environment.NewLine
						+ cont.DeadlineValue.ToString() + Environment.NewLine
						+ cont.SummaryText;
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
		private void AddToContents(HtmlElement elem, int contentKey) {
			this.dicContent.Add(elem, contentKey);
			elem.Click += new HtmlElementEventHandler(this.Content_Click);
			elem.MouseEnter += new HtmlElementEventHandler(this.Content_MouseEnter);
			elem.MouseLeave += new HtmlElementEventHandler(this.Content_MouseLeave);
		}
		private void ShowHelpOnHowToCancelMenu() {
			MessageBox.Show("Altキーを押していれば，\nマウスオーバーしてもティップが出ませんし，\nクリックしてもメニューが出ずに通常のクリックとして扱われます．", "ティップやメニューが邪魔", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		#region コンテキストメニュー
		private void tsmiPackagePerformClick_Click(object sender, EventArgs e) {
			this.ignoreMenuFlag = true;
			this.timerIgnoreMenu.Start();
			this.clickedPackage.InvokeMember("click");
		}
		private void tsmiPackageOpen_Click(object sender, EventArgs e) {
			this.Url = GUriBuilder.CreatePackagePageUri(this.dicPackage[this.clickedPackage]);
		}
		private void tsmiPackageCancel_Click(object sender, EventArgs e) {
			this.ShowHelpOnHowToCancelMenu();
		}

		private void tsmiContentPerformClick_Click(object sender, EventArgs e) {
			this.ignoreMenuFlag = true;
			this.timerIgnoreMenu.Start();
			this.clickedContent.InvokeMember("click");
		}
		private void tsmiContentOpenDetail_Click(object sender, EventArgs e) {
			this.Url = GUriBuilder.CreateContentDetailUri(this.dicContent[this.clickedContent]);
		}
		private void tspmiAddToPlaylist_PlaylistSelected(object sender, EventArgs e) {
			int contKey = this.dicContent[this.clickedContent];
			GContentClass cont;
			if (Program.CacheManager.TryFindContentOrTryFetchContent(contKey, out cont)) {
				this.tspmiAddToPlaylist.LastSelectedPlaylist.AddContent(cont);
			} else {
				MessageBox.Show(string.Format("{0}のキャッシュを持っておらず，なおかつ{0}の詳細ページからも情報を取得できなかった．", GConvert.ToContentId(contKey)), "コンテンツの取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void tsmiContentPlayWithoutAdding_Click(object sender, EventArgs e) {
			int contKey = this.dicContent[this.clickedContent];
			GContentClass cont;
			if (Program.CacheManager.TryFindContentOrTryFetchContent(contKey, out cont)) {
				Program.PlayContent(cont, null);
			} else {
				MessageBox.Show(string.Format("{0}のキャッシュを持っておらず，なおかつ{0}の詳細ページからも情報を取得できなかった．", GConvert.ToContentId(contKey)), "コンテンツの取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void tsmiContentCancel_Click(object sender, EventArgs e) {
			this.ShowHelpOnHowToCancelMenu();
		}
		#endregion

		private void wbxbdListener_XButton1Down(object sender, EventArgs e) {
			base.GoBack();
		}
		private void wbxbdListener_XButton2Down(object sender, EventArgs e) {
			base.GoForward();
		}
		
		private void timerIgnoreMenu_Tick(object sender, EventArgs e) {
			this.timerIgnoreMenu.Stop();
			//クリックイベントが複数起きることがあるので
			//フラグではなくタイマーでお茶を濁す
			this.ignoreMenuFlag = false;
		}

		public IEnumerator<int> GetContentKeyEnumerator() {
			return this.dicContent.Values.GetEnumerator();
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
