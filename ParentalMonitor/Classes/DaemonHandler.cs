using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ParentalMonitor.Classes
{
    public class DaemonHandler
    {
        public static DispatcherTimer daemonTimer = new DispatcherTimer();

        public static void startDaemonController()
        {
            daemonTimer = new DispatcherTimer();
            daemonTimer.Interval = Settings.threadingTimeDaemonController;
            daemonTimer.Tick += controlTimerTick;
            daemonTimer.Start();
        }

        private static void controlTimerTick(object sender, EventArgs e)
        {
            if (howManyProcessInstancesAreRunning(Settings.daemonProcessToMonitorName) < Settings.daemonInstancesToRun)
            {
                startProcess(Settings.daemonProcessToMonitorLocation);
                Console.WriteLine("Service: Started Daemon!");
            }
        }

        public static void stopDaemonController()
        {
            daemonTimer.Stop();
        }

        private static void startProcess(string processToStart)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = processToStart;
            proc.Start();
            Console.WriteLine("Main: Starting " + proc.ProcessName + ".");
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
