using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class UserCommandsEditor : Form {
		private Button[] btnsNeedingSelection;
		
		public UserCommandsEditor() {
			InitializeComponent();
			this.btnsNeedingSelection = new Button[]{
				this.btnUp, this.btnDown, this.btnDelete, this.btnModify,};
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = "外部コマンドエディタ";
			this.RefleshView();
			
			UserCommandsManager.Instance.UserCommandsChanged
				+= new UserCommandsChangedEventHandler(this.RefleshView);
			this.FormClosing += new FormClosingEventHandler(
				delegate(object sender, FormClosingEventArgs e) {
					UserCommandsManager.Instance.UserCommandsChanged -=
						new UserCommandsChangedEventHandler(this.RefleshView);
				});
		}
		
		private void RefleshView() {
			int oldSelIndex = this.lboxCommands.SelectedIndex;
			this.lboxCommands.Items.Clear();
			foreach(UserCommand uc in UserCommandsManager.Instance) {
				this.lboxCommands.Items.Add(uc);
			}
			int itemCount = this.lboxCommands.Items.Count;
			//位置復元のつもり
			this.lboxCommands.SelectedIndex = oldSelIndex < itemCount ? oldSelIndex : itemCount - 1;
			
			this.btnSort.Enabled = 0 != this.lboxCommands.Items.Count;
			bool hasItem = 0 < itemCount;
			foreach(Button b in this.btnsNeedingSelection) {
				b.Enabled = hasItem;
			}
			this.lboxCommands_SelectedIndexChanged(this, null);//無理やり選択しなおしたことにしとく
		}

		private void lboxCommands_SelectedIndexChanged(object sender, EventArgs e) {
			if(this.lboxCommands.SelectedIndex < 0) {
				foreach(Button b in this.btnsNeedingSelection) {
					b.Enabled = false;
				}
			} else {
				foreach(Button b in this.btnsNeedingSelection) {
					b.Enabled = true;
				}
				if(0 == this.lboxCommands.SelectedIndex) this.btnUp.Enabled = false;
				if(this.lboxCommands.Items.Count - 1 == this.lboxCommands.SelectedIndex) this.btnDown.Enabled = false;
				
				UserCommand uc = UserCommandsManager.Instance[this.lboxCommands.SelectedIndex];
				this.txtTitle.Text = uc.Title;
				this.txtFile.Text = uc.FileName;
				this.txtArg.Text = uc.Arguments;
			}
		}

		private void btnBrowse_Click(object sender, EventArgs e) {
			if(DialogResult.OK == this.openFileDialog1.ShowDialog()) {
				this.txtFile.Text = this.openFileDialog1.FileName;
			}
		}

		private void btnUp_Click(object sender, EventArgs e) {
			int selIdx = this.lboxCommands.SelectedIndex;
			UserCommandsManager.Instance.Swap(selIdx, selIdx - 1);
			this.lboxCommands.SelectedIndex--;
		}

		private void btnDown_Click(object sender, EventArgs e) {
			int selIdx = this.lboxCommands.SelectedIndex;
			UserCommandsManager.Instance.Swap(selIdx, selIdx + 1);
			this.lboxCommands.SelectedIndex++;
		}
		
		private void btnDelete_Click(object sender, EventArgs e) {
			int oldSelIdx = this.lboxCommands.SelectedIndex;
			if(this.lboxCommands.SelectedIndex == this.lboxCommands.Items.Count-1){
				this.lboxCommands.SelectedIndex--;
			}
			UserCommandsManager.Instance.RemoveAt(oldSelIdx);
		}
		
		private void btnSort_Click(object sender, EventArgs e) {
			UserCommandsManager.Instance.Sort();
		}
		
		private void btnInsert_Click(object sender, EventArgs e) {
			UserCommand uc = null;
			try {
				uc = new UserCommand(this.txtTitle.Text, this.txtFile.Text, this.txtArg.Text);
			} catch(Exception ex) {
				Utility.DisplayException(ex);
				return;
			}
			
			if(this.lboxCommands.SelectedIndex < 0) {
				UserCommandsManager.Instance.Add(uc);
			} else {
				UserCommandsManager.Instance.Insert(this.lboxCommands.SelectedIndex + 1, uc);
				this.lboxCommands.SelectedIndex++;
			}
		}

		private void btnModify_Click(object sender, EventArgs e) {
			UserCommand uc = null;
			try {
				uc = new UserCommand(this.txtTitle.Text, this.txtFile.Text, this.txtArg.Text);
			} catch(Exception ex) {
				Utility.DisplayException(ex);
				return;
			}
			UserCommandsManager.Instance[this.lboxCommands.SelectedIndex] = uc;
		}
	}
}
