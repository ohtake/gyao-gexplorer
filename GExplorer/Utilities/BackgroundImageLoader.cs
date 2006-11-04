using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Drawing;

namespace Yusen.GExplorer.Utilities {
	sealed class BackgroundImageLoader : IDisposable{
		private readonly Stack<BackgroundImageLoadTask> stack = new Stack<BackgroundImageLoadTask>();
		private readonly object workLock = new object();
		private volatile bool working = false;
		private volatile bool disposed = false;
		private readonly int intervalMilliseconds;
		
		private AutoResetEvent autoResetEvent = new AutoResetEvent(false);
		
		public BackgroundImageLoader() : this(0){
		}
		public BackgroundImageLoader(int intervalMilliseconds) {
			this.intervalMilliseconds = intervalMilliseconds;
		}
		
		public void PushTask(BackgroundImageLoadTask task) {
			if (this.disposed) throw new InvalidOperationException();
			lock (this.stack) {
				this.stack.Push(task);
				this.autoResetEvent.Set();
			}
		}
		public void ClearTasks() {
			if (this.disposed) throw new InvalidOperationException();
			lock (this.stack) {
				this.stack.Clear();
			}
		}
		public int StackLength {
			get {
				lock (this.stack) {
					return this.stack.Count;
				}
			}
		}
		
		private void Work(object o) {
			while (true) {
				if (!this.IsWorking) return;
				BackgroundImageLoadTask task = null;
				lock (this.stack) {
					if (this.stack.Count > 0) {
						task = this.stack.Pop();
					}
				}
				if (null == task) {
					this.autoResetEvent.WaitOne();
					continue;
				}
				HttpWebRequest req = WebRequest.Create(task.Uri) as HttpWebRequest;
				req.CachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable);
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
				Thread.Sleep(this.intervalMilliseconds);
			}
		}
		
		public void StartWorking() {
			lock (this.workLock) {
				if (this.disposed) throw new InvalidOperationException();
				if (this.IsWorking) throw new InvalidOperationException();

				this.IsWorking = true;
				if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this.Work))) {
					this.IsWorking = false;
					throw new InvalidOperationException();
				}
			}
		}
		public void StopWorking() {
			lock (this.workLock) {
				if (this.disposed) throw new InvalidOperationException();
				if (!this.IsWorking) throw new InvalidOperationException();

				this.IsWorking = false;
				this.autoResetEvent.Set();
			}
		}
		public bool IsWorking {
			get { return this.working; }
			private set { this.working = value; }
		}

		private void Dispose(bool disposing) {
			if (disposing) {
				lock (this.workLock) {
					if (this.IsWorking) this.StopWorking();
				}
				this.autoResetEvent.Close();
				this.disposed = true;
			}
		}

		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		~BackgroundImageLoader() {
			this.Dispose(false);
		}
	}

	sealed class BackgroundImageLoadTask {
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
