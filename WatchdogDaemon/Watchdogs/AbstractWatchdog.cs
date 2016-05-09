using System;
using Ninject;
using Ninject.Syntax;
using WatchdogDaemon.RuleEngine;
using WatchdogDatabaseAccessLayer.Models;
using WatchdogDatabaseAccessLayer.Repositories;
using WatchdogDatabaseAccessLayer.Repositories.Database;

namespace WatchdogDaemon.Watchdogs
{
    public abstract class AbstractWatchdog : IDisposable
    {
        protected AbstractWatchdog(IResolutionRoot kernel)
        {
            MessageRepository = kernel.Get<IRepository<Message>>();
            RuleRepository = kernel.Get<IRepository<Rule>>();
            AlertRepository = kernel.Get<IRepository<Alert>>();
            RuleEngine = kernel.Get<IRuleEngine>();
        }

        protected readonly IRepository<Alert> AlertRepository;
        protected readonly IRepository<Message> MessageRepository;
        protected readonly IRepository<Rule> RuleRepository;
        protected readonly IRuleEngine RuleEngine;

        public void Dispose()
        {
            MessageRepository.Dispose();
        }

        public abstract void Watch();
        public abstract void StopWatching();
        protected abstract void Run(object state);
    }
}