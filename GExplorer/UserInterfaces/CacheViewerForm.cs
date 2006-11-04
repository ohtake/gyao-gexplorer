using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.AppCore;

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
			this.dgvContent.DataSource = Program.CacheController.GDataSet.GContent;
			this.dgvPackage.DataSource = Program.CacheController.GDataSet.GPackage;
			this.dgcGenre.DataSource = Program.CacheController.GDataSet.GGenre;
		}
	}
	
	public sealed class CacheViewerFormOptions : FormOptionsBase {
		public CacheViewerFormOptions() {
		}
	}
}
