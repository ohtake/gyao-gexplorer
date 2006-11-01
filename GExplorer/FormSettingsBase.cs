using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Xml.Serialization;
using Yusen.GExplorer.UserInterfaces;
using Yusen.GExplorer.OldApp;

namespace Yusen.GExplorer {
	public partial class FormSettingsBase : BaseForm {
		public FormSettingsBase() : base() {
			InitializeComponent();
		}
	}
	
	[ReadOnly(true)]
	public class FormSettingsBaseSettings : INewSettings<FormSettingsBaseSettings> {
		private readonly FormSettingsBase owner;

		public FormSettingsBaseSettings() {
		}
		internal FormSettingsBaseSettings(FormSettingsBase owner) {
			this.owner = owner;
		}

		[XmlIgnore]
		[Browsable(false)]
		private bool HasOwner {
			get { return null != this.owner; }
		}
		
		#region INewSettings<FormSettingsBaseSettings> Members
		public void ApplySettings(FormSettingsBaseSettings newSettings) {
		}
		#endregion
	}
}
