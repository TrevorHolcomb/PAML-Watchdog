using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidServerException : Exception
    {
        public WatchdogInvalidServerException()
        {
        }

        public WatchdogInvalidServerException(string message)
            : base(message)
        {
        }

        public WatchdogInvalidServerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
