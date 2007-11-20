using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace Yusen.GExplorer.AppCore {
	static class AdultUtility {
		public static readonly string AdultAnswerBody = "adult_ans=1\r\n";
		private static readonly Regex regexAge = new Regex(
			@"<img src=""/common/img/r(?<Age>\d+)_r4",
			RegexOptions.Compiled | RegexOptions.ExplicitCapture);
		
		public static int? FindAdultThresholdInContent(string contentHtml) {
			Match match = AdultUtility.regexAge.Match(contentHtml);
			if (match.Success) {
				return int.Parse(match.Groups["Age"].Value);
			} else {
				return null;
			}
		}

		public static HttpWebRequest CreateAdultContentRequest(Uri contentUri) {
			HttpWebRequest req = WebRequest.Create(contentUri) as HttpWebRequest;
			req.Method = "POST";
			req.ContentType = "application/x-www-form-urlencoded";

			byte[] postData = Encoding.ASCII.GetBytes(AdultUtility.AdultAnswerBody);
			req.ContentLength = postData.Length;
			using (Stream reqStream = req.GetRequestStream()){
				reqStream.Write(postData, 0, postData.Length);
			}
			return req;
		}
		
		public static HttpWebRequest CreateAdultContentRequest(Uri contentUri, CookieContainer cc, int timeout) {
			HttpWebRequest req = AdultUtility.CreateAdultContentRequest(contentUri);
			req.CookieContainer = cc;
			req.Timeout = timeout;
			return req;
		}
		
		
	}
}
