using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentalMonitor.Classes
{
    public static class Settings
    {
        public static string saveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveFile.json");

        public static bool debugMode = false;

        public static TimeSpan threadingTime = TimeSpan.FromMinutes(0.1);

        public static bool showWarnings = true;

        public static DateTime day = DateTime.Today;

        //SERVICE SETTINGS
        public static TimeSpan threadingTimeServiceController = TimeSpan.FromMinutes(0.1);

        public static string serviceName = "Windows Maintenance Service";

        public static int serviceStartupTimeoutInMs = 30000;

        //DAEMON SETTINGS
        public static TimeSpan threadingTimeDaemonController = TimeSpan.FromMinutes(0.1);

        public static string daemonProcessToMonitorName = "ParentalMonitorDaemon";

        public static string daemonProcessToMonitorLocation = @"D:\GITHUB REPOS\ParentalMonitor\ParentalMonitorDaemon\bin\Release\ParentalMonitorDaemon.exe";

        public static int daemonInstancesToRun = 2;

    }
}
