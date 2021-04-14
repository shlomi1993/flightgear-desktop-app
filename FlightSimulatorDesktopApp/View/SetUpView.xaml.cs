using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FlightSimulatorDesktopApp.ViewModel;

namespace FlightSimulatorDesktopApp.View
{
    /// <summary>
    /// Interaction logic for SetUpView.xaml
    /// </summary>
    public partial class SetUpView : UserControl
    {
        private readonly ConnectionViewModel cvm;
        private readonly DataViewModel dvm;
        public SetUpView()
        {
            InitializeComponent();
            if (Application.Current is App)
            {
                Main_VM = (Application.Current as App).MainVM;
                cvm = Main_VM.ConnectionVM;
                dvm = Main_VM.DataVM;
                DataContext = dvm;

            }
        }

        public FlightSimulatorViewModel Main_VM { get; internal set; }
        private void ClickConnect(object sender, RoutedEventArgs e)
        {
            ConnectButton.Foreground = new SolidColorBrush(Colors.Blue);
            ConnectionSettings cs = new ConnectionSettings(cvm);
            cs.Show();
        }
        private void WindowClosing(object sender, CancelEventArgs e)
        {
            cvm.disconnect();
            Application.Current.Shutdown();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            BrowseButton.Foreground = new SolidColorBrush(Colors.Blue);
            DataSettings ds = new DataSettings(dvm);
            ds.Show();
        }
    }
}
