using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Yusen.GExplorer {
	public partial class ContentPredicatesEditControl : UserControl {
		private ContentPredicatesManager manager;
		private volatile ContentPredicate selPred = null;
		private ListViewItemStackableComparer comparer = new ListViewItemStackableComparer();
		
		public ContentPredicatesEditControl() {
			InitializeComponent();
			
			List<string> subjectNames = new List<string>();
			foreach (PropertyInfo pi in typeof(ContentAdapter).GetProperties()) {
				object[] attribs = pi.GetCustomAttributes(typeof(BrowsableAttribute), false);
				if (attribs.Length > 0 && !(attribs[0] as BrowsableAttribute).Browsable) {
					continue;
				}
				subjectNames.Add(pi.Name);
			}
			subjectNames.Sort();
			this.comboSubject.Items.AddRange(subjectNames.ToArray());

			this.comboPredicate.BeginUpdate();
			foreach (string predName in ContentPredicate.PredicateNames) {
				this.comboPredicate.Items.Add(predName);
			}
			this.comboPredicate.EndUpdate();
		}

		internal void SetManager(ContentPredicatesManager manager) {
			if (null != this.manager) {
				throw new InvalidOperationException();
			}
			this.manager = manager;

			this.manager.PredicatesChanged += new EventHandler(this.manager_PredicatesChanged);
			this.manager.LastTrueChanged += new EventHandler(this.manager_LastTrueChanged);
			this.Disposed += delegate {
				this.manager.PredicatesChanged -= new EventHandler(this.manager_PredicatesChanged);
				this.manager.LastTrueChanged -= new EventHandler(this.manager_LastTrueChanged);
			};

			this.UpdateView();
		}

		private void UpdateView() {
			this.listview1.BeginUpdate();
			this.listview1.Items.Clear();
			foreach (ContentPredicate cp in this.manager) {
				ListViewItem lvi = new ListViewItem(
					new string[]{
						cp.Comment, cp.SubjectName, cp.PredicateName, cp.ObjectValue,
						cp.CreatedTime.ToString(), cp.LastTrueTime.ToString()});
				lvi.Tag = cp;
				this.listview1.Items.Add(lvi);
			}
			this.listview1.EndUpdate();

			int cntAll = this.manager.Count;
			int cntAccCntId = this.manager.AcceralatedCntIdPredicatesCount;
			int cntAccPacId = this.manager.AcceralatedPacIdPredicatesCount;
			int cntAccTitle = this.manager.AcceralatedTitlePredicatesCount;
			int cntNoneAcc = this.manager.NonAcceralatedPredicatesCount;
			int cntDiff = cntAll - cntAccCntId - cntAccPacId - cntAccTitle - cntNoneAcc;
			this.lblCount.Text =
				string.Format(
					"総数[{0}] = 高速コンテンツID[{1}] + 高速パッケージID[{2}] + 高速タイトル[{3}] + 非高速化[{4}] + 食い違い数[{5}]",
					cntAll, cntAccCntId, cntAccPacId, cntAccTitle, cntNoneAcc, cntDiff);
		}

		private void manager_PredicatesChanged(object sender, EventArgs e) {
			this.UpdateView();
		}
		private void manager_LastTrueChanged(object sender, EventArgs e) {
			this.timerLastTrue.Start();
		}

		private void btnAdd_Click(object sender, EventArgs e) {
			ContentPredicate cp = null;
			try {
				cp = new ContentPredicate(
					this.txtComment.Text,
					this.comboSubject.Text,
					this.comboPredicate.Text,
					this.txtObject.Text);
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "条件の追加", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			this.manager.Add(cp);
		}

		private void listview1_ColumnClick(object sender, ColumnClickEventArgs e) {
			this.comparer.StackColumnIndex(e.Column);
			List<ListViewItem> lvis = new List<ListViewItem>();
			foreach (ListViewItem lvi in this.listview1.Items) {
				lvis.Add(lvi);
			}
			lvis.Sort(this.comparer);
			this.manager.SetAll(
				lvis.ConvertAll<ContentPredicate>(delegate(ListViewItem lvi) {
					return lvi.Tag as ContentPredicate;
				}));
		}

		private void listview1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
			if (e.IsSelected) {
				this.selPred = e.Item.Tag as ContentPredicate;
				this.timerItemSelect.Start();
			}
		}

		private void listview1_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.A:
					if (Keys.None != (Control.ModifierKeys & Keys.Control)) {
						this.listview1.BeginUpdate();
						foreach (ListViewItem lvi in this.listview1.Items) {
							lvi.Selected = true;
						}
						this.listview1.EndUpdate();
					}
					break;
				case Keys.Escape:
					this.listview1.BeginUpdate();
					foreach (ListViewItem lvi in this.listview1.Items) {
						lvi.Selected = false;
					}
					this.listview1.EndUpdate();
					break;
			}
		}
		private void tsmiDelete_Click(object sender, EventArgs e) {
			List<ContentPredicate> selectedPredicates = new List<ContentPredicate>();
			foreach (ListViewItem selItem in this.listview1.SelectedItems) {
				selectedPredicates.Add(selItem.Tag as ContentPredicate);
			}
			this.manager.RemoveAll(new Predicate<ContentPredicate>(
				delegate(ContentPredicate cp) {
					return selectedPredicates.Contains(cp);
				}
			));
		}

		private void timerItemSelect_Tick(object sender, EventArgs e) {
			this.timerItemSelect.Stop();
			ContentPredicate cp = this.selPred;
			if (null != cp) {
				this.txtComment.Text = cp.Comment;
				this.comboSubject.Text = cp.SubjectName;
				this.comboPredicate.Text = cp.PredicateName;
				this.txtObject.Text = cp.ObjectValue;
			}
		}

		private void timerLastTrue_Tick(object sender, EventArgs e) {
			this.timerLastTrue.Stop();
			this.UpdateView();
		}

	}
}
