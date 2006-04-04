using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("GenreSelected")]
	public sealed class ToolStripGenreMenuItem : ToolStripMenuItem{
		private sealed class ToolStripMenuItemWithGenre : ToolStripMenuItem {
			private readonly GGenre genre;
			public ToolStripMenuItemWithGenre(GGenre genre) : base(genre.GenreName){
				this.genre = genre;
			}
			public GGenre Genre {
				get { return this.genre; }
			}
		}
		
		public ToolStripGenreMenuItem() : base("ToolStripGenreMenuItem"){
		}

		public event EventHandler<GenreMenuItemSelectedEventArgs> GenreSelected;
		private GenreMenuVisibility menuVisibility = GenreMenuVisibility.None;

		[DefaultValue(GenreMenuVisibility.None)]
		public GenreMenuVisibility MenuVisibility {
			get { return this.menuVisibility; }
			set {
				if (this.menuVisibility == value) return;
				this.menuVisibility = value;
				
				if (base.DesignMode) return;
				
				bool showCrawlable = false;
				bool showUnCrawlable = false;
				switch (value) {
					case GenreMenuVisibility.Crawlable:
						showCrawlable = true;
						break;
					case GenreMenuVisibility.UnCrawlable:
						showUnCrawlable = true;
						break;
					case GenreMenuVisibility.All:
						showCrawlable = true;
						showUnCrawlable = true;
						break;
				}

				base.DropDownItems.Clear();
				List<ToolStripItem> items = new List<ToolStripItem>();
				foreach (GGenre genre in GGenre.AllGenres) {
					if (showCrawlable && genre.IsCrawlable) {
						items.Add(this.CreateSubmenuItem(genre));
					} else if(showUnCrawlable && !genre.IsCrawlable){
						items.Add(this.CreateSubmenuItem(genre));
					}
				}
				
				base.DropDownItems.AddRange(items.ToArray());
				base.Enabled = base.HasDropDownItems;
			}
		}

		private ToolStripMenuItemWithGenre CreateSubmenuItem(GGenre genre) {
			ToolStripMenuItemWithGenre tsmiwg = new ToolStripMenuItemWithGenre(genre);
			tsmiwg.Click += delegate(object sender, EventArgs e) {
				ToolStripMenuItemWithGenre sender2 = sender as ToolStripMenuItemWithGenre;
				EventHandler<GenreMenuItemSelectedEventArgs> handler = this.GenreSelected;
				if (null != handler) {
					handler(this, new GenreMenuItemSelectedEventArgs(sender2.Genre));
				}
			};
			return tsmiwg;
		}
	}

	public sealed class GenreMenuItemSelectedEventArgs : EventArgs{
		private readonly GGenre selectedGenre;
		public GenreMenuItemSelectedEventArgs(GGenre selectedGenre) {
			this.selectedGenre = selectedGenre;
		}
		public GGenre SelectedGenre {
			get { return this.selectedGenre; }
		}
	}

	[Flags]
	public enum GenreMenuVisibility {
		None = 0,
		Crawlable = 1,
		UnCrawlable = 2,
		All = 3,
	}
}
