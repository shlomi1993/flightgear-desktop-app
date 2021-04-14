using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.Model
{
    public interface IGraphsModel : INotifyPropertyChanged
    {
        //Properties
        public string ChosenProp { get; set; }
        public string AnotherProp { get; set; }
        public PlotModel ChosenModel { get; set; }
        public PlotModel CorrelativeModel { get; set; }
        public PlotModel LineRegModel { get; set; }
        public void setGraphs(string propName);
        public void updateGraphs();


    }
    class GraphsModel : IGraphsModel
    {
        private IFlightSimulatorModel fsm;
        private IDataModel dm;

        private string chosenProp;
        private string anotherProp;

        private PlotModel chosenModel = new PlotModel();
        private PlotModel correlativeModel = new PlotModel();
        private PlotModel lineRegModel = new PlotModel();

        private double[] timeArray;
        private double[] chosenArray;
        private double[] correlatedArray;

        // Notifier.
        public event PropertyChangedEventHandler PropertyChanged;

        [DllImport("AnomalyDetectionUtilLib.dll")]
        public static extern float pearson(float[] x, float[] y, int size);

        public GraphsModel(IFlightSimulatorModel fsm)
        {
            this.fsm = fsm;
            this.dm = fsm.DataModel;

            chosenProp = default(string);
            anotherProp = default(string);
        }

        public string ChosenProp
        {
            get => chosenProp;
            set
            {
                if (!chosenProp.Equals(value))
                {
                    chosenProp = value;
                    NotifyPropertyChanged("ChosenProp");
                }
            }
        }

        public string AnotherProp
        {
            get => anotherProp;
            set
            {
                if (!anotherProp.Equals(value))
                {
                    anotherProp = value;
                    NotifyPropertyChanged("AnotherProp");
                }
            }
        }

        public PlotModel ChosenModel
        {
            get => chosenModel;
            set
            {
                if (chosenModel != value)
                {
                    chosenModel = value;
                    NotifyPropertyChanged("ChosenModel");
                }
            }
        }

        public PlotModel CorrelativeModel
        {
            get => correlativeModel;
            set
            {
                if (correlativeModel != value)
                {
                    correlativeModel = value;
                    NotifyPropertyChanged("CorrelativeModel");
                }
            }
        }

        public PlotModel LineRegModel
        {
            get => lineRegModel;
            set
            {
                if (lineRegModel != value)
                {
                    lineRegModel = value;
                    NotifyPropertyChanged("LineRegModel");
                }
            }
        }

        public void initTime()
        {
            timeArray = new double[dm.getNumOfRows()];
            double frametime = 100 / fsm.Speed;
            int numOfRows = dm.getNumOfRows();

            for (int i = 0; i < numOfRows; i++)
            {
                timeArray[i] = frametime * i;
            }
        }

        // Notification method.
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public PlotModel createGraph(double[] x, double[] y, string title, string axisXName, string axisYName)
        {
            //Change series to scatter series for US8
            var series = new LineSeries();
            series.Title = "Value";
            int size = dm.getNumOfRows();
            for (int i = 0; i < fsm.IRow; i++)
            {
                series.Points.Add(new DataPoint(x[i], y[i]));
            }
            PlotModel pm = new PlotModel();
            pm.Series.Add(series);
            pm.Title = title;

            pm.LegendTitle = "Legend";
            pm.LegendOrientation = LegendOrientation.Horizontal;
            pm.LegendPlacement = LegendPlacement.Outside;
            pm.LegendPosition = LegendPosition.TopRight;
            pm.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            pm.LegendBorder = OxyColors.Black;

            var axisX = new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = anotherProp
            };
            pm.Axes.Add(axisX);
            axisX.Position = AxisPosition.Bottom;

            var axisY = new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Title = chosenProp
            };
            pm.Axes.Add(axisY);
            axisY.Position = AxisPosition.Left;

            return pm;
        }

        public int findIndex(string propName)
        {
            PropertyInfo[] properties = typeof(FlightSimulatorModel).GetProperties();
            int size = properties.Length;
            int i = 0;
            while (!properties[i].Name.Equals(propName)) { i++; }
            return i;
        }

        public float[] convertToFloatArray(double[] darray)
        {
            int size = darray.Length;
            float[] farray = new float[size];
            for (int i = 0; i < size; i++)
            {
                farray[i] = (float)darray[i];
            }
            return farray;
        }

        public void setGraphs(string propName)
        {
            chosenProp = propName;

            // Properties of FlightSimulatorModel.
            PropertyInfo[] properties = typeof(FlightSimulatorModel).GetProperties();

            // Load chosen property array.
            int chosenIndex = findIndex(propName);
            chosenArray = dm.getColumn(chosenIndex);

            // Load most correlated property array.
            int correlatedIndex = 0;
            int numOfCols = dm.getNumOfColumns();
            float maxPearson = 0;
            float newPearson = 0;
            float[] chosenData = convertToFloatArray(dm.getColumn(chosenIndex));
            for (int i = 0; i < numOfCols; i++)
            {
                if (i != chosenIndex)
                {
                    float[] potential = convertToFloatArray(dm.getColumn(i));
                    try
                    {
                        newPearson = Math.Abs(pearson(chosenData, potential, numOfCols));
                        if (newPearson > maxPearson)
                        {
                            maxPearson = newPearson;
                            correlatedIndex = i;
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("Problem in pearson dll.");
                    }
                }
            }
            anotherProp = properties[correlatedIndex].Name;
            correlatedArray = dm.getColumn(correlatedIndex);

            // Load time array.
            initTime();

            // Update Graphs PlotModels.
            string title1 = chosenProp + " in relation to Time.";
            ChosenModel = createGraph(timeArray, chosenArray, title1, "Time", chosenProp);
            string title2 = anotherProp + " in relation to Time.";
            CorrelativeModel = createGraph(timeArray, correlatedArray, title2, "Time", anotherProp);
            string title3 = chosenProp + " in relation to " + anotherProp;
            LineRegModel = createGraph(chosenArray, correlatedArray, title3, chosenProp, anotherProp);

        }

        public void updateGraphs()
        {
            int IRow = 0;
            int Speed = 1;
            int NumOfRows = this.dm.getNumOfRows();

            Thread mainThread = new Thread(delegate ()
            {
                while (IRow < NumOfRows)
                {
                    PropertyInfo[] properties = typeof(FlightSimulatorModel).GetProperties();
                    int size = dm.getNumOfColumns();
                    setGraphs(chosenProp);
                    IRow = IRow + 1;
                    Thread.Sleep((int)(100 * (1 / Speed)));
                }
                return;
            });
            mainThread.Start();
        }
    }
}
