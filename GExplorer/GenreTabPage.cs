using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using GGenre = Yusen.GCrawler.GGenre;

namespace Yusen.GExplorer {
	partial class GenreTabPage : TabPage {
		private GGenre genre = null;
		
		public GenreTabPage() {
			InitializeComponent();
			this.tsmiCopyGenreName.Click += delegate {
				Clipboard.SetText(this.Genre.GenreName);
			};
			this.tsmiCopyUri.Click += delegate{
				Clipboard.SetText(this.Genre.TopPageUri.AbsoluteUri);
			};
			this.tsmiCopyGenreNameAndUri.Click += delegate{
				Clipboard.SetText(this.Genre.GenreName + "\n" + this.Genre.TopPageUri.AbsoluteUri);
			};
			this.tsmiBrowseTopWithIE.Click += delegate {
				Utility.BrowseWithIE(this.Genre.TopPageUri);
			};
			this.tsmiBrowseTimetableWithIe.Click += delegate {
				Utility.BrowseWithIE(this.Genre.TimetableUri);
			};
		}
		public GenreTabPage(GGenre genre) : this() {
			this.genre = genre;
			base.Text = genre.GenreName;
		}
		
		public GGenre Genre {
			get { return this.genre; }
		}
		public void ShowContextMenu() {
			this.ShowContextMenu(Control.MousePosition);
		}
		public void ShowContextMenu(Point location) {
			if (null != this.Genre) {
				this.cmsGenre.Location = location;
				this.cmsGenre.Show();
			}
		}
	}
}
