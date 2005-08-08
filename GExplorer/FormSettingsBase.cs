using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class FormSettingsBase : FormBase, IHasSettings<FormSettingsBaseSettings>{
		private Size normalSize;
		private Point normalLocation;
		
		public FormSettingsBase() : base(){
			InitializeComponent();
		}
		
		public void FillSettings(FormSettingsBaseSettings settings) {
			settings.NormalSize = this.normalSize;
			settings.NormalLocation = this.normalLocation;
			settings.TopMost = this.TopMost;
			settings.WindowState = this.WindowState;
		}
		public void ApplySettings(FormSettingsBaseSettings settings) {
			if (null == settings) return;
			if (settings.NormalLocation.HasValue) {
				this.StartPosition = FormStartPosition.Manual;
				this.Location = settings.NormalLocation.Value;
			}
			this.Size = settings.NormalSize ?? this.Size;
			this.TopMost = settings.TopMost ?? this.TopMost;
			this.WindowState = settings.WindowState ?? this.WindowState;
		}
		private void SaveNormalSizeAndLocation() {
			switch (this.WindowState) {
				case FormWindowState.Normal:
					this.normalSize = this.Size;
					this.normalLocation = this.Location;
					break;
			}
		}

		private void FormSettingsBase_Layout(object sender, LayoutEventArgs e) {
			this.SaveNormalSizeAndLocation();
		}
		private void FormSettingsBase_Move(object sender, EventArgs e) {
			this.SaveNormalSizeAndLocation();
		}
		private void FormSettingsBase_Resize(object sender, EventArgs e) {
			this.SaveNormalSizeAndLocation();
		}
	}
	
	public class FormSettingsBaseSettings {
		private Size? normalSize;
		private Point? normalLocation;
		private bool? topMost;
		private FormWindowState? windowState;
		
		[Category("フォームの基本設定")]
		[DisplayName("通常時のサイズ")]
		[Description("最大化や最小化のされていない場合でのウィンドウのサイズ．")]
		public Size? NormalSize {
			get { return this.normalSize; }
			set { this.normalSize = value; }
		}
		[Category("フォームの基本設定")]
		[DisplayName("通常時の位置")]
		[Description("最大化や最小化のされていない場合でのウィンドウの位置．")]
		public Point? NormalLocation {
			get { return this.normalLocation; }
			set { this.normalLocation = value; }
		}
		[Category("フォームの基本設定")]
		[DisplayName("最前面")]
		[Description("Trueにすると最前面で表示する．")]
		public bool? TopMost {
			get { return this.topMost; }
			set { this.topMost = value; }
		}
		[Category("フォームの基本設定")]
		[DisplayName("フォームの状態")]
		[Description("通常，最小化，最大化の状態のこと．")]
		public FormWindowState? WindowState {
			get { return this.windowState; }
			set { this.windowState = value; }
		}
	}
}
