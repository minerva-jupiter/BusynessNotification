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
                CPUSlider.Value = CPUvalue;
            }
        }

        private void TextBox_MemorySlider_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBox_CPUSlider.ToString() != "")
            {
                double Memoryvalue = double.Parse(TextBox_MemorySlider.Text);
                CPUSlider.Value = Memoryvalue;
            }
        }

        private void MemorySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int Memoryvalue = (int)e.NewValue;
            TextBox_CPUSlider.Text = Memoryvalue.ToString();
            CPUSlider.Value = Memoryvalue;
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
                DiskSlider.Value = Diskvalue;
            }
        }
    }
}