using System;
using System.Collections.Generic;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.AppCore {
	[Serializable]
	public sealed class CrawlResult {
		private readonly List<GContentClass> contents;
		private readonly List<GContentClass> droppedContents;
		private readonly List<int> sortedCKeysNew;
		private readonly List<int> sortedCKeysModified;
		private readonly DateTime time;
		private readonly List<Uri> visitedPages;
		private readonly List<Uri> notVisitedPages;
		private readonly List<CrawlException> exceptions;

		public CrawlResult() {
			this.contents = new List<GContentClass>();
			this.droppedContents = new List<GContentClass>();
			this.sortedCKeysNew = new List<int>();
			this.sortedCKeysModified = new List<int>();
			this.time = DateTime.Now;
			this.visitedPages = new List<Uri>();
			this.notVisitedPages = new List<Uri>();
			this.exceptions = new List<CrawlException>();
		}

		public List<GContentClass> Contents {
			get { return this.contents; }
		}
		public List<GContentClass> DroppedContents {
			get { return this.droppedContents; }
		}
		public List<int> SortedCKeysNew {
			get { return this.sortedCKeysNew; }
		}
		public List<int> SortedCKeysModified {
			get { return this.sortedCKeysModified; }
		}
		public DateTime Time {
			get { return this.time; }
		}
		public List<Uri> VisitedPages {
			get { return this.visitedPages; }
		}
		public List<Uri> NotVisitedPages {
			get { return this.notVisitedPages; }
		}
		public List<CrawlException> Exceptions {
			get { return this.exceptions; }
		}
	}
}
