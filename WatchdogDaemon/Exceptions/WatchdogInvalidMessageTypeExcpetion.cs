using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidMessageTypeExcpetion : Exception
    {
        public WatchdogInvalidMessageTypeExcpetion()
        {
        }

        public WatchdogInvalidMessageTypeExcpetion(string message)
            : base(message)
        {
        }

        public WatchdogInvalidMessageTypeExcpetion(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
