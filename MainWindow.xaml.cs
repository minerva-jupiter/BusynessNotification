﻿using System.Text;
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
        public MainWindow()
        {
            InitializeComponent();

            ///設定値の読み取り
            bool CheckCPU = Properties.Settings.Default.CheckCPU;
            bool CheckMemory = Properties.Settings.Default.CheckMemory;
            bool CheckDisk = Properties.Settings.Default.CheckDisk;

            double SliderCPU = Properties.Settings.Default.SliderCPU;
            double SliderMemory = Properties.Settings.Default.SliderMemory;
            double SliderDisk = Properties.Settings.Default.SliderDisk;

            int SecCPU = Properties.Settings.Default.SecCPU;
            int SecMemory = Properties.Settings.Default.SecMemory;
            int SecDisk = Properties.Settings.Default.SecDisk;

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
            TextBox_CPUSlider.Text = InputNormalizerToDouble(TextBox_CPUSlider.Text,Properties.Settings.Default.SliderCPU).ToString();
        }

        private void TextBox_MemorySlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_MemorySlider.Text = InputNormalizerToDouble(TextBox_MemorySlider.Text,Properties.Settings.Default.SliderMemory).ToString();
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
            TextBox_CPUSlider.Text = InputNormalizerToDouble(TextBox_CPUSlider.Text.ToString(), Properties.Settings.Default.SliderDisk).ToString();
        }

        private void CPUSecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CPUSecTextBox.Text = InputNormalizerToDouble(CPUSecTextBox.Text,Properties.Settings.Default.SecCPU).ToString();
        }

        private void MemorySecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MemorySecTextBox.Text = InputNormalizerToDouble(MemorySecTextBox.Text.ToString(), Properties.Settings.Default.SecMemory).ToString();
        }

        private void DiskSecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DiskSecTextBox.Text = InputNormalizerToDouble(DiskSecTextBox.Text.ToString(), Properties.Settings.Default.SecDisk).ToString();
        }

        private void SaveSettingButton_Click(object sender, RoutedEventArgs e)
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
    }
}