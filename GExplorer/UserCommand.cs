using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;
using Process = System.Diagnostics.Process;
using StringBuilder = System.Text.StringBuilder;

namespace Yusen.GExplorer {
	/// <summary>�O���R�}���h�D</summary>
	/// <remarks>
	/// ���[�U���Ǝ��̃R�}���h���`���邱�Ƃ�
	/// �ȒP�ɃA�v���P�[�V�����̊g�����s����悤�ɂ���D
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
		/// �O���R�}���h�̖��́D
		/// ���j���[�̍��ږ��ɂȂ�̂� &amp; �ŃA�N�Z�X�L�[�̐ݒ肪�ł���D
		/// </summary>
		public string Title {
			get {
				return this.title;
			}
			set {
				if(null != this.title) throw new InvalidOperationException();
				if(string.IsNullOrEmpty(value)) throw new ArgumentException("�󔒂̕\�����͑ʖځD");
				this.title = value;
			}
		}
		
		/// <summary>
		/// �O���R�}���h�����s����ۂ̎��s�t�@�C�����D
		/// </summary>
		public string FileName{
			get {
				return this.fileName;
			}
			set {
				if(null != this.fileName) throw new InvalidOperationException();
				if(string.IsNullOrEmpty(value)) throw new ArgumentException("�󔒂̃t�@�C�����͑ʖځD");
				this.fileName = value;
			}
		}
		
		/// <summary>
		/// �O���v���O�������Ăяo�����̈����D
		/// { �� } �ň͂܂�Ă���ӏ��ɂ͑I�𒆂�<see cref="ContentAdapter"/>�ɉ�����
		/// �ʂ̕�����ɒu�������D
		/// �u���̖@���ɂ��Ă� ReadMe.txt �����Q�Ƃ̂��ƁD
		/// </summary>
		public string Arguments {
			get {
				return this.arguments;
			}
			set {
				if(null != this.arguments) throw new InvalidOperationException();
				if(null == value) throw new ArgumentNullException();
				if (!UserCommand.regexArgValidator.Match(value).Success) throw new ArgumentException("�����̏������Ԉ���Ă�D");
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
