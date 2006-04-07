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
		[Obsolete]//2.0.4.0
		private const string DeadlineFilenameXml = @"Deadlines.xml";
		[Obsolete]//2.0.4.0よりもちょっと前
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
				Dictionary<GGenre, CrawlResult> oldResults = null;
				using (Stream stream = new FileStream(Path.Combine(Cache.CacheDir, Cache.ResultsFilename), FileMode.Open)) {
					IFormatter formatter = new BinaryFormatter();
					oldResults = (Dictionary<GGenre, CrawlResult>)formatter.Deserialize(stream);
				}

				//Deadlines.* を削除
				FileInfo fiDeadline = new FileInfo(Path.Combine(Cache.CacheDir, Cache.DeadlineFilenameXml));
				if (fiDeadline.Exists) fiDeadline.Delete();
				fiDeadline = new FileInfo(Path.Combine(Cache.CacheDir, Cache.DeadlineFilenameBin));
				if (fiDeadline.Exists) fiDeadline.Delete();
				
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

			using (Stream stream = new FileStream(Path.Combine(Cache.CacheDir, Cache.ResultsFilename), FileMode.Create)) {
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, Cache.Instance.resultsDic);
			}
		}

		private ContentCacheControllerSortedDic cacheCtl;
		private Dictionary<GGenre, CrawlResult> resultsDic;
		
		private Cache() {
		}
		
		public IContentCacheController ContentCacheController{
			get { return this.cacheCtl; }
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

		public void ClearCrawlResults() {
			int numResults = Cache.Instance.ResultsDictionary.Count;
			Cache.Instance.ResultsDictionary.Clear();
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
		}
	}
}
