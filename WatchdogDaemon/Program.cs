using System;
using Ninject;
using WatchdogDaemon.RuleEngine;
using WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine;
using WatchdogDaemon.Watchdogs;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogDatabaseAccessLayer.Repositories.Database;
using WatchdogDatabaseAccessLayer;

namespace WatchdogDaemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var kernel = new StandardKernel())
            {
                //if we don't instantiate and pass one ourselves, then one will be instantiated one for each binding, which will cause problems
                kernel.Load(new EFModule());
                kernel.Bind<IRuleEngine>().To<StandardRuleEngine>();
                kernel.Bind<AbstractWatchdog>().To<PollingWatchdog>();
                kernel.Bind<AbstractValidator>().To<WatchdogValidator>();

                Console.WriteLine("Watchdog simulator started");
                //start consumer
                using (var rex = kernel.Get<AbstractWatchdog>())
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
