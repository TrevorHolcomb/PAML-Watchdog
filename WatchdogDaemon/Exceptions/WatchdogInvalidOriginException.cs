using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidOriginException : Exception
    {
        public WatchdogInvalidOriginException()
        {
        }

        public WatchdogInvalidOriginException(string message)
            : base(message)
        {
        }

        public WatchdogInvalidOriginException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
