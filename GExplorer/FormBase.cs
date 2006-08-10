using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	public partial class FormBase : Form {
		private static Icon customIcon = null;
		public static Icon CustomIcon {
			set { FormBase.customIcon = value; }
		}

		public FormBase() {
			InitializeComponent();
			if (null != FormBase.customIcon) {
				this.Icon = FormBase.customIcon;
			}
			//いまいち画質が悪い
			/* else {
				this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
			}*/
		}
	}
}
