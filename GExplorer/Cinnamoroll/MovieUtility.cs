using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Yusen.GExplorer.Cinnamoroll {
	static class MovieUtility {
		public const int ReleaseYear = 2007;
		public const int ReleaseMonth = 12;
		public const int ReleaseDay = 22;
		
		private static bool IsReleaseDate(DateTime dt) {
			return dt.Year == MovieUtility.ReleaseYear
				&& dt.Month == MovieUtility.ReleaseMonth
				&& dt.Day == MovieUtility.ReleaseDay;
		}
		
		[Obsolete]
		public static void ShowAdIfItsReleaseDate(IWin32Window owner) {
			if (MovieUtility.IsReleaseDate(DateTime.Today)) {
				new MovieAdForm().ShowDialog(owner);
			}
		}
	}
}
