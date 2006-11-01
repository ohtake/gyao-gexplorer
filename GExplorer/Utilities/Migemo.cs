using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Yusen.GExplorer.Utilities{
	sealed class Migemo : IDisposable{
#if false
		private enum DictionaryId{
			Invalid		= 0,
			Migemo		= 1,
			RomaToHira	= 2,
			HiraToKata	= 3,
			HanToZen	= 4,
		}
#endif
		private enum OperatorIndex : int{
			Or			= 0,
			NestIn		= 1,
			NestOut		= 2,
			SelectIn	= 3,
			SelectOut	= 4,
			NewLine		= 5,
		}

		[DllImport("migemo.dll", EntryPoint="migemo_open")]
		private static extern IntPtr MigemoOpen([In] string filename);
		[DllImport("migemo.dll", EntryPoint="migemo_close")]
		private static extern void MigemoClose(IntPtr migemoObj);
		
		[DllImport("migemo.dll", EntryPoint="migemo_query")]
		private static extern IntPtr MigemoQuery(IntPtr migemoObj, IntPtr query);
		[DllImport("migemo.dll", EntryPoint="migemo_release")]
		private static extern void MigemoRelease(IntPtr migemoObj, IntPtr answer);
		
		[DllImport("migemo.dll", EntryPoint="migemo_is_enable")]
		private static extern bool MigemoIsEnable(IntPtr migemoObj);
		[DllImport("migemo.dll", EntryPoint="migemo_set_operator")]
		private static extern int MigemoSetOperator(IntPtr migemoObj, OperatorIndex opIndex, [In] string opString);

		private IntPtr migemoObj = IntPtr.Zero;

		public Migemo(string dicFileName) {
			try {
				this.migemoObj = Migemo.MigemoOpen(dicFileName);
				Migemo.MigemoSetOperator(this.migemoObj, OperatorIndex.NestIn, "(?:");
				if (!Migemo.MigemoIsEnable(this.migemoObj)) {
					throw new MigemoException("おそらくmigemoの辞書が見つからなかった．");
				}
			}catch(DllNotFoundException e){
				throw new MigemoException("migemoのdllが見つからなかった．", e);
			}catch(MigemoException){
				throw;
			} catch(Exception e) {
				throw new MigemoException("不明な理由でmigemoの初期化失敗．", e);
			}
		}

		public string Query(string query) {
			IntPtr qry = IntPtr.Zero;
			IntPtr ans = IntPtr.Zero;
			string ansStr = string.Empty;
			try {
				qry = Marshal.StringToCoTaskMemAnsi(query);
				ans = Migemo.MigemoQuery(this.migemoObj, qry);
				if(IntPtr.Zero != ans) {
					ansStr = Marshal.PtrToStringAnsi(ans);
				}
			} finally {
				if (IntPtr.Zero != qry) {
					Marshal.FreeCoTaskMem(qry);
				}
				if(IntPtr.Zero != ans) {
					Migemo.MigemoRelease(this.migemoObj, ans);
				}
			}
			return ansStr;
		}

		private void Dispose(bool disposing) {
			if (disposing) {
			}
			if (IntPtr.Zero != this.migemoObj && Migemo.MigemoIsEnable(this.migemoObj)) {
				Migemo.MigemoClose(this.migemoObj);
				this.migemoObj = IntPtr.Zero;
			}
		}
		
		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Migemo(){
			this.Dispose(false);
		}
	}

	sealed class MigemoException : Exception {
		public MigemoException(string message)
			: base(message) {
		}
		public MigemoException(string message, Exception innerException)
			: base(message, innerException) {
		}
	}
}
