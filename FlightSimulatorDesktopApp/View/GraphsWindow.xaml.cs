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
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Series;

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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.gvm = gvm;
            DataContext = this.gvm;
        }

        public void GraphUS6Load()
        {
            PlotView graph1 = new PlotView();
            graph1.Model = new PlotModel { Title = gvm.VM_chosenProp };
            FunctionSeries graph1fs = new FunctionSeries();
            //graph1fs.Points.A
        }
    }
}
