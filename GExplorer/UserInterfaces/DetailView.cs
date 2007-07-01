//#define PATCH_FOR_INVISIBLE_TEXT_IN_VISTA

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web;
using System.Net.Cache;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.Utilities;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class DetailView : UserControl, IDetailViewBindingContract, INotifyPropertyChanged {
		private static readonly Regex regexText = new Regex(
			@"<div id=""txtarea"">(?<Text>[\s\S]{0,5000}?)</div>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		[Obsolete]
		private static readonly Regex regexIgnoreText = new Regex(
			@"<li>(?:\r|\n|\r\n)<p class=""ttl"">総合評価</p><p class=""star""><img src=""/recommend/img/star_\d+\.gif"" /></p><p class=""point"">10点中(?:\r|\n|\r\n)  \d+  点</p>(?:\r|\n|\r\n)<p class=""clear""></p>(?:\r|\n|\r\n)</li>|<li><a href=""mailto:address\?subject=&body=http://www\.gyao\.jp/sityou/catedetail/contents_id/cnt\d+/""><img src=""/sityou/img/img_info_friends\.gif"" /></a></li>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly Regex regexReviewCount = new Regex(
			@"<ul class=""part06""><li>レビュー投稿数</li><li> (?<Count>\d+)</li>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly Regex regexReviewAverageScore = new Regex(
			@"<ul class=""part07""><li class=""txt01"">総合評価</li><li><img src=""/recommend/img/star_(?<Score>\d+)\.gif"" alt=""ポイント"" /></li>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly Regex regexReviewPost = new Regex(
			@"<div class=""line"">(?:\r|\n|\r\n)<div class=""clearfix"">(?:\r|\n|\r\n)<h3 class=""part03"">(?<Title>.+?)</h3>(?:\r|\n|\r\n)<div class=""part04"">(?:\r|\n|\r\n)<ul><li class=""day"">投稿日：(?<Posted>.+?)</li><li><img src=""/recommend/img/star_0*(?<Score>\d+)\.gif"" /></li><li class=""txt03"">10点中\d+点</li></ul>(?:\r|\n|\r\n)<p>投稿者：(?<Author>.+?)(?<NetaBare><img src=""/common/img/neta.gif"" />(?:\r|\n|\r\n))?</p>(?:\r|\n|\r\n)</div>(?:\r|\n|\r\n)</div>(?:\r|\n|\r\n)<p class=""part05"">(?<Denominator>\d+)人中の(?<Numerator>\d+)人が「この番組レビューは参考になる」と評価しています。</p>(?:\r|\n|\r\n)<p class=""part06"">(?<Body>.+?)</p>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly string[] ColWidthPropertyNames = new string[] {
			"ColWidthNetabare", "ColWidthScore", "ColWidthRef", "ColWidthTitle", "ColWidthAuthor", "ColWidthPosted",
		};
		
		private sealed class ReviewPostListViewItem : ListViewItem {
			private string body;

			public ReviewPostListViewItem(bool netabare, string score, string denominator, string numerator, string title, string author, string posted, string body)
				: base(new string[] { netabare ? "!" : string.Empty, score, string.Format("{0}/{1}", numerator, denominator), title, author, posted,  }) {
				this.body = body;
			}
			public string Body {
				get { return this.body; }
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler StatusMessageChanged;

		private string statusMessage = string.Empty;
		
		private bool loadImageEnabled = true;
		private bool loadPageEnabled = true;
		private string descriptionStyle = string.Empty;
		
		private readonly BackgroundImageLoader bgImageLoader = new BackgroundImageLoader(Program.CookieContainer, new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable), DetailViewOptions.ImageTimeoutDefaultValue);
		private readonly BackgroundTextLoader bgTextLoader = new BackgroundTextLoader(Encoding.GetEncoding("Shift_JIS"), Program.CookieContainer, new RequestCachePolicy(RequestCacheLevel.Default), DetailViewOptions.PageTimeoutDefaultValue);
		
		private GContentClass cont;
		
		public DetailView() {
			InitializeComponent();
		}
		private void DetailView_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			this.wbDescription.DocumentText =
@"<html>
<head>
	<style type=""text/css"">
	body{
		margin:0;
		font-size:12px;
		line-height:1.4;
		color:black;
		background-color:white;
	}
	.part03{
		font-size:80%;
	}
	.part04{
		text-align:right;
		color:gray;
	}
	</style>
</head>
<body>
<!--
<div style=""オプションのスタイル"">
	説明文本体
</div>
-->
</body>
</html>";
			
			this.ChangeEnabilityOfMenuItems();

			this.bgImageLoader.TaskCompleted += new EventHandler<BackgroundImageLoadTaskComletedEventArgs>(bgImageLoader_TaskCompleted);
			this.bgTextLoader.TaskCompleted += new EventHandler<BackgroundTextLoadTaskCompletedEventArgs>(bgTextLoader_TaskCompleted);
			this.bgImageLoader.StartWorking();
			this.bgTextLoader.StartWorking();
			this.Disposed += delegate {
				this.bgImageLoader.TaskCompleted -= new EventHandler<BackgroundImageLoadTaskComletedEventArgs>(bgImageLoader_TaskCompleted);
				this.bgTextLoader.TaskCompleted -= new EventHandler<BackgroundTextLoadTaskCompletedEventArgs>(bgTextLoader_TaskCompleted);
				this.bgImageLoader.Dispose();
				this.bgTextLoader.Dispose();
			};
		}

		private void ChangeEnabilityOfMenuItems() {
			bool hasCont = (null != this.cont);

			this.tsmiCopyName.Enabled = hasCont;
			this.tsmiCopyUri.Enabled = hasCont;
			this.tsmiCopyBoth.Enabled = hasCont;
			this.tsmiCopyTriple.Enabled = hasCont;
			this.tsmiCopyImage.Enabled = hasCont;
		}

		public void ViewDetail(GContentClass cont) {
			if (null == cont) {
				return;
			}
			this.cont = cont;
			this.pgProperty.SelectedObject = cont;

			if (this.LoadImageEnabled) {
				this.bgImageLoader.ClearTasks();
				this.bgImageLoader.AddTaskFirst(new BackgroundImageLoadTask(cont.ImageLargeUri, cont));
			}
			if (this.LoadPageEnabled) {
				this.bgTextLoader.ClearTasks();
				this.bgTextLoader.AddTaskFirst(new BackgroundTextLoadTask(cont.ContentDetailUri, cont));
			}
			
			this.ChangeEnabilityOfMenuItems();
		}

		private void bgImageLoader_TaskCompleted(object sender, BackgroundImageLoadTaskComletedEventArgs e) {
			if (!object.ReferenceEquals(e.UserState, this.cont)) return;
			if (this.InvokeRequired) {
				this.Invoke(new EventHandler<BackgroundImageLoadTaskComletedEventArgs>(this.bgImageLoader_TaskCompleted), sender, e);
			} else {
				if (e.Success) {
					this.pbImage.Image = e.Image;
					e.DisposeImage = false;
				} else {
					this.pbImage.Image = this.pbImage.ErrorImage;
					this.StatusMessage = string.Format("詳細ビューの画像読み込みエラー: {0}", e.Error.Message);
				}
			}
		}
		private void bgTextLoader_TaskCompleted(object sender, BackgroundTextLoadTaskCompletedEventArgs e) {
			if (!object.ReferenceEquals(e.UserState, this.cont)) return;
			if (this.InvokeRequired) {
				this.Invoke(new EventHandler<BackgroundTextLoadTaskCompletedEventArgs>(this.bgTextLoader_TaskCompleted), sender, e);
			} else {
				if(!e.Success){
					this.wbDescription.Document.Body.InnerHtml = "取得失敗";
					this.tabpReview.Text = "レビュー(取得失敗)";
					this.lvReview.Items.Clear();
					this.txtReview.Clear();
					this.StatusMessage = string.Format("詳細ビューでの詳細ページ取得エラー: {0}", e.Error.Message);
					return;
				}
				string text = e.Text;
				Match m = DetailView.regexText.Match(text);
				if (m.Success) {
					string desc = m.Groups["Text"].Value;
					desc = DetailView.regexIgnoreText.Replace(desc, string.Empty);
					this.wbDescription.Document.Body.InnerHtml = string.Format(
						@"<div style=""{1}"">{0}</ul>", desc, this.descriptionStyle);
				} else {
					this.wbDescription.Document.Body.InnerHtml = "エラー: 正規表現にマッチしなかった";
				}
				m = DetailView.regexReviewCount.Match(text);
				if (m.Success) {
					int reviewCount = int.Parse(m.Groups["Count"].Value);
					m = DetailView.regexReviewAverageScore.Match(text);
					if (!m.Success) goto reviewFail;
					int aveScore = int.Parse(m.Groups["Score"].Value);
					this.lvReview.BeginUpdate();
					this.lvReview.Items.Clear();
					for (m = DetailView.regexReviewPost.Match(text); m.Success; m = m.NextMatch()) {
						this.lvReview.Items.Add(new ReviewPostListViewItem(
							m.Groups["NetaBare"].Success,
							m.Groups["Score"].Value,
							m.Groups["Denominator"].Value,
							m.Groups["Numerator"].Value,
							HttpUtility.HtmlDecode(m.Groups["Title"].Value),
							HttpUtility.HtmlDecode(m.Groups["Author"].Value),
							m.Groups["Posted"].Value,
							HttpUtility.HtmlDecode(m.Groups["Body"].Value)));
					}
					this.lvReview.EndUpdate();
					this.tabpReview.Text = string.Format("レビュー(数{0} 評価{1})", reviewCount, aveScore);
					goto reviewEnd;
				} else {
					goto reviewFail;
				}
			reviewFail:
				this.tabpReview.Text = "レビュー(無いか取得失敗)";
				this.lvReview.Items.Clear();
			reviewEnd:
				this.txtReview.Clear();
			}
		}
		
		private void lvReview_SelectedIndexChanged(object sender, EventArgs e) {
			if (this.lvReview.SelectedItems.Count > 0) {
				this.txtReview.Text = (this.lvReview.SelectedItems[0] as ReviewPostListViewItem).Body;
			}
		}
		
		private void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (null != handler) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		public ToolStripDropDown GetToolStripDropDown() {
			return this.tsmiDetailView.DropDown;
		}

		public void BindToOptions(DetailViewOptions options) {
			options.NeutralizeUnspecificValues(this);
			BindingContractUtility.BindAllProperties<DetailView, IDetailViewBindingContract>(this, options);
		}

		public string StatusMessage {
			get { return this.statusMessage; }
			private set {
				this.statusMessage = value;
				EventHandler handler = this.StatusMessageChanged;
				if (null != handler) {
					handler(this, EventArgs.Empty);
				}
			}
		}

		private void scRoot_SplitterMoved(object sender, SplitterEventArgs e) {
			this.OnPropertyChanged("ImageHeight");
		}
		private void scReview_SplitterMoved(object sender, SplitterEventArgs e) {
			this.OnPropertyChanged("ReviewListHeight");
		}
		private void lvReview_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e) {
			this.OnPropertyChanged(DetailView.ColWidthPropertyNames[e.ColumnIndex]);
		}
		#region IDetailViewBindingContract Members
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool LoadImageEnabled {
			get { return this.loadImageEnabled; }
			set {
				this.loadImageEnabled = value;
				this.OnPropertyChanged("LoadImageEnabled");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool LoadPageEnabled {
			get { return this.loadPageEnabled; }
			set {
				this.loadPageEnabled = value;
				this.OnPropertyChanged("LoadPageEnabled");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ImageTimeout {
			get { return this.bgImageLoader.Timeout; }
			set {
				this.bgImageLoader.Timeout = value;
				this.OnPropertyChanged("ImageTimeout");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PageTimeout {
			get { return this.bgTextLoader.Timeout; }
			set {
				this.bgTextLoader.Timeout = value;
				this.OnPropertyChanged("PageTimeout");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DescriptionStyle {
			get { return this.descriptionStyle; }
			set {
				this.descriptionStyle = value;
				this.OnPropertyChanged("DescriptionStyle");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ImageHeight {
			get {
				return this.scRoot.SplitterDistance;
			}
			set {
				this.scRoot.SplitterDistance = value;
				this.OnPropertyChanged("ImageHeight");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ReviewListHeight {
			get { return this.scReview.SplitterDistance; }
			set {
				this.scReview.SplitterDistance = value;
				this.OnPropertyChanged("ReviewListHeight");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthNetabare {
			get { return this.chNeta.Width; }
			set {
				this.chNeta.Width = value;
				this.OnPropertyChanged("ColWidthNetabare");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthScore {
			get { return this.chScore.Width; }
			set {
				this.chScore.Width = value;
				this.OnPropertyChanged("ColWidthScore");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthRef {
			get { return this.chRef.Width; }
			set {
				this.chRef.Width = value;
				this.OnPropertyChanged("ColWidthRef");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthTitle {
			get { return this.chTitle.Width; }
			set {
				this.chTitle.Width = value;
				this.OnPropertyChanged("ColWidthTitle");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthAuthor {
			get { return this.chAuthor.Width; }
			set {
				this.chAuthor.Width = value;
				this.OnPropertyChanged("ColWidthAuthor");
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int ColWidthPosted {
			get { return this.chDate.Width; }
			set {
				this.chDate.Width = value;
				this.OnPropertyChanged("ColWidthPosted");
			}
		}
#if PATCH_FOR_INVISIBLE_TEXT_IN_VISTA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EnablePatchForInvisibleTextInVista {
			get {
				return this.txtReview.BorderStyle == BorderStyle.Fixed3D;
			}
			set {
				this.txtReview.BorderStyle = value ? BorderStyle.None : BorderStyle.Fixed3D;
			}
		}
#endif
		#endregion

		private void tsmiCopyName_Click(object sender, EventArgs e) {
			Clipboard.SetText(ContentClipboardUtility.ConvertToName(this.cont));
		}
		private void tsmiCopyUri_Click(object sender, EventArgs e) {
			Clipboard.SetText(cont.ImageLargeUri.AbsoluteUri);
		}
		private void tsmiCopyBoth_Click(object sender, EventArgs e) {
			string name = ContentClipboardUtility.ConvertToName(this.cont);
			Uri uriImage = this.cont.ImageLargeUri;
			Clipboard.SetText(name + Environment.NewLine + uriImage.AbsoluteUri);
		}
		private void tsmiCopyTriple_Click(object sender, EventArgs e) {
			string name = ContentClipboardUtility.ConvertToName(this.cont);
			Uri uriDetail = this.cont.ContentDetailUri;
			Uri uriImage = this.cont.ImageLargeUri;
			Clipboard.SetText(name + Environment.NewLine  + uriDetail.AbsoluteUri + Environment.NewLine + uriImage.AbsoluteUri);
		}
		private void tsmiCopyImage_Click(object sender, EventArgs e) {
			if (null != this.pbImage.Image) {
				Clipboard.SetImage(this.pbImage.Image);
			}
		}

		private void btnReviewList_Click(object sender, EventArgs e) {
			if (null != this.cont) {
				Program.BrowsePage(this.cont.ReviewListUri);
			}
		}
		private void btnReviewInput_Click(object sender, EventArgs e) {
			if (null != this.cont) {
				Program.BrowsePage(this.cont.ReviewInputUri);
			}
		}
	}

	interface IDetailViewBindingContract : IBindingContract {
		bool LoadImageEnabled { get;set;}
		bool LoadPageEnabled { get;set;}
		int ImageTimeout { get;set;}
		int PageTimeout { get;set;}
		string DescriptionStyle { get;set;}
		
		int ImageHeight { get;set;}
		int ReviewListHeight { get;set;}
		
		int ColWidthNetabare { get;set;}
		int ColWidthScore { get;set;}
		int ColWidthRef { get;set;}
		int ColWidthTitle { get;set;}
		int ColWidthAuthor { get;set;}
		int ColWidthPosted { get;set;}

#if PATCH_FOR_INVISIBLE_TEXT_IN_VISTA
		bool EnablePatchForInvisibleTextInVista { get;set;}
#endif
	}
	public sealed class DetailViewOptions : IDetailViewBindingContract {
		internal const int ImageTimeoutDefaultValue = 8000;
		internal const int PageTimeoutDefaultValue = 8000;
		
		public DetailViewOptions() {
		}

		#region IDetailViewBindingContract Members
		private bool loadImageEnabled = true;
		[Category("通信")]
		[DisplayName("画像の取得")]
		[Description("画像を取得します．")]
		[DefaultValue(true)]
		public bool LoadImageEnabled {
			get { return this.loadImageEnabled; }
			set { this.loadImageEnabled = value; }
		}
		private bool loadPageEnabled = true;
		[Category("通信")]
		[DisplayName("説明文とレビューの取得")]
		[Description("説明文とレビューを取得します．")]
		[DefaultValue(true)]
		public bool LoadPageEnabled {
			get { return this.loadPageEnabled; }
			set { this.loadPageEnabled = value; }
		}
		private int imageTimeout = DetailViewOptions.ImageTimeoutDefaultValue;
		[Category("通信")]
		[DisplayName("画像のタイムアウト")]
		[Description("画像を取得するさいのタイムアウトをミリ秒で指定します．")]
		[DefaultValue(DetailViewOptions.ImageTimeoutDefaultValue)]
		public int ImageTimeout {
			get { return this.imageTimeout; }
			set { this.imageTimeout = value; }
		}
		private int pageTimeout = DetailViewOptions.PageTimeoutDefaultValue;
		[Category("通信")]
		[DisplayName("説明文とレビューのタイムアウト")]
		[Description("説明文とレビューを取得するさいのタイムアウトをミリ秒で指定します．")]
		[DefaultValue(DetailViewOptions.PageTimeoutDefaultValue)]
		public int PageTimeout {
			get { return this.pageTimeout; }
			set { this.pageTimeout = value; }
		}
		private string descriptionStyle = "font-size:100%;";
		[Category("説明文")]
		[DisplayName("説明文のUL要素のスタイル")]
		[Description("説明文のUL要素のstyle属性を指定します．入力チェックは行っていないので不正な文字列を指定しないでください．変更は再表示後に有効になります．")]
		[DefaultValue("font-size:100%;")]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string DescriptionStyle {
			get { return this.descriptionStyle; }
			set { this.descriptionStyle = value; }
		}
		
		private int imageHeight = -1;
		[Category("スプリッタの位置")]
		[DisplayName("画像の高さ")]
		[Description("画像の高さを指定します．")]
		[DefaultValue(-1)]
		public int ImageHeight {
			get { return this.imageHeight; }
			set { this.imageHeight = value; }
		}
		private int reviewListHeight = -1;
		[Category("スプリッタの位置")]
		[DisplayName("レビュー一覧の高さ")]
		[Description("レビュー一覧の高さを指定します．")]
		[DefaultValue(-1)]
		public int ReviewListHeight {
			get { return this.reviewListHeight; }
			set { this.reviewListHeight = value; }
		}

		private int colWidthNetabare = -1;
		[Category("カラム幅")]
		[DisplayName("[0] ネタバレ")]
		[Description("カラム 'ネタバレ' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthNetabare {
			get { return this.colWidthNetabare; }
			set { this.colWidthNetabare = value; }
		}
		private int colWidthScore = -1;
		[Category("カラム幅")]
		[DisplayName("[1] 評価")]
		[Description("カラム '評価' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthScore {
			get { return this.colWidthScore; }
			set { this.colWidthScore = value; }
		}
		private int colWidthRef = -1;
		[Category("カラム幅")]
		[DisplayName("[2] 参考")]
		[Description("カラム '参考' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthRef {
			get { return this.colWidthRef; }
			set { this.colWidthRef = value; }
		}
		private int colWidthTitle = -1;
		[Category("カラム幅")]
		[DisplayName("[3] タイトル")]
		[Description("カラム 'タイトル' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthTitle {
			get { return this.colWidthTitle; }
			set { this.colWidthTitle = value; }
		}
		private int colWidthAuthor = -1;
		[Category("カラム幅")]
		[DisplayName("[4] 投稿者")]
		[Description("カラム '投稿者' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthAuthor {
			get { return this.colWidthAuthor; }
			set { this.colWidthAuthor = value; }
		}
		private int colWidthPosted = -1;
		[Category("カラム幅")]
		[DisplayName("[5] 投稿日")]
		[Description("カラム '投稿日' の幅を指定します．")]
		[DefaultValue(-1)]
		public int ColWidthPosted {
			get { return this.colWidthPosted; }
			set { this.colWidthPosted = value; }
		}

#if PATCH_FOR_INVISIBLE_TEXT_IN_VISTA
		private bool enablePatchForInvisibleTextInVista = false;
		[Category("不具合対策")]
		[DisplayName("Vistaでのレビューテキスト")]
		[Description("Vistaでレビューテキストにマウスオーバーするとテキストボックスの文字が消える現象にいい加減に対策してみる．")]
		[DefaultValue(false)]
		public bool EnablePatchForInvisibleTextInVista {
			get { return this.enablePatchForInvisibleTextInVista; }
			set { this.enablePatchForInvisibleTextInVista = value; }
		}
#endif
		#endregion
		
		internal void NeutralizeUnspecificValues(DetailView dview) {
			if (this.ImageHeight < 0) this.ImageHeight = dview.ImageHeight;
			if (this.ReviewListHeight < 0) this.ReviewListHeight = dview.ReviewListHeight;
			if (this.ColWidthNetabare < 0) this.ColWidthNetabare = dview.ColWidthNetabare;
			if (this.ColWidthScore < 0) this.ColWidthScore = dview.ColWidthScore;
			if (this.ColWidthRef < 0) this.ColWidthRef = dview.ColWidthRef;
			if (this.ColWidthTitle < 0) this.ColWidthTitle = dview.ColWidthTitle;
			if (this.ColWidthAuthor < 0) this.ColWidthAuthor = dview.ColWidthAuthor;
			if (this.ColWidthPosted < 0) this.ColWidthPosted = dview.ColWidthPosted;
		}
	}
}
