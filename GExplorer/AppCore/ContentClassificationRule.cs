using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using System.Threading;
using System.IO;

namespace Yusen.GExplorer.AppCore {
	public sealed class ContentClassificationRule {
		private string comment;
		private string subject;
		private ContentClassificationPredicate predicate;
		private string @object;
		private string destination;
		private DateTime created;
		private DateTime lastApplied;

		public string Comment {
			get { return this.comment; }
			set { this.comment = value; }
		}
		public string Subject {
			get { return this.subject; }
			set { this.subject = value; }
		}
		public ContentClassificationPredicate Predicate {
			get { return this.predicate; }
			set { this.predicate = value; }
		}
		public string Object {
			get { return this.@object; }
			set { this.@object = value; }
		}
		public string Destination {
			get { return this.destination; }
			set { this.destination = value; }
		}
		public DateTime Created {
			get { return this.created; }
			set { this.created = value; }
		}
		public DateTime LastApplied {
			get { return this.lastApplied; }
			set { this.lastApplied = value; }
		}
	}

	public enum ContentClassificationPredicate {
		StringEquals,
		StringContains,
		StringStartsWith,
		StringEndsWith,
	}

	sealed class ContentClassificationRulesManager {
		public event EventHandler ContentCllasificationRulesManagerChanged;
		
		private List<ContentClassificationRule> rules;

		private int updateCount = 0;
		private bool updatedFlag = false;

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

			EventHandler handler = this.ContentCllasificationRulesManagerChanged;
			if (null != handler) {
				handler(this, EventArgs.Empty);
			}
		}
		public bool IsUpdating {
			get { return this.updateCount > 0; }
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
	}
}
