using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	class ContentAdapter {
		private GGenre innerGenre;
		private GContent innerCont;
		
		public ContentAdapter(GContent innerCont, GGenre innerGenre) {
			this.innerCont = innerCont;
			this.innerGenre = innerGenre;
		}
		
		[Category("�L�[")]
		public string ContentId {
			get { return this.innerCont.ContentId; }
		}
		[Category("�t�����")]
		public string Title {
			get { return this.innerCont.Title; }
		}
		[Category("�t�����")]
		public string SubTitle {
			get { return this.innerCont.SubTitle; }
		}
		[Category("�t�����")]
		public string EpisodeNumber {
			get { return this.innerCont.EpisodeNumber; }
		}
		[Category("�t�����")]
		public string Duration {
			get { return this.innerCont.Duration; }
		}
		[Category("�t�����")]
		public string LongDescription {
			get { return this.innerCont.LongDescription; }
		}
		[Category("��u�����t���������")]
		public bool FromCache {
			get { return this.innerCont.FromCache; }
		}
		[Category("��u�����t���������")]
		public string DisplayName{
			get {
				StringBuilder sb = new StringBuilder();
				sb.Append("[" + this.innerGenre.GenreName + "]");
				sb.Append(" " + this.Title);
				if ("" != this.EpisodeNumber) {
					sb.Append(" / " + this.EpisodeNumber);
				}
				if ("" != this.SubTitle && this.Title != this.SubTitle) {
					sb.Append(" / " + this.SubTitle);
				}
				return sb.ToString();
			}
		}
		[Category("URI")]
		public Uri DetailPageUri {
			get { return this.innerCont.DetailPageUri; }
		}
		[Category("URI")]
		public Uri PlayerPageUri {
			get {
				return GContent.CreatePlayerPageUri(this.ContentId, GlobalSettings.Instance.BitRate);
			}
		}
		[Category("URI")]
		public Uri MediaFileUri {
			get {
				return GContent.CreateMediaFileUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate);
			}
		}
		[Category("URI")]
		public Uri ImageLargeUri {
			get {return this.innerCont.ImageLargeUri;}
		}
		[Category("URI")]
		public Uri ImageSmallUri {
			get { return this.innerCont.ImageSmallUri; }
		}
	}
}
