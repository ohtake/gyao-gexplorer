using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Yusen.GCrawler {
	public class Crawler {
		private class CrawlerHelper{
			private static UriLinkTypePair ConvertUriWithoutFragment(UriLinkTypePair pair) {
				if (string.IsNullOrEmpty(pair.Uri.Fragment)) {
					return pair;
				} else {
					return new UriLinkTypePair(new Uri(pair.Uri.GetLeftPart(UriPartial.Query)), pair.LinkType);
				}
			}
			
			private BackgroundWorker bw;

			private CrawlSettings settings;
			private GGenre genre;
			private IHtmlParser parser;
			private IContentCacheController cacheController;
			
			private Queue<Uri> pagesWaiting = new Queue<Uri>();
			private List<Uri> pagesSuccess = new List<Uri>();
			private List<Uri> pagesFailed = new List<Uri>();
			
			private Queue<int> pacsWaiting = new Queue<int>();
			private Dictionary<int, GPackage> pacsSuccess = new Dictionary<int, GPackage>();
			private List<int> pacsFailed = new List<int>();
			
			private Queue<int> contsWatings = new Queue<int>();
			private Dictionary<int, GContent> contsVisited = new Dictionary<int, GContent>();
			private List<int> contsIgnored = new List<int>();

			private Dictionary<int, int> contPackRelations = new Dictionary<int, int>();
			private SortedDictionary<int, ContentPropertiesOnPackagePage> cpPacs = new SortedDictionary<int, ContentPropertiesOnPackagePage>();
			private List<Exception> ignoredExceptions = new List<Exception>();
			
			public CrawlerHelper(CrawlSettings settings, GGenre genre, IHtmlParser parser, IContentCacheController cacheController, BackgroundWorker bw) {
				this.bw = bw;
				this.settings = settings;
				this.genre = genre;
				this.parser = parser;
				this.cacheController = cacheController;
				
				switch(settings.CrawlOrder) {
					case CrawlOrder.TopPageFirst:
						this.pagesWaiting.Enqueue(this.genre.TopPageUri);
						this.PushTimeTable();
						break;
					case CrawlOrder.TimetableFirst:
						this.PushTimeTable();
						this.pagesWaiting.Enqueue(this.genre.TopPageUri);
						break;
				}
			}
			private void PushTimeTable (){
				switch(this.settings.TimeTableSortType) {
					case TimetableSortType.RecentlyUpdatedFirst:
						this.pagesWaiting.Enqueue(this.genre.TimetableRecentlyUpdatedFirstUri);
						break;
					case TimetableSortType.DeadlineNearFirst:
						this.pagesWaiting.Enqueue(this.genre.TimetableDeadlineNearFirstUri);
						break;
				}
			}
			
			public CrawlResult StartCrawling() {
				if (this.bw.CancellationPending) return null;
				this.CrawlPages();
				if (this.bw.CancellationPending) return null;
				this.FetchPackages();
				if (this.bw.CancellationPending) return null;
				this.FetchContents();
				if (this.bw.CancellationPending) return null;
				return this.CreateCrawlResult(this.BuildGpcTree());
			}
			private void ReportProgressToBackgroundWorker(string message) {
				int visited =
					this.pagesSuccess.Count + this.pagesFailed.Count
					+this.pacsSuccess.Count + this.pacsFailed.Count
					+ this.contsVisited.Count + this.contsIgnored.Count;
				int waiting = this.pagesWaiting.Count + this.pacsWaiting.Count + this.contsWatings.Count;
				CrawlProgressEventArgs e = new CrawlProgressEventArgs(message, visited, waiting);
				this.bw.ReportProgress(100*visited/(visited + waiting), e);
			}
			private void OnIgnoringException(Exception e) {
				this.ignoredExceptions.Add(e);
			}
			/// <summary>
			/// パッケージとコンテンツ以外のページをクロールし，
			/// パッケージとコンテンツのIDを取得する．
			/// </summary>
			private void CrawlPages() {
				while (this.pagesWaiting.Count > 0) {
					if (this.bw.CancellationPending) return;
					
					if(this.pagesSuccess.Count + this.pagesFailed.Count >= this.settings.MaxPages) {
						this.OnIgnoringException(new Exception(string.Format("クロールする一般ページの上限数に達した． ({0}/{1})", this.settings.MaxPages, this.pagesSuccess.Count + this.pagesWaiting.Count)));
						this.pagesWaiting.Clear();
						return;
					}
					this.ReportProgressToBackgroundWorker(string.Format("フェーズ 1/4: 一般ページを取得中 ({0}/{1}) {2}",
						this.pagesSuccess.Count + this.pagesFailed.Count,
						this.pagesSuccess.Count + this.pagesFailed.Count + this.pagesWaiting.Count,
						this.pagesWaiting.Peek().PathAndQuery));
					
					Uri uri = this.pagesWaiting.Dequeue();
					List<UriLinkTypePair> links;
					try {
						links = this.parser.ExtractLinks(uri);
					} catch(Exception e) {
						this.pagesFailed.Add(uri);
						this.OnIgnoringException(new Exception(string.Format("一般ページの取得失敗 <{0}>", uri.AbsoluteUri), e));
						continue;
					}
					this.pagesSuccess.Add(uri);
					
					links = links.ConvertAll<UriLinkTypePair>(CrawlerHelper.ConvertUriWithoutFragment);
					foreach(UriLinkTypePair pair in links) {
						//addMyList 関数は放送予定コンテンツをMyGyaOのリストへの追加にも使われるので無視する
						if (this.settings.IgnoreAddMyListFunction && pair.Uri.AbsoluteUri.StartsWith(@"javascript:addMylist(")) {
							continue;
						}
						//ID抽出
						string id;
						if(GPackage.TryExtractPackageId(pair.Uri, out id)) {
							int key = GPackage.ConvertToKeyFromId(id);
							if(!this.pacsWaiting.Contains(key)) {
								this.pacsWaiting.Enqueue(key);
								continue;
							}
						}
						if(GContent.TryExtractContentId(pair.Uri, out id)) {
							int key = GContent.ConvertToKeyFromId(id);
							if(!this.contsWatings.Contains(key)) {
								this.contsWatings.Enqueue(key);
								continue;
							}
						}
						//IDが取れなかったらクロールキューに追加
						if(this.IsInRestriction(pair.Uri)) {
							switch(pair.LinkType) {
								case LinkType.AnchorOrFrame:
									if (this.pagesWaiting.Contains(pair.Uri)) break;
									if (this.pagesSuccess.Contains(pair.Uri)) break;
									if (this.pagesFailed.Contains(pair.Uri)) break;
									if (uri.Equals(pair.Uri)) break;
									this.pagesWaiting.Enqueue(pair.Uri);
									break;
							}
						}
					}
				}
			}
			/// <summary>
			/// シリーズ一覧ページを読み込んでコンテンツのIDなどを取得する．
			/// </summary>
			private void FetchPackages() {
				while(this.pacsWaiting.Count > 0){
					if (this.bw.CancellationPending) return;

					int packKey = this.pacsWaiting.Dequeue();
					this.ReportProgressToBackgroundWorker(string.Format("フェーズ 2/4: シリーズ一覧ページを取得中 ({0}/{1}) {2}",
						this.pacsSuccess.Count + this.pacsFailed.Count,
						this.pacsSuccess.Count + this.pacsFailed.Count + this.pacsWaiting.Count,
						GPackage.ConvertToIdFromKey(packKey)));
					GPackage package;
					List<int> childContKeys;
					try{
						package = GPackage.DoDownload(packKey, out childContKeys, this.cpPacs);
					}catch(Exception e){
						this.pacsFailed.Add(packKey);
						this.OnIgnoringException(e);
						continue;
					}
					if (this.genre.GenreKey.Equals(package.GenreKey)) {
						this.pacsSuccess.Add(packKey, package);
					} else {
						this.pacsFailed.Add(packKey);
						this.OnIgnoringException(new Exception(string.Format("別ジャンルのため無視 <{0}>", package.PackageId)));
						continue;
					}
					
					foreach (int contKey in childContKeys) {
						if (!this.contsWatings.Contains(contKey)) {
							this.contsWatings.Enqueue(contKey);
						}
						this.contPackRelations.Add(contKey, packKey);
					}
				}
			}
			/// <summary>
			/// コンテンツページを読み込んで情報を取得する．
			/// もしキャッシュがあればキャッシュを利用する．
			/// </summary>
			private void FetchContents() {
				while(this.contsWatings.Count > 0){
					if (this.bw.CancellationPending) return;
					
					int contKey = this.contsWatings.Dequeue();
					ContentCache cache;
					GContent content;
					if (this.cacheController.TryGetCache(contKey, out cache)) {
						content = cache.Content;
						content.FromCache = true;
						
						this.ReportProgressToBackgroundWorker(string.Format("フェーズ 3/4: 詳細ページのキャッシュにヒット ({0}/{1}) {2}",
							this.contsVisited.Count + this.contsIgnored.Count,
							this.contsVisited.Count + this.contsIgnored.Count + this.contsWatings.Count,
							GContent.ConvertToIdFromKey(contKey)));
						if (this.genre.GenreKey.Equals(content.GenreKey)) {
							//ヒット成功
							goto success;
						} else {
							//ヒット成功だけども別ジャンルだった
							this.contsIgnored.Add(contKey);
							this.OnIgnoringException(new Exception(string.Format("別ジャンルのため無視 <{0}>", content.ContentId)));
						}
					} else {
						this.ReportProgressToBackgroundWorker(string.Format("フェーズ 3/4: 詳細ページの取得中 ({0}/{1}) {2}",
							this.contsVisited.Count + this.contsIgnored.Count,
							this.contsVisited.Count + this.contsIgnored.Count + this.contsWatings.Count,
							GContent.ConvertToIdFromKey(contKey)));
						try {
							ContentPropertiesOnPackagePage cpPac;
							if (this.cpPacs.TryGetValue(contKey, out cpPac)) {
								content = GContent.DoDownload(contKey, this.contPackRelations[contKey], cpPac);
							} else {
								content = GContent.DoDownload(contKey);
							}
							if (this.genre.GenreKey.Equals(content.GenreKey)) {
								//ダウンロード成功
								this.cacheController.AddCache(content);
								goto success;
							} else {
								//ダウンロード成功だけども別ジャンルだった
								this.contsIgnored.Add(contKey);
								this.OnIgnoringException(new Exception(string.Format("別ジャンルのため無視 <{0}>", content.ContentId)));
							}
						} catch (ContentDownloadException e) {
							//ダウンロード失敗だけどダミーを作って入れておく
							content = GContent.CreateDummyContent(contKey, this.genre, e.Message);
							this.contsVisited.Add(contKey, content);
							this.OnIgnoringException(e);
						}
					}
					continue;
				success:
					this.contsVisited.Add(contKey, content);
					//取得しているパッケージなら次
					int packKey = content.PackageKey;
					if (0 == packKey || this.pacsSuccess.ContainsKey(packKey) || this.pacsFailed.Contains(packKey)) {
						continue;
					}
					// 取得していないパッケージなら取得する
					// ここから FetchPackages のコピペ改変
					GPackage package;
					List<int> childContKeys;
					try {
						package = GPackage.DoDownload(packKey, out childContKeys, this.cpPacs);
					} catch (Exception e) {
						this.pacsFailed.Add(packKey);
						this.OnIgnoringException(e);
						continue;
					}
					this.pacsSuccess.Add(packKey, package);
					
					foreach (int key in childContKeys) {
						if (contKey == key) {
							ContentPropertiesOnPackagePage cpPac = this.cpPacs[contKey];
							if (!content.HasSameContentPropertiesOnPackagePage(cpPac)) {
								//パッケージページで取れた情報を追加できたのならば追加してキャッシュ更新
								content.SetContentPropertiesOnPackagePage(cpPac);
								this.cacheController.RemoveCache(content.ContentKey);
								this.cacheController.AddCache(content);
							}
						}else if (!this.contsWatings.Contains(key) && !this.contsVisited.ContainsKey(key) && !this.contsIgnored.Contains(key)) {
							//新たに見つかったコンテンツをキューに追加
							this.contsWatings.Enqueue(key);
						}
						this.contPackRelations.Add(key, packKey);
					}
					continue;
				}
			}
			/// <summary>
			/// クロール結果からジャンル，パッケージ，コンテンツの木構造を作る．
			/// </summary>
			private ReadOnlyCollection<GPackage> BuildGpcTree() {
				this.ReportProgressToBackgroundWorker("フェーズ 4/4: データ構造の解析中");
				//パッケージに含まれるコンテンツ
				List<GPackage> packages = new List<GPackage>();
				foreach (KeyValuePair<int, GPackage> packPair in this.pacsSuccess) {
					packages.Add(packPair.Value);
					List<GContent> contents = new List<GContent>();
					foreach (KeyValuePair<int, int> cpPair in this.contPackRelations) {
						if (packPair.Key == cpPair.Value) {
							GContent cont;
							if (this.contsVisited.TryGetValue(cpPair.Key, out cont)) {
								contents.Add(cont);
							} else {
								this.OnIgnoringException(new Exception(string.Format("GPCツリーの作成でパッケージ<{0}>にはコンテンツ<{1}>が含まれているはずなのにコンテンツ<{1}>が取得されていない．", packPair.Key, cpPair.Key)));
							}
						}
					}
					packPair.Value.Contents = contents.AsReadOnly();
				}
				//どのパッケージに含まれているか不明なコンテンツ
				List<GContent> contentsWithoutPack = new List<GContent>();
				foreach (KeyValuePair<int, GContent> contPair in this.contsVisited) {
					if (!this.contPackRelations.ContainsKey(contPair.Key)) {
						contentsWithoutPack.Add(contPair.Value);
					}
				}
				if (contentsWithoutPack.Count > 0) {
					GPackage dummyPackage = GPackage.CreateDummyPackage(this.genre);
					dummyPackage.Contents = contentsWithoutPack.AsReadOnly();
					packages.Add(dummyPackage);
				}
				return packages.AsReadOnly();
			}
			private CrawlResult CreateCrawlResult(ReadOnlyCollection<GPackage> packages) {
				return new CrawlResult(this.genre, packages, this.pagesSuccess.AsReadOnly(), this.ignoredExceptions.AsReadOnly());
			}
			private bool IsInRestriction(Uri uri) {
				return uri.AbsoluteUri.StartsWith(this.genre.RootUri.AbsoluteUri);
			}
		}
		
		private readonly IHtmlParser parser;
		private readonly IContentCacheController cacheController;

		public Crawler(IHtmlParser parser, IContentCacheController cacheController) {
			this.parser = parser;
			this.cacheController = cacheController;
		}
		
		public CrawlResult Crawl(CrawlSettings settings, GGenre genre, BackgroundWorker bw) {
			if (!genre.IsCrawlable) {
				throw new ArgumentException("ジャンル「" + genre.GenreName + "」はクロールできない．");
			}
			
			CrawlerHelper helper = new CrawlerHelper(settings, genre, this.parser, this.cacheController, bw);
			CrawlResult result = helper.StartCrawling();
			return result;
		}
	}

	public enum CrawlOrder {
		TopPageFirst,
		TimetableFirst,
	}
	public enum TimetableSortType {
		RecentlyUpdatedFirst=0,
		DeadlineNearFirst=1,
	}
	public class CrawlSettings {
		private int maxPages;
		private CrawlOrder crawlOrder;
		private TimetableSortType timeTableSortType;
		private bool ignoreAddMyListFunction;

		public CrawlSettings(int maxPages, CrawlOrder crawlOrder, TimetableSortType timeTableSortType, bool ignoreAddMyListFunction) {
			this.maxPages = maxPages;
			this.crawlOrder = crawlOrder;
			this.timeTableSortType = timeTableSortType;
			this.ignoreAddMyListFunction = ignoreAddMyListFunction;
		}
		public int MaxPages {
			get { return this.maxPages; }
		}
		public CrawlOrder CrawlOrder {
			get { return this.crawlOrder; }
		}
		public TimetableSortType TimeTableSortType {
			get { return this.timeTableSortType; }
		}
		public bool IgnoreAddMyListFunction {
			get { return this.ignoreAddMyListFunction; }
		}
	}

	public class CrawlProgressEventArgs : EventArgs {
		private readonly string message;
		private readonly int visited;
		private readonly int waiting;

		internal CrawlProgressEventArgs(string message, int visited, int waiting) {
			this.message = message;
			this.visited = visited;
			this.waiting = waiting;
		}
		public string Message {
			get { return this.message; }
		}
		public int Visited {
			get { return this.visited; }
		}
		public int Waiting {
			get { return this.waiting; }
		}
	}
	
	[Serializable]
	public class CrawlResult {
		public static CrawlResult Merge(GGenre genre, CrawlResult x, CrawlResult y) {
			List<GPackage> packages = new List<GPackage>();
			packages.AddRange(x.Packages);
			packages.AddRange(y.Packages);
			List<Uri> pages = new List<Uri>();
			pages.AddRange(x.VisitedPages);
			pages.AddRange(y.VisitedPages);
			List<Exception> ignoredExceptinos = new List<Exception>();
			ignoredExceptinos.AddRange(x.IgnoredExceptions);
			ignoredExceptinos.AddRange(y.IgnoredExceptions);

			return new CrawlResult(genre, packages.AsReadOnly(), pages.AsReadOnly(), ignoredExceptinos.AsReadOnly());
		}
		public static CrawlResult Merge(GGenre genre, IEnumerable<CrawlResult> results) {
			CrawlResult seed = new CrawlResult(
				genre,
				new ReadOnlyCollection<GPackage>(new List<GPackage>()),
				new ReadOnlyCollection<Uri>(new List<Uri>()),
				new ReadOnlyCollection<Exception>(new List<Exception>()));
			foreach (CrawlResult result in results) {
				seed = CrawlResult.Merge(genre, seed, result);
			}
			return seed;
		}

		private readonly GGenre genre;
		private readonly ReadOnlyCollection<GPackage> packages;
		private readonly ReadOnlyCollection<Uri> visitedPages;
		private readonly ReadOnlyCollection<Exception> ignoredExceptions;
		private readonly DateTime time;

		internal CrawlResult(
				GGenre genre,
				ReadOnlyCollection<GPackage> packages, ReadOnlyCollection<Uri> vPages,
				ReadOnlyCollection<Exception> ignoredExceptions) {
			this.genre = genre;
			this.packages = packages;
			this.visitedPages = vPages;
			this.ignoredExceptions = ignoredExceptions;
			this.time = DateTime.Now;
		}
		
		public GGenre Genre {
			get { return this.genre; }
		}
		public ReadOnlyCollection<GPackage> Packages {
			get { return this.packages; }
		}
		public ReadOnlyCollection<Uri> VisitedPages {
			get {return this.visitedPages;}
		}
		public ReadOnlyCollection<Exception> IgnoredExceptions {
			get { return this.ignoredExceptions; }
		}
		public DateTime Time {
			get { return this.time; }
		}
	}
}
