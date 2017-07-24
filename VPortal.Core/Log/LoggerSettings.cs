using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Log
{
    public class LoggerSettings : ILoggerSettings
    {
        public bool AllowLogging { get; set; } = true;
    }
}
