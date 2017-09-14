using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentalMonitorDaemon.Config
{
    public class Settings
    {
        public static string processToMonitorName = "ParentalMonitor";

        public static string processToMonitorLocation = @"D:\GITHUB REPOS\ParentalMonitor\ParentalMonitor\bin\Release\ParentalMonitor.exe";

        public static TimeSpan threadingTime = TimeSpan.FromMinutes(0.1);
    }
}
