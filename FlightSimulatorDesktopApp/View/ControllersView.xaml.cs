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
using FlightSimulatorDesktopApp.ViewModel;
using FlightSimulatorDesktopApp.View;

namespace FlightSimulatorDesktopApp.View
{
    /// <summary>
    /// Interaction logic for ControllersView.xaml
    /// </summary>
    public partial class ControllersView : UserControl
    {
        private ControllersViewModel covm;
        public ControllersView()
        {
            InitializeComponent();
            if (Application.Current is App)
            {
                Main_VM = (Application.Current as App).MainVM;
                covm = Main_VM.ControllersVM;
                DataContext = covm;

            }
        }

        public FlightSimulatorViewModel Main_VM { get; internal set; }
    }
}
