using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using GGenre = Yusen.GCrawler.GGenre;

namespace Yusen.GExplorer {
	partial class GenreTabPage : TabPage {
		public event EventHandler<GenreReloadRequestedEventArgs> ReloadRequested;
		private GGenre genre = null;
		
		public GenreTabPage() {
			InitializeComponent();
			this.tsmiReload.Font = new Font(this.tsmiReload.Font, FontStyle.Bold);
			this.tsmiReload.Click += delegate {
				if (null != this.ReloadRequested) {
					this.ReloadRequested(this, new GenreReloadRequestedEventArgs(this.genre));
				}
			};
			this.tsmiBrowseTop.Click += delegate {
				Utility.Browse(this.Genre.TopPageUri);
			};
			this.tsmiBrowseTimetableRecentlyUpdatedFirst.Click +=delegate {
				Utility.Browse(this.Genre.TimetableRecentlyUpdatedFirstUri);
			};
			this.tsmiBrowseTimetableDeadlineNearFirst.Click += delegate {
				Utility.Browse(this.Genre.TimetableDeadlineNearFirstUri);
			};
			this.tsmiCopyGenreName.Click += delegate {
				Clipboard.SetText(this.Genre.GenreName);
			};
			this.tsmiCopyUri.Click += delegate{
				Clipboard.SetText(this.Genre.TopPageUri.AbsoluteUri);
			};
			this.tsmiCopyGenreNameAndUri.Click += delegate{
				Clipboard.SetText(this.Genre.GenreName + Environment.NewLine + this.Genre.TopPageUri.AbsoluteUri);
			};
		}
		public GenreTabPage(GGenre genre) : this() {
			this.genre = genre;
			base.Text = genre.GenreName;
			//base.ToolTipText = genre.GenreName + Environment.NewLine + genre.TopPageUri.AbsoluteUri;
		}
		
		public GGenre Genre {
			get { return this.genre; }
		}
		public void ShowContextMenu() {
			this.ShowContextMenu(Control.MousePosition);
		}
		public void ShowContextMenu(Point location) {
			if (null != this.Genre) {
				this.cmsGenre.Show(location);
			}
		}
	}

	class GenreReloadRequestedEventArgs : EventArgs {
		private readonly GGenre genre;
		public GenreReloadRequestedEventArgs(GGenre genre) {
			this.genre = genre;
		}
		public GGenre Genre {
			get { return this.genre; }
		}
	}
}
