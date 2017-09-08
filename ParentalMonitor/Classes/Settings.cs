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
    }
}
