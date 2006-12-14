using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Drawing;

namespace Yusen.GExplorer.Utilities {
	sealed class BackgroundImageLoader : SuccesiveTaskWorkerBase<BackgroundImageLoadTask, BackgroundImageLoadTaskComletedEventArgs>{
		private readonly CookieContainer cookieContainer;
		private readonly RequestCachePolicy requestCachePolicy;
		private int timeout;
		
		private BackgroundImageLoader(){
		}
		public BackgroundImageLoader(CookieContainer cookieContainer, RequestCachePolicy requestCachePolicy, int timeout){
			this.cookieContainer = cookieContainer;
			this.requestCachePolicy = requestCachePolicy;
			this.timeout = timeout;
		}

		protected override BackgroundImageLoadTaskComletedEventArgs DoTask(BackgroundImageLoadTask task) {
			HttpWebRequest req = WebRequest.Create(task.Uri) as HttpWebRequest;
			req.CookieContainer = this.cookieContainer;
			req.CachePolicy = this.requestCachePolicy;
			req.Timeout = this.timeout;
			try {
				HttpWebResponse res = req.GetResponse() as HttpWebResponse;
				using (Stream stream = res.GetResponseStream()) {
					Image img = Image.FromStream(stream);
					return new BackgroundImageLoadTaskComletedEventArgs(img, task.UserState);
				}
			} catch(Exception ex) {
				return new BackgroundImageLoadTaskComletedEventArgs(ex, task.UserState);
			}
		}

		protected override void OnTaskComplated(BackgroundImageLoadTaskComletedEventArgs e) {
			base.OnTaskComplated(e);
			if (e.Success && e.DisposeImage) {
				e.Image.Dispose();
			}
		}
		
		public int Timeout {
			get { return this.timeout; }
			set { this.timeout = value; }
		}
	}

	sealed class BackgroundImageLoadTask : SuccessiveTaskBase{
		private Uri uri;
		private object userState;
		
		public BackgroundImageLoadTask(Uri uri, object userState) {
			this.uri = uri;
			this.userState = userState;
		}
		public Uri Uri {
			get { return this.uri; }
		}
		public Object UserState {
			get { return this.userState; }
		}
	}
	
	sealed class BackgroundImageLoadTaskComletedEventArgs : SuccessiveTaskCompletedEventArgs{
		private readonly bool success;
		private readonly object userState;
		private readonly Image image;
		private readonly Exception error;

		private bool disposeImage = false;

		public BackgroundImageLoadTaskComletedEventArgs(Image image, object userState) {
			this.success = true;
			this.image = image;
			this.userState = userState;
		}
		public BackgroundImageLoadTaskComletedEventArgs(Exception ex, object userState) {
			this.success = false;
			this.error = ex;
			this.userState = userState;
		}

		public bool Success {
			get { return this.success; }
		}
		public object UserState {
			get { return this.userState; }
		}
		public Image Image {
			get { return this.image; }
		}
		public Exception Error {
			get { return this.error; }
		}
		
		public bool DisposeImage {
			get { return this.disposeImage; }
			set { this.disposeImage = value; }
		}
	}
}
