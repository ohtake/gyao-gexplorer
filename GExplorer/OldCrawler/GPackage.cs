using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Yusen.GExplorer.Utilities;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.OldCrawler {
	[Serializable]
	public class GPackage {
		private int packageKey;
		private int genreKey;
		
		private readonly string packageName;
		private ReadOnlyCollection<GContent> contents;

		private GPackage(int packageKey, int genreKey, string packageName) {
			this.packageKey = packageKey;
			this.genreKey = genreKey;
			this.packageName = packageName;
		}

		public int PackageKey {
			get { return this.packageKey; }
		}
		public string PackageId {
			get { return GConvert.ToPackageId(this.PackageKey); }
		}
		public string PackageName {
			get { return this.packageName; }
		}

		public ReadOnlyCollection<GContent> Contents {
			get { return this.contents; }
			internal set { this.contents = value; }
		}
	}
}
