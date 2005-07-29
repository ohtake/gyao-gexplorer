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
	
	/// <summary>NGパッケージ．<see cref="GPackage"/>に対してNGの判定を行う．</summary>
	public class NgPackage {
		private string comment;
		private string propertyName;
		private TwoStringsPredicateMethod method;
		private string word;
		private DateTime created;
		private DateTime lastAbone;
		
		public NgPackage(){
		}
		internal NgPackage(string comment, string propertyName, TwoStringsPredicateMethod method, string word) {
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
		/// <summary>NG処理で主語となる<see cref="GPackage"/>のプロパティ名．</summary>
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
		/// <summary>NGパッケージの作成日時．</summary>
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
		internal bool IsNgPackage(GPackage p) {
			try {
				PropertyInfo pi = typeof(GPackage).GetProperty(this.propertyName);
				string propValue = pi.GetValue(p, null).ToString();
				MethodInfo mi = typeof(string).GetMethod(this.method.ToString(), new Type[] {typeof(string)});
				if((bool)mi.Invoke(propValue, new object[] { this.word })) {
					this.lastAbone = DateTime.Now;
					return true;
				} else {
					return false;
				}
			} catch(Exception e) {
				Utility.DisplayException(e);
				return false;
			}
		}
	}
	
	//delegate void NgPackagesChangedEventHandler();
	//delegate void NgPackagesLastAboneChangedEventHandler();
	
	class NgPackagesManager : ItemsManagerBase<NgPackage> {
		private static NgPackagesManager instance = new NgPackagesManager();
		public static NgPackagesManager Instance {
			get {
				return NgPackagesManager.instance;
			}
		}
		
		public event EventHandler NgPackagesChanged;
		public event EventHandler LastAboneChanged;
		
		private NgPackagesManager() : base() {
		}
		
		override protected void OnChanged() {
			if(null != this.NgPackagesChanged) {
				this.NgPackagesChanged(this, EventArgs.Empty);
			}
		}
		
		override protected string XmlFileName{
			get { return @"NgPackages.xml";}
		}
		
		public bool IsNgPackage(GPackage p) {
			foreach(NgPackage np in base.items) {
				if(np.IsNgPackage(p)) {
					if(null != this.LastAboneChanged) {
						this.LastAboneChanged(this, EventArgs.Empty);
					}
					return true;
				}
			}
			return false;
		}
	}
}
