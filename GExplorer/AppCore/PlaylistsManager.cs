using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using System.ComponentModel;
using System.IO;
using System.Collections;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.AppCore {
	sealed class PlaylistsManager : IEnumerable<Playlist> {
		public event EventHandler PlaylistsManagerChanged;
		public event EventHandler CurrentContentAndPlaylistChanged;
		
		private List<Playlist> lists = new List<Playlist>();
		private TimeSpan grandTotalTime = TimeSpan.Zero;
		private int grandTotalContentsCount = 0;
		
		private Playlist currentPlaylist = null;
		private GContentClass currentContent = null;
		
		private int updateCount = 0;
		private bool updatedFlag = false;

		public PlaylistsManager() {
		}

		public void BeginUpdate() {
			Interlocked.Increment(ref this.updateCount);
		}
		public void EndUpdate() {
			if (!this.IsUpdating) throw new InvalidOperationException();
			if (0 == Interlocked.Decrement(ref this.updateCount)) {
				if (this.updatedFlag) {
					this.updatedFlag = false;
					this.OnPlaylistsManagerChanged();
				}
			}
		}
		private void OnPlaylistsManagerChanged() {
			if (this.IsUpdating) {
				this.updatedFlag = true;
				return;
			}
			TimeSpan time = TimeSpan.Zero;
			int count = 0;
			foreach (Playlist pl in this.lists) {
				time += pl.SubtotalTime;
				count += pl.ContentCount;
			}
			this.GrandTotalTime = time;
			this.GrandTotalContentsCount = count;
			
			EventHandler handler = this.PlaylistsManagerChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		public TimeSpan GrandTotalTime {
			get { return this.grandTotalTime; }
			private set { this.grandTotalTime = value; }
		}
		public int GrandTotalContentsCount {
			get { return this.grandTotalContentsCount; }
			private set { this.grandTotalContentsCount = value; }
		}
		public bool IsUpdating {
			get { return this.updateCount > 0; }
		}
		public int PlaylistsCount {
			get { return this.lists.Count; }
		}
		private void ChildPlaylistChangedHandler(object sender, EventArgs e) {
			this.OnPlaylistsManagerChanged();
		}
		public void SerializePlaylists(string filename) {
			XmlSerializer xs = new XmlSerializer(typeof(List<Playlist>));
			using (FileStream fs = new FileStream(filename, FileMode.Create)) {
				xs.Serialize(fs, this.lists);
			}
		}
		public void DeserializePlaylists(string filename) {
			this.ClearPlaylists();
			if (!File.Exists(filename)) return;
			XmlSerializer xs = new XmlSerializer(typeof(List<Playlist>));
			using (FileStream fs = new FileStream(filename, FileMode.Open)) {
				this.lists = xs.Deserialize(fs) as List<Playlist>;
			}
			foreach (Playlist pl in this.lists) {
				pl.PlaylistChanged += this.ChildPlaylistChangedHandler;
				pl.RecalculateSubtotalTime();
			}
			this.OnPlaylistsManagerChanged();
		}
		public bool RemovePlaylist(Playlist pl) {
			if (this.lists.Remove(pl)) {
				pl.PlaylistChanged -= this.ChildPlaylistChangedHandler;
				pl.Destroy();
				this.OnPlaylistsManagerChanged();
				return true;
			} else {
				return false;
			}
		}
		public void ClearPlaylists() {
			foreach (Playlist pl in this.lists) {
				pl.PlaylistChanged -= this.ChildPlaylistChangedHandler;
				pl.Destroy();
			}
			this.lists.Clear();
			this.OnPlaylistsManagerChanged();
		}
		public Playlist GetOrCreatePlaylistNamedAs(string name) {
			foreach (Playlist list in this.lists) {
				if (name.Equals(list.Name)) {
					return list;
				}
			}
			return this.CreatePlaylistNamedAs(name);
		}
		public Playlist CreatePlaylistNamedAs(string name) {
			Playlist pl = new Playlist(name);
			pl.PlaylistChanged += this.ChildPlaylistChangedHandler;
			this.lists.Add(pl);
			this.OnPlaylistsManagerChanged();
			return pl;
		}
		private void Swap(int idx1, int idx2) {
			Playlist temp = this.lists[idx1];
			this.lists[idx1] = this.lists[idx2];
			this.lists[idx2] = temp;
			this.OnPlaylistsManagerChanged();
		}
		public void MoveToTop(Playlist pl) {
			int idx = this.lists.IndexOf(pl);
			if (idx > 0) {
				this.lists.RemoveAt(idx);
				this.lists.Insert(0, pl);
				this.OnPlaylistsManagerChanged();
			}
		}
		public void MoveUp(Playlist pl) {
			int idx = this.lists.IndexOf(pl);
			if (idx > 0) {
				this.Swap(idx, idx - 1);
			}
		}
		public void MoveDown(Playlist pl) {
			int idx = this.lists.IndexOf(pl);
			if (idx < this.lists.Count - 1) {
				this.Swap(idx, idx + 1);
			}
		}
		public void MoveToBottom(Playlist pl) {
			int idx = this.lists.IndexOf(pl);
			if (idx < this.lists.Count - 1) {
				this.lists.RemoveAt(idx);
				this.lists.Add(pl);
				this.OnPlaylistsManagerChanged();
			}
		}
		public Playlist CurrentPlaylist {
			get { return this.currentPlaylist; }
		}
		public GContentClass CurrentContent {
			get { return this.currentContent; }
		}
		private void OnCurrentContentAndPlaylistChanged() {
			EventHandler handler = this.CurrentContentAndPlaylistChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		public void SetCurrentContentAndPlaylist(GContentClass content, Playlist pl) {
			this.currentContent = content;
			this.currentPlaylist = pl;
			this.OnCurrentContentAndPlaylistChanged();
		}
		public Playlist this[int idx]{
			get{return this.lists[idx];}
		}
		public IEnumerator<Playlist> GetEnumerator() {
			return this.lists.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
		public void Sort(IComparer<Playlist> comparer) {
			this.lists.Sort(comparer);
			this.OnPlaylistsManagerChanged();
		}
	}

	public sealed class Playlist/* : IEnumerable<GContentClass>*/{
		public event EventHandler PlaylistChanged;
		public event EventHandler PlaylistDestroyed;

		private string name = string.Empty;
		private TimeSpan subtotalTime = TimeSpan.Zero;
		private DateTime created = DateTime.Now;
		private DateTime lastModified = DateTime.Now;
		
		private List<GContentClass> contents = new List<GContentClass>();
		
		private int updateCount = 0;
		private bool updatedFlag = false;
		
		public Playlist() {
		}
		public Playlist(string name) {
			this.name = name;
		}
		
		public void BeginUpdate() {
			Interlocked.Increment(ref this.updateCount);
			
		}
		public void EndUpdate() {
			if (! this.IsUpdating) throw new InvalidOperationException();
			if (0 == Interlocked.Decrement(ref this.updateCount)) {
				if (this.updatedFlag) {
					this.updatedFlag = false;
					this.OnPlaylistChanged();
				}
			}
		}
		public void RecalculateSubtotalTime() {
			TimeSpan time = TimeSpan.Zero;
			foreach (GContentClass cont in this.contents) {
				if (cont.DurationValue.HasValue) {
					time += cont.DurationValue.Value;
				}
			}
			this.SubtotalTime = time;
		}
		private void OnPlaylistChanged() {
			if (this.IsUpdating) {
				this.updatedFlag = true;
				return;
			}
			this.RecalculateSubtotalTime();
			this.lastModified = DateTime.Now;

			EventHandler handler = this.PlaylistChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnPlaylistDestroyed() {
			this.contents.Clear();
			this.OnPlaylistChanged();
			EventHandler handler = this.PlaylistDestroyed;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		
		[XmlAttribute]
		public string Name {
			get { return this.name; }
			set {
				this.name = value;
				//デシリアライズ時にはLastModifiedより先に呼ばれることを祈る
				this.OnPlaylistChanged();
			}
		}
		[XmlAttribute]
		public DateTime Created {
			get { return this.created; }
			[EditorBrowsable(EditorBrowsableState.Never)]
			set { this.created = value; }
		}
		[XmlAttribute]
		public DateTime LastModified {
			get { return this.lastModified; }
			[EditorBrowsable(EditorBrowsableState.Never)]
			set { this.lastModified = value; }
		}
		[XmlIgnore]
		public TimeSpan SubtotalTime {
			get { return this.subtotalTime; }
			private set { this.subtotalTime = value; }
		}
		[XmlIgnore]
		public int ContentCount {
			get { return this.contents.Count; }
		}
		[XmlIgnore]
		public bool IsUpdating {
			get { return this.updateCount > 0; }
		}
		[XmlElement]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public List<GContentClass> Contents {
			get { return this.contents; }
			set {
				this.contents = value;
				this.RecalculateSubtotalTime();
			}
		}
		
		public void Destroy() {
			this.OnPlaylistDestroyed();
		}
		public void AddContent(GContentClass cont) {
			this.contents.Add(cont.Clone());
			this.OnPlaylistChanged();
		}
		public bool RemoveContent(GContentClass cont) {
			if (this.contents.Remove(cont)) {
				this.OnPlaylistChanged();
				return true;
			}
			return false;
		}
		private void Swap(int idx1, int idx2) {
			GContentClass temp = this.contents[idx1];
			this.contents[idx1] = this.contents[idx2];
			this.contents[idx2] = temp;
			this.OnPlaylistChanged();
		}
		public void MoveToTop(GContentClass cont) {
			int idx = this.contents.IndexOf(cont);
			if (idx > 0) {
				this.contents.RemoveAt(idx);
				this.contents.Insert(0, cont);
				this.OnPlaylistChanged();
			}
		}
		public void MoveUp(GContentClass cont) {
			int idx = this.contents.IndexOf(cont);
			if (idx > 0) {
				this.Swap(idx, idx - 1);
			}
		}
		public void MoveDown(GContentClass cont) {
			int idx = this.contents.IndexOf(cont);
			if (idx < this.contents.Count - 1) {
				this.Swap(idx, idx + 1);
			}
		}
		public void MoveToBottom(GContentClass cont) {
			int idx = this.contents.IndexOf(cont);
			if (idx < this.contents.Count - 1) {
				this.contents.RemoveAt(idx);
				this.contents.Add(cont);
				this.OnPlaylistChanged();
			}
		}
		public int IndexOf(GContentClass cont) {
			return this.contents.IndexOf(cont);
		}
		public GContentClass this[int idx] {
			get { return this.contents[idx]; }
		}
		public void RemoveAt(int idx) {
			this.contents.RemoveAt(idx);
			this.OnPlaylistChanged();
		}
		public bool Contains(GContentClass cont) {
			return this.contents.Contains(cont);
		}

		public IEnumerator<GContentClass> GetEnumerator() {
			return this.contents.GetEnumerator();
		}
		public void Sort(IComparer<GContentClass> comparer) {
			this.contents.Sort(comparer);
			this.OnPlaylistChanged();
		}
	}
}
