using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Xml.Serialization;
using Yusen.GExplorer.UserInterfaces;

namespace Yusen.GExplorer.OldApp {
	public sealed partial class NgFavContentsEditor : BaseForm{
		public NgFavContentsEditor() {
			InitializeComponent();
		}
		private void NgContentsEditor_Load(object sender, EventArgs e) {
			this.editorNg.SetManager(ContentPredicatesManager.NgManager);
			this.editorFav.SetManager(ContentPredicatesManager.FavManager);
		}
	}
}
