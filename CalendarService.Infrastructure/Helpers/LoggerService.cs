using CalendarService.Core.Interfaces.Helpers;
using CalendarService.Core.Models.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Infrastructure.Helpers
{
    public class LoggerService : ILoggerService
    {
        private static object mutex;
        public LoggerService()
        {
            mutex = new object();
        }

        public void LogError(Exception exception, string message)
        {

            try
            {
                string directory = AppSettings.LogPath;
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string filepath = directory + @"\" + DateTime.Now.Date.ToString("dd-MMM-yyyy") + ".txt";
                lock (mutex)
                {
                    File.AppendAllText(filepath, "Event Time: " + DateTime.Now.ToString() + " | Message: " + message + " | Exception: " + exception + Environment.NewLine);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not log info to file: {ex.Message}");
            }
        }

        public void LogInfo(string message)
        {
            try
            {
                string directory = AppSettings.LogPath;
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string filepath = directory + @"\" + DateTime.Now.Date.ToString("dd-MMM-yyyy") + ".txt";
                lock (mutex)
                {
                    File.AppendAllText(filepath, "Event Time: " + DateTime.Now.ToString() + " | Message: " + message + Environment.NewLine);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not log info to file: {ex.Message}");
            }
        }
    }
}
