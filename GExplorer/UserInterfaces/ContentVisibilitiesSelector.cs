using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Yusen.GExplorer.UserInterfaces {
	sealed partial class ContentVisibilitiesSelector : UserControl {
		private Dictionary<RadioButton, ContentVisibilities> dicPresetRV = new Dictionary<RadioButton, ContentVisibilities>();
		private Dictionary<RadioButton, ContentVisibilities> dicCustomRV = new Dictionary<RadioButton, ContentVisibilities>();
		private Dictionary<ContentVisibilities, RadioButton> dicPresetVR = new Dictionary<ContentVisibilities, RadioButton>();
		private Dictionary<ContentVisibilities, RadioButton> dicCustomVR = new Dictionary<ContentVisibilities, RadioButton>();

		public event EventHandler ContentVisibilitiesChanged;
		public event EventHandler CloseClick;

		private ContentVisibilities contentVisibilities = ContentVisibilities.None;
		private volatile bool updating = false;
		private readonly object updateLock = new object();

		public ContentVisibilitiesSelector() {
			InitializeComponent();

			if (base.DesignMode) return;

			this.dicPresetRV.Add(this.radioPresetToumei, ContentVisibilities.PresetToumei);
			this.dicPresetRV.Add(this.radioPresetSabori, ContentVisibilities.PresetSabori);
			this.dicPresetRV.Add(this.radioPresetHakidame, ContentVisibilities.PresetHakidame);
			this.dicPresetRV.Add(this.radioPresetYorigonomi, ContentVisibilities.PresetYorigonomi);
			this.dicPresetRV.Add(this.radioPresetShinchaku, ContentVisibilities.PresetShinchaku);
			foreach (KeyValuePair<RadioButton, ContentVisibilities> pair in this.dicPresetRV) {
				this.dicPresetVR.Add(pair.Value, pair.Key);
			}

			this.dicCustomRV.Add(this.radioNgTrue, ContentVisibilities.NgTrue);
			this.dicCustomRV.Add(this.radioNgFalse, ContentVisibilities.NgFalse);
			this.dicCustomRV.Add(this.radioNgBoth, ContentVisibilities.NgDontCare);
			this.dicCustomRV.Add(this.radioFavTrue, ContentVisibilities.FavTrue);
			this.dicCustomRV.Add(this.radioFavFalse, ContentVisibilities.FavFalse);
			this.dicCustomRV.Add(this.radioFavBoth, ContentVisibilities.FavDontCare);
			this.dicCustomRV.Add(this.radioNewTrue, ContentVisibilities.NewTrue);
			this.dicCustomRV.Add(this.radioNewFalse, ContentVisibilities.NewFalse);
			this.dicCustomRV.Add(this.radioNewBoth, ContentVisibilities.NewDontCare);
			foreach (KeyValuePair<RadioButton, ContentVisibilities> pair in this.dicCustomRV) {
				this.dicCustomVR.Add(pair.Value, pair.Key);
			}

			this.ContentVisibilities = ContentVisibilities.PresetToumei;
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ContentVisibilities ContentVisibilities {
			get {
				return this.contentVisibilities;
			}
			set {
				ContentVisibilities vis = ContentVisibilitiesUtility.SanitizeValue(value);
				if (vis == this.contentVisibilities) return;
				this.contentVisibilities = vis;
				
				lock (this.updateLock) {
					this.updating = true;
					
					//プリセット更新
					RadioButton presetRadio;
					if (this.dicPresetVR.TryGetValue(vis, out presetRadio)) {
						presetRadio.Checked = true;
					} else {
						foreach (RadioButton radio in dicPresetRV.Keys) {
							radio.Checked = false;
						}
					}
					//カスタム更新
					this.dicCustomVR[vis & ContentVisibilities.NgDontCare].Checked = true;
					this.dicCustomVR[vis & ContentVisibilities.FavDontCare].Checked = true;
					this.dicCustomVR[vis & ContentVisibilities.NewDontCare].Checked = true;

					this.updating = false;
				}

				this.OnContentVisibilitiesChanged();
			}
		}

		private void OnContentVisibilitiesChanged() {
			EventHandler handler = this.ContentVisibilitiesChanged;
			if(null != handler){
				handler(this, EventArgs.Empty);
			}
		}
		
		private void RadioButton_CheckedChanged(object sender, EventArgs e) {
			if (this.updating) return;
			RadioButton sender2 = sender as RadioButton;
			
			ContentVisibilities vis;
			if (this.dicPresetRV.TryGetValue(sender2, out vis)) {
				this.ContentVisibilities = vis;
			} else if (this.dicCustomRV.TryGetValue(sender2, out vis)) {
				ContentVisibilities oldVis = this.ContentVisibilities;
				if ((vis & ContentVisibilities.NgDontCare) != ContentVisibilities.None) {
					vis |= oldVis & ~ContentVisibilities.NgDontCare;
				} else if ((vis & ContentVisibilities.FavDontCare) != ContentVisibilities.None) {
					vis |= oldVis & ~ContentVisibilities.FavDontCare;
				} else if ((vis & ContentVisibilities.NewDontCare) != ContentVisibilities.None) {
					vis |= oldVis & ~ContentVisibilities.NewDontCare;
				} else {
					throw new InvalidOperationException();
				}
				this.ContentVisibilities = vis;
			} else {
				throw new InvalidOperationException();
			}
		}

		private void btnClose_Click(object sender, EventArgs e) {
			EventHandler handler = this.CloseClick;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
	}

	[Flags]
	public enum ContentVisibilities {
		None = 0,

		NgTrue = 1,
		NgFalse = 2,
		NgDontCare = NgTrue | NgFalse,
		FavTrue = 4,
		FavFalse = 8,
		FavDontCare = FavTrue | FavFalse,
		NewTrue = 16,
		NewFalse = 32,
		NewDontCare = NewTrue | NewFalse,

		PresetToumei = NgFalse | FavDontCare | NewDontCare,
		PresetSabori = NgDontCare | FavDontCare | NewDontCare,
		PresetHakidame = NgTrue | FavDontCare | NewDontCare,
		PresetYorigonomi = NgDontCare | FavTrue | NewDontCare,
		PresetShinchaku = NgDontCare | FavDontCare | NewTrue,
	}

	internal static class ContentVisibilitiesUtility {
		public static ContentVisibilities SanitizeValue(ContentVisibilities vis) {
			if ((vis & ContentVisibilities.NgDontCare) == ContentVisibilities.None) {
				vis |= ContentVisibilities.NgDontCare;
			}
			if ((vis & ContentVisibilities.FavDontCare) == ContentVisibilities.None) {
				vis |= ContentVisibilities.FavDontCare;
			}
			if ((vis & ContentVisibilities.NewDontCare) == ContentVisibilities.None) {
				vis |= ContentVisibilities.NewDontCare;
			}
			return vis;
		}

		public static string ConvertToFlagsString(ContentVisibilities vis) {
			char[] flags = new char[3];
			switch (vis & ContentVisibilities.NgDontCare) {
				case ContentVisibilities.NgTrue:
					flags[0] = 'T';
					break;
				case ContentVisibilities.NgFalse:
					flags[0] = 'F';
					break;
				case ContentVisibilities.NgDontCare:
					flags[0] = '*';
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			switch (vis & ContentVisibilities.FavDontCare) {
				case ContentVisibilities.FavTrue:
					flags[1] = 'T';
					break;
				case ContentVisibilities.FavFalse:
					flags[1] = 'F';
					break;
				case ContentVisibilities.FavDontCare:
					flags[1] = '*';
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			switch (vis & ContentVisibilities.NewDontCare) {
				case ContentVisibilities.NewTrue:
					flags[2] = 'T';
					break;
				case ContentVisibilities.NewFalse:
					flags[2] = 'F';
					break;
				case ContentVisibilities.NewDontCare:
					flags[2] = '*';
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return new string(flags);
		}
	}
}
