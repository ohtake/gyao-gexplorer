using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using Yusen.GExplorer.GyaoModel;
using Yusen.GExplorer.Utilities;
using System.ComponentModel;

namespace Yusen.GExplorer.AppCore {
	sealed class CacheController {
		private static readonly Regex regexGName = new Regex(
			@"var menu=new MenuByGName\('(?<GenreRoot>[a-z]+?)'",
			RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
		private static readonly Regex regexPackagePackage = new Regex(
			@"<h1 class=""part01"">「(?<PackageName>.*?)」の表示</h1>[\s\S]{0,500}?<h2>(?<CatchCopy>.*?)</h2>[\s\S]{0,10}?<p class=""part03"">(?<PackageText1>.*?)</p>",
			RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
		private static readonly Regex regexPackageContent = new Regex(
			@"<p class=""part01""><a href=""/sityou/catedetail/contents_id/(?<ContentId>cnt\d{7})/""><img src="".{0,50}?"" alt=""画像"" border=""0"" /></a></p>(?:\r|\n|\r\n)<ul class=""part02""><li class=""sub"">(?<SeriesNumber>.*)&nbsp;&nbsp;(?<Subtitle>.*)</li><li>(?<Summary>.*)</li></ul>[\s\S]{0,1000}?<ul>(?:\r|\n|\r\n)<li><img src=""/common/images/img_clock.gif"" alt=""時間"" width=""13"" height=""13"" />(?<Duration>.*?)</li>(?:\r|\n|\r\n)<li>(?<Deadline>.*?)</li>(?:\r|\n|\r\n)</ul>",
			RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.ExplicitCapture);
		private static readonly Regex regexContentPage = new Regex(
			@"<h1 class=""part01"">(?<Title>.*?)&nbsp;(?:\r|\n|\r\n)(?:(?:(?<SeriesNumber>.*?)　)?(?<Subtitle>.*?))?</h1>(?:\r|\n|\r\n)<ul class=""part02""><li>番組時間(?:（CM除く）)?：</li><li>(?<Duration>.*?)</li></ul>(?:[\s\S]{0,6000}?<p class=""part02"">放送期間：(?<Deadline>.*?)</p>)?",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline);
		private static readonly Regex regexInputPacId = new Regex(
			@"<a href=""http://www\.gyao\.jp/sityou_review/review_list\.php\?contents_id=cnt\d{7}&pac_id=(?<PackageId>pac\d{7})"">",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Singleline);

		private readonly string cacheDirectory;
		private readonly Encoding encoding = Encoding.GetEncoding("Shift_JIS");
		
		private readonly GDataSet dataSet = new GDataSet();
		private readonly List<GGenreClass> allGenres = new List<GGenreClass>();
		private readonly SortedDictionary<int, GGenreClass> dicGenreByKey = new SortedDictionary<int, GGenreClass>();
		private readonly SortedDictionary<string, GGenreClass> dicGenreByRoot = new SortedDictionary<string, GGenreClass>();
		private readonly CookieContainer cookieContainer;

		private readonly CacheControllerOptions options;

		private CacheController() {
		}
		public CacheController(string cacheDirectory, CookieContainer cookieContainer, CacheControllerOptions options) : this(){
			this.cacheDirectory = cacheDirectory;
			this.cookieContainer = cookieContainer;
			this.options = options;
		}
		public IEnumerable<GGenreClass> GetEnumerableOfAllGenres() {
			return this.allGenres;
		}
		private GGenreClass GetCachedGenre(int genreKey) {
			GGenreClass ret;
			if(this.dicGenreByKey.TryGetValue(genreKey, out ret)){
				return ret;
			}else{
				return null;
			}
		}
		private GGenreClass GetCachedGenre(string rootDirName) {
			GGenreClass ret;
			if (this.dicGenreByRoot.TryGetValue(rootDirName, out ret)) {
				return ret;
			} else {
				return null;
			}
		}
		private GGenreClass GetCachedGenre(GDataSet.GPackageRow prow) {
			if (prow.IsGenreKeyNull()) return null;
			else return this.GetCachedGenre(prow.GenreKey);
		}
		private GGenreClass GetCachedGenre(GDataSet.GContentRow crow) {
			if (crow.IsGenreKeyNull()) return null;
			else return this.GetCachedGenre(crow.GenreKey);
		}
		public GDataSet GDataSet {
			get { return this.dataSet; }
		}

		private string GenreFullFilename {
			get { return Path.Combine(this.cacheDirectory, "DataTable_21_Genre.xml"); }
		}
		private string PackageFullFilename {
			get { return Path.Combine(this.cacheDirectory, "DataTable_21_Package.xml"); }
		}
		private string ContentFullFilename {
			get { return Path.Combine(this.cacheDirectory, "DataTable_21_Content.xml"); }
		}
		private string GetResultFullFilenameOf(GGenreClass genre) {
			return Path.Combine(this.cacheDirectory, string.Format("CrawlResult_21_{0}_{1}.bin", genre.GenreId, genre.RootDirectory));
		}
		private void CreateGenreClasses() {
			this.allGenres.Clear();
			this.dicGenreByKey.Clear();
			this.dicGenreByRoot.Clear();
			foreach (GDataSet.GGenreRow grow in this.dataSet.GGenre) {
				GGenreClass genre = new GGenreClass(grow);
				this.allGenres.Add(genre);
				this.dicGenreByKey.Add(genre.GenreKey, genre);
				this.dicGenreByRoot.Add(genre.RootDirectory, genre);
			}
		}
		public void ResetToDefaultGenres() {
			this.dataSet.GGenre.ResetToDefaultGenres();
			this.CreateGenreClasses();
		}
		public void DeserializeGenreTable() {
			if (File.Exists(this.GenreFullFilename)) this.dataSet.GGenre.ReadXml(this.GenreFullFilename);
			this.CreateGenreClasses();
		}
		public void DeserializePackageAndContentTables() {
			if (File.Exists(this.PackageFullFilename)) this.dataSet.GPackage.ReadXml(this.PackageFullFilename);
			if (File.Exists(this.ContentFullFilename)) this.dataSet.GContent.ReadXml(this.ContentFullFilename);
		}
		public void SerializeTables() {
			this.dataSet.GGenre.WriteXml(this.GenreFullFilename);
			this.dataSet.GPackage.WriteXml(this.PackageFullFilename);
			this.dataSet.GContent.WriteXml(this.ContentFullFilename);
		}
		public void SerializeCrawlResult(GGenreClass genre, CrawlResult result) {
			using (FileStream fs = new FileStream(this.GetResultFullFilenameOf(genre), FileMode.Create)) {
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(fs, result);
			}
		}
		public bool TryDeserializeCrawlResult(GGenreClass genre, out CrawlResult result) {
			try {
				using (FileStream fs = new FileStream(this.GetResultFullFilenameOf(genre), FileMode.Open)) {
					BinaryFormatter formatter = new BinaryFormatter();
					result = (CrawlResult)formatter.Deserialize(fs);
					return true;
				}
			} catch {
				result = null;
				return false;
			}
		}
		
		public bool TryFindContent(int contKey, out GContentClass content) {
			GDataSet.GContentRow row = this.dataSet.GContent.FindByContentKey(contKey);
			if (null != row) {
				content = new GContentClass(row, this.FindPackage(row), this.GetCachedGenre(row));
				return true;
			} else {
				content = null;
				return false;
			}
		}
		public bool TryFindPackage(int pacKey, out GPackageClass package) {
			GDataSet.GPackageRow row = this.dataSet.GPackage.FindByPackageKey(pacKey);
			if (null != row) {
				package = new GPackageClass(row, this.GetCachedGenre(row));
				return true;
			} else {
				package = null;
				return false;
			}
		}
		private GContentClass FetchContent(int contKey) {
			Uri uri = GUriBuilder.CreateContentDetailUri(contKey);
			TextReader reader = TextReader.Null;
			try {
				HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
				req.CookieContainer = this.cookieContainer;
				req.Timeout = this.options.Timeout;
				
				reader = new StreamReader(req.GetResponse().GetResponseStream(), this.encoding);
				string allHtml = reader.ReadToEnd();
				
				int? age = AdultUtility.FindAdultThresholdInContent(allHtml);
				if (age.HasValue) {
					req = AdultUtility.CreateAdultContentRequest(uri, this.cookieContainer, this.options.Timeout);
					reader.Dispose();
					reader = new StreamReader(req.GetResponse().GetResponseStream(), this.encoding);
					allHtml = reader.ReadToEnd();
				}

				int? genreKey = null;
				string packageId = null;
				string title;
				string seriesNumber;
				string subtitle;
				string durationText;
				string deadlineText;
				
				Match match =CacheController.regexContentPage.Match(allHtml);
				if (match.Success) {
					Match matchGenreRoot = CacheController.regexGName.Match(allHtml);
					if (matchGenreRoot.Success) {
						genreKey = this.GetCachedGenre(matchGenreRoot.Groups["GenreRoot"].Value).GenreKey;
					}
					Match matchPacId = CacheController.regexInputPacId.Match(allHtml);
					if (matchPacId.Success) {
						packageId = matchPacId.Groups["PackageId"].Value;
					}
					title = HtmlUtility.HtmlToText(match.Groups["Title"].Value);
					seriesNumber = HtmlUtility.HtmlToText(match.Groups["SeriesNumber"].Value);
					subtitle = HtmlUtility.HtmlToText(match.Groups["Subtitle"].Value);
					durationText = match.Groups["Duration"].Value;
					deadlineText = match.Groups["Deadline"].Value;
				} else {
					throw new Exception(string.Format("詳細ページの解釈に失敗．{0}", GConvert.ToContentId(contKey)));
				}

				GDataSet.GContentRow row = this.dataSet.GContent.NewGContentRow();
				row.ContentKey = contKey;
				if (!string.IsNullOrEmpty(packageId)) row.PackageKey = GConvert.ToPackageKey(packageId);
				if (genreKey.HasValue) row.GenreKey = genreKey.Value;
				row.Title = title;
				row.SeriesNumber = seriesNumber;
				row.Subtitle = subtitle;
				row.SummaryHtml = null;
				row.DurationValue = GConvert.ToTimeSpan(durationText);
				row.DeadlineText = deadlineText;
				DateTime now = DateTime.Now;
				row.Created = now;
				row.LastModified = now;
				
				this.dataSet.GContent.AddGContentRow(row);
				return new GContentClass(row, this.FindPackage(row), this.GetCachedGenre(row));
			} finally {
				reader.Dispose();
			}
		}
		public GContentClass FindContentOrFetchContent(int contKey) {
			GContentClass cont;
			if (this.TryFindContent(contKey, out cont)) {
				return cont;
			} else {
				return this.FetchContent(contKey);
			}
		}

		private GPackageClass FindPackage(int pacKey) {
			GDataSet.GPackageRow row = this.dataSet.GPackage.FindByPackageKey(pacKey);
			if (null != row) {
				return new GPackageClass(row, this.GetCachedGenre(row));
			} else {
				return null;
			}
		}
		private GPackageClass FindPackage(GDataSet.GContentRow crow) {
			if (crow.IsPackageKeyNull()) return null;
			else return this.FindPackage(crow.PackageKey);
		}
		public GPackageClass FetchPackage(int pacKey, out List<GContentClass> contents) {
			Uri pacUri = GUriBuilder.CreatePackagePageUri(pacKey);
			List<GContentClass> children = new List<GContentClass>();
			TextReader reader = TextReader.Null;
			try{
				HttpWebRequest req = WebRequest.Create(pacUri) as HttpWebRequest;
				req.CookieContainer = this.cookieContainer;
				req.Timeout = this.options.Timeout;
				
				reader = new StreamReader(req.GetResponse().GetResponseStream(), this.encoding);
				string allHtml = reader.ReadToEnd();

				int? genreKey = null;
				GGenreClass genre = null;
				string packageName;
				string packageCatch;
				string packageText;

				Match matchPackage = CacheController.regexPackagePackage.Match(allHtml);
				if (matchPackage.Success) {
					Match matchGenreRoot = CacheController.regexGName.Match(allHtml);
					if (matchGenreRoot.Success) {
						genre = this.GetCachedGenre(matchGenreRoot.Groups["GenreRoot"].Value);
						genreKey = genre.GenreKey;
					}
					packageName = HtmlUtility.HtmlToText(matchPackage.Groups["PackageName"].Value);
					packageCatch = HtmlUtility.HtmlToText(matchPackage.Groups["CatchCopy"].Value);
					packageText = HtmlUtility.HtmlToText(matchPackage.Groups["PackageText1"].Value);
				} else {
					throw new Exception("パッケージの情報を取れなかった．");
				}
				
				GPackageClass package = this.CreatePackageAndStoreOrUpdate(pacKey, genre, packageName, packageCatch, packageText);
				for (Match matchContent = CacheController.regexPackageContent.Match(allHtml); matchContent.Success; matchContent = matchContent.NextMatch()) {
					string id = matchContent.Groups["ContentId"].Value;
					string seriesNumber = HtmlUtility.HtmlToText(matchContent.Groups["SeriesNumber"].Value);
					string subtitle = HtmlUtility.HtmlToText(matchContent.Groups["Subtitle"].Value);
					TimeSpan durationValue = GConvert.ToTimeSpan(matchContent.Groups["Duration"].Value);
					string summaryHtml = matchContent.Groups["Summary"].Value;
					string deadlineText = HtmlUtility.HtmlToText(matchContent.Groups["Deadline"].Value);

					children.Add(this.CreateContentAndStoreOrUpdate(GConvert.ToContentKey(id), package, genre, packageName, seriesNumber, subtitle, summaryHtml, durationValue, deadlineText));
				}

				if (children.Count < 1) {
					throw new Exception("シリーズ一覧ページからコンテンツをひとつも取得できなかった．");
				}
				
				contents = children;
				return package;
			}finally{
				reader.Close();
			}
		}

		private GPackageClass CreatePackageAndStoreOrUpdate(int packageKey, GGenreClass genre, string pacTitle, string pacCatch, string pacText) {
			int? genreKey = (null != genre) ? genre.GenreKey : (int?)null;
			GDataSet.GPackageRow row = this.dataSet.GPackage.FindByPackageKey(packageKey);
			if (null != row) {
				bool updateFlag = false;
				if (genreKey.HasValue && (row.IsGenreKeyNull() || genreKey.Value != row.GenreKey)) {
					row.GenreKey = genreKey.Value;
					updateFlag = true;
				}
				if (row.IsPackageTitleNull() || !row.PackageTitle.Equals(pacTitle)) {
					row.PackageTitle = pacTitle;
					updateFlag = true;
				}
				if (row.IsPackageCatchNull() || !row.PackageCatch.Equals(pacCatch)) {
					row.PackageCatch = pacCatch;
					updateFlag = true;
				}
				if (row.IsPackageTextNull() || !row.PackageText.Equals(pacText)) {
					row.PackageText = pacText;
					updateFlag = true;
				}
				if (updateFlag) {
					row.LastModified = DateTime.Now;
				}
				return new GPackageClass(row, genre);
			} else {
				row = this.dataSet.GPackage.NewGPackageRow();
				row.PackageKey = packageKey;
				if(genreKey.HasValue) row.GenreKey = genreKey.Value;
				row.PackageTitle = pacTitle;
				row.PackageCatch = pacCatch;
				row.PackageText = pacText;
				DateTime now = DateTime.Now;
				row.Created = now;
				row.LastModified = now;
				
				this.dataSet.GPackage.AddGPackageRow(row);
				return new GPackageClass(row, genre);
			}
		}

		private GContentClass CreateContentAndStoreOrUpdate(int contKey, GPackageClass package, GGenreClass genre, string title, string seriesNumber, string subtitle, string summaryHtml, TimeSpan durationValue, string deadlineText) {
			int? pacKey = (null != package) ? package.PackageKey : (int?)null;
			int? genreKey = (null != genre) ? genre.GenreKey : (int?)null;
			GDataSet.GContentRow row = this.dataSet.GContent.FindByContentKey(contKey);
			if (null != row) {
				bool updateFlag = false;
				if (pacKey.HasValue && (row.IsPackageKeyNull() || pacKey.Value != row.PackageKey)) {
					row.PackageKey = pacKey.Value;
					updateFlag = true;
				}
				if (genreKey.HasValue && (row.IsGenreKeyNull() || genreKey.Value != row.GenreKey)) {
					row.GenreKey = genreKey.Value;
					updateFlag = true;
				}
				if (row.IsTitleNull() || !row.Title.Equals(title)) {
					row.Title = title;
					updateFlag = true;
				}
				if (row.IsSeriesNumberNull() || !row.SeriesNumber.Equals(seriesNumber)) {
					row.SeriesNumber = seriesNumber;
					updateFlag = true;
				}
				if (row.IsSubtitleNull() || !row.Subtitle.Equals(subtitle)) {
					row.Subtitle = subtitle;
					updateFlag = true;
				}
				if (row.IsSummaryHtmlNull() || !row.SummaryHtml.Equals(summaryHtml)) {
					row.SummaryHtml = summaryHtml;
					updateFlag = true;
				}
				if (row.IsDurationValueNull() || !row.DurationValue.Equals(durationValue)) {
					row.DurationValue = durationValue;
					updateFlag = true;
				}
				if (row.IsDeadlineTextNull() || !row.DeadlineText.Equals(deadlineText)) {
					row.DeadlineText = deadlineText;
					updateFlag = true;
				}
				
				if (updateFlag) {
					row.LastModified = DateTime.Now;
				}
				return new GContentClass(row, package, genre);
			} else {
				row = this.dataSet.GContent.NewGContentRow();
				row.ContentKey = contKey;
				if(pacKey.HasValue) row.PackageKey = pacKey.Value;
				if(genreKey.HasValue) row.GenreKey = genreKey.Value;
				row.Title = title;
				row.SeriesNumber = seriesNumber;
				row.Subtitle = subtitle;
				row.SummaryHtml = summaryHtml;
				row.DurationValue = durationValue;
				row.DeadlineText = deadlineText;
				DateTime now = DateTime.Now;
				row.Created = now;
				row.LastModified = now;
				
				this.dataSet.GContent.AddGContentRow(row);
				return new GContentClass(row, package, genre);
			}
		}
	}
	
	public sealed class CacheControllerOptions {
		public CacheControllerOptions() {
		}
		
		private int timeout = 8000;
		[Category("通信")]
		[DisplayName("タイムアウト")]
		[Description("シリーズ一覧ページと詳細ページを取得するときのタイムアウトをミリ秒で指定します．")]
		[DefaultValue(8000)]
		public int Timeout {
			get { return this.timeout; }
			set { this.timeout = value; }
		}
	}
}
