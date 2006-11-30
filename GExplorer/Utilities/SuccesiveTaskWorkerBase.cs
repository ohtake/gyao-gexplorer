using System;
using System.Collections.Generic;
using System.Threading;

namespace Yusen.GExplorer.Utilities {
	abstract class SuccesiveTaskWorkerBase<TTask> : IDisposable
		where TTask : SuccessiveTaskBase {
		private readonly LinkedList<TTask> tasks = new LinkedList<TTask>();
		private readonly object workLock = new object();
		private volatile bool working = false;
		private volatile bool disposed = false;
		private AutoResetEvent autoResetEvent = new AutoResetEvent(false);

		public void AddTaskFirst(TTask task) {
			if (this.disposed) throw new InvalidOperationException();
			lock (this.tasks) {
				this.tasks.AddFirst(task);
				this.autoResetEvent.Set();
			}
		}
		public void AddTaskLast(TTask task) {
			if (this.disposed) throw new InvalidOperationException();
			lock (this.tasks) {
				this.tasks.AddLast(task);
				this.autoResetEvent.Set();
			}
		}
		public void ClearTasks() {
			if (this.disposed) throw new InvalidOperationException();
			lock (this.tasks) {
				this.tasks.Clear();
			}
		}
		public int TaskCount {
			get {
				lock (this.tasks) {
					return this.tasks.Count;
				}
			}
		}
		
		protected abstract void DoTask(TTask task);
		
		private void Work(object o) {
			while (true) {
				if (!this.IsWorking) return;
				TTask task = null;
				lock (this.tasks) {
					if (this.tasks.Count > 0) {
						task = this.tasks.First.Value;
						this.tasks.RemoveFirst();
					}
				}
				if (null == task) {
					this.autoResetEvent.WaitOne();
					continue;
				}
				this.DoTask(task);
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

		protected virtual void Dispose(bool disposing) {
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
		~SuccesiveTaskWorkerBase() {
			this.Dispose(false);
		}
	}
	
	abstract class SuccessiveTaskBase {
	}
}
