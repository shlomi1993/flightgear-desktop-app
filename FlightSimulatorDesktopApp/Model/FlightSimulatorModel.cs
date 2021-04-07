using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.Model
{
    public class FlightSimulatorModel : IFlightSimulatorModel
    {
        // Norifier.
        public event PropertyChangedEventHandler PropertyChanged;

        // Connection and Data privates.
        private IConnectionModel telnetClient;
        private IDataModel database;
        private bool isConnected;

        // Simulator's data privates.
        private double aileron;
        private double elevator;
        private double rudder;
        private double flaps;
        private double slats;
        private double speedbrake;
        private double throttle1;
        private double throttle2;
        private double enginePump1;
        private double enginePump2;
        private double electricPump1;
        private double electricPump2;
        private double externalPower;
        private double _APUGenerator;
        private double latitude;
        private double longitude;
        private double altitude;
        private double roll;
        private double pitch;
        private double heading;
        private double sideSlip;
        private double airSpeed;
        private double glideslope;
        private double verticalSpeed;
        private double airspeedIndicator;
        private double altimeterAltitude;
        private double altimeterPressure;
        private double attitudeIndicatedPitch;
        private double attitudeIndicatedRoll;
        private double attitudeInternalPitch;
        private double attitudeInternalRoll;
        private double encoderIndicated;
        private double encoderPressure;
        private double gpsAltitude;
        private double gpsGroundSpeed;
        private double gpsVerticalSpeed;
        private double indicatedHeading;
        private double magneticCompass;
        private double slipSkidBall;
        private double turn;
        private double verticalSpeedIndicator;
        private double engineRPM;

        // Constructor.
        public FlightSimulatorModel(ConnectionModel cm, DataModel dm)
        {
            telnetClient = cm;
            database = dm;
            isConnected = false;
        }

        // Simulator Properties.
        public double Aileron
        {
            get => aileron;
            set
            {
                if (aileron != value)
                {
                    aileron = value;
                    NotifyPropertyChanged("Aileron");
                }
            }
        }
        public double Elevator
        {
            get => elevator;
            set
            {
                if (elevator != value)
                {
                    elevator = value;
                    NotifyPropertyChanged("Elevator");
                }
            }
        }
        public double Rudder
        {
            get => rudder;
            set
            {
                if (rudder != value)
                {
                    rudder = value;
                    NotifyPropertyChanged("Rudder");
                }
            }
        }
        public double Flaps
        {
            get => flaps;
            set
            {
                if (flaps != value)
                {
                    flaps = value;
                    NotifyPropertyChanged("Flaps");
                }
            }
        }
        public double Slats
        {
            get => slats;
            set
            {
                if (slats != value)
                {
                    slats = value;
                    NotifyPropertyChanged("Slats");
                }
            }
        }
        public double Speedbrake
            {
            get => speedbrake;
            set {
                if (speedbrake != value)
                {
                    speedbrake = value;
                    NotifyPropertyChanged("Speedbrake");
                }
            }
        }
        public double Throttle1
        {
            get => throttle1;
            set
            {
                if (throttle1 != value)
                {
                    throttle1 = value;
                    NotifyPropertyChanged("Throttle1");
                }
            }
        }
        public double Throttle2
        {
            get => throttle2;
            set
            {
                if (throttle2 != value)
                {
                    throttle2 = value;
                    NotifyPropertyChanged("Throttle2");
                }
            }
        }
        public double EnginePump1
        {
            get => enginePump1;
            set
            {
                if (enginePump1 != value)
                {
                    enginePump1 = value;
                    NotifyPropertyChanged("EnginePump1");
                }
            }
        }
        public double EnginePump2
        {
            get => enginePump2;
            set
            {
                if (enginePump2 != value)
                {
                    enginePump2 = value;
                    NotifyPropertyChanged("EnginePump2");
                }
            }
        }
        public double ElectricPump1
        {
            get => electricPump1;
            set
            {
                if (electricPump1 != value)
                {
                    electricPump1 = value;
                    NotifyPropertyChanged("ElectricPump1");
                }
            }
        }
        public double ElectricPump2
        {
            get => electricPump2;
            set
            {
                if (electricPump2 != value)
                {
                    electricPump2 = value;
                    NotifyPropertyChanged("ElectricPump2");
                }
            }
        }
        public double ExternalPower
        {
            get => externalPower;
            set
            {
                if (externalPower != value)
                {
                    externalPower = value;
                    NotifyPropertyChanged("ExternalPower");
                }
            }
        }
        public double APUGenerator
        {
            get => _APUGenerator;
            set
            {
                if (_APUGenerator != value)
                {
                    _APUGenerator = value;
                    NotifyPropertyChanged("APUGenerator");
                }
            }
        }
        public double Latitude
        {
            get => latitude;
            set
            {
                if (latitude != value)
                {
                    latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }
        public double Longitude
        {
            get => longitude;
            set
            {
                if (longitude != value)
                {
                    longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }
        public double Altitude
        {
            get => altitude;
            set
            {
                if (altitude != value)
                {
                    altitude = value;
                    NotifyPropertyChanged("Altitude");
                }
            }
        }
        public double Roll
        {
            get => roll;
            set
            {
                if (roll != value)
                {
                    roll = value;
                    NotifyPropertyChanged("Roll");
                }
            }
        }
        public double Pitch
        {
            get => pitch;
            set
            {
                if (pitch != value)
                {
                    pitch = value;
                    NotifyPropertyChanged("Pitch");
                }
            }
        }
        public double Heading
        {
            get => heading;
            set
            {
                if (heading != value)
                {
                    heading = value;
                    NotifyPropertyChanged("Heading");
                }
            }
        }
        public double SideSlip
        {
            get => sideSlip;
            set
            {
                if (sideSlip != value)
                {
                    sideSlip = value;
                    NotifyPropertyChanged("SideSlip");
                }
            }
        }
        public double AirSpeed
        {
            get => airSpeed;
            set
            {
                if (airSpeed != value)
                {
                    airSpeed = value;
                    NotifyPropertyChanged("AirSpeed");
                }
            }
        }
        public double Glideslope
        {
            get => glideslope;
            set
            {
                if (glideslope != value)
                {
                    glideslope = value;
                    NotifyPropertyChanged("Glideslope");
                }
            }
        }
        public double VerticalSpeed
        {
            get => verticalSpeed;
            set
            {
                if (verticalSpeed != value)
                {
                    verticalSpeed = value;
                    NotifyPropertyChanged("VerticalSpeed");
                }
            }
        }
        public double AirspeedIndicator
        {
            get => airspeedIndicator;
            set
            {
                if (airspeedIndicator != value)
                {
                    airspeedIndicator = value;
                    NotifyPropertyChanged("AirspeedIndicator");
                }
            }
        }
        public double AltimeterAltitude
        {
            get => altimeterAltitude;
            set
            {
                if (altimeterAltitude != value)
                {
                    altimeterAltitude = value;
                    NotifyPropertyChanged("AltimeterAltitude");
                }
            }
        }
        public double AltimeterPressure
        {
            get => altimeterPressure;
            set
            {
                if (altimeterPressure != value)
                {
                    altimeterPressure = value;
                    NotifyPropertyChanged("AltimeterPressure");
                }
            }
        }
        public double AttitudeIndicatedPitch
        {
            get => attitudeIndicatedPitch;
            set
            {
                if (attitudeIndicatedPitch != value)
                {
                    attitudeIndicatedPitch = value;
                    NotifyPropertyChanged("AttitudeIndicatedPitch");
                }
            }
        }
        public double AttitudeIndicatedRoll
        {
            get => attitudeIndicatedRoll;
            set
            {
                if (attitudeIndicatedRoll != value)
                {
                    attitudeIndicatedRoll = value;
                    NotifyPropertyChanged("AttitudeIndicatedRoll");
                }
            }
        }
        public double AttitudeInternalPitch
        {
            get => attitudeInternalPitch;
            set
            {
                if (attitudeInternalPitch != value)
                {
                    attitudeInternalPitch = value;
                    NotifyPropertyChanged("AttitudeInternalPitch");
                }
            }
        }
        public double AttitudeInternalRoll
        {
            get => attitudeInternalRoll;
            set
            {
                if (attitudeInternalRoll != value)
                {
                    attitudeInternalRoll = value;
                    NotifyPropertyChanged("AttitudeInternalRoll");
                }
            }
        }
        public double EncoderIndicated
        {
            get => encoderIndicated;
            set
            {
                if (encoderIndicated != value)
                {
                    encoderIndicated = value;
                    NotifyPropertyChanged("EncoderIndicated");
                }
            }
        }
        public double EncoderPressure
        {
            get => encoderPressure;
            set
            {
                if (encoderPressure != value)
                {
                    encoderPressure = value;
                    NotifyPropertyChanged("EncoderPressure");
                }
            }
        }
        public double GpsAltitude
        {
            get => gpsAltitude;
            set
            {
                if (gpsAltitude != value)
                {
                    gpsAltitude = value;
                    NotifyPropertyChanged("GpsAltitude");
                }
            }
        }
        public double GpsGroundSpeed
        {
            get => gpsGroundSpeed;
            set
            {
                if (gpsGroundSpeed != value)
                {
                    gpsGroundSpeed = value;
                    NotifyPropertyChanged("GpsGroundSpeed");
                }
            }
        }
        public double GpsVerticalSpeed
        {
            get => gpsVerticalSpeed;
            set
            {
                if (gpsVerticalSpeed != value)
                {
                    gpsVerticalSpeed = value;
                    NotifyPropertyChanged("GpsVerticalSpeed");
                }
            }
        }
        public double IndicatedHeading
        {
            get => indicatedHeading;
            set
            {
                if (indicatedHeading != value)
                {
                    indicatedHeading = value;
                    NotifyPropertyChanged("IndicatedHeading");
                }
            }
        }
        public double MagneticCompass
        {
            get => magneticCompass;
            set
            {
                if (magneticCompass != value)
                {
                    magneticCompass = value;
                    NotifyPropertyChanged("MagneticCompass");
                }
            }
        }
        public double SlipSkidBall
        {
            get => slipSkidBall;
            set
            {
                if (slipSkidBall != value)
                {
                    slipSkidBall = value;
                    NotifyPropertyChanged("SlipSkidBall");
                }
            }
        }
        public double Turn
        {
            get => turn;
            set
            {
                if (turn != value)
                {
                    turn = value;
                    NotifyPropertyChanged("Turn");
                }
            }
        }
        public double VerticalSpeedIndicator
        {
            get => verticalSpeedIndicator;
            set
            {
                if (verticalSpeedIndicator != value)
                {
                    verticalSpeedIndicator = value;
                    NotifyPropertyChanged("VerticalSpeedIndicator");
                }
            }
        }
        public double EngineRPM {
            get => engineRPM;
            set
            {
                if (engineRPM != value)
                {
                    engineRPM = value;
                    NotifyPropertyChanged("EngineRPM");
                }
            }
        }

        // Connection checker.
        public bool IsConnected { get => isConnected; }

        // Notification method.
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Starter method.
        public void start()
        {
            new Thread(delegate () {
                var rows = System.IO.File.ReadLines(database.FilePath);
                foreach (string row in rows)
                {
                    telnetClient.write(row);
                    PropertyInfo[] properties = typeof(FlightSimulatorModel).GetProperties();
                    string[] splitted = row.Split(",");
                    int size = splitted.Length;
                    for (int i = 0; i < size; i++)
                    {
                        properties[i].SetValue(this, Double.Parse(splitted[i]));
                    }
                    Thread.Sleep(100); // read the data in 10Hz - needs to be according to playback file.
                }
                telnetClient.disconnect();
                return;
            }).Start();

        }

    }
}