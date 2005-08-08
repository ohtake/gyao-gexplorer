using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using GGenre = Yusen.GCrawler.GGenre;

namespace Yusen.GExplorer {
	partial class GenreTabControl : TabControl {
		public event EventHandler<GenreTabPageEventArgs> GenreSelected;
		public event EventHandler<GenreTabPageEventArgs> GenreDoubleClick;

		public GenreTabControl() {
			InitializeComponent();
		}
		
		public void AddGenre(GGenre genre) {
			GenreTabPage gtp = new GenreTabPage(genre);
			base.TabPages.Add(gtp);
		}
		
		public GGenre SelectedGenre {
			get {
				GenreTabPage selGtp = base.SelectedTab as GenreTabPage;
				if (null == selGtp) return null;
				return selGtp.Genre;
			}
			set {
				foreach (TabPage tp in base.TabPages) {
					GenreTabPage gtp = tp as GenreTabPage;
					if (null == gtp) continue;
					if(value == gtp.Genre){
						base.SelectedTab = tp;
						break;
					}
				}
			}
		}

		private void GenreTabControl_MouseClick(object sender, MouseEventArgs e) {
			switch (e.Button) {
				case MouseButtons.Right:
					for (int i=0; i<base.TabCount; i++) {
						Rectangle tabRect = base.GetTabRect(i);
						if (tabRect.Contains(e.Location)) {
							GenreTabPage gtp = base.TabPages[i] as GenreTabPage;
							if (null != gtp) {
								gtp.ShowContextMenu();
							}
							base.SelectedIndex = i;
						}
					}
					break;
			}
		}

		private void GenreTabControl_DoubleClick(object sender, EventArgs e) {
			if (null != this.GenreDoubleClick) {
				GGenre genre = this.SelectedGenre;
				if (null != genre) {
					this.GenreDoubleClick(this, new GenreTabPageEventArgs(genre));
				}
			}
		}

		private void GenreTabControl_SelectedIndexChanged(object sender, EventArgs e) {
			if (null != this.GenreSelected) {
				GGenre genre = this.SelectedGenre;
				if (null != genre) {
					this.GenreSelected(this, new GenreTabPageEventArgs(genre));
				}
			}
		}
	}
	
	class GenreTabPageEventArgs : EventArgs {
		private readonly GGenre genre;
		public GenreTabPageEventArgs(GGenre genre) {
			this.genre = genre;
		}
		public GGenre Genre {
			get { return this.genre; }
		}
	}
}
