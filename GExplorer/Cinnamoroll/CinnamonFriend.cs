using System;
using System.Collections.Generic;

namespace Yusen.GExplorer.Cinnamoroll {
	sealed class CinnamonFriend {
		public static readonly CinnamonFriend Cinnamon = new CinnamonFriend("Cinnamon", "シナモン", 3, 6);
		public static readonly CinnamonFriend Cappuccino = new CinnamonFriend("Cappuccino", "カプチーノ", 6, 27);
		public static readonly CinnamonFriend Mocha = new CinnamonFriend("Mocha", "モカ", 2, 20);
		public static readonly CinnamonFriend Chiffon = new CinnamonFriend("Chiffon", "シフォン", 1, 14);
		public static readonly CinnamonFriend Espresso = new CinnamonFriend("Espresso", "エスプレッソ", 12, 4);
		public static readonly CinnamonFriend Milk = new CinnamonFriend("Milk", "みるく", 2, 4);
		
		public static IEnumerable<CinnamonFriend> GetFriendsEnumerable() {
			yield return CinnamonFriend.Cinnamon;
			yield return CinnamonFriend.Cappuccino;
			yield return CinnamonFriend.Mocha;
			yield return CinnamonFriend.Chiffon;
			yield return CinnamonFriend.Espresso;
			yield return CinnamonFriend.Milk;
		}
		
		public static CinnamonFriend WhoseBirthday(int month, int day) {
			foreach (CinnamonFriend friend in CinnamonFriend.GetFriendsEnumerable()) {
				if (friend.BirthMonth == month) {
					if (friend.BirthDay == day) {
						return friend;
					}
				}
			}
			return null;
		}
		
		private readonly string englishName;
		private readonly string japaneseName;
		private readonly int birthMonth;
		private readonly int birthDay;
		
		private CinnamonFriend(string enName, string jaName, int birthMonth, int birthDay) {
			this.englishName = enName;
			this.japaneseName = jaName;
			this.birthMonth = birthMonth;
			this.birthDay = birthDay;
		}
		
		public string EnglishName {
			get { return this.englishName; }
		}
		public string JapaneseName {
			get { return this.japaneseName; }
		}
		public int BirthMonth {
			get { return this.birthMonth; }
		}
		public int BirthDay {
			get { return this.birthDay; }
		}
	}
}

