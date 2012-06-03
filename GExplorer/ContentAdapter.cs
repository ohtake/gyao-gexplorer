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
		[Description("�R���e���c��ID�D")]
		public string ContentId {
			get { return this.innerCont.ContentId; }
		}
		[Category("�t�����")]
		[Description("�^�C�g���D")]
		public string Title {
			get { return this.innerCont.Title; }
		}
		[Category("�t�����")]
		[Description("�T�u�^�C�g���D")]
		public string SubTitle {
			get { return this.innerCont.SubTitle; }
		}
		[Category("�t�����")]
		[Description("�b���D")]
		public string EpisodeNumber {
			get { return this.innerCont.EpisodeNumber; }
		}
		[Category("�t�����")]
		[Description("�������ԁD")]
		public string Duration {
			get { return this.innerCont.Duration; }
		}
		[Category("�t�����")]
		[Description("�ڍ׋L�q(��)�D")]
		public string LongDescription {
			get { return this.innerCont.LongDescription; }
		}
		[Category("��u�����t���������")]
		[Description("True�̏ꍇ�̓L���b�V������ǂ܂ꂽ���Ƃ������D")]
		public bool FromCache {
			get { return this.innerCont.FromCache; }
		}
		[Category("��u�����t���������")]
		[Description("�W����������^�C�g����K���ɑg�ݍ��킹���\�����D")]
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
		[Description("�ڍ׃y�[�W��URI�D")]
		public Uri DetailPageUri {
			get { return this.innerCont.DetailPageUri; }
		}
		[Category("URI")]
		[Description("IE�Ő��K�ɍĐ�����ꍇ��URI�D(�O���[�o���ݒ�̃r�b�g���[�g�̉e�����󂯂�D)")]
		public Uri PlayerPageUri {
			get {
				return GContent.CreatePlayerPageUri(this.ContentId, GlobalSettings.Instance.BitRate);
			}
		}
		[Category("URI")]
		[Description("�v���C���X�g��URI�D(�O���[�o���ݒ�̃r�b�g���[�g�̉e�����󂯂�D���[�UID���܂ށD)")]
		public Uri PlayListUri {
			get {
				return GContent.CreatePlayListUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate);
			}
		}
		[Category("URI")]
		[Description("����t�@�C����URI�D(�O���[�o���ݒ�̃r�b�g���[�g�̉e�����󂯂�D���[�UID���܂ށD)")]
		public Uri MediaFileUri {
			get {
				return GContent.CreateMediaFileUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate);
			}
		}
		[Category("URI")]
		[Description("�R���e���c�̉摜(��)��URI�D")]
		public Uri ImageLargeUri {
			get {return this.innerCont.ImageLargeUri;}
		}
		[Category("URI")]
		[Description("�R���e���c�̉摜(��)��URI�D")]
		public Uri ImageSmallUri {
			get { return this.innerCont.ImageSmallUri; }
		}
		[Category("URI")]
		[Description("�����ߔԑg�̈ē��y�[�W��URI�D (�O���[�o���ݒ�̃r�b�g���[�g�̉e�����󂯂�D)")]
		public Uri RecomendPageUri {
			get {
				return GContent.CreateRecomendPageUri(this.ContentId, GlobalSettings.Instance.BitRate);
			}
		}
	}
}
