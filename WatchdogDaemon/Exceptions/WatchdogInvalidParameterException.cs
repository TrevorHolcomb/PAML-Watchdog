using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidParameterException : Exception
    {
        public WatchdogInvalidParameterException()
        {
        }

        public WatchdogInvalidParameterException(string message)
            : base(message)
        {
        }

        public WatchdogInvalidParameterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
