using FlightSimulatorDesktopApp.ViewModel;
using FlightSimulatorDesktopApp.Model;

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
using FlightSimulatorDesktopApp.View;
using System.Diagnostics;

namespace FlightSimulatorDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FlightSimulatorModel fsm;
        private FlightSimulatorViewModel fsvm;
        private ConnectionViewModel cvm;
        private DataViewModel dvm;


        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            fsm = new FlightSimulatorModel(new TelnetClient());
            fsvm = new FlightSimulatorViewModel(fsm);
            cvm = new ConnectionViewModel(fsm);
            dvm = new DataViewModel(fsm);
            DataContext = fsvm;

        }
        private void ClickConnect(object sender, RoutedEventArgs e)
        {
            ConnectButton.Foreground = new SolidColorBrush(Colors.Blue);
            ConnectionSettings cs = new ConnectionSettings(cvm);
            cs.Show();
        }
        private void LoadData(object sender, RoutedEventArgs e)
        {
            BrowseButton.Foreground = new SolidColorBrush(Colors.Blue);
            DataSettings ds = new DataSettings(dvm);
            ds.Show();
        }
        private void ClickDisconnect(object sender, RoutedEventArgs e)
        {
            cvm.disconnect(); // need to check if disconnected already.           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fsvm.start();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            cvm.disconnect();
            Application.Current.Shutdown();
        }
    }
}
