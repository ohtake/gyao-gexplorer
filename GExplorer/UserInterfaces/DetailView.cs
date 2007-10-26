//#define PATCH_FOR_INVISIBLE_TEXT_ON_VISTA

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Net.Cache;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class DetailView : UserControl, IDetailViewBindingContract, INotifyPropertyChanged {
		private const int ReviewCountOnDetailPage = 5;
		private const int ReviewCountOnListPage = 50;
		
		private static readonly Regex regexText = new Regex(
			@"<!--↓テキストエリア↓-->(?<Text>[\s\S]{0,5000}?)<!--↑テキストエリア↑-->",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly Regex regexReviewCount = new Regex(
			@"<ul class=""part06""><li>レビュー投稿数</li><li> (?<Count>\d+)</li>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly Regex regexReviewAverageScore = new Regex(
			@"<ul class=""part07""><li class=""txt01"">総合評価</li><li><img src=""/recommend/img/star_(?<Score>\d+)\.gif"" alt=""ポイント"" /></li>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly Regex regexReviewPost = new Regex(
			@"<div class=""line"">(?:\r|\n|\r\n)<div class=""clearfix"">(?:\r|\n|\r\n)<h3 class=""part03"">(?<Title>.+?)</h3>(?:\r|\n|\r\n)<div class=""part04"">(?:\r|\n|\r\n)<ul><li class=""day"">投稿日：(?<Posted>.+?)</li><li><img src=""/recommend/img/star_0*(?<Score>\d+)\.gif"" /></li><li class=""txt03"">10点中\d+点</li></ul>(?:\r|\n|\r\n)<p>投稿者：(?<Author>.+?)(?<NetaBare><img src=""/common/img/neta.gif"" />(?:\r|\n|\r\n))?</p>(?:\r|\n|\r\n)</div>(?:\r|\n|\r\n)</div>(?:\r|\n|\r\n)<p class=""part05"">(?<Denominator>\d+)人中の(?<Numerator>\d+)人が「この番組レビューは参考になる」と評価しています。</p>(?:\r|\n|\r\n)<p class=""part06"">(?<Body>.{0,3000}?)</p>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly Regex regexReviewListPost = new Regex(
			@"<span class=""bk12b"">\d+&nbsp;(?<Title>.+?)</span>[\s\S]{0,20}?<td width=""140"" class=""bk10"">投稿者：(?<Author>.+?)</td>[\s\S]{0,20}?<td width=""118"" align=""center"" class=""bk10"">投稿日：(?<Posted>.+?)</td>[\s\S]{0,20}?<td width=""102"" align=""center""><img src=""/recommend/img/star_0*(?<Score>\d+)\.gif"" />[\s\S]{0,500}?(?<NetaBare><img src=""http://www.gyao.jp/common/img/neta.gif"" alt=""ネタバレ"" width=""40"" height=""15"" border=""0"">)?[\s\S]{0,50}?<td align=""right"" height=""35"" class=""bk10"">(?<Denominator>\d+)人中(?<Numerator>\d+)人が「この番組レビューは参考になる」と評価しています。</td>[\s\S]{0,100}?<p class=""marginT5B5 bk12"">(?<Body>.{0,3000}?)</p>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly string[] ColWidthPropertyNames = new string[] {
			"ColWidthNetabare", "ColWidthScore", "ColWidthRef", "ColWidthTitle", "ColWidthAuthor", "ColWidthPosted",
		};
		private static readonly Comparison<ReviewPostListViewItem>[] ReviewComparisons = new Comparison<ReviewPostListViewItem>[]{
			new Comparison<ReviewPostListViewItem>(delegate(ReviewPostListViewItem x, ReviewPostListViewItem y){
				return x.Netabare.CompareTo(y.Netabare);
			}),
			new Comparison<ReviewPostListViewItem>(delegate(ReviewPostListViewItem x, ReviewPostListViewItem y){
				return - x.Score.CompareTo(y.Score);
			}),
			new Comparison<ReviewPostListViewItem>(delegate(ReviewPostListViewItem x, ReviewPostListViewItem y){
				double refRateX = DetailView.CalculateReviewRefRate(x.Denominator, x.Numerator);
				double refRateY = DetailView.CalculateReviewRefRate(y.Denominator, y.Numerator);
				return - refRateX.CompareTo(refRateY);
			}),
			new Comparison<ReviewPostListViewItem>(delegate(ReviewPostListViewItem x, ReviewPostListViewItem y){
				return x.Title.CompareTo(y.Title);
			}),
			new Comparison<ReviewPostListViewItem>(delegate(ReviewPostListViewItem x, ReviewPostListViewItem y){
				return x.Author.CompareTo(y.Author);
			}),
			new Comparison<ReviewPostListViewItem>(delegate(ReviewPostListViewItem x, ReviewPostListViewItem y){
				return - x.Posted.CompareTo(y.Posted);
			}),
		};
		private static double CalculateReviewRefRate(int denominator, int numerator) {
			if (numerator == 0 || denominator == 0) return 0;
			else return (double)numerator / (double)denominator;
		}

		private sealed class ReviewPostListViewItem : ListViewItem {
			private readonly bool netabare;
			private readonly int score;
			private readonly int denominator;
			private readonly int numerator;
			private readonly string title;
			private readonly string author;
			private readonly string posted;
			private readonly string body;

			public ReviewPostListViewItem(bool netabare, string score, string denominator, string numerator, string title, string author, string posted, string body)
				: base(new string[] { netabare ? "!" : string.Empty, score, string.Format("{0}/{1}", numerator, denominator), title, author, posted,  }) {
				this.netabare = netabare;
				this.score = int.Parse(score);
				this.denominator = int.Parse(denominator);
				this.numerator = int.Parse(numerator);
				this.title = title;
				this.author = author;
				this.posted = posted;
				this.body = body;
			}
			public bool Netabare {
				get { return this.netabare; }
			}
			public int Score {
				get { return this.score; }
			}
			public int Denominator {
				get { return this.denominator; }
			}
			public int Numerator {
				get { return this.numerator; }
			}
			public string Title {
				get { return this.title; }
			}
			public string Author {
				get { return this.author; }
			}
			public string Posted {
				get { return this.posted; }
			}
			public string Body {
				get { return this.body; }
			}
		}
		private abstract class PageRequestState {
			private readonly GContentClass content;
			public PageRequestState(GContentClass cont) {
				this.content = cont;
			}
			public GContentClass Content {
				get { return this.content; }
			}
		}
		private sealed class ContentPageRequestState : PageRequestState {
			private readonly int? age;

			public ContentPageRequestState(GContentClass cont): this(cont, null) {
			}
			public ContentPageRequestState(GContentClass cont, int? age) :base(cont) {
				this.age = age;
			}

			public int? Age {
				get { return this.age; }
			}
		}
		private sealed class ReviewListPageRequestState : PageRequestState {
			private readonly int startCount;

			public ReviewListPageRequestState(GContentClass content, int startCount) :base(content) {
				this.startCount = startCount;
			}

			public int StartCount {
				get { return this.startCount; }
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
		private int reviewTotalCount = 0;
		private readonly StackableComparisonsComparer<ReviewPostListViewItem> reviewComparer;
		
		public DetailView() {
			InitializeComponent();

			this.reviewComparer = new StackableComparisonsComparer<ReviewPostListViewItem>();
			this.lvReview.ListViewItemSorter = this.reviewComparer;

			List<ToolStripItem> sortMenus = new List<ToolStripItem>();
			foreach (ColumnHeader ch in this.lvReview.Columns) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem(ch.Text);
				tsmi.Tag = ch.Index;
				tsmi.Click += delegate (object sender, EventArgs e){
					this.PushReviewComparison((int)(sender as ToolStripMenuItem).Tag);
				};
				sortMenus.Add(tsmi);
			}
			this.tsmiSortReviewPosts.DropDownItems.AddRange(sortMenus.ToArray());
		}
		private void DetailView_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			this.wbDescription.DocumentText =
@"<html>
<head>
	<style type=""text/css"">
	body{
		margin: 0;
		font-size: 12px;
		line-height: 1.4;
		color: Black;
		background-color: White;
	}
	#staff{
		color: Teal;
		background-color: Beige;
		border: solid 1px BurlyWood;
		margin: 0px 10px 5px 10px;
		padding: 0px;
	}
	#staff h2, #staff p{
		font-size: 100%;
		margin: 0.25em;
		padding: 0em;
	}
	.part03{
		font-size: 80%;
	}
	.part04{
		text-align: right;
		color: Gray;
	}
	#gexplorer_warning{
		color: DarkRed;
		background-color: Pink;
		border: solid 1px DarkRed;
		text-align: center;
		margin: 0px 20px 5px 20px;
		padding: 4px;
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
			
			this.ChangeEnabilityOfContentDrivenControls();
			this.ChangeEnabilityOfReadMoreControls(false);
			
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

		private void ChangeEnabilityOfContentDrivenControls() {
			bool hasCont = (null != this.cont);

			this.tsmiCopyName.Enabled = hasCont;
			this.tsmiCopyUri.Enabled = hasCont;
			this.tsmiCopyBoth.Enabled = hasCont;
			this.tsmiCopyTriple.Enabled = hasCont;
			this.tsmiCopyImage.Enabled = hasCont;

			this.tsmiOpenReviewList.Enabled = hasCont;
			this.tsmiOpenReviewPost.Enabled = hasCont;

			this.btnReviewList.Enabled = hasCont;
			this.btnReviewInput.Enabled = hasCont;
		}
		private void ChangeEnabilityOfReadMoreControls(bool enability) {
			this.tsmiReadMoreReviews.Enabled = enability;
			this.btnReadMore.Enabled = enability;
		}

		public void ViewDetail(GContentClass cont) {
			if (null == cont) {
				return;
			}
			this.cont = cont;
			this.pgProperty.SelectedObject = cont;
			this.reviewTotalCount = 0;
			this.reviewComparer.ClearComparisons();
			this.ChangeEnabilityOfReadMoreControls(false);
			
			if (this.LoadImageEnabled) {
				this.bgImageLoader.ClearTasks();
				this.bgImageLoader.AddTaskFirst(new BackgroundImageLoadTask(cont.ImageLargeUri, cont));
			}
			if (this.LoadPageEnabled) {
				this.bgTextLoader.ClearTasks();
				this.bgTextLoader.AddTaskFirst(new BackgroundTextLoadTask(cont.ContentDetailUri, new ContentPageRequestState(cont)));
			}
			
			this.ChangeEnabilityOfContentDrivenControls();
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
			PageRequestState state = e.UserState as PageRequestState;
			if (!object.ReferenceEquals(state.Content, this.cont)) return;
			if (this.InvokeRequired) {
				this.Invoke(new EventHandler<BackgroundTextLoadTaskCompletedEventArgs>(this.bgTextLoader_TaskCompleted), sender, e);
			} else {
				ContentPageRequestState stateDetail = state as ContentPageRequestState;
				ReviewListPageRequestState stateList = state as ReviewListPageRequestState;

				if (stateDetail != null) this.HandleTextLoadCompletedDetail(e, stateDetail);
				else if (stateList != null) this.HandleTextLoadCompletedList(e, stateList);
				else throw new ArgumentException();
			}
		}
		private void HandleTextLoadCompletedDetail(BackgroundTextLoadTaskCompletedEventArgs e, ContentPageRequestState reqStateDetail) {
			if(!e.Success){
				this.wbDescription.Document.Body.InnerHtml = "取得失敗";
				this.tabpReview.Text = "レビュー(取得失敗)";
				this.lvReview.Items.Clear();
				this.txtReview.Clear();
				this.StatusMessage = string.Format("詳細ビューでの詳細ページ取得エラー: {0}", e.Error.Message);
				return;
			}
			string text = e.Text;

			if (!reqStateDetail.Age.HasValue) {
				int? age = AdultUtility.FindAdultThresholdInContent(text);
				if (age.HasValue) {
					this.bgTextLoader.AddTaskFirst(new BackgroundTextLoadTask(reqStateDetail.Content.ContentDetailUri, AdultUtility.AdultAnswerBody, new ContentPageRequestState(reqStateDetail.Content, age.Value)));
					return;
				}
			}
			
			Match m = DetailView.regexText.Match(text);
			if (m.Success) {
				string desc = m.Groups["Text"].Value;
				this.wbDescription.Document.Body.InnerHtml = string.Format(
					@"{0}<div style=""{1}"">{2}</ul>",
					reqStateDetail.Age.HasValue ?
						string.Format(@"<div id=""gexplorer_warning"">R{0}のコンテンツです</div>", reqStateDetail.Age.Value)
						: string.Empty,
					this.descriptionStyle,
					desc);
			} else {
				this.wbDescription.Document.Body.InnerHtml =
					string.Format(@"<div id=""gexplorer_warning"">正規表現にマッチしませんでした</div><pre><code>{0}</code></pre>",
						HttpUtility.HtmlEncode(text));
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
				this.reviewTotalCount = reviewCount;
				this.ChangeEnabilityOfReadMoreControls(reviewCount > DetailView.ReviewCountOnDetailPage);
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
		private void HandleTextLoadCompletedList(BackgroundTextLoadTaskCompletedEventArgs e, ReviewListPageRequestState reqStateList) {
			if (!e.Success) {
				this.bgTextLoader.ClearTasks();
				this.StatusMessage = string.Format("レビューの追加取得失敗エラー: {0}", e.Error.Message);
				return;
			}
			
			List<ReviewPostListViewItem> lvis = new List<ReviewPostListViewItem>();
			for (Match m = DetailView.regexReviewListPost.Match(e.Text); m.Success; m = m.NextMatch()) {
				lvis.Add(new ReviewPostListViewItem(
						m.Groups["NetaBare"].Success,
						m.Groups["Score"].Value,
						m.Groups["Denominator"].Value,
						m.Groups["Numerator"].Value,
						HttpUtility.HtmlDecode(m.Groups["Title"].Value),
						HttpUtility.HtmlDecode(m.Groups["Author"].Value),
						m.Groups["Posted"].Value,
						HttpUtility.HtmlDecode(m.Groups["Body"].Value)));
			}
			
			if (lvis.Count <= 0) {
				this.bgTextLoader.ClearTasks();
				this.StatusMessage = "レビューの追加取得でレビューが一件も拾えなかった．";
				return;
			} else {
				this.lvReview.Items.AddRange(lvis.ToArray());
			}
		}

		private void lvReview_SelectedIndexChanged(object sender, EventArgs e) {
			if (this.lvReview.SelectedItems.Count > 0) {
				this.txtReview.Text = (this.lvReview.SelectedItems[0] as ReviewPostListViewItem).Body;
			}
		}
		private void lvReview_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.PushReviewComparison(e.Column);
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
		private void PushReviewComparison(int idx) {
			this.reviewComparer.PushComparison(DetailView.ReviewComparisons[idx]);
			this.lvReview.Sort();
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
#if PATCH_FOR_INVISIBLE_TEXT_ON_VISTA
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

		#region メニュー
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
		private void tsmiReadMoreReviews_Click(object sender, EventArgs e) {
			this.ChangeEnabilityOfReadMoreControls(false);
			for (int start = DetailView.ReviewCountOnDetailPage; start < this.reviewTotalCount; start += DetailView.ReviewCountOnListPage) {
				Uri uri = GUriBuilder.CreateReviewListUri(this.cont.ContentId, this.cont.PackageId, start);
				ReviewListPageRequestState state = new ReviewListPageRequestState(this.cont, start);
				this.bgTextLoader.AddTaskLast(new BackgroundTextLoadTask(uri, state));
			}
			this.tabControl1.SelectedTab = this.tabpReview;
		}
		private void tsmiOpenReviewList_Click(object sender, EventArgs e) {
			Program.BrowsePage(this.cont.ReviewListUri);
		}
		private void tsmiOpenReviewPost_Click(object sender, EventArgs e) {
			Program.BrowsePage(this.cont.ReviewInputUri);
		}
		#endregion

		private void btnReadMore_Click(object sender, EventArgs e) {
			this.tsmiReadMoreReviews.PerformClick();
		}
		private void btnReviewList_Click(object sender, EventArgs e) {
			this.tsmiOpenReviewList.PerformClick();
		}
		private void btnReviewInput_Click(object sender, EventArgs e) {
			this.tsmiOpenReviewPost.PerformClick();
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

#if PATCH_FOR_INVISIBLE_TEXT_ON_VISTA
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

#if PATCH_FOR_INVISIBLE_TEXT_ON_VISTA
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
