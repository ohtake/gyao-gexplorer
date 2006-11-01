using System;
using System.Collections.Generic;
using System.Text;
using Yusen.GExplorer.GyaoModel;
using System.Drawing;

namespace Yusen.GExplorer.AppCore {
	sealed class SimpleContentsVirtualGenre : IVirtualGenre {
		private readonly string shortName;
		private readonly string longName;
		private readonly CrawlResult result;

		private SimpleContentsVirtualGenre() {
		}
		public SimpleContentsVirtualGenre(IEnumerable<GContentClass> conts, string shortName, string longName) {
			this.result = new CrawlResult();
			this.result.Contents.AddRange(conts);
			this.shortName = shortName;
			this.longName = longName;
		}

		#region IVirtualGenre Members
		public string ShortName {
			get { return this.shortName; }
		}
		public string LongName {
			get { return this.longName; }
		}
		public Color Color {
			get { return Color.Black; }
		}
		public CrawlResult GetCrawlResult() {
			return this.result;
		}
		#endregion
	}
}
