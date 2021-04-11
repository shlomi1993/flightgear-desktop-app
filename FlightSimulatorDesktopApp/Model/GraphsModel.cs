using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
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
        //public string Title { get; set; }
        public IList<DataPoint> GraphPoints { get; set; }
        public string ChosenProp { get; set; }
        public string CorrelatedProp { get; set; }
        public double[] AxisX { get; set; }
        public double[] AxisY { get; set; }
        public void loadGraph(string propName);
        public void loadGraph(string propNameX, string propNameY);

    }
    public class GraphsModel : IGraphsModel
    {
        private DataModel dm;
        //private string title;
        private IList<DataPoint> graphPoints;
        private string chosenProp;
        private string correlatedProp;
        private double[] axisX;
        private double[] axisY;
        private PlotModel pm;

        public GraphsModel(DataModel dm)
        {
            this.dm = dm;
            //title = default(string);
            graphPoints = null;
            chosenProp = default(string);
            correlatedProp = default(string);
            axisX = null;
            axisY = null;
        }

        // TO IMPLEMENT!
        //public string Title
        //{
        //    get => title;
        //    set
        //    {
        //        if (!title.Equals(value))
        //        {
        //            title = value;
        //            NotifyPropertyChanged("title");
        //        }
        //    }
        //}

        public IList<DataPoint> GraphPoints
        {
            get => graphPoints;
            set
            {
                if (!graphPoints.SequenceEqual(value))
                {
                    graphPoints = value;
                    NotifyPropertyChanged("GraphPoints");
                }
            }
        }
        public string ChosenProp
        {
            get => chosenProp;
            set
            {
                if (!chosenProp.Equals(value))
                {
                    chosenProp = value;
                    NotifyPropertyChanged("AxisXName");
                }
            }
        }
        public string CorrelatedProp
        {
            get => correlatedProp;
            set
            {
                if (!correlatedProp.Equals(value))
                {
                    correlatedProp = value;
                    NotifyPropertyChanged("AxisYName");
                }
            }
        }
        public double[] AxisX
        {
            get => axisX;
            set
            {
                if (!axisX.SequenceEqual(value))
                {
                    axisX = value;
                    NotifyPropertyChanged("AxisX");
                }
            }
        }
        public double[] AxisY
        {
            get => axisY;
            set
            {
                if (!axisY.SequenceEqual(value))
                {
                    axisY = value;
                    NotifyPropertyChanged("AxisY");
                }
            }
        }

        public PlotModel PlotModel
        {
            get { return pm; }
            set { pm = value; NotifyPropertyChanged("PlotModel"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Notification method.
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Option 1: Y = prop, X = other time
        public void loadGraph(string propName)
        {
            CorrelatedProp = "Time";
            ChosenProp = propName;
            PropertyInfo[] properties = typeof(FlightSimulatorModel).GetProperties();
            int size = properties.Length;

            int i = 0;
            while (i < size)
            {
                if (properties[i].Name.Equals(propName))
                    break;
                i++;
            }
            AxisY = dm.getColumn(i);

            size = dm.Rows;
            for (int j = 0; j < size; j++)
            {
                axisX[j] = j;
            }
            //NotifyPropertyChanged("AxisX");

            for(int k = 0; k < size; k++)
            {
                graphPoints.Add(new DataPoint(axisX[k], axisY[k]));
            }
            NotifyPropertyChanged("GraphPoints");

        }

        // Option 2:
        public void loadGraph(string propNameX, string propNameY)
        {
            throw new NotImplementedException();
        }

        //Create graph with time as X axis, and chosen property as Y axis
        public void createGraphUS6()
        {
            pm = new PlotModel { Title = "Property = " + ChosenProp };

        }

        private void SetUpModel()
        {
            pm.LegendTitle = "Legend";
            pm.LegendOrientation = LegendOrientation.Horizontal;
            pm.LegendPlacement = LegendPlacement.Outside;
            pm.LegendPosition = LegendPosition.TopRight;
            pm.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            pm.LegendBorder = OxyColors.Black;

            var timeAxis = new OxyPlot.Axes.LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80 };
            pm.Axes.Add(timeAxis);
            timeAxis.Position = AxisPosition.Bottom;
            var valueAxis = new OxyPlot.Axes.LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            pm.Axes.Add(valueAxis);
            valueAxis.Position = AxisPosition.Left;
        }





    }
}
