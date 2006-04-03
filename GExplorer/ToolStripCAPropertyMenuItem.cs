using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;
using System.ComponentModel;

namespace Yusen.GExplorer {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("PropertySelected")]
	public sealed class ToolStripCAPropertyMenuItem : ToolStripMenuItem {
		private sealed class ToolStripMenuItemWithPropertyInfo : ToolStripMenuItem{
			private PropertyInfo propertyInfo;
			public ToolStripMenuItemWithPropertyInfo(PropertyInfo propertyInfo) : base(propertyInfo.Name){
				this.propertyInfo = propertyInfo;
			}
			public PropertyInfo PropertyInfo{
				get{return this.propertyInfo;}
			}
		}

		[Description("サブメニューのプロパティ名が選択された")]
		public event EventHandler<CAPropertySelectedEventArgs> PropertySelected;

		public ToolStripCAPropertyMenuItem() : base("ToolStripCAPropertyMenuItem") {
			if (base.DesignMode) return;
			this.CreateSubMenus();
		}
		
		private void CreateSubMenus() {
			Dictionary<string, ToolStripItem> categories = new Dictionary<string, ToolStripItem>();
			
			foreach (PropertyInfo pi in typeof(ContentAdapter).GetProperties(BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public)) {
				//Browsableのもののみ
				object[] bAttribs = pi.GetCustomAttributes(typeof(BrowsableAttribute), false);
				if (bAttribs.Length > 0 && !(bAttribs[0] as BrowsableAttribute).Browsable) {
					continue;
				}
				//カテゴリごとにサブメニュー化
				string category = string.Empty;
				object[] cAttribs = pi.GetCustomAttributes(typeof(CategoryAttribute), false);
				if (cAttribs.Length > 0) {
					category = (cAttribs[0] as CategoryAttribute).Category;
				}
				if (!categories.ContainsKey(category)) {
					categories.Add(category, new ToolStripMenuItem(category));
				}
				//メニューの作成と追加
				ToolStripMenuItemWithPropertyInfo mi = new ToolStripMenuItemWithPropertyInfo(pi);
				mi.Click += delegate(object sender, EventArgs e) {
					ToolStripMenuItemWithPropertyInfo tsmiwpi = sender as ToolStripMenuItemWithPropertyInfo;
					EventHandler<CAPropertySelectedEventArgs> handler = this.PropertySelected;
					if (null == handler) {
						throw new InvalidOperationException("PropertySelected イベントがハンドルされていない．");
					}
					handler(this, new CAPropertySelectedEventArgs(tsmiwpi.PropertyInfo));
				};
				(categories[category] as ToolStripMenuItem).DropDownItems.Add(mi);
			}
			
			base.DropDownItems.AddRange(new List<ToolStripItem>(categories.Values).ToArray());
		}
	}
}
