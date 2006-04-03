using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

#if CLIP_RESUME
namespace Yusen.GCrawler {
	[TypeConverter(typeof(ClipResumeInfoConverter))]
	public struct ClipResumeInfo {
		private int clipBegin;
		private int clipNo;
		
		public ClipResumeInfo(int clipNo, int clipBegin) {
			this.clipNo = clipNo;
			this.clipBegin = clipBegin;
		}
		
		[XmlAttribute]
		[Description("clipBegin: 中断位置の累計秒数．")]
		[DefaultValue(0)]
		public int ClipBegin {
			get { return this.clipBegin; }
			set { this.clipBegin = value; }
		}
		[XmlAttribute]
		[Description("clipNo: 中断位置のクリップナンバー．意味なし？")]
		[DefaultValue(0)]
		public int ClipNo {
			get { return this.clipNo; }
			set { this.clipNo = value; }
		}
		
		[XmlIgnore]
		[ReadOnly(true)]
		[Description("空かどうか．")]
		[DefaultValue(true)]
		public bool IsEmpty {
			get { return 0 == this.ClipBegin; }
		}
	}

	public sealed class ClipResumeInfoConverter : ExpandableObjectConverter {
		private readonly static Regex regex = new Regex(@"^\s*(\d+)\s*,\s*(\d+)\s*$");

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
			if (typeof(ClipResumeInfo) == destinationType) return true;
			else return base.CanConvertTo(context, destinationType);
		}
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
			if (typeof(string) == destinationType && value is ClipResumeInfo) {
				ClipResumeInfo resumeInfo = (ClipResumeInfo)value;
				if (resumeInfo.IsEmpty) {
					return string.Empty;
				} else {
					return resumeInfo.ClipNo + "," + resumeInfo.ClipBegin;
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
			if (sourceType == typeof(string)) return true;
			return base.CanConvertFrom(context, sourceType);
		}
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
			if (value is string) {
				try {
					string s = (value as string).Trim();
					
					if (string.IsNullOrEmpty(s)) {
						return new ClipResumeInfo();
					} else {
						Match match = ClipResumeInfoConverter.regex.Match(value as string);
						if (match.Success) {
							int no = int.Parse(match.Groups[1].Value);
							int begin = int.Parse(match.Groups[2].Value);
							return new ClipResumeInfo(no, begin);
						} else {
							throw new ArgumentException("文字列 '" + s + "' は正規表現 '" + ClipResumeInfoConverter.regex.ToString() + "' にマッチしない．");
						}
					}
				} catch (Exception e) {
					throw new ArgumentException(
						"文字列 '" + (string)value + "' を ClipResumeInfo 型に変換できません．", e);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
#endif
