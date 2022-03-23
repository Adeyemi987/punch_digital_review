using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Models.Constants
{
    public class AppSettings
    {
        public static string DbConnectionString { get; set; }
        public static string EnableSwagger { get; set; }
        public static string LogPath { get; set; }
    }
}
