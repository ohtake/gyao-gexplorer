using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Yusen.GExplorer.Utilities;
using Yusen.GExplorer.GyaoModel;

namespace Yusen.GExplorer.OldApp {
	[Serializable]
	public class GContent {
		private int contentKey;
		private int packageKey;
		private int genreKey;

		private string title;
		private string seriesNumber;
		private string subtitle;
		private string duration;
		private string description1;
		private string description2;
		private string description3;
		private string description4;

		private string deadline;
		private string summary;
		
		private bool fromCache;
		
		public GContent() {
			this.fromCache = true;
		}
		public int ContentKey {
			get { return this.contentKey; }
			set { this.contentKey = value; }
		}
		[XmlIgnore]
		public string ContentId {
			get { return GConvert.ToContentId(this.ContentKey);}
		}
		public int PackageKey {
			get { return this.packageKey; }
			set { this.packageKey = value; }
		}
		[XmlIgnore]
		public string PackageId {
			get { return GConvert.ToPackageId(this.PackageKey); }
		}
		public int GenreKey {
			get { return this.genreKey; }
			set { this.genreKey = value; }
		}
		
		public string Title {
			get { return this.title; }
			set { this.title = value; }
		}
		public string SeriesNumber {
			get { return this.seriesNumber; }
			set { this.seriesNumber = value; }
		}
		public string Subtitle {
			get { return this.subtitle; }
			set { this.subtitle = value; }
		}
		public string Duration {
			get { return this.duration; }
			set { this.duration = value; }
		}
		public string Description1 {
			get { return this.description1; }
			set { this.description1 = value; }
		}
		public string Description2 {
			get { return this.description2; }
			set { this.description2 = value; }
		}
		public string Description3 {
			get { return this.description3; }
			set { this.description3 = value; }
		}
		public string Description4 {
			get { return this.description4; }
			set { this.description4 = value; }
		}
		
		public string Summary {
			get { return this.summary; }
			set { this.summary = value; }
		}
		public string Deadline {
			get { return this.deadline; }
			set { this.deadline = value; }
		}
		
		[XmlIgnore]
		public bool FromCache {
			get { return this.fromCache; }
			internal set { this.fromCache = value; }
		}
	}
	
	[Serializable]
	public class ContentDownloadException : Exception {
		protected ContentDownloadException(SerializationInfo info, StreamingContext context):base(info,context){
		}
		
		public ContentDownloadException(string message)
			: base(message) {
		}
		public ContentDownloadException(string message, Exception innerException)
			: base(message, innerException) {
		}
	}
}
