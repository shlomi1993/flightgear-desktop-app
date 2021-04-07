using FlightSimulatorDesktopApp.View;
using FlightSimulatorDesktopApp.ViewModel;
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

namespace FlightSimulatorDesktopApp.View
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        private PlayerViewModel pvm;
        private double speed;
        private int row;
        public PlayerView()
        {
            InitializeComponent();
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            pvm.StartFrom(speed, row);
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            pvm.StartFrom(speed, row + 10);
        }

        private void Reverse_Click(object sender, RoutedEventArgs e)
        {
            pvm.StartFrom(speed, row - 10);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            this.row = (int)slider.Value;
            pvm.StartFrom(speed, row);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int input = Convert.ToInt32(speedChanged.Text);
            if (input >= 2)
            {
                this.speed = 2;
            }
            else if (input <= 0.5)
            {
                this.speed = 0.5;
            }
            else if (input > 0.5 && input <= 1)
            {
                this.speed = 1;
            }
            else
            {
                this.speed = 1.5;
            }
        }
    }
}
