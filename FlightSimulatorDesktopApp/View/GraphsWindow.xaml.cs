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
using System.Windows.Shapes;

namespace FlightSimulatorDesktopApp.View
{
    /// <summary>
    /// Interaction logic for GraphsWindow.xaml
    /// </summary>
    public partial class GraphsWindow : Window
    {
        private GraphsViewModel gvm;
        public GraphsWindow(GraphsViewModel gvm)
        {
            InitializeComponent();
            this.gvm = gvm;
            DataContext = this.gvm.gm;
        }
    }
}
