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
                if (!Processes.checkIfProcessIsRunning())
                {
                    Processes.startProcess();
                }
                Thread.Sleep(5000);
            }
        }
    }
}
