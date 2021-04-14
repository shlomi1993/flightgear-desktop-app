using FlightSimulatorDesktopApp.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorDesktopApp.View
{
    /// <summary>
    /// Interaction logic for AnomalyDetectionView.xaml
    /// </summary>
    public partial class AnomalyDetectionView : UserControl
    {
        private readonly AnomalyDetectionViewModel advm;
        private float threshold;
        private string anomaliousFilePath;
        private string DLLFilePath;

        private bool CSVIsSet;
        private bool DLLIsSet;
        private bool thresholdIsSet;


        public AnomalyDetectionView()
        {
            InitializeComponent();
            if (Application.Current is App)
            {
                Main_VM = (Application.Current as App).MainVM;
                advm = Main_VM.AnomalyDetectionVM;
                DataContext = advm;
                threshold = -1;
                CSVIsSet = false;
                DLLIsSet = false;
                thresholdIsSet = false;
                Run_Button.IsEnabled = false;
                txtPath_DLL.IsReadOnly = true;
                txtPath_CSV.IsReadOnly = true;

            }
        }

        //public AnomalyDetectionViewModel ADVM { get => advm; }

        public FlightSimulatorViewModel Main_VM { get; internal set; }

        //private void Browse_Click(object sender, RoutedEventArgs e)
        //{
        //    LinearAnomalyWindows cs = new LinearAnomalyWindows(advm);
        //    cs.Show();
        //}

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                advm.Detect(anomaliousFilePath, DLLFilePath, threshold);
                AnomalyDetectionResultWindow adrw = new AnomalyDetectionResultWindow(advm);
                adrw.Show();

            }
            catch (Exception)
            {
                Debug.WriteLine("Problem in OK_Click");     // Remove in the end.
            }
        }

        private void DLL_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtPath_DLL.Text = openFileDialog.FileName;
                DLLFilePath = openFileDialog.FileName;
            }
            DLLIsSet = true;
            updateRunButton();
        }

        private void CSV_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                txtPath_CSV.Text = openFileDialog.FileName;
                anomaliousFilePath = openFileDialog.FileName;
            }
            CSVIsSet = true;
            updateRunButton();
        }

        private void threshold_changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                float temp = (float)Double.Parse(txtCorr.Text);
                if (0 < temp && temp < 1)
                {
                    threshold = temp;
                    thresholdIsSet = true;
                }
                else
                {
                    threshold = -1;
                    thresholdIsSet = false;
                }

            }
            catch (Exception)
            {
                threshold = -1;
                thresholdIsSet = false;
            }
            finally
            {
                updateRunButton();
            }

        }
        private void updateRunButton()
        {
            if (CSVIsSet && DLLIsSet && thresholdIsSet)
            {
                Run_Button.IsEnabled = true;
            }
            else
            {
                Run_Button.IsEnabled = false;
            }
        }

        private void txtPath_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPath_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
