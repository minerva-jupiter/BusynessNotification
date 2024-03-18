using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BusynessNotification
{
    internal class ObserveUseage
    {


        public async void Controller()
        {
            Debug.WriteLine("Controller has colled");
            ///設定から数値を取得
            bool CheckCPU = Properties.Settings.Default.CheckCPU;
            bool CheckMemory = Properties.Settings.Default.CheckMemory;
            bool CheckDisk = Properties.Settings.Default.CheckDisk;

            double CPUSlider = Properties.Settings.Default.SliderCPU;
            ///double MemorySlider = Properties.Settings.Default.SliderMemory;
            double MemorySlider = 80;
            double DiskSlider = Properties.Settings.Default.SliderDisk;

            int SecCPU = Properties.Settings.Default.SecCPU;
            int SecMemory = Properties.Settings.Default.SecMemory;
            int SecDisk = Properties.Settings.Default.SecDisk;

            int flagCPU = 0;
            int flagMemory = 0;
            int flagDisk = 0;

            Debug.WriteLine("Setting file have readed");
            Debug.WriteLine("memorySlider " + MemorySlider);

            ///インスタンス
            System.Diagnostics.PerformanceCounter cpuCounter = new System.Diagnostics.PerformanceCounter("Processor Information", "% Processor Utility", "_Total");

            PerformanceCounter performanceCounterRAM = new PerformanceCounter();
            performanceCounterRAM.CounterName = "% Committed Bytes In Use";
            performanceCounterRAM.CategoryName = "Memory";

            PerformanceCounter Disc1 = new PerformanceCounter();
            Disc1.CategoryName = "LogicalDisk";
            Disc1.CounterName = "Disk Transfers/sec";
            Disc1.InstanceName = "C:";

            System.Threading.Timer timer = null;
            
            TimerCallback timer_delegate = async state =>
            {

                Debug.WriteLine("timer is still waiking");

                if (CheckCPU)
                {
                    await Task.Run(() =>
                    {
                        if (cpuCounter.NextValue() <= CPUSlider)
                        {
                            flagCPU++;

                            Debug.WriteLine("CPU flag" + flagCPU);

                        }
                        else
                        {
                            flagCPU = 0;
                        }
                    });
                }
                if (CheckMemory)
                {
                    await Task.Run(() =>
                    {
                        if (performanceCounterRAM.NextValue() <= MemorySlider)
                        {
                            flagMemory++;

                            Debug.WriteLine("RAM flag" + flagMemory);

                        }
                        else
                        {
                            flagMemory = 0;
                        }
                    });
                }
                if (CheckDisk)
                {
                    await Task.Run(() =>
                    {
                        if (Disc1.NextValue() <= DiskSlider)
                        {
                            flagDisk++;

                            Debug.WriteLine("Disk flag" + flagDisk);

                        }
                        else
                        {
                            flagDisk = 0;
                        }
                    });
                }


                if(flagCPU >= SecCPU &&  flagMemory >= SecMemory && flagDisk >= SecDisk)
                {
                    Debug.WriteLine("All clear");

                    timer.Dispose();

                    new ToastContentBuilder()
                        .AddText("Byssyness Notification")
                        .AddText("PCは使用可能な状態です。")
                        .Show();
                    Application.Exit();
                }
            };
            timer = new System.Threading.Timer(timer_delegate,null,1000,1000);


        }
    }
}
