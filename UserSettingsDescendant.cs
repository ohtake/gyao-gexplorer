using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using CultureInfo = System.Globalization.CultureInfo;
using System.Reflection;

namespace Yusen.GExplorer {
	/// <summary><see cref="UserSettings"/>�����[�g�Ƃ���؍\���ł̎q���m�[�h�D</summary>
	/// <seealso cref="UserSettings"/>
	public abstract class UserSettingsDescendant {
	}
	/// <summary>
	/// <see cref="UserSettings"/>�����[�g�Ƃ���؍\���ł̎q���m�[�h�ł���C
	/// �Ȃ�����<see cref="UserSettings"/>�̒����ɂ���m�[�h�D
	/// </summary>
	/// <remarks>
	/// <see cref="UserSettingsDescendant"/>�Ƃ͈Ⴂ�C
	/// �����̃m�[�h��<see cref="UserSettingsChangeCompletedEventHandler"/>�̃C�x���g��
	/// ���������邱�Ƃ��o����D
	/// ���̃C�x���g�������鎞�ɂ͐e�m�[�h�ł����l�̃C�x���g�𔭐�������D
	/// </remarks>
	/// <seealso cref="UserSettings"/>
	public abstract class UserSettingsChild : UserSettingsDescendant {
		internal event UserSettingsChangeCompletedEventHandler ChangeCompleted;
		internal void OnChangeCompleted() {
			if(null != this.ChangeCompleted) {
				this.ChangeCompleted();
			}
			UserSettings.Instance.OnChangeCompleted(false);
		}
	}
	
	internal class UserSettingsDescendantConverter : ExpandableObjectConverter {
		public override bool CanConvertTo(
				ITypeDescriptorContext context, Type destinationType) {
			if(typeof(UserSettingsDescendant) == destinationType) {
				return true;
			} else {
				return base.CanConvertTo(context, destinationType);
			}
		}
		public override object ConvertTo(
				ITypeDescriptorContext context, CultureInfo culture,
				object value, Type destinationType) {
			if(typeof(System.String) == destinationType
					&& value is UserSettingsDescendant) {
				return "(�W�J��)";
			} else {
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
	/// <summary><see cref="Form"/>�̐ݒ�D</summary>
	/// <seealso cref="UserSettings"/>
	[TypeConverter(typeof(UserSettingsDescendantConverter))]
	public class UscForm : UserSettingsChild {
		private Size size;
		private Point location;
		private FormStartPosition startPosition = FormStartPosition.Manual;
		private FormWindowState windowState = FormWindowState.Normal;

		public UscForm()
			: this(Size.Empty, Point.Empty) {
		}
		public UscForm(Size size, Point location) {
			this.size = size;
			this.location = location;
		}

		[DisplayName("�T�C�Y")]
		[Description("�E�B���h�E�̃T�C�Y�D")]
		public Size Size {
			get { return this.size; }
			set { this.size = value; }
		}
		[DisplayName("�ʒu")]
		[Description("�E�B���h�E�̈ʒu�D�����z�u��Manual�ɂ���K�v����D")]
		public Point Location {
			get { return this.location; }
			set { this.location = value; }
		}
		[DisplayName("�����z�u")]
		[Description("�N�������Ƃ��ɕ\�������ʒu�̌�����@�D")]
		[DefaultValue(FormStartPosition.Manual)]
		public FormStartPosition StartPosition {
			get { return this.startPosition; }
			set { this.startPosition = value; }
		}
		[DisplayName("���")]
		[Description("�ŏ����C�ʏ�C�ő剻�D")]
		[DefaultValue(FormWindowState.Normal)]
		public FormWindowState WindowState {
			get { return this.windowState; }
			set { this.windowState = value; }
		}

		internal void StoreSettings(Form f) {
			this.Size = f.Size;
			this.Location = f.Location;
			this.StartPosition = f.StartPosition;
			this.WindowState = f.WindowState;
		}

		internal void ApplySettings(Form f) {
			if(f.Size != this.Size) f.Size = this.Size;
			if(f.Location != this.Location) f.Location = this.Location;
			if(f.StartPosition != this.StartPosition) f.StartPosition = this.StartPosition;
			if(f.WindowState != this.WindowState) f.WindowState = this.WindowState;
		}
	}
	/// <summary><see cref="UscForm"/>�̋@�\�ɉ�����<see cref="Form.TopMost"/>���ۑ�����D</summary>
	/// <seealso cref="UserSettings"/>
	[TypeConverter(typeof(UserSettingsDescendantConverter))]
	public class UscFormTopmostable : UscForm {
		private bool topMost = false;

		public UscFormTopmostable()
			: this(Size.Empty, Point.Empty) {
		}
		public UscFormTopmostable(Size size, Point location)
			: base(size, location) {
		}

		[DisplayName("�őO��")]
		[Description("�E�B���h�E����Ɏ�O�ɕ\�����܂��D")]
		[DefaultValue(false)]
		public bool TopMost {
			get { return this.topMost; }
			set { this.topMost = value; }
		}
		internal new void StoreSettings(Form f) {
			base.StoreSettings(f);
			this.topMost = f.TopMost;
		}
		internal new void ApplySettings(Form f) {
			base.ApplySettings(f);
			if(f.TopMost != this.TopMost) f.TopMost = this.TopMost;
		}
	}
	/// <summary><see cref="MainForm"/>�̐ݒ�D<see cref="GenreListView"/>�̐ݒ���܂ށD</summary>
	/// <seealso cref="UserSettings"/>
	[TypeConverter(typeof(UserSettingsDescendantConverter))]
	public sealed class UscMainForm : UscForm {
		private UsdGenreListView lvSettings = new UsdGenreListView();
		
		public UscMainForm()
			: this(Size.Empty, Point.Empty) {
		}
		public UscMainForm(Size size, Point location)
			: base(size, location) {
		}

		[DisplayName("���X�g�r���[")]
		[Description("���X�g�r���[�̐ݒ���s���܂��D")]
		public UsdGenreListView LvSettings {
			get { return this.lvSettings; }
			set { this.lvSettings = value; }
		}

		internal void StoreSettings(MainForm mf) {
			base.StoreSettings(mf);
			this.LvSettings.StoreSettings(mf.GenreListView);
		}
		internal void ApplySettings(MainForm mf) {
			base.ApplySettings(mf);
			this.LvSettings.ApplySettings(mf.GenreListView);
		}
	}
	/// <summary><see cref="GenreListView"/>�̐ݒ�D</summary>
	/// <seealso cref="UserSettings"/>
	[TypeConverter(typeof(UserSettingsDescendantConverter))]
	public sealed class UsdGenreListView : UserSettingsDescendant {
		private bool multiSelect = false;
		private bool fullRowSelect = true;
		private View view = View.Details;
		private int colWidthId = 90;
		private int colWidthLimit = 80;
		private int colWidthEpisode = 70;
		private int colWidthLead = 320;
		private AboneType aboneType = AboneType.Toumei;

		public UsdGenreListView() {
		}

		[DisplayName("�����I��")]
		[Description("�����I����L���ɂ��܂��D�둀��h�~�̂��� False �����D")]
		[DefaultValue(false)]
		public bool MultiSelect {
			get { return this.multiSelect; }
			set { this.multiSelect = value; }
		}
		[DisplayName("�s�S�̂őI��")]
		[Description("�\���`���� Details �̎��C�s�S�̂�I�����܂��D")]
		[DefaultValue(true)]
		public bool FullRowSelect {
			get { return this.fullRowSelect; }
			set { this.fullRowSelect = value; }
		}
		[DisplayName("�\���`��")]
		[Description("���X�g�r���[�̕\���`����ύX���܂��D")]
		[DefaultValue(View.Details)]
		public View View {
			get { return this.view; }
			set { this.view = value; }
		}
		[DisplayName("�J������ (1. contents_id)")]
		[Description("���X�g�r���[�̃J�����̕��D(1. contents_id)")]
		[DefaultValue(90)]
		public int ColWidthId {
			get { return this.colWidthId; }
			set { this.colWidthId = value; }
		}
		[DisplayName("�J������ (2. �z�M�I����)")]
		[Description("���X�g�r���[�̃J�����̕��D(2. �z�M�I����)")]
		[DefaultValue(80)]
		public int ColWidthLimit {
			get { return this.colWidthLimit; }
			set { this.colWidthLimit = value; }
		}
		[DisplayName("�J������ (3. �b)")]
		[Description("���X�g�r���[�̃J�����̕��D(3. �b)")]
		[DefaultValue(70)]
		public int WidthEpisode {
			get { return this.colWidthEpisode; }
			set { this.colWidthEpisode = value; }
		}
		[DisplayName("�J������ (4. ���[�h)")]
		[Description("���X�g�r���[�̃J�����̕��D(4. ���[�h)")]
		[DefaultValue(320)]
		public int ColWidthLead {
			get { return this.colWidthLead; }
			set { this.colWidthLead = value; }
		}
		[DisplayName("���ځ`����@")]
		[Description("Toumei: ����; Sabori: ���ڂ�; Hakidame: �|������;")]
		[DefaultValue(AboneType.Toumei)]
		public AboneType AboneType {
			get { return this.aboneType; }
			set { this.aboneType = value; }
		}
		internal void StoreSettings(GenreListView glv) {
			this.MultiSelect = glv.MultiSelect;
			this.FullRowSelect = glv.FullRowSelect;
			this.View = glv.View;
			this.ColWidthId = glv.ColWidthId;
			this.ColWidthLimit = glv.ColWidthLimit;
			this.WidthEpisode = glv.ColWidthEpisode;
			this.ColWidthLead = glv.ColWidthLead;
			this.AboneType = glv.AboneType;
		}
		internal void ApplySettings(GenreListView glv) {
			if (glv.MultiSelect != this.MultiSelect) glv.MultiSelect = this.MultiSelect;
			if (glv.FullRowSelect != this.FullRowSelect) glv.FullRowSelect = this.FullRowSelect;
			if (glv.View != this.View) glv.View = this.View;
			if (glv.ColWidthId != this.ColWidthId) glv.ColWidthId = this.ColWidthId;
			if (glv.ColWidthLimit!= this.ColWidthLimit) glv.ColWidthLimit = this.ColWidthLimit;
			if (glv.ColWidthEpisode != this.WidthEpisode) glv.ColWidthEpisode = this.WidthEpisode;
			if (glv.ColWidthLead != this.ColWidthLead) glv.ColWidthLead = this.ColWidthLead;
			if (glv.AboneType != this.AboneType) glv.AboneType = this.AboneType;
		}
	}
	
	/// <summary><see cref="PlayerForm"/>�̐ݒ�D</summary>
	/// <seealso cref="UserSettings"/>
	[TypeConverter(typeof(UserSettingsDescendantConverter))]
	public sealed class UscPlayerForm : UscFormTopmostable {
		private bool autoVolume = true;
		private bool mediaKeys = true;
		private bool closeOnEnd = true;
		
		public UscPlayerForm()
			: this(Size.Empty, Point.Empty) {
		}
		public UscPlayerForm(Size size, Point location)
			: base(size, location) {
		}
		
		[DisplayName("�������ʒ���")]
		[Description("CM�Ǝv�킵������̉��ʂ�20�ɂ��C�����ł͂Ȃ��̂�100�ɂ���D")]
		[DefaultValue(true)]
		public bool AutoVolume {
			get { return this.autoVolume; }
			set { this.autoVolume = value; }
		}
		[DisplayName("���f�B�A�L�[���g��")]
		[Description("�L�[�{�[�h�́u�Đ��E�ꎞ��~�v�Ȃǂ̃L�[��L���ɂ���D�������Đ��E�B���h�E�Ƀt�H�[�J�X���Ȃ��Ɠ��삵�Ȃ��D")]
		[DefaultValue(true)]
		public bool MediaKeys {
			get { return this.mediaKeys; }
			set { this.mediaKeys = value; }
		}
		[DisplayName("�Đ��I���ŕ���")]
		[Description("�Đ����I��������E�B���h�E�������I�ɕ���D")]
		[DefaultValue(true)]
		public bool CloseOnEnd {
			get { return this.closeOnEnd; }
			set { this.closeOnEnd = value; }
		}
		
		internal void StoreSettings(PlayerForm pf) {
			base.StoreSettings(pf);
			this.AutoVolume = pf.AutoVolumeEnabled;
			this.CloseOnEnd = pf.CloseOnEnd;
		}
		internal void ApplySettings(PlayerForm pf) {
			base.ApplySettings(pf);
			if(pf.AutoVolumeEnabled != this.AutoVolume) pf.AutoVolumeEnabled = this.AutoVolume;
			if(pf.CloseOnEnd != this.CloseOnEnd) pf.CloseOnEnd = this.CloseOnEnd;
		}
	}
}
