using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlightSimulatorDesktopApp.Model
{
    public class DataModel
    {
        private double[,] database;
        private int numOfRows;
        private int numOfColumns;
        private IEnumerable<string> rows;
        public DataModel(string filePath)
        {
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
        }
        public double getDataFrom(int i, int j)
        {
            return database[i, j];
        }
        public double[] getColumn(int j)
        {
            double[] col = new Double[numOfRows];
            for (int i = 0; i < numOfRows; i++)
            {
                col[i] = database[i, j];
            }
            return col;
        }
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
            foreach (string row in rows)
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
    }
}
