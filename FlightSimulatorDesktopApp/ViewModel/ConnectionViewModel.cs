using FlightSimulatorDesktopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.ViewModel
{
    public partial class ConnectionViewModel : INotifyPropertyChanged
    {
        public IConnectionModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public ConnectionViewModel(IConnectionModel cm)
        {
            model = cm;
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
            model.connect(ip, port);            
        }
        public void disconnect()
        {
            model.disconnect();
        }
        public string VM_ConnectionStatus
        {
            get => model.ConnectionStatus;
        }

    }
}
