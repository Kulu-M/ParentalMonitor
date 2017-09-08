using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsMaintenanceService.Config;

namespace WindowsMaintenanceService.Classes
{
    static class Processes
    {
        public static void startProcess()
        {
            Process.Start(ConfigFile.processToMonitorLocation);
        }

        public static bool checkIfProcessIsRunning()
        {
            var localProcesses = Process.GetProcesses();
            try
            {
                foreach (var proc in localProcesses)
                {
                    if (proc.ProcessName == ConfigFile.processToMonitorName)
                    {
                        Console.WriteLine("Process found running: " + ConfigFile.processToMonitorName);
                        return true;
                    }
                }
            }
            catch
            {
                // ignored
            }
            Console.WriteLine("Process not running: " + ConfigFile.processToMonitorName);
            return false;
        }
    }
}
