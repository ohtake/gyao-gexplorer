using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Yusen.GCrawler;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public class ContentAdapter {
		private GContent innerCont;
		private GTimeSpan gTimeSpan;
		private string deadLine = string.Empty;
		private string comment = string.Empty;

		public ContentAdapter() {
		}
		public ContentAdapter(GContent innerCont) {
			this.InnerContent = innerCont;
			this.TryResetDeadline();
		}
		
		[Browsable(false)]
		public GContent InnerContent {
			get {
				return this.innerCont;
			}
			set {
				this.innerCont = value;
				this.gTimeSpan = new GTimeSpan(this.innerCont.Duration);
			}
		}
		[ReadOnly(true)]
		[Category("��u�����t���������")]
		[Description("�z�M�����D���Ȃ�K���Ȃ̂ŕK�������M�p�ł���킯����Ȃ��D")]
		public string DeadLine {
			get { return this.deadLine; }
			set { this.deadLine = value; }
		}
		[Category("���[�U�����͂�����")]
		[Description("�R�����g�D���[�U�����R�ɓ��͂ł���D�������v���C���X�g�ɓ����Ă�����̂ɑ΂��ē��͂��Ȃ��ƂقƂ�ǈӖ��Ȃ��D")]
		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		
		
		[XmlIgnore]
		[Category("�L�[")]
		[Description("�R���e���c��ID�D")]
		public string ContentId {
			get { return this.innerCont.ContentId; }
		}
		[XmlIgnore]
		[Category("�t�����")]
		[Description("�W���������D")]
		public string GenreName {
			get { return this.innerCont.GenreName; }
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
				sb.Append("[" + this.GenreName + "]");
				sb.Append(" " + this.Title);
				if (! string.IsNullOrEmpty(this.EpisodeNumber)) {
					sb.Append(" / " + this.EpisodeNumber);
				}
				if (! string.IsNullOrEmpty(this.SubTitle) && this.Title != this.SubTitle) {
					sb.Append(" / " + this.SubTitle);
				}
				return sb.ToString();
			}
		}
		[XmlIgnore]
		[Category("��u�����t���������")]
		[Description("�������Ԃ̃p�[�Y���ʁD")]
		public GTimeSpan GTimeSpan {
			get {
				return this.gTimeSpan;
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
		public Uri RecommendPageUri {
			get {
				return GContent.CreateRecommendPageUri(this.ContentId, GlobalSettings.Instance.BitRate);
			}
		}
		
		
		public Uri ChapterMediaFileUriOf(int chapterNo) {
			return GContent.CreateMediaFileUri(this.ContentId, GlobalSettings.Instance.UserNo, GlobalSettings.Instance.BitRate, chapterNo);
		}
		public bool TryResetDeadline() {
			if (GlobalVariables.DeadLineDictionaryReadonly.TryGetDeadLine(this.ContentId, out this.deadLine)) {
				return true;
			} else {
				this.deadLine = string.Empty;
				return false;
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

	class SelectedContentsChangedEventArgs : EventArgs {
		private ReadOnlyCollection<ContentAdapter> contents;

		public SelectedContentsChangedEventArgs() {
			this.contents = new ReadOnlyCollection<ContentAdapter>(new List<ContentAdapter>());
		}
		public SelectedContentsChangedEventArgs(IEnumerable<ContentAdapter> contents) {
			this.contents = new ReadOnlyCollection<ContentAdapter>(new List<ContentAdapter>(contents));
		}
		public ReadOnlyCollection<ContentAdapter> Contents {
			get { return this.contents; }
		}
	}

	class ContentSelectionChangedEventArgs : EventArgs {
		private readonly ContentAdapter content;
		private readonly bool isSelected;
		public ContentSelectionChangedEventArgs(ContentAdapter content, bool isSelected) {
			this.content = content;
			this.isSelected = isSelected;
		}
		public ContentAdapter Content {
			get { return this.content; }
		}
		public bool IsSelected {
			get { return this.isSelected; }
		}
	}
}
