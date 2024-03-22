using System.Text;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.AccessControl;
using System.Text.Json;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

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
                MessageBoxResult result;

                result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
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
            TextBox_CPUSlider.Text = InputNormalizerToDouble(TextBox_CPUSlider.Text,SliderCPU).ToString();
        }

        private void TextBox_MemorySlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_MemorySlider.Text = InputNormalizerToDouble(TextBox_MemorySlider.Text,SliderMemory).ToString();
        }
        private void TextBox_DiskSlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_CPUSlider.Text = InputNormalizerToDouble(TextBox_CPUSlider.Text.ToString(), SliderDisk).ToString();
        }

        private void CPUSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int CPUvalue = (int)e.NewValue;
            TextBox_CPUSlider.Text = CPUvalue.ToString();
            CPUSlider.Value = CPUvalue;
        }
        private void MemorySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int Memoryvalue = (int)e.NewValue;
            TextBox_MemorySlider.Text = Memoryvalue.ToString();
            MemorySlider.Value = Memoryvalue;
        }

        private void DiskSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int Diskvalue = (int)e.NewValue;
            TextBox_DiskSlider.Text = Diskvalue.ToString();
            DiskSlider.Value = Diskvalue;
        }

        private void CPUSecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CPUSecTextBox.Text = InputNormalizerToDouble(CPUSecTextBox.Text,SecCPU).ToString();
        }

        private void MemorySecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemorySecTextBox.Text = InputNormalizerToDouble(MemorySecTextBox.Text.ToString(), SecMemory).ToString();
        }

        private void DiskSecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DiskSecTextBox.Text = InputNormalizerToDouble(DiskSecTextBox.Text.ToString(), SecDisk).ToString();
        }

        private void SaveSettingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingJson data = new SettingJson();

                data.CheckCPU = true;
                data.CheckMemory = true;
                data.CheckDisk = true;
                data.SliderCPU = 50;
                data.SliderMemory = 50;
                data.SliderDisk = 50;
                data.SecCPU = 5;
                data.SecMemory = 5;
                data.SecDisk = 5;

                string jsonStr = JsonSerializer.Serialize(data);
                Debug.WriteLine(jsonStr);
            }
            catch (Exception)
            {
                string messageBoxText = "Please try to set this app expect on secur app";
                string caption = "Failed to get setting";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
            finally
            {
                string messageBoxText = "Saving setting prosess is finished";
                string caption = "Finish saving setting prosess";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result;

                result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }
        }

        static double InputNormalizerToDouble(string input,double oldValue)
        {
            double value;
            bool NormalizationCan = double.TryParse(input, out value);
            if(NormalizationCan == false || value < 0 || value > 100)
            {
                string messageBoxText = "Please enter up to 100";
                string caption = "unexpected input";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
                value = oldValue;
                return value;
            }
            else
            {
                return value;
            }
        }
    }
}