using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using CultureInfo = System.Globalization.CultureInfo;

namespace Yusen.GExplorer {
	public abstract class UserSettingsDescendant {
	}
	public abstract class UserSettingsChild : UserSettingsDescendant {
		internal event UserSettingsChangeCompletedEventHandler ChangeCompleted;
		internal void OnChangeCompleted() {
			if(null != this.ChangeCompleted) {
				this.ChangeCompleted();
			}
			UserSettings.Instance.OnChangeCompleted(false);
		}
	}
	
	class UserSettingsDescendantConverter : ExpandableObjectConverter {
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
	[TypeConverter(typeof(UserSettingsDescendantConverter))]
	public sealed class UscMainForm : UscForm {
		[TypeConverter(typeof(UserSettingsDescendantConverter))]
		public sealed class UsdGenreListView : UserSettingsDescendant {
			private bool multiSelect = false;
			private bool fullRowSelect = true;
			private View view = View.Details;
			private int colWidthId = 90;
			private int colWidthLimit = 80;
			private int colWidthEpisode = 70;
			private int colWidthLead = 320;

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
			internal void StoreSettings(ListView lv) {
				this.MultiSelect = lv.MultiSelect;
				this.FullRowSelect = lv.FullRowSelect;
				this.View = lv.View;
				this.ColWidthId = lv.Columns[0].Width;
				this.ColWidthLimit = lv.Columns[1].Width;
				this.WidthEpisode = lv.Columns[2].Width;
				this.ColWidthLead = lv.Columns[3].Width;
			}
			internal void ApplySettings(ListView lv) {
				if(lv.MultiSelect != this.MultiSelect) lv.MultiSelect = this.MultiSelect;
				if(lv.FullRowSelect != this.FullRowSelect) lv.FullRowSelect = this.FullRowSelect;
				if(lv.View != this.View) lv.View = this.View;
				if(lv.Columns[0].Width != this.ColWidthId) lv.Columns[0].Width = this.ColWidthId;
				if(lv.Columns[1].Width != this.ColWidthLimit) lv.Columns[1].Width = this.ColWidthLimit;
				if(lv.Columns[2].Width != this.WidthEpisode) lv.Columns[2].Width = this.WidthEpisode;
				if(lv.Columns[3].Width != this.ColWidthLead) lv.Columns[3].Width = this.ColWidthLead;
			}
		}

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
			this.LvSettings.StoreSettings(mf.ListView);
		}
		internal void ApplySettings(MainForm mf) {
			base.ApplySettings(mf);
			this.LvSettings.ApplySettings(mf.ListView);
		}
	}
	[TypeConverter(typeof(UserSettingsDescendantConverter))]
	public sealed class UscPlayerForm : UscFormTopmostable {
		private bool autoVolume = true;

		public UscPlayerForm()
			: this(Size.Empty, Point.Empty) {
		}
		public UscPlayerForm(Size size, Point location)
			: base(size, location) {
		}

		[DisplayName("�������ʒ���")]
		[Description("CM�Ǝv�킵������̉��ʂ�10�ɂ��C�����ł͂Ȃ��̂�100�ɂ��܂��D")]
		[DefaultValue(true)]
		public bool AutoVolume {
			get { return this.autoVolume; }
			set { this.autoVolume = value; }
		}
		internal void StoreSettings(PlayerForm pf) {
			base.StoreSettings(pf);
			this.AutoVolume = pf.AutoVolumeEnabled;
		}
		internal void ApplySettings(PlayerForm pf) {
			base.ApplySettings(pf);
			if(pf.AutoVolumeEnabled != this.AutoVolume) pf.AutoVolumeEnabled = this.AutoVolume;
		}
	}
}
