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
				return "(展開可)";
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

		[DisplayName("サイズ")]
		[Description("ウィンドウのサイズ．")]
		public Size Size {
			get { return this.size; }
			set { this.size = value; }
		}
		[DisplayName("位置")]
		[Description("ウィンドウの位置．初期配置をManualにする必要あり．")]
		public Point Location {
			get { return this.location; }
			set { this.location = value; }
		}
		[DisplayName("初期配置")]
		[Description("起動したときに表示される位置の決定方法．")]
		[DefaultValue(FormStartPosition.Manual)]
		public FormStartPosition StartPosition {
			get { return this.startPosition; }
			set { this.startPosition = value; }
		}
		[DisplayName("状態")]
		[Description("最小化，通常，最大化．")]
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

		[DisplayName("最前面")]
		[Description("ウィンドウを常に手前に表示します．")]
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

			[DisplayName("複数選択")]
			[Description("複数選択を有効にします．誤操作防止のため False 推奨．")]
			[DefaultValue(false)]
			public bool MultiSelect {
				get { return this.multiSelect; }
				set { this.multiSelect = value; }
			}
			[DisplayName("行全体で選択")]
			[Description("表示形式が Details の時，行全体を選択します．")]
			[DefaultValue(true)]
			public bool FullRowSelect {
				get { return this.fullRowSelect; }
				set { this.fullRowSelect = value; }
			}
			[DisplayName("表示形式")]
			[Description("リストビューの表示形式を変更します．")]
			[DefaultValue(View.Details)]
			public View View {
				get { return this.view; }
				set { this.view = value; }
			}
			[DisplayName("カラム幅 (1. contents_id)")]
			[Description("リストビューのカラムの幅．(1. contents_id)")]
			[DefaultValue(90)]
			public int ColWidthId {
				get { return this.colWidthId; }
				set { this.colWidthId = value; }
			}
			[DisplayName("カラム幅 (2. 配信終了日)")]
			[Description("リストビューのカラムの幅．(2. 配信終了日)")]
			[DefaultValue(80)]
			public int ColWidthLimit {
				get { return this.colWidthLimit; }
				set { this.colWidthLimit = value; }
			}
			[DisplayName("カラム幅 (3. 話)")]
			[Description("リストビューのカラムの幅．(3. 話)")]
			[DefaultValue(70)]
			public int WidthEpisode {
				get { return this.colWidthEpisode; }
				set { this.colWidthEpisode = value; }
			}
			[DisplayName("カラム幅 (4. リード)")]
			[Description("リストビューのカラムの幅．(4. リード)")]
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

		[DisplayName("リストビュー")]
		[Description("リストビューの設定を行います．")]
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

		[DisplayName("自動音量調整")]
		[Description("CMと思わしき動画の音量を10にし，そうではないのを100にします．")]
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
