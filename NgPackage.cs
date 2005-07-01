using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Yusen.GExplorer {
	public enum TwoStringsPredicateMethod {
		Equals,
		Contains,
		StartsWith,
		EndsWith,
	}
	
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
		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		public string PropertyName {
			get { return this.propertyName; }
			set { this.propertyName = value; }
		}
		public TwoStringsPredicateMethod Method {
			get { return this.method; }
			set { this.method = value; }
		}
		public string Word {
			get { return this.word; }
			set { this.word = value; }
		}
		public DateTime Created {
			get { return this.created; }
			set { this.created = value; }
		}
		public DateTime LastAbone {
			get { return this.lastAbone; }
			set { this.lastAbone = value; }
		}
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
	
	delegate void NgPackagesChangedEventHandler();
	delegate void NgPackagesLastAboneChangedEventHandler();
	
	class NgPackagesManager : ItemsManagerBase<NgPackage> {
		private static NgPackagesManager instance = new NgPackagesManager();
		public static NgPackagesManager Instance {
			get {
				return NgPackagesManager.instance;
			}
		}
		
		public event NgPackagesChangedEventHandler NgPackagesChanged;
		public event NgPackagesLastAboneChangedEventHandler LastAboneChanged;
		
		private NgPackagesManager() : base() {
		}
		
		override protected void OnChanged() {
			if(null != this.NgPackagesChanged) {
				this.NgPackagesChanged();
			}
		}
		
		override protected string XmlFileName{
			get { return @"NgPackages.xml";}
		}
		
		public bool IsNgPackage(GPackage p) {
			foreach(NgPackage np in base.items) {
				if(np.IsNgPackage(p)) {
					if(null != this.LastAboneChanged) {
						this.LastAboneChanged();
					}
					return true;
				}
			}
			return false;
		}
	}
}
