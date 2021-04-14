using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.Model
{
    public interface IAnomalyDetectionModel : INotifyPropertyChanged
    {
        public string Output { get; set; }
        public void Detect(string anomaliousFilePath, string algoFilePath, float threshold);
        public IDataModel DataModel { get; }

    }
    public class AnomalyDetectionModel : IAnomalyDetectionModel
    {
        // Privates.
        private string output;
        private IDataModel dm;

        // Nofifier.
        public event PropertyChangedEventHandler PropertyChanged;
        
        // DLL managment methods.

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllPath);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr module, string name);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr module);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void runAnomalyDetectionAlgorithm(float t);

        // Constructor.
        public AnomalyDetectionModel(IDataModel dm)
        {
            this.dm = dm;
            output = default(string);
        }

        // Properties.
        public IDataModel DataModel { get => dm; }
        public string Output
        {
            get => output;
            set
            {
                if (output != value)
                {
                    output = value;
                    NotifyPropertyChanged("Output");
                }
            }
        }

        // Notification method.
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Anomaly Detection method.
        public void Detect(string anomaliousFilePath, string algoFilePath, float threshold)
        {
            // Remove anomalies.txt file if exists.
            try { File.Delete("anomalies.txt"); }
            catch (Exception) { Debug.WriteLine("Problem in anomalies.txt removal."); }

            // Create test file.
            dm.createDataCSV(anomaliousFilePath, "anomalyTest.csv");

            // Load dll.
            IntPtr dll = LoadLibrary(algoFilePath);
            if (dll == IntPtr.Zero)
            {
                Debug.WriteLine("Problem in detect");
                return;
            }

            // Get address of the loaded-dll in the memory.
            IntPtr address = GetProcAddress(dll, typeof(runAnomalyDetectionAlgorithm).Name);
            if (address == IntPtr.Zero)
            {
                Debug.WriteLine("Couldn't find the function " + typeof(runAnomalyDetectionAlgorithm).Name);
                return;
            }

            // Create instance of the function runAnomalyDetectionAlgorithm();
            runAnomalyDetectionAlgorithm runAnomalyDetectionAlgorithm =
                (runAnomalyDetectionAlgorithm)Marshal.GetDelegateForFunctionPointer(address,
                typeof(runAnomalyDetectionAlgorithm));

            // Run algo.
            runAnomalyDetectionAlgorithm(threshold);

            // Free memory
            FreeLibrary(dll);

            // Read algo-dll output.
            Output = File.ReadAllText("anomalies.txt");
            
        }

        public PlotModel visualize()
        {
            string[] rows = output.Split("\n");
            int len = rows.Length;

            double[] timeSteps = new double[len];
            string[] propertiesA = new string[len];
            string[] propertiesB = new string[len];

            try
            {
                for (int i = 0; i < len; i++)
                {
                    string[] cols = rows[i].Split("\t");
                    timeSteps[i] = int.Parse(cols[0]);
                    string[] props = cols[1].Split("-");
                    propertiesA[i] = props[0];
                    propertiesB[i] = props[1];
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Error in AnomalyDetectionModel.visualize().");
            }

            PlotModel pm;
            return null; // to continue.
            
            
        }

    }
}
