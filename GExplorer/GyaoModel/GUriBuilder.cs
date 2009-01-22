using System;
using System.Collections.Generic;
using System.Text;

namespace Yusen.GExplorer.GyaoModel {
	public static class GUriBuilder {
		public static readonly Uri TopPageUri = new Uri("http://www.gyao.jp/");

		public static Uri CreateGenreToppageUri(string rootDirectory) {
			return new Uri("http://www.gyao.jp/" + rootDirectory + "/");
		}

		public static Uri CreatePackagePageUri(string packageId) {
			return new Uri("http://www.gyao.jp/sityou/catelist/pac_id/" + packageId + "/");
		}
		public static Uri CreatePackagePageUri(int packageKey) {
			return GUriBuilder.CreatePackagePageUri(GConvert.ToPackageId(packageKey));
		}
		public static Uri CreatePackageImageMiddleUri(string packageId, string imageDir) {
			return new Uri(string.Format("http://www.gyao.jp/img/info/{0}/{1}_m.jpg", imageDir, packageId));
		}


		public static Uri CreateContentDetailUri(string contId) {
			return new Uri("http://www.gyao.jp/sityou/catedetail/contents_id/" + contId + "/");
		}
		public static Uri CreateContentDetailUri(int contKey) {
			return GUriBuilder.CreateContentDetailUri(GConvert.ToContentId(contKey));
		}
		public static Uri CreateContentImageUri(string contId, string imageDir, char imageSizePostfix) {
			return new Uri("http://www.gyao.jp/img/info/" + imageDir + "/" + contId + "_" + imageSizePostfix + ".jpg");
		}
		public static Uri CreatePlayerUri(string contId, GBitrate bitrate) {
			return new Uri(
				"http://www.gyao.jp/login/judge_cookie/?"
				+ "login_from=shityou"
				+ "&contentsId=" + contId
				+ "&rateId=" + GConvert.ToBitrateId(bitrate)
				+ "&chapterNo="
				+ "&recommend="
				+ "&contents_id="
				+ "&code=");
		}
		public static Uri CreateRecommendationUri(string contId, GBitrate bitrate) {
			return new Uri(
				"http://www.gyao.jp/sityou/catedetail/?"
				+ "login_from=shityou"
				+ "&contentsId=" + contId
				+ "&rateId=" + GConvert.ToBitrateId(bitrate)
				+ "&chapterNo="
				+ "&recommend=1"
				+ "&contents_id=" + contId
				+ "&code=");
		}
		public static Uri CreateReviewListUri(string contId, string pacId) {
			return new Uri(
				"http://www.gyao.jp/sityou_review/review_list.php?"
				+ "contents_id=" + contId
				+ "&pac_id=" + pacId);
		}
		public static Uri CreateReviewListUri(string contId, string pacId, int startCount) {
			return new Uri(
				"http://www.gyao.jp/sityou_review/review_list.php?"
				+ "contents_id=" + contId
				+ "&pac_id=" + pacId
				+ "&start=" + startCount.ToString());
		}
		public static Uri CreateReviewInputUri(string contId, string pacId) {
			return new Uri(
				"http://www.gyao.jp/sityou_review/review_input.php?"
				+ "fromScene=reviewlist"
				+ "&toScene=reviewinput"
				+ "&contentsId=" + contId
				+ "&pac_id=" + pacId);
		}
		
		public static Uri CreateLivedoorVideoGyaoSearchUri(string query) {
			Uri uri = new Uri(string.Format(
				"http://stream.search.livedoor.com/search/?svc=gyao&num=100&q={0}",
				query));
			return uri;
		}
	}
}
