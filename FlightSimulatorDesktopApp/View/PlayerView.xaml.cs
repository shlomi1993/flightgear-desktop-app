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
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        private PlayerViewModel pvm;
        private bool isPlayed;
        private bool toStart;
        public PlayerView()
        {
            InitializeComponent();
            if(Application.Current is App)
            {
                Main_VM = (Application.Current as App).MainVM;
                pvm = Main_VM.PlayerVM;
                DataContext = pvm;

            }
            isPlayed = true;
            toStart = false;
        }

        public void setViewModel(PlayerViewModel pvm)
        {
            this.pvm = pvm;
            DataContext = this.pvm;
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if(!toStart)
            {
                toStart = true;
                pvm.Start();
            }
            if (!isPlayed)
            {
                isPlayed = true;
                pvm.Play();
            }
        }
        private void Forward_Click(object sender, RoutedEventArgs e)
        {

            if ((pvm.VM_IRow + 10) < pvm.VM_NumOfRows)
            {
                pvm.VM_IRow += 10;
            }
        }

        private void Reverse_Click(object sender, RoutedEventArgs e)
        {
            if (pvm.VM_IRow - 10 < 0)
            {
                pvm.VM_IRow = 0;
            }
            else
            {
                pvm.VM_IRow -= 10;
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (isPlayed)
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

        public FlightSimulatorViewModel Main_VM { get; internal set; }
    }
}
