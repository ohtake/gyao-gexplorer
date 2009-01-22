using System;

namespace Yusen.GExplorer.GyaoModel {
	partial class GDataSet {
		partial class GGenreDataTable {
			public void ResetToDefaultGenres() {
				this.Clear();
				
				this.AddGGenreRow(1, "映画", "cinema", "cinema", 0xCC, 0x00, 0x00);
				this.AddGGenreRow( 2, "ドラマ", "drama", "dorama", 0xFF, 0x99, 0x66);
				this.AddGGenreRow( 3, "音楽", "music", "music", 0x99, 0x33, 0xCC);
				this.AddGGenreRow( 6, "アニメ", "anime", "anime", 0x00, 0x99, 0xFF);
				this.AddGGenreRow(25, "ゲーム", "game", "game", 0x00, 0x99, 0x66);
				this.AddGGenreRow( 5, "バラエティ", "variety", "variety", 0x66, 0xCC, 0x33);
				this.AddGGenreRow(29, "ジョッキー", "jockey", "jockey", 0x66, 0x00, 0xCC);
				this.AddGGenreRow( 9, "スポーツ", "sports", "sports", 0x00, 0x66, 0xFF);
				this.AddGGenreRow( 4, "女優・アイドル", "idol", "idol", 0xFF, 0x66, 0xCC);
				//this.AddGGenreRow(31, "歌える♪カラオケ", "karaoke", "karaoke", 0xF0, 0x82, 0x00);
				
				// 画像置き場？
				this.AddGGenreRow(20, "カルチャー", "life", "culture", 0x00, 0x00, 0x00);
				this.AddGGenreRow(21, "美容／ビューティー", "beauty", "health", 0xFF, 0x99, 0xCC);
				this.AddGGenreRow(22, "ニュース・ビジネス", "news", "newsbiz", 0x00, 0x33, 0x99);
				
				// ミックス (リストあり、画像あり)
				this.AddGGenreRow(15, "トレンド", "trend", "trend", 0xFF, 0x99, 0x33);
				// ミックス (リストあり、画像なし)
				this.AddGGenreRow(10001, "アジア", "asia", "dummy-asia", 0x00, 0x00, 0x00);
				// ミックス (リストなし、画像なし)
				this.AddGGenreRow(10002, "恋ストーリー", "lovestory", "dummy-lovestory", 0x00, 0x00, 0x00);
				this.AddGGenreRow(10003, "昭和TV", "showa", "dummy-showa", 0x00, 0x00, 0x00);
				this.AddGGenreRow(10004, "ちきゅう", "eco", "dummy-eco", 0x00, 0x00, 0x00);
				
				/*
				 *  7	--> news
				 *  8	ラジオ
				 * 10	ドキュメンタリー
				 * 11	教育・学習
				 * 12	映像ブログ
				 * 13	世の中のイベント・動き
				 * 14	ステーションコール (pac0000062)
				 * 15	--> beauty --> トレンド
				 * 16	ビジネス
				 * 17	オメコメ
				 * 18	--> c.gyao.jp
				 * 19	--> election
				 * 20	ライフ＆カルチャー
				 * 21	ビューティー＆ファッション --> トレンド
				 * 23	アンケート
				 * 24	ショッピング
				 * 26	テスト (pac0001397)
				 * 27	マンション情報
				 * 28	コミックス
				 * 30	歌ブログ
				 * 36	CM
				 */
			}
		}
	}
}
