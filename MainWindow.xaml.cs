using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void CPUSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int CPUvalue = (int)e.NewValue;
            TextBox_CPUSlider.Text = CPUvalue.ToString();
            CPUSlider.Value = CPUvalue;
        }

        private void TextBox_CPUSlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_CPUSlider.ToString() != "")
            {
                double CPUvalue = double.Parse(TextBox_CPUSlider.Text);
                if (0 <= CPUvalue && CPUvalue　<= 100)
                {
                    CPUSlider.Value = CPUvalue;
                }
            }
        }

        private void TextBox_MemorySlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_CPUSlider.ToString() != "")
            {
                double Memoryvalue = double.Parse(TextBox_MemorySlider.Text);
                if (0 <= Memoryvalue && Memoryvalue <= 100)
                {
                    MemorySlider.Value = Memoryvalue;
                }
            }
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
            if (TextBox_DiskSlider.Text.ToString() != "")
            {
                double Diskvalue = double.Parse (TextBox_DiskSlider.Text);
                if(0 <= Diskvalue && Diskvalue <= 100) { 
                    DiskSlider.Value = Diskvalue; 
                }
            }
        }

        private void CPUSecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CPUSecTextBox.Text.ToString() != "")
            {
                int CPUSecValue = new int();
                bool CPUSecCan = int.TryParse (CPUSecTextBox.Text.ToString(),out CPUSecValue);
                if (CPUSecCan == false || CPUSecValue < 0 || CPUSecValue >= 1000)
                {
                    string messageBoxText = "Please enter up to 3 digits?";
                    string caption = "unexpected input";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon);
                    CPUSecTextBox.
                }
            }
        }

        private void MemorySecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MemorySecTextBox.Text.ToString() != "")
            {
                int MemorySecValue = new int();
                bool MemorySecCan = int.TryParse(MemorySecTextBox.Text.ToString(), out MemorySecValue);
                if (MemorySecCan == false || MemorySecValue < 0 || MemorySecValue >= 1000)
                {
                    string messageBoxText = "Please enter up to 3 digits?";
                    string caption = "unexpected input";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon);
                }
            }
        }

        private void DiskSecTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DiskSecTextBox.Text.ToString() != "")
            {
                int DiskSecValue = new int();
                bool DiskSecCan = int.TryParse(DiskSecTextBox.Text.ToString(), out DiskSecValue);
                if (DiskSecCan == false || DiskSecValue < 0 || DiskSecValue >= 1000)
                {
                    string messageBoxText = "Please enter up to 3 digits?";
                    string caption = "unexpected input";
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result;

                    result = MessageBox.Show(messageBoxText, caption, button, icon);
                }
            }
        }
    }
}