using FlightSimulatorDesktopApp.Model;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.ViewModel
{

    public class GraphsViewModel
    {
        private string property1;
        private string property2;
        private DataModel dm;       //CHECK HOW TO IMPORT DATAMODEL

        //Create 3 graphs for graphs window
        public PlotModel timeModel { get; private set; }
        public PlotModel correlativeModel { get; private set; }
        public PlotModel lineRegModel { get; private set; }
        public GraphsViewModel()
        {
            property1 = "Time";
            property2 = "AirSpeed";

            timeModel = new PlotModel { Title = "Property chosen: " + property2 };
            //timeModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            setUpModel(timeModel, property1, property2);
            LoadGraphData(timeModel, false);

            //TODO: use funcion to find correlative property to property1, and put it in property2
            property2 = ("GroundSpeed");

            correlativeModel = new PlotModel { Title = "Correlative property: " + property2 };
            //correlativeModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            setUpModel(correlativeModel, property1, property2);
            LoadGraphData(correlativeModel, false);

            property1 = ("AirSpeed");

            lineRegModel = new PlotModel { Title = "Line Regression" };
            //lineRegModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            setUpModel(lineRegModel, property1, property2);
            LoadGraphData(correlativeModel, true);
        }

        //Define graph style and settings
        private void setUpModel(PlotModel pm, string property1, string property2)
        {

            pm.LegendTitle = "Legend";
            pm.LegendOrientation = LegendOrientation.Horizontal;
            pm.LegendPlacement = LegendPlacement.Outside;
            pm.LegendPosition = LegendPosition.TopRight;
            pm.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            pm.LegendBorder = OxyColors.Black;

            //TODO: make option to change timeAxis to value (for userstory 8)
            var timeAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80, Title = property1 };
            pm.Axes.Add(timeAxis);
            timeAxis.Position = AxisPosition.Bottom;
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = property2 };
            pm.Axes.Add(valueAxis);
            valueAxis.Position = AxisPosition.Left;
        }

        //Load correct loadGraphData function
        private void LoadGraphData(PlotModel pm, bool time)
        {
            //Check if load graph with time axis or not
            if (time == false)
            {
                LoadGraphData1(pm);
            }
            else
            {
                LoadGraphData2(pm);
            }
        }

        //Load graph data for plot *with* time
        private void LoadGraphData1(PlotModel pm)
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
                if (properties[i].Name.Equals(property1))
                {
                    property1 = properties[i].Name;
                    num1 = i;
                    break;
                }
            }
            //Add data points of time = X and property1 = Y
            for (int j = 0; j < dm.Columns; j++)
            {
                lineserie.Points.Add(new DataPoint(j, num1));  //values of time? right now its j for testing
            }
            pm.Series.Add(lineserie);
            //NEED TO INSERT TIME VALUES TO property2Values

            //NotifyPropertyChanged("Points");

        }

        //Load graph data for plot *without* time
        private void LoadGraphData2(PlotModel pm)
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
                if (properties[i].Name.Equals(property1))
                {
                    property1 = properties[i].Name;
                    num1 = i;
                }
                if (properties[i].Name.Equals(property2))
                {
                    property2 = properties[i].Name;
                    num2 = i;
                }
            }
            //Add data points of property1 = X and property2 = Y
            for (int j = 0; j < dm.Columns; j++)
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
