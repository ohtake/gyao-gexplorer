using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class MainForm : BaseForm, IMainFormBindingContract, INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		private GContentClass lastSelectedContent;
		
		public MainForm() {
			InitializeComponent();
			
			this.tsmiGenreTab.DropDown = this.genreSelctControl1.GetToolStripDropDown();
			this.tsmiCrawlResultView.DropDown = this.crawlResultView.GetToolStripDropDown();
			this.tsmiPlaylistCollection.DropDown = this.playlistsView1.GetPlaylistCollectionToolStripDropDown();
			this.tsmiPlaylist.DropDown = this.playlistsView1.GetPlaylistToolStripDropDown();
			this.tsmiDetailView.DropDown = this.detailView1.GetToolStripDropDown();

			if (base.DesignMode) return;
			this.Text = Program.ApplicationName;
		}

		private void MainForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			this.tsslMessage.Text = string.Empty;
			Program.ProgramSerializationProgress += new ProgressChangedEventHandler(Program_ProgramSerializationProgress);

			if (null == Program.RootOptions) return;

			Program.RootOptions.MainFormOptions.ApplyFormBaseOptionsAndTrackValues(this);
			Program.RootOptions.MainFormOptions.NeutralizeUnspecificValues(this);
			BindingContractUtility.BindAllProperties<MainForm, IMainFormBindingContract>(this, Program.RootOptions.MainFormOptions);
			this.genreSelctControl1.LoadOpenTabs(Program.RootOptions.MainFormOptions.GenreSelectControlOptions);
			this.crawlResultView.BindToOptions(Program.RootOptions.MainFormOptions.CrawlResultViewOptions);
			this.playlistsView1.BindToOptions(Program.RootOptions.MainFormOptions.PlaylistsViewOptions);
			this.detailView1.BindToOptions(Program.RootOptions.MainFormOptions.DetailViewOptions);
		}
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			switch (e.CloseReason) {
				case CloseReason.UserClosing:
					if (null != Program.PlaylistsManager.CurrentContent) {
						switch (MessageBox.Show("再生中ですがアプリケーションを終了しますか？", Program.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
							case DialogResult.No:
								e.Cancel = true;
								break;
						}
					}
					break;
			}
		}
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
			this.tsmiAbortCrawl.PerformClick();
			
			this.Enabled = false;
			this.genreSelctControl1.StoreOpenTabs(Program.RootOptions.MainFormOptions.GenreSelectControlOptions);
			Program.SerializeSettings();
		}

		private void Program_ProgramSerializationProgress(object sender, ProgressChangedEventArgs e) {
			this.tspbProgress.Value = e.ProgressPercentage;
			this.tsslMessage.Text = e.UserState as string;
			Application.DoEvents();
		}
		
		private void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (null != handler) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		#region ジャンルタブ
		private void genreSelctControl1_CrawlResultSelected(object sender, EventArgs e) {
			this.crawlResultView.ViewCrawlResult(this.genreSelctControl1.SelectedCrawlResult);
		}
		private void genreSelctControl1_RequiredHeightChanged(object sender, EventArgs e) {
			this.splitContainer3.SplitterDistance = this.genreSelctControl1.RequiredHeight;
			this.splitContainer3.Panel1.ResumeLayout();
		}
		private void genreSelctControl1_StatusMessageChanged(object sender, EventArgs e) {
			this.SetTemporalStatusMessage(this.genreSelctControl1.StatusMessage);
		}
		private void genreSelctControl1_CrawlProgressChanged(object sender, ProgressChangedEventArgs e) {
			ICrawlProgressState state = e.UserState as ICrawlProgressState;
			this.tspbProgress.Value = state.TotalPercentage;
			this.tsslMessage.Text = state.TotalMessage;
		}
		private void genreSelctControl1_CrawlStarted(object sender, EventArgs e) {
			this.tsmiAbortCrawl.Enabled = true;
		}
		private void genreSelctControl1_CrawlEnded(object sender, EventArgs e) {
			this.tspbProgress.Value = 0;
			this.tsmiAbortCrawl.Enabled = false;
		}
		#endregion

		#region メインメニュー
		private void tsmiCreateNewPlaylist_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "プレイリストの新規作成";
			this.inputBoxDialog1.Message = "作成するプレイリストの名称を入力してください．";
			this.inputBoxDialog1.Input = string.Empty;
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					Program.PlaylistsManager.CreatePlaylistNamedAs(this.inputBoxDialog1.Input);
					break;
			}
		}
		private void tsmiBrowseTopPage_Click(object sender, EventArgs e) {
			Program.BrowsePage(GUriBuilder.TopPageUri);
		}
		private void tsmiBrowsePackagePage_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "パッケージIDを指定して開く";
			this.inputBoxDialog1.Message = "パッケージIDを入力してください．";
			this.inputBoxDialog1.Input = "pac0000000";
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					Program.BrowsePage(GUriBuilder.CreatePackagePageUri(this.inputBoxDialog1.Input));
					break;
			}
		}
		private void tsmiBrowseContentPage_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "コンテンツIDを指定して開く";
			this.inputBoxDialog1.Message = "コンテンツIDを入力してください．";
			this.inputBoxDialog1.Input = "cnt0000000";
			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					Program.BrowsePage(GUriBuilder.CreateContentDetailUri(this.inputBoxDialog1.Input));
					break;
			}
		}
		private void tsmiQuit_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void tsmiSearchLivedoorGyaO_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "livedoor動画でGyaO検索";
			this.inputBoxDialog1.Message = "検索する語句を入力してください．";
			this.inputBoxDialog1.Input = string.Empty;

			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					string query = this.inputBoxDialog1.Input;
					if (string.IsNullOrEmpty(query)) return;
					Program.BrowsePage(GUriBuilder.CreateLivedoorVideoGyaoSearchUri(query));
					break;
			}
		}
		private void tsmiCacheViewer_Click(object sender, EventArgs e) {
			Program.ShowCacheViewerForm();
		}
		private void tsmiRuleEditForm_Click(object sender, EventArgs e) {
			Program.ShowContentClassificationRuleEditForm();
		}
		private void tsmiExternalCommandsEditor_Click(object sender, EventArgs e) {
			Program.ShowExternalCommandsEditForm();
		}
		private void tsmiOptionsForm_Click(object sender, EventArgs e) {
			Program.ShowOptionsForm();
		}
		private void tsmiAbortCrawl_Click(object sender, EventArgs e) {
			this.genreSelctControl1.RequestCrawlCancellation();
		}
		#endregion

		#region IMainFormBindingContract Members
		public int CrawlResultViewWidth {
			get { return this.splitContainer1.SplitterDistance; }
			set {
				this.splitContainer1.SplitterDistance = value;
				this.OnPropertyChanged("CrawlResultViewWidth");
			}
		}
		public int CrawlResultViewHeight {
			get { return this.splitContainer2.SplitterDistance; }
			set {
				this.splitContainer2.SplitterDistance = value;
				this.OnPropertyChanged("CrawlResultViewHeight");
			}
		}
		#endregion

		private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e) {
			this.OnPropertyChanged("CrawlResultViewWidth");
		}
		private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e) {
			this.OnPropertyChanged("CrawlResultViewHeight");
		}
		
		public void AddVirtualGenre(IVirtualGenre vgenre) {
			this.genreSelctControl1.AddAndSelectVirtualGenre(vgenre);
		}
		
		private void crawlResultView_LastSelectedContentChanged(object sender, EventArgs e) {
			this.SetLastSelectContentDelayed(this.crawlResultView.LastSelectedContent);
		}
		private void playlistsView1_LastSelectedContentChanged(object sender, EventArgs e) {
			this.SetLastSelectContentDelayed(this.playlistsView1.LastSelectedContent);
		}
		private void SetLastSelectContentDelayed(GContentClass cont) {
			this.lastSelectedContent = cont;
			this.timerContentSelect.Start();
		}
		private void timerContentSelect_Tick(object sender, EventArgs e) {
			this.timerContentSelect.Stop();
			this.detailView1.ViewDetail(this.lastSelectedContent);
		}
		
		private void SetTemporalStatusMessage(string message) {
			this.tsslMessage.Text = message;
			this.timerMessage.Stop();
			this.timerMessage.Start();
		}
		private void timerMessage_Tick(object sender, EventArgs e) {
			this.timerMessage.Stop();
			this.tsslMessage.Text = string.Empty;
		}
		
		private void detailView1_StatusMessageChanged(object sender, EventArgs e) {
			this.SetTemporalStatusMessage(this.detailView1.StatusMessage);
		}

		private void crawlResultView_LastSelectingPlaylistChanged(object sender, EventArgs e) {
			this.playlistsView1.SelectPlaylist(this.crawlResultView.LastSelectingPlaylist);
		}

	}
	
	interface IMainFormBindingContract : IBindingContract {
		/// <summary>ただし負数で未定義とする．</summary>
		int CrawlResultViewWidth { get;set;}
		/// <summary>ただし負数で未定義とする．</summary>
		int CrawlResultViewHeight { get;set;}
	}
	public sealed class MainFormOptions : FormOptionsBase, IMainFormBindingContract {
		public MainFormOptions() {
		}
		
		#region IMainFormBindingContract Members
		private int crawlResultViewWidth = -1;
		[Category("スプリッタの位置")]
		[DisplayName("クロール結果ビューの幅")]
		[Description("クロール結果ビューの幅を指定します．")]
		[DefaultValue(-1)]
		public int CrawlResultViewWidth {
			get { return this.crawlResultViewWidth;}
			set { this.crawlResultViewWidth = value;}
		}

		private int crawlResultViewHeight = -1;
		[Category("スプリッタの位置")]
		[DisplayName("ジャンルタブとクロール結果ビューの高さ")]
		[Description("ジャンルタブとクロール結果ビューの高さを指定します．")]
		[DefaultValue(-1)]
		public int CrawlResultViewHeight {
			get { return this.crawlResultViewHeight; }
			set { this.crawlResultViewHeight = value; }
		}
		#endregion

		private GenreSelectControlOptions genreSelectControlOptions = new GenreSelectControlOptions();
		[Browsable(false)]
		[SubOptions("ジャンルタブ", "ジャンルタブに関する設定")]
		public GenreSelectControlOptions GenreSelectControlOptions {
			get { return this.genreSelectControlOptions; }
			set { this.genreSelectControlOptions = value; }
		}

		private CrawlResultViewOptions crawlResultViewOptions = new CrawlResultViewOptions();
		[Browsable(false)]
		[SubOptions("クロール結果ビュー", "クロール結果ビューに関する設定")]
		public CrawlResultViewOptions CrawlResultViewOptions {
			get { return this.crawlResultViewOptions; }
			set { this.crawlResultViewOptions = value; }
		}

		private PlaylistsViewOptions playlistsViewOptions = new PlaylistsViewOptions();
		[Browsable(false)]
		[SubOptions("プレイリストビュー", "プレイリストビューに関する設定")]
		public PlaylistsViewOptions PlaylistsViewOptions {
			get { return this.playlistsViewOptions; }
			set { this.playlistsViewOptions = value; }
		}

		private DetailViewOptions detailViewOptions = new DetailViewOptions();
		[Browsable(false)]
		[SubOptions("詳細ビュー", "詳細ビューに関する設定")]
		public DetailViewOptions DetailViewOptions {
			get { return this.detailViewOptions; }
			set { this.detailViewOptions = value; }
		}
		
		internal void NeutralizeUnspecificValues(MainForm mainForm) {
			if (this.CrawlResultViewWidth < 0) this.CrawlResultViewWidth = mainForm.CrawlResultViewWidth;
			if (this.CrawlResultViewHeight < 0) this.CrawlResultViewHeight = mainForm.CrawlResultViewHeight;
		}
	}
}
