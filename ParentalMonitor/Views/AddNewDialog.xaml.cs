using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ParentalMonitor.Classes;

namespace ParentalMonitor.Views
{
    /// <summary>
    /// Interaction logic for AddNewDialog.xaml
    /// </summary>
    public partial class AddNewDialog : Window
    {
        public string newProcessName = "";
        public TimeSpan newProcessAllowedRuntime;
        public bool timeSpanCanBeUsed = false;
        public bool firstTime = true;

        public AddNewDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            img_green.Visibility = Visibility.Hidden;
            img_red.Visibility = Visibility.Visible;
        }

        private void ___ParentalMonitor_component_76818_png_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(
                "Enter the exact process name like in the Task Manager - but without the file ending ('.exe'). You can also choose a running process from the List." + Environment.NewLine +" After that input the time in HH:MM.",
                "Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void tb_allowedRunTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!firstTime)
            {
                try
                {
                    newProcessAllowedRuntime = TimeSpan.Parse(tb_allowedRunTime.Text);
                    img_green.Visibility = Visibility.Visible;
                    img_red.Visibility = Visibility.Hidden;
                    timeSpanCanBeUsed = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    img_green.Visibility = Visibility.Hidden;
                    img_red.Visibility = Visibility.Visible;
                    timeSpanCanBeUsed = false;
                }
            }
            firstTime = false;
        }

        private void b_save_Click(object sender, RoutedEventArgs e)
        {
            newProcessName = tb_newProcessName.Text;
            if (String.IsNullOrWhiteSpace(tb_newProcessName.Text))
            {
                MessageBox.Show("Please enter a Process name or choose one from the List", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!timeSpanCanBeUsed)
            {
                MessageBox.Show("Please enter a valid Timespan", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            else if (timeSpanCanBeUsed)
            {
                var newProc = new RestrictedProcess
                {
                    actualRunningTime = TimeSpan.Zero,
                    allowedRunningTime = newProcessAllowedRuntime,
                    name = newProcessName,
                };
                if (App._settings.restrictedProcessesList == null) App._settings.restrictedProcessesList = new List<RestrictedProcess>();
                App._settings.restrictedProcessesList.Add(newProc);
                Close();
            }
        }

        private void b_showProcList_Click(object sender, RoutedEventArgs e)
        {
            ProcessListChooser proc = new ProcessListChooser();
            proc.Owner = this;
            proc.ShowDialog();
            newProcessName = proc.newProcessName;
            tb_newProcessName.Text = newProcessName;
        }

        private void ___ParentalMonitor_component_76818_png_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Hand;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void ___ParentalMonitor_component_76818_png_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void b_cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
