using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yusen.GExplorer{
	public delegate void CategorySelectedEventHander(CategorySelector sender, Category category);
	
	public partial class CategorySelector : UserControl{
		/// <summary>カテゴリが選択されたときに発生するイベント</summary>
		public event CategorySelectedEventHander CategorySelected;
		
		public CategorySelector() {
			InitializeComponent();
			
			foreach(Category c in Category.ListCategories()) {
				this.comboCategory.Items.Add(c);
			}
			this.comboCategory.SelectedIndexChanged += new EventHandler(comboCategory_SelectedIndexChanged);
		}
		
		void comboCategory_SelectedIndexChanged(object sender, EventArgs e){
			Category selectedCat = (Category)((ComboBox)sender).SelectedItem;
			if(null != this.CategorySelected) {
				this.CategorySelected(this, selectedCat);
			}
		}
	}
}
