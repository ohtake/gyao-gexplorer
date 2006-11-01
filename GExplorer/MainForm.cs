using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Yusen.GExplorer.OldCrawler;
using System.Xml.Serialization;
using System.Text;
using System.Web;
using Yusen.GExplorer.OldApp;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer {
	public sealed partial class MainForm : FormSettingsBase, IFormWithNewSettings<MainForm.MainFormSettings> {
		public sealed class MainFormSettings : INewSettings<MainFormSettings> {
			private readonly MainForm owner;
			
			public MainFormSettings() : this(null) {
			}
			internal MainFormSettings(MainForm owner) {
				this.owner = owner;
				this.formSettingsBaseSettings = new FormSettingsBaseSettings(owner);
				if (this.HasOwner) {
					this.crawlResultViewSettings = new CrawlResultView.CrawlResultViewSettings(owner.crawlResultView1);
					this.contentDetailViewSettings = new ContentDetailView.ContentDetailViewSettings(owner.contentDetailView1);
				} else {
					this.crawlResultViewSettings = new CrawlResultView.CrawlResultViewSettings();
					this.contentDetailViewSettings = new ContentDetailView.ContentDetailViewSettings();
				}
			}
			
			[Browsable(false)]
			[XmlIgnore]
			private bool HasOwner {
				get { return null != this.owner; }
			}
			
			[ReadOnly(true)]
			[Category("位置とサイズ")]
			[DisplayName("フォームの基本設定")]
			[Description("フォームの基本的な設定です．")]
			public FormSettingsBaseSettings FormSettingsBaseSettings {
				get { return this.formSettingsBaseSettings; }
				set { this.FormSettingsBaseSettings.ApplySettings(value); }
			}
			private readonly FormSettingsBaseSettings formSettingsBaseSettings;
			
			[Category("位置とサイズ")]
			[DisplayName("クロール結果ビューの幅")]
			[Description("クロール結果ビューの幅をピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ListViewWidth {
				get {
					if (this.HasOwner) return this.owner.scListsAndDetail.SplitterDistance;
					else return this.listViewWidth;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.scListsAndDetail.SplitterDistance = value.Value;
					else this.listViewWidth = value;
				}
			}
			private int? listViewWidth;

			[Category("位置とサイズ")]
			[DisplayName("クロール結果ビューの高さ")]
			[Description("クロール結果ビューの高さをピクセルで指定します．")]
			[DefaultValue(null)]
			public int? ListViewHeight {
				get {
					if (this.HasOwner) return this.owner.scLists.SplitterDistance;
					else return this.listViewHeight;
				}
				set {
					if (this.HasOwner && value.HasValue) this.owner.scLists.SplitterDistance = value.Value;
					else this.listViewHeight = value;
				}
			}
			private int? listViewHeight;

			[Category("動作")]
			[DisplayName("タブ切り替えでフォーカス移動")]
			[Description("ジャンルタブでタブを切り替えるとクロール結果ビューにフォーカスを移動させます．")]
			[DefaultValue(true)]
			public bool FocusOnResultAfterGenreChanged {
				get { return this.focusOnResultAfterGenreChanged; }
				set { this.focusOnResultAfterGenreChanged = value; }
			}
			private bool focusOnResultAfterGenreChanged = true;

			[Browsable(false)]
			public CrawlResultView.CrawlResultViewSettings CrawlResultViewSettings {
				get { return this.crawlResultViewSettings; }
				set { this.crawlResultViewSettings.ApplySettings(value); }
			}
			private readonly CrawlResultView.CrawlResultViewSettings crawlResultViewSettings;

			[Browsable(false)]
			public ContentDetailView.ContentDetailViewSettings ContentDetailViewSettings {
				get { return this.contentDetailViewSettings; }
				set { this.contentDetailViewSettings.ApplySettings(value); }
			}
			private readonly ContentDetailView.ContentDetailViewSettings contentDetailViewSettings;

			#region INewSettings<MainFormSettings> Members
			public void ApplySettings(MainFormSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}
		
		private static MainForm instance = null;
		public static MainForm Instance {
			get {
				if (!MainForm.HasInstance) {
					MainForm.instance = new MainForm();
				}
				return MainForm.instance;
			}
		}
		public static bool HasInstance {
			get { return null != MainForm.instance && !MainForm.instance.IsDisposed; }
		}
		
		private ContentAdapter seletedCont = null;

		private MainFormSettings settings;

		private MainForm() {
			InitializeComponent();
		}
		
		private void ClearStatusBarInfo() {
			if (this.InvokeRequired) {
				this.Invoke(new MethodInvoker(delegate {
					this.ClearStatusBarInfo();
				}));
			} else {
				this.tspbCrawl.Value = 0;
				this.tsslCrawl.Text = string.Empty;
			}
		}
		private void SetStatutBarTextTemporary(string text) {
			if(this.InvokeRequired) {
				this.Invoke(new ParameterizedThreadStart(delegate(object param) {
					this.SetStatutBarTextTemporary(param as string);
				}), text);
			} else {
				this.timerClearStatusText.Stop();
				this.tsslCrawl.Text = text;
				this.timerClearStatusText.Start();
			}
		}
		
		private void CheckInvalidContentPredicates(ContentPredicatesManager manager, string cpName, bool showResultOnSuccess) {
			string title = "妥当でない" + cpName;
			ContentPredicate[] preds = manager.GetInvalidPredicates();
			if (preds.Length > 0) {
				string separator = "-------------------------------------------------";
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(separator);
				sb.AppendLine("コメント\t主語\t述語\t目的語\t作成日時");
				sb.AppendLine(separator);
				foreach (ContentPredicate cp in preds) {
					sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", cp.Comment, cp.SubjectName, cp.PredicateName, cp.ObjectValue, cp.CreatedTime));
				}
				sb.AppendLine(separator);
				switch (MessageBox.Show(string.Format("妥当でない{0}が {1} 個見つかりました．\n\n{2}\n削除しますか？", cpName, preds.Length, sb.ToString()), title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)) {
					case DialogResult.Yes:
						int removeCnt = manager.RemoveInvalidPredicates();
						MessageBox.Show(string.Format("妥当でなかった{0}を {1} 個削除しました．", cpName, removeCnt), title, MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case DialogResult.No:
						MessageBox.Show(string.Format("妥当でない{0}を保持したまま判定処理が行われるとエラーが起こりえます．\n{0}エディタで妥当でない{0}の削除を行ってください．", cpName), title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
				}
			} else if (showResultOnSuccess) {
				MessageBox.Show(string.Format("妥当でない{0}はありませんでした．", cpName), title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		public string FilenameForSettings {
			get { return @"MainFormSettings.xml"; }
		}

		private void MainForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			this.settings = new MainFormSettings(this);
			Utility.LoadSettingsAndEnableSaveOnClosedNew(this);
			
			this.ClearStatusBarInfo();
			this.CheckInvalidContentPredicates(ContentPredicatesManager.NgManager, "NGコンテンツ", false);
			this.CheckInvalidContentPredicates(ContentPredicatesManager.FavManager, "FAVコンテンツ", false);
		}
		
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			switch (e.CloseReason) {
				case CloseReason.UserClosing:
					if (PlayList.Instance.HasCurrentContent) {
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
			this.Enabled = false;
		}

		private void crawlResultView1_ContentSelectionChanged(object sender, ContentSelectionChangedEventArgs e) {
			if (e.IsSelected) {
				this.seletedCont = e.Content;
				this.timerViewDetail.Start();
			}
		}
		private void contentDetailView1_ImageLoadError(object sender, ImageLoadErrorEventArgs e) {
			this.SetStatutBarTextTemporary("詳細ビューでの画像読み込みエラー: " + e.Exception.Message);
		}
		
		private void tsmiQuit_Click(object sender, EventArgs e) {
			this.Close();
		}
		private void tsmiUserCommandsEditor_Click(object sender, EventArgs e) {
			UserCommandsEditor uce = UserCommandsEditor.Instance;
			uce.Owner = this;
			uce.Show();
			uce.Focus();
		}
		private void tsmiNgFavContentsEditor_Click(object sender, EventArgs e) {
			NgFavContentsEditor nfce = NgFavContentsEditor.Instance;
			nfce.Owner = this;
			nfce.Show();
			nfce.Focus();
		}
		private void tsmiSearchLivedoorGyaO_Click(object sender, EventArgs e) {
			this.inputBoxDialog1.Title = "livedoor動画でGyaO検索";
			this.inputBoxDialog1.Message = "検索する語句を入力してください．";
			this.inputBoxDialog1.Input = string.Empty;

			switch (this.inputBoxDialog1.ShowDialog()) {
				case DialogResult.OK:
					string query = this.inputBoxDialog1.Input;
					if (string.IsNullOrEmpty(query)) return;
					
					Utility.Browse(GUriBuilder.CreateLivedoorVideoGyaoSearchUri(query));
					break;
			}
		}
		private void tsmiSerializeSettingsNow_Click(object sender, EventArgs e) {
			switch (MessageBox.Show("アプリケーションを終了せずに終了処理の真似事をします．\nあまりお勧めできませんが実行しますか？", "設定ファイルとキャッシュの強制的な書き出し", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) {
				case DialogResult.Yes:
					break;
				default:
					return;
			}
			
			this.Enabled = false;
			this.SetStatutBarTextTemporary("各フォームごとの設定ファイルを書き出し中．");
			if (UserCommandsEditor.HasInstance) Utility.SerializeSettings(UserCommandsEditor.Instance.FilenameForSettings, UserCommandsEditor.Instance.Settings);
			if (NgFavContentsEditor.HasInstance) Utility.SerializeSettings(NgFavContentsEditor.Instance.FilenameForSettings, NgFavContentsEditor.Instance.Settings);
			Utility.SerializeSettings(this.FilenameForSettings, this.Settings);
			
			this.Enabled = true;
			
			this.ClearStatusBarInfo();
			this.SetStatutBarTextTemporary("設定ファイルとキャッシュの強制的な書き出しを完了しました．");
		}


		private void timerViewDetail_Tick(object sender, EventArgs e) {
			this.timerViewDetail.Stop();
			this.contentDetailView1.Content = this.seletedCont;
		}

		private void timerClearStatusText_Tick(object sender, EventArgs e) {
			this.timerClearStatusText.Stop();
			this.tsslCrawl.Text = string.Empty;
		}

		#region IHasNewSettings<MainFormSettings> Members
		public MainForm.MainFormSettings Settings {
			get { return this.settings; }
		}
		#endregion

	}
}
