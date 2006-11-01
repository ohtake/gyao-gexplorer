using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer.OldApp {
	public struct GTimeSpan {
		private static readonly Regex regex = new Regex(
			@"^((?<h>\d+)時間)?((?<m>\d+)分)?((?<s>\d+)秒)?$",
			RegexOptions.ExplicitCapture | RegexOptions.Compiled);
		
		private TimeSpan? timeSpan;
		private string original;

		public GTimeSpan(string original) {
			if (null == original) {
				original = string.Empty;
			}
			this.original = original;
			Match match = GTimeSpan.regex.Match(original);
			if (match.Success) {
				string sh = match.Groups["h"].Value;
				string sm = match.Groups["m"].Value;
				string ss = match.Groups["s"].Value;
				int ih = string.IsNullOrEmpty(sh) ? 0 : int.Parse(sh);
				int im = string.IsNullOrEmpty(sm) ? 0 : int.Parse(sm);
				int @is = string.IsNullOrEmpty(ss) ? 0 : int.Parse(ss);
				this.timeSpan = new TimeSpan(ih, im, @is);
			} else {
				this.timeSpan = null;
			}
		}

		public bool HasTimeSpan {
			get { return this.timeSpan.HasValue; }
		}
		public TimeSpan TimeSpan {
			get { return this.timeSpan.Value; }
		}
		public string Original {
			get { return this.original; }
		}

		public override string ToString() {
			if (this.HasTimeSpan) {
				return this.TimeSpan.ToString();
			} else {
				return "? \"" + this.Original + "\"";
			}
		}
	}
}

