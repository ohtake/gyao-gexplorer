using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yusen.GCrawler;


namespace Yusen.GExplorer {
	sealed partial class BitRateForm : FormBase {
		public BitRateForm() {
			InitializeComponent();
			this.Text = Program.ApplicationName;
		}

		public GBitRate BitRate {
			get {
				return this.rdoSuperFine.Checked ? GBitRate.SuperFine : GBitRate.Standard;
			}
			set {
				switch (value) {
					case GBitRate.SuperFine:
						this.rdoSuperFine.Checked = true;
						break;
					case GBitRate.Standard:
						this.rdoStandard.Checked = true;
						break;
				}
			}
		}
		
		public bool SkipNextTimeEnabled {
			get { return this.chkSkipNextTime.Checked; }
			set { this.chkSkipNextTime.Checked = value; }
		}
	}
}
