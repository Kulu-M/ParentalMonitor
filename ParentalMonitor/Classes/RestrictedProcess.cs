using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentalMonitor.Classes
{
    public class RestrictedProcess
    {
        public string name { get; set; }
        public TimeSpan allowedRunningTime { get; set; }
        public TimeSpan actualRunningTime { get; set; }
        public int processInstances { get; set; }

        public RestrictedProcess()
        {
            name = "ProcessName";
            actualRunningTime = TimeSpan.Zero;
            allowedRunningTime = TimeSpan.Zero;
            processInstances = 0;
        }
    }
}
