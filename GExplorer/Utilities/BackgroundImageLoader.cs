using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Drawing;

namespace Yusen.GExplorer.Utilities {
	sealed class BackgroundImageLoader : SuccesiveTaskWorkerBase<BackgroundImageLoadTask>{
		private readonly CookieContainer cookieContainer;
		private readonly RequestCachePolicy requestCachePolicy;

		private BackgroundImageLoader(){
		}
		public BackgroundImageLoader(CookieContainer cookieContainer, RequestCachePolicy requestCachePolicy){
			this.cookieContainer = cookieContainer;
			this.requestCachePolicy = requestCachePolicy;
		}
		
		protected override void DoTask(BackgroundImageLoadTask task) {
			HttpWebRequest req = WebRequest.Create(task.Uri) as HttpWebRequest;
			req.CookieContainer = this.cookieContainer;
			req.CachePolicy = this.requestCachePolicy;
			try {
				HttpWebResponse res = req.GetResponse() as HttpWebResponse;
				using (Stream stream = res.GetResponseStream()) {
					Image img = Image.FromStream(stream);
					if (!task.InvokeCallback(img)) {
						img.Dispose();
					}
				}
			} catch {
				task.InvokeCallback(null);
			}
		}
	}

	sealed class BackgroundImageLoadTask : SuccessiveTaskBase{
		private Uri uri;
		private BackgroundImageLoadCompletedCallback callback;
		private object userState;
		
		public BackgroundImageLoadTask(Uri uri, BackgroundImageLoadCompletedCallback callback, object userState) {
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
		public bool InvokeCallback(Image img) {
			return this.callback(img, this.userState);
		}
	}
	
	/// <summary></summary>
	/// <param name="img">読み込んだ画像</param>
	/// <returns>引数の画像を受け取るのならばtrue</returns>
	delegate bool BackgroundImageLoadCompletedCallback(Image img, object userState);
}
