using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsMaintenanceService.Classes;
using WindowsMaintenanceService.Settings;

namespace WindowsMaintenanceService
{
    public partial class Service1 : ServiceBase
    {
        private Thread _thread;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //OnStart-Callback muss returnen, sonst sieht WIN den Service als 'timed out' an und killt ihn -> Logik muss in eigenem Thread laufen

            _thread = new Thread(ThreadWorkerLogic)
            {
                Name = "Windows Service Logic",
                IsBackground = true
            };
            _thread.Start();
            //EventLog.WriteEntry("My simple service started.");
        }

        protected override void OnStop()
        {
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        private void ThreadWorkerLogic()
        {
            while (true)
            {
                if (Processes.howManyProcessInstancesAreRunning(Settings.Settings.mainProcessToMonitorLocation) == 0)
                {
                    Processes.startProcess(Settings.Settings.mainProcessToMonitorLocation);
                    Console.WriteLine("Service: Started Main!");
                }

                var processName =
                    AppDomain.CurrentDomain.FriendlyName.Substring(0, AppDomain.CurrentDomain.FriendlyName.Length - 4);

                if (Processes.howManyProcessInstancesAreRunning(processName) < Settings.Settings.daemonInstancesToRun)
                {
                    Processes.startProcess(Settings.Settings.daemonProcessToMonitorLocation);
                    Console.WriteLine("Service: Started Daemon!");
                }
                
                Thread.Sleep(Settings.Settings.threadingTime);
            }
        }
    }
}
