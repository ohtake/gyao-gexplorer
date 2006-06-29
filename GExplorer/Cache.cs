using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Yusen.GCrawler;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Forms;

namespace Yusen.GExplorer {
	sealed class Cache {
		private static readonly string CacheDir = Path.Combine(Application.StartupPath, "Cache");
		private const string ContentsFilename = @"Contents.xml";
		private const string ResultsFilename = @"CrawlResults.bin";
		
		private static Cache instance = new Cache();
		public static Cache Instance {
			get { return Cache.instance; }
		}
		public static void Initialize() {
			DirectoryInfo di = new DirectoryInfo(Cache.CacheDir);
			if (!di.Exists) di.Create();

			Cache.Instance.cacheCtl = new ContentCacheControllerSortedDic();
			try {
				FileInfo fi = new FileInfo(Path.Combine(Cache.CacheDir, Cache.ContentsFilename));
				if (fi.Exists) {
					Cache.instance.cacheCtl.DeserializeContents(fi.FullName);
				}
			} catch (Exception e) {
				Program.DisplayException("キャッシュの読み込みエラー", e);
			}

			Cache.Instance.resultsDic = new Dictionary<GGenre, CrawlResult>();
			try {
				FileInfo fi = new FileInfo(Path.Combine(Cache.CacheDir, Cache.ResultsFilename));
				if (fi.Exists) {
					Dictionary<GGenre, CrawlResult> oldResults = null;
					using (Stream stream = new FileStream(fi.FullName, FileMode.Open)) {
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
				}
			} catch (Exception e){
				Program.DisplayException("前回終了時のクロール結果の読み込み失敗", e);
			}
		}
		public static void Serialize() {
			Cache.Instance.cacheCtl.SerializeContentes(Path.Combine(Cache.CacheDir, Cache.ContentsFilename));

			using (Stream stream = new FileStream(Path.Combine(Cache.CacheDir, Cache.ResultsFilename), FileMode.Create)) {
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, Cache.Instance.resultsDic);
			}
		}

		private ContentCacheControllerSortedDic cacheCtl;
		private Dictionary<GGenre, CrawlResult> resultsDic; //<int, CrawlResult> にすべきか
		
		private Cache() {
		}
		
		public IContentCacheController ContentCacheController{
			get { return this.cacheCtl; }
		}
		public IDictionary<GGenre, CrawlResult> ResultsDictionary {
			get { return this.resultsDic; }
		}

		public List<int> GetSortedReachableContentKeys() {
			List<int> reachable = new List<int>();
			foreach (CrawlResult result in this.resultsDic.Values) {
				foreach (GPackage package in result.Packages) {
					foreach (GContent cont in package.Contents) {
						reachable.Add(cont.ContentKey);
					}
				}
			}
			reachable.Sort();
			return reachable;
		}

		public void RemoveCachesUnreachable() {
			List<int> reachable = this.GetSortedReachableContentKeys();
			
			int success = 0;
			int failed = 0;
			int ignored = 0;
			foreach(int key in this.ContentCacheController.ListAllCacheKeys()) {
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

		public ContentAdapter GetCacheOrDownloadContent(int contentKey) {
			ContentCache cache;
			if (this.cacheCtl.TryGetCache(contentKey, out cache)) {
				return new ContentAdapter(cache.Content);
			} else {
				return new ContentAdapter(GContent.DoDownload(contentKey));
			}
		}

		public IList<GContent> FindCaches(string query) {
			List<GContent> conts = new List<GContent>();
			foreach (int key in this.cacheCtl.ListAllCacheKeys()) {
				ContentCache cache;
				if (!this.cacheCtl.TryGetCache(key, out cache)) continue;
				GContent cont = cache.Content;
				
				if (cont.Title.Contains(query)) goto match;
				if (cont.SeriesNumber.Contains(query)) goto match;
				if (cont.Subtitle.Contains(query)) goto match;
				if (cont.Summary.Contains(query)) goto match;
				if (cont.Description1.Contains(query)) goto match;
				if (cont.Description2.Contains(query)) goto match;
				if (cont.Description3.Contains(query)) goto match;
				if (cont.Description4.Contains(query)) goto match;
				continue;
			match:
				conts.Add(cont);
				continue;
			}
			return conts;
		}
	}
}
