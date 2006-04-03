using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using GGenre = Yusen.GCrawler.GGenre;

namespace Yusen.GExplorer {
	public sealed partial class GenreTabPage : TabPage {
		public event EventHandler CrawlRequested;
		public event EventHandler ResultRemoved;
		private GGenre genre = null;
		
		public GenreTabPage() {
			InitializeComponent();
			this.tsmiCrawl.Font = new Font(this.tsmiCrawl.Font, FontStyle.Bold);
			this.tsmiCrawl.Click += delegate {
				if (null != this.CrawlRequested) {
					this.CrawlRequested(this, EventArgs.Empty);
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
			this.tsmiRemoveCrawlResult.Click += delegate {
				Cache.Instance.ResultsDictionary.Remove(this.Genre);
				if (null != this.ResultRemoved) {
					this.ResultRemoved(this, EventArgs.Empty);
				}
			};
		}
		
		public GenreTabPage(GGenre genre) : this(genre, genre.GenreName, string.Empty) {
		}
		public GenreTabPage(GGenre genre, string displayText, string tooltipText) : this() {
			this.genre = genre;
			base.Text = displayText;
			base.ToolTipText = tooltipText;
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

		private void cmsGenre_Opening(object sender, CancelEventArgs e) {
			this.tsmiRemoveCrawlResult.Enabled = Cache.Instance.ResultsDictionary.ContainsKey(this.Genre);
		}
	}
}
