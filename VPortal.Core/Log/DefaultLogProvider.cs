using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Log
{
    public class DefaultLogProvider : ILogProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerSettings _loggerSetting;

        public DefaultLogProvider(IHostingEnvironment hostingEnvironment, ILoggerSettings loggerSetting)
        {
            _hostingEnvironment = hostingEnvironment;
            _loggerSetting = loggerSetting;
        }
        public ILogger GetLogger(string name)
        {
            return new FileBaseLogger(_hostingEnvironment, _loggerSetting);
        }
    }
}
