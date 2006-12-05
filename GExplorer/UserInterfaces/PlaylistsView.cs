using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Yusen.GExplorer.AppCore;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class PlaylistsView : UserControl, IPlaylistsViewBindingContract, INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler LastSelectedContentChanged;

		private GContentClass lastSelectedContent;

		public PlaylistsView() {
			InitializeComponent();

			this.tslTitle.Font = new Font(this.tslTitle.Font, FontStyle.Bold);
		}
		
		private void PlaylistsView_Load(object sender, EventArgs e) {
			if (base.DesignMode) return;
			
		}
		private void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (null != handler) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		private void OnLastSelectedContentChanged() {
			EventHandler handler = this.LastSelectedContentChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		
		private void pvcControl_StatusMessagesChanged(object sender, EventArgs e) {
			this.tslPlaylistsCount.Text = this.pvcControl.StatusPlaylistCount;
			this.tslGrandTotalContentsCount.Text = this.pvcControl.StatusContentCount;
			this.tslGrandTotalTime.Text = this.pvcControl.StatusDurationSum;
		}
		private void pvcControl_ViewPlaylistRequested(object sender, EventArgs e) {
			this.pvpControl.AttachToPlaylist(this.pvcControl.LastRequestedPlaylist);
		}

		private void pvpControl_StatusMessagesChanged(object sender, EventArgs e) {
			this.tslPlaylistName.Text = this.pvpControl.StatusMessagePlaylistName;
			this.tslSubtotalContentCount.Text = this.pvpControl.StatusMessageContentCount;
			this.tslSubtotalTime.Text = this.pvpControl.StatusMessageDuration;
		}
		
		public ToolStripDropDown GetPlaylistCollectionToolStripDropDown() {
			return this.pvcControl.GetToolStripDropDown();
		}
		public ToolStripDropDown GetPlaylistToolStripDropDown() {
			return this.pvpControl.GetToolStripDropDown();
		}
		public void BindToOptions(PlaylistsViewOptions options) {
			options.NeutralizeUnspecificValues(this);
			BindingContractUtility.BindAllProperties<PlaylistsView, IPlaylistsViewBindingContract>(this, options);
			this.pvcControl.BindToOptions(options.PlaylistsViewCControlOptions);
			this.pvpControl.BindToOptions(options.PlaylistsViewPControlOptions);
		}
		public GContentClass LastSelectedContent {
			get { return this.lastSelectedContent; }
			private set {
				//if (this.lastSelectedContent != value) {
					this.lastSelectedContent = value;
					this.OnLastSelectedContentChanged();
				//}
			}
		}

		public void SelectPlaylist(Playlist playlist) {
			this.pvcControl.SelectPlaylist(playlist);
		}

		#region IPlaylistsViewBindingContract Members
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int PlaylistCollectionWidth {
			get { return this.splitContainer1.SplitterDistance;}
			set {
				this.splitContainer1.SplitterDistance = value;
				this.OnPropertyChanged("PlaylistCollectionWidth");
			}
		}
		#endregion

		private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e) {
			this.OnPropertyChanged("PlaylistCollectionWidth");
		}

		private void pvpControl_LastSelectedContentChanged(object sender, EventArgs e) {
			this.LastSelectedContent = this.pvpControl.LastSelectedContent;
		}
	}

	interface IPlaylistsViewBindingContract : IBindingContract {
		int PlaylistCollectionWidth { get;set;}
	}
	public sealed class PlaylistsViewOptions : IPlaylistsViewBindingContract {
		public PlaylistsViewOptions() {
		}

		#region IPlaylistsViewBindingContract Members
		private int playlistCollectionWidth = -1;
		[Category("スプリッタの位置")]
		[DisplayName("コレクションコントロールの幅")]
		[Description("コレクションコントロールの幅を指定します．")]
		[DefaultValue(-1)]
		public int PlaylistCollectionWidth {
			get { return this.playlistCollectionWidth; }
			set { this.playlistCollectionWidth = value; }
		}
		
		#endregion
		private PlaylistsViewCControlOptions playlistsViewCControlOptions = new PlaylistsViewCControlOptions();
		[Browsable(false)]
		[SubOptions("コレクション", "プレイリストビュー・コレクションコントロールに関する設定")]
		public PlaylistsViewCControlOptions PlaylistsViewCControlOptions {
			get { return this.playlistsViewCControlOptions; }
			set { this.playlistsViewCControlOptions = value; }
		}
		private PlaylistsViewPControlOptions playlistsViewPControlOptions = new PlaylistsViewPControlOptions();
		[Browsable(false)]
		[SubOptions("プレイリスト", "プレイリストビュー・プレイリストコントロールに関する設定")]
		public PlaylistsViewPControlOptions PlaylistsViewPControlOptions {
			get { return this.playlistsViewPControlOptions; }
			set { this.playlistsViewPControlOptions = value; }
		}

		internal void NeutralizeUnspecificValues(PlaylistsView pv) {
			if (this.PlaylistCollectionWidth < 0) this.PlaylistCollectionWidth = pv.PlaylistCollectionWidth;
		}
	}
}
