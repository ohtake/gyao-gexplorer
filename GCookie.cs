using System;
using System.Text.RegularExpressions;

namespace Yusen.GExplorer {
	static class GCookie{
		private static readonly Regex regexUserNo = new Regex(@"Cookie_UserId *= *([0-9]+)", RegexOptions.Singleline);
		
		private static int userNo = 0;
		private static string cookie = null;
		
		public static bool HasCookie {
			get {
				return null != GCookie.cookie;
			}
		}
		
		public static bool IsRegistered {
			get {
				if(! GCookie.HasCookie) throw new InvalidOperationException();
				return (0 != userNo);
			}
		}
		public static int UserNo {
			get {
				if(!GCookie.IsRegistered) throw new InvalidOperationException();
				return GCookie.userNo;
			}
		}
		public static string Cookie {
			get {
				if(! GCookie.HasCookie) throw new InvalidOperationException();
				return GCookie.cookie;
			}
			set {
				if(null == value) throw new ArgumentNullException();
				if(GCookie.HasCookie) throw new InvalidOperationException();
				GCookie.cookie = value;
				
				Match match = GCookie.regexUserNo.Match(value);
				if(match.Success) {
					GCookie.userNo = int.Parse(match.Groups[1].Value);
				}
			}
		}
	}
}
