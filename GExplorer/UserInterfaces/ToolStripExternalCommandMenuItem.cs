using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Yusen.GExplorer.AppCore;

namespace Yusen.GExplorer.UserInterfaces {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("ExternalCommandSelected")]
	public sealed class ToolStripExternalCommandMenuItem : ToolStripMenuItem{
		private sealed class ToolStripMenuItemWithExternalCommand : ToolStripMenuItem {
			private readonly ExternalCommand ec;
			
			public ToolStripMenuItemWithExternalCommand(ExternalCommand ec)
				: base(ec.Title) {
				this.ec = ec;
			}
			public ExternalCommand ExternalCommand {
				get { return this.ec; }
			}
		}

		public event EventHandler ExternalCommandSelected;
		private ExternalCommand lastSelectedExternalCommand;

		public ToolStripExternalCommandMenuItem()
			: base("ToolStripExternalCommandMenuItem") {
			
			base.DropDownItems.Add("ExtCmd1");
			base.DropDownItems.Add("ExtCmd2");
			
			if (base.DesignMode) return;

			this.DropDownOpening += new EventHandler(this.ToolStripExternalCommandMenuItem_DropDownOpening);
		}
		private void ToolStripExternalCommandMenuItem_DropDownOpening(object sender, EventArgs e) {
			if (base.DesignMode) return;
			if (null == Program.ExternalCommandsManager) return;
			
			this.CreateDropDownItems();
		}
		private void CreateDropDownItems() {
			List<ToolStripItem> items = new List<ToolStripItem>();
			foreach (ExternalCommand ec in Program.ExternalCommandsManager) {
				if (ec.Title == ExternalCommand.SeparatorTitle) {
					items.Add(new ToolStripSeparator());
				} else {
					ToolStripMenuItemWithExternalCommand tsmiwec = new ToolStripMenuItemWithExternalCommand(ec);
					tsmiwec.Click += new EventHandler(tsmiwec_Click);
					items.Add(tsmiwec);
				}
			}
			if (items.Count == 0) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem("(外部コマンドなし)");
				tsmi.Enabled = false;
				items.Add(tsmi);
			}
			base.DropDownItems.Clear();
			base.DropDownItems.AddRange(items.ToArray());
		}

		private void tsmiwec_Click(object sender, EventArgs e) {
			this.OnExternalCommandSelected((sender as ToolStripMenuItemWithExternalCommand).ExternalCommand);
		}
		private void OnExternalCommandSelected(ExternalCommand ec) {
			this.lastSelectedExternalCommand = ec;
			EventHandler handler = this.ExternalCommandSelected;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		public ExternalCommand LastSelectedExternalCommand {
			get { return this.lastSelectedExternalCommand; }
		}
	}
}
