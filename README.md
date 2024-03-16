# BusynessNotification
これは起動後に遅いPCに対して、快適な動作をする状態になったことを通知するアプリケーションです。
HDDをシステムドライブにしたPCやスタートアップアプリが多いPCに対して最も効果的に働きます。
また、このアプリを常駐させることで、動作が重くなった際にどのリソースを開放するべきかを通知してくれる機能を追加予定です。

ダークテーマ参考:https://zenn.dev/apterygiformes/books/470ba1042dfbef/viewer/b798d6
to ModernWpfUI

SystemCPUuseage
SystemMemoryUseage
from Windows.System.Diagnostics

and Disk activeTime 

テーマはユーザーのwindowsのテーマが自動的に適応されます。

タスクトレイ常駐化はこちら
https://qiita.com/TiggeZaki/items/aa17edbef0cc5f4736d9

設定ファイルのいじり方はこちら
https://learn.microsoft.com/ja-jp/dotnet/desktop/winforms/advanced/how-to-write-user-settings-at-run-time-with-csharp?view=netframeworkdesktop-4.8