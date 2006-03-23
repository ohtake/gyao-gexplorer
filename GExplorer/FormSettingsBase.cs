using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	public partial class FormSettingsBase : FormBase{
		public FormSettingsBase()
			: base() {
			InitializeComponent();
		}
	}
	
	[ReadOnly(true)]
	[TypeConverter(typeof(FormSettingsBaseSettingsConverter))]
	public class FormSettingsBaseSettings : INewSettings<FormSettingsBaseSettings> {
		private readonly FormSettingsBase owner;

		public FormSettingsBaseSettings() {
		}
		internal FormSettingsBaseSettings(FormSettingsBase owner) {
			this.owner = owner;
		}

		[XmlIgnore]
		[Browsable(false)]
		private bool HasOwner {
			get { return null != this.owner; }
		}
		
		[Category("フォームの基本設定")]
		[DisplayName("通常時の領域")]
		[Description("最大化や最小化のされていない場合でのウィンドウの位置とサイズ")]
		public Rectangle? RestoreBounds {
			get {
				if (this.HasOwner) {
					if (FormWindowState.Normal == this.owner.WindowState) {
						return new Rectangle(this.owner.Location, this.owner.Size);
					} else {
						return this.owner.RestoreBounds;
					}
				} else {
					return this.restoreBounds;
				}
			}
			set {
				if (this.HasOwner && value.HasValue) {
					this.owner.StartPosition = FormStartPosition.Manual;
					this.owner.Location = value.Value.Location;
					this.owner.Size = value.Value.Size;
					this.owner.WindowState = FormWindowState.Normal;
				} else {
					this.restoreBounds = value;
				}
			}
		}
		private Rectangle? restoreBounds;

		[Category("フォームの基本設定")]
		[DisplayName("最大化")]
		[Description("最大化されている場合はTrue．")]
		[DefaultValue(false)]
		public bool IsMaximized {
			get {
				if (this.HasOwner) return this.owner.WindowState == FormWindowState.Maximized;
				return this.isMaximized;
			}
			set {
				if (this.HasOwner) {
					this.owner.WindowState = value ? FormWindowState.Maximized : FormWindowState.Normal;
				} else {
					this.isMaximized = value;
				}
			}
		}
		private bool isMaximized = false;

		#region INewSettings<FormSettingsBaseSettings> Members
		public void ApplySettings(FormSettingsBaseSettings newSettings) {
			//this.owner.Visible = false;
			this.RestoreBounds = newSettings.RestoreBounds;
			this.IsMaximized = newSettings.IsMaximized;
			//this.owner.Visible = true;
		}
		#endregion
	}
	public sealed class FormSettingsBaseSettingsConverter : ExpandableObjectConverter {
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType){
			if (typeof(FormSettingsBaseSettings) == destinationType) return true;
			else return base.CanConvertTo(context, destinationType);
		}
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if (typeof(string) == destinationType && value is FormSettingsBaseSettings) {
				return "(展開可)";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
