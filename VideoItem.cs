using System;

namespace Yusen.GExplorer {
	public enum VideoItemType {
		Normal,
		PackList,
		Special,
	}
	public enum BitRate {
		High = 2,
		Low = 1,
	}
	
	//‚ ‚Ü‚è‚É‚à‰˜‚¢‚È‚ 
	public struct VideoItem{
		private VideoItemType type;
		
		private bool isNew;
		private int cntId;
		private string limit;
		private string subgenre;
		private string pack;
		private string episode;
		private string lead;

		private Uri specialUri;

		private int packId;

		public VideoItem(bool isNew, int cntId, string limit,
				string subgenre, string pack, string episode, string lead) {
			this.type = VideoItemType.Normal;
			this.isNew = isNew;
			this.cntId = cntId;
			this.limit = limit;
			this.subgenre = subgenre;
			this.pack = pack;
			this.episode = episode;
			this.lead = lead;

			this.specialUri = null;
			this.packId = -1;
		}

		public VideoItem(bool isNew, Uri specialUri, string limit,
				string subgenre, string pack, string lead) {
			this.type = VideoItemType.Special;
			this.isNew = isNew;
			this.specialUri = specialUri;
			this.limit = limit;
			this.subgenre = subgenre;
			this.pack = pack;
			this.lead = lead;
			
			this.cntId = -1;
			this.episode = "";
			this.packId = -1;
		}

		public VideoItem(int packId, string subgenre, string pack) {
			this.type = VideoItemType.PackList;
			this.packId = packId;
			this.subgenre = subgenre;
			this.pack = pack;

			this.isNew = false;
			this.cntId = -1;
			this.specialUri = null;
			this.limit = "";
			this.episode = "";
			this.lead = "";
		}
		
		public VideoItemType Type {
			get {
				return this.type;
			}
		}
		
		public bool IsNew {
			get {
				return this.isNew;
			}
		}
		public int CntId {
			get {
				return this.cntId;
			}
		}
		public string Limit {
			get {
				return this.limit;
			}
		}
		public string Subgenre {
			get {
				return this.subgenre;
			}
			set {//‹ê“÷
				this.subgenre = value;
			}
		}
		public string Pack {
			get {
				return this.pack;
			}
			set {//‹ê“÷
				this.pack = value;
			}
		}
		public string Episode {
			get {
				return this.episode;
			}
		}
		public string Lead {
			get {
				return this.lead;
			}
		}
		
		public Uri SpecialUri {
			get {
				return this.specialUri;
			}
		}

		public int PackId {
			get {
				return this.packId;
			}
		}
		
		public Uri GetPlayListUri(int userId, BitRate bitRate) {
			return new Uri(
				"http://www.gyao.jp/sityou/asx.php?"
				+"contentsId=cnt" + this.CntId.ToString("0000000")
				+ "&userNo=" + userId.ToString()
				+ "&rateId=bit" + ((int)bitRate).ToString("0000000"));
		}
		
		public Uri GetMediaFileUri(int userId, BitRate bitRate) {
			return new Uri(
				"rtsp://wms.cd.gyao.jp/gyaovod01?QueryString="
				+ "contentsId=cnt" + this.CntId.ToString("0000000")
				+ ":userNo=" + userId.ToString()
				+ ":rateId=bit" + ((int)bitRate).ToString("0000000"));
		}
		
		public Uri GetDocumentUri(BitRate bitRate) {
			return new Uri(
				"http://www.gyao.jp/sityou/movie/contentsId/"
				+ "cnt" + this.CntId.ToString("0000000") + "/"
				+ "rateId/bit" + ((int)bitRate).ToString("0000000") + "/"
				+ "login_from/shityou/");
		}
	}
}
