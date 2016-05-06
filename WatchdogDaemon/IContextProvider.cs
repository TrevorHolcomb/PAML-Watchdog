using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchdogDatabaseAccessLayer;

namespace Watchdog
{
    public interface IContextProvider
    {
        WatchdogDatabaseContainer GetDatabaseContext();
    }
}
