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

namespace FlightSimulatorDesktopApp.View
{
    /// <summary>
    /// Interaction logic for IPorPortError.xaml
    /// </summary>
    public partial class IPorPortError : Window
    {
        public IPorPortError()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
