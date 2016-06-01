using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidMessageTypeException : Exception
    {
        public WatchdogInvalidMessageTypeException()
        {
        }

        public WatchdogInvalidMessageTypeException(string message)
            : base(message)
        {
        }

        public WatchdogInvalidMessageTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
