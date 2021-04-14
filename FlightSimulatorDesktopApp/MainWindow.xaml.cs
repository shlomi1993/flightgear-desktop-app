using FlightSimulatorDesktopApp.ViewModel;
using FlightSimulatorDesktopApp.Model;
using FlightSimulatorDesktopApp.View;
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
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Main_VM = (Application.Current as App).MainVM;
            DataContext = Main_VM;
            InitializeComponent();
        }

        public FlightSimulatorViewModel Main_VM { get; internal set; }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Main_VM.start();
        //}
    }
}
