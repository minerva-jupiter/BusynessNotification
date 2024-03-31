# BusynessNotification

## 概要
これは起動後に遅いPCに対して、快適な動作をする状態になったことを通知するアプリケーションです。
HDDをシステムドライブにしたPCやスタートアップアプリが多いPCに対して最も効果的に働きます。

## インストール方法 How to install
1.リリースについている「BN_installer.zip」をダウンロードした後、解凍します(インストールされる場所と回答された場所は関係ないです)。
2.解凍したフォルダの中にある「setup.exe」(PCがmsiに対応している場合は「BN_installer.msi」でも可)を起動します。
3.あとはインストーラーに従ってインストールします。

## 使い方 How to use
基本的にはインストールした時点でおすすめの初期設定値が適応されており、インストール後の起動から有効になります。
初期設定で十分にアプリケーションが働いているという人は何もしなくて大丈夫です。
設定の変更が必要な場合(常にCPU使用率が50以上であり、初期設定だと通知が届かないなど)は以下の方法で設定画面を開いてください。

設定値(何％で安定とするかや持続秒数)を変更したい場合は、起動中にタスクトレイに表示されているアイコンをクリックすると設定画面が出てきます。
PC安定の通知が届いた後、アプリケーションは自己シャットダウンしますので、タスクトレイにアイコンがない場合はスタートからアプリケーションを起動して、再度タスクトレイを確認ください。

### 上級者向けオプション
このアプリケーションの設定はプログラムファイル(デフォルトで"C:\Program Files\Minerva_juppiter\BusynessNotification")内の「SettingJson.json」内に保存されています。
そちらをいじって設定値を変更してもらっても何の問題も起こらないです。

## 設定画面の使い方 How to setting Window
設定画面の表示方法は「使い方」に記載しているので、そちらを参照ください。
設定画面の上の3行は9つのグライドに分かれており、それぞれが一つの設定値を変更するための手段になっています。
1行目はCPUに関する設定、2行目はRAMに関する設定、3行目はDiskに関する設定です。
1列目にはそれぞれの値をチェックするかどうか、2列目にはそれぞれの値がどこを下回ったら安定したとするか、3列目には何秒連続で2列目で設定した値を下回ったら通知するか、を決定できます。
なお、このアプリケーションではCPU、RAM、Diskすべての基準を満たすまで通知を送らない仕様です。
好みの値に設定し終えたら、必ず、一番下の行にある「Save」ボタンを押してからウィンドウの×ボタンを押してアプリケーションを終了してください。
ただし、設定ウィンドウを閉じてしまうと、WPFアプリケーションの特性上、アプリケーション自体がシャットダウンされてしまうのでご注意ください。

以下、開発メモ

ダークテーマ参考:https://zenn.dev/apterygiformes/books/470ba1042dfbef/viewer/b798d6
to ModernWpfUI

SystemCPUuseage
SystemMemoryUseage
from Windows.System.Diagnostics
https://windev.just4fun.biz/?.NET/CPU%E4%BD%BF%E7%94%A8%E7%8E%87%E3%82%92%E5%8F%96%E5%BE%97%E3%81%99%E3%82%8B

and Disk activeTime 
https://codezine.jp/article/detail/3238?p=4

テーマはユーザーのwindowsのテーマが自動的に適応されます。

タスクトレイ常駐化はこちら
https://qiita.com/TiggeZaki/items/aa17edbef0cc5f4736d9

設定ファイルのいじり方はこちら
https://learn.microsoft.com/ja-jp/dotnet/desktop/winforms/advanced/how-to-write-user-settings-at-run-time-with-csharp?view=netframeworkdesktop-4.8

timer.thread.timer
https://atmarkit.itmedia.co.jp/ait/articles/0511/11/news118.html

タスクトレイアイコン
https://qiita.com/kyv28v/items/0a5b078d246000cb6781
http://csfun.blog49.fc2.com/blog-entry-93.html

MainWindow非表示はこれ
https://windev.just4fun.biz/?WPF/%E8%B5%B7%E5%8B%95%E3%81%97%E3%81%A6%E3%82%82MainWindow%E3%82%92%E8%A1%A8%E7%A4%BA%E3%81%97%E3%81%9F%E3%81%8F%E3%81%AA%E3%81%84%E5%A0%B4%E5%90%88

通知
https://zenn.dev/ambleside138/articles/75e7f8fdcf4fdc

アイコンの設定
https://it-skill-memo.work/2019/08/17/%E3%80%90wpf%E3%80%91%E3%82%A2%E3%82%A4%E3%82%B3%E3%83%B3%E3%82%92%E8%A8%AD%E5%AE%9A%E3%81%99%E3%82%8B/
通知に表示されるアプリケーションのアイコンは「コンテンツ」タスクトレイのものは「リソース」として別々のファイルとして保存している。

インストーラーを同ソリューションの内部のフォルダに作成することによって、同一のリポジトリに保存することに成功した。