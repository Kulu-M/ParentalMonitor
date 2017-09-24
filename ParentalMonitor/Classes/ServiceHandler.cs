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
        public static DispatcherTimer serviceTimer = new DispatcherTimer();

        public static void startServiceControllerAndService()
        {
            serviceTimer = new DispatcherTimer();
            serviceTimer.Interval = App._settings.threadingTimeServiceController;
            serviceTimer.Tick += controlTimerTick;
            serviceTimer.Start();
        }

        public static void stopServiceControllerAndService()
        {
           serviceTimer.Stop();
           StopService();
        }

        private static void controlTimerTick(object sender, EventArgs e)
        {
            //check if service is running

            var sc = new ServiceController(App._settings.serviceName);
            //try
            //{
                switch (sc.Status)
                {
                    case ServiceControllerStatus.Running:
                        Console.WriteLine("Service is running!");
                        break;
                    case ServiceControllerStatus.Stopped:
                        Console.WriteLine("Service has stopped!");
                        StartService();
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
            //}
            //catch (InvalidOperationException exception)
            //{
            //    var path =
            //        @"D:\GITHUB REPOS\ParentalMonitor\HiddenWindowsService\HiddenServiceInstaller\Release";
            //    var path2 = "setup.exe";
            //    var x = Path.Combine(path, path2);
            //    //InstallService(x);
            //}
            
        }

        #region START, STOP, RESTART SERVICES

        private static void StartService()
        {
            var service = new ServiceController(App._settings.serviceName);
            var timeout = TimeSpan.FromMilliseconds(App._settings.serviceStartupTimeoutInMs);
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, timeout);
        }

        private static void StopService()
        {
            ServiceController service = new ServiceController(App._settings.serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(App._settings.serviceStartupTimeoutInMs);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                Console.WriteLine("Service status: " + service.Status);
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
