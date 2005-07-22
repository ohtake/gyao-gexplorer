using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

#if false
namespace Yusen.GExplorer {
	interface IFormSettings {
		Point Location { get; set;}
		Size Size { get;set;}
		FormStartPosition StartPosition { get; set;}
		FormWindowState WindowState { get;set;}
	}
	interface ITopmostableFormSettings : IFormSettings{
		bool TopMost { get;set;}
	}
	
	interface IPlayerFormSettings : ITopmostableFormSettings {
		bool AutoVolumeEnabled { get;set;}
		bool CloseOnEndEnabled { get;set;}
		bool MeidaKeysEnabled { get;set;}
	}
	
	interface IGenreListViewSettings {
		AboneType AboneType { get;set;}
		View View { get;set;}
		bool MultiSelect { get;set;}
		bool FullRowSelect { get;set;}
		int ColWidthId { get;set;}
		int ColWidthLimit { get;set;}
		int ColWidthEpisode { get;set;}
		int ColWidthLead { get;set;}
	}

	interface IFormSettingsBindable : IFormSettings {
		event FormClosingEventHandler FormClosing;
		event EventHandler LocationChanged;
		event EventHandler SizeChanged;
	}
	interface ITopmostableFormSettingsBindable : IFormSettingsBindable, ITopmostableFormSettings {
	}
	interface IPlayerFormSettingsBindable : ITopmostableFormSettingsBindable, IPlayerFormSettings {
	}
	interface IGenreListViewSettingsBindable : IGenreListViewSettings {
		event EventHandler AboneTypeChanged;
		event EventHandler ViewChanged;
		event EventHandler MultiSelectChanged;
		event EventHandler FullRowSelectChanged;
		event ColumnWidthChangedEventHandler ColumnWidthChanged;
	}
	static class Utility2 {
		public static void CopyAllProperties<T>(T from, T to) {
			foreach(PropertyInfo pi in typeof(T).GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance)) {
				pi.SetValue(to, pi.GetValue(from, null), null);
			}
		}
		public static void SubstituteDifferentProperties<T>(T from, T to) {
			foreach(PropertyInfo pi in typeof(T).GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance)) {
				object propto = pi.GetValue(to, null);
				object propfrom = pi.GetValue(from, null);
				if(propto != propfrom) {
					pi.SetValue(to, propfrom, null);
				}
			}
		}
	}
}
#endif
