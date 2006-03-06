using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	public partial class SplashForm : Form {
		private bool endFlag = false;

		public SplashForm() {
			InitializeComponent();
			this.Text = Program.ApplicationName;
		}
		
		public void Initialize(string message, int maxProg) {
			this.lblMessage.Text = message;
			this.txtLog.Clear();
			this.progbTotal.Value = 0;
			this.progbTotal.Maximum = maxProg;
			this.Show();
			Application.DoEvents();
		}
		public void StepProgress(string stepMessage) {
			this.txtLog.AppendText(stepMessage + "\r\n");
			this.progbTotal.Value++;
			Application.DoEvents();
		}
		public void EndProgress() {
			this.endFlag = true;
			this.Close();
		}

		private void btnAbort_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void SplashForm_FormClosing(object sender, FormClosingEventArgs e) {
			if(!this.endFlag) {
				Environment.Exit(1);
			}
		}
	}
}