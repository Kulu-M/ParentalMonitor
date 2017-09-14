using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentalMonitorProcessKiller
{
    public class Processes
    {
        public static void killProcess(string process)
        {
            var localProcesses = Process.GetProcesses();
            try
            {
                foreach (var proc in localProcesses)
                {
                    if (proc.ProcessName == process)
                    {
                        proc.Kill();
                        Console.WriteLine("Killed: " + process + ".");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
