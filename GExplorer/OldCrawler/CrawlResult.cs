using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Yusen.GExplorer.OldCrawler {
	[Serializable]
	public class CrawlResult {
		private readonly GGenre genre;
		private readonly ReadOnlyCollection<GPackage> packages;
		private readonly ReadOnlyCollection<Uri> visitedPages;
		private readonly ReadOnlyCollection<Exception> ignoredExceptions;
		private readonly DateTime time;

		internal CrawlResult(
				GGenre genre,
				ReadOnlyCollection<GPackage> packages, ReadOnlyCollection<Uri> vPages,
				ReadOnlyCollection<Exception> ignoredExceptions) {
			this.genre = genre;
			this.packages = packages;
			this.visitedPages = vPages;
			this.ignoredExceptions = ignoredExceptions;
			this.time = DateTime.Now;
		}
		
		public GGenre Genre {
			get { return this.genre; }
		}
		public ReadOnlyCollection<GPackage> Packages {
			get { return this.packages; }
		}
		public ReadOnlyCollection<Uri> VisitedPages {
			get {return this.visitedPages;}
		}
		public ReadOnlyCollection<Exception> IgnoredExceptions {
			get { return this.ignoredExceptions; }
		}
		public DateTime Time {
			get { return this.time; }
		}
	}
}
