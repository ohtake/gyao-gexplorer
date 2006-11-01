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

			if (Program.CacheManager == null) return;
			this.dgvContent.DataSource = Program.CacheManager.GDataSet.GContent;
			this.dgvPackage.DataSource = Program.CacheManager.GDataSet.GPackage;
			this.dgcGenre.DataSource = Program.CacheManager.GDataSet.GGenre;
		}
	}
	
	public sealed class CacheViewerFormOptions : FormOptionsBase {
		public CacheViewerFormOptions() {
		}
	}
}
