using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.UserInterfaces;
using System.Text;

namespace Yusen.GExplorer.OldApp {
	public sealed partial class OldMainForm : BaseForm {
		public OldMainForm() {
			InitializeComponent();
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

		private void MainForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
			this.CheckInvalidContentPredicates(ContentPredicatesManager.NgManager, "NGコンテンツ", false);
			this.CheckInvalidContentPredicates(ContentPredicatesManager.FavManager, "FAVコンテンツ", false);
		}
		private void tsmiNgFavContentsEditor_Click(object sender, EventArgs e) {
			NgFavContentsEditor nfce = new NgFavContentsEditor();
			nfce.Show(this);
		}
	}
}
