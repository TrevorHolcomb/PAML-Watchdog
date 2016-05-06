using System;
using System.Linq;
using System.Data.Entity;
using WatchdogDatabaseAccessLayer;
using System.Threading;
using Watchdog;

namespace WatchdogDaemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Watchdog simulator started");
            //start consumer
            using (var rex = new PollingWatchdog(new WatchdogContextProvider(), new RuleEngine()))
            {
                rex.Watch();
                Console.WriteLine("Press \'Q\' to quit");
                while (Console.ReadKey().Key == ConsoleKey.Q) { }
            }
        }
    }
}
