using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Yusen.GExplorer {
	/// <summary>2つの<see cref="string"/>を比較して真偽値を返すメソッドの名称．</summary>
	public enum TwoStringsPredicateMethod {
		Equals,
		Contains,
		StartsWith,
		EndsWith,
	}
	
	/// <summary>NGコンテンツ．<see cref="ContentAdapter"/>に対してNGの判定を行う．</summary>
	public class NgContent {
		private string comment;
		private string propertyName;
		private TwoStringsPredicateMethod method;
		private string word;
		private DateTime created;
		private DateTime lastAbone;
		
		public NgContent(){
		}
		internal NgContent(string comment, string propertyName, TwoStringsPredicateMethod method, string word) {
			this.comment = comment;
			this.propertyName = propertyName;
			this.method = method;
			this.word = word;
			this.created = DateTime.Now;
			this.lastAbone = DateTime.MinValue;
		}
		/// <summary>コメント．意味なし．</summary>
		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		/// <summary>NG処理で主語となる<see cref="ContentAdapter"/>のプロパティ名．</summary>
		public string PropertyName {
			get { return this.propertyName; }
			set { this.propertyName = value; }
		}
		/// <summary>NG処理で述語となる<see cref="string"/>のメソッド名．</summary>
		public TwoStringsPredicateMethod Method {
			get { return this.method; }
			set { this.method = value; }
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
		/// <summary>NGか否かを判定する．NGであった場合には<see cref="LastAbone"/>が更新される．</summary>
		/// <param name="p">判定対象の<see cref="GPackage"/>．</param>
		/// <returns>NGであったらtrueが返る．</returns>
		internal bool IsNgContent(ContentAdapter cont) {
			PropertyInfo pi = cont.GetType().GetProperty(this.propertyName);
			string propValue = pi.GetValue(cont, null).ToString();
			MethodInfo mi = typeof(string).GetMethod(this.method.ToString(), new Type[] {typeof(string)});
			if((bool)mi.Invoke(propValue, new object[] { this.word })) {
				this.lastAbone = DateTime.Now;
				return true;
			} else {
				return false;
			}
		}
	}
	
	class NgContentsManager : ItemsManagerBase<NgContent> {
		private static NgContentsManager instance = new NgContentsManager();
		public static NgContentsManager Instance {
			get {
				return NgContentsManager.instance;
			}
		}
		
		public event EventHandler NgContentsChanged;
		public event EventHandler LastAboneChanged;

		private NgContentsManager() : base() {
		}
		
		override protected void OnChanged() {
			if(null != this.NgContentsChanged) {
				this.NgContentsChanged(this, EventArgs.Empty);
			}
		}
		
		public bool IsNgContent(ContentAdapter cont) {
			foreach(NgContent nc in base.items) {
				if (nc.IsNgContent(cont)) {
					if(null != this.LastAboneChanged) {
						this.LastAboneChanged(this, EventArgs.Empty);
					}
					return true;
				}
			}
			return false;
		}
		
		protected override string FilenameForSerialization {
			get { return @"NgContents.xml"; }
		}
	}
}
