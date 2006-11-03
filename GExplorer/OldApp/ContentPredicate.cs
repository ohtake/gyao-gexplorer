using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;

namespace Yusen.GExplorer.OldApp {
	public sealed class ContentPredicate {
		internal static readonly string PredicateNameEquals = "Equals";
		internal static readonly string PredicateNameContains = "Contains";
		internal static readonly string PredicateNameStartsWith = "StartsWith";
		internal static readonly string PredicateNameEndsWith = "EndsWith";
		internal static readonly string[] PredicateNames = new string[] {
			ContentPredicate.PredicateNameEquals, ContentPredicate.PredicateNameContains,
			ContentPredicate.PredicateNameStartsWith, ContentPredicate.PredicateNameEndsWith };
		internal static bool IsInvalidContentPredicate(ContentPredicate cp) {
			return null == cp.subjInfo || null == cp.predInfo;
		}
		
		private string comment;
		private string subjectName;
		private string predicateName;
		private string objectValue;
		private DateTime createdTime;
		private DateTime lastTrueTime;
		
		private PropertyInfo subjInfo;
		private MethodInfo predInfo;

		
		public ContentPredicate() {
		}
		public ContentPredicate(string comment, string subjectName, string predicateName, string objectValue) {
			this.Comment = comment;
			this.SubjectName = subjectName;
			this.PredicateName = predicateName;
			this.ObjectValue = objectValue;
			this.CreatedTime = DateTime.Now;

			if (null == this.subjInfo) {
				throw new ArgumentException("指定された主語に対応するプロパティが見つかりませんでした: " + subjectName);
			}
			if (null == this.predInfo) {
				throw new ArgumentException("指定された述語に対応するメソッドが見つかりませんでした: " + predicateName);
			}
		}

		[XmlAttribute]
		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		[XmlAttribute]
		public string SubjectName {
			get { return this.subjectName; }
			set {
				this.subjectName = value;
				this.subjInfo = typeof(ContentAdapter).GetProperty(value);
			}
		}
		[XmlAttribute]
		public string PredicateName {
			get { return this.predicateName; }
			set {
				this.predicateName = value;
				this.predInfo = typeof(string).GetMethod(value, new Type[] { typeof(string) });
			}
		}
		[XmlAttribute]
		public string ObjectValue {
			get { return this.objectValue; }
			set { this.objectValue = value; }
		}
		[XmlAttribute]
		public DateTime CreatedTime {
			get { return this.createdTime; }
			set { this.createdTime = value; }
		}
		[XmlAttribute]
		public DateTime LastTrueTime {
			get { return this.lastTrueTime; }
			set { this.lastTrueTime = value; }
		}
		
		internal bool IsTrueFor(ContentAdapter cont) {
			string subjValue = this.subjInfo.GetValue(cont, null).ToString();
			return (bool)this.predInfo.Invoke(subjValue, new object[] { this.ObjectValue });
		}
	}

	internal sealed class ContentPredicatesManager : ItemsManagerBase<ContentPredicate> {
		private static ContentPredicatesManager ngManager = new ContentPredicatesManager("ContentPredicatesNg.xml");
		public static ContentPredicatesManager NgManager {
			get {return ContentPredicatesManager.ngManager;}
		}
		private static ContentPredicatesManager favManager = new ContentPredicatesManager("ContentPredicatesFav.xml");
		public static ContentPredicatesManager FavManager {
			get { return ContentPredicatesManager.favManager; }
		}
		
		public event EventHandler PredicatesChanged;
		public event EventHandler LastTrueChanged;

		private string settingFileName;

		private SortedDictionary<string, ContentPredicate> dicCntId = new SortedDictionary<string, ContentPredicate>();
		private SortedDictionary<string, ContentPredicate> dicPacId = new SortedDictionary<string, ContentPredicate>();
		private SortedDictionary<string, ContentPredicate> dicTitle = new SortedDictionary<string, ContentPredicate>();
		private List<ContentPredicate> listNoneAcc = new List<ContentPredicate>();

		private ContentPredicatesManager(string settingFileName) : base() {
			this.settingFileName = settingFileName;
		}

		private void RecreateAccelarationList() {
			this.dicCntId.Clear();
			this.dicPacId.Clear();
			this.dicTitle.Clear();
			this.listNoneAcc.Clear();

			foreach (ContentPredicate cp in base.items) {
				if (ContentPredicate.PredicateNameEquals.Equals(cp.PredicateName)) {
					switch (cp.SubjectName) {
						case ContentAdapter.PropertyNameContentId:
							this.dicCntId[cp.ObjectValue] = cp;
							continue;
						case ContentAdapter.PropertyNamePackageId:
							this.dicPacId[cp.ObjectValue] = cp;
							continue;
						case ContentAdapter.PropertyNameTitle:
							this.dicTitle[cp.ObjectValue] = cp;
							continue;
					}
				}
				this.listNoneAcc.Add(cp);
			}
		}

		protected override void OnChanged() {
			this.RecreateAccelarationList();
			EventHandler handler = this.PredicatesChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnLastTrueChanged() {
			EventHandler handler = this.LastTrueChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}

		public bool IsTrueFor(ContentAdapter cont) {
			{
				ContentPredicate cp;
				if (this.dicCntId.TryGetValue(cont.ContentId, out cp)) {
					cp.LastTrueTime = DateTime.Now;
					this.OnLastTrueChanged();
					return true;
				}
				if (this.dicPacId.TryGetValue(cont.PackageId, out cp)) {
					cp.LastTrueTime = DateTime.Now;
					this.OnLastTrueChanged();
					return true;
				}
				if (this.dicTitle.TryGetValue(cont.Title, out cp)) {
					cp.LastTrueTime = DateTime.Now;
					this.OnLastTrueChanged();
					return true;
				}
			}
			foreach (ContentPredicate cp in this.listNoneAcc) {
				if (cp.IsTrueFor(cont)) {
					cp.LastTrueTime = DateTime.Now;
					this.OnLastTrueChanged();
					return true;
				}
			}
			return false;
		}

		public ContentPredicate[] EnumerateTruePredicatesFor(ContentAdapter cont) {
			List<ContentPredicate> cps = new List<ContentPredicate>();
			{
				ContentPredicate cp;
				if (this.dicCntId.TryGetValue(cont.ContentId, out cp)) {
					cps.Add(cp);
				}
				if (this.dicPacId.TryGetValue(cont.PackageId, out cp)) {
					cps.Add(cp);
				}
				if (this.dicTitle.TryGetValue(cont.Title, out cp)) {
					cps.Add(cp);
				}
			}
			foreach (ContentPredicate cp in this.listNoneAcc) {
				if (cp.IsTrueFor(cont)) {
					cps.Add(cp);
				}
			}
			return cps.ToArray();
		}
		protected override string FilenameForSerialization {
			get { return this.settingFileName; }
		}

		public int AcceralatedCntIdPredicatesCount {
			get { return this.dicCntId.Count; }
		}
		public int AcceralatedPacIdPredicatesCount {
			get { return this.dicPacId.Count; }
		}
		public int AcceralatedTitlePredicatesCount {
			get { return this.dicTitle.Count; }
		}
		public int NonAcceralatedPredicatesCount {
			get { return this.listNoneAcc.Count; }
		}

		public ContentPredicate[] GetInvalidPredicates() {
			return base.items.FindAll(ContentPredicate.IsInvalidContentPredicate).ToArray();
		}
		public int RemoveInvalidPredicates() {
			int removeCnt = base.items.RemoveAll(ContentPredicate.IsInvalidContentPredicate);
			if (removeCnt > 0) {
				this.OnChanged();
			}
			return removeCnt;
		}
	}

}
