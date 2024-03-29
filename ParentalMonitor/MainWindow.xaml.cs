﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
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
using Instant_Process_Killer;
using ParentalMonitor.Classes;
using ParentalMonitor.Views;
using Settings = ParentalMonitor.Classes.Settings;

namespace ParentalMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region VarDec

        public DispatcherTimer controlTimer = new DispatcherTimer();
        public bool programActive;

        #endregion VarDec

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App._settings.day = DateTime.Today;
            controlTimer = new DispatcherTimer();
            controlTimer.Interval = App._settings.threadingTime;
            controlTimer.Tick += controlTimerTick;

            deactivateProgram();

            lv_main.ItemsSource = App._settings.restrictedProcessesList;
        }

        #region Hide

        private void b_hideWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        #endregion Hide

        #region TimeControl

        private void b_activateControl_Click(object sender, RoutedEventArgs e)
        {
            if (programActive)
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
            programActive = true;
        }

        public void deactivateProgram()
        {
            controlTimer.Stop();
            tb_status.Foreground = new SolidColorBrush(Colors.Red);
            tb_status.Text = "Parental Control Deactivated";
            b_activateDeactivateControl.Content = "Activate";
            programActive = false;
        }
        
        private void controlTimerTick(object sender, EventArgs e)
        {
            lv_main.Items.Refresh();

            dayChangedChecker();

            foreach (var restrictedProcess in App._settings.restrictedProcessesList)
            {
                //Check if Process is running and raise runtime
                if (checkIfProcessIsRunning(restrictedProcess.name))
                {
                    restrictedProcess.actualRunningTime += App._settings.threadingTime;
                }

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
            if (App._settings.day != DateTime.Today)
            {
                dayChanged();
                return true;
            }
            return false;
        }

        public void dayChanged()
        {
            App._settings.day = DateTime.Today;
            resetTimeLimitsForRestrictedProcesses();
        }

        private void resetTimeLimitsForRestrictedProcesses()
        {
            foreach (var proc in App._settings.restrictedProcessesList)
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

        private void b_delete_Click(object sender, RoutedEventArgs e)
        {
            if (lv_main.SelectedItem != null)
            {
                App._settings.restrictedProcessesList.Remove(lv_main.SelectedItem as RestrictedProcess);
            }
            lv_main.Items.Refresh();
        }

        private void b_edit_Click(object sender, RoutedEventArgs e)
        {
            if (lv_main.SelectedItem != null)
            {
                App._processHandover = lv_main.SelectedItem as RestrictedProcess;
                EditDialog edit = new EditDialog();
                edit.Owner = this;
                edit.ShowDialog();
                lv_main.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Select a Process from the List.", "Nothing selected!", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void b_quit_Click(object sender, RoutedEventArgs e)
        {
            SaveLoad.SaveToJson();

            try
            {
                var localProcesses = Process.GetProcesses();
                foreach (var proc in localProcesses)
                {
                    if (proc.ProcessName == App._settings.daemonProcessToMonitorName || proc.ProcessName == App._settings.serviceName)
                    {
                        proc.Kill();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            //Quit the Program
            Application.Current.Shutdown();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ParentalMonitor.Classes.TaskScheduler.scheduleTask();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //ToDo Unschedule
        }

        private void b_settings_Click(object sender, RoutedEventArgs e)
        {
            Views.Settings settings = new Views.Settings();
            settings.Owner = this;
            settings.ShowDialog();

            App._id = HotKeyManager.RegisterHotKey(App._settings.key, App._settings.modifierKey);
        }
    }
}
