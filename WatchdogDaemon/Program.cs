using System;
using Ninject;
using WatchdogDaemon.Watchdogs;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDaemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind<IRepository<Message>>().To<EFMessageRepository>();
                kernel.Bind<IRepository<Alert>>().To<EFAlertRepository>();
                kernel.Bind<IRepository<Rule>>().To<EFRuleRepository>();

                Console.WriteLine("Watchdog simulator started");
                //start consumer
                using (var rex = new PollingWatchdog(kernel))
                {
                    rex.Watch();
                    Console.WriteLine("Press \'Q\' to quit");
                    while (Console.ReadKey().Key == ConsoleKey.Q) { }
                    rex.StopWatching();
                }
            }
        }
    }
}
