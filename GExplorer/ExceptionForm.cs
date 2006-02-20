using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Yusen.GExplorer {
	sealed partial class ExceptionForm : FormBase {
		private static int ControlTopComparison(Control a, Control b) {
			return a.Top.CompareTo(b.Top);
		}
		private static string TrimStackTrace(string stackTrace) {
			if(string.IsNullOrEmpty(stackTrace)) {
				return string.Empty;
			}
			StringBuilder sb = new StringBuilder();
			using(StringReader reader = new StringReader(stackTrace)) {
				string line;
				while (null != (line = reader.ReadLine())) {
					if (sb.Length>0) {
						sb.AppendLine();
					}
					sb.Append(line.Trim());
				}
			}
			return sb.ToString();
		}
		
		private Exception exception = null;
		
		public ExceptionForm() {
			InitializeComponent();
			this.txtExceptionMessage.Font = new Font(this.txtExceptionMessage.Font, FontStyle.Bold);
			
			this.txtProdVer.Text = Application.ProductVersion.ToString();
			this.txtClrVer.Text = Environment.Version.ToString();
			this.txtOsVer.Text = Environment.OSVersion.ToString();
			
			this.Exception = null;
		}
		public ExceptionForm(Exception e) : this() {
			this.Exception = e;
		}
		private ExceptionForm(Exception e, bool allowAbort) : this(e) {
			this.AllowAbort = allowAbort;
		}

		public Exception Exception {
			get { return this.exception; }
			set {
				this.exception = value;
				if (value != null) {
					this.txtExceptionType.Text = value.GetType().ToString();
					this.txtExceptionMessage.Text = value.Message;
					this.txtStackTrace.Text = ExceptionForm.TrimStackTrace(value.StackTrace);
					if (null != value.InnerException) {
						this.llblInnerException.Text = value.InnerException.GetType().ToString();
						this.llblInnerException.Enabled = true;
					} else {
						this.llblInnerException.Text = "null";
						this.llblInnerException.Enabled = false;
					}
				} else {
					this.txtExceptionType.Clear();
					this.txtExceptionMessage.Clear();
					this.txtStackTrace.Clear();
					this.llblInnerException.Text = "null";
					this.llblInnerException.Enabled = false;
				}
			}
		}
		public bool AllowAbort {
			get { return this.btnAbort.Enabled; }
			set { this.btnAbort.Enabled = value; }
		}
		private void ExceptionForm_Shown(object sender, EventArgs e) {
			this.btnContinue.Focus();
		}
		private void btnCopy_Click(object sender, EventArgs e) {
			List<Control> names = new List<Control>();
			List<Control> values = new List<Control>();
			foreach (Control c in this.tableLayoutPanel1.Controls) {
				if ((c is TextBox) || (c is LinkLabel)) {
					values.Add(c);
				}else if (c is Label) {
					names.Add(c);
				}
			}
			names.Sort(ExceptionForm.ControlTopComparison);
			values.Sort(ExceptionForm.ControlTopComparison);

			if (names.Count != values.Count) {
				throw new Exception("names と values の個数が合わない．");
			}

			StringBuilder sb = new StringBuilder();
			for (int i=0; i<names.Count; i++) {
				if (sb.Length > 0) {
					sb.Append(Environment.NewLine);
				}
				sb.Append(names[i].Text + ": ");
				sb.Append(values[i].Text);
			}
			Clipboard.SetText(sb.ToString());
		}
		private void llblInnerException_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			using (ExceptionForm ef = new ExceptionForm(this.exception.InnerException, this.AllowAbort)) {
				ef.Text = "内部例外";
				DialogResult result = ef.ShowDialog(this);
				switch (result) {
					case DialogResult.None:
					case DialogResult.Cancel:
						break;
					default:
						this.DialogResult = result;
						this.Close();
						break;
				}
			}
		}
	}
}
