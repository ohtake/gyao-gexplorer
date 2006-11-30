using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.IO;

namespace Yusen.GExplorer.Utilities {
	sealed class BackgroundTextLoader : SuccesiveTaskWorkerBase<BacktroundTextLoadTask>{
		private readonly Encoding encoding;
		private readonly CookieContainer cookieContainer;
		private readonly RequestCachePolicy requestCachePolicy;
		
		private BackgroundTextLoader(){
		}
		public BackgroundTextLoader(Encoding encoding, CookieContainer cookieContainer, RequestCachePolicy requestCachePolicy) {
			this.encoding = encoding;
			this.cookieContainer = cookieContainer;
			this.requestCachePolicy = requestCachePolicy;
		}

		protected override void DoTask(BacktroundTextLoadTask task) {
			HttpWebRequest req = WebRequest.Create(task.Uri) as HttpWebRequest;
			req.CookieContainer = this.cookieContainer;
			req.CachePolicy = this.requestCachePolicy;
			try {
				HttpWebResponse res = req.GetResponse() as HttpWebResponse;
				using (Stream stream = res.GetResponseStream())
				using (TextReader reader = new StreamReader(stream, this.encoding)) {
					string text = reader.ReadToEnd();
					task.InvokeCallback(text);
				}
			} catch {
				task.InvokeCallback(null);
			}
		}
	}

	sealed class BacktroundTextLoadTask : SuccessiveTaskBase{
		private Uri uri;
		private BackgroundTextLoadCompletedCallback callback;
		private object userState;
		
		public BacktroundTextLoadTask(Uri uri, BackgroundTextLoadCompletedCallback callback, object userState) {
			this.uri = uri;
			this.callback = callback;
			this.userState = userState;
		}
		public Uri Uri {
			get { return this.uri; }
		}
		public Object UserState {
			get { return this.userState; }
		}
		public void InvokeCallback(string text) {
			this.callback(text, this.userState);
		}
	}
	
	delegate void BackgroundTextLoadCompletedCallback(string text, object userState);
}
