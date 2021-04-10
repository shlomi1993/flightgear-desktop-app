using FlightSimulatorDesktopApp.Model;
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
    public partial class PlayerView : Window
    {
        private PlayerViewModel pvm;
        private bool isPlayed;
        public PlayerView(PlayerViewModel pvm)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            isPlayed = true;
            this.pvm = pvm;
            DataContext = this.pvm;
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlayed)
            {
                isPlayed = true;
                pvm.Play();
            }
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {

            if ((pvm.VM_IRow + 10) < pvm.VM_NumOfRows) {
                pvm.VM_IRow += 10;
            }
        }

        private void Reverse_Click(object sender, RoutedEventArgs e)
        {
            if(pvm.VM_IRow - 10 < 0)
            {
                pvm.VM_IRow = 0;
            } else
            {
                pvm.VM_IRow -= 10;
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if(isPlayed)
            {
                isPlayed = false;
                pvm.Pause();
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (isPlayed)
            {
                isPlayed = false;
                pvm.Stop();
            }
        }
    }
}
