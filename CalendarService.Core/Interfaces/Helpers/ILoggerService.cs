using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarService.Core.Interfaces.Helpers
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogError(Exception exception, string message);
    }
}
