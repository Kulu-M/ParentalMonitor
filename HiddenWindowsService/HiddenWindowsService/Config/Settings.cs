using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMaintenanceService.Settings
{
    static class Settings
    {
        public static string serviceName = "Windows Maintenance Service";

        public static string mainProcessToMonitorName = "ParentalMonitor";

        public static string mainProcessToMonitorLocation = @"D:\GITHUB REPOS\ParentalMonitor\ParentalMonitor\bin\x64\Release\ParentalMonitor.exe";

        public static string daemonProcessToMonitorName = "ParentalMonitorDaemon";

        public static string daemonProcessToMonitorLocation = @"D:\GITHUB REPOS\ParentalMonitor\ParentalMonitorDaemon\bin\Release\ParentalMonitorDaemon.exe";

        public static int daemonInstancesToRun = 2;

        public static int threadingTime = 1000;
    }
}
