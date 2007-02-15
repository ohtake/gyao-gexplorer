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

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class DetailView : UserControl, IDetailViewBindingContract, INotifyPropertyChanged {
		public const string DefaultDescription1Style = "font-size:12px;";
		public const string DefaultDescription2Style = "font-size:12px;";
		public const string DefaultDescription3Style = "font-size:10px;";
		public const string DefaultDescription4Style = "font-size:10px; text-align:right;";
		
		private static readonly Regex regexDesc = new Regex(
			@"<table width=""296"" border=""0"" cellspacing=""0"" cellpadding=""0"">\r?\n<tr>\r?\n<td align=""left"">(?<Desc1>.*?)</td>\r?\n</tr>\r?\n</table>\r?\n</div>\r?\n<div class=""marginT10"">\r?\n<table width=""296"" border=""0"" cellspacing=""0"" cellpadding=""0"">\r?\n<tr>\r?\n<td align=""left"">\r?\n(?<Desc2>.*?)</td>\r?\n</tr>\r?\n</table>\r?\n</div>\r?\n<div class=""marginT10"">\r?\n<table width=""296"" border=""0"" cellspacing=""0"" cellpadding=""0"">\r?\n<tr>\r?\n<td align=""(left|center)""( class=""text10"")?>\r?\n(?<Desc3>[\s\S]*?)</td>\r?\n</tr>\r?\n</table>\r?\n</div>\r?\n<div class=""marginT10"">\r?\n<table width=""296"" border=""0"" cellspacing=""0"" cellpadding=""0"">\r?\n<tr>\r?\n<td align=""right""( class=""text10"")?>\r?\n(?<Desc4>.*?)</td>\r?\n</tr>\r?\n</table>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly Regex regexReviewSummary = new Regex(
			@"<td width=""115"" class=""title12b"" bgcolor=""#666666"" align=""left"">(?:<span class=""marginR10"">(?<Summary>.*?)点?</span>)?</td>",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		private static readonly Regex regexReviewPost = new Regex(
			@"<td width=""210"" height=""25"" class=""bk12b"">(?<Title>.*?)</td>\r?\n<td width=""140"" class=""bk10"">投稿者：(?<Author>.*?)</td>\r?\n<td width=""118"" align=""center"" class=""bk10"">投稿日：(?<Posted>.*?) </td>\r?\n<td width=""102"" align=""center"">\r?\n<table width=""85"" border=""0"" cellspacing=""0"" cellpadding=""0"">\r?\n(<td><img src = ""http://www\.gyao\.jp/common/images/star_(w|half_n|half_w)\.gif"" width=""15"" height=""15"" border=""0""> </td>\r?\n){5} \r?\n</table>\r?\n</td>\r?\n<td width=""50"" align=""right"" class=""bk10"">(?<Score>\d+)点</td>\r?\n</tr>\r?\n</table>\r?\n</td></tr>\r?\n<tr><td><table width=""620"" border=""0"" cellspacing=""0"" cellpadding=""0"">\r?\n(?<NetaBare><tr><td><img src=""http://www\.gyao\.jp/common/images/neta\.gif"" alt=""ネタバレ"" width=""40"" height=""15"" border=""0""></td>\r?\n)?<td align=""right"" height=""35"" class=""bk10"">(?<Denominator>\d+)人中(?<Numerator>\d+)人が「この番組レビューは参考になる」と評価しています。</td></tr>\r?\n</table></td></tr>\r?\n<tr><td><p class=""marginT5B5"">(?<Body>.*?)</p></td></tr>",
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
		
		private readonly BackgroundImageLoader bgImageLoader = new BackgroundImageLoader(Program.CookieContainer, new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable), DetailViewOptions.ImageTimeoutDefaultValue);
		private readonly BackgroundTextLoader bgTextLoader = new BackgroundTextLoader(Encoding.GetEncoding("Shift_JIS"), Program.CookieContainer, new RequestCachePolicy(RequestCacheLevel.Default), DetailViewOptions.PageTimeoutDefaultValue);
		
		private GContentClass cont;
		private string description1Style = DetailView.DefaultDescription1Style;
		private string description2Style = DetailView.DefaultDescription2Style;
		private string description3Style = DetailView.DefaultDescription3Style;
		private string description4Style = DetailView.DefaultDescription4Style;
		
		public DetailView() {
			InitializeComponent();
		}
		private void DetailView_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			this.wbDescription.DocumentText = "<html><body style='margin:0px;'></body></html>";
			
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
					this.lblReviewSummary.Text = "レビュー取得失敗";
					this.tabpReview.Text = "レビュー(取得失敗)";
					this.lvReview.Items.Clear();
					this.txtReview.Clear();
					this.StatusMessage = string.Format("詳細ビューでの詳細ページ取得エラー: {0}", e.Error.Message);
					return;
				}
				string text = e.Text;
				Match m = DetailView.regexDesc.Match(text);
				if (m.Success) {
					this.wbDescription.Document.Body.InnerHtml = string.Format(@"<p style=""{4}"">{0}</p><p style=""{5}"">{1}</p><p style=""{6}"">{2}</p><p style=""{7}"">{3}</p>",
						m.Groups["Desc1"].Value, m.Groups["Desc2"].Value, m.Groups["Desc3"].Value, m.Groups["Desc4"].Value,
						this.Description1Style, this.Description2Style, this.Description3Style, this.Description4Style);
				} else {
					this.wbDescription.Document.Body.InnerHtml = "エラー: 正規表現にマッチしなかった";
				}
				m = DetailView.regexReviewSummary.Match(text);
				if (m.Success) {
					string aveScore = m.Groups["Summary"].Value;
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
					this.lblReviewSummary.Text = string.IsNullOrEmpty(aveScore) ? "レビューなし" : string.Format("新着分{0}件 総合評価{1}点", this.lvReview.Items.Count, aveScore);
					this.tabpReview.Text = string.IsNullOrEmpty(aveScore) ? "レビュー(0)" : string.Format("レビュー({0};{1})", this.lvReview.Items.Count, aveScore);
				} else {
					this.lblReviewSummary.Text = "レビュー取得失敗";
					this.tabpReview.Text = "レビュー(取得失敗)";
					this.lvReview.Items.Clear();
				}
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
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Description1Style {
			get { return this.description1Style; }
			set {
				if (this.description1Style != value) {
					this.description1Style = value;
					this.OnPropertyChanged("Description1Style");
				}
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Description2Style {
			get { return this.description2Style; }
			set {
				if (this.description2Style != value) {
					this.description2Style = value;
					this.OnPropertyChanged("Description2Style");
				}
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Description3Style {
			get { return this.description3Style; }
			set {
				if (this.description3Style != value) {
					this.description3Style = value;
					this.OnPropertyChanged("Description3Style");
				}
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Description4Style {
			get { return this.description4Style; }
			set {
				if (this.description4Style != value) {
					this.description4Style = value;
					this.OnPropertyChanged("Description4Style");
				}
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
	}

	interface IDetailViewBindingContract : IBindingContract {
		bool LoadImageEnabled { get;set;}
		bool LoadPageEnabled { get;set;}
		int ImageTimeout { get;set;}
		int PageTimeout { get;set;}
		int ImageHeight { get;set;}

		int ReviewListHeight { get;set;}
		int ColWidthNetabare { get;set;}
		int ColWidthScore { get;set;}
		int ColWidthRef { get;set;}
		int ColWidthTitle { get;set;}
		int ColWidthAuthor { get;set;}
		int ColWidthPosted { get;set;}

		string Description1Style { get;set;}
		string Description2Style { get;set;}
		string Description3Style { get;set;}
		string Description4Style { get;set;}

#if PATCH_FOR_INVISIBLE_TEXT_IN_VISTA
		bool EnablePatchForInvisibleTextInVista { get;set;}
#endif
	}
	public sealed class DetailViewOptions : IDetailViewBindingContract {
		internal const int ImageTimeoutDefaultValue = 5000;
		internal const int PageTimeoutDefaultValue = 5000;
		
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

		private string description1Style = DetailView.DefaultDescription1Style;
		[Category("説明文")]
		[DisplayName("説明文1のスタイル")]
		[Description("説明文1の表示に用いる HTML の style 属性を指定します．入力チェックは行っていないので不正な文字列を指定しないでください．変更は再表示後に有効になります．")]
		[DefaultValue(DetailView.DefaultDescription1Style)]
		public string Description1Style {
			get { return this.description1Style; }
			set { this.description1Style = value; }
		}
		private string description2Style = DetailView.DefaultDescription2Style;
		[Category("説明文")]
		[DisplayName("説明文2のスタイル")]
		[Description("説明文2の表示に用いる HTML の style 属性を指定します．入力チェックは行っていないので不正な文字列を指定しないでください．変更は再表示後に有効になります．")]
		[DefaultValue(DetailView.DefaultDescription2Style)]
		public string Description2Style {
			get { return this.description2Style; }
			set { this.description2Style = value; }
		}
		private string description3Style = DetailView.DefaultDescription3Style;
		[Category("説明文")]
		[DisplayName("説明文3のスタイル")]
		[Description("説明文3の表示に用いる HTML の style 属性を指定します．入力チェックは行っていないので不正な文字列を指定しないでください．変更は再表示後に有効になります．")]
		[DefaultValue(DetailView.DefaultDescription3Style)]
		public string Description3Style {
			get { return this.description3Style; }
			set { this.description3Style = value; }
		}
		private string description4Style = DetailView.DefaultDescription4Style;
		[Category("説明文")]
		[DisplayName("説明文4のスタイル")]
		[Description("説明文4の表示に用いる HTML の style 属性を指定します．入力チェックは行っていないので不正な文字列を指定しないでください．変更は再表示後に有効になります．")]
		[DefaultValue(DetailView.DefaultDescription4Style)]
		public string Description4Style {
			get { return this.description4Style;}
			set { this.description4Style = value; }
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
