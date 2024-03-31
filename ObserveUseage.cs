using Microsoft.Extensions.Configuration;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BusynessNotification
{
    internal class ObserveUseage
    {


        public async void Controller()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("SettingJson.json")
                .Build();
            Debug.WriteLine("Controller has colled");
            ///設定から数値を取得
            bool CheckCPU = Convert.ToBoolean(configuration["CheckCPU"]);
            bool CheckMemory = Convert.ToBoolean(configuration["CheckMemory"]);
            bool CheckDisk = Convert.ToBoolean(configuration["CheckDisk"]);

            double CPUSlider = Convert.ToDouble(configuration["SliderCPU"]);
            double MemorySlider = Convert.ToDouble(configuration["SliderMemory"]);
            double DiskSlider = Convert.ToDouble(configuration["SliderDisk"]);

            double SecCPU = Convert.ToDouble(configuration["SecCPU"]);
            double SecMemory = Convert.ToDouble(configuration["SecMemory"]);
            double SecDisk = Convert.ToDouble(configuration["SecDisk"]);

            int flagCPU = 0;
            int flagMemory = 0;
            int flagDisk = 0;

            Debug.WriteLine("Setting file have readed");

            Debug.WriteLine("CheckCPU=" + CheckCPU + " CheckMemory=" + CheckMemory + " CheckDisk=" + CheckDisk);
            Debug.WriteLine("SliderCPU=" + CPUSlider + " SliderMemory=" + MemorySlider + " SliderDisk=" + DiskSlider);
            Debug.WriteLine("SecCPU=" + SecCPU + " SecMemory=" + SecMemory + " SecDisk=" + SecDisk);

            ///インスタンス
            System.Diagnostics.PerformanceCounter cpuCounter = new("Processor Information", "% Processor Utility", "_Total");

            PerformanceCounter performanceCounterRAM = new()
            {
                CounterName = "% Committed Bytes In Use",
                CategoryName = "Memory"
            };

            PerformanceCounter Disc1 = new()
            {
                CategoryName = "LogicalDisk",
                CounterName = "Disk Transfers/sec",
                InstanceName = "C:"
            };

            System.Threading.Timer timer = null;

            async void timer_delegate(object? state)
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


                if (flagCPU >= SecCPU && flagMemory >= SecMemory && flagDisk >= SecDisk)
                {
                    Debug.WriteLine("All clear");

                    timer.Dispose();

                    new ToastContentBuilder()
                        .AddText("Byssyness Notification")
                        .AddText("This PC is now available")
                        .Show();

                    [DllImport("user32.dll")]
                    static extern IntPtr GetForegroundWindow();

                    [DllImport("user32.dll")]
                    static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

                    _ = GetWindowThreadProcessId(GetForegroundWindow(), out int processid);
                    Process p = Process.GetProcessById(processid);
                    if (p.MainModule.FileVersionInfo.ProductName != "BusynessNotification")
                    {
                        Environment.Exit(0);
                    }
                }
            }
            timer = new System.Threading.Timer(timer_delegate, null, 1000, 1000);
        }
    }
}
