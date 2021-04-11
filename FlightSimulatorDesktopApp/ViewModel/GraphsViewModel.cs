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
    public partial class GraphsViewModel : INotifyPropertyChanged
    {
        private IGraphsModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public GraphsViewModel(IGraphsModel m)
        {
            model = m;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        //public string VM_Title
        //{
        //    get => model.Title;
        //}

        public string VM_DisplayValue { get; set; }
        public IList<DataPoint> VM_Points { get => model.GraphPoints; }

        public string VM_chosenProp { get => model.ChosenProp; }

        //public string VM_AxisXName { get => model.AxisXName; }
        //public string VM_AxisYName { get => model.AxisYName; }
        //public double[] VM_AxisX { get => model.AxisX; }
        //public double[] VM_AxisY { get => model.AxisY; }
    }


}
