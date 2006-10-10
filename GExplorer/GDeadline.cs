using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer {
	public struct GDeadline {
		private static readonly Regex regex = new Regex(
			@"^(?<Month>\d{1,2})/(?<Date>\d{1,2})（(?<Day>.)）(?<Hour>.+)まで$",
			RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		private int month;
		private int date;
		private char day;
		private int hour;
		private string original;
		
		private string result;

		public GDeadline(string original) {
			if (null == original) {
				original = string.Empty;
			}
			this.original = original;
			Match match = GDeadline.regex.Match(original);
			if (match.Success) {
				this.month = int.Parse(match.Groups["Month"].Value);
				this.date = int.Parse(match.Groups["Date"].Value);
				this.day = match.Groups["Day"].Value[0];
				string sh = match.Groups["Hour"].Value;
				if ("正午" == sh) {
					this.hour = 12;
				} else if (sh.EndsWith("時")) {
					sh = sh.Substring(0, sh.Length - 1);
					if (!int.TryParse(sh, out this.hour)) {
						goto failed;
					}
				} else {
					goto failed;
				}
				this.result = string.Format("{0:d2}/{1:d2}({2}){3:d2}時", this.month, this.date, this.day, this.hour);
				return;
			}
		failed:
			this.month = 0;
			this.date = 0;
			this.day = ' ';
			this.hour = 0;
			this.result = null;
		}
		public bool CanParse {
			get { return null != this.result; }
		}
		
		public override string ToString() {
			if (this.CanParse) {
				return this.result;
			} else if (!string.IsNullOrEmpty(this.original)) {
				return "? \"" + this.original + "\"";
			} else {
				return string.Empty;
			}
		}
	}
}
