using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ParentalMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region VarDec

        public DispatcherTimer controlTimer = new DispatcherTimer();

        #endregion VarDec

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            controlTimer = new DispatcherTimer();
            controlTimer.Interval = TimeSpan.FromMinutes(1);
            controlTimer.Tick += controlTimerTick;
        }

        #region Show-Hide

        public void showMainWindow()
        {
            
        }

        public void hideMainWindow()
        {
            
        }

        #endregion Show-Hide

        #region TimeControl

        private void b_activateControl_Click(object sender, RoutedEventArgs e)
        {
            controlTimer.Start();
        }

        private void b_deactivateControl_Click(object sender, RoutedEventArgs e)
        {
            controlTimer.Stop();
        }

        private void controlTimerTick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion TimeControl


    }
}
