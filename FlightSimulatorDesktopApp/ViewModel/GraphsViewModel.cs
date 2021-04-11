using FlightSimulatorDesktopApp.Model;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.ViewModel
{

    public class GraphsViewModel : INotifyPropertyChanged
    {
        public GraphsModel gm;

        public event PropertyChangedEventHandler PropertyChanged;

        public GraphsViewModel(GraphsModel gm)
        {
            this.gm = gm;
            gm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void setUpModel(PlotModel pm, string anotherProp, string chosenProp)
        {
            gm.setUpModel(pm, anotherProp, chosenProp);
        }

        public void LoadGraphData(PlotModel pm, string anotherProp, string chosenProp, bool time)
        {
            gm.LoadGraphData(pm, anotherProp, chosenProp, time);
        }
    }

}
