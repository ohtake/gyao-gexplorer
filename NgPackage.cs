using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Yusen.GExplorer {
	/// <summary>2��<see cref="string"/>���r���Đ^�U�l��Ԃ����\�b�h�̖��́D</summary>
	public enum TwoStringsPredicateMethod {
		Equals,
		Contains,
		StartsWith,
		EndsWith,
	}
	
	/// <summary>NG�p�b�P�[�W�D<see cref="GPackage"/>�ɑ΂���NG�̔�����s���D</summary>
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
		/// <summary>�R�����g�D�Ӗ��Ȃ��D</summary>
		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		/// <summary>NG�����Ŏ��ƂȂ�<see cref="GPackage"/>�̃v���p�e�B���D</summary>
		public string PropertyName {
			get { return this.propertyName; }
			set { this.propertyName = value; }
		}
		/// <summary>NG�����ŏq��ƂȂ�<see cref="string"/>�̃��\�b�h���D</summary>
		public TwoStringsPredicateMethod Method {
			get { return this.method; }
			set { this.method = value; }
		}
		/// <summary>NG�����ŖړI��ƂȂ�NG���[�h�D</summary>
		public string Word {
			get { return this.word; }
			set { this.word = value; }
		}
		/// <summary>NG�p�b�P�[�W�̍쐬�����D</summary>
		public DateTime Created {
			get { return this.created; }
			set { this.created = value; }
		}
		/// <summary>�ŏING�����D</summary>
		public DateTime LastAbone {
			get { return this.lastAbone; }
			set { this.lastAbone = value; }
		}
		/// <summary>NG���ۂ��𔻒肷��DNG�ł������ꍇ�ɂ�<see cref="LastAbone"/>���X�V�����D</summary>
		/// <param name="p">����Ώۂ�<see cref="GPackage"/>�D</param>
		/// <returns>NG�ł�������true���Ԃ�D</returns>
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
