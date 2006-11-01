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
		private readonly Queue<BackgroundImageLoadTask> queue = new Queue<BackgroundImageLoadTask>();
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
		
		public void EnqueueTask(BackgroundImageLoadTask task) {
			if (this.disposed) throw new InvalidOperationException();
			lock (this.queue) {
				this.queue.Enqueue(task);
				this.autoResetEvent.Set();
			}
		}
		public void ClearTasks() {
			if (this.disposed) throw new InvalidOperationException();
			lock (this.queue) {
				this.queue.Clear();
			}
		}
		public int QueueLength {
			get {
				lock (this.queue) {
					return this.queue.Count;
				}
			}
		}
		
		private void Work(object o) {
			while (true) {
				if (!this.IsWorking) return;
				BackgroundImageLoadTask task = null;
				lock (this.queue) {
					if (this.queue.Count > 0) {
						task = this.queue.Dequeue();
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
		
		public BackgroundImageLoadTask(Uri uri, BackgroundImageLoadCompletedCallback callback) {
			this.uri = uri;
			this.callback = callback;
		}
		public Uri Uri {
			get { return this.uri; }
		}
		public bool InvokeCallback(Image img) {
			return this.callback(img);
		}
	}
	
	/// <summary></summary>
	/// <param name="img">読み込んだ画像</param>
	/// <returns>引数の画像を受け取るのならばtrue</returns>
	delegate bool BackgroundImageLoadCompletedCallback(Image img);
}
