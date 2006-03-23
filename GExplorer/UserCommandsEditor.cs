using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public sealed partial class UserCommandsEditor : FormSettingsBase, IFormWithNewSettings<UserCommandsEditor.UserCommandsEditorSettings> {
		public sealed class UserCommandsEditorSettings : INewSettings<UserCommandsEditorSettings> {
			private readonly UserCommandsEditor owner;

			public UserCommandsEditorSettings()
				: this(null) {
			}
			internal UserCommandsEditorSettings(UserCommandsEditor owner) {
				this.owner = owner;
				this.formSettingsBaseSettings = new FormSettingsBaseSettings(owner);
			}

			[XmlIgnore]
			[Browsable(false)]
			private bool HasOwner {
				get { return null != this.owner; }
			}

			[ReadOnly(true)]
			[Category("位置とサイズ")]
			[DisplayName("フォームの基本設定")]
			[Description("フォームの基本的な設定です．")]
			public FormSettingsBaseSettings FormSettingsBaseSettings {
				get { return this.formSettingsBaseSettings; }
				set { this.FormSettingsBaseSettings.ApplySettings(value); }
			}
			private FormSettingsBaseSettings formSettingsBaseSettings;

			#region INewSettings<UserCommandsEditorSettings> Members
			public void ApplySettings(UserCommandsEditorSettings newSettings) {
				Utility.SubstituteAllPublicProperties(this, newSettings);
			}
			#endregion
		}
		
		private static UserCommandsEditor instance = null;
		public static UserCommandsEditor Instance {
			get {
				if(null == UserCommandsEditor.instance || UserCommandsEditor.instance.IsDisposed) {
					UserCommandsEditor.instance = new UserCommandsEditor();
				}
				return UserCommandsEditor.instance;
			}
		}
		
		private readonly Button[] btnsNeedingSelection;
		private UserCommandsEditorSettings settings;

		private UserCommandsEditor() {
			InitializeComponent();
			
			this.btnsNeedingSelection = new Button[]{
				this.btnUp, this.btnDown, this.btnDelete, this.btnModify,};

			//簡易追加機能のメニューを作成
			while (this.tscapmiProperty.HasDropDownItems) {
				ToolStripItem tsi = this.tscapmiProperty.DropDownItems[0];
				this.tscapmiProperty.DropDownItems.Remove(tsi);
				this.cmsArgs.Items.Add(tsi);
			}
			{//リテラル文字
				this.cmsArgs.Items.Add(new ToolStripSeparator());
				ToolStripMenuItem tsmiLiterals = new ToolStripMenuItem("リテラル文字");
				foreach (string escaped in UserCommand.GetEscapedLiterals()) {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(UserCommand.UnescapeLiteral(escaped));
					tsmi.Tag = escaped;
					tsmi.Click += delegate(object sender2, EventArgs e2) {
						this.AppendArg((sender2 as ToolStripMenuItem).Tag as string);
					};
					tsmiLiterals.DropDownItems.Add(tsmi);
				}
				this.cmsArgs.Items.Add(tsmiLiterals);
			}
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

		private void UserCommandsEditor_Load(object sender, EventArgs e) {
			//コマンド
			UserCommandsManager.Instance.UserCommandsChanged
				+= new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			this.FormClosing += delegate {
				UserCommandsManager.Instance.UserCommandsChanged -=
					new EventHandler(this.UserCommandsManager_UserCommandsChanged);
			};
			this.UserCommandsManager_UserCommandsChanged(null, EventArgs.Empty);

			this.settings = new UserCommandsEditorSettings(this);
			Utility.LoadSettingsAndEnableSaveOnClosedNew(this);
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
			this.cmsArgs.Show(btn.PointToScreen(new Point(0, btn.Height)));
		}
		private void btnInsert_Click(object sender, EventArgs e) {
			UserCommand uc = null;
			try {
				uc = new UserCommand(this.txtTitle.Text, this.txtFile.Text, this.txtArg.Text);
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "外部コマンドの挿入", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "外部コマンドの変更", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			UserCommandsManager.Instance[this.lboxCommands.SelectedIndex] = uc;
		}

		private void tscapmiProperty_PropertySelected(object sender, CAPropertySelectedEventArgs e) {
			this.AppendArg("{" + e.PropertyInfo.Name + "}");
		}
		
		private void AppendArg(string arg) {
			string beforeCarret = this.txtArg.Text.Substring(0, this.txtArg.SelectionStart);
			string afterCarret = this.txtArg.Text.Substring(this.txtArg.SelectionStart + this.txtArg.SelectionLength);

			this.txtArg.Text = beforeCarret + arg + afterCarret;
			this.txtArg.SelectionLength = 0;
			this.txtArg.SelectionStart = beforeCarret.Length + arg.Length;
		}

		#region IHasNewSettings<UserCommandsEditorSettings> Members
		public UserCommandsEditor.UserCommandsEditorSettings Settings {
			get { return this.settings; }
		}
		#endregion
	}
}
