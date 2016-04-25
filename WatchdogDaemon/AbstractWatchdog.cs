using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchdogDaemon
{
    abstract class AbstractWatchdog
    {
        public abstract AbstractWatchdog GetInstance();
        public abstract void Watch();
        protected abstract void ConsumeNewMessages(object state);
        
    }
}
