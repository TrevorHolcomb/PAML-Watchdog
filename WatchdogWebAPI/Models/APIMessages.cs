using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogWebAPI.Models
{
    public class IncomingMessage
    {
        public string Server { get; set; }
        public string Engine { get; set; }
        public string Origin { get; set; }
        public string MessageTypeName { get; set; }
        public APIMessageParameter[] Params { get; set; }
    }

    public class OutGoingMessage
    {
        public string Server { get; set; }
        public string Engine { get; set; }
        public string Origin { get; set; }
        public string MessageTypeName { get; set; }
        public APIMessageParameter[] Params { get; set; }
    }
}