using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class CacheViewerForm : BaseForm {
		public CacheViewerForm() {
			InitializeComponent();
		}

		private void CacheViewerForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			if (Program.RootOptions == null) return;
			Program.RootOptions.CacheViewerFormOptions.ApplyFormBaseOptionsAndTrackValues(this);
			
			if (Program.CacheController == null) return;
			this.dgcGenre.DataSource = Program.CacheController.GDataSet.GGenre;
			this.dgvPackage.DataSource = Program.CacheController.GDataSet.GPackage;
			this.dgvContent.DataSource = Program.CacheController.GDataSet.GContent;
			
			
		}

		private void tsmiCViewInCrawlResultView_Click(object sender, EventArgs e) {
			List<int> rowIndices = new List<int>();
			foreach (DataGridViewCell cell in this.dgvContent.SelectedCells) {
				if (!rowIndices.Contains(cell.RowIndex)) {
					rowIndices.Add(cell.RowIndex);
				}
			}
			rowIndices.Sort();
			
			List<GContentClass> conts = new List<GContentClass>();
			foreach (int idx in rowIndices) {
				DataGridViewRow dgvRow = this.dgvContent.Rows[idx];
				DataRowView drv = dgvRow.DataBoundItem as DataRowView;
				if(drv == null) continue;
				GDataSet.GContentRow row = drv.Row as GDataSet.GContentRow;
				GContentClass cont;
				if (Program.CacheController.TryFindContent(row.ContentKey, out cont)) {
					conts.Add(cont);
				}
			}
			Program.AddVirtualGenre(new SimpleContentsVirtualGenre(conts, "キャッシュビューア", "キャッシュビューアからの書き出し" + Environment.NewLine + DateTime.Now.ToString()));
		}
	}
	
	public sealed class CacheViewerFormOptions : FormOptionsBase {
		public CacheViewerFormOptions() {
		}
	}
}
