using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab3Lib;
using CPSimLib;
using CircuitPlaygroundLib;

namespace Pomodoro_Timer_CSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICircuitPlayground pomodoro = new Lab3();
        ICircuitPlayground cp = null;

        public MainWindow()
        {
            InitializeComponent();
            port.ItemsSource = pomodoro.GetDevices();
        }

        private void OpenDevice(ICircuitPlayground device, int index)
        {
            if (index != -1 && cp == null)
            {
                cp = device;

                //cp.DataReceived += Sim_DataReceived;
                cp.Open(index);

                label.Content = "Connected";
            }
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            //OpenDevice(sim, port.SelectedIndex);
            OpenDevice(pomodoro, port.SelectedIndex);
            pomodoro.GetDevices();
        }

        private void ColorChange(object sender, RoutedEventArgs e)
        {
            int value = (int)red.Value + (int)blue.Value + (int)green.Value;
            //int pixel = (int)pinNum.Value;
            int pixel = 0;
            //rgb.Content = value.ToString();
            //text.Text = pixel.ToString();

            LedColor led = new LedColor();
            led.R = (byte)red.Value;
            led.G = (byte)green.Value;
            led.B = (byte)blue.Value;

            pomodoro.SetPixelColor(pixel, led);
            cp.SetPixelColor(pixel, led);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte R = (byte)red.Value;
            byte G = (byte)green.Value;
            byte B = (byte)blue.Value;

            colorBox.Fill = new SolidColorBrush(Color.FromRgb(R, G, B));
        }

    }
}
