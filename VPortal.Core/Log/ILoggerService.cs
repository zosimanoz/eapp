using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Log
{
    public interface ILoggerService
    {
        DailyLog GetTodaysLogs(int offset = 0, int limit = 20);
        DailyLog GetLogs(int offset, int limit, DateTime day);
    }
}
