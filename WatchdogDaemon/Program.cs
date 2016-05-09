using System;
using Ninject;
using WatchdogDaemon.Watchdogs;
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
                kernel.Bind<IMessageRepository>().To<EFMessageRepository>();
                kernel.Bind<IAlertRepository>().To<EFAlertRepository>();
                kernel.Bind<IRuleRepository>().To<EFRuleRepository>();

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
