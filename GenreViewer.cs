using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	partial class GenreViewer : UserControl {
		public event GenreListViewRefleshedEventHandler Refreshed;
		
		public GenreViewer() {
			InitializeComponent();
			
			this.tabControl1.TabPages.Clear();
			foreach(GGenre g in GGenre.AllGenres) {
				TabPage tab = new TabPage(g.GenreName);
				tab.Tag = g;
				this.tabControl1.TabPages.Add(tab);
			}
			this.tabControl1.SelectedIndex = -1;

			this.tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
			this.tabControl1.DoubleClick += new EventHandler(tabControl1_DoubleClick);
			//ÉCÉxÉìÉgÇÃçƒëó
			this.genreListView1.Refreshed += new GenreListViewRefleshedEventHandler(
				delegate(GenreListView sender, GGenre genre, int cntCount) {
					if(null != this.Refreshed) this.Refreshed(sender, genre, cntCount);
				});
		}
		
		void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
			GGenre genre = (GGenre)this.tabControl1.SelectedTab.Tag;
			this.genreListView1.Display(genre);
		}
		
		void tabControl1_DoubleClick(object sender, EventArgs e) {
			GGenre genre = (GGenre)this.tabControl1.SelectedTab.Tag;
			genre.FetchAll();
			this.genreListView1.Display(genre);
		}
	}
}

