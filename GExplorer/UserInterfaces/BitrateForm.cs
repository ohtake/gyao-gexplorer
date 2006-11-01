using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Yusen.GExplorer.UserInterfaces;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class BitrateForm : BaseForm {
		public BitrateForm() {
			InitializeComponent();
			this.Text = Program.ApplicationName;
		}

		public GBitrate Bitrate {
			get {
				return this.rdoSuperFine.Checked ? GBitrate.SuperFine : GBitrate.Standard;
			}
			set {
				switch (value) {
					case GBitrate.SuperFine:
						this.rdoSuperFine.Checked = true;
						break;
					case GBitrate.Standard:
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
