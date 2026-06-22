# CounterInfo

### 開発の動機

①地元のハローワークの混雑状況サイトが気に入らなかった。（特に「待ち時間が分からない」こと）
https://www316.voicecall.jp/VcWeb/TopMenu/topmenu?sid=41721671379
②前社ではXamarinを使っていたが、珍しくnet.MAUIの求人があった。
（もう少し有名なFlutterのポートフォリオも作らなければならないから、そんなに余裕はないけど）

### 感想

MVVMtoolkitが気持ちよかった（小学生並の感想）
ReactiveProperty時代よりも簡単になっているし、OnPropertyChangedに苦しむ必要もない。
FlutterのViewModelと同格になった印象ではある。

### 対Flutter

①起動はMAUIが早い、ビルドはFlutterが早い
Flutterは（アプリ起動時）変更なくてもビルドが掛かる。
②UIの再利用性はFlutterの勝ち
MAUIでも出来なくはないのだが、プロパティ設定するコードを書くのが面倒。
これはMVVMtoolkitでも解決できない。
③コーディングの利便性はC#(MAUI)の勝ち
関数オーバーロードはないし、リフレクションもない。

### 改善点（不具合）

SQLiteであっても、InsertするときにID指定すれば、（そのIDで）入るはずである。
プラグイン（SQLite-PCL）の仕様なのか分からないが、なぜか入らない。

(AutoIncrementされる)IDと、（表示用の）順番を分ければ良いのだが…
（製品でもないポートフォリオで、他もやらなければいけないのに、余裕が足らない）