using FlightSimulatorDesktopApp.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for DataSettings.xaml
    /// </summary>
    public partial class DataSettings : Window
    {
        private string filePath;
        private DataViewModel dvm;
        public DataSettings(DataViewModel dvm)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.dvm = dvm;
        }
        private void openFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtPath.Text = openFileDialog.FileName;
                filePath = openFileDialog.FileName;
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            dvm.LoadData(filePath);
            Close();
        }
    }
}
