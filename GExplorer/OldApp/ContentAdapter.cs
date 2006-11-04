using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;

namespace Yusen.GExplorer.OldApp {
	public class ContentAdapter{
		public const string PropertyNameContentId = "ContentId";
		public const string PropertyNamePackageId = "PackageId";
		public const string PropertyNameTitle = "Title";
		
		private GContent innerCont;
		
		public ContentAdapter() {
		}
		public ContentAdapter(GContent innerCont) {
			this.InnerContent = innerCont;
		}

		[Browsable(false)]
		public GContent InnerContent {
			get {
				return this.innerCont;
			}
			set {
				this.innerCont = value;
			}
		}
		
		[XmlIgnore]
		[Category("キー")]
		[Description("コンテンツのキー．")]
		public int ContentKey {
			get { return this.innerCont.ContentKey; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("コンテンツのID．")]
		public string ContentId {
			get { return this.innerCont.ContentId; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("パッケージのキー．")]
		public int PackageKey {
			get { return this.innerCont.PackageKey; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("パッケージのID．")]
		public string PackageId {
			get { return this.innerCont.PackageId; }
		}
		[XmlIgnore]
		[Category("キー")]
		[Description("ジャンルのキー．")]
		public int GenreKey {
			get { return this.innerCont.GenreKey; }
		}
		
		[XmlIgnore]
		[Category("付随情報C")]
		[Description("タイトル．")]
		public string Title {
			get { return this.innerCont.Title; }
		}
	}
}
