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
		/// �O���R�}���h�̖��́D
		/// ���j���[�̍��ږ��ɂȂ�̂� &amp; �ŃA�N�Z�X�L�[�̐ݒ肪�ł���D
		/// </summary>
		public string Title {
			get {
				return this.title;
			}
			set {
				if(null != this.title) throw new InvalidOperationException();
				if(null == value) throw new ArgumentNullException();
				if("" == value) throw new ArgumentException("�󔒂̕\�����͑ʖځD");
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
				if(null == value) throw new ArgumentNullException();
				if("" == value) throw new ArgumentException("�󔒂̃t�@�C�����͑ʖځD");
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
				if(!UserCommand.argValidator.Match(value).Success) throw new ArgumentException("�����̏������Ԉ���Ă�D");
				this.arguments = value;
			}
		}
		
		internal void Execute(ContentAdapter cont) {
			string args = this.arguments;
			while(true) {
				Match match = UserCommand.varientExtractor.Match(args);
				if(!match.Success) break;
				PropertyInfo pi = cont.GetType().GetProperty(match.Groups[1].Value);
				string propValue = pi.GetValue(cont, null).ToString();
				args = args.Replace(match.Value, propValue);
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
