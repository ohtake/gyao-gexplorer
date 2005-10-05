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
		private const string regexVarient = @"([_A-Za-z]\w*)";
		private static readonly Regex argValidator =
			new Regex(@"^(?:[^{}]|\{" + UserCommand.regexVarient + @"\})*$");
		private static readonly Regex varientExtractor =
			new Regex(@"\{" + UserCommand.regexVarient + @"\}");
		
		private string title = null;
		private string fileName = null;
		private string arguments = null;
		
		public UserCommand(string title, string fileName, string arguments):this() {
			this.Title = title;
			this.FileName = fileName;
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
				if(string.IsNullOrEmpty(value)) throw new ArgumentException("空白の表示名は駄目．");
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
				if(string.IsNullOrEmpty(value)) throw new ArgumentException("空白のファイル名は駄目．");
				this.fileName = value;
			}
		}
		
		/// <summary>
		/// 外部プログラムを呼び出す時の引数．
		/// { と } で囲まれている箇所には選択中の<see cref="ContentAdapter"/>に応じて
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
		
		internal void Execute(IEnumerable<ContentAdapter> conts) {
			string args = this.arguments;
			while (true) {
				Match match = UserCommand.varientExtractor.Match(args);
				if (!match.Success) break;
				PropertyInfo pi = typeof(ContentAdapter).GetProperty(match.Groups[1].Value);
				StringBuilder sb = new StringBuilder();
				foreach (ContentAdapter cont in conts) {
					if (sb.Length > 0) {
						sb.Append(' ');
					}
					sb.Append(pi.GetValue(cont, null).ToString());
				}
				args = args.Replace(match.Value, sb.ToString());
			}
			Process.Start(this.fileName, args);
		}
		
		public int CompareTo(UserCommand other) {
			return this.Title.CompareTo(other.Title);
		}
		public override string ToString() {
			return this.Title;
		}
	}
	
	class UserCommandsManager : ItemsManagerBase<UserCommand>{
		private static UserCommandsManager instance = new UserCommandsManager();
		public static UserCommandsManager Instance {
			get {
				return UserCommandsManager.instance;
			}
		}
		
		public event EventHandler UserCommandsChanged;
		
		private UserCommandsManager() : base(){
		}
		
		override protected void OnChanged() {
			if(null != this.UserCommandsChanged) {
				this.UserCommandsChanged(this, EventArgs.Empty);
			}
		}
		
		public void Sort() {
			base.items.Sort();
			this.OnChanged();
		}
		protected override string FilenameForSerialization {
			get { return "UserCommands.xml"; }
		}
	}
}
