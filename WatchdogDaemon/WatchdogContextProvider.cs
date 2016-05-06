using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer;

namespace Watchdog
{
    class WatchdogContextProvider : IContextProvider
    {
        public WatchdogDatabaseContainer GetDatabaseContext()
        {
            return new WatchdogDatabaseContainer();
        }
    }
}
