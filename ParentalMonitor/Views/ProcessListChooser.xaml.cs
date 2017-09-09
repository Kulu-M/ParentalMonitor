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
using System.Windows.Shapes;

namespace ParentalMonitor.Views
{
    /// <summary>
    /// Interaction logic for ProcessListChooser.xaml
    /// </summary>
    public partial class ProcessListChooser : Window
    {
        public string newProcessName = "";
        
        public ProcessListChooser()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var localProcesses = Process.GetProcesses();
            var localProcessesSorted =
                localProcesses.Where(x => x.SessionId == Process.GetCurrentProcess().SessionId)
                    .OrderBy(x => x.ProcessName);
            try
            {
                lb_proc.ItemsSource = localProcessesSorted;                
            }
            catch
            {
                // ignored
            }
        }

        private void b_select_Click(object sender, RoutedEventArgs e)
        {
            var process = lb_proc.SelectedItem as Process;
            if (process != null)
                ProcessSelected(process.ProcessName);
            Close();
        }

        private void b_cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void ProcessSelected(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                newProcessName = input;
            }
        }

        private void lb_proc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var process = lb_proc.SelectedItem as Process;
            if (process != null)
                ProcessSelected(process.ProcessName);
            Close();
        }
    }
}
