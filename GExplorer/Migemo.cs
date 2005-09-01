using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace Yusen.GExplorer{
	class Migemo : IDisposable{
		[DllImport("migemo.dll", EntryPoint="migemo_open")]
		private static extern IntPtr MigemoOpen(string filename);
		
		[DllImport("migemo.dll", EntryPoint="migemo_close")]
		private static extern void MigemoClose(IntPtr migemoObj);
		
		[DllImport("migemo.dll", EntryPoint="migemo_query")]
		private static extern string MigemoQuery(IntPtr migemoObj, byte[] query);
		
		[DllImport("migemo.dll", EntryPoint="migemo_release")]
		private static extern void MigemoRelease(IntPtr migemoObj, string answer);
		
		[DllImport("migemo.dll", EntryPoint="migemo_is_enable")]
		private static extern bool MigemoIsEnable(IntPtr migemoObj);

		private IntPtr migemoObj;
		private string lastAns = null;

		public Migemo(string dicFileName) {
			try {
				this.migemoObj = Migemo.MigemoOpen(dicFileName);
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
			lock (this) {
				if (null != this.lastAns) {
					string last = this.lastAns;
					this.lastAns = null;
					Migemo.MigemoRelease(this.migemoObj, last);
				}
				this.lastAns = Migemo.MigemoQuery(this.migemoObj, Encoding.GetEncoding("Shift_JIS").GetBytes(query));
				if (null != this.lastAns) {
					return this.lastAns.ToString();
				} else {
					return string.Empty;
				}
			}
		}
		
		public void Dispose() {
			if (Migemo.MigemoIsEnable(this.migemoObj)) {
				if(null != this.lastAns){
					Migemo.MigemoRelease(this.migemoObj, this.lastAns);
					this.lastAns = null;
				}
				Migemo.MigemoClose(this.migemoObj);
				this.migemoObj = IntPtr.Zero;
			}
		}
	}

	enum MigemoDictionaryId {
		Invalid=0,
		Migemo=1,
		Roma2Hira=2,
		Hira2Kana=3,
		Han2Zen=4,
	}
	enum MigemoOperatorIndex {
		Or = 0,
		NestIn = 1,
		NestOut = 2,
		SelectIn = 3,
		SelectOut = 4,
		NewLine = 5,
	}
	class MigemoException : Exception {
		public MigemoException(string message)
			: base(message) {
		}
		public MigemoException(string message, Exception innerException)
			: base(message, innerException) {
		}
	}
}
