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
		private sealed class CategoryHelper {
			private string name;
			private SortedDictionary<string, PropertyInfo> pis = new SortedDictionary<string, PropertyInfo>();
			public CategoryHelper(string name) {
				this.name = name;
			}
			public void AddProperty(PropertyInfo pi) {
				this.pis.Add(pi.Name, pi);
			}
			public ToolStripMenuItem CreateCategoryMenu(ToolStripCAPropertyMenuItem owner) {
				ToolStripMenuItem catMenu = new ToolStripMenuItem(this.name);
				List<ToolStripItem> subMenus = new List<ToolStripItem>();
				foreach (PropertyInfo pi in this.pis.Values) {
					ToolStripMenuItemWithPropertyInfo tsmiwpi = new ToolStripMenuItemWithPropertyInfo(pi);
					tsmiwpi.Click += delegate(object sender, EventArgs e) {
						ToolStripMenuItemWithPropertyInfo sender2 = sender as ToolStripMenuItemWithPropertyInfo;
						EventHandler<CAPropertySelectedEventArgs> handler = owner.PropertySelected;
						if (null == handler) {
							throw new InvalidOperationException("PropertySelected イベントがハンドルされていない．");
						}
						handler(this, new CAPropertySelectedEventArgs(sender2.PropertyInfo));
					};
					subMenus.Add(tsmiwpi);
				}
				catMenu.DropDownItems.AddRange(subMenus.ToArray());
				return catMenu;
			}
		}
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
			SortedDictionary<string, CategoryHelper> categories = new SortedDictionary<string, CategoryHelper>();
			
			foreach (PropertyInfo pi in typeof(ContentAdapter).GetProperties(BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public)) {
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
				if (!categories.ContainsKey(category)) {
					categories.Add(category, new CategoryHelper(category));
				}
				//プロパティの追加
				(categories[category] as CategoryHelper).AddProperty(pi);
			}

			//メニュー作成
			List<ToolStripItem> menuItems = new List<ToolStripItem>();
			foreach (CategoryHelper ch in categories.Values) {
				menuItems.Add(ch.CreateCategoryMenu(this));
			}
			base.DropDownItems.AddRange(menuItems.ToArray());
		}
	}
}
