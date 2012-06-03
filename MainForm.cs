using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Process = System.Diagnostics.Process;

namespace Yusen.GExplorer {
	public partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();
			this.Text = Application.ProductName + " " + Application.ProductVersion;
			this.tsslCategoryStat.Text = "";
			
			this.categorySelector1.CategorySelected += new CategorySelectedEventHander(this.categoryViewer1.ViewCategory);
			this.categoryViewer1.VideoSelected += new VideoSelectedEvenetHandler(this.Play);
			this.categoryViewer1.LoadCompleted += new CategoryViwerLoadCompletedEventHander(this.UpdateStatusBar);
		}
		
		private void Play(CategoryViewer sender, VideoItem video) {
			int userId = (int)this.numUserId.Value;
			if(0 == userId) {
				MessageBox.Show(
					"userNo �𐳂������Ȑ\�����܂��傤�D",
					Application.ProductName,
					MessageBoxButtons.OK,
					MessageBoxIcon.Hand);
				return;
			}
			new DetailForm(video, userId).Show();
		}
		
		private void UpdateStatusBar(CategoryViewer sender, Category category,
				int numNormal, int numSeries, int numSpecial) {
			this.tsslCategoryStat.Text =
				"[" + category.Name + "]"
				+ (0 == numNormal ? "" : "    ����: " + numNormal.ToString())
				+ (0 == numSeries ? "" : "    �S�V���[�Y������: " + numSeries.ToString())
				+ (0 == numSpecial ? "" : "    ���W�y�[�W: " + numSpecial.ToString());
		}
	}
}

