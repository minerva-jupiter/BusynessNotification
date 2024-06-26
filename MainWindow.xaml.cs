﻿using System.Windows;
using System.Windows.Controls;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BusynessNotification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool CheckCPU = true;
        bool CheckMemory = true;
        bool CheckDisk = true;

        double SliderCPU = 50;
        double SliderMemory = 50;
        double SliderDisk = 50;

        double SecCPU = 5;
        double SecMemory = 5;
        double SecDisk = 5;
        public MainWindow()
        {
            InitializeComponent();

            ///設定値の読み取り
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("SettingJson.json")
                .Build();

            try
            {
                CheckCPU = Convert.ToBoolean(configuration["CheckCPU"]);
                CheckMemory = Convert.ToBoolean(configuration["CheckMemory"]);
                CheckDisk = Convert.ToBoolean(configuration["CheckDisk"]);

                SliderCPU = Convert.ToDouble(configuration["SliderCPU"]);
                SliderMemory = Convert.ToDouble(configuration["SliderMemory"]);
                SliderDisk = Convert.ToDouble(configuration["SliderDisk"]);

                SecCPU = Convert.ToDouble(configuration["SecCPU"]);
                SecMemory = Convert.ToDouble(configuration["SecMemory"]);
                SecDisk = Convert.ToDouble(configuration["SecDisk"]);
            }
            catch (Exception)
            {
                string messageBoxText = "Please try to set this app expect on secur app";
                string caption = "Failed to get setting";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;

                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }

            ///取得した値をUIに適応
            CPUcheck.IsChecked = CheckCPU;
            Memorycheck.IsChecked = CheckMemory;
            Diskcheck.IsChecked = CheckDisk;

            CPUSlider.Value = SliderCPU;
            MemorySlider.Value = SliderMemory;
            DiskSlider.Value = SliderDisk;

            TextBox_CPUSlider.Text = SliderCPU.ToString();
            TextBox_DiskSlider.Text = SliderMemory.ToString();
            TextBox_DiskSlider.Text = SliderDisk.ToString();

            CPUSecTextBox.Text = SecCPU.ToString();
            MemorySecTextBox.Text = SecMemory.ToString();
            DiskSecTextBox.Text = SecDisk.ToString();
        }

        private void TextBox_CPUSlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            SliderCPU = InputNormalizerToDouble(TextBox_CPUSlider.Text, SliderCPU);
            TextBox_CPUSlider.Text = SliderCPU.ToString();
            CPUSlider.Value = SliderCPU;
        }

        private void TextBox_MemorySlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            SliderMemory = InputNormalizerToDouble(TextBox_MemorySlider.Text, SliderMemory);
            TextBox_MemorySlider.Text = SliderMemory.ToString();
            MemorySlider.Value = SliderMemory;
        }
        private void TextBox_DiskSlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            SliderDisk = InputNormalizerToDouble(TextBox_DiskSlider.Text.ToString(), SliderDisk);
            TextBox_DiskSlider.Text = SliderDisk.ToString();
            DiskSlider.Value = SliderDisk;
        }

        private void CPUSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderCPU = Math.Round(CPUSlider.Value);
            CPUSlider.Value = SliderCPU;
            TextBox_CPUSlider.Text = SliderCPU.ToString();
        }
        private void MemorySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderMemory = Math.Round(MemorySlider.Value);
            MemorySlider.Value = SliderMemory;
            TextBox_MemorySlider.Text = SliderMemory.ToString();
        }

        private void DiskSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderDisk = Math.Round(DiskSlider.Value);
            DiskSlider.Value = SliderDisk;
            TextBox_DiskSlider.Text = SliderDisk.ToString();
        }

        private void CPUSecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SecCPU = InputNormalizerToDouble(CPUSecTextBox.Text, SecCPU);
            CPUSecTextBox.Text = SecCPU.ToString();
        }

        private void MemorySecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SecMemory = InputNormalizerToDouble(MemorySecTextBox.Text, SecMemory);
            MemorySecTextBox.Text = SecMemory.ToString();
        }

        private void DiskSecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SecDisk = InputNormalizerToDouble(DiskSecTextBox.Text.ToString(), SecDisk);
            DiskSecTextBox.Text = SecDisk.ToString();
        }

        private void SaveSettingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingJson data = new()
                {
                    CheckCPU = Convert.ToBoolean(CPUcheck.IsChecked),
                    CheckMemory = Convert.ToBoolean(Memorycheck.IsChecked),
                    CheckDisk = Convert.ToBoolean(Diskcheck.IsChecked),
                    SliderCPU = SliderCPU,
                    SliderMemory = SliderMemory,
                    SliderDisk = SliderDisk,
                    SecCPU = SecCPU,
                    SecMemory = SecMemory,
                    SecDisk = SecDisk
                };

                string jsonStr = JsonSerializer.Serialize(data);
                File.WriteAllText(@"SettingJson.json", jsonStr);
                Debug.WriteLine(jsonStr);
            }
            catch (Exception ex)
            {
                string messageBoxText = "Please try to set this app expect on secur app.(Exception number: " + ex + ")";
                string caption = "Failed to save setting";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;

                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
            finally
            {
                string messageBoxText = "Saving setting prosess is finished";
                string caption = "Finish saving setting prosess";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;

                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        static double InputNormalizerToDouble(string input,double oldValue)
        {
            if(input == null || input == ""){
                return oldValue;
            }
            bool NormalizationCan = double.TryParse(input, out double value);
            if (NormalizationCan == false || value < 0 || value > 100)
            {
                string messageBoxText = "Please enter up to 100";
                string caption = "unexpected input";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;

                System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
                return oldValue;
            }
            else
            {
                return value;
            }
        }
    }
}