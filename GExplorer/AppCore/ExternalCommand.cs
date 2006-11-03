using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Reflection;
using Process = System.Diagnostics.Process;
using System.Text;
using System.Web;
using System.Xml.Serialization;
using Yusen.GExplorer.GyaoModel;
using System.IO;
using System.Collections;
using System.Threading;

namespace Yusen.GExplorer.AppCore {
	public class ExternalCommand{
		public static IEnumerable<string> GetEscapedLiterals() {
			return ExternalCommand.dicLiteralsEU.Keys;
		}
		public static string UnescapeLiteral(string escaped) {
			return ExternalCommand.dicLiteralsEU[escaped];
		}

		public static readonly string SeparatorTitle = "-";

		private const string strOpenBrace = "{";
		private const string strCloseBrace = "}";
		private const string strEscapedOpenBrace = "{{";
		private const string strEscapedCloseBrace = "}}";
		private const string reVarient = @"(?<PropName>[_A-Za-z]\w*)";
		private const string reCodePageName = @"(?<CodePageName>[-_A-Za-z0-9]+)";
		private static readonly Regex regexArgValidator;
		private static readonly Regex regexReplaceable;
		private static readonly Dictionary<string, string> dicLiteralsEU = new Dictionary<string, string>();

		static ExternalCommand() {
			string replaceable =
				@"\{" + ExternalCommand.reVarient + @"(?:\:" + ExternalCommand.reCodePageName + @")?\}"
				+ "|" + Regex.Escape(ExternalCommand.strEscapedOpenBrace)
				+ "|" + Regex.Escape(ExternalCommand.strEscapedCloseBrace);

			ExternalCommand.regexArgValidator = new Regex(@"^(?:[^{}]|" + replaceable + @")*$");
			ExternalCommand.regexReplaceable = new Regex(replaceable);

			ExternalCommand.dicLiteralsEU.Add(ExternalCommand.strEscapedOpenBrace, ExternalCommand.strOpenBrace);
			ExternalCommand.dicLiteralsEU.Add(ExternalCommand.strEscapedCloseBrace, ExternalCommand.strCloseBrace);
		}

		private sealed class ExternalCommandArgumentReplacer {
			private IEnumerable<GContentClass> conts;
			public ExternalCommandArgumentReplacer(IEnumerable<GContentClass> conts) {
				this.conts = conts;
			}
			public string ExpandPropertyValues(string arg) {
				return ExternalCommand.regexReplaceable.Replace(arg, this.EvaluateMatch);
			}
			private string EvaluateMatch(Match match) {
				if (ExternalCommand.dicLiteralsEU.ContainsKey(match.Value)) {
					return ExternalCommand.UnescapeLiteral(match.Value);
				} else {
					PropertyInfo pi = typeof(GContentClass).GetProperty(match.Groups["PropName"].Value);
					Encoding encoding = null;
					string codePageName = match.Groups["CodePageName"].Value;
					if (!string.IsNullOrEmpty(codePageName)) {
						encoding = Encoding.GetEncoding(codePageName);
					}
					
					StringBuilder sb = new StringBuilder();
					foreach (GContentClass cont in this.conts) {
						if (sb.Length > 0) {
							sb.Append(' ');
						}
						sb.Append(pi.GetValue(cont, null).ToString());
					}
					
					if (null == encoding) {
						return sb.ToString();
					} else {
						return HttpUtility.UrlEncode(sb.ToString(), encoding);
					}
				}
			}
		}

		private string title = null;
		private string filename = null;
		private string arguments = null;
		
		public ExternalCommand(string title, string fileName, string arguments)
			: this() {
			this.Title = title;
			this.Filename = fileName;
			this.Arguments = arguments;
		}
		public ExternalCommand() {
		}
		
		[XmlAttribute]
		public string Title {
			get { return this.title; }
			set { this.title = value; }
		}
		[XmlAttribute]
		public string Filename {
			get { return this.filename; }
			set { this.filename = value; }
		}
		[XmlAttribute]
		public string Arguments {
			get { return this.arguments; }
			set { this.arguments = value; }
		}
		
		[XmlIgnore]
		internal bool IsSeparator {
			get { return ExternalCommand.SeparatorTitle.Equals(this.Title); }
		}
		
		internal void Execute(IEnumerable<GContentClass> conts) {
			ExternalCommandArgumentReplacer replacer = new ExternalCommandArgumentReplacer(conts);
			string args = replacer.ExpandPropertyValues(this.Arguments);
			if (string.IsNullOrEmpty(this.Filename)) {
				Process.Start(args);
			} else {
				Process.Start(Environment.ExpandEnvironmentVariables(this.filename), args);
			}
		}
		
		public override string ToString() {
			return this.Title;
		}
	}
	
	sealed class ExternalCommandsManager : IEnumerable<ExternalCommand> {
		public event EventHandler ExternalCommandsManagerChanged;
		private List<ExternalCommand> commands;
		private int updateCount = 0;
		private bool updatedFlag = false;
		
		public ExternalCommandsManager() {
			this.commands = new List<ExternalCommand>();
		}
		
		#region シリアライズ・デシリアライズ
		public void Serialize(string filename) {
			XmlSerializer xs = new XmlSerializer(typeof(List<ExternalCommand>));
			using (StreamWriter sw = new StreamWriter(filename)) {
				xs.Serialize(sw, this.commands);
			}
		}
		public bool TryDeserialize(string filename) {
			FileInfo fi = new FileInfo(filename);
			if (!fi.Exists) return false;
			
			XmlSerializer xs = new XmlSerializer(typeof(List<ExternalCommand>));
			List<ExternalCommand> deserializedList = null;
			try {
				using (StreamReader sr = new StreamReader(filename)) {
					deserializedList = xs.Deserialize(sr) as List<ExternalCommand>;
				}
			} catch {
				return false;
			}
			if (null == deserializedList) return false;
			
			this.commands.AddRange(deserializedList);
			this.OnExternalCommandsManagerChanged();
			return true;
		}
		#endregion
		
		#region リスト操作
		public void Add(ExternalCommand ec) {
			this.commands.Add(ec);
			this.OnExternalCommandsManagerChanged();
		}
		public bool Remove(ExternalCommand ec) {
			if (this.commands.Remove(ec)) {
				this.OnExternalCommandsManagerChanged();
				return true;
			} else {
				return false;
			}
		}
		public void RemoveAt(int idx) {
			this.commands.RemoveAt(idx);
			this.OnExternalCommandsManagerChanged();
		}
		public void Insert(int index, ExternalCommand ec) {
			this.commands.Insert(index, ec);
			this.OnExternalCommandsManagerChanged();
		}
		public void Sort(Comparison<ExternalCommand> comparison) {
			this.commands.Sort(comparison);
			this.OnExternalCommandsManagerChanged();
		}
		public void Swap(int idx1, int idx2) {
			ExternalCommand temp = this.commands[idx1];
			this.commands[idx1] = this.commands[idx2];
			this.commands[idx2] = temp;
			this.OnExternalCommandsManagerChanged();
		}
		public void MoveToTop(ExternalCommand ec) {
			int idx = this.commands.IndexOf(ec);
			if (idx > 0) {
				this.commands.RemoveAt(idx);
				this.commands.Insert(0, ec);
				this.OnExternalCommandsManagerChanged();
			}
		}
		public void MoveUp(ExternalCommand ec) {
			int idx = this.commands.IndexOf(ec);
			if (idx > 0) {
				this.Swap(idx, idx - 1);
			}
		}
		public void MoveDown(ExternalCommand ec) {
			int idx = this.commands.IndexOf(ec);
			if (idx < this.commands.Count - 1) {
				this.Swap(idx, idx + 1);
			}
		}
		public void MoveToBottom(ExternalCommand ec) {
			int idx = this.commands.IndexOf(ec);
			if (idx < this.commands.Count - 1) {
				this.commands.RemoveAt(idx);
				this.commands.Add(ec);
				this.OnExternalCommandsManagerChanged();
			}
		}
		#endregion
		
		#region アップデート関係
		public void BeginUpdate() {
			Interlocked.Increment(ref this.updateCount);
		}
		public void EndUpdate() {
			if (!this.IsUpdating) throw new InvalidOperationException();
			if (0 == Interlocked.Decrement(ref this.updateCount)) {
				if (this.updatedFlag) {
					this.OnExternalCommandsManagerChanged();
				}
			}
		}
		public bool IsUpdating {
			get { return this.updateCount > 0; }
		}
		private void OnExternalCommandsManagerChanged() {
			if (this.IsUpdating) {
				this.updatedFlag = true;
				return;
			}
			this.updatedFlag = false;
			
			EventHandler handler = this.ExternalCommandsManagerChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		#endregion

		public ExternalCommand this[int index] {
			get { return this.commands[index]; }
		}

		#region IEnumerable<ExternalCommand> Members
		public IEnumerator<ExternalCommand> GetEnumerator() {
			return this.commands.GetEnumerator();
		}
		#endregion
		
		#region IEnumerable Members
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
		#endregion

	}
}
