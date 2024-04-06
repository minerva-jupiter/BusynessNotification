using System.Diagnostics;
using System.Windows;
using Windows.ApplicationModel;
using Windows.UI.Notifications;
using Windows.UI.Notifications.Management;

namespace BusynessNotification
{


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : System.Windows.Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ///ここにスタートアップ時の処理を記述
            var icon = GetResourceStream(new Uri("BussynessNotificaion.ico", UriKind.Relative)).Stream;
            var notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true,
                Icon = new System.Drawing.Icon(icon),
                Text = "BusynessNotification"
            };
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_Click);

            ///通知の中にBusynessNotificationからの通知が残っていれば削除する
            ///get the listener
            UserNotificationListener userNotificationListener = UserNotificationListener.Current;
            ///and request access the the user's notification
            UserNotificationListenerAccessStatus userNotificationListenerAccessStatus = await userNotificationListener.RequestAccessAsync();
            if(userNotificationListenerAccessStatus != UserNotificationListenerAccessStatus.Allowed)
            {

            }
            ///get the toast notification
            IReadOnlyList<UserNotification> notifs = await userNotificationListener.GetNotificationsAsync(NotificationKinds.Toast);
            int i = 0;

            Debug.WriteLine("length of notification is " + notifs.Count);
            ///Debug.WriteLine("notifis is " + notifs[0]);

            while (notifs.Count >= i+1)
            {
                Debug.WriteLine("notifiChech is called");
                UserNotification notification = notifs[i];
                Debug.WriteLine(notification.AppInfo.DisplayInfo.DisplayName);
                if(notification.AppInfo.DisplayInfo.DisplayName == "BusynessNotification")
                {
                    userNotificationListener.RemoveNotification(notification.Id);
                }
                i++;
            }

            ObserveUseage observeUseage = new();
            observeUseage.Controller();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private void NotifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var wnd = new MainWindow();
                wnd.Show();
            }
        }
    }
}
