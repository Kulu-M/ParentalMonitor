using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Instant_Process_Killer;
using ParentalMonitor.Classes;
using Application = System.Windows.Application;

namespace ParentalMonitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static int _id = new int();

        public static RestrictedProcess _processHandover;

        public static List<RestrictedProcess> _restrictedProcessesList;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _id = 0;

            //Register Hotkey
            App._id = HotKeyManager.RegisterHotKey(Keys.F1, ModifierKeys.None);
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;

            //Start Monitoring the Service
            if (Settings.debugMode) return;
            ServiceHandler.startServiceControllerAndService();
        }

        private void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (MainWindow.Visibility == Visibility.Visible) return;
            MainWindow.Visibility = Visibility.Visible;
        }
    }
}
