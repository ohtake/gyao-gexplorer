using System;

namespace Yusen.GExplorer {
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
	public class SubOptionsAttribute : Attribute{
		private readonly string name;
		private readonly string description;
		private SubOptionsAttribute()
			: base() {
		}
		public SubOptionsAttribute(string name)
			: this(name, string.Empty) {
		}
		public SubOptionsAttribute(string name, string desc)
			: this() {
			this.name = name;
			this.description = desc;
		}
		public string Name {
			get { return this.name; }
		}
		public string Description {
			get { return this.description; }
		}
	}
}
