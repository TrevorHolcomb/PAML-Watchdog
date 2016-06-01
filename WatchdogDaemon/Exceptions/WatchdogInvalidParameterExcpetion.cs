using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidParameterExcpetion : Exception
    {
        public WatchdogInvalidParameterExcpetion()
        {
        }

        public WatchdogInvalidParameterExcpetion(string message)
            : base(message)
        {
        }

        public WatchdogInvalidParameterExcpetion(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
