using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentalMonitorDaemon.Classes;

namespace ParentalMonitorProcessKiller
{
    class Program
    {
        public static List<string> processesToKill = new List<string>();

        static void Main(string[] args)
        {
            processesToKill.Add(ProcessesToKill.parentalMonitorMain);
            processesToKill.Add(ProcessesToKill.parentalMonitorDaemon);

            while (true)
            {
                Console.WriteLine("Kill?");

                // Keep the console window open in debug mode.
                Console.WriteLine("Press any key to kill.");
                Console.ReadKey();

                ServiceHandler.StopService();

                foreach (var VARIABLE in processesToKill)
                {
                    Processes.killProcess(VARIABLE);
                    
                }
                Console.WriteLine(Environment.NewLine);
            }
        }
    }

}
