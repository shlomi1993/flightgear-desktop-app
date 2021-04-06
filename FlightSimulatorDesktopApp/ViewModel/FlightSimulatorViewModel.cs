using FlightSimulatorDesktopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.ViewModel
{
    public class FlightSimulatorViewModel : INotifyPropertyChanged
    {
        private IFlightSimulatorModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public FlightSimulatorViewModel(IFlightSimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public double VM_Aileron
        {
            get { return model.Aileron; }
        }

        public void start()
        {
            model.start();
        }
        // ... contineue with property getters for each model property.
    }
}
