using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsMaintenanceService.Settings;
using Toolkit;

namespace WindowsMaintenanceService.Classes
{
    static class Processes
    {
        public static void startProcess(string processLocation)
        {
            ApplicationLoader.PROCESS_INFORMATION procInfo;
            ApplicationLoader.StartProcessAndBypassUAC(processLocation, out procInfo);
        }

        public static int howManyProcessInstancesAreRunning(string processName)
        {
            var localProcesses = Process.GetProcesses();
            var count = 0;
            try
            {
                foreach (var proc in localProcesses)
                {
                    if (proc.ProcessName == processName)
                    {
                        count++;
                    }
                }
                return count;
            }
            catch
            {
                // ignored
            }
            return count;
        }
    }
}
