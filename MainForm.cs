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
					"userNo を正しく自己申告しましょう．",
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
				+ (0 == numNormal ? "" : "    見る: " + numNormal.ToString())
				+ (0 == numSeries ? "" : "    全シリーズを見る: " + numSeries.ToString())
				+ (0 == numSpecial ? "" : "    特集ページ: " + numSpecial.ToString());
		}
	}
}

