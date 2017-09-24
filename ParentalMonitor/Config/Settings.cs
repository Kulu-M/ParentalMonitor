using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ParentalMonitor.Classes
{
    public class Settings
    {
        public Keys key;

        public ModifierKeys modifierKey;

        public List<RestrictedProcess> restrictedProcessesList = new List<RestrictedProcess>();

        public  bool debugMode;

        public  TimeSpan threadingTime;

        public  DateTime day;

        //SERVICE SETTINGS
        public  TimeSpan threadingTimeServiceController;

        public  string serviceName;

        public  int serviceStartupTimeoutInMs;

        //DAEMON SETTINGS
        public  TimeSpan threadingTimeDaemonController;

        public  string daemonProcessToMonitorName;

        public  string daemonProcessToMonitorLocation;

        public  int daemonInstancesToRun;

        public Settings()
        {
            debugMode = false;
            threadingTime = TimeSpan.FromMinutes(0.1);
            day = DateTime.Today;
            threadingTimeServiceController = TimeSpan.FromMinutes(0.1);
            serviceName = "Windows Maintenance Service";
            serviceStartupTimeoutInMs = 30000;
            threadingTimeDaemonController = TimeSpan.FromMinutes(0.1);
            daemonProcessToMonitorName = "ParentalMonitorDaemon";
            daemonProcessToMonitorLocation = @"D:\GITHUB REPOS\ParentalMonitor\ParentalMonitorDaemon\bin\Release\ParentalMonitorDaemon.exe";
            daemonInstancesToRun = 2;
        }
    }

    //public static class Settings
    //{
    //    public static string saveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveFile.json");

    //    public static bool debugMode = false;

    //    public static TimeSpan threadingTime = TimeSpan.FromMinutes(0.1);

    //    public static bool showWarnings = true;

    //    public static DateTime day = DateTime.Today;

    //    //SERVICE SETTINGS
    //    public static TimeSpan threadingTimeServiceController = TimeSpan.FromMinutes(0.1);

    //    public static string serviceName = "Windows Maintenance Service";

    //    public static int serviceStartupTimeoutInMs = 30000;

    //    //DAEMON SETTINGS
    //    public static TimeSpan threadingTimeDaemonController = TimeSpan.FromMinutes(0.1);

    //    public static string daemonProcessToMonitorName = "ParentalMonitorDaemon";

    //    public static string daemonProcessToMonitorLocation = @"D:\GITHUB REPOS\ParentalMonitor\ParentalMonitorDaemon\bin\Release\ParentalMonitorDaemon.exe";

    //    public static int daemonInstancesToRun = 2;

    //}
}
