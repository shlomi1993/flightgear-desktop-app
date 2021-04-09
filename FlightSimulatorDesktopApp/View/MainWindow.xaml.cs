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
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace FlightSimulatorDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Models privates.
        private FlightSimulatorModel fsm;
        private DataModel dm;
        private ConnectionModel cm;

        // ViewModels privates.
        private FlightSimulatorViewModel fsvm;
        private ConnectionViewModel cvm;
        private DataViewModel dvm;

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();

            dm = new DataModel();
            dvm = new DataViewModel(dm);

            cm = new ConnectionModel();
            cvm = new ConnectionViewModel(cm);

            fsm = new FlightSimulatorModel(cm, dm);
            fsvm = new FlightSimulatorViewModel(fsm);
            
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fsvm.start();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            cvm.disconnect();
            Application.Current.Shutdown();
        }
    }
}
