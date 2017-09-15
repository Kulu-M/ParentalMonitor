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
using System.Windows.Threading;

namespace ParentalMonitor.Views
{
    /// <summary>
    /// Interaction logic for ProcessListChooser.xaml
    /// </summary>
    public partial class ProcessListChooser : Window
    {
        public string newProcessName = "";

        public DispatcherTimer processListRefreshTimer;

        public static int processCount = 0;
        
        public ProcessListChooser()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshProcessList();
            processListRefreshTimer = new DispatcherTimer();
            processListRefreshTimer.Interval = TimeSpan.FromSeconds(3);
            processListRefreshTimer.Tick += processListRefreshTimerTick;
            processListRefreshTimer.Start();
        }

        private void processListRefreshTimerTick(object sender, EventArgs e)
        {
            refreshProcessList();
        }

        private void refreshProcessList()
        {
            var selected = 0;

            var localProcesses = Process.GetProcesses();

            if (localProcesses.Length == processCount)
            {
                return;
            }
            processCount = localProcesses.Length;

            var process = lb_proc.SelectedItem as Process;
            if (process != null) selected = process.Id;

            var localProcessesSorted =
                localProcesses.Where(x => x.SessionId == Process.GetCurrentProcess().SessionId)
                    .OrderBy(x => x.ProcessName);

            try
            {
                lb_proc.ItemsSource = localProcessesSorted;
                if (selected != 0)
                {
                    lb_proc.SelectedItem = localProcessesSorted.First(y => y.Id == selected);
                }
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

        private void b_refresh_Click(object sender, RoutedEventArgs e)
        {
            refreshProcessList();
        }
    }
}
