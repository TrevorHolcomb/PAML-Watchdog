using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidOriginExcpetion : Exception
    {
        public WatchdogInvalidOriginExcpetion()
        {
        }

        public WatchdogInvalidOriginExcpetion(string message)
            : base(message)
        {
        }

        public WatchdogInvalidOriginExcpetion(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
