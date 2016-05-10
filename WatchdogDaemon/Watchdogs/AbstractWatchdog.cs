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
            MessageRepository = kernel.Get<WatchdogDatabaseAccessLayer.Repositories.Repository<Message>>();
            RuleRepository = kernel.Get<WatchdogDatabaseAccessLayer.Repositories.Repository<Rule>>();
            AlertRepository = kernel.Get<WatchdogDatabaseAccessLayer.Repositories.Repository<Alert>>();
            RuleEngine = kernel.Get<IRuleEngine>();
        }

        protected readonly WatchdogDatabaseAccessLayer.Repositories.Repository<Alert> AlertRepository;
        protected readonly WatchdogDatabaseAccessLayer.Repositories.Repository<Message> MessageRepository;
        protected readonly WatchdogDatabaseAccessLayer.Repositories.Repository<Rule> RuleRepository;
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