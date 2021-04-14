using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulatorDesktopApp.Model;
using OxyPlot;

namespace FlightSimulatorDesktopApp.ViewModel
{
    public class GraphsViewModel : INotifyPropertyChanged
    {
        public IGraphsModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public GraphsViewModel(IGraphsModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }

        public PlotModel VM_ChosenModel { get => model.ChosenModel; }
        public PlotModel VM_CorrelativeModel { get => model.CorrelativeModel; }
        public PlotModel VM_LineRegModel { get => model.LineRegModel; }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void setGraphs(string chosenPropName)
        {
            model.setGraphs(chosenPropName);
        }

        public void updateGraphs()
        {
            model.updateGraphs();
        }
    }
}
