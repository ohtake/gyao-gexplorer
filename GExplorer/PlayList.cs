using System;
using System.Collections.Generic;
using System.Text;

namespace Yusen.GExplorer {
	public class PlayList {
		private PlayList instance = new PlayList();
		public PlayList Instance {
			get { return this.instance; }
		}
		
		
		private List<ContentAdapter> contents;
		
		
		
	}
	
	class PlayListAddedEventArgs : EventArgs {
		ContentAdapter addedCont;
		public PlayListAddedEventArgs(ContentAdapter addedCont) {
			this.addedCont = addedCont;
		}
		public ContentAdapter AddedContent {
			get { return this.addedCont; }
		}
	}
}

