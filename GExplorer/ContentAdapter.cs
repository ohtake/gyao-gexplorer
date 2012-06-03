using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Yusen.GCrawler;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public class ContentAdapter {
		private GContent innerCont;
		private string genreName;

		public ContentAdapter() {
		}
		public ContentAdapter(GContent innerCont, GGenre genre) {
			this.innerCont = innerCont;
			this.genreName = genre.GenreName;
		}
		
		[Browsable(false)]
		public GContent InnerContent {
			get { return this.innerCont; }
			set { this.innerCont = value; }
		}
		[Browsable(false)]
		public string GenreName {
			get { return this.genreName; }
			set { this.genreName = value; }
		}
		
		[XmlIgnore]
		[Category("�L�[")]
		[Description("�R���e���c��ID�D")]
		public string ContentId {
			get { return this.innerCont.ContentId; }
		}
		[XmlIgnore]
		[Category("�t�����")]
		[Description("�^�C�g���D")]
		public string Title {
			get { return this.innerCont.Title; }
		}
		[XmlIgnore]
		[Category("�t�����")]
		[Description("�T�u�^�C�g���D")]
		public string SubTitle {
			get { return this.innerCont.SubTitle; }
		}
		[XmlIgnore]
		[Category("�t�����")]
		[Description("�b���D")]
		public string EpisodeNumber {
			get { return this.innerCont.EpisodeNumber; }
		}
		[XmlIgnore]
		[Category("�t�����")]
		[Description("�������ԁD")]
		public string Duration {
			get { return this.innerCont.Duration; }
		}
		[XmlIgnore]
		[Category("�t�����")]
		[Description("�ڍ׋L�q(��)�D")]
		public string LongDescription {
			get { return this.innerCont.LongDescription; }
		}
		[XmlIgnore]
		[Category("��u�����t���������")]
		[Description("True�̏ꍇ�̓L���b�V������ǂ܂ꂽ���Ƃ������D")]
		public bool FromCache {
			get { return this.innerCont.FromCache; }
		}
		[XmlIgnore]
		[Category("��u�����t���������")]
		[Description("�W����������^�C�g����K���ɑg�ݍ��킹���\�����D")]
		public string DisplayName{
			get {
				StringBuilder sb = new StringBuilder();
				sb.Append("[" + this.genreName + "]");
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
		[XmlIgnore]
		[Category("URI")]
		[Description("�ڍ׃y�[�W��URI�D")]
		public Uri DetailPageUri {
			get { return this.innerCont.DetailPageUri; }
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("IE�Ő��K�ɍĐ�����ꍇ��URI�D(�O���[�o���ݒ�̃r�b�g���[�g�̉e�����󂯂�D)")]
		public Uri PlayerPageUri {
			get {
				return GContent.CreatePlayerPageUri(this.ContentId, GlobalSettings.Instance.BitRate);
			}
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("�v���C���X�g��URI�D(�O���[�o���ݒ�̃r�b�g���[�g�̉e�����󂯂�D���[�UID���܂ށD)")]
		public Uri PlayListUri {
			get {
				return GContent.CreatePlayListUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate);
			}
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("����t�@�C����URI�D(�O���[�o���ݒ�̃r�b�g���[�g�̉e�����󂯂�D���[�UID���܂ށD)")]
		public Uri MediaFileUri {
			get {
				return GContent.CreateMediaFileUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate);
			}
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("�R���e���c�̉摜(��)��URI�D")]
		public Uri ImageLargeUri {
			get {return this.innerCont.ImageLargeUri;}
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("�R���e���c�̉摜(��)��URI�D")]
		public Uri ImageSmallUri {
			get { return this.innerCont.ImageSmallUri; }
		}
		[XmlIgnore]
		[Category("URI")]
		[Description("�����ߔԑg�̈ē��y�[�W��URI�D (�O���[�o���ݒ�̃r�b�g���[�g�̉e�����󂯂�D)")]
		public Uri RecomendPageUri {
			get {
				return GContent.CreateRecomendPageUri(this.ContentId, GlobalSettings.Instance.BitRate);
			}
		}

		public override bool Equals(object obj) {
			if (null == obj) {
				return false;
			}
			if (!(obj is ContentAdapter)) {
				return base.Equals(obj);
			}
			return this.ContentId.Equals((obj as ContentAdapter).ContentId);
		}
		public override int GetHashCode() {
			return this.ContentId.GetHashCode();
		}
		public override string ToString() {
			return "<" + this.ContentId + "> " + this.DisplayName;
		}
	}
}
