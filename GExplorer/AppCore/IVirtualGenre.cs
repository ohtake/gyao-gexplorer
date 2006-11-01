using System;
using System.Text;
using System.Drawing;

namespace Yusen.GExplorer.AppCore {
	interface IVirtualGenre {
		string ShortName { get;}
		string LongName { get;}
		Color Color { get;}
		CrawlResult GetCrawlResult();
	}
}
