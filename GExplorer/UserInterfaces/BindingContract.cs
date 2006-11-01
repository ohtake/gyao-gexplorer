using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Yusen.GExplorer.UserInterfaces {
	static class BindingContractUtility {
		public static void BindAllProperties<TControl, TIOptions>(TControl control, TIOptions options) where TControl : Control, TIOptions, INotifyPropertyChanged where TIOptions : IBindingContract {
			foreach (PropertyInfo pi in typeof(TIOptions).GetProperties()) {
				control.DataBindings.Add(pi.Name, options, pi.Name, false, DataSourceUpdateMode.OnPropertyChanged);
			}
		}
	}
	
	interface IBindingContract {
	}
}
