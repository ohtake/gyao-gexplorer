using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Xml.Serialization;
using Yusen.GExplorer.OldApp;

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
				if (!UserCommandsEditor.HasInstance) {
					UserCommandsEditor.instance = new UserCommandsEditor();
				}
				return UserCommandsEditor.instance;
			}
		}
		public static bool HasInstance {
			get {
				return null != UserCommandsEditor.instance && !UserCommandsEditor.instance.IsDisposed;
			}
		}
		
		private readonly Button[] btnsNeedingSelection;
		private UserCommandsEditorSettings settings;

		private UserCommandsEditor() {
			InitializeComponent();
			
			this.btnsNeedingSelection = new Button[]{
				this.btnUp, this.btnDown, this.btnDelete, this.btnModify,};
			
			if (base.DesignMode) return;
			
			{//簡易追加機能のメニューを作成
				List<ToolStripItem> categories = new List<ToolStripItem>();
				foreach (ToolStripItem item in this.tscapmiProperty.DropDownItems) {
					categories.Add(item);
				}
				this.tscapmiProperty.DropDownItems.Clear();
				categories.Reverse();
				foreach (ToolStripItem item in categories) {
					this.cmsArgs.Items.Insert(0, item);
				}
			}
			{//リテラル文字
				List<ToolStripItem> literals = new List<ToolStripItem>();
				foreach (string escaped in UserCommand.GetEscapedLiterals()) {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(UserCommand.UnescapeLiteral(escaped));
					tsmi.Tag = escaped;
					tsmi.Click += delegate(object sender2, EventArgs e2) {
						this.AppendArg((sender2 as ToolStripMenuItem).Tag as string);
					};
					literals.Add(tsmi);
				}
				this.tsmiLiterals.DropDownItems.AddRange(literals.ToArray());
			}
			{//コードページ
				List<ToolStripItem> firstLetters = new List<ToolStripItem>();
				List<EncodingInfo> eis = new List<EncodingInfo>(Encoding.GetEncodings());
				eis.Sort(new Comparison<EncodingInfo>(delegate(EncodingInfo a, EncodingInfo b){
					return string.Compare(a.DisplayName, b.DisplayName, false);
				}));
				char firstLetter = char.MinValue;
				ToolStripMenuItem tsmiFirstLetter = null;
				foreach (EncodingInfo ei in eis) {
					string displayName = ei.DisplayName;
					if (string.IsNullOrEmpty(displayName)) continue;
					if (null == tsmiFirstLetter || firstLetter != displayName[0]) {
						firstLetter = displayName[0];
						tsmiFirstLetter = new ToolStripMenuItem(firstLetter.ToString());
						firstLetters.Add(tsmiFirstLetter);
					}
					ToolStripMenuItem tsmi = new ToolStripMenuItem(ei.DisplayName.Replace("&", "&&"));
					tsmi.Tag = ei.Name;
					tsmi.Click += delegate(object sender2, EventArgs e2) {
						this.AppendArg((sender2 as ToolStripMenuItem).Tag as string);
					};
					tsmiFirstLetter.DropDownItems.Add(tsmi);
				}
				this.tsmiCodepages.DropDownItems.AddRange(firstLetters.ToArray());
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
		
		private void btnSeparator_Click(object sender, EventArgs e) {
			this.txtTitle.Text = UserCommand.SeparatorTitle;
			this.txtFile.Text = string.Empty;
			this.txtArg.Text = string.Empty;
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
