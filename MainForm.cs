using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Process = System.Diagnostics.Process;

namespace Yusen.GExplorer {
	partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();
			Utility.AddHelpMenu(this.menuStrip1);
			this.Icon = Utility.GetGExplorerIcon();
			this.Text = Application.ProductName + " " + Application.ProductVersion;
			this.tsslCategoryStat.Text = "";
			//���j���[����
			this.tsmiQuit.Click += new EventHandler(delegate(object sender, EventArgs e) {
				this.Close();
			});
			//���̃C�x���g
			this.genreViewer1.Refreshed += new GenreListViewRefleshedEventHandler(
				delegate(GenreListView sender, GGenre genre, int cntCount) {
					this.tsslCategoryStat.Text =
						"[" + genre.GenreName + "] " + cntCount.ToString() + "�̃I�u�W�F�N�g";
				});
			GGenre.LoadingPackages += new LoadingPackagesEventHandler(
				delegate(GGenre sender, GPackage package, int nume, int denom) {
					this.tspbPackages.Maximum = denom;
					this.tspbPackages.Value = nume;
					this.tspbPackages.Text = "[" + sender.GenreName + "] " + nume.ToString() + "/" + denom.ToString();
				});
		}
		
	}
}

