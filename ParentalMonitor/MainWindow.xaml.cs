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

namespace ParentalMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region VarDec

        public TimeSpan threadingTime = TimeSpan.FromMinutes(0.1);
        public DispatcherTimer controlTimer = new DispatcherTimer();
        private List<RestrictedProcess> restrictedProcessesList;

        #endregion VarDec

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Settings.day = DateTime.Today;
            restrictedProcessesList = new List<RestrictedProcess>();
            controlTimer = new DispatcherTimer();
            controlTimer.Interval = threadingTime;
            controlTimer.Tick += controlTimerTick;

            insertExampleProcess();
        }

        private void insertExampleProcess()
        {
            restrictedProcessesList.Add(new RestrictedProcess { name = "firefox", allowedRunningTime = TimeSpan.Zero});
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
            controlTimer.Start();
        }

        private void b_deactivateControl_Click(object sender, RoutedEventArgs e)
        {
            controlTimer.Stop();
        }

        private void controlTimerTick(object sender, EventArgs e)
        {
            dayChangedChecker();

            foreach (var restrictedProcess in restrictedProcessesList)
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
            foreach (var proc in restrictedProcessesList)
            {
                proc.actualRunningTime = TimeSpan.Zero;
            }
        }


        #endregion TimeControl

       
    }
}
