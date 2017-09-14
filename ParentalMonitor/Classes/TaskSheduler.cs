using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;

namespace ParentalMonitor.Classes
{
    public class TaskScheduler
    {
        //Scheduled Task, checks every 5 minutes if the exe is still running
        public static void scheduleTask()
        {
            // TaskService.Instance.AddTask("Windows Maintenance Service Daemon", QuickTriggerType.Hourly, "myprogram.exe", "-a arg");

            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Windows Maintenance Service is needed for system critical updates which cannot be acquired by Windows Update.";
                td.Principal.LogonType = TaskLogonType.InteractiveToken;
                td.RegistrationInfo.Author = "Windows Corporation";

                // Create a trigger that will fire the task at this time every other day
                td.Triggers.Add(new TimeTrigger
                {
                    StartBoundary = DateTime.Now + TimeSpan.FromMinutes(5),
                    Enabled = true,
                    EndBoundary = DateTime.MaxValue
                });

                // Create an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName)));

                // Register the task in the root folder
                const string taskName = "Windows Maintenance Daemon";
                ts.RootFolder.RegisterTaskDefinition(taskName, td);
            }
        }

        public void unSheduleTask()
        {
            
        }
    }
}
