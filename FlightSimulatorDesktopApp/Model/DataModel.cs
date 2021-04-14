using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace FlightSimulatorDesktopApp.Model
{
    public interface IDataModel : INotifyPropertyChanged
    {
        public void loadData(string path);
        public double getDataFrom(int i, int j);
        public double[] getColumn(int j);
        public double[] getRow(int i);
        public string FilePath { get; }

        public int Rows { get; }
        public int Columns { get; }


    }
    public class DataModel : IDataModel
    {

        // Privates.
        private string filePath;
        private double[,] database;
        private int numOfRows;
        private int numOfColumns;

        // Notifer.
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor.
        public DataModel()
        {
            filePath = null;
            database = null;
            numOfRows = 0;
            numOfColumns = 0;
        }

        // filePath property getter.
        public string FilePath { get => filePath; }


        public int Rows => numOfRows;

        public int Columns => numOfColumns;


        // Notification method.
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Data loader method.
        public void loadData(string path)
        {
            // Backup privates for case of error.
            string oldPath = filePath;
            int oldNumOfRows = numOfRows;
            int oldNumOfCols = numOfColumns;
            double[,] oldDB = database;

            // Update database.
            try
            {
                filePath = path;
                var rows = File.ReadLines(filePath);
                numOfRows = rows.Count();
                numOfColumns = rows.First().Split(",").Length;
                database = new Double[numOfRows, numOfColumns];
                int i = 0;
                foreach (string row in rows)
                {
                    string[] splitted = row.Split(",");
                    for (int j = 0; j < numOfColumns; j++)
                    {
                        database[i, j] = Double.Parse(splitted[j]);
                    }
                    i++;
                }
                NotifyPropertyChanged("FilePath");
            }

            // Restore in case something went wrong.
            catch (Exception)
            {
                filePath = oldPath;
                database = oldDB;
                numOfRows = oldNumOfRows;
                numOfColumns = oldNumOfCols;
            }

        }

        // This method allows to get a specific data from the database.
        public double getDataFrom(int i, int j)
        {
            return database[i, j];
        }

        // This method allows to get all the data of a specific property.
        public double[] getColumn(int j)
        {
            double[] col = new Double[numOfRows];
            for (int i = 0; i < numOfRows; i++)
            {
                col[i] = database[i, j];
            }
            return col;
        }

        // This method allows to get the data of all the fields at a specific point in time.
        public double[] getRow(int i)
        {
            double[] row = new Double[numOfColumns];
            for (int j = 0; j < numOfColumns; j++)
            {
                row[j] = database[i, j];
            }
            return row;
        }

    }
}
