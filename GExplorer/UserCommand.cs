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
		public static IEnumerable<string> GetEscapedLiterals() {
			return UserCommand.dicLiteralsEU.Keys;
		}
		public static string UnescapeLiteral(string escaped) {
			return UserCommand.dicLiteralsEU[escaped];
		}
		
		private const string strOpenBrace = "{";
		private const string strCloseBrace = "}";
		private const string strEscapedOpenBrace = "{{";
		private const string strEscapedCloseBrace = "}}";
		private const string reVarient = @"(?<PropName>[_A-Za-z]\w*)";
		private static readonly Regex regexArgValidator;
		private static readonly Regex regexReplaceable;
		private static readonly Dictionary<string, string> dicLiteralsEU = new Dictionary<string,string>();
		
		static UserCommand(){
			string replaceable = @"\{" + UserCommand.reVarient + @"\}|" + Regex.Escape(UserCommand.strEscapedOpenBrace) + "|" + Regex.Escape(UserCommand.strEscapedCloseBrace);
			UserCommand.regexArgValidator = new Regex(@"^(?:[^{}]|" + replaceable + @")*$");
			UserCommand.regexReplaceable = new Regex(replaceable);
			
			UserCommand.dicLiteralsEU.Add(UserCommand.strEscapedOpenBrace, UserCommand.strOpenBrace);
			UserCommand.dicLiteralsEU.Add(UserCommand.strEscapedCloseBrace, UserCommand.strCloseBrace);
		}

		private sealed class UserCommandArgumentReplacer {
			private IEnumerable<ContentAdapter> conts;
			public UserCommandArgumentReplacer(IEnumerable<ContentAdapter> conts) {
				this.conts = conts;
			}
			public string ExpandPropertyValues(string arg) {
				return UserCommand.regexReplaceable.Replace(arg, this.EvaluateMatch);
			}
			private string EvaluateMatch(Match match) {
				if (UserCommand.dicLiteralsEU.ContainsKey(match.Value)) {
					return UserCommand.UnescapeLiteral(match.Value);
				} else {
					PropertyInfo pi = typeof(ContentAdapter).GetProperty(match.Groups["PropName"].Value);
					StringBuilder sb = new StringBuilder();
					foreach (ContentAdapter cont in this.conts) {
						if (sb.Length > 0) {
							sb.Append(' ');
						}
						sb.Append(pi.GetValue(cont, null).ToString());
					}
					return sb.ToString();
				}
			}
		}

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
				if (!UserCommand.regexArgValidator.Match(value).Success) throw new ArgumentException("引数の書式が間違ってる．");
				this.arguments = value;
			}
		}
		
		internal void Execute(IEnumerable<ContentAdapter> conts) {
			UserCommandArgumentReplacer replacer = new UserCommandArgumentReplacer(conts);
			string args = replacer.ExpandPropertyValues(this.Arguments);
			Process.Start(Environment.ExpandEnvironmentVariables(this.fileName), args);
		}
		
		public int CompareTo(UserCommand other) {
			return this.Title.CompareTo(other.Title);
		}
		public override string ToString() {
			return this.Title;
		}
	}
	
	sealed class UserCommandsManager : ItemsManagerBase<UserCommand>{
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

	public sealed class UserCommandSelectedEventArgs : EventArgs {
		private UserCommand command;
		public UserCommandSelectedEventArgs(UserCommand command) {
			this.command = command;
		}
		public UserCommand UserCommand {
			get { return this.command; }
		}
	}

}
