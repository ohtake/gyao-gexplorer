using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;
using Process = System.Diagnostics.Process;
using StringBuilder = System.Text.StringBuilder;

using System.IO;
using XmlSerializer = System.Xml.Serialization.XmlSerializer;
using System.Collections;

namespace Yusen.GExplorer {
	public class UserCommand : IComparable<UserCommand> {
		private const string regexVarient = @"(?<classPrefix>[cpg]):(?<property>[_A-Za-z]\w*)";
		private static readonly Regex argValidator =
			new Regex(@"^(?:[^{}]|\{" + UserCommand.regexVarient + @"\})*$", RegexOptions.Compiled | RegexOptions.Singleline);
		private static readonly Regex varientExtractor =
			new Regex(@"\{" + UserCommand.regexVarient + @"\}", RegexOptions.Compiled | RegexOptions.Singleline);
		
		private string title = null;
		private string fileName = null;
		private string arguments = null;
		
		public UserCommand(string title, string filename, string arguments) {
			this.Title = title;
			this.FileName = filename;
			this.Arguments = arguments;
		}
		public UserCommand() {
		}
		
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
				switch(match.Groups["classPrefix"].Value) {
					case "g":
						type = typeof(GGenre);
						break;
					case "p":
						type = typeof(GPackage);
						break;
					case "c":
						type = typeof(GContent);
						break;
					default:
						throw new Exception();
				}
				PropertyInfo pi = type.GetProperty(match.Groups["property"].Value);
				StringBuilder sb = new StringBuilder();
				foreach(GContent c in contents) {
					Object o = null;
					switch(match.Groups["classPrefix"].Value) {
						case "g":
							o = c.Genre;
							break;
						case "p":
							o = c.Package;
							break;
						case "c":
							o = c;
							break;
						default:
							throw new Exception();
					}
					try {
						sb.Append(pi.GetValue(o, null).ToString() + " ");//スペース区切り
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
	
	class UserCommandsManager : IEnumerable<UserCommand>{
		private const string fileName = @"UserCommands.xml";
		private static UserCommandsManager instance = new UserCommandsManager();
		public static UserCommandsManager Instance {
			get {
				return UserCommandsManager.instance;
			}
		}
		public static void SaveCommandsToFile() {
			TextWriter tw = null;
			try {
				XmlSerializer xs = new XmlSerializer(typeof(List<UserCommand>));
				tw = new StreamWriter(UserCommandsManager.fileName);
				xs.Serialize(tw, UserCommandsManager.Instance.commands);
			} catch(Exception e) {
				Utility.DisplayException(e);
			} finally {
				if(null != tw) {
					tw.Close();
				}
			}
		}
		public static void LoadCommandsFromFile() {
			TextReader tr = null;
			try {
				XmlSerializer xs = new XmlSerializer(typeof(List<UserCommand>));
				tr = new StreamReader(UserCommandsManager.fileName);
				UserCommandsManager.Instance.commands = (List<UserCommand>)xs.Deserialize(tr);
			} catch(FileNotFoundException) {
				return;
			} catch(Exception e) {
				Utility.DisplayException(e);
			} finally {
				if(null != tr) {
					tr.Close();
				}
				UserCommandsManager.Instance.OnUserCommandsChanged();
			}
		}
		
		public event UserCommandsChangedEventHandler UserCommandsChanged;
		private List<UserCommand> commands = new List<UserCommand>();
		private UserCommandsEditor editor = null;
		
		private UserCommandsManager() {
		}
		
		private void OnUserCommandsChanged() {
			if(null != this.UserCommandsChanged) {
				this.UserCommandsChanged();
			}
		}
		
		public UserCommand this[int idx] {
			get {
				return this.commands[idx];
			}
			set {
				this.commands[idx] = value;
				this.OnUserCommandsChanged();
			}
		}
		public int Count {
			get {
				return this.commands.Count;
			}
		}
		public IEnumerator<UserCommand> GetEnumerator() {
			return this.commands.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
		
		public void RemoveAt(int idx) {
			this.commands.RemoveAt(idx);
			this.OnUserCommandsChanged();
		}
		public void Add(UserCommand command) {
			this.commands.Add(command);
			this.OnUserCommandsChanged();
		}
		public void Insert(int idx, UserCommand command) {
			this.commands.Insert(idx, command);
			this.OnUserCommandsChanged();
		}
		public void Swap(int x, int y) {
			UserCommand cx = this[x];
			this[x] = this[y];
			this[y] = cx;
			this.OnUserCommandsChanged();
		}
		public void Sort() {
			this.commands.Sort();
			this.OnUserCommandsChanged();
		}

		public void ShowEditor() {
			if(null == this.editor || !this.editor.Visible) {
				this.editor = new UserCommandsEditor();
				this.editor.Show();
			} else {
				this.editor.Focus();
			}
		}
	}
}
