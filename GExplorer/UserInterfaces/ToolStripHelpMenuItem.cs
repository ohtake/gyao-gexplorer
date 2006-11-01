using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Yusen.GExplorer.UserInterfaces {
	public sealed partial class ToolStripHelpMenuItem : ToolStripMenuItem {
		public ToolStripHelpMenuItem() : base("ToolStripHelpMenuItem"){
			InitializeComponent();
			
			this.tsmiReadMe.Click += delegate {
				Process.Start("ReadMe.txt");
			};
			this.tsmiChangeLog.Click += delegate {
				Process.Start("ChangeLog.txt");
			};
			this.tsmiAbout.Click += delegate {
				using (AboutBox abox = new AboutBox()) {
					abox.ShowDialog(this.Owner.FindForm());
				}
			};
			
		}
	}
}
