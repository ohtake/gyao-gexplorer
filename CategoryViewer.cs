using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Process = System.Diagnostics.Process;

namespace Yusen.GExplorer {
	public delegate void CategoryViwerLoadCompletedEventHander
		(CategoryViewer sender, Category category, int numNormal, int numSeries, int numSpecial);
	public delegate void VideoSelectedEvenetHandler(CategoryViewer sender, VideoItem video);
	
	public partial class CategoryViewer : UserControl {
		/// <summary>カテゴリのトップページをロードし終わったら発生</summary>
		public event CategoryViwerLoadCompletedEventHander LoadCompleted;
		/// <summary>ビデオ項目をダブルクリックした場合に発生</summary>
		public event VideoSelectedEvenetHandler VideoSelected;
		
		private Category category;
		private Parser parser;
		
		public CategoryViewer() {
			InitializeComponent();
			this.dgviewTop.DoubleClick += new EventHandler(dgviewTop_DoubleClick);
			this.btnBrowse.Click += new EventHandler(btnBrowse_Click);
			
			this.parser = new Parser();
		}
		
		private void dgviewTop_DoubleClick(object sender, EventArgs e) {
			if(0 >= this.dgviewTop.SelectedCells.Count) return;
			
			VideoItem vi = (VideoItem)this.dgviewTop.Rows[this.dgviewTop.SelectedCells[0].RowIndex].Cells["VideoItem"].Value;
			if(null != this.VideoSelected) {
				this.VideoSelected(this, vi);
			}
		}
		
		public void ViewCategory(CategorySelector sender, Category category) {
			this.category = category;
			
			VideoItem[] items = this.parser.ExtractVideoItemsFromCategoryHtml(category);
			this.dgviewTop.Rows.Clear();
			this.txtSeries.Clear();
			this.txtSpecial.Clear();
			foreach(VideoItem item in items) {
				switch(item.Type) {
					case VideoItemType.Normal:
						int index = this.dgviewTop.Rows.Add();
						DataGridViewCellCollection cells = this.dgviewTop.Rows[index].Cells;
						cells["VideoItem"].Value = item;
						cells["New"].Value = item.IsNew;
						cells["ID"].Value = item.CntId;
						cells["配信終了日"].Value = item.Limit;
						cells["サブジャンル"].Value = item.Subgenre;
						cells["パック"].Value = item.Pack;
						cells["話"].Value = item.Episode;
						cells["リード"].Value = item.Lead;
						break;
					case VideoItemType.PackList:
						this.txtSeries.AppendText(
							item.Subgenre + "\t"
							+ item.Pack + "\t"
							+ item.PackId.ToString() + "\n");
						break;
					case VideoItemType.Special:
						this.txtSpecial.AppendText(
							item.Subgenre + "\t"
							+ item.Pack + "\t"
							+ item.SpecialUri.AbsolutePath + "\n");
						break;
				}
			}
			
			if(null != this.LoadCompleted) {
				int numNormal = 0, numSeries = 0, numSpecial = 0;
				foreach(VideoItem item in items) {
					switch(item.Type) {
						case VideoItemType.Normal:
							numNormal++;
							break;
						case VideoItemType.PackList:
							numSeries++;
							break;
						case VideoItemType.Special:
							numSpecial++;
							break;
					}
				}
				this.LoadCompleted(this, category, numNormal, numSeries, numSpecial);
			}
		}
		
		private void btnBrowse_Click(object sender, EventArgs e) {
			if(null != category.Uri) {
				Process.Start(category.Uri.AbsoluteUri);
			}
		}
	}
}
