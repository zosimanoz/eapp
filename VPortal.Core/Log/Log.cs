﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VPortal.Core.Log
{
    public class Log
    {
        public string LogId { get; set; }
        public string Status { get; set; }
        public string DateTime { get; set; }
        public int LogType { get; set; }
        public string Error { get; set; }
    }
}
