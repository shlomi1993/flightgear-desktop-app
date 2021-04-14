using FlightSimulatorDesktopApp.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for AnomalyDetectionResultWindow.xaml
    /// </summary>
    public partial class AnomalyDetectionResultWindow : Window
    {
        private AnomalyDetectionViewModel advm;
        public AnomalyDetectionResultWindow(AnomalyDetectionViewModel advm)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.advm = advm;
            DataContext = this.advm;
            txtOutput.IsReadOnly = true;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
