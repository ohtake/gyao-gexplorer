using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer {
	public struct GDeadline {
		private static readonly Regex regex = new Regex(
			@"^(?<Month>\d{1,2})/(?<Date>\d{1,2})（(?<Day>.)）正午まで$",
			RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		private bool canParse;
		private int month;
		private int date;
		private char day;
		private string original;

		public GDeadline(string original) {
			if (null == original) {
				original = string.Empty;
			}
			this.original = original;
			Match match = GDeadline.regex.Match(original);
			this.canParse = match.Success;
			if (this.canParse) {
				this.month = int.Parse(match.Groups["Month"].Value);
				this.date = int.Parse(match.Groups["Date"].Value);
				this.day = match.Groups["Day"].Value[0];
			} else {
				this.month = 0;
				this.date = 0;
				this.day = ' ';
			}
		}
		public bool CanParse {
			get { return this.canParse; }
		}
		public string Original {
			get { return this.original; }
		}

		public override string ToString() {
			if (this.CanParse) {
				return string.Format("{0:d2}/{1:d2}({2})正午まで", this.month, this.date, this.day);
			} else if (!string.IsNullOrEmpty(this.original)) {
				return "? \"" + this.original + "\"";
			} else {
				return string.Empty;
			}
		}
	}
}
