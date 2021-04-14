using FlightSimulatorDesktopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace FlightSimulatorDesktopApp.ViewModel
{
    public partial class PlayerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulatorModel model;
        string time;
        public PlayerViewModel(IFlightSimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string Time
        {
            get { return time; }
            set
            {
                double calc = VM_IRow * (0.1 * (1 / VM_Speed));
                time = String.Format("{00:00:00}", calc);
            }
        }

        public void Start()
        {
            this.model.start();
        }
        public void Play()
        {
            this.model.play();
        }

        public void Pause()
        {
            this.model.pause();
        }

        public void Stop()
        {
            this.model.stop();
        }



        public int VM_IRow
        {
            get { return this.model.IRow; }
            set { this.model.IRow = value; }
        }

        public int VM_NumOfRows
        {
            get { return this.model.NumOfRows; }
            set { }
        }

        public double VM_Speed
        {
            get { return this.model.Speed; }
            set { this.model.Speed = value; }
        }

        public double VM_Time
        {
            get { return this.model.Time; }
            set { this.model.Time = value; }
        }
    }
}
