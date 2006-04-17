using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Text;
using System.Reflection;

namespace Yusen.GExplorer {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("SubmenuSelected")]
	public sealed partial class ToolStripNgFavMenuItem : ToolStripMenuItem {
		public event EventHandler<ContentSelectionRequiredEventArgs> SubmenuSelected;

		public ToolStripNgFavMenuItem() : base("ToolStripNgFavMenuItem"){
			this.InitializeComponent();
			
			this.tsmiAddNgTitle.Click += delegate {
				this.AddUniqueContentPredicatesOnSelectedContents(ContentPredicatesManager.NgManager, ContentAdapter.PropertyNameTitle, "簡易NG");
			};
			this.tsmiAddNgCntId.Click += delegate {
				this.AddUniqueContentPredicatesOnSelectedContents(ContentPredicatesManager.NgManager, ContentAdapter.PropertyNameContentId, "簡易NG");
			};
			this.tsmiAddNgPacId.Click += delegate {
				this.AddUniqueContentPredicatesOnSelectedContents(ContentPredicatesManager.NgManager, ContentAdapter.PropertyNamePackageId, "簡易NG");
			};

			this.tsmiAddFavTitle.Click += delegate {
				this.AddUniqueContentPredicatesOnSelectedContents(ContentPredicatesManager.FavManager, ContentAdapter.PropertyNameTitle, "簡易FAV");
			};
			this.tsmiAddFavPacId.Click += delegate {
				this.AddUniqueContentPredicatesOnSelectedContents(ContentPredicatesManager.FavManager, ContentAdapter.PropertyNamePackageId, "簡易FAV");
			};

			this.tsmiTestNg.Click += delegate {
				this.TestPredicatesOnSelectedContents(ContentPredicatesManager.NgManager, "NGテスト");
			};
			this.tsmiTestFav.Click += delegate {
				this.TestPredicatesOnSelectedContents(ContentPredicatesManager.FavManager, "FAVテスト");
			};
		}

		private IEnumerable<ContentAdapter> GetSelectedContents() {
			EventHandler<ContentSelectionRequiredEventArgs> handler = this.SubmenuSelected;
			if (null == handler) {
				goto notHandled;
			}
			ContentSelectionRequiredEventArgs e = new ContentSelectionRequiredEventArgs();
			handler(this, e);
			if (null == e.Selection) {
				goto notHandled;
			}
			return e.Selection;
		notHandled:
			throw new InvalidOperationException("SubmenuSelectedイベントがハンドルされていなかった．");
		}

		private void AddUniqueContentPredicatesOnSelectedContents(ContentPredicatesManager manager, string caPropertyName, string comment) {
			//値取得
			PropertyInfo pi = typeof(ContentAdapter).GetProperty(caPropertyName);
			List<string> objVals = new List<string>();
			foreach (ContentAdapter ca in this.GetSelectedContents()) {
				string objVal = pi.GetValue(ca, null).ToString();
				if (!objVals.Contains(objVal)) {
					objVals.Add(objVal);
				}
			}
			//プレディケイト作成
			List<ContentPredicate> predicates = new List<ContentPredicate>();
			foreach (string objVal in objVals) {
				predicates.Add(new ContentPredicate(comment, caPropertyName, ContentPredicate.PredicateNameEquals, objVal));
			}
			//追加
			if (predicates.Count > 0) {
				manager.AddRange(predicates);
			}
		}

		private void TestPredicatesOnSelectedContents(ContentPredicatesManager manager, string title) {
			List<ContentPredicate> preds = new List<ContentPredicate>();
			foreach (ContentAdapter cont in this.GetSelectedContents()) {
				foreach (ContentPredicate pred in manager.EnumerateTruePredicatesFor(cont)) {
					if (!preds.Contains(pred)) {
						preds.Add(pred);
					}
				}
			}
			if (0 == preds.Count) {
				MessageBox.Show("選択されたコンテンツに該当する条件はありません．", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			} else {
				string separator = "-------------------------------------------------";
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(separator);
				sb.AppendLine("コメント\t主語\t述語\t目的語\t作成日時");
				sb.AppendLine(separator);
				foreach (ContentPredicate pred in preds) {
					sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", pred.Comment, pred.SubjectName, pred.PredicateName, pred.ObjectValue, pred.CreatedTime.ToString()));
				}
				sb.AppendLine(separator);
				switch (MessageBox.Show("以下の条件に該当します．\n\n" + sb.ToString() + "\n該当する条件をすべて削除しますか？", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) {
					case DialogResult.Yes:
						manager.RemoveAll(preds.Contains);
						break;
				}
			}
		}

	}
}
