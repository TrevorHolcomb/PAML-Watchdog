using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidServerExcpetion : Exception
    {
        public WatchdogInvalidServerExcpetion()
        {
        }

        public WatchdogInvalidServerExcpetion(string message)
            : base(message)
        {
        }

        public WatchdogInvalidServerExcpetion(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
