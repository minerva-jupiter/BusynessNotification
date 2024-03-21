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

        int SecCPU = 5;
        int SecMemory = 5;
        int SecDisk = 5;
        public MainWindow()
        {
            InitializeComponent();

            ///設定値の読み取り

            try
            {
                CheckCPU = Properties.Settings.Default.CheckCPU;
                CheckMemory = Properties.Settings.Default.CheckMemory;
                CheckDisk = Properties.Settings.Default.CheckDisk;

                SliderCPU = Properties.Settings.Default.SliderCPU;
                SliderMemory = Properties.Settings.Default.SliderMemory;
                SliderDisk = Properties.Settings.Default.SliderDisk;

                SecCPU = Properties.Settings.Default.SecCPU;
                SecMemory = Properties.Settings.Default.SecMemory;
                SecDisk = Properties.Settings.Default.SecDisk;
            }catch (Exception)
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

        private void CPUSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int CPUvalue = (int)e.NewValue;
            TextBox_CPUSlider.Text = CPUvalue.ToString();
            CPUSlider.Value = CPUvalue;
        }

        private void TextBox_CPUSlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_CPUSlider.Text = InputNormalizerToDouble(TextBox_CPUSlider.Text,SliderCPU).ToString();
        }

        private void TextBox_MemorySlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_MemorySlider.Text = InputNormalizerToDouble(TextBox_MemorySlider.Text,SliderMemory).ToString();
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

        private void TextBox_DiskSlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_CPUSlider.Text = InputNormalizerToDouble(TextBox_CPUSlider.Text.ToString(), SliderDisk).ToString();
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
                Properties.Settings.Default.CheckCPU = CPUcheck.IsChecked.Value;
                Properties.Settings.Default.CheckMemory = Memorycheck.IsChecked.Value;
                Properties.Settings.Default.CheckDisk = Diskcheck.IsChecked.Value;

                Properties.Settings.Default.SliderCPU = CPUSlider.Value;
                Properties.Settings.Default.SliderMemory = MemorySlider.Value;
                Properties.Settings.Default.SliderDisk = DiskSlider.Value;

                Properties.Settings.Default.SecCPU = int.Parse(CPUSecTextBox.Text.ToString());
                Properties.Settings.Default.SecMemory = int.Parse(MemorySecTextBox.Text.ToString());
                Properties.Settings.Default.SecDisk = int.Parse(DiskSecTextBox.Text.ToString());

                Properties.Settings.Default.Save();
            }catch (Exception)
            {
                string messageBoxText = "Please try to set this app expect on secur app";
                string caption = "Failed to get setting";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;

                result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon);
            }

        }

        static double InputNormalizerToDouble(string input,double oldValue)
        {
            double value;
            bool NormalizationCan = double.TryParse(input, out value);
            if(NormalizationCan == false || value < 0 || value >= 1000)
            {
                string messageBoxText = "Please enter up to 3 digits?";
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            System.Diagnostics.Process.Start(System.Windows.Application.ResourceAssembly.Location);
        }
    }
}