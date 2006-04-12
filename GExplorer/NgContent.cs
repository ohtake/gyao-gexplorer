using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;

namespace Yusen.GExplorer {
	/// <summary>NGコンテンツ．<see cref="ContentAdapter"/>に対してNGの判定を行う．</summary>
	public class NgContent {
		public static readonly string MethodNameEquals = "Equals";
		public static readonly string MethodNameContains = "Contains";
		public static readonly string MethodNameStartsWith = "StartsWith";
		public static readonly string MethodNameEndsWith = "EndsWith";
		public static readonly string[] Methods = new string[] { NgContent.MethodNameEquals, NgContent.MethodNameContains, NgContent.MethodNameStartsWith, NgContent.MethodNameEndsWith};
		
		internal static bool IsInvalidNgContent(NgContent ngc) {
			return null == ngc.subjInfo || null == ngc.predInfo;
		}

		private string comment;
		private string propertyName;
		private string method;
		private string word;
		private DateTime created;
		private DateTime lastAbone;

		private PropertyInfo subjInfo;
		private MethodInfo predInfo;
		
		public NgContent(){
		}
		internal NgContent(string comment, string propertyName, string method, string word) {
			this.Comment = comment;
			this.PropertyName = propertyName;
			this.Method = method;
			this.Word = word;
			this.Created = DateTime.Now;
			this.LastAbone = DateTime.MinValue;
			
			if (null == this.subjInfo) {
				throw new ArgumentException("存在しないプロパティ名: " + this.propertyName);
			}
			if (null == this.predInfo) {
				throw new ArgumentException("存在しないメソッド名: " + this.method);
			}
		}
		/// <summary>コメント．意味なし．</summary>
		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		/// <summary>NG処理で主語となる<see cref="ContentAdapter"/>のプロパティ名．</summary>
		public string PropertyName {
			get { return this.propertyName; }
			set {
				this.propertyName = value;
				this.subjInfo = typeof(ContentAdapter).GetProperty(value);
			}
		}
		/// <summary>NG処理で述語となる<see cref="string"/>のメソッド名．</summary>
		public string Method {
			get { return this.method; }
			set {
				this.method = value;
				this.predInfo = typeof(string).GetMethod(value, new Type[] { typeof(string)});
			}
		}
		/// <summary>NG処理で目的語となるNGワード．</summary>
		public string Word {
			get { return this.word; }
			set { this.word = value; }
		}
		/// <summary>NGコンテンツの作成日時．</summary>
		public DateTime Created {
			get { return this.created; }
			set { this.created = value; }
		}
		/// <summary>最終NG日時．</summary>
		public DateTime LastAbone {
			get { return this.lastAbone; }
			set { this.lastAbone = value; }
		}
		/// <summary>NGか否かを判定する．</summary>
		/// <param name="p">判定対象の<see cref="GPackage"/>．</param>
		/// <returns>NGであったらtrueが返る．</returns>
		internal bool IsNgContent(ContentAdapter cont) {
			string propValue = this.subjInfo.GetValue(cont, null).ToString();
			return (bool)this.predInfo.Invoke(propValue, new object[] { this.word });
		}
	}

	internal sealed class NgContentsManager : ItemsManagerBase<NgContent> {
		private static NgContentsManager instance = new NgContentsManager();
		public static NgContentsManager Instance {
			get {
				return NgContentsManager.instance;
			}
		}

		public event EventHandler NgContentsChanged;
		public event EventHandler LastAboneChanged;

		private SortedDictionary<string, NgContent> dicNgId = new SortedDictionary<string, NgContent>();
		private SortedDictionary<string, NgContent> dicNgTitle = new SortedDictionary<string, NgContent>();
		private List<NgContent> listNoneAccelarated = new List<NgContent>();


		private NgContentsManager()
			: base() {
		}

		private void RecreateAccelarationList() {
			this.dicNgId.Clear();
			this.dicNgTitle.Clear();
			this.listNoneAccelarated.Clear();

			foreach (NgContent ngc in base.items) {
				if (NgContent.MethodNameEquals.Equals(ngc.Method)) {
					switch (ngc.PropertyName) {
						case ContentAdapter.PropertyNameContentId:
							this.dicNgId[ngc.Word] = ngc;
							continue;
						case ContentAdapter.PropertyNameTitle:
							this.dicNgTitle[ngc.Word] = ngc;
							continue;
					}
				}
				this.listNoneAccelarated.Add(ngc);
			}
		}

		override protected void OnChanged() {
			this.RecreateAccelarationList();
			if (null != this.NgContentsChanged) {
				this.NgContentsChanged(this, EventArgs.Empty);
			}
		}

		public bool IsNgContent(ContentAdapter cont) {
			NgContent ngc;
			if (this.dicNgId.TryGetValue(cont.ContentId, out ngc)) {
				ngc.LastAbone = DateTime.Now;
				if (null != this.LastAboneChanged) {
					this.LastAboneChanged(this, EventArgs.Empty);
				}
				return true;
			}
			if (this.dicNgTitle.TryGetValue(cont.Title, out ngc)) {
				ngc.LastAbone = DateTime.Now;
				if (null != this.LastAboneChanged) {
					this.LastAboneChanged(this, EventArgs.Empty);
				}
				return true;
			}
			foreach (NgContent nc in this.listNoneAccelarated) {
				if (nc.IsNgContent(cont)) {
					nc.LastAbone = DateTime.Now;
					if (null != this.LastAboneChanged) {
						this.LastAboneChanged(this, EventArgs.Empty);
					}
					return true;
				}
			}
			return false;
		}

		public NgContent[] EnumerateNgsTo(ContentAdapter cont) {
			List<NgContent> ngs = new List<NgContent>();
			NgContent ngc;
			if (this.dicNgId.TryGetValue(cont.ContentId, out ngc)) {
				ngs.Add(ngc);
			}
			if (this.dicNgTitle.TryGetValue(cont.Title, out ngc)) {
				ngs.Add(ngc);
			}
			foreach (NgContent nc in this.listNoneAccelarated) {
				if (nc.IsNgContent(cont)) {
					ngs.Add(nc);
				}
			}
			return ngs.ToArray();
		}
		protected override string FilenameForSerialization {
			get { return @"NgContents.xml"; }
		}
		
		public int AcceralatedNgIdsCount {
			get { return this.dicNgId.Count; }
		}
		public int AcceralatedNgTitlesCount {
			get { return this.dicNgTitle.Count; }
		}
		public int NonAcceralatedNgContentsCount {
			get { return this.listNoneAccelarated.Count; }
		}
		
		public NgContent[] GetInvalidNgContents() {
			return base.items.FindAll(NgContent.IsInvalidNgContent).ToArray();
		}
		public int RemoveInvalidNgContents() {
			int removeCnt = base.items.RemoveAll(NgContent.IsInvalidNgContent);
			if (removeCnt > 0) {
				this.OnChanged();
			}
			return removeCnt;
		}
	}
}
