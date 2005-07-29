using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using CultureInfo = System.Globalization.CultureInfo;
using System.Reflection;

namespace Yusen.GExplorer {
	/// <summary><see cref="UserSettings"/>をルートとする木構造での子孫ノード．</summary>
	/// <seealso cref="UserSettings"/>
	public abstract class UserSettingsDescendant {
	}
	/// <summary>
	/// <see cref="UserSettings"/>をルートとする木構造での子孫ノードであり，
	/// なおかつ<see cref="UserSettings"/>の直下にあるノード．
	/// </summary>
	/// <remarks>
	/// <see cref="UserSettingsDescendant"/>とは違い，
	/// 直下のノードは<see cref="UserSettingsChangeCompletedEventHandler"/>のイベントを
	/// 発生させることが出来る．
	/// このイベントがおきる時には親ノードでも同様のイベントを発生させる．
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
				return "(展開可)";
			} else {
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
	/// <summary><see cref="Form"/>の設定．</summary>
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
	/// <summary><see cref="UscForm"/>の機能に加えて<see cref="Form.TopMost"/>も保存する．</summary>
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
	/// <summary><see cref="MainForm"/>の設定．<see cref="GenreListView"/>の設定も含む．</summary>
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

		[DisplayName("リストビュー")]
		[Description("リストビューの設定を行います．")]
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
	/// <summary><see cref="GenreListView"/>の設定．</summary>
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
		[DisplayName("あぼ～ん方法")]
		[Description("Toumei: 透明; Sabori: さぼり; Hakidame: 掃き溜め;")]
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
	
	/// <summary><see cref="PlayerForm"/>の設定．</summary>
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
		
		[DisplayName("自動音量調整")]
		[Description("CMと思わしき動画の音量を20にし，そうではないのを100にする．")]
		[DefaultValue(true)]
		public bool AutoVolume {
			get { return this.autoVolume; }
			set { this.autoVolume = value; }
		}
		[DisplayName("メディアキーを使う")]
		[Description("キーボードの「再生・一時停止」などのキーを有効にする．ただし再生ウィンドウにフォーカスがないと動作しない．")]
		[DefaultValue(true)]
		public bool MediaKeys {
			get { return this.mediaKeys; }
			set { this.mediaKeys = value; }
		}
		[DisplayName("再生終了で閉じる")]
		[Description("再生が終了したらウィンドウを自動的に閉じる．")]
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
