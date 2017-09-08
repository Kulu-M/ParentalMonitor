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
using System.Windows.Threading;
using ParentalMonitor.Classes;
using ParentalMonitor.Views;

namespace ParentalMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region VarDec

        public TimeSpan threadingTime = TimeSpan.FromMinutes(0.01);
        public DispatcherTimer controlTimer = new DispatcherTimer();
        //public List<RestrictedProcess> restrictedProcessesList;
        public bool programmActive;

        #endregion VarDec

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Settings.day = DateTime.Today;
            App._restrictedProcessesList = new List<RestrictedProcess>();
            controlTimer = new DispatcherTimer();
            controlTimer.Interval = threadingTime;
            controlTimer.Tick += controlTimerTick;

            deactivateProgram();

            insertExampleProcess();

            lv_main.ItemsSource = App._restrictedProcessesList;
        }

        private void insertExampleProcess()
        {
            App._restrictedProcessesList.Add(new RestrictedProcess { name = "iexplore", allowedRunningTime = TimeSpan.FromMinutes(5)});
        }

        #region Show-Hide

        private void b_hideWindow_Click(object sender, RoutedEventArgs e)
        {
            hideMainWindow();
        }

        public void showMainWindow()
        {
            //Done in App.xaml.cs
        }

        public void hideMainWindow()
        {
            this.Hide();
        }

        #endregion Show-Hide

        #region TimeControl

        private void b_activateControl_Click(object sender, RoutedEventArgs e)
        {
            if (programmActive)
            {
                deactivateProgram();
            }
            else
            {
                activateProgram();
            }
        }

        public void activateProgram()
        {
            controlTimer.Start();
            tb_status.Foreground = new SolidColorBrush(Colors.DarkGreen);
            tb_status.Text = "Parental Control Activated";
            b_activateDeactivateControl.Content = "Deactivate";
            programmActive = true;
        }

        public void deactivateProgram()
        {
            controlTimer.Stop();
            tb_status.Foreground = new SolidColorBrush(Colors.Red);
            tb_status.Text = "Parental Control Deactivated";
            b_activateDeactivateControl.Content = "Activate";
            programmActive = false;
        }
        
        private void controlTimerTick(object sender, EventArgs e)
        {
            lv_main.Items.Refresh();

            dayChangedChecker();

            foreach (var restrictedProcess in App._restrictedProcessesList)
            {
                //Check if Process is running and raise runtime
                if (checkIfProcessIsRunning(restrictedProcess.name))
                {
                    restrictedProcess.actualRunningTime += threadingTime;
                }

                //Send Warning if enabled


                //Kill Processes when their runtime is up
                if (checkIfProcessIsRunning(restrictedProcess.name) && restrictedProcess.actualRunningTime >= restrictedProcess.allowedRunningTime)
                {
                    killProcess(restrictedProcess);
                }
            }
        }

        private void killProcess(RestrictedProcess restrictedProcess)
        {
            var localProcesses = Process.GetProcesses();
            try
            {
                foreach (var proc in localProcesses)
                {
                    if (proc.ProcessName == restrictedProcess.name)
                    {
                        proc.Kill();
                        restrictedProcess.processInstances++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (restrictedProcess.processInstances > 0)
            {
                Console.WriteLine("Process terminated: '" + restrictedProcess.name + "'. The Process had " + restrictedProcess.processInstances + " Instances.");
                restrictedProcess.processInstances = 0;
            }
            else
            {
                Console.WriteLine("Something went wrong killing the Process '" + restrictedProcess.name + "'.");
            }
        }

        private bool checkIfProcessIsRunning(string restrictedProcessName)
        {
            var localProcesses = Process.GetProcessesByName(restrictedProcessName);
            return localProcesses.Length > 0;
        }

        public bool dayChangedChecker()
        {
            if (Settings.day != DateTime.Today)
            {
                dayChanged();
                return true;
            }
            return false;
        }

        public void dayChanged()
        {
            Settings.day = DateTime.Today;
            resetTimeLimitsForRestrictedProcesses();
        }

        private void resetTimeLimitsForRestrictedProcesses()
        {
            foreach (var proc in App._restrictedProcessesList)
            {
                proc.actualRunningTime = TimeSpan.Zero;
            }
        }

        #endregion TimeControl

        private void b_add_Click(object sender, RoutedEventArgs e)
        {
            AddNewDialog add = new AddNewDialog();
            add.Owner = this;
            add.ShowDialog();

            lv_main.Items.Refresh();
        }
    }
}
