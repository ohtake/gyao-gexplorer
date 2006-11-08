using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Xml.Serialization;
using Yusen.GExplorer.AppCore;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class ExternalCommandsEditForm : BaseForm {
		private readonly Button[] btnsNeedingSelection;
		
		public ExternalCommandsEditForm() {
			InitializeComponent();
			
			this.btnsNeedingSelection = new Button[]{
				this.btnUp, this.btnDown, this.btnDelete, this.btnModify,};
		}
		private void ExternalCommandsEditor_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;

			{//簡易追加機能のメニューを作成
				List<ToolStripItem> categories = new List<ToolStripItem>();
				foreach (ToolStripItem item in this.tscpmiPropertyName.DropDownItems) {
					categories.Add(item);
				}
				this.tscpmiPropertyName.DropDownItems.Clear();
				categories.Reverse();
				foreach (ToolStripItem item in categories) {
					this.cmsArgs.Items.Insert(0, item);
				}
			}
			{//リテラル文字
				List<ToolStripItem> literals = new List<ToolStripItem>();
				foreach (string escaped in ExternalCommand.GetEscapedLiterals()) {
					ToolStripMenuItem tsmi = new ToolStripMenuItem(ExternalCommand.UnescapeLiteral(escaped));
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
				eis.Sort(new Comparison<EncodingInfo>(delegate(EncodingInfo a, EncodingInfo b) {
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

			//オプション
			if (Program.RootOptions == null) return;
			Program.RootOptions.ExternalCommandsEditorOptions.ApplyFormBaseOptionsAndTrackValues(this);
			
			//コマンド
			if (Program.ExternalCommandsManager == null) return;
			Program.ExternalCommandsManager.ExternalCommandsManagerChanged += new EventHandler(ExternalCommandsManager_ExternalCommandsManagerChanged);
			this.FormClosed += delegate {
				Program.ExternalCommandsManager.ExternalCommandsManagerChanged -= new EventHandler(ExternalCommandsManager_ExternalCommandsManagerChanged);
			};
			this.ExternalCommandsManager_ExternalCommandsManagerChanged(null, EventArgs.Empty);
		}
		
		private void ExternalCommandsManager_ExternalCommandsManagerChanged(object sender, EventArgs e) {
			int oldSelIndex = this.lboxCommands.SelectedIndex;

			this.lboxCommands.BeginUpdate();
			this.lboxCommands.Items.Clear();
			foreach (ExternalCommand ec in Program.ExternalCommandsManager) {
				this.lboxCommands.Items.Add(ec);
			}
			this.lboxCommands.EndUpdate();

			int itemCount = this.lboxCommands.Items.Count;
			//位置復元のつもり
			this.lboxCommands.SelectedIndex = oldSelIndex < itemCount ? oldSelIndex : itemCount - 1;

			bool hasItem = 0 < itemCount;
			foreach (Button b in this.btnsNeedingSelection) {
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
				
				ExternalCommand ec = Program.ExternalCommandsManager[this.lboxCommands.SelectedIndex];
				this.txtTitle.Text = ec.Title;
				this.txtFile.Text = ec.Filename;
				this.txtArg.Text = ec.Arguments;
			}
		}
		
		private void btnUp_Click(object sender, EventArgs e) {
			int selIdx = this.lboxCommands.SelectedIndex;
			Program.ExternalCommandsManager.Swap(selIdx, selIdx - 1);
			this.lboxCommands.SelectedIndex--;
		}
		
		private void btnDown_Click(object sender, EventArgs e) {
			int selIdx = this.lboxCommands.SelectedIndex;
			Program.ExternalCommandsManager.Swap(selIdx, selIdx + 1);
			this.lboxCommands.SelectedIndex++;
		}
		
		private void btnDelete_Click(object sender, EventArgs e) {
			int oldSelIdx = this.lboxCommands.SelectedIndex;
			if(this.lboxCommands.SelectedIndex == this.lboxCommands.Items.Count-1){
				this.lboxCommands.SelectedIndex--;
			}
			Program.ExternalCommandsManager.RemoveAt(oldSelIdx);
		}
		
		private void btnSeparator_Click(object sender, EventArgs e) {
			this.txtTitle.Text = ExternalCommand.SeparatorTitle;
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
			ExternalCommand ec = new ExternalCommand(this.txtTitle.Text, this.txtFile.Text, this.txtArg.Text);
			
			if(this.lboxCommands.SelectedIndex < 0) {
				Program.ExternalCommandsManager.Add(ec);
			} else {
				Program.ExternalCommandsManager.Insert(this.lboxCommands.SelectedIndex + 1, ec);
				this.lboxCommands.SelectedIndex++;
			}
		}
		
		private void btnModify_Click(object sender, EventArgs e) {
			ExternalCommand ec = new ExternalCommand(this.txtTitle.Text, this.txtFile.Text, this.txtArg.Text);
			Program.ExternalCommandsManager.BeginUpdate();
			Program.ExternalCommandsManager.RemoveAt(this.lboxCommands.SelectedIndex);
			Program.ExternalCommandsManager.Insert(this.lboxCommands.SelectedIndex, ec);
			Program.ExternalCommandsManager.EndUpdate();
		}
		
		private void AppendArg(string arg) {
			string beforeCarret = this.txtArg.Text.Substring(0, this.txtArg.SelectionStart);
			string afterCarret = this.txtArg.Text.Substring(this.txtArg.SelectionStart + this.txtArg.SelectionLength);
			
			this.txtArg.Text = beforeCarret + arg + afterCarret;
			this.txtArg.SelectionLength = 0;
			this.txtArg.SelectionStart = beforeCarret.Length + arg.Length;
		}
	}
	
	public sealed class ExternalCommandsEditFormOptions : FormOptionsBase {
	}
}
