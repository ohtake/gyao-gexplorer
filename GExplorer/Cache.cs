using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Yusen.GCrawler;

namespace Yusen.GExplorer {
	class Cache {
		private const string CacheDir = @"Cache";
		private const string DeadlineFilename = @"Deadlines.bin";
		private const string ResultsFilename = @"CrawlResults.bin";
		
		private static Cache instance = new Cache();
		public static Cache Instance {
			get { return Cache.instance; }
		}
		public static void Initialize() {
			Cache.Instance.cacheCtl = new ContentCacheControllerXml(Cache.CacheDir);
			
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
			
			try {
				using (Stream stream = new FileStream(Path.Combine(Cache.CacheDir, Cache.DeadlineFilename), FileMode.Open)) {
					IFormatter formatter = new BinaryFormatter();
					Cache.Instance.deadlineTable = (DeadlineTableSortedDic)formatter.Deserialize(stream);
				}
			} catch {
				Cache.Instance.deadlineTable = new DeadlineTableSortedDic();
			}
		}
		public static void Serialize() {
			//this.cacheCtl には何もしない
			
			using (Stream stream = new FileStream(Path.Combine(Cache.CacheDir, Cache.ResultsFilename), FileMode.Create)) {
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, Cache.Instance.resultsDic);
			}
			using (Stream stream = new FileStream(Path.Combine(Cache.CacheDir, Cache.DeadlineFilename), FileMode.Create)) {
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, Cache.Instance.deadlineTable);
			}
		}
		
		private ContentCacheControllerXml cacheCtl;
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
	}
}
