using System;
using Ninject;
using WatchdogDaemon.RuleEngine;
using WatchdogDaemon.RuleEngine.ExpressionEvaluatorEngine;
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
                //if we don't instantiate and pass one ourselves, then one will be instantiated one for each binding, which will cause problems
                WatchdogDatabaseContainer dbContainer = new WatchdogDatabaseContainer();
                kernel.Bind<Repository<Message>>().To<EFMessageRepository>().WithConstructorArgument("container", dbContainer);
                kernel.Bind<Repository<Alert>>().To<EFAlertRepository>().WithConstructorArgument("container", dbContainer);
                kernel.Bind<Repository<Rule>>().To<EFRuleRepository>().WithConstructorArgument("container", dbContainer);
                kernel.Bind<IRuleEngine>().To<StandardRuleEngine>();

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
