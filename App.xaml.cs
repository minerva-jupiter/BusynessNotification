using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace BusynessNotification
{


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
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
