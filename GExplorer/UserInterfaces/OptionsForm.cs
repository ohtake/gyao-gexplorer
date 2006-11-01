using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.AppCore;
using System.Reflection;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class OptionsForm : BaseForm, IOptionsFormBindingContract, INotifyPropertyChanged{
		private sealed class OptionsTreeNode : TreeNode {
			private object options;
			public OptionsTreeNode(object options, string name, string desc)
				: base(name) {
				base.ToolTipText = desc;
				this.options = options;
			}
			public object Options {
				get { return this.options; }
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public OptionsForm() {
			InitializeComponent();
		}

		private void OptionsForm_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			if (Program.RootOptions == null) return;
			
			Program.RootOptions.OptionsFormOptions.ApplyFormBaseOptionsAndTrackValues(this);
			Program.RootOptions.OptionsFormOptions.NeutralizeUnspecificValues(this);
			BindingContractUtility.BindAllProperties<OptionsForm, IOptionsFormBindingContract>(this, Program.RootOptions.OptionsFormOptions);
			
			this.tvOptions.Nodes.AddRange(this.TraverseRoot(Program.RootOptions));
		}

		private OptionsTreeNode[] TraverseRoot(RootOptions root) {
			List<OptionsTreeNode> children = new List<OptionsTreeNode>();
			foreach (PropertyInfo pi in typeof(RootOptions).GetProperties()) {
				object[] soAttribs = pi.GetCustomAttributes(typeof(SubOptionsAttribute), false);
				if (soAttribs.Length > 0) {
					SubOptionsAttribute soAttrib = soAttribs[0] as SubOptionsAttribute;
					OptionsTreeNode otn = this.TraverseChild(pi.GetValue(root, null), soAttrib.Name, soAttrib.Description);
					otn.ExpandAll();
					children.Add(otn);
				}
			}
			return children.ToArray();
		}
		private OptionsTreeNode TraverseChild(object options, string name, string desc) {
			OptionsTreeNode otn = new OptionsTreeNode(options, name, desc);
			List<TreeNode> children = new List<TreeNode>();
			foreach (PropertyInfo pi in options.GetType().GetProperties()) {
				object[] soAttribs = pi.GetCustomAttributes(typeof(SubOptionsAttribute), false);
				if (soAttribs.Length > 0) {
					SubOptionsAttribute soAttrib = soAttribs[0] as SubOptionsAttribute;
					children.Add(this.TraverseChild(pi.GetValue(options, null), soAttrib.Name, soAttrib.Description));
				}
			}
			otn.Nodes.AddRange(children.ToArray());
			return otn;
		}

		private void tvOptions_AfterSelect(object sender, TreeViewEventArgs e) {
			this.pgOptions.SelectedObject = (e.Node as OptionsTreeNode).Options;
		}

		private void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (null != handler) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#region IOptionsFormBindingContract Members
		public int TreeViewWidth {
			get { return this.splitContainer1.SplitterDistance;}
			set {
				this.splitContainer1.SplitterDistance = value;
				this.OnPropertyChanged("TreeViewWidth");
			}
		}

		#endregion

		private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e) {
			this.OnPropertyChanged("TreeViewWidth");
		}
	}

	interface IOptionsFormBindingContract : IBindingContract {
		/// <summary>負数で未定義</summary>
		int TreeViewWidth { get;set;}
		bool TopMost { get;set;}
	}

	public sealed class OptionsFormOptions : FormOptionsBase , IOptionsFormBindingContract{
		public OptionsFormOptions() {
		}

		#region IOptionsFormBindingContract Members
		private int treeViewWidth = -1;
		[Category("スプリッタの位置")]
		[DisplayName("ツリービューの幅")]
		[Description("ツリービューの幅を指定します．")]
		[DefaultValue(-1)]
		public int TreeViewWidth {
			get { return this.treeViewWidth; }
			set { this.treeViewWidth = value; }
		}

		private bool topMost = false;
		[Category("Zオーダ")]
		[DisplayName("最前面")]
		[Description("フォームを最前面に表示します．")]
		[DefaultValue(false)]
		public bool TopMost {
			get { return this.topMost; }
			set { this.topMost = value; }
		}
		#endregion
		
		internal void NeutralizeUnspecificValues(OptionsForm optionsForm) {
			if (this.TreeViewWidth < 0) this.TreeViewWidth = optionsForm.TreeViewWidth;
		}
	}
}

