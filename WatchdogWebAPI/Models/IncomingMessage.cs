using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatchdogWebAPI.Models
{
    public class IncomingMessage
    {
        public string Server { get; set; }
        public int MessageTypeId { get; set; }
        public object Params { get; set; }
    }

    public class OutGoingMessage
    {
        public string Server { get; set; }
        public string Origin { get; set; }
        public int MessageTypeId { get; set; }
        public object Params { get; set; }
    }
}