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

		[Description("サブメニューの外部コマンドが選択された")]
		public event EventHandler<UserCommandSelectedEventArgs> UserCommandSelected;

		public ToolStripUserCommandMenuItem() : base("ToolStripUserCommandMenuItem"){
			UserCommandsManager.Instance.UserCommandsChanged += new EventHandler(UserCommandsManager_UserCommandsChanged);
			this.CreateSubMenuItems();
			base.EnabledChanged += delegate {//デザイナが勝手に Enable = false にしてしまうことへの対策
				this.CreateSubMenuItems();
			};
		}
		private void CreateSubMenuItems() {
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
			base.DropDownItems.AddRange(items.ToArray());
			base.Enabled = base.HasDropDownItems;
		}
		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			this.CreateSubMenuItems();
		}
		
		protected override void Dispose(bool disposing) {
			if (disposing) {
				UserCommandsManager.Instance.UserCommandsChanged -= new EventHandler(UserCommandsManager_UserCommandsChanged);
			}
			base.Dispose(disposing);
		}
	}

	public sealed class UserCommandSelectedEventArgs : EventArgs {
		private UserCommand command;
		public UserCommandSelectedEventArgs(UserCommand command) {
			this.command = command;
		}
		public UserCommand UserCommand {
			get { return this.command; }
		}
	}
}
