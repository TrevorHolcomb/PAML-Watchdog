using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon.Exceptions
{
    class WatchdogInvalidEngineExcpetion : Exception
    {
        public WatchdogInvalidEngineExcpetion()
        {
        }

        public WatchdogInvalidEngineExcpetion(string message)
            : base(message)
        {
        }

        public WatchdogInvalidEngineExcpetion(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
