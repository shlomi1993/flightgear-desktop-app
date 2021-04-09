using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.Model
{
    public interface IFlightSimulatorModel : INotifyPropertyChanged
    {
        // Connection to flight-gear.
        void connect(string ip, int port);
        void disconnect();
        void start();
        void startFrom(double speed, int row);
        void play();
        void pause();
        void stop();
        bool IsConnected { get; }


        // Data setter.
        public void loadData(string path);

        // Properties according to given playback_small.xml file
        double Aileron { get; set; }
        double Elevator { get; set; }
        double Rudder { get; set; }
        double Flaps { get; set; }
        double Slats { get; set; }
        double Speedbrake { get; set; }
        double Throttle1 { get; set; }
        double Throttle2 { get; set; }
        double EnginePump1 { get; set; }
        double EnginePump2 { get; set; }
        double ElectricPump1 { get; set; }
        double ElectricPump2 { get; set; }
        double ExternalPower { get; set; }
        double APUGenerator { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        double Altitude { get; set; }
        double Roll { get; set; }
        double Pitch { get; set; }
        double Heading { get; set; }
        double SideSlip { get; set; }
        double AirSpeed { get; set; }
        double Glideslope { get; set; }
        double VerticalSpeed { get; set; }
        double AirspeedIndicator { get; set; }
        double AltimeterAltitude { get; set; }
        double AltimeterPressure { get; set; }
        double AttitudeIndicatedPitch { get; set; }
        double AttitudeIndicatedRoll { get; set; }
        double AttitudeInternalPitch { get; set; }
        double AttitudeInternalRoll { get; set; }
        double EncoderIndicated { get; set; }
        double EncoderPressure { get; set; }
        double GpsAltitude { get; set; }
        double GpsGroundSpeed { get; set; }
        double GpsVerticalSpeed { get; set; }
        double IndicatedHeading { get; set; }
        double MagneticCompass { get; set; }
        double SlipSkidBall { get; set; }
        double Turn { get; set; }
        double VerticalSpeedIndicator { get; set; }
        double EngineRPM { get; set; }
        int IRow { get; set; }
        int NumOfRows { get; }
        double Speed { get; set; }


        //// Flight Controls.
        //double Aileron { get; set; }
        //double Elevator { get; set; }
        //double Rudder { get; set; }
        //double Flaps { get; set; }
        //double Slats { get; set; }
        //double Speedbrake { get; set; }
        //double Throttle { get; set; }

        //// Gear.
        //double EnginePump { get; set; }
        //double ElectricPump { get; set; }
        //double ExternalPower { get; set; }
        //double APUGenerator { get; set; }

        //// Position.
        //double Latitude { get; set; }
        //double Longtitude { get; set; }
        //double Altitude { get; set; }

        //// Orientation.
        //double Roll { get; set; }
        //double Pitch { get; set; }
        //double Yaw { get; set; }
        //string Heading { get; set; }
        //double SideSlip { get; set; }

        //// Velocities.
        //double AirSpeed { get; set; }
        //double Glideslope { get; set; }
        //string VerticalSpeed { get; set; }
        //string GroundSpeed { get; set; }
        //string Altimeter { get; set; }

    }

}
