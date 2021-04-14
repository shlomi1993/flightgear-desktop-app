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
using System.Windows.Shapes;
using System.Net;
using FlightSimulatorDesktopApp.ViewModel;
using System.Diagnostics;

namespace FlightSimulatorDesktopApp.View
{
    /// <summary>
    /// Interaction logic for ConnectionSettings.xaml
    /// </summary>
    public partial class ConnectionSettings : Window
    {
        private string ip;
        private int port;
        public ConnectionViewModel cvm;
        public ConnectionSettings(ConnectionViewModel cvm)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.cvm = cvm;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            IPAddress address;
            if (!IPAddress.TryParse(ip, out address) || port == default(int) || port < 0 || 65535 < port)
            {
                IPorPortError err = new IPorPortError();
                err.Show();
            }
            else
            {
                cvm.connect(ip, port);
                Close();
            }
        }

        private void IPChanged(object sender, TextChangedEventArgs e)
        {
            ip = txtIP.Text;
        }

        private void portChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                port = Int32.Parse(txtPort.Text);
            }
            catch (Exception)
            {
                port = default(int);
            }

        }
    }
}
