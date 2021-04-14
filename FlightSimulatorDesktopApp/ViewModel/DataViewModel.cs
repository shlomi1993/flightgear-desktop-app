using FlightSimulatorDesktopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.ViewModel
{
    public partial class DataViewModel : INotifyPropertyChanged
    {
        public IDataModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public DataViewModel(IDataModel dm)
        {
            model = dm;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public string VM_FilePath { get => model.FilePath; }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void LoadData(string filePath)
        {
            model.loadData(filePath);
        }
        
    }
}
