using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AxWMPLib;
using WMPLib;

namespace Yusen.GExplorer {
	public partial class DetailForm : Form {
		public DetailForm(VideoItem video, int userId) {
			InitializeComponent();
			
			StringBuilder title = new StringBuilder(video.Subgenre);
			if("" != video.Pack) {
				title.Append(" / " + video.Pack);
				if("" != video.Episode) {
					title.Append(" / " + video.Episode);
				}
			}
			this.Text = title.ToString();
			this.txtDocument.Text = video.GetDocumentUri(BitRate.High).AbsoluteUri;
			this.txtPlayList.Text = video.GetPlayListUri(userId, BitRate.High).AbsoluteUri;
			this.txtMediaFile.Text = video.GetMediaFileUri(userId, BitRate.High).AbsoluteUri;
			
			this.wmpMain.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(wmpMain_OpenStateChange);
			this.wmpMain.URL = (video.GetMediaFileUri(userId, BitRate.High).AbsoluteUri);
			this.ieMain.Navigate(
				"http://www.gyao.jp/sityou/catedetail/",
				"",
				new ASCIIEncoding().GetBytes("pac_id=&contents_id=cnt" + video.CntId.ToString("0000000") + "&login_from=shityou"),
				"Content-Type: application/x-www-form-urlencoded\nReferer: http://www.gyao.jp/sityou/catedetail/\n");
		}

		void wmpMain_OpenStateChange(object sender, _WMPOCXEvents_OpenStateChangeEvent e) {
			// THANKSTO: http://pc8.2ch.net/test/read.cgi/esite/1116115226/81 ÇÃê_
			if(this.chkAutoVolume.Checked && WMPOpenState.wmposMediaOpen == this.wmpMain.openState) {
				this.wmpMain.settings.volume =
					this.wmpMain.currentMedia.getItemInfo("WMS_CONTENT_DESCRIPTION_PLAYLIST_ENTRY_URL").StartsWith("Adv:") ? 10 : 100;
			}
		}
	}
}
