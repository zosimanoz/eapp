using System;
using System.Collections.Generic;
using System.Text;
using VPortal.Core.Log;

namespace VPortal.Core.Log.Services
{
    public class LoggerService : ILoggerService
    {
        public DailyLog GetLogs(int offset, int limit, DateTime day)
        {
            var logsList = new List<Log>();

            logsList.Add(new Log()
            {
                LogId = null,
                LogType = 2,
                DateTime = DateTime.Now.ToString(),
                Error = "Test error",
                Status = ""
            });


            var data = new DailyLog()
            {
                Logs = logsList,
                TotalCount = 1
            };

            return data;
        }

        public DailyLog GetTodaysLogs(int offset = 0, int limit = 20)
        {
            var logsList = new List<Log>();

            logsList.Add(new Log()
            {
                LogId = null,
                LogType = 2,
                DateTime = DateTime.Now.ToString(),
                Error = "Test error",
                Status = ""
            });


            var data = new DailyLog()
            {
                Logs = logsList,
                TotalCount = 1
            };

            return data;
        }
    }
}
