using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;

namespace FlightSimulatorDesktopApp.Model
{
    public interface IDataModel : INotifyPropertyChanged
    {
        public void loadData(string path);
        public double getDataFrom(int i, int j);
        public double[] getColumn(int j);
        public double[] getRow(int i);
        public string getStringRow(int i);
        public int getNumOfRows();
        public int getNumOfColumns();
        public string FilePath { get; }
        public void createDataCSV(string srcFilePath, string dstFileName);


    }
    public class DataModel : IDataModel
    {

        // Privates.
        private string filePath;
        private double[,] database;
        private int numOfRows;
        private int numOfColumns;
        private IEnumerable<string> rows;
        private PropertyInfo[] properties = typeof(FlightSimulatorModel).GetProperties();

        // Notifer.
        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor.
        public DataModel()
        {
            filePath = null;
            database = null;
            rows = null;
            numOfRows = 0;
            numOfColumns = 0;
        }

        // filePath property getter.
        public string FilePath { get => filePath; }

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
                this.rows = rows;
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
                createDataCSV(filePath, "anomalyTrain.csv");
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


        public string getStringRow(int i)
        {
            int k = 0;
            foreach (string row in this.rows)
            {
                if (k == i)
                {
                    return row;
                }
                k++;
            }
            return null;

        }

        public int getNumOfRows()
        {
            return numOfRows;
        }
        public int getNumOfColumns()
        {
            return numOfColumns;
        }

        public void createDataCSV(string srcFilePath, string dstFileName)
        {

            try { File.Delete(dstFileName); }
            catch (Exception) { }

            var rows = File.ReadLines(srcFilePath);
            string[] firstColumns = rows.First().Split(",");

            // Check if need headers.
            double n;
            bool noHeaders = Double.TryParse(firstColumns[0], out n);
            if (!noHeaders)
                rows = rows.Skip(1);

            // Create headers line.
            string headers = "";
            int i = 0;
            while (!properties[i].Name.Equals("EngineRPM"))
            {
                headers += properties[i].Name;
                headers += ",";
                i++;
            }
            headers += "EngineRPM";


            using (var sw = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), dstFileName)))
            {
                sw.WriteLine(headers);
                foreach (string row in rows)
                    sw.WriteLine(row);
            }
          
        }
    }
}
