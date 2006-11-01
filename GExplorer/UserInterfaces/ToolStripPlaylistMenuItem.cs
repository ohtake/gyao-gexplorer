using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Yusen.GExplorer.AppCore;

namespace Yusen.GExplorer.UserInterfaces {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	[DefaultEvent("PlaylistSelected")]
	public sealed class ToolStripPlaylistMenuItem : ToolStripMenuItem{
		private const string NewPlaylistName = "新規プレイリスト";
		private sealed class ToolStripMenuItemWithPlaylist : ToolStripMenuItem {
			private readonly Playlist playlist;

			public ToolStripMenuItemWithPlaylist(Playlist pl)
				: base(pl.Name) {
				this.playlist = pl;
			}
			public Playlist Playlist {
				get { return this.playlist; }
			}
		}
		
		public event EventHandler PlaylistSelected;
		private Playlist lastSelectedPlaylist;

		public ToolStripPlaylistMenuItem()
			: base("ToolStripPlaylistMenuItem") {
			
			base.DropDownItems.Add("プレイリスト名1");
			base.DropDownItems.Add("プレイリスト名2");
			base.DropDownItems.Add(new ToolStripSeparator());
			base.DropDownItems.Add("新規プレイリスト");

			if (base.DesignMode) return;
			
			this.DropDownOpening += new EventHandler(this.ToolStripPlaylistMenuItem_DropDownOpening);
		}
		private void ToolStripPlaylistMenuItem_DropDownOpening(object sender, EventArgs e) {
			if (base.DesignMode) return;
			if (null == Program.PlaylistsManager) return;
			
			this.CreateDropDownItems();
		}
		private void CreateDropDownItems() {
			List<ToolStripItem> items = new List<ToolStripItem>();
			foreach (Playlist pl in Program.PlaylistsManager) {
				ToolStripMenuItemWithPlaylist tsmiwp = new ToolStripMenuItemWithPlaylist(pl);
				tsmiwp.Click += new EventHandler(tsmiwp_Click);
				items.Add(tsmiwp);
			}
			if (items.Count == 0) {
				ToolStripMenuItem tsmi = new ToolStripMenuItem("(プレイリストなし)");
				tsmi.Enabled = false;
				items.Add(tsmi);
			}
			items.Add(new ToolStripSeparator());
			ToolStripTextBox tstbNew = new ToolStripTextBox();
			tstbNew.Text = ToolStripPlaylistMenuItem.NewPlaylistName;
			tstbNew.KeyDown += new KeyEventHandler(tstbNew_KeyDown);
			items.Add(tstbNew);
			base.DropDownItems.Clear();
			base.DropDownItems.AddRange(items.ToArray());
		}
		
		private void tstbNew_KeyDown(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Enter:
					ToolStripTextBox tstb = sender as ToolStripTextBox;
					Playlist pl = Program.PlaylistsManager.CreatePlaylistNamedAs(tstb.Text);
					this.OnPlaylistSelected(pl);
					tstb.PerformClick();
					tstb.Text = ToolStripPlaylistMenuItem.NewPlaylistName;
					break;
			}
		}
		private void OnPlaylistSelected(Playlist pl) {
			this.lastSelectedPlaylist = pl;
			EventHandler handler = this.PlaylistSelected;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void tsmiwp_Click(object sender, EventArgs e) {
			ToolStripMenuItemWithPlaylist tsmiwp = sender as ToolStripMenuItemWithPlaylist;
			this.OnPlaylistSelected(tsmiwp.Playlist);
		}
		public Playlist LastSelectedPlaylist {
			get { return this.lastSelectedPlaylist; }
		}
	}
}
