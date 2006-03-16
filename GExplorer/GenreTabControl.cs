﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using GGenre = Yusen.GCrawler.GGenre;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	sealed partial class GenreTabControl : TabControl {
		public event EventHandler<GenreSelectedEventArgs> GenreSelected;

		public GenreTabControl() {
			InitializeComponent();
		}

		public void AddGenre(GGenre genre) {
			GenreTabPage gtp = new GenreTabPage(genre);
			gtp.CrawlRequested += new EventHandler(gtp_ReloadRequested);
			gtp.ResultRemoved += new EventHandler(gtp_ResultRemoved);
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
					if(gtp.Genre.Equals(value)){
						base.SelectedTab = tp;
						return;
					}
				}
				this.SelectedTab = null;
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
								gtp.ShowContextMenu(this.PointToScreen(e.Location));
							}
						}
					}
					break;
				case MouseButtons.Middle:
					for (int i=0; i<base.TabCount; i++) {
						Rectangle tabRect = base.GetTabRect(i);
						if (tabRect.Contains(e.Location)) {
							this.TabPages.RemoveAt(i);
						}
					}
					break;
			}
		}

		private void GenreTabControl_DoubleClick(object sender, EventArgs e) {
			this.OnGenreSelected(true);
		}
		
		private void GenreTabControl_SelectedIndexChanged(object sender, EventArgs e) {
			this.OnGenreSelected(false);
		}

		private void gtp_ReloadRequested(object sender, EventArgs e) {
			this.SelectedGenre = (sender as GenreTabPage).Genre;
			this.OnGenreSelected(true);
		}
		private void gtp_ResultRemoved(object sender, EventArgs e) {
			this.SelectedGenre = (sender as GenreTabPage).Genre;
			this.OnGenreSelected(false);
		}

		private void OnGenreSelected(bool forceReload) {
			if (null != this.GenreSelected) {
				GGenre genre = this.SelectedGenre;
				if (null != genre) {
					this.GenreSelected(this, new GenreSelectedEventArgs(genre, forceReload));
				}
			}
		}
	}

	class GenreSelectedEventArgs : EventArgs {
		private readonly GGenre genre;
		private readonly bool forceReload;

		public GenreSelectedEventArgs(GGenre genre, bool forceReload) {
			this.genre = genre;
			this.forceReload = forceReload;
		}
		public GGenre Genre {
			get { return this.genre; }
		}
		public bool ForceReload {
			get { return this.forceReload; }
		}
	}
}
