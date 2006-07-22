using System;
using System.Collections.Generic;
using System.Text;
using BindingFlags = System.Reflection.BindingFlags;
using Interaction = Microsoft.VisualBasic.Interaction;

namespace Yusen.GExplorer {
	public class XmlHttpWrapper {
		private object xmlhttp;
		private Type type;

		public XmlHttpWrapper() {
			this.xmlhttp = Interaction.CreateObject("MSXML2.XMLHTTP", string.Empty);
			this.type = this.xmlhttp.GetType();
		}

		public string GetResponseText(Uri uri) {
			this.type.InvokeMember("open", BindingFlags.InvokeMethod, null, this.xmlhttp,
				new object[] { "GET", uri.AbsoluteUri, false });
			this.type.InvokeMember("send", BindingFlags.InvokeMethod, null, this.xmlhttp,
				new object[] { });
			return type.InvokeMember("responseText", BindingFlags.GetProperty, null, this.xmlhttp,
				new object[] { }) as string;
		}
	}
}
