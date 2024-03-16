﻿using System;
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
            ///設定から数値を取得
            bool CheckCPU = Properties.Settings.Default.CheckCPU;
            bool CheckMemory = Properties.Settings.Default.CheckMemory;
            bool CheckDisk = Properties.Settings.Default.CheckDisk;

            double CPUSlider = Properties.Settings.Default.SliderCPU;
            double MemorySlider = Properties.Settings.Default.SliderMemory;
            double DiskSlider = Properties.Settings.Default.SliderDisk;

            int SecCPU = Properties.Settings.Default.SecCPU;
            int SecMemory = Properties.Settings.Default.SecMemory;
            int SecDisk = Properties.Settings.Default.SecDisk;

            int flagCPU = 0;
            int flagMemory = 0;
            int flagDisk = 0;

            ///インスタンス
            System.Diagnostics.PerformanceCounter cpuCounter = new System.Diagnostics.PerformanceCounter("Processor Information", "% Processor Utility", "_Total");

            PerformanceCounter performanceCounterRAM = new PerformanceCounter();
            performanceCounterRAM.CounterName = "% Committed Bytes In Use";
            performanceCounterRAM.CategoryName = "Memory";

            PerformanceCounter Disc1 = new PerformanceCounter();
            Disc1.CategoryName = "LogicalDisk";
            Disc1.CounterName = "Disk Transfers/sec";
            Disc1.InstanceName = "C:";

            Timer timer = null;
            
            TimerCallback timer_delegate = async state =>
            {
                if (CheckCPU)
                {
                    await Task.Run(() =>
                    {
                        if (cpuCounter.NextValue() >= CPUSlider)
                        {
                            flagCPU++;
                        }
                    });
                }
                if (CheckMemory)
                {
                    await Task.Run(() =>
                    {
                        if (performanceCounterRAM.NextValue() >= MemorySlider)
                        {
                            flagMemory++;
                        }
                    });
                }
                if (CheckDisk)
                {
                    await Task.Run(() =>
                    {
                        if (Disc1.NextValue() >= DiskSlider)
                        {
                            flagDisk++;
                        }
                    });
                }


                if(flagCPU >= CPUSlider &&  flagMemory >= MemorySlider && flagDisk >= DiskSlider)
                {
                    timer.Dispose();
                }
            };
            timer = new Timer(timer_delegate,null,1000,1000);


        }
    }
}