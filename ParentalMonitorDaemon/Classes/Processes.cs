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
            if (!checkIfProcessIsRunning())
            {
                startProcess();
            }
            else
            {
                Console.WriteLine("Daemon: Already running!");
            }
        }

        private static bool checkIfProcessIsRunning()
        {
            var localProcesses = Process.GetProcesses();
            try
            {
                foreach (var proc in localProcesses)
                {
                    if (proc.ProcessName == Settings.processToMonitorName)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                // ignored
            }
            return false;
        }

        private static void startProcess()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = Settings.processToMonitorLocation;
            proc.Start();
            Console.WriteLine("Daemon: Starting " + proc.ProcessName + ".");
        }
    }
}
