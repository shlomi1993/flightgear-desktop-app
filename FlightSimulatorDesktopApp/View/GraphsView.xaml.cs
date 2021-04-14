using FlightSimulatorDesktopApp.Model;
using FlightSimulatorDesktopApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl
    {
        private readonly GraphsViewModel gvm;
        public List<String> features;
        public GraphsView()
        {

            InitializeComponent();

            PropertyInfo[] properties = typeof(FlightSimulatorModel).GetProperties();

            foreach (PropertyInfo pi in properties)
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = pi.Name;
                featureListBox.Items.Add(newItem);
                if (pi.Name.Equals("EngineRPM")) { break; }
            }

            if (Application.Current is App)
            {
                Main_VM = (Application.Current as App).MainVM;
                gvm = Main_VM.GraphsVM;
                DataContext = gvm;
            }
        }

        public FlightSimulatorViewModel Main_VM { get; internal set; }

        private void featureListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (featureListBox.SelectedItem != null)
            {
                Console.WriteLine((featureListBox.SelectedItem as ListBoxItem).Content.ToString());
                ListBoxItem selected = featureListBox.SelectedItem as ListBoxItem;

                if (selected != null)
                {
                    gvm.setGraphs(selected.Content.ToString());
                    gvm.VM_ChosenModel.Series.Clear();
                    gvm.VM_CorrelativeModel.Series.Clear();
                    gvm.VM_LineRegModel.Series.Clear();
                    gvm.updateGraphs();
                    gvm.VM_ChosenModel.InvalidatePlot(true);
                    gvm.VM_CorrelativeModel.InvalidatePlot(true);
                    gvm.VM_LineRegModel.InvalidatePlot(true);

                }
            }

        }
    }


}
