using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsMaintenanceService.Settings;

namespace WindowsMaintenanceService.Classes
{
    static class Processes
    {
        public static void startProcess()
        {
            Process.Start(Settings.Settings.processToMonitorLocation);
        }

        public static bool checkIfProcessIsRunning()
        {
            var localProcesses = Process.GetProcesses();
            try
            {
                foreach (var proc in localProcesses)
                {
                    if (proc.ProcessName == Settings.Settings.processToMonitorName)
                    {
                        Console.WriteLine("Process found running: " + Settings.Settings.processToMonitorName);
                        return true;
                    }
                }
            }
            catch
            {
                // ignored
            }
            Console.WriteLine("Process not running: " + Settings.Settings.processToMonitorName);
            return false;
        }
    }
}
