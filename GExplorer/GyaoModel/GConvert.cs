using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer.GyaoModel {
	public static class GConvert {
		private static readonly Regex regexDuration = new Regex(
			@"^((?<h>\d+)時間)?((?<m>\d+)分)?((?<s>\d+)秒)?$",
			RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		public static TimeSpan ToTimeSpan(string duratin) {
			Match match = GConvert.regexDuration.Match(duratin);
			if (match.Success) {
				string sh = match.Groups["h"].Value;
				string sm = match.Groups["m"].Value;
				string ss = match.Groups["s"].Value;
				int ih = string.IsNullOrEmpty(sh) ? 0 : int.Parse(sh);
				int im = string.IsNullOrEmpty(sm) ? 0 : int.Parse(sm);
				int @is = string.IsNullOrEmpty(ss) ? 0 : int.Parse(ss);
				return new TimeSpan(ih, im, @is);
			} else {
				throw new ArgumentException();
			}
		}
		
		public static string ToGenreId(int key) {
			return string.Format("gen{0:d7}", key);
		}
		public static int ToGenreKey(string id) {
			return int.Parse(id.Substring(3)); // 3 == "gen".Length
		}
		
		public static string ToPackageId(int key) {
			return string.Format("pac{0:d7}", key);
		}
		public static int ToPackageKey(string id) {
			return int.Parse(id.Substring(3)); // 3 == "pac".Length
		}
		
		public static string ToContentId(int key) {
			return string.Format("cnt{0:d7}", key);
		}
		public static int ToContentKey(string id) {
			return int.Parse(id.Substring(3)); // 3 == "cnt".Length
		}

		public static string ToBitrateId(GBitrate bitrate) {
			switch (bitrate) {
				case GBitrate.SuperFine:
					return "bit0000002";
				case GBitrate.Standard:
					return "bit0000001";
				default:
					throw new ArgumentException();
			}
		}

	}
}
