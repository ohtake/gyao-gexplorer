using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;

namespace Yusen.GExplorer {
	partial class UserCommandsEditor : FormSettingsBase, IFormWithSettings<UserCommandsEditorSettings>{
		private static UserCommandsEditor instance = null;
		public static UserCommandsEditor Instance {
			get {
				if(null == UserCommandsEditor.instance || UserCommandsEditor.instance.IsDisposed) {
					UserCommandsEditor.instance = new UserCommandsEditor();
				}
				return UserCommandsEditor.instance;
			}
		}
		
		private Button[] btnsNeedingSelection;
		
		private UserCommandsEditor() {
			InitializeComponent();
			this.btnsNeedingSelection = new Button[]{
				this.btnUp, this.btnDown, this.btnDelete, this.btnModify,};
			this.Text = "外部コマンドエディタ";
			this.UserCommandsManager_UserCommandsChanged(null, EventArgs.Empty);
			
			//簡易追加機能のメニューを作成
			this.GenerateArgumentHelperMenu();
			
			//コマンド
			UserCommandsManager.Instance.UserCommandsChanged
				+= new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			this.FormClosing += delegate{
				UserCommandsManager.Instance.UserCommandsChanged -=
					new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			};
			
			Utility.LoadSettingsAndEnableSaveOnClosed(this);
		}

		public void FillSettings(UserCommandsEditorSettings settings) {
			base.FillSettings(settings);
		}

		public void ApplySettings(UserCommandsEditorSettings settings) {
			base.ApplySettings(settings);
		}

		public string FilenameForSettings {
			get { return @"UserCommandsEditorSettings.xml"; }
		}
		private void UserCommandsManager_UserCommandsChanged(object sender, EventArgs e) {
			int oldSelIndex = this.lboxCommands.SelectedIndex;
			
			this.lboxCommands.BeginUpdate();
			this.lboxCommands.Items.Clear();
			foreach(UserCommand uc in UserCommandsManager.Instance) {
				this.lboxCommands.Items.Add(uc);
			}
			this.lboxCommands.EndUpdate();
			
			int itemCount = this.lboxCommands.Items.Count;
			//位置復元のつもり
			this.lboxCommands.SelectedIndex = oldSelIndex < itemCount ? oldSelIndex : itemCount - 1;
			
			this.btnSort.Enabled = 0 != this.lboxCommands.Items.Count;
			bool hasItem = 0 < itemCount;
			foreach(Button b in this.btnsNeedingSelection) {
				b.Enabled = hasItem;
			}
			this.lboxCommands_SelectedIndexChanged(this, EventArgs.Empty);//無理やり選択しなおしたことにしとく
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
		private void btnBrowse_Click(object sender, EventArgs e) {
			if(DialogResult.OK == this.openFileDialog1.ShowDialog()) {
				this.txtFile.Text = this.openFileDialog1.FileName;
			}
		}
		private void btnArg_Click(object sender, EventArgs e) {
			Button btn = sender as Button;
			this.cmsArgs.Location = btn.PointToScreen(new Point(0, btn.Height));
			this.cmsArgs.Show();
		}
		private void btnInsert_Click(object sender, EventArgs e) {
			UserCommand uc = null;
			uc = new UserCommand(this.txtTitle.Text, this.txtFile.Text, this.txtArg.Text);
			
			if(this.lboxCommands.SelectedIndex < 0) {
				UserCommandsManager.Instance.Add(uc);
			} else {
				UserCommandsManager.Instance.Insert(this.lboxCommands.SelectedIndex + 1, uc);
				this.lboxCommands.SelectedIndex++;
			}
		}
		
		private void btnModify_Click(object sender, EventArgs e) {
			UserCommand uc = null;
			uc = new UserCommand(this.txtTitle.Text, this.txtFile.Text, this.txtArg.Text);
			UserCommandsManager.Instance[this.lboxCommands.SelectedIndex] = uc;
		}

		private void GenerateArgumentHelperMenu() {
			foreach (PropertyInfo pi in typeof(ContentAdapter).GetProperties(BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public)) {
				object[] attribs = pi.GetCustomAttributes(typeof(BrowsableAttribute), false);
				if (attribs.Length > 0 && !(attribs[0] as BrowsableAttribute).Browsable) {
					continue;
				}
				
				ToolStripMenuItem mi = new ToolStripMenuItem();
				mi.Text = "{" + pi.Name + "}";
				mi.Click += new EventHandler(this.AppendArgWithMenuItemText);
				this.cmsArgs.Items.Add(mi);
			}
		}
		
		void AppendArgWithMenuItemText(object sender, EventArgs e) {
			string labelName = (sender as ToolStripMenuItem).Text;
			if("" == this.txtArg.Text || this.txtArg.Text.EndsWith(" ")) {
				this.txtArg.Text += labelName;
			} else {
				this.txtArg.Text += " " + labelName;
			}
		}
	}
	
	public class UserCommandsEditorSettings : FormSettingsBaseSettings{
	}
}
