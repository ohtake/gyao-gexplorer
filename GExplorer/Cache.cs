using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Yusen.GCrawler;
using System.Collections.ObjectModel;

namespace Yusen.GExplorer {
	sealed class Cache {
		private const string CacheDir = @"Cache";
		private const string ContentsFilename = @"Contents.xml";
		private const string DeadlineFilenameXml = @"Deadlines.xml";
		private const string DeadlineFilenameBin = @"Deadlines.bin";
		private const string ResultsFilename = @"CrawlResults.bin";
		
		private static Cache instance = new Cache();
		public static Cache Instance {
			get { return Cache.instance; }
		}
		public static void Initialize() {
			DirectoryInfo di = new DirectoryInfo(Cache.CacheDir);
			if (!di.Exists) di.Create();

			Cache.Instance.cacheCtl = new ContentCacheControllerSortedDic(Cache.CacheDir);
			Cache.instance.cacheCtl.DeserializeContents(Path.Combine(Cache.CacheDir, Cache.ContentsFilename));

			try {
				using (Stream stream = new FileStream(Path.Combine(Cache.CacheDir, Cache.DeadlineFilenameBin), FileMode.Open)) {
					IFormatter formatter = new BinaryFormatter();
					Cache.Instance.deadlineTable = (DeadlineTableSortedDic)formatter.Deserialize(stream);
				}
			} catch {
				Cache.Instance.deadlineTable = new DeadlineTableSortedDic();
			}
			Cache.Instance.deadlineTable.DeserializeDeadlineTableFromXml(Path.Combine(Cache.CacheDir, Cache.DeadlineFilenameXml));

			try {
				Dictionary<GGenre, CrawlResult> oldResults = null;
				using (Stream stream = new FileStream(Path.Combine(Cache.CacheDir, Cache.ResultsFilename), FileMode.Open)) {
					IFormatter formatter = new BinaryFormatter();
					oldResults = (Dictionary<GGenre, CrawlResult>)formatter.Deserialize(stream);
				}
				
				//かつてのバージョンではあったが新しいバージョンではなくなったジャンルを削除
				Dictionary<GGenre, CrawlResult> newResults = new Dictionary<GGenre, CrawlResult>();
				foreach (GGenre genre in GGenre.AllGenres) {
					CrawlResult result;
					if (genre.IsCrawlable && oldResults.TryGetValue(genre, out result)) {
						newResults.Add(genre, result);
					}
				}
				
				Cache.Instance.resultsDic = newResults;
			} catch {
				Cache.Instance.resultsDic = new Dictionary<GGenre, CrawlResult>();
			}
		}
		public static void Serialize() {
			Cache.Instance.cacheCtl.SerializeContentes(Path.Combine(Cache.CacheDir, Cache.ContentsFilename));
			Cache.Instance.cacheCtl.DeleteTempCachesRead();

			Cache.Instance.deadlineTable.SerializeDeadlineTableInXml(Path.Combine(Cache.CacheDir, Cache.DeadlineFilenameXml));
			FileInfo fiDeadlineBinary = new FileInfo(Path.Combine(Cache.CacheDir, Cache.DeadlineFilenameBin));
			if (fiDeadlineBinary.Exists) fiDeadlineBinary.Delete();

			using (Stream stream = new FileStream(Path.Combine(Cache.CacheDir, Cache.ResultsFilename), FileMode.Create)) {
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, Cache.Instance.resultsDic);
			}
		}

		public event EventHandler<CacheEventArgs> CacheRearranged;

		private ContentCacheControllerSortedDic cacheCtl;
		private DeadlineTableSortedDic deadlineTable;
		private Dictionary<GGenre, CrawlResult> resultsDic;
		
		private Cache() {
		}
		
		public IContentCacheController ContentCacheController{
			get { return this.cacheCtl; }
		}
		public IDeadlineTable DeadlineTable {
			get { return this.deadlineTable; }
		}
		public IDeadlineTableReadOnly DeadlineTableReadOnly {
			get { return this.deadlineTable; }
		}
		public IDictionary<GGenre, CrawlResult> ResultsDictionary {
			get { return this.resultsDic; }
		}

		public List<string> GetSortedReachableContentIds() {
			List<string> reachable = new List<string>();
			foreach (CrawlResult result in this.resultsDic.Values) {
				foreach (GPackage package in result.Packages) {
					foreach (GContent cont in package.Contents) {
						reachable.Add(cont.ContentId);
					}
				}
			}
			reachable.Sort();
			return reachable;
		}

		private void OnCacheRearranged(CacheEventArgs e) {
			if(null != this.CacheRearranged) {
				this.CacheRearranged(this, e);
			}
		}

		public void ClearCrawlResults() {
			int numResults = Cache.Instance.ResultsDictionary.Count;
			Cache.Instance.ResultsDictionary.Clear();
			this.OnCacheRearranged(new CacheEventArgs(
				string.Format("クロール結果の破棄    破棄数: {0}", numResults)));
		}
		public void RemoveCachesUnreachable() {
			List<string> reachable = this.GetSortedReachableContentIds();
			
			int success = 0;
			int failed = 0;
			int ignored = 0;
			foreach(string key in this.ContentCacheController.ListAllCacheKeys()) {
				if(reachable.BinarySearch(key) >= 0) {
					ignored++;
				} else {
					if(this.ContentCacheController.RemoveCache(key)) {
						success++;
					} else {
						failed++;
					}
				}
			}
			this.OnCacheRearranged(new CacheEventArgs(
				string.Format("キャッシュの削除    到達可により無視: {0}    削除成功: {1}    削除失敗: {2}",
					ignored, success, failed)));
		}
		public void RemoveCachesAll() {
			int success = 0;
			int failed = 0;
			foreach(string key in this.ContentCacheController.ListAllCacheKeys()) {
				if(Cache.Instance.ContentCacheController.RemoveCache(key)) {
					success++;
				} else {
					failed++;
				}
			}
			this.OnCacheRearranged(new CacheEventArgs(
				string.Format("キャッシュの削除    削除成功: {0}    削除失敗: {1}",
					success, failed)));
		}
		public void RemoveDeadlineEntriesUnreacheable() {
			List<string> reachable = this.GetSortedReachableContentIds();

			int success = 0;
			int failed = 0;
			int ignored = 0;
			foreach(string key in new List<string>(this.DeadlineTable.ListContentIds())) {
				if(reachable.BinarySearch(key) >= 0) {
					ignored++;
				} else {
					if(this.DeadlineTable.RemoveDeadlineOf(key)) {
						success++;
					} else {
						failed++;
					}
				}
			}
			this.OnCacheRearranged(new CacheEventArgs(
				string.Format("配信期限エントリーの整理    到達可により無視: {0}    削除成功: {1}    削除失敗: {2}",
					ignored, success, failed)));
		}
		public void RemoveDeadlineEntriesAll() {
			int count = this.DeadlineTable.Count;
			this.DeadlineTable.ClearDeadlines();
			this.OnCacheRearranged(new CacheEventArgs(
				string.Format("配信期限エントリーの整理    削除成功: {0}" + count)));
		}
	}

	class CacheEventArgs : EventArgs {
		private string message;
		public CacheEventArgs(string message) {
			this.message = message;
		}
		public string Message {
			get { return this.message; }
		}
	}
}
