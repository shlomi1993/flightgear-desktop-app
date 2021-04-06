using FlightSimulatorDesktopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.ViewModel
{
    public partial class ConnectionViewModel : INotifyPropertyChanged
    {
        public IFlightSimulatorModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public ConnectionViewModel(IFlightSimulatorModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void connect(string ip, int port)
        {
            try
            {
                model.connect(ip, port);
            } catch (Exception)
            {
                // need to notify the view.
            }
            
        }
        public void disconnect()
        {
            model.disconnect();
        }

    }
}
