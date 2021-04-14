using FlightSimulatorDesktopApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.ViewModel
{
    public class ControllersViewModel : INotifyPropertyChanged
    {
        public IFlightSimulatorModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public ControllersViewModel(IFlightSimulatorModel m)
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
        public void ControllersProperties()
        {
        }
        // Properties
        public double VM_Aileron
        {
            get { return model.Aileron; }
            set
            {
                model.Aileron = value;
            }
        }
        public double VM_Elevator
        {
            get { return model.Elevator * 65 + 125; }
            set
            {
                model.Elevator = value;
            }
        }
        public double VM_Throttle1
        {
            get { return model.Throttle1; }
            set
            {
                model.Throttle1 = value;
            }
        }
        public double VM_Throttle2
        {
            get { return model.Throttle2; }
            set
            {
                model.Throttle2 = value;
            }
        }
        public double VM_Rudder
        {
            get { return model.Rudder; }
            set
            {
                model.Rudder = value;
            }
        }
        public double VM_AltimeterAltitude
        {
            get { return model.AltimeterAltitude; }
            set
            {
                model.AltimeterAltitude = value;
            }
        }
        public double VM_AirSpeed
        {
            get { return model.AirSpeed; }
            set
            {
                model.AirSpeed = value;
            }
        }
        public double VM_AttitudeIndicatedPitch
        {
            get { return model.AttitudeIndicatedPitch; }
            set
            {
                model.AttitudeIndicatedPitch = value;
            }
        }
        public double VM_AttitudeIndicatedRoll
        {
            get { return model.AttitudeIndicatedRoll; }
            set
            {
                model.AttitudeIndicatedRoll = value;
            }
        }
        public double VM_IndicatedHeading
        {
            get { return model.IndicatedHeading; }
            set
            {
                model.IndicatedHeading = value;
            }
        }
        public double VM_SideSlip
        {
            get { return model.SideSlip; }
            set
            {
                model.SideSlip = value;
            }
        }
    }
}