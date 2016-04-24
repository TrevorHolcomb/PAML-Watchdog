using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog
{
    public abstract class AbstractWatchdog
    {
        public abstract AbstractWatchdog GetInstance();
        public abstract void Watch();
        public abstract void StopWatching();
        protected abstract void ConsumeNewMessages(object state);
        
    }
}
