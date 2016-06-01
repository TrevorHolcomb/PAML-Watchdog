using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidEngineException : Exception
    {
        public WatchdogInvalidEngineException()
        {
        }

        public WatchdogInvalidEngineException(string message)
            : base(message)
        {
        }

        public WatchdogInvalidEngineException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
