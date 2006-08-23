using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace Yusen.GExplorer {
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
	public sealed class ToolStripPlayModeDropDownButton : ToolStripDropDownButton{
		private const string UnknownTime = "??:??:??";
		
		private struct ClipInfo {
			public TimeSpan? Begin;
			public TimeSpan? Duration;
		}

		private static string FormatTimespan(TimeSpan t) {
			return string.Format("{0:d2}:{1:d2}:{2:d2}", (int)t.TotalHours, t.Minutes, t.Seconds);
		}

		public event EventHandler<PlayModeSelectedEventArgs> PlayModeSelected;
		public event EventHandler GoToChapterRequested;

		private bool menuUpdateReserved = true;
		private readonly object updateLock = new object();

		private SortedDictionary<int, ClipInfo> infos = new SortedDictionary<int, ClipInfo>();
		private int? currentMode = null;
		private int? currentClipNo = null;

		private bool persistentToolStipItemsCreated = false;
		private List<ToolStripItem> tsItemsHeader;
		private List<ToolStripItem> tsItemsFooter;
		private ToolStripMenuItem tsmiNormal;
		
		public ToolStripPlayModeDropDownButton() : base("ToolStripPlayModeDropDownButton") {
			this.DropDownItems.Add(new ToolStripMenuItem("dummy"));
			this.DropDown.Opening += new CancelEventHandler(DropDown_Opening);
		}

		private void DropDown_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
			lock (this.updateLock) {
				if (this.menuUpdateReserved) {
					if (!this.persistentToolStipItemsCreated) {
						this.CreatePersistentToolStipItems();
					}
					this.UpdateMenuItems();
					this.menuUpdateReserved = false;
				}
			}
		}
		private void ReserveMenuUpdate() {
			lock (this.updateLock) {
				this.menuUpdateReserved = true;
			}
		}
		private void CreatePersistentToolStipItems() {
			this.tsItemsHeader = new List<ToolStripItem>();
			this.tsmiNormal = new ToolStripMenuItem("通常再生(&N)");
			this.tsmiNormal.Click += delegate {
				this.OnPlayModeSelect(null);
			};
			this.tsItemsHeader.Add(this.tsmiNormal);
			this.tsItemsHeader.Add(new ToolStripSeparator());

			this.tsItemsFooter = new List<ToolStripItem>();
			this.tsItemsFooter.Add(new ToolStripSeparator());
			ToolStripMenuItem tsmiGoToChapter = new ToolStripMenuItem("チャプタ番号を指定(&G)...");
			tsmiGoToChapter.Click += delegate {
				EventHandler handler = this.GoToChapterRequested;
				if (null != handler) {
					handler(this, EventArgs.Empty);
				}
			};
			this.tsItemsFooter.Add(tsmiGoToChapter);
		}

		public void AddClipInfo(int clipNum, TimeSpan begin, TimeSpan duration) {
			ClipInfo info;
			if (this.infos.TryGetValue(clipNum, out info)) {
				info.Begin = begin;
				info.Duration = duration;
				this.infos[clipNum] = info;
			} else {
				info.Begin = begin;
				info.Duration = duration;
				this.infos.Add(clipNum, info);
			}
			this.ReserveMenuUpdate();
		}
		public void ClearClipInfo() {
			this.infos.Clear();
			this.ReserveMenuUpdate();
		}
		public void NotifyCurrentModeAndNumber(int? mode, int? clipNo) {
			this.currentMode = mode;
			this.currentClipNo = clipNo;
			this.ReserveMenuUpdate();
			
			//テキスト更新
			string text;
			if (this.currentMode.HasValue) {
				text = string.Format("チャプタ{0}", this.currentMode.Value); 
			} else {
				text = "通常";
			}
			if (this.currentClipNo.HasValue) {
				text += string.Format(" (clipNo={0})", this.currentClipNo.Value);
			}
			base.Text = text;

			//クリップ情報がなかったらとりあえず追加
			if (this.currentMode.HasValue) {
				if (!this.infos.ContainsKey(this.currentMode.Value)) {
					this.infos.Add(this.currentMode.Value, new ClipInfo());
				}
			}
			if (this.currentClipNo.HasValue) {
				if (!this.infos.ContainsKey(this.currentClipNo.Value)) {
					this.infos.Add(this.currentClipNo.Value, new ClipInfo());
				}
			}
		}
		
		private void UpdateMenuItems() {
			//ヘッダ付加
			List<ToolStripItem> items = new List<ToolStripItem>(this.tsItemsHeader);
			
			//きっとクリップ1は存在するだろう
			if (!this.infos.ContainsKey(1)) {
				items.Add(this.CreateClipMenuItem(1, null));
			}
			//既知のクリップ
			foreach (KeyValuePair<int, ClipInfo> pair in this.infos) {
				items.Add(this.CreateClipMenuItem(pair.Key, pair.Value));
			}
			
			//フッタ付加
			items.AddRange(this.tsItemsFooter);
			
			//ドロップダウンクリア
			this.DropDownItems.Clear();
			
			//通常再生のチェックとか
			bool isNormalMode = !this.currentMode.HasValue;
			this.tsmiNormal.Checked = isNormalMode;
			this.tsmiNormal.Enabled = !isNormalMode;
			
			//追加
			this.DropDownItems.AddRange(items.ToArray());
		}

		private ToolStripMenuItem CreateClipMenuItem(int clipNum, ClipInfo? info) {
			string dur = ToolStripPlayModeDropDownButton.UnknownTime;
			string begin = ToolStripPlayModeDropDownButton.UnknownTime;
			string end = ToolStripPlayModeDropDownButton.UnknownTime;
			if (info.HasValue && info.Value.Duration.HasValue) {
				dur = ToolStripPlayModeDropDownButton.FormatTimespan(info.Value.Duration.Value);
				if (info.Value.Begin.HasValue) {
					begin = ToolStripPlayModeDropDownButton.FormatTimespan(info.Value.Begin.Value);
					end = ToolStripPlayModeDropDownButton.FormatTimespan(info.Value.Begin.Value + info.Value.Duration.Value);
				}
			}
			ToolStripMenuItem tsmi = new ToolStripMenuItem(
				string.Format("クリップ{0} {1} ({2}-{3})", clipNum, dur, begin, end));
			tsmi.Tag = clipNum;
			tsmi.Click += delegate(object sender, EventArgs e) {
				ToolStripMenuItem sender2 = sender as ToolStripMenuItem;
				this.OnPlayModeSelect((int)sender2.Tag);
			};
			
			if (this.currentMode.HasValue && this.currentMode.Value.Equals(clipNum)) {
				tsmi.Checked = true;
				tsmi.Enabled = false;
			} else if (!this.currentMode.HasValue && this.currentClipNo.HasValue && this.currentClipNo.Equals(clipNum)) {
				tsmi.Checked = true;
			}
			
			return tsmi;
		}

		private void OnPlayModeSelect(int? chapterNum) {
			EventHandler<PlayModeSelectedEventArgs> handler = this.PlayModeSelected;
			if (null != handler) {
				PlayModeSelectedEventArgs e;
				if (chapterNum.HasValue) {
					e = new PlayModeSelectedEventArgs(chapterNum.Value);
				} else {
					e = new PlayModeSelectedEventArgs();
				}
				handler(this, e);
			}
		}
	}

	public sealed class PlayModeSelectedEventArgs : EventArgs {
		private int? chapterNum;

		public PlayModeSelectedEventArgs() {
		}
		public PlayModeSelectedEventArgs(int chapterNum) {
			this.chapterNum = chapterNum;
		}

		public bool IsNormalMode{
			get { return !this.chapterNum.HasValue; }
		}
		public int ChapterNumber {
			get {
				if (this.IsNormalMode) throw new InvalidOperationException();
				return this.chapterNum.Value;
			}
		}
	}
}
