using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.Model
{
    public interface IGraphsModel : INotifyPropertyChanged
    {
        //Properties
        public string ChosenProp { get; set; }  
        public string AnotherProp { get; set; }  
        public PlotModel TimeModel { get; } //private set;? 
        public PlotModel CorrelativeModel { get; }
        public PlotModel LineRegModel { get;  }

        //Methods
        public void setUpModel(PlotModel pm, string anotherProp, string chosenProp);
        public void LoadGraphData(PlotModel pm, string anotherProp, string chosenProp, bool time);
    }
    public class GraphsModel : IGraphsModel
    {
        private IDataModel dm;
        private string chosenProp;
        private string anotherProp;
        private PlotModel timeModel = new PlotModel();
        private PlotModel correlativeModel = new PlotModel();
        private PlotModel lineRegModel = new PlotModel();

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

        public PlotModel TimeModel { get => timeModel; }

        public PlotModel CorrelativeModel { get => correlativeModel; }

        public PlotModel LineRegModel { get => lineRegModel; }

        //CHECK CONS
        public GraphsModel() { }
        public GraphsModel(IDataModel dm)
        {
            //chosenProp = null;
            //anotherProp = null;
            this.dm = dm;
            timeModel.Title = "Property chosen: " + chosenProp;
            correlativeModel.Title = "Correlative property: " + anotherProp;
            lineRegModel.Title = "Line Regression";

            //string anotherProp, chosenProp;
            anotherProp = "Time";
            chosenProp = "AirSpeed";

            //timeModel = new PlotModel { Title = "Property chosen: " + chosenProp };
            //timeModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            setUpModel(timeModel, anotherProp, chosenProp);
            LoadGraphData(timeModel, anotherProp, chosenProp, false);

            //TODO: use funcion to find correlative property to anotherProp, and put it in chosenProp
            chosenProp = ("GroundSpeed");

            //correlativeModel = new PlotModel { Title = "Correlative property: " + chosenProp };
            //correlativeModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            setUpModel(correlativeModel, anotherProp, chosenProp);
            LoadGraphData(timeModel, anotherProp, chosenProp, false);

            anotherProp = ("AirSpeed");

            //lineRegModel = new PlotModel { Title = "Line Regression" };
            //lineRegModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            setUpModel(lineRegModel, anotherProp, chosenProp);
            LoadGraphData(timeModel, anotherProp, chosenProp, true);
        }


        public event PropertyChangedEventHandler PropertyChanged;



        // Notification method.
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void setUpModel(PlotModel pm, string anotherProp, string chosenProp)
        {
            pm.LegendTitle = "Legend";
            pm.LegendOrientation = LegendOrientation.Horizontal;
            pm.LegendPlacement = LegendPlacement.Outside;
            pm.LegendPosition = LegendPosition.TopRight;
            pm.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            pm.LegendBorder = OxyColors.Black;

            //TODO: make option to change timeAxis to value (for userstory 8)
            var timeAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80, Title = anotherProp };
            pm.Axes.Add(timeAxis);
            timeAxis.Position = AxisPosition.Bottom;
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = chosenProp };
            pm.Axes.Add(valueAxis);
            valueAxis.Position = AxisPosition.Left;
        }

        //Load correct loadGraphData function
        public void LoadGraphData(PlotModel pm, string anotherProp, string chosenProp, bool time)
        {
            //Check if load graph with time axis or not
            if (time == false)
            {
                LoadGraphData1(pm, anotherProp, chosenProp);
            }
            else
            {
                LoadGraphData2(pm, anotherProp, chosenProp);
            }
        }

        //Load graph data for plot *with* time
        private void LoadGraphData1(PlotModel pm, string anotherProp, string chosenProp)
        {
            PropertyInfo[] properties = typeof(FlightSimulatorModel).GetProperties();
            int size = properties.Length;
            int i;
            int num1 = 0;
            LineSeries lineserie = new LineSeries();

            //Run on each property name and check for equal property
            for (i = 0; i < size; i++)
            {
                //Find correct property name and value
                if (properties[i].Name.Equals(anotherProp))
                {
                    //anotherProp = properties[i].Name;
                    num1 = i;
                    break;
                }
            }
            //Add data points of time = X and anotherProp = Y
            for (int j = 0; j < dm.Rows; j++)
            {
                lineserie.Points.Add(new DataPoint(j, dm.getDataFrom(j, num1)));  //values of time? right now its j for testing
            }
            pm.Series.Add(lineserie);
            //NEED TO INSERT TIME VALUES TO chosenPropValues

            //NotifyPropertyChanged("Points");

        }

        //Load graph data for plot *without* time
        private void LoadGraphData2(PlotModel pm, string anotherProp, string chosenProp)
        {
            PropertyInfo[] properties = typeof(FlightSimulatorModel).GetProperties();
            int size = properties.Length;
            int i;
            int num1 = 0, num2 = 0;
            LineSeries lineserie = new LineSeries();

            //Run on each property name and check for equal property
            for (i = 0; i < size; i++)
            {
                //Find correct properties names and values
                if (properties[i].Name.Equals(anotherProp))
                {
                    //anotherProp = properties[i].Name;
                    num1 = i;
                }
                if (properties[i].Name.Equals(chosenProp))
                {
                    //chosenProp = properties[i].Name;
                    num2 = i;
                }
            }
            //Add data points of anotherProp = X and chosenProp = Y
            for (int j = 0; j < dm.Rows; j++)
            {
                //TODO: Use LineReg func ==> Line, findout insert Line to graph
                //maybe -- lineRegModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)")); ?
                lineserie.Points.Add(new DataPoint(num1, num2));
            }
            pm.Series.Add(lineserie);


        }

        /////////////////////////////////////////////////////////////////////////////////////////
        //For me not needed right now

        //Add line of given points to given graph
        public void addLineGraph(PlotModel pm, List<DataPoint> points)
        {
            LineSeries lineserie = new LineSeries
            {
                ItemsSource = points,
                DataFieldX = "X",
                DataFieldY = "Y",
                StrokeThickness = 2,
                MarkerSize = 0,
                LineStyle = LineStyle.Solid,
                Color = OxyColors.Black,
                MarkerType = MarkerType.Circle,
            };

            pm.Series.Add(lineserie);

        }

        //Add line of given points to given graph
        public void addLineGraph2(PlotModel pm, DataPoint[] dataPointsArr)   //Way to create sort of DataPoint[]? other than adding each point
        {
            FunctionSeries fs = new FunctionSeries();
            foreach (DataPoint dataPoint in dataPointsArr)
            {
                fs.Points.Add(dataPoint);
            }
            pm.Series.Add(fs);
        }
    }
}
