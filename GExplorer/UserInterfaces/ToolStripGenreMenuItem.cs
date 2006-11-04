using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using Yusen.GExplorer.GyaoModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Yusen.GExplorer.UserInterfaces {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("GenreSelected")]
	public sealed class ToolStripGenreMenuItem : ToolStripMenuItem{
		private sealed class ToolStripMenuItemWithGenre : ToolStripMenuItem {
			private readonly GGenreClass genre;

			public ToolStripMenuItemWithGenre(GGenreClass genre)
				: base(genre.GenreName) {
				this.genre = genre;
			}
			public GGenreClass Genre {
				get { return this.genre; }
			}
			protected override void OnPaint(PaintEventArgs e) {
				Color colorGfc = this.genre.GenreColor;
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
					using (SolidBrush brushText = new SolidBrush(SystemColors.ControlText)) {
						e.Graphics.FillRectangle(brushIcon, rectIcon);
						e.Graphics.DrawString(base.Text, base.Font, brushText, ptText);
					}
				}
			}
		}

		public event EventHandler GenreSelected;
		private GGenreClass lastSelectedGenre;
		
		public ToolStripGenreMenuItem()
			: base("ToolStripGenreMenuItem") {
			
			base.DropDownItems.Add("ジャンル名1");
			base.DropDownItems.Add("ジャンル名2");

			if (base.DesignMode) return;
			if (null == Program.CacheController) return;

			List<ToolStripItem> items = new List<ToolStripItem>();
			foreach (GGenreClass genre in Program.CacheController.GetEnumerableOfAllGenres()) {
				ToolStripMenuItemWithGenre tsmiwg = new ToolStripMenuItemWithGenre(genre);
				tsmiwg.Click += new EventHandler(tsmiwg_Click);
				items.Add(tsmiwg);
			}
			base.DropDownItems.Clear();
			base.DropDownItems.AddRange(items.ToArray());
		}

		private void tsmiwg_Click(object sender, EventArgs e) {
			ToolStripMenuItemWithGenre tsmiwg = sender as ToolStripMenuItemWithGenre;
			this.lastSelectedGenre = tsmiwg.Genre;
			EventHandler handler = this.GenreSelected;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		public GGenreClass LastSelectedGenre {
			get { return this.lastSelectedGenre; }
		}
	}
}
