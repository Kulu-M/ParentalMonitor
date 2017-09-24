using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ParentalMonitor.Classes
{
    public class SaveLoad
    {
        public static void SaveToJson()
        {
            try
            {
                File.WriteAllText(App._settings.saveFilePath, JsonConvert.SerializeObject(App._settings));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void LoadFromJson()
        {

            try
            {
               App._settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveFile.json"))) ?? new Settings();
            }
            catch (Exception e)
            {
                App._settings = new Settings();
                File.WriteAllText(App._settings.saveFilePath, JsonConvert.SerializeObject(App._settings));
                Console.WriteLine(e);
            }
            
        }
    }

    
}
