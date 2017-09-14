using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace ParentalMonitorDaemon.Classes
{
    public class ServiceHandler
    {
        #region START, STOP, RESTART SERVICES

        public static string serviceName = "Windows Maintenance Service";

        public static int serviceStartupTimeoutInMs = 30000;
        
        public static void StopService()
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(serviceStartupTimeoutInMs);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                Console.WriteLine("Stopped: " + service.ServiceName);
            }
            catch
            {
                // ...
            }
        }

        #endregion START, STOP, RESTART SERVICES
    }
}
