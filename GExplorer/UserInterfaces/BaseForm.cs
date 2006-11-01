using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yusen.GExplorer.UserInterfaces {
	public partial class BaseForm : Form {
		private static Icon customIcon = null;
		public static Icon CustomIcon {
			set { BaseForm.customIcon = value; }
		}
		
		public BaseForm() {
			InitializeComponent();
			if (null != BaseForm.customIcon) {
				this.Icon = BaseForm.customIcon;
			}
			//いまいち画質が悪い
			/* else {
				this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
			}*/
		}
	}
}
