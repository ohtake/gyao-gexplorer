using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Yusen.GExplorer.UserInterfaces {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	public sealed class ToolStripContentVisibilitiesSelector : ToolStripControlHost {
		public event EventHandler ContentVisibilitiesChanged;
		
		private readonly ContentVisibilitiesSelector selector;
		
		public ToolStripContentVisibilitiesSelector()
			: this(new ContentVisibilitiesSelector()) {
		}
		private ToolStripContentVisibilitiesSelector(ContentVisibilitiesSelector selector)
			: base(selector) {
			this.selector = selector;
			if (base.DesignMode) return;
			
			this.selector.ContentVisibilitiesChanged += new EventHandler(selector_ContentVisibilitiesChanged);
			this.selector.CloseClick += new EventHandler(selector_CloseClick);
		}
		
		private void selector_ContentVisibilitiesChanged(object sender, EventArgs e) {
			EventHandler handler = this.ContentVisibilitiesChanged;
			if (null != handler) {
				handler(this, e);
			}
		}
		private void selector_CloseClick(object sender, EventArgs e) {
			base.PerformClick();
		}

		public ContentVisibilities ContentVisibilities {
			get { return this.selector.ContentVisibilities; }
			set { this.selector.ContentVisibilities = value; }
		}
	}
}
