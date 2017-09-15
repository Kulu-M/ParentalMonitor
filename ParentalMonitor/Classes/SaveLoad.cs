using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParentalMonitor.Classes
{
    public class SaveLoad
    {
        public static void SaveToJson()
        {
            try
            {
                File.WriteAllText(Settings.saveFilePath, JsonConvert.SerializeObject(App._restrictedProcessesList));
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
                App._restrictedProcessesList = JsonConvert.DeserializeObject<List<RestrictedProcess>>(File.ReadAllText(Settings.saveFilePath));
            }
            catch (Exception e)
            {
                File.Delete(Settings.saveFilePath);
                Console.WriteLine(e);
            }
            
        }
    }
}
