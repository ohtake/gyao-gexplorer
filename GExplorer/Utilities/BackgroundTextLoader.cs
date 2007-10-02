using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.IO;

namespace Yusen.GExplorer.Utilities {
	sealed class BackgroundTextLoader : SuccesiveTaskWorkerBase<BackgroundTextLoadTask, BackgroundTextLoadTaskCompletedEventArgs>{
		private readonly Encoding encoding;
		private readonly CookieContainer cookieContainer;
		private readonly RequestCachePolicy requestCachePolicy;
		private int timeout;
		
		private BackgroundTextLoader(){
		}
		public BackgroundTextLoader(Encoding encoding, CookieContainer cookieContainer, RequestCachePolicy requestCachePolicy, int timeout) {
			this.encoding = encoding;
			this.cookieContainer = cookieContainer;
			this.requestCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache); //requestCachePolicy;
			this.timeout = timeout;
		}

		protected override BackgroundTextLoadTaskCompletedEventArgs DoTask(BackgroundTextLoadTask task) {
			HttpWebRequest req = WebRequest.Create(task.Uri) as HttpWebRequest;
			req.CookieContainer = this.cookieContainer;
			req.CachePolicy = this.requestCachePolicy;
			req.Timeout = this.timeout;
			if (task.PostBody != null) {
				req.Method = "POST";
				req.ContentType = "application/x-www-form-urlencoded";
				byte[] postData = Encoding.ASCII.GetBytes(task.PostBody);
				req.ContentLength = postData.Length;
				using (Stream reqStream = req.GetRequestStream()) {
					reqStream.Write(postData, 0, postData.Length);
				}
			}
			try {
				string text;
				HttpWebResponse res = req.GetResponse() as HttpWebResponse;
				using (Stream stream = res.GetResponseStream())
				using (TextReader reader = new StreamReader(stream, this.encoding)) {
					text = reader.ReadToEnd();
				}
				return new BackgroundTextLoadTaskCompletedEventArgs(text, task.UserState);
			} catch (Exception ex) {
				return new BackgroundTextLoadTaskCompletedEventArgs(ex, task.UserState);
			}
		}
		
		public int Timeout {
			get { return this.timeout; }
			set { this.timeout = value; }
		}
	}

	sealed class BackgroundTextLoadTask : SuccessiveTaskBase{
		private Uri uri;
		private string postBody = null;
		private object userState;
		
		public BackgroundTextLoadTask(Uri uri, object userState) {
			this.uri = uri;
			this.userState = userState;
		}
		public BackgroundTextLoadTask(Uri uri, string postBody, object userState) : this(uri, userState){
			this.postBody = postBody;
		}
		public Uri Uri {
			get { return this.uri; }
		}
		public string PostBody {
			get { return this.postBody; }
		}
		public Object UserState {
			get { return this.userState; }
		}
	}

	sealed class BackgroundTextLoadTaskCompletedEventArgs : SuccessiveTaskCompletedEventArgs {
		private readonly bool success;
		private readonly string text;
		private readonly Exception error;
		private readonly object userState;

		public BackgroundTextLoadTaskCompletedEventArgs(string text, object userState) {
			this.success = true;
			this.text = text;
			this.userState = userState;
		}
		public BackgroundTextLoadTaskCompletedEventArgs(Exception ex, object userState) {
			this.success = false;
			this.error = ex;
			this.userState = userState;
		}

		public bool Success {
			get { return this.success; }
		}
		public string Text {
			get { return this.text; }
		}
		public Exception Error {
			get { return this.error; }
		}
		public object UserState {
			get { return this.userState; }
		}
	}
}
