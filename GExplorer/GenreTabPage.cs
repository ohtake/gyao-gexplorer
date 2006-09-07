using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Yusen.GCrawler;

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
			this.tsmiCatalogPackage.Click += delegate{
				List<PackageAdapter> pas = new List<PackageAdapter>();
				foreach (GPackage pac in Cache.Instance.ResultsDictionary[this.genre].Packages) {
					pas.Add(new PackageAdapter(pac));
				}
				BrowserForm.Browse(pas, false);
			};
			this.tsmiCatalogContent.Click += delegate {
				List<ContentAdapter> cas = new List<ContentAdapter>();
				foreach (GPackage pac in Cache.Instance.ResultsDictionary[this.genre].Packages) {
					cas.AddRange(new PackageAdapter(pac).ContentAdapters);
				}
				BrowserForm.Browse(cas);
			};
			this.tsmiCatalogBoth.Click += delegate {
				List<PackageAdapter> pas = new List<PackageAdapter>();
				foreach (GPackage pac in Cache.Instance.ResultsDictionary[this.genre].Packages) {
					pas.Add(new PackageAdapter(pac));
				}
				BrowserForm.Browse(pas, true);
			};
			this.tsmiBrowseTimetableRecentlyUpdatedFirst.Click += delegate {
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
				switch (MessageBox.Show(string.Format("ジャンル「{0}」のクロール結果を破棄しますか？", this.genre.GenreName), "クロール結果の破棄", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
					case DialogResult.Yes:
						break;
					default:
						return;
				}
				Cache.Instance.ResultsDictionary.Remove(this.Genre);
				if (null != this.ResultRemoved) {
					this.ResultRemoved(this, EventArgs.Empty);
				}
			};
		}
		
		public GenreTabPage(GGenre genre) : this(){
			this.genre = genre;
			base.Text = genre.GenreName;
			base.ToolTipText = string.Format("{0}\n{1}", genre.GenreName, genre.TopPageUri.AbsoluteUri);
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
			bool hasResult = Cache.Instance.ResultsDictionary.ContainsKey(this.Genre);
			this.tsmiCatalog.Enabled = hasResult;
			this.tsmiRemoveCrawlResult.Enabled = hasResult;
		}
	}
}
