using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;
using Process = System.Diagnostics.Process;
using StringBuilder = System.Text.StringBuilder;

namespace Yusen.GExplorer {
	/// <summary>外部コマンド．</summary>
	/// <remarks>
	/// ユーザが独自のコマンドを定義することで
	/// 簡単にアプリケーションの拡張を行えるようにする．
	/// </remarks>
	public class UserCommand : IComparable<UserCommand> {
		private const string regexVarient = @"(?<classPrefix>[cpg]):(?<property>[_A-Za-z]\w*)";
		private static readonly Regex argValidator =
			new Regex(@"^(?:[^{}]|\{" + UserCommand.regexVarient + @"\})*$", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex varientExtractor =
			new Regex(@"\{" + UserCommand.regexVarient + @"\}", RegexOptions.Compiled | RegexOptions.Singleline);
		
		private string title = null;
		private string fileName = null;
		private string arguments = null;
		
		public UserCommand(string title, string filename, string arguments):this() {
			this.Title = title;
			this.FileName = filename;
			this.Arguments = arguments;
		}
		public UserCommand() {
		}
		
		/// <summary>
		/// 外部コマンドの名称．
		/// メニューの項目名になるので &amp; でアクセスキーの設定ができる．
		/// </summary>
		public string Title {
			get {
				return this.title;
			}
			set {
				if(null != this.title) throw new InvalidOperationException();
				if(null == value) throw new ArgumentNullException();
				if("" == value) throw new ArgumentException("空白の表示名は駄目．");
				this.title = value;
			}
		}
		
		/// <summary>
		/// 外部コマンドを実行する際の実行ファイル名．
		/// </summary>
		public string FileName{
			get {
				return this.fileName;
			}
			set {
				if(null != this.fileName) throw new InvalidOperationException();
				if(null == value) throw new ArgumentNullException();
				if("" == value) throw new ArgumentException("空白のファイル名は駄目．");
				this.fileName = value;
			}
		}
		
		/// <summary>
		/// 外部プログラムを呼び出す時の引数．
		/// { と } で囲まれている箇所には選択中の<see cref="GContent"/>に応じて
		/// 別の文字列に置換される．
		/// 置換の法則については ReadMe.txt 等を参照のこと．
		/// </summary>
		public string Arguments {
			get {
				return this.arguments;
			}
			set {
				if(null != this.arguments) throw new InvalidOperationException();
				if(null == value) throw new ArgumentNullException();
				if(!UserCommand.argValidator.Match(value).Success) throw new ArgumentException("引数の書式が間違ってる．");
				this.arguments = value;
			}
		}
		
		internal void Execute(IEnumerable<GContent> contents) {
			string args = this.arguments;
			while(true) {
				Match match = UserCommand.varientExtractor.Match(args);
				if(!match.Success) break;
				Type type = null;
				GContent[] contArray = new List<GContent>(contents).ToArray();
				Object[] gArray = null;
				switch(match.Groups["classPrefix"].Value) {
					case "g":
						type = typeof(GGenre);
						gArray = Array.ConvertAll<GContent, GGenre>(
							contArray,
							new Converter<GContent, GGenre>(delegate(GContent input) {
								return input.Genre;
							}));
						break;
					case "p":
						type = typeof(GPackage);
						gArray = Array.ConvertAll<GContent, GPackage>(
							contArray,
							new Converter<GContent, GPackage>(delegate(GContent input) {
								return input.Package;
							}));
						break;
					case "c":
						type = typeof(GContent);
						gArray = contArray;
						break;
					default:
						throw new Exception();
				}
				PropertyInfo pi = type.GetProperty(match.Groups["property"].Value);
				StringBuilder sb = new StringBuilder();
				foreach(object g in gArray) {
					try {
						sb.Append(pi.GetValue(g, null).ToString() + " ");//スペース区切り
					} catch(Exception e) {
						Utility.DisplayException(e);
						return;
					}
				}
				if(sb.Length > 0) sb.Remove(sb.Length - 1, 1); //末尾のスペース削除
				args = args.Replace(match.Value, sb.ToString()); //変数置換
			}
			try {
				Process.Start(this.fileName, args);
			}catch(Exception e){
				Utility.DisplayException(e);
			}
		}
		
		public int CompareTo(UserCommand other) {
			return this.Title.CompareTo(other.Title);
		}
		public override string ToString() {
			return this.Title;
		}
	}
	
	delegate void UserCommandsChangedEventHandler();
	
	class UserCommandsManager : ItemsManagerBase<UserCommand>{
		private static UserCommandsManager instance = new UserCommandsManager();
		public static UserCommandsManager Instance {
			get {
				return UserCommandsManager.instance;
			}
		}
		
		public event UserCommandsChangedEventHandler UserCommandsChanged;
		
		private UserCommandsManager() : base(){
		}
		
		override protected void OnChanged() {
			if(null != this.UserCommandsChanged) {
				this.UserCommandsChanged();
			}
		}
		
		public void Sort() {
			base.items.Sort();
			this.OnChanged();
		}
		
		override protected string XmlFileName{
			get { return @"UserCommands.xml";}
		}
	}
}
