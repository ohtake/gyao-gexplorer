﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using System.Threading;
using System.IO;
using Yusen.GExplorer.GyaoModel;
using System.Collections;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer.AppCore {
	public sealed class ContentClassificationRule {
		public const string SubjectTitle = "Title";
		
		private string comment;
		private string subject;
		private StringPredicate predicate;
		private string @object;
		private string destination;
		private DateTime created;
		private DateTime lastApplied;

		public ContentClassificationRule() {
		}
		public ContentClassificationRule(string comment, string subject, StringPredicate predicate, string @object, string destination) {
			this.comment = comment;
			this.subject = subject;
			this.predicate = predicate;
			this.@object = @object;
			this.destination = destination;
			this.created = DateTime.Now;
			this.lastApplied = DateTime.MinValue;
		}

		[XmlAttribute]
		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		[XmlAttribute]
		public string Subject {
			get { return this.subject; }
			set { this.subject = value; }
		}
		[XmlAttribute]
		public StringPredicate Predicate {
			get { return this.predicate; }
			set { this.predicate = value; }
		}
		[XmlAttribute]
		public string Object {
			get { return this.@object; }
			set { this.@object = value; }
		}
		[XmlAttribute]
		public string Destination {
			get { return this.destination; }
			set { this.destination = value; }
		}
		[XmlAttribute]
		public DateTime Created {
			get { return this.created; }
			set { this.created = value; }
		}
		[XmlAttribute]
		public DateTime LastApplied {
			get { return this.lastApplied; }
			set { this.lastApplied = value; }
		}
	}
	
	public enum StringPredicate {
		StringEquals,
		StringContains,
		StringStartsWith,
		StringEndsWith,
		RegexMatch,
	}

	sealed class ContentClassificationRulesManager : IEnumerable<ContentClassificationRule> {
		public event EventHandler ContentCllasificationRulesManagerChanged;
		public event EventHandler LastAppliedTimeChanged;
		
		private List<ContentClassificationRule> rules;
		private SortedDictionary<string, PropertyInfo> pInfos = new SortedDictionary<string, PropertyInfo>();
		
		private int updateCount = 0;
		private bool updatedFlag = false;

		private Dictionary<string, List<ContentClassificationRule>> accEqTitles = new Dictionary<string, List<ContentClassificationRule>>();
		private List<ContentClassificationRule> accMiscRules = new List<ContentClassificationRule>();
		
		public ContentClassificationRulesManager() {
			this.rules = new List<ContentClassificationRule>();
		}

		public void BeginUpdate() {
			Interlocked.Increment(ref this.updateCount);
		}
		public void EndUpdate() {
			if (!this.IsUpdating) throw new InvalidOperationException();
			if (0 == Interlocked.Decrement(ref this.updateCount)) {
				if (this.updatedFlag) {
					this.OnContentCllasificationRulesManagerChanged();
				}
			}
		}
		private void OnContentCllasificationRulesManagerChanged() {
			if (this.IsUpdating) {
				this.updatedFlag = true;
				return;
			}
			this.updatedFlag = false;

			this.CreateAccelarationStructures();

			EventHandler handler = this.ContentCllasificationRulesManagerChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		private void OnLastAppliedTimeChanged() {
			EventHandler handler = this.LastAppliedTimeChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		public bool IsUpdating {
			get { return this.updateCount > 0; }
		}

		private void CreateAccelarationStructures() {
			this.accEqTitles.Clear();
			this.accMiscRules.Clear();
			
			foreach (ContentClassificationRule rule in this.rules) {
				if (rule.Subject.Equals(ContentClassificationRule.SubjectTitle) && rule.Predicate == StringPredicate.StringEquals) {
					List<ContentClassificationRule> list;
					if (this.accEqTitles.TryGetValue(rule.Object, out list)) {
						list.Add(rule);
					} else {
						list = new List<ContentClassificationRule>();
						list.Add(rule);
						this.accEqTitles.Add(rule.Object, list);
					}
				} else {
					this.accMiscRules.Add(rule);
				}
			}
		}

		#region シリアライズ・デシリアライズ
		public void Serialize(string filename) {
			XmlSerializer xs = new XmlSerializer(typeof(List<ContentClassificationRule>));
			using (StreamWriter sw = new StreamWriter(filename)) {
				xs.Serialize(sw, this.rules);
			}
		}
		public bool TryDeserialize(string filename) {
			FileInfo fi = new FileInfo(filename);
			if (!fi.Exists) return false;

			XmlSerializer xs = new XmlSerializer(typeof(List<ContentClassificationRule>));
			List<ContentClassificationRule> deserializedList = null;
			try {
				using (StreamReader sr = new StreamReader(filename)) {
					deserializedList = xs.Deserialize(sr) as List<ContentClassificationRule>;
				}
			} catch {
				return false;
			}
			if (null == deserializedList) return false;
			
			this.rules.AddRange(deserializedList);
			this.OnContentCllasificationRulesManagerChanged();
			return true;
		}
		#endregion

		public void Add(ContentClassificationRule rule) {
			this.rules.Add(rule);
			this.OnContentCllasificationRulesManagerChanged();
		}
		public bool Remove(ContentClassificationRule rule) {
			if (this.rules.Remove(rule)) {
				this.OnContentCllasificationRulesManagerChanged();
				return true;
			} else {
				return false;
			}
		}
		public void Sort(IComparer<ContentClassificationRule> comparer) {
			this.rules.Sort(comparer);
			this.OnContentCllasificationRulesManagerChanged();
		}

		public string[] GetDestinationsFor(GContentClass cont) {
			ContentClassificationRule[] appliableRules = this.GetAppliableRulesFor(cont);
			string[] destinations = new string[appliableRules.Length];
			for (int i = 0; i < appliableRules.Length; i++) {
				destinations[i] = appliableRules[i].Destination;
			}
			return destinations;
		}
		public ContentClassificationRule[] GetAppliableRulesFor(GContentClass cont) {
			List<ContentClassificationRule> rules = new List<ContentClassificationRule>();
			List<ContentClassificationRule> accList;
			if (this.accEqTitles.TryGetValue(cont.Title, out accList)) {
				rules.AddRange(accList);
				DateTime now = DateTime.Now;
				foreach (ContentClassificationRule rule in accList) {
					rule.LastApplied = now;
				}
			}
			foreach (ContentClassificationRule rule in this.accMiscRules) {
				PropertyInfo pi;
				if (!this.pInfos.TryGetValue(rule.Subject, out pi)) {
					pi = typeof(GContentClass).GetProperty(rule.Subject);
					if (null == pi) {
						throw new InvalidOperationException(string.Format("プロパティ {0} は存在しないっぽい．", rule.Subject));
					}
					this.pInfos.Add(rule.Subject, pi);
				}
				object sbjValueObject = pi.GetValue(cont, null);
				string sbjValue = (sbjValueObject == null) ? string.Empty : sbjValueObject.ToString();
				bool flag;
				switch (rule.Predicate) {
					case StringPredicate.StringEquals:
						flag = sbjValue.Equals(rule.Object);
						break;
					case StringPredicate.StringContains:
						flag = sbjValue.Contains(rule.Object);
						break;
					case StringPredicate.StringStartsWith:
						flag = sbjValue.StartsWith(rule.Object);
						break;
					case StringPredicate.StringEndsWith:
						flag= sbjValue.EndsWith(rule.Object);
						break;
					case StringPredicate.RegexMatch:
						flag = Regex.Match(sbjValue, rule.Object).Success;
						break;
					default:
						throw new InvalidOperationException();
				}
				if (flag) {
					rules.Add(rule);
					rule.LastApplied = DateTime.Now;
				}
			}
			if (rules.Count > 0) {
				this.OnLastAppliedTimeChanged();
			}
			return rules.ToArray();
		}

		public IEnumerator<ContentClassificationRule> GetEnumerator() {
			return this.rules.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
	}
}
