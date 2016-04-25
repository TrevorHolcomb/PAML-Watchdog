using System;
using System.Linq;
using System.Data.Entity;
using WatchdogDatabaseAccessLayer;
using System.Threading;

namespace WatchdogDaemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Watchdog simulator started");
            //start consumer
            AbstractWatchdog rex = new PollingWatchdog(new WatchdogDatabaseContext(), new RuleEngine());
            rex.Watch();

            while (true) { }
        }
    }
}
