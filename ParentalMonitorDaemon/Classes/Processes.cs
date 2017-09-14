using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentalMonitorDaemon.Config;

namespace ParentalMonitorDaemon.Classes
{
    class Processes
    {
        public static void processLogic()
        {
            if (howManyProcessInstancesAreRunning(Settings.processToMonitorName) == 0)
            {
                startProcess(Settings.processToMonitorLocation);
            }
            else
            {
                Console.WriteLine("Daemon: Already running!");
            }

            var processName =
                AppDomain.CurrentDomain.FriendlyName.Substring(0, AppDomain.CurrentDomain.FriendlyName.Length - 4);

            if (howManyProcessInstancesAreRunning(processName) < Settings.daemonInstancesToRun)
            {
                startProcess(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName));
            }
        }

        private static int howManyProcessInstancesAreRunning(string processName)
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

        private static void startProcess(string processToStart)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = processToStart;
            proc.Start();
            Console.WriteLine("Daemon: Starting " + proc.ProcessName + ".");
        }
    }
}
