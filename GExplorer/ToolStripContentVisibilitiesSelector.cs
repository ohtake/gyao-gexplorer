﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Yusen.GExplorer {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	public sealed class ToolStripContentVisibilitiesSelector : ToolStripControlHost{
		public event EventHandler ContentVisibilitiesChanged;
		public event EventHandler CloseClick;

		private readonly ContentVisibilitiesSelector selector;
		
		public ToolStripContentVisibilitiesSelector()
			: this(new ContentVisibilitiesSelector()) {
		}
		private ToolStripContentVisibilitiesSelector(ContentVisibilitiesSelector selector)
			: base(selector) {
			this.selector = selector;
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
			EventHandler handler = this.CloseClick;
			if (null != handler) {
				handler(this, e);
			}
		}
		
		public ContentVisibilities ContentVisibilities {
			get { return this.selector.ContentVisibilities; }
			set { this.selector.ContentVisibilities = value; }
		}
	}
}