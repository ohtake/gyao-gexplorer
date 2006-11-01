using System;

namespace Yusen.GExplorer.GyaoModel {
	partial class GDataSet {
		partial class GGenreDataTable {
			public void ResetToDefaultGenres() {
				this.Clear();
				
				this.AddGGenreRow(22, "ニュース・ビジネス", "news", "newsbiz", 0x00, 0x33, 0x99);
				this.AddGGenreRow( 1, "映画", "cinema", "cinema", 0xCC, 0x00, 0x00);
				this.AddGGenreRow( 3, "音楽", "music", "music", 0x99, 0x33, 0xCC);
				this.AddGGenreRow( 2, "ドラマ", "drama", "dorama", 0xFF, 0x99, 0x66);
				this.AddGGenreRow( 9, "スポーツ", "sports", "sports", 0x00, 0x66, 0xFF);
				this.AddGGenreRow(10, "ドキュメンタリー", "documentary", "documentary", 0x00, 0x99, 0x33);
				this.AddGGenreRow(21, "ビューティー＆ファッション", "beauty", "health", 0xFF, 0x99, 0xCC);
				this.AddGGenreRow(20, "ライフ＆カルチャー", "life", "culture", 0x99, 0x66, 0x33);
				this.AddGGenreRow( 6, "アニメ", "anime", "anime", 0x00, 0x99, 0xFF);
				this.AddGGenreRow( 5, "バラエティ", "variety", "variety", 0x66, 0xCC, 0x33);
				this.AddGGenreRow( 4, "アイドル・グラビア", "idol", "idol", 0xFF, 0x66, 0xCC);
				this.AddGGenreRow(12, "映像ブログ", "blog", "blog", 0xFF, 0x99, 0x99);
				this.AddGGenreRow(24, "ショッピング", "shopping", "shopping", 0xFF, 0x66, 0x00);
				this.AddGGenreRow(25, "ゲーム", "game", "game", 0x00, 0x99, 0x66);
				this.AddGGenreRow(28, "コミックス", "comics", "comics", 0xA7, 0x0B, 0x85);
				this.AddGGenreRow(27, "マンション情報", "special", "mansion", 0xFF, 0xC1, 0x00);
				/*
				 *  7	--> news
				 *  8	ラジオ
				 * 11	教育・学習
				 * 13	世の中のイベント・動き
				 * 14	ステーションコール (pac0000062)
				 * 15	--> beauty
				 * 16	ビジネス
				 * 17	オメコメ
				 * 18	--> c.gyao.jp
				 * 19	--> election
				 * 23	アンケート
				 * 26	テスト (pac0001397)
				 */
			}
		}
	}
}
