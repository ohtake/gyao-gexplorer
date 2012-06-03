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
		
		[Category("�t�H�[���̊�{�ݒ�")]
		[DisplayName("�ʏ펞�̃T�C�Y")]
		[Description("�ő剻��ŏ����̂���Ă��Ȃ��ꍇ�ł̃E�B���h�E�̃T�C�Y�D")]
		public Size? NormalSize {
			get { return this.normalSize; }
			set { this.normalSize = value; }
		}
		[Category("�t�H�[���̊�{�ݒ�")]
		[DisplayName("�ʏ펞�̈ʒu")]
		[Description("�ő剻��ŏ����̂���Ă��Ȃ��ꍇ�ł̃E�B���h�E�̈ʒu�D")]
		public Point? NormalLocation {
			get { return this.normalLocation; }
			set { this.normalLocation = value; }
		}
		[Category("�t�H�[���̊�{�ݒ�")]
		[DisplayName("�őO��")]
		[Description("True�ɂ���ƍőO�ʂŕ\������D")]
		public bool? TopMost {
			get { return this.topMost; }
			set { this.topMost = value; }
		}
		[Category("�t�H�[���̊�{�ݒ�")]
		[DisplayName("�t�H�[���̏��")]
		[Description("�ʏ�C�ŏ����C�ő剻�̏�Ԃ̂��ƁD")]
		public FormWindowState? WindowState {
			get { return this.windowState; }
			set { this.windowState = value; }
		}
	}
}
