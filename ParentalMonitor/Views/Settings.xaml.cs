using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ParentalMonitor.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cb_key.Items.Clear();
            cb_modifier.Items.Clear();

            foreach (var key in Enum.GetValues(typeof(Keys)))
            {
                cb_key.Items.Add(key);
            }
            cb_key.SelectedItem = App._settings.key;

            foreach (var keyMod in Enum.GetValues(typeof(ModifierKeys)))
            {
                cb_modifier.Items.Add(keyMod);
            }
            cb_modifier.SelectedItem = App._settings.modifierKey;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App._settings.key = cb_key.SelectedItem as Keys? ?? Keys.None;
            App._settings.modifierKey = cb_modifier.SelectedItem as ModifierKeys? ?? ModifierKeys.None;
        }
    }
}
