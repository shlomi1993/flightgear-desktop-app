using FlightSimulatorDesktopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.ViewModel
{
    public class AnomalyDetectionViewModel : INotifyPropertyChanged
    {
        private IAnomalyDetectionModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public AnomalyDetectionViewModel(IAnomalyDetectionModel adm)
        {
            model = adm;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public IAnomalyDetectionModel ADM { get => model; }
        public string VM_Output { get => model.Output; }

        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Detect(string anomaliousFilePath, string algoFilePath, float threshold)
        {
            model.Detect(anomaliousFilePath, algoFilePath, threshold);
        }

    }


}
