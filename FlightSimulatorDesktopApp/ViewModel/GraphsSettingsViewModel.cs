using FlightSimulatorDesktopApp.Model;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.ViewModel
{
    public partial class GraphsSettingsViewModel : INotifyPropertyChanged
    {
        public IFlightSimulatorModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public GraphsSettingsViewModel(IFlightSimulatorModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            
        }

        public GraphsSettingsViewModel()
        {
            //Added graph shows history of chosen property
            this.HistoryModel = new PlotModel { Title = "Property: $$$" };
            this.HistoryModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

            //Added graph shows most corelative to chosen property
            this.CorelativeModel = new PlotModel { Title = "Most corelative property: $$$" };
            this.CorelativeModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

            //Added graph shows linear regression between 2 properties
            this.LinRegModel = new PlotModel { Title = "Linear regression property: X property: Y" };
            this.LinRegModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }
        public PlotModel HistoryModel { get; private set; }
        public PlotModel CorelativeModel { get; private set; }
        public PlotModel LinRegModel { get; private set; }
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
