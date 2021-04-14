using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FlightSimulatorDesktopApp.Model;
using FlightSimulatorDesktopApp.View;
using FlightSimulatorDesktopApp.ViewModel;

namespace FlightSimulatorDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public FlightSimulatorViewModel MainVM { get; internal set; }

        public MainWindow MainWindowView { get; set; }
        public object Main_VM { get; internal set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IConnectionModel connectionModel = new ConnectionModel();
            IDataModel dataModel = new DataModel();
            IAnomalyDetectionModel anomalyDetectionModel = new AnomalyDetectionModel(dataModel);
            IFlightSimulatorModel model = new FlightSimulatorModel(connectionModel, dataModel);
            IGraphsModel graphsModel = new GraphsModel(model);
            MainVM = new FlightSimulatorViewModel(model, dataModel, connectionModel, anomalyDetectionModel, graphsModel);

            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
