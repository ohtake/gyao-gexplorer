using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.AppCore;
using System.Text;

namespace Yusen.GExplorer.UserInterfaces {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("SubmenuSelected")]
	public sealed partial class ToolStripClassificationRuleMenuItem : ToolStripMenuItem {
		public event EventHandler SubmenuSelected;
		
		private Action<IEnumerable<GContentClass>> lastSelectedAction;
		
		public ToolStripClassificationRuleMenuItem()
			: base("ToolStripClassificationRuleMenuItem") {
			InitializeComponent();
			
			this.tsmiNgTitle.Click += new EventHandler(tsmiNgTitle_Click);
			this.tspmiFavTitle.PlaylistSelected += new EventHandler(tspmiFavTitle_PlaylistSelected);
			this.tsmiTestRules.Click += new EventHandler(tsmiTestRules_Click);
		}
		
		private Action<IEnumerable<GContentClass>> CreateActionOnAddingByTitle(string destination) {
			return new Action<IEnumerable<GContentClass>>(delegate(IEnumerable<GContentClass> conts) {
				List<string> titles = new List<string>();
				foreach (GContentClass cont in conts) {
					if (!titles.Contains(cont.Title)) {
						titles.Add(cont.Title);
					}
				}
				Program.ContentClassificationRulesManager.BeginUpdate();
				foreach (string title in titles) {
					ContentClassificationRule rule = new ContentClassificationRule("簡易追加", ContentClassificationRule.SubjectTitle, StringPredicate.StringEquals, title, destination);
					Program.ContentClassificationRulesManager.Add(rule);
				}
				Program.ContentClassificationRulesManager.EndUpdate();
			});
		}
		private void TestRules(IEnumerable<GContentClass> conts) {
			List<ContentClassificationRule> rules = new List<ContentClassificationRule>();
			foreach (GContentClass cont in conts) {
				foreach (ContentClassificationRule rule in Program.ContentClassificationRulesManager.GetAppliableRulesFor(cont)) {
					if (!rules.Contains(rule)) {
						rules.Add(rule);
					}
				}
			}
			if (0 == rules.Count) {
				MessageBox.Show("選択されたコンテンツに該当するルールはありません．", "仕分けテスト", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} else {
				string separator = "-------------------------------------------------";
				StringBuilder sb = new StringBuilder();
				sb.AppendLine(separator);
				sb.AppendLine("コメント\t主語\t述語\t目的語\t仕分け先\t作成日時");
				sb.AppendLine(separator);
				foreach (ContentClassificationRule rule in rules) {
					sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", rule.Comment, rule.Subject, rule.Predicate, rule.Object, rule.Destination, rule.Created));
				}
				sb.AppendLine(separator);
				switch (MessageBox.Show("以下のルールに該当します．\n\n" + sb.ToString() + "\n該当するルールをすべて削除しますか？", "仕分けテスト", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) {
					case DialogResult.Yes:
						Program.ContentClassificationRulesManager.BeginUpdate();
						foreach (ContentClassificationRule rule in rules) {
							Program.ContentClassificationRulesManager.Remove(rule);
						}
						Program.ContentClassificationRulesManager.EndUpdate();
						break;
				}
			}
		}
		
		private void tsmiNgTitle_Click(object sender, EventArgs e) {
			this.OnSubmenuSelected(this.CreateActionOnAddingByTitle(string.Empty));
		}
		private void tspmiFavTitle_PlaylistSelected(object sender, EventArgs e) {
			string plName = this.tspmiFavTitle.LastSelectedPlaylist.Name;
			this.OnSubmenuSelected(this.CreateActionOnAddingByTitle(plName));
		}
		private void tsmiTestRules_Click(object sender, EventArgs e) {
			this.OnSubmenuSelected(this.TestRules);
		}
		
		private void OnSubmenuSelected(Action<IEnumerable<GContentClass>> action) {
			this.lastSelectedAction = action;
			EventHandler handler = this.SubmenuSelected;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		public Action<IEnumerable<GContentClass>> LastSelectedAction {
			get { return this.lastSelectedAction; }
		}
	}
}
