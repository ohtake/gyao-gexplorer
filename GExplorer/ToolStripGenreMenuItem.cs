using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using Yusen.GCrawler;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Yusen.GExplorer {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("GenreSelected")]
	public sealed class ToolStripGenreMenuItem : ToolStripMenuItem{
		private sealed class ToolStripMenuItemWithGenre : ToolStripMenuItem {
			private readonly GGenre genre;
			private bool genreColored = true;
			
			public ToolStripMenuItemWithGenre(GGenre genre) : base(genre.GenreName){
				this.genre = genre;
			}
			public GGenre Genre {
				get { return this.genre; }
			}
			public bool GenreColored {
				set { this.genreColored = value; }
			}
			protected override void OnPaint(PaintEventArgs e) {
				if (!this.genreColored) {
					base.OnPaint(e);
					return;
				}
				Color colorGfc = this.genre.GenreForeColor;
				Point ptText = new Point(e.ClipRectangle.Left + 25 + 8, e.ClipRectangle.Top + 4);
				if (base.Selected) {
					using (SolidBrush brushBack = new SolidBrush(colorGfc))
					using (SolidBrush brushText = new SolidBrush(Color.White)) {
						e.Graphics.FillRectangle(brushBack, e.ClipRectangle);
						e.Graphics.DrawString(base.Text, base.Font, brushText, ptText);
					}
				} else {
					Rectangle rectIcon = new Rectangle(e.ClipRectangle.Location, new Size(25, e.ClipRectangle.Height));
					using (LinearGradientBrush brushIcon = new LinearGradientBrush(rectIcon, Color.White, colorGfc, LinearGradientMode.Horizontal))
					using (SolidBrush brushText = new SolidBrush(SystemColors.ControlText)){
						e.Graphics.FillRectangle(brushIcon, rectIcon);
						e.Graphics.DrawString(base.Text, base.Font, brushText, ptText);
					}
				}
			}
		}
		
		public ToolStripGenreMenuItem() : base("ToolStripGenreMenuItem"){
		}

		public event EventHandler<GenreMenuItemSelectedEventArgs> GenreSelected;
		private GenreMenuVisibility menuVisibility = GenreMenuVisibility.None;
		private bool hasAvaibaleSubmenus = false;
		private bool genreColored = true;

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

				List<ToolStripItem> items = new List<ToolStripItem>();
				foreach (GGenre genre in GGenre.AllGenres) {
					if (showCrawlable && genre.IsCrawlable) {
						items.Add(this.CreateSubmenuItem(genre));
					} else if(showUnCrawlable && !genre.IsCrawlable){
						items.Add(this.CreateSubmenuItem(genre));
					}
				}

				//base.DropDownItems.Clear();
				if (items.Count > 0) {
					this.hasAvaibaleSubmenus = true;
					foreach (ToolStripItem tsi in base.DropDownItems) {
						items.Add(tsi);
					}
					base.DropDownItems.Clear();
					base.DropDownItems.AddRange(items.ToArray());
				} else {
					this.hasAvaibaleSubmenus = false;
					ToolStripMenuItem tsmi = new ToolStripMenuItem("(なし)");
					tsmi.Enabled = false;
					base.DropDownItems.Insert(0, tsmi);
				}
			}
		}
		
		[DefaultValue(true)]
		public bool GenreColored {
			get { return this.genreColored; }
			set {
				if (this.genreColored != value) {
					this.genreColored = value;
					foreach (ToolStripItem tsi in this.DropDownItems) {
						ToolStripMenuItemWithGenre tsmiwg = tsi as ToolStripMenuItemWithGenre;
						if (null != tsmiwg) {
							tsmiwg.GenreColored = value;
						}
					}
				}
			}
		}
		
		public bool HasAvailableSubmenus {
			get { return this.hasAvaibaleSubmenus; }
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
