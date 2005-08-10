using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Yusen.GCrawler {
	public class Crawler {
		private class CrawlerHelper {
			private static Uri ConvertUriWithoutFragment(Uri uri) {
				if (uri.Fragment.Length > 0) {
					return new Uri(uri.GetLeftPart(UriPartial.Query));
				} else {
					return uri;
				}
			}
			
			public event EventHandler<CrawlProgressEventArgs> CrawlProgress;
			public event EventHandler<IgnorableErrorOccuredEventArgs> IgnorableErroeOcured;
			
			private GGenre genre;
			private IContentCacheController cacheController;
			private IHtmlParser parser;
			private Stack<Uri> waitingPages = new Stack<Uri>();
			private Queue<string> waitingPackages = new Queue<string>();
			private Queue<string> waitingContents = new Queue<string>();
			private List<Uri> visitedPages = new List<Uri>();
			private Dictionary<string, GPackage> visitedPackages = new Dictionary<string, GPackage>();
			private Dictionary<string, GContent> visitedContents = new Dictionary<string, GContent>();
			private Dictionary<string, bool> contentsCached = new Dictionary<string, bool>();
			private Dictionary<string, string> contPackRelations = new Dictionary<string, string>();

			public CrawlerHelper(GGenre genre, IHtmlParser parser, IContentCacheController cacheController) {
				this.genre = genre;
				this.parser = parser;
				this.cacheController = cacheController;
				this.waitingPages.Push(this.StartPageUri);
			}
			
			public CrawlResult StartCrawling() {
				if (this.TryCrawlPages() && this.TryFetchPackages() && this.TryFetchContents()){
					this.BuildGpcTree();
					return this.CreateCrawlResult();
				}
				return new CrawlResult(this.genre);
			}
			private void OnCrawlProgress(string message) {
				if (null != this.CrawlProgress) {
					this.CrawlProgress(this, new CrawlProgressEventArgs(
						message,
						this.visitedPages.Count + this.visitedPackages.Count + this.visitedContents.Count,
						this.waitingPages.Count + this.waitingPackages.Count + this.waitingContents.Count));
				}
			}
			private bool OnIgnorableErrorOccured(string message) {
				if (null != this.IgnorableErroeOcured) {
					IgnorableErrorOccuredEventArgs args = new IgnorableErrorOccuredEventArgs(message);
					this.IgnorableErroeOcured(this, args);
					return args.Ignore;
				}
				return true;
			}
			/// <summary>
			/// パッケージとコンテンツ以外のページをクロールし，
			/// パッケージとコンテンツのIDを取得する．
			/// </summary>
			private bool TryCrawlPages() {
				while (this.waitingPages.Count > 0) {
					this.OnCrawlProgress("一般ページを取得中 " + this.waitingPages.Peek().AbsolutePath);
					
					Uri uri = this.waitingPages.Pop();
					this.visitedPages.Add(uri);
					List<Uri> links, images;
					if (!this.parser.TryExtractLinks(uri, out links, out images)) {
						if (this.OnIgnorableErrorOccured("一般ページの取得失敗．" + uri.AbsoluteUri)) {
							continue;
						} else {
							return false;
						}
					}
					
					links = links.ConvertAll<Uri>(CrawlerHelper.ConvertUriWithoutFragment);
					links.RemoveAll(this.waitingPages.Contains);
					links.RemoveAll(this.visitedPages.Contains);
					
					List<Uri> packageUris = links.FindAll(GPackage.CanExtractPackageId);
					links.RemoveAll(GPackage.CanExtractPackageId);
					packageUris.AddRange(images.FindAll(GPackage.CanExtractPackageId));
					images.RemoveAll(GPackage.CanExtractPackageId);
					
					List<Uri> contentUris = links.FindAll(GContent.CanExtractContentId);
					links.RemoveAll(GContent.CanExtractContentId);
					contentUris.AddRange(images.FindAll(GContent.CanExtractContentId));
					images.RemoveAll(GContent.CanExtractContentId);
					
					links = links.FindAll(this.IsInRestriction);
					links.Reverse();
					foreach (Uri link in links) {
						if(! this.waitingPages.Contains(link)) this.waitingPages.Push(link);
					}
					
					List<string> packageIds = packageUris.ConvertAll<string>(GPackage.ExtractPackageId);
					packageIds.RemoveAll(this.waitingPackages.Contains);
					foreach(string packageId in packageIds){
						if(!this.waitingPackages.Contains(packageId)) this.waitingPackages.Enqueue(packageId);
					}
					
					List<string> contentIds = contentUris.ConvertAll<string>(GContent.ExtractContentId);
					contentIds.RemoveAll(this.waitingContents.Contains);
					foreach (string contentId in contentIds) {
						if(! this.waitingContents.Contains(contentId)) this.waitingContents.Enqueue(contentId);
					}
				}
				return true;
			}
			/// <summary>
			/// パッケージページを読み込んでコンテンツのIDを取得する．
			/// </summary>
			private bool TryFetchPackages() {
				while(this.waitingPackages.Count > 0){
					string packId = this.waitingPackages.Dequeue();
					this.OnCrawlProgress("パッケージページを取得中 " + packId);
					GPackage package;
					List<string> childContIds;
					if (!GPackage.TryDownload(packId, out package, out childContIds)) {
						if (this.OnIgnorableErrorOccured("パッケージページの取得ミスもしくは解析ミス．" + packId)) {
							continue;
						} else {
							return false;
						}
					}
					
					this.visitedPackages.Add(packId, package);
					foreach (string contId in childContIds) {
						if (!this.waitingContents.Contains(contId)) {
							this.waitingContents.Enqueue(contId);
						}
						this.contPackRelations.Add(contId, packId);
					}
				}
				return true;
			}
			/// <summary>
			/// コンテンツページを読み込んで情報を取得する．
			/// もしキャッシュがあればキャッシュを利用する．
			/// </summary>
			private bool TryFetchContents() {
				while(this.waitingContents.Count > 0){
					string contId = this.waitingContents.Dequeue();
					GContent content;
					if (this.cacheController.TryGetCache(contId, out content)) {
						this.OnCrawlProgress("キャッシュにヒット " + contId);
						this.visitedContents.Add(contId, content);
						this.contentsCached.Add(contId, true);
						continue;
					}
					this.OnCrawlProgress("詳細ページの取得中 " + contId);
					if (GContent.TryDownload(contId, out content)) {
						//ダウンロード成功
						this.visitedContents.Add(contId, content);
						this.contentsCached.Add(contId, false);
						this.cacheController.AddToCache(content);
						continue;
					}
					//ダウンロード失敗
					if (this.OnIgnorableErrorOccured("詳細ページの取得ミスまたは解析ミス．" + contId)) {
						content = GContent.CreateDummyContent(contId);
						this.visitedContents.Add(contId, content);
						this.contentsCached.Add(contId, false);
						continue;
					} else {
						return false;
					}
				}
				return true;
			}
			/// <summary>
			/// クロール結果からジャンル，パッケージ，コンテンツの木構造を作る．
			/// </summary>
			private void BuildGpcTree() {
				this.OnCrawlProgress("データ構造の解析中");
				//パッケージに含まれるコンテンツ
				List<GPackage> packages = new List<GPackage>();
				foreach (KeyValuePair<string, GPackage> packPair in this.visitedPackages) {
					packages.Add(packPair.Value);
					List<GContent> contents = new List<GContent>();
					foreach (KeyValuePair<string, string> cpPair in this.contPackRelations) {
						if (packPair.Key == cpPair.Value) {
							contents.Add(this.visitedContents[cpPair.Key]);
						}
					}
					packPair.Value.Contents = contents.AsReadOnly();
				}
				//どのパッケージに含まれているか不明なコンテンツ
				GPackage dummyPackage = GPackage.CreateDummyPackage();
				List<GContent> contentsWithoutPack = new List<GContent>();
				foreach (KeyValuePair<string, GContent> contPair in this.visitedContents) {
					if (!this.contPackRelations.ContainsKey(contPair.Key)) {
						contentsWithoutPack.Add(contPair.Value);
					}
				}
				if (contentsWithoutPack.Count > 0) {
					dummyPackage.Contents = contentsWithoutPack.AsReadOnly();
					packages.Add(dummyPackage);
				}
				this.genre.Packages = packages.AsReadOnly();
			}
			private CrawlResult CreateCrawlResult() {
				int cDown = 0;
				int cCache = 0;
				foreach (KeyValuePair<string, bool> pair in this.contentsCached) {
					if (pair.Value) {
						cCache++;
					} else {
						cDown++;
					}
				}
				return new CrawlResult(this.genre, this.visitedPages.AsReadOnly(), cDown, cCache);
			}
			private bool IsInRestriction(Uri uri) {
				return uri.AbsoluteUri.StartsWith(this.genre.RootUri.AbsoluteUri);
			}
			private Uri StartPageUri {
				get {return this.genre.TopPageUri;}
			}
		}
		
		public event EventHandler<CrawlProgressEventArgs> CrawlProgress;
		public event EventHandler<IgnorableErrorOccuredEventArgs> IgnorableErrorOccured;
		private readonly IHtmlParser parser;
		private readonly IContentCacheController cacheController;

		public Crawler(IHtmlParser parser, IContentCacheController cacheController) {
			this.parser = parser;
			this.cacheController = cacheController;
		}
		
		public CrawlResult Crawl(GGenre genre) {
			CrawlerHelper helper = new CrawlerHelper(genre, this.parser, this.cacheController);
			helper.CrawlProgress += delegate(object sender, CrawlProgressEventArgs e) {
				if (null != this.CrawlProgress) {
					this.CrawlProgress(sender, e);
				}
			};
			helper.IgnorableErroeOcured += delegate(object sender, IgnorableErrorOccuredEventArgs e) {
				if (null != this.IgnorableErrorOccured) {
					this.IgnorableErrorOccured(sender, e);
				}
			};
			return helper.StartCrawling();
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
	public class IgnorableErrorOccuredEventArgs : EventArgs {
		private bool ignore = true;
		private string message;
		internal IgnorableErrorOccuredEventArgs(string message){
			this.message = message;
		}
		public string Message {
			get { return this.message; }
		}
		public bool Ignore {
			get { return this.ignore; }
			set { this.ignore = value; }
		}
	}
	public class CrawlResult {
		private readonly bool success;
		private readonly GGenre genre;
		private readonly ReadOnlyCollection<Uri> visitedPages;
		private readonly int contentsDownloaded;
		private readonly int contentsCached;
		
		/// <summary>
		/// クロール成功時のクロール結果
		/// </summary>
		/// <param name="genre"></param>
		/// <param name="vPages"></param>
		/// <param name="cDown"></param>
		/// <param name="cCache"></param>
		internal CrawlResult(GGenre genre, ReadOnlyCollection<Uri> vPages, int cDown, int cCache) {
			this.success = true;
			this.genre = genre;
			this.visitedPages = vPages;
			this.contentsDownloaded = cDown;
			this.contentsCached = cCache;
		}
		/// <summary>
		/// クロール失敗時のクロール結果
		/// </summary>
		/// <param name="genre"></param>
		internal CrawlResult(GGenre genre) {
			this.success = false;
			this.genre = genre;
			this.visitedPages = new ReadOnlyCollection<Uri>(new List<Uri>());
			this.contentsDownloaded = 0;
			this.contentsCached = 0;
		}
		
		public GGenre Genre {
			get { return this.genre; }
		}
		public ReadOnlyCollection<Uri> VisitedPages {
			get {return this.visitedPages;}
		}
		public int ContentsDownloaded {
			get { return this.contentsDownloaded; }
		}
		public int ContentsCached {
			get { return this.contentsCached; }
		}
		public bool Success {
			get { return this.success; }
		}
	}
}
