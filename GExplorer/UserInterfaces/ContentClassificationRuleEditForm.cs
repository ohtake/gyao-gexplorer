using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.Utilities;

namespace Yusen.GExplorer.UserInterfaces {
	partial class ContentClassificationRuleEditForm : BaseForm {
		private sealed class RuleListViewItem : ListViewItem {
			ContentClassificationRule rule;
			public RuleListViewItem(ContentClassificationRule rule)
				: base(new string[] { rule.Comment, rule.Subject, rule.Predicate.ToString(), rule.Object, rule.Destination, rule.Created.ToString(), rule.LastApplied.ToString() }) {
				this.rule = rule;
			}
			public ContentClassificationRule Rule {
				get { return this.rule; }
			}
		}

		private static readonly Comparison<ContentClassificationRule>[] comparisons = new Comparison<ContentClassificationRule>[]{
			new Comparison<ContentClassificationRule>(delegate(ContentClassificationRule x, ContentClassificationRule y){
				return x.Comment.CompareTo(y.Comment);
			}),
			new Comparison<ContentClassificationRule>(delegate(ContentClassificationRule x, ContentClassificationRule y){
				return x.Subject.CompareTo(y.Subject);
			}),
			new Comparison<ContentClassificationRule>(delegate(ContentClassificationRule x, ContentClassificationRule y){
				return x.Predicate.CompareTo(y.Predicate);
			}),
			new Comparison<ContentClassificationRule>(delegate(ContentClassificationRule x, ContentClassificationRule y){
				return x.Object.CompareTo(y.Object);
			}),
			new Comparison<ContentClassificationRule>(delegate(ContentClassificationRule x, ContentClassificationRule y){
				return x.Destination.CompareTo(y.Destination);
			}),
			new Comparison<ContentClassificationRule>(delegate(ContentClassificationRule x, ContentClassificationRule y){
				return x.Created.CompareTo(y.Created);
			}),
			new Comparison<ContentClassificationRule>(delegate(ContentClassificationRule x, ContentClassificationRule y){
				return x.LastApplied.CompareTo(y.LastApplied);
			}),
		};

		private ContentClassificationRule lastSelectedRule = null;
		private StackableComparisonsComparer<ContentClassificationRule> comparer = new StackableComparisonsComparer<ContentClassificationRule>();
		
		public ContentClassificationRuleEditForm() {
			InitializeComponent();
		}
		
		private void ContentClassificationRuleEditForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;

			List<string> dropdowns = new List<string>();
			foreach (PropertyInfo pi in typeof(GContentClass).GetProperties()) {
				dropdowns.Add(pi.Name);
			}
			dropdowns.Sort();
			this.cmbSubject.Items.AddRange(dropdowns.ToArray());
			dropdowns.Clear();
			
			foreach (StringPredicate sp in Enum.GetValues(typeof(StringPredicate))) {
				dropdowns.Add(sp.ToString());
			}
			this.cmbPredicate.Items.AddRange(dropdowns.ToArray());
			dropdowns.Clear();

			if (Program.PlaylistsManager == null) return;
			Program.PlaylistsManager.PlaylistsManagerChanged += new EventHandler(PlaylistsManager_PlaylistsManagerChanged);
			this.Disposed += delegate {
				Program.PlaylistsManager.PlaylistsManagerChanged -= new EventHandler(PlaylistsManager_PlaylistsManagerChanged);
			};
			this.UpdateDestinationDropDownItems();

			if (Program.ContentClassificationRulesManager == null) return;
			Program.ContentClassificationRulesManager.ContentCllasificationRulesManagerChanged += new EventHandler(ContentClassificationRulesManager_ContentCllasificationRulesManagerChanged);
			this.Disposed += delegate {
				Program.ContentClassificationRulesManager.ContentCllasificationRulesManagerChanged -= new EventHandler(ContentClassificationRulesManager_ContentCllasificationRulesManagerChanged);
			};
			this.UpdateRuleList();
			
			if (Program.RootOptions == null) return;
			Program.RootOptions.ContentClassificationRuleEditFormOptions.ApplyFormBaseOptionsAndTrackValues(this);
		}

		private void UpdateRuleList() {
			this.lvRules.BeginUpdate();
			this.lvRules.Items.Clear();
			List<RuleListViewItem> items = new List<RuleListViewItem>();
			foreach (ContentClassificationRule rule in Program.ContentClassificationRulesManager) {
				items.Add(new RuleListViewItem(rule));
			}
			this.lvRules.Items.AddRange(items.ToArray());
			this.lvRules.EndUpdate();
		}

		private void ContentClassificationRulesManager_ContentCllasificationRulesManagerChanged(object sender, EventArgs e) {
			this.UpdateRuleList();
		}

		private void UpdateDestinationDropDownItems() {
			this.cmbDestination.BeginUpdate();
			this.cmbDestination.Items.Clear();
			List<string> pls = new List<string>();
			foreach (Playlist pl in Program.PlaylistsManager) {
				pls.Add(pl.Name);
			}
			this.cmbDestination.Items.AddRange(pls.ToArray());
			this.cmbDestination.EndUpdate();
		}

		private void PlaylistsManager_PlaylistsManagerChanged(object sender, EventArgs e) {
			this.UpdateDestinationDropDownItems();
		}

		private ContentClassificationRule[] GetSelectedRules() {
			List<ContentClassificationRule> rules = new List<ContentClassificationRule>();
			foreach (RuleListViewItem rlvi in this.lvRules.SelectedItems) {
				rules.Add(rlvi.Rule);
			}
			return rules.ToArray();
		}

		private void btnAdd_Click(object sender, EventArgs e) {
			ContentClassificationRule rule = new ContentClassificationRule(
				this.txtComment.Text,
				this.cmbSubject.Text,
				(StringPredicate)Enum.Parse(typeof(StringPredicate), this.cmbPredicate.Text),
				this.txtObject.Text,
				this.cmbDestination.Text);
			Program.ContentClassificationRulesManager.Add(rule);
		}

		private void lvRules_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if (e.IsSelected) {
				this.lastSelectedRule = (e.Item as RuleListViewItem).Rule;
				this.timerSelectionDelay.Start();
			}
		}

		private void lvRules_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.comparer.PushComparison(ContentClassificationRuleEditForm.comparisons[e.Column]);
			Program.ContentClassificationRulesManager.Sort(this.comparer);
		}

		private void timerSelectionDelay_Tick(object sender, EventArgs e) {
			this.timerSelectionDelay.Stop();
			this.txtComment.Text = this.lastSelectedRule.Comment;
			this.cmbSubject.Text = this.lastSelectedRule.Subject;
			this.cmbPredicate.Text = this.lastSelectedRule.Predicate.ToString();
			this.txtObject.Text = this.lastSelectedRule.Object;
			this.cmbDestination.Text = this.lastSelectedRule.Destination;
		}

		private void tsmiRemoveRule_Click(object sender, EventArgs e) {
			ContentClassificationRule[] rules = this.GetSelectedRules();
			Program.ContentClassificationRulesManager.BeginUpdate();
			foreach (ContentClassificationRule rule in rules) {
				Program.ContentClassificationRulesManager.Remove(rule);
			}
			Program.ContentClassificationRulesManager.EndUpdate();
		}
	}

	public sealed class ContentClassificationRuleEditFormOptions : FormOptionsBase {
	}
}

