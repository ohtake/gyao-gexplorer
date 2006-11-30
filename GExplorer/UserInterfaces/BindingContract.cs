using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Yusen.GExplorer.UserInterfaces {
	static class BindingContractUtility {
		public static void BindAllProperties<TControl, TIBindingContract>(TControl control, TIBindingContract target)
			where TControl : Control, TIBindingContract, INotifyPropertyChanged
			where TIBindingContract : IBindingContract {
			foreach (PropertyInfo pi in typeof(TIBindingContract).GetProperties()) {
				control.DataBindings.Add(pi.Name, target, pi.Name, false, DataSourceUpdateMode.OnPropertyChanged);
			}
		}
	}
	
	interface IBindingContract {
	}
}
