GyaO 専用ブラウザ GExplorer 1.1.0.0

◆必要な環境
.NET Framework 2.0.50215 がインストールされており正常に動作していること

.NET Framework Version 2.0 Redistributable Package Beta 2 (x86)
http://www.microsoft.com/downloads/details.aspx?FamilyId=7ABD8C8F-287E-4C7E-9A4A-A4ECFF40FC8E&displaylang=en

まだβ製品しかないけれども 2ch でうｐされているプログラムを実行するほどの度胸のある人ならMSのβ製品なんて恐るるに足らんでしょう

◆実行方法
GExplorer\bin\Release\GExplorer.exe をダブルクリックする
同じディレクトリに AxInterop.WMPLib.dll と Interop.WMPLib.dll があること

◆アンインストール方法
ファイルを丸ごと削除する
.NET Framework 2.0 も必要なければこれもアンインストールする

◆操作方法
・ユーザIDの自動取得
	GExplorer 起動前に IE で GyaO のユーザ登録を済ませておくこと
	ユーザIDの自動取得に失敗したら GExplorer は起動できません
・ジャンルタブ
	読み込んでいないジャンルのタブを選択すると読み込む
	タブをダブルクリックすると強制的に読み込みなおす
・リストビュー
	ダブルクリックやEnterキーにより専用プレーヤで再生する
	リスト内を右クリックすれば他の操作も行えたりする (まだ開発途中)
	新着の動画にはアイコンに星印がつく
	特集ページのあるパッケージはアイコンがHTMLになる
・専用プレーヤ
	特に説明は要らないかと

◆既知の不具合
ビデオ再生ウィンドウを開いたままメインのウィンドウを閉じるとやばいかも
ビデオの再生が始まる前に再生ウィンドウを閉じるとやばいかも
1.1.0.0も午前中にテストをしたことがないので，配信終了日が点滅している状況での動作は不明
全画面表示時に右上の×を押しても閉じられない
フォームのアイコンが透過しないっぽい (なので非透過のアイコンを使用)

◆ソースを弄ってカスタマイズしたい
Visual C# 2005 Express Edition のβ製品を拾ってくるのがよいかと
http://lab.msdn.microsoft.com/express/vcsharp/

◆謝辞
CMか否かの判定を行うのに下記の神のソースを参考にしました
http://pc8.2ch.net/test/read.cgi/esite/1116115226/81
アイコン提供
http://pc8.2ch.net/test/read.cgi/esite/1118420039/13

