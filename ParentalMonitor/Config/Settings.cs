using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentalMonitor.Classes
{
    public static class Settings
    {
        public static bool debugMode = true;

        public static TimeSpan timeToThreadInMinutes = TimeSpan.FromMinutes(1);

        public static TimeSpan globalWarningTime = TimeSpan.FromMinutes(5);

        public static bool showWarnings = true;

        public static DateTime day = DateTime.Today;

        public static string processToMonitorLocation = @"D:\GITHUB REPOS\ParentalMonitor\ParentalMonitor\bin\Release\ParentalMonitor.exe";

        //SERVICE SETTINGS
        public static TimeSpan threadingTimeServiceController = TimeSpan.FromMinutes(0.1);

        public static string serviceName = "Windows Maintenance Service";

        public static int serviceStartupTimeoutInMs = 30000;

    }
}
