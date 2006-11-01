using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Yusen.GExplorer.UserInterfaces {
	public abstract class FormOptionsBase {
		public FormOptionsBase() {
		}

		private Rectangle? formBounds = null;
		[Category("フォームの基本設定")]
		[DisplayName("矩形領域")]
		[Description("ウィンドウの状態が標準の場合の位置とサイズを示します．")]
		[ReadOnly(true)]
		[DefaultValue(null)]
		public Rectangle? FormBounds {
			get { return this.formBounds; }
			set { this.formBounds = value; }
		}

		private bool isMaximized = false;
		[Category("フォームの基本設定")]
		[DisplayName("最大化")]
		[Description("ウィンドウを開くときに最大化して開きます．")]
		[ReadOnly(true)]
		[DefaultValue(false)]
		public bool IsMaximized {
			get { return this.isMaximized; }
			set { this.isMaximized = value; }
		}

		public void ApplyFormBaseOptionsAndTrackValues(Form form) {
			if (this.FormBounds.HasValue) {
				form.StartPosition = FormStartPosition.Manual;
				form.Bounds = this.FormBounds.Value;
			}
			if (this.IsMaximized) {
				form.WindowState = FormWindowState.Maximized;
			}
			form.ResizeEnd += new EventHandler(form_ResizeEnd);
			form.LocationChanged += new EventHandler(form_LocationChanged);
			form.FormClosed += new FormClosedEventHandler(form_FormClosed);
		}
		private void StoreValues(Form form) {
			switch (form.WindowState) {
				case FormWindowState.Normal:
					this.FormBounds = form.Bounds;
					break;
				default:
					this.FormBounds = form.RestoreBounds;
					break;
			}
			this.IsMaximized = (form.WindowState == FormWindowState.Maximized);
		}

		private void form_LocationChanged(object sender, EventArgs e) {
			this.StoreValues(sender as Form);
		}
		private void form_ResizeEnd(object sender, EventArgs e) {
			this.StoreValues(sender as Form);
		}
		private void form_FormClosed(object sender, FormClosedEventArgs e) {
			Form form = sender as Form;
			form.ResizeEnd -= this.form_ResizeEnd;
			form.LocationChanged -= this.form_LocationChanged;
			form.FormClosed -= this.form_FormClosed;
		}
	}
}
