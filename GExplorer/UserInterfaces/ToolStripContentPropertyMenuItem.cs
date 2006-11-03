using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;
using System.ComponentModel;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.UserInterfaces {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("PropertySelected")]
	public sealed class ToolStripContentPropertyMenuItem : ToolStripMenuItem {
		private sealed class ToolStripMenuItemWithPropertyInfo : ToolStripMenuItem{
			private PropertyInfo propertyInfo;
			public ToolStripMenuItemWithPropertyInfo(PropertyInfo propertyInfo) : base(propertyInfo.Name){
				this.propertyInfo = propertyInfo;
			}
			public PropertyInfo PropertyInfo{
				get{return this.propertyInfo;}
			}
		}

		public event EventHandler PropertySelected;

		private PropertyInfo lastSelectedPropertyInfo;

		public ToolStripContentPropertyMenuItem()
			: base("ToolStripContentPropertyMenuItem") {
			if (base.DesignMode) return;
			this.CreateSubMenus();
		}
		
		private void CreateSubMenus() {
			SortedDictionary<string, List<ToolStripItem>> cates = new SortedDictionary<string, List<ToolStripItem>>();
			
			foreach (PropertyInfo pi in typeof(GContentClass).GetProperties(BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public)) {
				//Browsableのもののみ
				object[] bAttribs = pi.GetCustomAttributes(typeof(BrowsableAttribute), false);
				if (bAttribs.Length > 0 && !(bAttribs[0] as BrowsableAttribute).Browsable) {
					continue;
				}
				//カテゴリごとに分類
				string category = string.Empty;
				object[] cAttribs = pi.GetCustomAttributes(typeof(CategoryAttribute), false);
				if (cAttribs.Length > 0) {
					category = (cAttribs[0] as CategoryAttribute).Category;
				}
				List<ToolStripItem> items;
				if (!cates.TryGetValue(category, out items)) {
					items = new List<ToolStripItem>();
					cates.Add(category, items);
				}
				//プロパティの追加
				ToolStripMenuItemWithPropertyInfo menu = new ToolStripMenuItemWithPropertyInfo(pi);
				menu.Click += new EventHandler(menu_Click);
				items.Add(menu);
			}

			//メニュー作成
			List<ToolStripItem> cateMenus = new List<ToolStripItem>();
			foreach (KeyValuePair<string, List<ToolStripItem>> pair in cates) {
				pair.Value.Sort(new Comparison<ToolStripItem>(delegate(ToolStripItem a, ToolStripItem b) {
					return a.Text.CompareTo(b.Text);
				}));
				ToolStripMenuItem cateMenu = new ToolStripMenuItem(pair.Key);
				cateMenu.DropDownItems.AddRange(pair.Value.ToArray());
				cateMenus.Add(cateMenu);
			}
			base.DropDownItems.AddRange(cateMenus.ToArray());
		}
		
		private void menu_Click(object sender, EventArgs e) {
			ToolStripMenuItemWithPropertyInfo sender2 = sender as ToolStripMenuItemWithPropertyInfo;
			this.OnPropertySelected(sender2.PropertyInfo);
		}

		private void OnPropertySelected(PropertyInfo pi) {
			this.lastSelectedPropertyInfo = pi;
			EventHandler handler = this.PropertySelected;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}

		public PropertyInfo LastSelectedPropertyInfo {
			get { return this.lastSelectedPropertyInfo; }
		}
	}
}
