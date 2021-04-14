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
        // Starter method.
        void start();
        void play();
        void pause();
        void stop();

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

        // Additional Properties.
        int IRow { get; set; }
        int NumOfRows { get; }
        double Speed { get; set; }
        double Time { get; set; }
        public IDataModel DataModel { get; }

    }

}
