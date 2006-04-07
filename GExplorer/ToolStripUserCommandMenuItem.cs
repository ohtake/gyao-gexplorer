using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace Yusen.GExplorer {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("UserCommandSelected")]
	public sealed class ToolStripUserCommandMenuItem : ToolStripMenuItem{
		private sealed class ToolStripMenuItemWithUserCommand : ToolStripMenuItem {
			UserCommand command;
			public ToolStripMenuItemWithUserCommand(UserCommand command) : base(command.Title) {
				this.command = command;
			}
			public UserCommand UserCommand {
				get { return this.command; }
			}
		}

		[Description("サブメニューの外部コマンドが選択された．")]
		public event EventHandler<UserCommandSelectedEventArgs> UserCommandSelected;

		public ToolStripUserCommandMenuItem() : base("ToolStripUserCommandMenuItem"){
			if (base.DesignMode) return;
			
			this.RecreateSubMenuItems();
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
		}
		private void RecreateSubMenuItems() {
			List<ToolStripItem> items = new List<ToolStripItem>();
			
			foreach (UserCommand command in UserCommandsManager.Instance) {
				ToolStripMenuItemWithUserCommand tsmiwuc = new ToolStripMenuItemWithUserCommand(command);
				tsmiwuc.Click += delegate(object sender, EventArgs e) {
					ToolStripMenuItemWithUserCommand sender2 = sender as ToolStripMenuItemWithUserCommand;
					EventHandler<UserCommandSelectedEventArgs> handler = this.UserCommandSelected;
					if (null == handler) {
						throw new InvalidOperationException("UserCommandSelectedイベントがハンドルされていない");
					}
					handler(this, new UserCommandSelectedEventArgs(sender2.UserCommand));
				};
				items.Add(tsmiwuc);
			}
			
			base.DropDownItems.Clear();
			if (items.Count > 0) {
				base.DropDownItems.AddRange(items.ToArray());
			} else {
				ToolStripMenuItem tsmi = new ToolStripMenuItem("(なし)");
				tsmi.Enabled = false;
				base.DropDownItems.Add(tsmi);
			}
		}
		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.RecreateSubMenuItems();
		}
		
		protected override void Dispose(bool disposing) {
			if (disposing) {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			}
			base.Dispose(disposing);
		}
	}
}
