using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ParentalMonitorDaemon.Classes;
using ParentalMonitorDaemon.Config;

namespace ParentalMonitorDaemon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DispatcherTimer serviceTimer = new DispatcherTimer();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            startProcessCheckerTimer();
        }

        public static void startProcessCheckerTimer()
        {
            serviceTimer = new DispatcherTimer();
            serviceTimer.Interval = Settings.threadingTime;
            serviceTimer.Tick += controlTimerTick;
            serviceTimer.Start();
        }

        private static void controlTimerTick(object sender, EventArgs e)
        {
            Processes.processLogic();
        }
    }
}
