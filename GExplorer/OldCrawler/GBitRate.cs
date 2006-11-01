using System;
using System.Collections.Generic;
using System.Text;

namespace Yusen.GExplorer.OldCrawler {
	public enum GBitRate {
		SuperFine = 2,
		Standard = 1,
	}

	internal static class GBitRateUtility {
		public static string ConvertToIdFromKey(GBitRate key) {
			return string.Format("bit{0:d7}", (int)key);
		}
	}
}
