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
            System.Windows.Forms.NotifyIcon notifyIcon;
            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "BusynessNotification";
            notifyIcon.Visible = true;

            //アイコンにコンテキストメニュー「終了」を追加する
            ContextMenuStrip menuStrip = new ContextMenuStrip();

            ToolStripMenuItem exitItem = new ToolStripMenuItem();
            exitItem.Text = "終了";
            menuStrip.Items.Add(exitItem);
            exitItem.Click += new EventHandler(exitItem_Click);
            notifyIcon.ContextMenuStrip = menuStrip;
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            ObserveUseage observeUseage = new ObserveUseage();
            observeUseage.Controller();

            void exitItem_Click(object sender, EventArgs e)
            {
                try
                {
                    notifyIcon.Dispose();
                    System.Windows.Application.Current.Shutdown();
                }
                catch { }
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var mainwindow = new MainWindow();
            mainwindow.Show();
        }
    }

}
