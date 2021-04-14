using FlightSimulatorDesktopApp.Model;
using FlightSimulatorDesktopApp.ViewModel;
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
        // Models privates.
        private IFlightSimulatorModel model;
        private IConnectionModel cm;
        private IDataModel dm;
        private IGraphsModel gm;
        private IAnomalyDetectionModel adm;

        // ViewModels privates.
        private readonly PlayerViewModel pvm;
        private readonly ConnectionViewModel cvm;
        private readonly DataViewModel dvm;
        private readonly ControllersViewModel covm;
        private readonly GraphsViewModel gvm;
        private readonly AnomalyDetectionViewModel advm;

        // Notifier.
        public event PropertyChangedEventHandler PropertyChanged;

        public FlightSimulatorViewModel(IFlightSimulatorModel model, IDataModel dm, IConnectionModel cm, IAnomalyDetectionModel adm, IGraphsModel gm)
        {
            this.model = model;
            this.cm = cm;
            this.dm = dm;
            this.gm = gm;
            this.adm = adm;
            this.pvm = new PlayerViewModel(this.model);
            this.cvm = new ConnectionViewModel(this.cm);
            this.dvm = new DataViewModel(this.dm);
            this.covm = new ControllersViewModel(this.model);
            this.gvm = new GraphsViewModel(this.gm);
            this.advm = new AnomalyDetectionViewModel(this.adm);

            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public PlayerViewModel PlayerVM
        {
            get { return pvm; }
        }

        public ControllersViewModel ControllersVM
        {
            get { return covm; }
        }

        public ConnectionViewModel ConnectionVM
        {
            get { return cvm; }
        }

        public DataViewModel DataVM
        {
            get { return dvm; }
        }

        public AnomalyDetectionViewModel AnomalyDetectionVM
        {
            get { return advm; }
        }

        public GraphsViewModel GraphsVM
        {
            get { return gvm; }
        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        //public void start()
        //{
        //    model.start();
        //}

    }
}
