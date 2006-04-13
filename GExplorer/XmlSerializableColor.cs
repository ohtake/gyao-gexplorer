using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Globalization;

namespace Yusen.GExplorer {
	[TypeConverter(typeof(XmlSerializableColorConverter))]
	public struct XmlSerializableColor {
		private bool isNamedColor;
		private string name;
		private byte red;
		private byte green;
		private byte blue;
		private byte alpha;
		
		public XmlSerializableColor(Color c) {
			this.isNamedColor = c.IsNamedColor;
			if(this.isNamedColor){
				this.name = c.Name;
			} else {
				this.name = null;
			}
			this.red = c.R;
			this.green = c.G;
			this.blue = c.B;
			this.alpha = c.A;
		}

		[XmlAttribute]
		public bool IsKnownColor {
			get { return this.isNamedColor; }
			set { this.isNamedColor = value; }
		}
		[XmlAttribute]
		public string Name {
			get { return this.name; }
			set { this.name = value; }
		}
		[XmlAttribute]
		public byte Red {
			get { return this.red; }
			set { this.red = value; }
		}
		[XmlAttribute]
		public byte Green {
			get { return this.green; }
			set { this.green = value; }
		}
		[XmlAttribute]
		public byte Blue {
			get { return this.blue; }
			set { this.blue = value; }
		}
		[XmlAttribute]
		public byte Alpha {
			get { return this.alpha; }
			set { this.alpha = value; }
		}

		public Color ToColor() {
			if (this.isNamedColor) {
				return Color.FromName(this.name);
			} else {
				return Color.FromArgb(this.alpha, this.red, this.green, this.blue);
			}
		}
	}

	public class XmlSerializableColorConverter : ColorConverter {
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			if (typeof(XmlSerializableColor) == destinationType) return true;
			else return base.CanConvertTo(context, destinationType);
		}
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if (typeof(string) == destinationType && value is XmlSerializableColor) {
				return base.ConvertTo(context, culture, ((XmlSerializableColor)value).ToColor(), destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			return base.CanConvertFrom(context, sourceType);
		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			if (value is string) {
				Color color = (Color)base.ConvertFrom(context, culture, value);
				return new XmlSerializableColor(color);
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
