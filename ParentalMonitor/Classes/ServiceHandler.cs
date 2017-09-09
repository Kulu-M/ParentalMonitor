using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Configuration.Install;
using System.IO;

namespace ParentalMonitor.Classes
{
    class ServiceHandler
    {
        //public TimeSpan threadingTime = TimeSpan.FromMinutes(0.1);
        public static DispatcherTimer serviceTimer = new DispatcherTimer();

        public static void startServiceController()
        {
            serviceTimer = new DispatcherTimer();
            serviceTimer.Interval = Settings.threadingTimeServiceController;
            serviceTimer.Tick += controlTimerTick;
            serviceTimer.Start();
        }

        public static void stopServiceController()
        {
           serviceTimer.Stop();
        }

        private static void controlTimerTick(object sender, EventArgs e)
        {
            //check if service is running

            var sc = new ServiceController(Settings.serviceName);
            try
            {
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        Console.WriteLine("Service is running!");
                        break;
                    case ServiceControllerStatus.Stopped:
                        Console.WriteLine("Service has stopped!");
                        StartService(Settings.serviceName, Settings.serviceStartupTimeoutInMs);
                        break;
                    case ServiceControllerStatus.Paused:
                        Console.WriteLine("Service is paused!");
                        break;
                    case ServiceControllerStatus.StopPending:
                        Console.WriteLine("Service has random status!");
                        break;
                    case ServiceControllerStatus.StartPending:
                        Console.WriteLine("Service has random status!");
                        break;
                    case ServiceControllerStatus.ContinuePending:
                        Console.WriteLine("Service has random status!");
                        break;
                    case ServiceControllerStatus.PausePending:
                        Console.WriteLine("Service has random status!");
                        break;
                    default:
                        Console.WriteLine("Service has random status!");
                        break;
                }
            }
            catch (InvalidOperationException exception)
            {
                var path =
                    @"D:\GITHUB REPOS\ParentalMonitor\HiddenWindowsService\HiddenServiceInstaller\Release";
                var path2 = "setup.exe";
                var x = Path.Combine(path, path2);
                //InstallService(x);
            }
            
        }

        #region START, STOP, RESTART SERVICES

        private static void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                // ...
            }
        }

        private static void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch
            {
                // ...
            }
        }

        private static void RestartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                // ...
            }
        }

        #endregion START, STOP, RESTART SERVICES

        #region INSTALL SERVICES

        public static void InstallService(string exeFilename)
        {
            string[] commandLineOptions = new string[1] { "/LogFile=install.log" };

            System.Configuration.Install.AssemblyInstaller installer = new System.Configuration.Install.AssemblyInstaller(exeFilename, commandLineOptions);

            installer.UseNewContext = true;
            installer.Install(null);
            installer.Commit(null);

        }

        #endregion INSTALL SERVICES

    }
}
